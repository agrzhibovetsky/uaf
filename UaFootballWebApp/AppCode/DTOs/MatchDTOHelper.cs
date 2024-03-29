﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using UaFDatabase;

namespace UaFootball.AppCode
{
    /// <summary>
    /// Summary description for MatchDTOHelper
    /// </summary>
    public class MatchDTOHelper : IDTOHelper<MatchDTO>
    {
        public MatchDTO ConvertDBObjectToDTO(Match dbObj)
        {
            MatchDTO dtoObj = new MatchDTO()
            {
                AwayClub_Id = dbObj.AwayClub_Id,
                AwayNationalTeam_Id = dbObj.AwayNationalTeam_Id,
                AwayPenaltyScore = dbObj.AwayPenaltyScore,
                AwayScore = dbObj.AwayScore,
                Competition_Id = dbObj.Competition_Id,
                CompetitionStage_Id = dbObj.CompetitionStage_Id,
                Date = dbObj.Date,
                HomeClub_Id = dbObj.HomeClub_Id,
                HomeNationalTeam_Id = dbObj.HomeNationalTeam_Id,
                HomePenaltyScore = dbObj.HomePenaltyScore,
                HomeScore = dbObj.HomeScore,
                Match_Id = dbObj.Match_Id,
                Referee = dbObj.Referee_Id == null ? null : new RefereeDTO
                {
                    Referee_Id = dbObj.Referee_Id.Value
                },
                Season_Id = dbObj.Season_Id,
                Spectators = dbObj.Spectators,
                Stadium = new StadiumDTO {Stadium_ID = dbObj.Stadium_Id},
                IsNationalTeamMatch = dbObj.AwayNationalTeam_Id.HasValue,
                Flags = dbObj.Flags,
                SpecialNote = dbObj.SpecialNote,
                AdminNotes = dbObj.AdminNotes,
                Sources = dbObj.Sources,
                CreatedAt = dbObj.CreatedDate
            };

            return dtoObj;
        }

        public MatchDTO ConvertDBObjectToDTO(vwMatch dbObj)
        {
            MatchDTO dtoObj = new MatchDTO()
            {
                AwayClub_Id = dbObj.AwayClub_Id,
                AwayNationalTeam_Id = dbObj.AwayNationalTeam_Id,
                AwayPenaltyScore = dbObj.AwayPenaltyScore,
                AwayScore = dbObj.AwayScore,
                //Competition_Id = dbObj.Competition_Id,
                //CompetitionStage_Id = dbObj.CompetitionStage_Id,
                CompetitionStageName = dbObj.CompetitionStage_Name,
                Date = dbObj.Date,
                HomeClub_Id = dbObj.HomeClub_Id,
                HomeNationalTeam_Id = dbObj.HomeNationalTeam_Id,
                HomePenaltyScore = dbObj.HomePenaltyScore,
                HomeScore = dbObj.HomeScore,
                Match_Id = dbObj.Match_ID,
                //Referee_Id = dbObj.Referee_Id,
                Season_Id = dbObj.Season_Id,
                //Spectators = dbObj.Spectators,
                //Stadium_Id = dbObj.Stadium_Id,
                HomeTeamName = dbObj.HomeTeam,
                AwayTeamName = dbObj.AwayTeam,
                HomeTeamCountryCode = dbObj.HomeTeamCountryCode,
                AwayTeamCountryCode = dbObj.AwayTeamCountryCode,
                CompetitionName = dbObj.Competition_Name,
                SeasonName = dbObj.Season_Description,
                CompetitionLevelCode = dbObj.CompetitionLevel_Cd,
                IsNationalTeamMatch = dbObj.CompetitionLevel_Cd.Equals(Constants.DB.CompetitionLevelCd_NationalTeam),
                Flags = dbObj.Flags,
                Spectators = dbObj.Spectators
            };

            return dtoObj;
        }

