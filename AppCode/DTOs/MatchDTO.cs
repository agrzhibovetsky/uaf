using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFootball.DB;

namespace UaFootball.AppCode
{

    /// <summary>
    /// Summary description for MatchDTO
    /// </summary>
    [Serializable]
    public class MatchDTO
    {

        public int Match_Id { get; set; }

        public int? HomeClub_Id { get; set; }

        public int? AwayClub_Id { get; set; }

        public int? HomeNationalTeam_Id { get; set; }

        public int? AwayNationalTeam_Id { get; set; }

        public short HomeScore { get; set; }

        public short AwayScore { get; set; }

        public short? HomePenaltyScore { get; set; }

        public short? AwayPenaltyScore { get; set; }

        public int Competition_Id { get; set; }

        public string CompetitionName { get; set; }

        public string CompetitionLevelCode { get; set; }

        public int Season_Id { get; set; }

        public string SeasonName { get; set; }

        //public int Stadium_Id { get; set; }

        public int? Spectators { get; set; }

        //public int? Referee_Id { get; set; }

        //public string RefereeName { get; set; }
        
        public DateTime Date { get; set; }

        public int? CompetitionStage_Id { get; set; }

        public string CompetitionStageName { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string HomeTeamCountryCode { get; set; }

        public string AwayTeamCountryCode { get; set; }

        public bool IsNationalTeamMatch { get; set; }

        public MultimediaDTO HomeTeamLogo { get; set; }

        public MultimediaDTO AwayTeamLogo { get; set; }

        public List<MatchLineupDTO> Lineup { get; set; }

        public List<MatchEventDTO> Events { get; set; }

        public StadiumDTO Stadium { get; set; }

        public RefereeDTO Referee { get; set; }

        public int? Flags { get; set; }

        public string SpecialNote { get; set; }

        public string AdminNotes { get; set; }

        public string Sources { get; set; }

        public int PhotoCount { get; set; }

        public int VideoCount { get; set; }

        public DateTime? CreatedAt { get; set; }

        public List<MultimediaDTO> Multimedia { get; set; }

        public List<MatchNoteDTO> Notes { get; set; }

        public MatchDTO()
        {
            Lineup = new List<MatchLineupDTO>();
            Events = new List<MatchEventDTO>();
            Notes = new List<MatchNoteDTO>();
        }
    } 
}