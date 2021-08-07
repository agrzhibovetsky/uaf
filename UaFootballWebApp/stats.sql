/* ---- Stadium - Attendence % ---- */
select Match_Id, CAST(m.Spectators as float)/(CAST(s.Capacity as float)) as perc
from Matches m
join Stadiums s on m.Stadium_Id = s.Stadium_Id
order by perc desc

/* ---- Players - count % ---- */
select COUNT(*) from Players

/* ---- Players - most goals % ---- */
select p.Last_Name, COUNT(*) as gc from MatchEvents me
join Players p on me.Player1_Id = p.Player_Id
where Event_Cd='G'
group by p.Last_Name
order by gc desc

/* ---- Players - most yellow cards % ---- */
select p.Last_Name, COUNT(*) as gc from MatchEvents me
join Players p on me.Player1_Id = p.Player_Id
where Event_Cd='Y1'
group by p.Last_Name
order by gc desc

/* ---- Substs - most popular minute % ---- */
select COUNT(*) as gc, Minute from MatchEvents 
where Event_Cd='S'
group by Minute
order by gc desc, Minute

/* --- ALL UNT players ----*/
select p.Player_Id, p.First_Name, p.Last_Name, COUNT(*) as mc from MatchLineups ml
join Matches m on ml.Match_Id = m.Match_Id
join Players p on ml.Player_Id = p.Player_Id
where ((IsHomeTeamPlayer=1 AND m.HomeNationalTeam_Id=1) OR (IsHomeTeamPlayer=0 AND m.AwayNationalTeam_Id=1))
group by p.Player_Id, p.First_Name, p.Last_Name
order by mc desc

select p.Player_Id, p.First_Name, p.Last_Name, COUNT(*) as mc from MatchLineups ml
join Matches m on ml.Match_Id = m.Match_Id
join Players p on ml.Player_Id = p.Player_Id
where p.Country_Id<>1
group by p.Player_Id, p.First_Name, p.Last_Name
order by mc desc

select p.Last_Name, p.First_Name, COUNT(*) as c from dbo.MatchLineups ml
join Matches m on ml.Match_Id = m.Match_Id
join Players p on ml.Player_Id = p.Player_Id
where ((m.AwayClub_Id = 7 AND ml.IsHomeTeamPlayer=0) OR (m.HomeClub_Id =7 AND ml.IsHomeTeamPlayer=1)) AND m.Season_Id=9
group by p.Last_Name, p.First_Name
order by c desc


/* UNT goals without uploaded video*/
select m.Match_ID, m.Date, m.HomeTeam, m.AwayTeam, p.First_Name, p.Last_Name, case when me.EventFlags=0 and mt.MultimediaTag_ID is not null THEN 'Missing tags' ELSE 'Missing video' END [Desription] from MatchEvents me
join Players p on me.Player1_Id = p.Player_Id
join vwMatches m on me.Match_Id = m.Match_Id
left outer join MultimediaTags mt on mt.MatchEvent_ID = me.MatchEvent_Id
where me.Match_Id in
(
select Match_Id from Matches
where AwayNationalTeam_Id =1 or HomeNationalTeam_Id = 1 
)
and (Event_Cd='G' or Event_Cd='P') and p.Country_Id = 1 and EventFlags & 128 =0 and (mt.MultimediaTag_ID is null or me.EventFlags=0)
order by m.Date desc


/* UNT - number of photos */
select pCount.cnt, m.Date, m.HomeTeam, m.AwayTeam, m.HomeScore, m.AwayScore
from
       vwMatches m
	   left outer join 
	   (
			select mt.Match_ID, count(*) cnt from
			Multimedia mm
			join MultimediaTags mt on mm.Multimedia_ID = mt.Multimedia_ID
			where mm.MultimediaSubType_CD='MP'
			group by mt.Match_ID
	   ) pCount on pCount.Match_ID = m.Match_ID
Where 
		(m.HomeNationalTeam_Id=1 or m.AwayNationalTeam_Id = 1) --AND pCount.cnt IS NULL	
Order by m.Date desc	

/* eurocup season - # of photos */
select ISNULL(d.PhotoCount,0), g.HomeTeam, g.AwayTeam, g.Date from 
vwMatches g left outer join
(
select count(*) as PhotoCount, g.Match_Id from 
Multimedia m join
MultimediaTags mt on m.Multimedia_ID = mt.Multimedia_ID
join Matches g on mt.Match_ID = g.Match_Id
where m.MultimediaSubType_CD = 'MP'
group by g.Match_Id) d
on g.Match_Id = d.Match_Id
where g.Season_Id=29 and PhotoCount is null--24--19--17 
order by g.Date desc

