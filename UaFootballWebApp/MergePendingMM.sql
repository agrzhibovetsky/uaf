MERGE dbo.PendingMultimedia as Target
using
(select m.Match_ID, p.Player_Id, me.MatchEvent_Id, mt.Multimedia_ID, EventFlags, 'MV' as SubType
from MatchEvents me
join Players p on me.Player1_Id = p.Player_Id
join vwMatches m on me.Match_Id = m.Match_Id
left outer join MultimediaTags mt on mt.MatchEvent_ID = me.MatchEvent_Id
where me.Match_Id in
(
select Match_Id from Matches
where AwayNationalTeam_Id =1 or HomeNationalTeam_Id = 1 
)
and (Event_Cd='G' or Event_Cd='P') and p.Country_Id = 1 and EventFlags & 128 =0 
--order by m.Date desc
union 
select m.Match_ID, p.Player_Id,  me.MatchEvent_Id, mt.Multimedia_ID, EventFlags, 'MV' as SubType
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
where (Event_Cd='G' or Event_Cd='P') and ((ml.IsHomeTeamPlayer = 1 and hcn.Country_ID=1) or (ml.IsHomeTeamPlayer = 0 and acn.Country_ID=1))
and EventFlags & 128 =0 

) as Source
On Source.Match_Id  = Target.Match_Id and Source.MatchEvent_Id = Target.Event_Id
WHEN Not Matched Then
 INSERT ([Match_Id], [Event_Id], [Player_Id], [Multimedia_Id], [Multimedia_SubType_CD], [EventFlags])
 VALUES (Source.Match_Id, Source.MatchEvent_Id, Source.Player_Id, Source.Multimedia_ID, Source.SubType, Source.EventFlags)
WHEN MATCHED THEN
	UPDATE SET [Multimedia_Id] = Source.Multimedia_Id, [EventFlags] = Source.EventFlags
;