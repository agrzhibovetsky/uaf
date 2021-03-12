using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Matches
    {
        public Matches()
        {
            MatchEvents = new HashSet<MatchEvents>();
            MatchLineups = new HashSet<MatchLineups>();
            MatchNotes = new HashSet<MatchNotes>();
            MultimediaTags = new HashSet<MultimediaTags>();
        }

        public int MatchId { get; set; }
        public int? HomeClubId { get; set; }
        public int? AwayClubId { get; set; }
        public int? HomeNationalTeamId { get; set; }
        public int? AwayNationalTeamId { get; set; }
        public short HomeScore { get; set; }
        public short AwayScore { get; set; }
        public short? HomePenaltyScore { get; set; }
        public short? AwayPenaltyScore { get; set; }
        public int CompetitionId { get; set; }
        public int? CompetitionStageId { get; set; }
        public int SeasonId { get; set; }
        public int StadiumId { get; set; }
        public int? Spectators { get; set; }
        public int? RefereeId { get; set; }
        public DateTime Date { get; set; }
        public int? Flags { get; set; }
        public string SpecialNote { get; set; }
        public string AdminNotes { get; set; }
        public string Sources { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Clubs AwayClub { get; set; }
        public NationalTeams AwayNationalTeam { get; set; }
        public Competitions Competition { get; set; }
        public CompetitionStages CompetitionStage { get; set; }
        public Clubs HomeClub { get; set; }
        public NationalTeams HomeNationalTeam { get; set; }
        public Referees Referee { get; set; }
        public Seasons Season { get; set; }
        public Stadiums Stadium { get; set; }
        public ICollection<MatchEvents> MatchEvents { get; set; }
        public ICollection<MatchLineups> MatchLineups { get; set; }
        public ICollection<MatchNotes> MatchNotes { get; set; }
        public ICollection<MultimediaTags> MultimediaTags { get; set; }
    }
}