        public void CopyDTOToDbObject(MatchDTO dtoObj, Match dbObj)
        {
            dbObj.AwayClub_Id = dtoObj.AwayClub_Id;
            dbObj.AwayNationalTeam_Id = dtoObj.AwayNationalTeam_Id;
            dbObj.AwayPenaltyScore = dtoObj.AwayPenaltyScore;
            dbObj.AwayScore = dtoObj.AwayScore;
            dbObj.Competition_Id = dtoObj.Competition_Id;
            dbObj.CompetitionStage_Id = dtoObj.CompetitionStage_Id;
            dbObj.Date = dtoObj.Date;
            dbObj.HomeClub_Id = dtoObj.HomeClub_Id;
            dbObj.HomeNationalTeam_Id = dtoObj.HomeNationalTeam_Id;
            dbObj.HomePenaltyScore = dtoObj.HomePenaltyScore;
            dbObj.HomeScore = dtoObj.HomeScore;
            dbObj.Match_Id = dtoObj.Match_Id;
            dbObj.Referee_Id = dtoObj.Referee == null ? null : (int?)dtoObj.Referee.Referee_Id;
            dbObj.Season_Id = dtoObj.Season_Id;
            dbObj.Spectators = dtoObj.Spectators;
            dbObj.Stadium_Id = dtoObj.Stadium.Stadium_ID;
            dbObj.Spectators = dtoObj.Spectators;
            dbObj.Flags = dtoObj.Flags;
            dbObj.SpecialNote = dtoObj.SpecialNote;
            dbObj.AdminNotes = dtoObj.AdminNotes;
            dbObj.Sources = dtoObj.Sources;

            foreach (MatchLineupDTO l in dtoObj.Lineup)
            {

                if ((l.MatchLineup_Id > 0) && dbObj.MatchLineups.Any(ml => ml.MatchLineup_Id == l.MatchLineup_Id))  //update existing record
                {
                    MatchLineup mlToUpdate = dbObj.MatchLineups.Single(ml => ml.MatchLineup_Id == l.MatchLineup_Id);
                    l.CopyDTOToDbObject(mlToUpdate);
                }
                else //add new record
                {
                    if (l.Player_Id > 0 || l.CoachId > 0)
                    {
                        MatchLineup mlToAdd = new MatchLineup();
                        l.CopyDTOToDbObject(mlToAdd);
                        dbObj.MatchLineups.Add(mlToAdd);
                    }
                }

            }

            foreach (MatchEventDTO e in dtoObj.Events)
            {
                if ((e.MatchEvent_Id > 0) && dbObj.MatchEvents.Any(me=>me.MatchEvent_Id == e.MatchEvent_Id))
                {
                    MatchEvent meToUpdate = dbObj.MatchEvents.Single(me => me.MatchEvent_Id == e.MatchEvent_Id);
                    e.CopyDTOToDbObject(meToUpdate);
                }
                else
                {
                    MatchEvent eToAdd= new MatchEvent();
                    e.CopyDTOToDbObject(eToAdd);
                    dbObj.MatchEvents.Add(eToAdd);
                }
            }

            foreach (MatchNoteDTO note in dtoObj.Notes)
            {
                MatchNote noteToAdd = new MatchNote { Code = note.Code, Text = note.Text };
                dbObj.MatchNotes.Add(noteToAdd);
            }

        }

        public MatchDTO GetFromDB(int objectId)
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                var dbData = (from match in db.Matches
                              where match.Match_Id == objectId
                              select new { m = match, r = match.Referee, s = match.Stadium, cn = match.Stadium.City.City_Name, compName = match.Competition.Competition_Name, compStageName = match.CompetitionStage.CompetitionStage_Name }).Single();
                
                MatchDTO ret = ConvertDBObjectToDTO(dbData.m);

                if (dbData.r != null)
                {
                    ret.Referee = new RefereeDTOHelper().ConvertDBObjectToDTO(dbData.r);
                }

                ret.Stadium = new StadiumDTOHelper().ConvertDBObjectToDTO(dbData.s);
                ret.Stadium.City_Name = dbData.cn;

