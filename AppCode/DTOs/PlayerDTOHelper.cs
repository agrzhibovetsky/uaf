using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UaFootball.DB;
using System.IO;

namespace UaFootball.AppCode
{
    /// <summary>
    /// Summary description for PlayerDTOHelper
    /// </summary>
    public class PlayerDTOHelper : IDTOHelper<PlayerDTO>
    {
        public PlayerDTO ConvertDBObjectToDTO(Player dbObj)
        {
            PlayerDTO dtoObj = new PlayerDTO()
            {
                Country_Id = dbObj.Country_Id,
                DOB = dbObj.DOB,
                First_Name = dbObj.First_Name,
                First_Name_Int = dbObj.First_Name_Int,
                Height = dbObj.Height,
                Last_Name = dbObj.Last_Name,
                Last_Name_Int = dbObj.Last_Name_Int,
                Middle_Name = dbObj.Middle_Name,
                Player_Id = dbObj.Player_Id,
                Weight = dbObj.Weight,
                RequiresReview = dbObj.RequiresReview,
                UACity = dbObj.UACity_Name,
                UARegion = dbObj.UARegion_Name,
                LastUpdateTime = dbObj.LastUpdate_DT,
                Display_Name = dbObj.Display_Name,
                NameSearchString = dbObj.NameSearchString
            };

            return dtoObj;
        }

        public void CopyDTOToDbObject(PlayerDTO dtoObj, Player dbObj)
        {
            dbObj.Player_Id = dtoObj.Player_Id;
            dbObj.First_Name = dtoObj.First_Name;
            dbObj.First_Name_Int = dtoObj.First_Name_Int;
            dbObj.DOB = dtoObj.DOB;
            dbObj.Height = dtoObj.Height;
            dbObj.Last_Name = dtoObj.Last_Name;
            dbObj.Last_Name_Int = dtoObj.Last_Name_Int;
            dbObj.Middle_Name = dtoObj.Middle_Name;
            dbObj.Weight = dtoObj.Weight;
            dbObj.Country_Id = dtoObj.Country_Id;
            dbObj.RequiresReview = dtoObj.RequiresReview;
            dbObj.UACity_Name = dtoObj.UACity;
            dbObj.UARegion_Name = dtoObj.UARegion;
            dbObj.LastUpdate_DT = DateTime.Now;
            dbObj.Display_Name = dtoObj.Display_Name;
            dbObj.NameSearchString = dtoObj.NameSearchString;
        }

        public PlayerDTO GetFromDB(int objectId)
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                var dbData = (from player in db.Players
                              where player.Player_Id == objectId
                              select new { p = player, c = player.Country.Country_Name }).Single();
                PlayerDTO ret = ConvertDBObjectToDTO(dbData.p);
                ret.Country_Name = dbData.c;

                ret.Multimedia = new List<MultimediaDTO>();

