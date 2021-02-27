using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFDatabase;

namespace UaFootball.AppCode
{
    [Serializable]
    public class MatchLineupDTO
    {
        public int MatchLineup_Id { get; set; }

        public int? Player_Id {get; set;}

        public string Player_FirstName { get; set; }

        public string Player_FirstName_Int { get; set; }

        public string Player_LastName { get; set; }

        public string Player_LastName_Int { get; set; }

        public string Player_DisplayName { get; set; }

        public int Player_CountryId { get; set; }

        public int? ShirtNum {get; set;}

        public bool IsHomeTeamPlayer { get; set; }

        public bool IsSubstitute { get; set; }

        public int Match_Id { get; set; }

        public int? CoachId { get; set; }

        public string Coach_FirstName { get; set; }

        public string Coach_LastName { get; set; }

        public int Flags { get; set; }

        public int MatchNo { get; set; }

        public bool didntPlay { get; set; }

        public bool cameAsSubstitute { get; set; }

        public bool wasSubstituted { get; set; }

        public int minutesPlayed { get; set; }

        public void CopyDTOToDbObject(MatchLineup dbObj)
        {
            dbObj.Match_Id = Match_Id;
            dbObj.IsSubstitute = IsSubstitute;
            dbObj.IsHomeTeamPlayer = IsHomeTeamPlayer;
            dbObj.Player_Id = Player_Id;
            dbObj.ShirtNumber = ShirtNum;
            dbObj.MatchLineup_Id = MatchLineup_Id;
            dbObj.Coach_Id = CoachId;
            dbObj.Flags = Flags;
        }

        public MatchLineupDTO ConvertDBObjectToDTO(MatchLineup dbObj)
        {
            return new MatchLineupDTO
            {
                IsHomeTeamPlayer = dbObj.IsHomeTeamPlayer,
                IsSubstitute = dbObj.IsSubstitute,
                Match_Id = dbObj.Match_Id,
                MatchLineup_Id = dbObj.MatchLineup_Id,
                Player_Id = dbObj.Player_Id,
                ShirtNum = dbObj.ShirtNumber,
                CoachId = dbObj.Coach_Id,
                Flags = dbObj.Flags ?? 0
            };
        }
    }
}