                ret.CompetitionName = dbData.compName;
                ret.CompetitionStageName = dbData.compStageName;
                
                if (ret.HomeNationalTeam_Id.HasValue)
                {
                    ret.HomeTeamName = db.NationalTeams.Single(nt => nt.NationalTeam_Id == ret.HomeNationalTeam_Id).Country.Country_Name;
                    ret.AwayTeamName = db.NationalTeams.Single(nt => nt.NationalTeam_Id == ret.AwayNationalTeam_Id).Country.Country_Name;
                    MultimediaTag homeTeamLogoTag = db.MultimediaTags.Where(t => t.NationalTeam_ID == ret.HomeNationalTeam_Id && t.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.NationalTeamLogo).OrderByDescending(m => m.Multimedia_ID).FirstOrDefault();
                    if (homeTeamLogoTag != null)
                        ret.HomeTeamLogo = MultimediaDTO.FromDBObject(homeTeamLogoTag.Multimedia);
                    MultimediaTag awayTeamLogoTag = db.MultimediaTags.Where(t => t.NationalTeam_ID == ret.AwayNationalTeam_Id && t.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.NationalTeamLogo).OrderByDescending(m => m.Multimedia_ID).FirstOrDefault();
                    if (awayTeamLogoTag != null)
                        ret.AwayTeamLogo = MultimediaDTO.FromDBObject(awayTeamLogoTag.Multimedia);
                }
                else
                {
                    ret.HomeTeamName = db.Clubs.Single(c => c.Club_ID == ret.HomeClub_Id).Club_Name;
                    ret.AwayTeamName = db.Clubs.Single(c => c.Club_ID == ret.AwayClub_Id).Club_Name;
                    ret.HomeTeamLogo = db.MultimediaTags.Where(t => t.Club_ID == ret.HomeClub_Id && t.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.ClubLogo).OrderByDescending(m => m.Multimedia_ID).Select(m => MultimediaDTO.FromDBObject(m.Multimedia)).FirstOrDefault();
                    ret.AwayTeamLogo = db.MultimediaTags.Where(t => t.Club_ID == ret.AwayClub_Id && t.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.ClubLogo).OrderByDescending(m => m.Multimedia_ID).Select(m => MultimediaDTO.FromDBObject(m.Multimedia)).FirstOrDefault();
                }

                IQueryable<MatchLineupDTO> lineup = from matchLineup in db.MatchLineups
                                                 where matchLineup.Match_Id == objectId //&& matchLineup.Player_Id>0
                                                 orderby matchLineup.IsSubstitute, matchLineup.MatchLineup_Id
                                                 select new MatchLineupDTO
                                                 {
                                                     IsHomeTeamPlayer = matchLineup.IsHomeTeamPlayer,
                                                     IsSubstitute = matchLineup.IsSubstitute,
                                                     Match_Id = matchLineup.Match_Id,
                                                     Player_Id = matchLineup.Player_Id,
                                                     MatchLineup_Id = matchLineup.MatchLineup_Id,
                                                     Player_FirstName = matchLineup.Player.First_Name,
                                                     Player_LastName = matchLineup.Player.Last_Name,
                                                     Player_DisplayName = matchLineup.Player.Display_Name,
                                                     Player_FirstName_Int = matchLineup.Player.First_Name_Int,
                                                     Player_LastName_Int = matchLineup.Player.Last_Name_Int,
                                                     Player_CountryId = matchLineup.Coach_Id.HasValue ? 0 : matchLineup.Player.Country_Id,
                                                     ShirtNum = matchLineup.ShirtNumber,
                                                     CoachId = matchLineup.Coach_Id,
                                                     Coach_FirstName = matchLineup.Coach.FirstName,
                                                     Coach_LastName = matchLineup.Coach.LastName,
                                                     Flags = matchLineup.Flags ?? 0
                                                 };
                ret.Lineup.AddRange(lineup.AsEnumerable());