                var dbPlayerLogos = from mt in db.MultimediaTags
                                    where mt.Player_ID == objectId && mt.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.PlayerLogo
                                    select mt.Multimedia;

                
                foreach (Multimedia m in dbPlayerLogos)
                {
                    MultimediaDTO playerLogo = m.ToDTO();
                    playerLogo.IsUploaded = true;
                    ret.Multimedia.Add(playerLogo);
                }
                
                
                return (ret);
            }
        }

        public int SaveToDB(PlayerDTO dtoObj)
        {
            Player dbObj = new Player();

            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                if (dtoObj.Player_Id > 0)
                {
                    dbObj = db.Players.Single(cc => cc.Player_Id == dtoObj.Player_Id);
                }
                else
                {
                    db.Players.InsertOnSubmit(dbObj);
                }

                CopyDTOToDbObject(dtoObj, dbObj);

                /*IEnumerable<MultimediaTag> mTagsToDel = db.MultimediaTags.Where(t => t.Player_ID == dtoObj.Player_Id && t.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.PlayerLogo);
                db.MultimediaTags.DeleteAllOnSubmit(mTagsToDel);

                foreach (MultimediaDTO multimedia in dtoObj.Multimedia)
                {
                    Multimedia dbMultimedia = new Multimedia();
                    multimedia.CopyDTOToDbObject(dbMultimedia);

                    if (!multimedia.IsUploaded)
                    {
                        string mmRelativePath = PathHelper.GetMultimediaRelativePath(dbMultimedia);
                        string dropBoxFilePath = PathHelper.GetFileSystemPath(Constants.Paths.DropBoxPath, mmRelativePath, multimedia.FileName);

                        FileInfo fi = new FileInfo(dropBoxFilePath);
                        if (fi.Exists)
                        {
                            DateTime Now = DateTime.Now;
                            string newFileName = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", Now.Year, Now.Month, Now.Day, Now.Hour, Now.Minute, Now.Second, Now.Millisecond, fi.Extension);
                            string newFilePath = PathHelper.GetFileSystemPath(Constants.Paths.MultimediaStorageRoot, mmRelativePath, multimedia.FileName);

                            File.Copy(dropBoxFilePath, newFilePath);

                            dbMultimedia.FilePath = "";
                            dbMultimedia.FileName = newFileName;
                        }
                    }
                    
                    db.Multimedias.InsertOnSubmit(dbMultimedia);

                    MultimediaTag newTag = new MultimediaTag();
                    newTag.Multimedia = dbMultimedia;
                    newTag.Player = dbObj;
                    db.MultimediaTags.InsertOnSubmit(newTag);
                    
                }*/

                db.SubmitChanges();

                return dbObj.Player_Id;
            }
        }

        public void DeleteFromDB(int objectId)
        {
            using (var db = new UaFootball_DBDataContext())
            {
                Player c = db.Players.Single(cc => cc.Player_Id == objectId);
                db.Players.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
        }

        public List<PlayerDTO> GetAllFromDB()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                IEnumerable<PlayerDTO> players = from dbObj in db.Players
                                                 orderby dbObj.Last_Name
                                                 select new PlayerDTO
                                                 {
                                                     Country_Id = dbObj.Country_Id,
                                                     Country_Name = dbObj.Country.Country_Name,
                                                     DOB = dbObj.DOB,
                                                     First_Name = dbObj.First_Name,
                                                     First_Name_Int = dbObj.First_Name_Int,
                                                     Height = dbObj.Height,
                                                     Last_Name = dbObj.Last_Name,
                                                     Last_Name_Int = dbObj.Last_Name_Int,
                                                     Middle_Name = dbObj.Middle_Name,
                                                     Player_Id = dbObj.Player_Id,
                                                     Weight = dbObj.Weight,
                                                 };
                return players.ToList();
            }
        }

        public List<PlayerDTO> GetFromDB(string query, Constants.QueryType qType, bool uaOnly, bool includeOnReview)
        {
            List<PlayerDTO> players = new List<PlayerDTO>();

            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                IQueryable<Player> dbPlayers = null;

                if (qType == Constants.QueryType.StartsWith)
                {
                    dbPlayers = from dbObj in db.Players
                              where dbObj.Last_Name.StartsWith(query)
                              select dbObj;
                }
                else
                    if (qType == Constants.QueryType.All)
                    {
                        dbPlayers = from dbObj in db.Players select dbObj;
                    }
         
                if (uaOnly)
                {
                    dbPlayers = dbPlayers.Where(o => o.Country.Country_Code == Constants.CountryCodeUA);
                }

                if (!includeOnReview)
                {
                    dbPlayers = dbPlayers.Where(o => (o.RequiresReview == null) || (o.RequiresReview == false));
                }
                else
                {
                    dbPlayers = dbPlayers.Where(o => (o.RequiresReview == true));
                }

                var dbPlayers2 = from dbObj in dbPlayers
                                    orderby dbObj.Last_Name
                                    select new
                                    {
                                        p = dbObj,
                                        Country_Name = dbObj.Country.Country_Name
                                    };

                foreach (var dbPlayer in dbPlayers2)
                {
                    PlayerDTO player = ConvertDBObjectToDTO(dbPlayer.p);
                    player.Country_Name = dbPlayer.Country_Name;
                    players.Add(player);
                }
                
            }

            return players;
        }

        public PlayerDTO GetPlayer(int playerId)
        {
            using (var db = new UaFootball_DBDataContext())
            {
                DataLoadOptions dlo = new DataLoadOptions();
                dlo.LoadWith<Multimedia>(m => m.MultimediaTags);
                db.LoadOptions = dlo;

                var dbPlayer = (from player in db.Players
                               where player.Player_Id == playerId
                               select new
                               {
                                   p = player,
                                   Country_Name = player.Country.Country_Name
                               }).Single();

                PlayerDTO p = ConvertDBObjectToDTO(dbPlayer.p);
                p.Country_Name = dbPlayer.Country_Name;
                p.Matches = new List<MatchDTO>();


                //TODO - optimize db query
                var dbMatches = from match in db.vwMatches
                                join matchLineup in db.MatchLineups on match.Match_ID equals matchLineup.Match_Id
                                orderby match.Date
                                where matchLineup.Player_Id == playerId
                                select new
                                {
                                    M = match,
                                    ML = matchLineup
                                    //ME = from matchEvent in db.MatchEvents where matchEvent.Match_Id == match.Match_ID && (matchEvent.Player1_Id == playerId || matchEvent.Player2_Id == playerId) select matchEvent
                                    //match.MatchEvents.Where(me => me.Player1_Id == playerId || me.Player2_Id == playerId),
                                };
                var dbMatchEvents = from matchEvent in db.MatchEvents
                                    where matchEvent.Player1_Id == playerId || matchEvent.Player2_Id == playerId
                                    select new { ev = matchEvent, p1 = matchEvent.Player, p2 = matchEvent.Player1};

                foreach (var dbMatch in dbMatches)
                {
                    MatchDTO match = new MatchDTOHelper().ConvertDBObjectToDTO(dbMatch.M);
                    match.Lineup = new List<MatchLineupDTO>();
                    MatchLineupDTO ml = new MatchLineupDTO().ConvertDBObjectToDTO(dbMatch.ML);
                    
                    match.Lineup.Add(ml);
                    match.Events = new List<MatchEventDTO>();
                    foreach (var me in dbMatchEvents.Where(me=>me.ev.Match_Id == match.Match_Id))
                    {
                        MatchEventDTO meDTO = new MatchEventDTO().ConvertDBObjectToDTO(me.ev);
                        if (db.MultimediaTags.Any(mt => mt.MatchEvent_ID == me.ev.MatchEvent_Id && mt.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.MatchVideo)) meDTO.HasVideo = true;

                        if (meDTO.Player2_Id == playerId) meDTO.AppliesToSecondPlayer = true;
                        if (me.p1 != null)
                        {
                            meDTO.Player1 = new PlayerDTOHelper().ConvertDBObjectToDTO(me.p1);
                        }
                        if (me.p2 != null)
                        {
                            meDTO.Player2 = new PlayerDTOHelper().ConvertDBObjectToDTO(me.p2);
                        }
                        match.Events.Add(meDTO);
                    }

                    p.Matches.Add(match);

                }
                
                List<Multimedia> multimedia = (from tag in db.MultimediaTags
                                               join m in db.Multimedias on tag.Multimedia_ID equals m.Multimedia_ID
                                               where tag.Player_ID == playerId
                                               select m).ToList();
                List<Multimedia> multimediaVideo = (from tag in db.MultimediaTags
                                                    join matchEvent in db.MatchEvents on tag.MatchEvent_ID equals matchEvent.MatchEvent_Id
                                                    join m in db.Multimedias on tag.Multimedia_ID equals m.Multimedia_ID
                                                    where (matchEvent.Player1_Id == playerId || matchEvent.Player2_Id == playerId) && m.MultimediaType_CD == Constants.DB.MutlimediaTypes.Video
                                                    select m).ToList();
                                                   
                multimedia.AddRange(multimediaVideo);
                p.Multimedia = multimedia.Select(m => m.ToDTO()).ToList();

                foreach (Multimedia m in multimedia.Where(mm=>mm.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.MatchPhoto || mm.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.MatchVideo))
                {
                    int matchId = m.MultimediaTags.SingleOrDefault(mt=>mt.Match_ID!=null).Match_ID.Value;
                    if (m.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.MatchPhoto)
                        p.Matches.SingleOrDefault(match=>match.Match_Id == matchId).PhotoCount++;
                    else p.Matches.SingleOrDefault(match=>match.Match_Id == matchId).VideoCount++;
                }

                foreach (MatchDTO match in p.Matches)
                {
                    MatchLineupDTO lineup = match.Lineup[0];

                    lineup.didntPlay = false;
                    lineup.cameAsSubstitute = false;
                    lineup.wasSubstituted = false;
                    
                    int startMinute = -1;
                    if (lineup.IsSubstitute)
                    {
                        MatchEventDTO playerInEvent = match.Events.FirstOrDefault(me => (me.Event_Cd == Constants.DB.EventTypeCodes.Substitution) && (me.Player2_Id == playerId));
                        if (playerInEvent != null)
                        {
                            startMinute = playerInEvent.Minute == 46 ? 45 : playerInEvent.Minute;
                            lineup.cameAsSubstitute = true;
                        }
                        else
                        {
                            lineup.didntPlay = true;
                        }
                    }
                    else
                    {
                        startMinute = 0;
                    }

                    int finishMinute = 90;
                    if (match.Flags.HasValue && (match.Flags & Constants.DB.MatchFlags.Duration120Minutes) > 0) finishMinute = 120;

                    MatchEventDTO playerOutEvent = match.Events.FirstOrDefault(me => (me.Event_Cd == Constants.DB.EventTypeCodes.Substitution) && (me.Player1_Id == playerId));
                    if (playerOutEvent != null)
                    {
                        finishMinute = playerOutEvent.Minute == 46 ? 45 : playerOutEvent.Minute;
                        lineup.wasSubstituted = true;
                    }

                    MatchEventDTO redCardEvent = match.Events.FirstOrDefault(me => (me.Event_Cd == Constants.DB.EventTypeCodes.RedCard || me.Event_Cd == Constants.DB.EventTypeCodes.SecondYellowCard));
                    if (redCardEvent != null)
                    {
                        finishMinute = redCardEvent.Minute;
                    }

                    lineup.minutesPlayed = finishMinute - startMinute;
                    if (lineup.minutesPlayed == 0) lineup.minutesPlayed++;
                    
                }

                List<MatchDTO> orderedMatches = p.Matches.OrderBy(m => m.Date).ToList();
                int matchNo = 0;
                for (var i = 0; i < orderedMatches.Count; i++)
                {
                    if ((orderedMatches[i].Lineup[0].Flags & Constants.DB.LineupFlags.Debut) > 0)
                    {
                        matchNo = 1;
                        orderedMatches[i].Lineup[0].MatchNo = 1;
                    }
                    else
                    {
                        if (matchNo > 0 && !orderedMatches[i].Lineup[0].didntPlay && orderedMatches[i].CompetitionLevelCode==Constants.DB.CompetitionLevelCd_NationalTeam)
                        {
                            
                            matchNo++;
                            orderedMatches[i].Lineup[0].MatchNo = matchNo;
                            //p.Matches.Find(m => m.Match_Id == orderedMatches[i].Match_Id).Lineup[0].MatchNo = 3;
                        }
                    }
                }

                return p;
            }
        }

        public PlayerDTOHelper()
        {

        }
    } 
}