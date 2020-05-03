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
select m.Match_ID, m.HomeTeam, m.AwayTeam, p.First_Name, p.Last_Name from MatchEvents me
join Players p on me.Player1_Id = p.Player_Id
join vwMatches m on me.Match_Id = m.Match_Id
left outer join MultimediaTags mt on mt.MatchEvent_ID = me.MatchEvent_Id
where me.Match_Id in
(
select Match_Id from Matches
where AwayNationalTeam_Id =1 or HomeNationalTeam_Id = 1 
)
and (Event_Cd='G' or Event_Cd='P') and p.Country_Id = 1 and mt.MultimediaTag_ID is null and EventFlags & 128 =0
order by me.Match_Id

/* eurocup season - goals without uploaded video */
select me.MatchEvent_ID, me.Event_Cd, me.Minute, hc.Club_Name, ac.Club_Name, p.First_Name, p.Last_Name, mt.MultimediaTag_ID 
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
select Match_Id from Matches where Season_Id=19
)
and (Event_Cd='G' or Event_Cd='P') and ((ml.IsHomeTeamPlayer = 1 and hcn.Country_ID=1) or (ml.IsHomeTeamPlayer = 0 and acn.Country_ID=1))
and EventFlags & 128 =0 and MultimediaTag_ID is null
order by me.Match_Id


/* eurocup season - # of photos */
select d.PhotoCount, g.HomeTeam, g.AwayTeam, g.Date from 
vwMatches g left outer join
(
select count(*) as PhotoCount, g.Match_Id from 
Multimedia m join
MultimediaTags mt on m.Multimedia_ID = mt.Multimedia_ID
join Matches g on mt.Match_ID = g.Match_Id
where m.MultimediaSubType_CD = 'MP'
group by g.Match_Id) d
on g.Match_Id = d.Match_Id
where g.Season_Id=24--19--17
order by g.Date desc


/*all dynamo players  dk=2, shahta=7, dnipro=17*/

select distinct(p.Player_Id), p.First_Name, p.Last_Name, p.Display_Name, ml.ShirtNumber from MatchLineups ml
join Matches m on ml.Match_Id = m.Match_Id
join Players p on ml.Player_Id = p.Player_Id
where ((m.HomeClub_Id = 7 and ml.IsHomeTeamPlayer = 1) or (m.AwayClub_Id = 7 and ml.IsHomeTeamPlayer = 0))
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

/*--UNT captains, including missing--*/
select top 1000 
  matches.Match_ID, HomeTeamCountryCode, AwayTeamCountryCode, First_Name, Last_Name
  from 
	(select * from vwMatches where (AwayTeamCountryCode='UA' or HomeTeamCountryCode='UA') And CompetitionLevel_Cd='N') as matches
   left outer join
     (	select p.*, Match_Id from MatchLineups ml 
		left outer join Players p on ml.Player_Id = p.Player_Id
		where Flags & 2 >0 and p.Country_Id=1
	 ) as captains on matches.Match_ID = captains.Match_Id
	order by Date