                IQueryable<MatchEventDTO> events = from matchEvent in db.MatchEvents
                                                   where matchEvent.Match_Id == objectId
                                                   select new MatchEventDTO
                                                   {
                                                       Event_Cd = matchEvent.Event_Cd,
                                                       EventFlags = matchEvent.EventFlags,
                                                       Match_Id = objectId,
                                                       MatchEvent_Id = matchEvent.MatchEvent_Id,
                                                       Minute = matchEvent.Minute,
                                                       Player1 = new PlayerDTO
                                                       {
                                                           First_Name = matchEvent.Player.First_Name,
                                                           Last_Name = matchEvent.Player.Last_Name,
                                                           Display_Name = matchEvent.Player.Display_Name,
                                                           Country_Id = matchEvent.Player.Player_Id > 0 ? matchEvent.Player.Country_Id : 0
                                                       },
                                                       Player1_Id = matchEvent.Player1_Id,
                                                       //Player1_FName = matchEvent.Player.First_Name,
                                                       //Player1_SName = matchEvent.Player.Last_Name,
                                                       //Player1_DName = matchEvent.Player.Display_Name,
                                                       //Player2_FName = matchEvent.Player1.First_Name,
                                                       //Player2_SName = matchEvent.Player1.Last_Name,
                                                       //Player2_DName = matchEvent.Player1.Display_Name,
                                                       Player2_Id = matchEvent.Player2_Id,
                                                       Player2 = new PlayerDTO
                                                       {
                                                           First_Name =matchEvent.Player1.First_Name,
                                                           Last_Name = matchEvent.Player1.Last_Name,
                                                           Display_Name = matchEvent.Player1.Display_Name,
                                                           Country_Id = matchEvent.Player1.Player_Id > 0 ? matchEvent.Player1.Country_Id : 0
                                                       },
                                                       Coach_Id = matchEvent.CoachId,
                                                       Coach = new CoachDTO
                                                       {
                                                           FirstName = matchEvent.Coach.FirstName,
                                                           LastName = matchEvent.Coach.LastName
                                                       }
                                                   };
                ret.Events.AddRange(events.AsEnumerable());

                IQueryable<MultimediaDTO> multimedia = (from m in db.Multimedias
                                                        join mt in db.MultimediaTags on m.Multimedia_ID equals mt.Multimedia_ID
                                                        where mt.Match_ID == objectId
                                                        select m).Distinct().Select(m=> new MultimediaDTO
                                                        { Multimedia_ID = m.Multimedia_ID, MultimediaType_CD = m.MultimediaType_CD});

                ret.Multimedia = new List<MultimediaDTO>();
                ret.Multimedia.AddRange(multimedia);

                IQueryable<MatchNoteDTO> notes = from note in db.MatchNotes
                                                 where note.Match_Id == objectId
                                                 select new MatchNoteDTO { Code = note.Code.Trim(), Text = note.Text, MatchNote_Id = note.MatchNote_Id };
                ret.Notes.AddRange(notes);
                for(int i=0; i<ret.Notes.Count;i++)
                {
                    MatchNoteDTO note = ret.Notes[i];
                    note.CodeDescription = Constants.MatchNoteSetups.Single(s => s.Code == note.Code).Description;
                    note.RowIndex = i;
                }

                for (int i = 0; i < ret.Events.Count; i++)
                {
                    //need this hack to display event on match edit
                    if (ret.Events[i].Event_Cd == Constants.DB.EventTypeCodes.CoachYellowCard)
                    {
                        ret.Events[i].Player1.First_Name = ret.Events[i].Coach.FirstName;
                        ret.Events[i].Player1.Last_Name = ret.Events[i].Coach.LastName;
                        ret.Events[i].Player1.Player_Id = ret.Events[i].Coach_Id.Value;
                    }
                }