/* eurocup season - goals without uploaded video or tags */
select me.MatchEvent_ID, me.Event_Cd, me.Minute, hc.Club_Name, ac.Club_Name, p.First_Name, p.Last_Name, mt.MultimediaTag_ID, EventFlags 
from MatchEvents me
join Players p on me.Player1_Id = p.Player_Id
left outer join MultimediaTags mt on mt.MatchEvent_ID = me.MatchEvent_Id
join Matches m on me.Match_Id = m.Match_Id
join Clubs hc on m.HomeClub_Id = hc.Club_ID
join Clubs ac on m.AwayClub_Id = ac.Club_ID
join Cities hct on hct.City_ID = hc.City_ID
join Countries hcn on hct.Country_ID = hcn.Country_ID
join Cities act on act.City_ID = ac.City_ID
join Countries acn on act.Country_ID = acn.Country_ID
join MatchLineups ml on ml.Match_Id = m.Match_ID and ml.Player_Id = me.Player1_Id
where me.Match_Id in 
(
select Match_Id from Matches where Season_Id=29
)
and (Event_Cd='G' or Event_Cd='P') and ((ml.IsHomeTeamPlayer = 1 and hcn.Country_ID=1) or (ml.IsHomeTeamPlayer = 0 and acn.Country_ID=1))
and EventFlags & 128 =0 and (MultimediaTag_ID is null or EventFlags=0)
order by me.Match_Id


/*all dynamo players  dk=2, shahta=7, dnipro=17*/

select distinct(p.Player_Id), p.First_Name, p.Last_Name, p.Display_Name, ml.ShirtNumber 
from MatchLineups ml
join Matches m on ml.Match_Id = m.Match_Id
join Players p on ml.Player_Id = p.Player_Id
where ((m.HomeClub_Id = 2 and ml.IsHomeTeamPlayer = 1) or (m.AwayClub_Id = 2 and ml.IsHomeTeamPlayer = 0))
order by ShirtNumber


/* most popular shirt numbers*/
select COUNT(*) as c, ShirtNumber 
from
(
select distinct(p.Player_Id), p.First_Name, p.Last_Name, p.Display_Name, ml.ShirtNumber 
from MatchLineups ml
join Matches m on ml.Match_Id = m.Match_Id
join Players p on ml.Player_Id = p.Player_Id 
join Competitions c on m.Competition_Id = C.Competition_Id
--where p.Country_Id=1 and c.CompetitionLevel_Cd='C'
) as subq
group by ShirtNumber
--order by c desc
order by ShirtNumber

/*plaers that used shit number N */
select distinct(p.Player_Id), p.First_Name, p.Last_Name, p.Display_Name, ml.ShirtNumber 
from MatchLineups ml
join Matches m on ml.Match_Id = m.Match_Id
join Players p on ml.Player_Id = p.Player_Id 
join Competitions c on m.Competition_Id = C.Competition_Id
where ShirtNumber=10 and p.Country_Id=1 and c.CompetitionLevel_Cd='N'


/*players-coaches*/

select p.First_Name, p.Last_Name, c.FirstName, c.LastName, p.* 
from Players p 
join Coaches c on p.DOB=c.DOB and p.Country_Id=c.CountryId



/**-- players - logo count- */
select count(*) as c, First_Name, Last_Name, p.Player_Id
From Multimedia m
JOIN MultimediaTags mt on mt.Multimedia_ID = m.Multimedia_ID
JOIN PLayers p on mt.Player_ID= p.Player_Id
where MultimediaSubType_CD='PL' and mt.Player_ID is not null and m.DateTaken is null
group by mt.Player_ID, p.First_Name, p.Last_Name, p.Player_Id
order by c desc

/* UNT cocaches and Captains */
select c.FirstName + ' '+ c.LastName as Coach, p.First_Name + ' ' + p.Last_Name as Captain, m.Match_ID, m.Date, m.HomeTeam, m.AwayTeam, m.HomeScore, m.AwayScore 
from UaFootball.dbo.vwMatches m
left outer join 
	(select Coach_Id, Match_Id, IsHomeTeamPlayer from 
	 UaFootball.dbo.MatchLineups where Coach_Id is not null) Coaches
	 on m.Match_ID = Coaches.Match_Id and (Coaches.IsHomeTeamPlayer = CASE WHEN m.HomeNationalTeam_Id=1 THEN 1 ELSE 0 END)
left outer join UaFootball.dbo.Coaches c on Coaches.Coach_Id = c.CoachId
left outer join 
	(select Player_Id, Match_Id, IsHomeTeamPlayer from 
	 UaFootball.dbo.MatchLineups where Flags & 2 >0) Captains
	 on m.Match_ID = Captains.Match_Id and (Captains.IsHomeTeamPlayer = CASE WHEN m.HomeNationalTeam_Id=1 THEN 1 ELSE 0 END)
left outer join UaFootball.dbo.Players p on Captains.Player_Id = p.Player_Id
where HomeNationalTeam_Id = 1 or AwayNationalTeam_Id=1
order by m.Date desc


/*players by region*/
SELECT count(*) as c, UARegion_Name
  FROM [UaFootball].[dbo].[Players] 
  where Country_ID=1 and UARegion_Name is not null
group by UARegion_Name
order by c