                return (ret);
            }
        }

        public int SaveToDB(MatchDTO dtoObj)
        {
            Match dbObj = new Match();

            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                if (dtoObj.Match_Id > 0)
                {
                    dbObj = db.Matches.Single(cc => cc.Match_Id == dtoObj.Match_Id);
                }
                else
                {
                    dbObj.CreatedDate = DateTime.Now;
                    db.Matches.InsertOnSubmit(dbObj);
                }

                CopyDTOToDbObject(dtoObj, dbObj);

                db.SubmitChanges();

                return dbObj.Match_Id;
            }
        }

        public void DeleteFromDB(int objectId)
        {
            using (var db = DBManager.GetDB())
            {
                Match c = db.Matches.Single(cc => cc.Match_Id == objectId);
                db.Matches.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
        }

        public List<MatchDTO> GetAllFromDB()
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                List<MatchDTO> ret = new List<MatchDTO>();
                var Matches = (from dbObj in db.Matches
                               orderby dbObj.Match_Id descending
                              select new
                              {
                                  match = dbObj,
                                  homeClubName = dbObj.Club.Club_Name,
                                  awayClubName = dbObj.Club1.Club_Name,
                                  homeNationalTeamName = dbObj.NationalTeam.Country.Country_Name,
                                  awayNationalTeamName = dbObj.NationalTeam1.Country.Country_Name
                              }).Take(25);

                foreach (var dbObj in Matches)
                {
                    MatchDTO newMatch = ConvertDBObjectToDTO(dbObj.match);
                    if (newMatch.HomeClub_Id != null)
                    {
                        newMatch.HomeTeamName = dbObj.homeClubName;
                        newMatch.AwayTeamName = dbObj.awayClubName;
                    }
                    else
                    {
                        newMatch.HomeTeamName = dbObj.homeNationalTeamName;
                        newMatch.AwayTeamName = dbObj.awayNationalTeamName;
                    }

                    ret.Add(newMatch);
                }

                return ret.ToList();
            }
        }

        public List<MatchDTO> GetListFromDB(SearchParameters.Match searchParam)
        {
            List<MatchDTO> ret = new List<MatchDTO>();
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                var dbData = from matchData in db.vwMatches select matchData;

                if (searchParam.CompetitionCode != null)
                {
                    dbData = dbData.Where(d => d.CompetitionLevel_Cd.Equals(searchParam.CompetitionCode));
                }

                if (searchParam.Competition_Id > 0)
                {
                    dbData = dbData.Where(d => d.Competition_Id == searchParam.Competition_Id);
                }

                if (searchParam.Season_Id > 0)
                {
                    dbData = dbData.Where(d => d.Season_Id == searchParam.Season_Id);
                }

                if (searchParam.Referee_Id > 0)
                {
                    dbData = dbData.Where(d => d.Referee_Id == searchParam.Referee_Id);

                }

                if (searchParam.Stadium_Id > 0)
                {
                    dbData = dbData.Where(d => d.Stadium_Id == searchParam.Stadium_Id);
                }

                if (searchParam.Coach_Id > 0)
                {
                    List<int> lineups = db.MatchLineups.Where(ml => ml.Coach_Id == searchParam.Coach_Id).Select(ml=>ml.Match_Id).ToList();
                    dbData = dbData.Where(d => lineups.Contains(d.Match_ID));
                }

                if (searchParam.Club_Id > 0)
                {
                    dbData = dbData.Where(d => d.HomeClub_Id == searchParam.Club_Id || d.AwayClub_Id == searchParam.Club_Id);
                }

                if (searchParam.NationalTeam_Id > 0)
                {
                    dbData = dbData.Where(d => d.HomeNationalTeam_Id == searchParam.NationalTeam_Id|| d.AwayNationalTeam_Id == searchParam.NationalTeam_Id);
                }

                dbData = dbData.OrderByDescending(d => d.Date);

                foreach (var md in dbData)
                {
                    ret.Add(ConvertDBObjectToDTO(md));
                }
            }

            return ret;
        }

        public MatchDTOHelper()
        {
            
        }
    } 
}