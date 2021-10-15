using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFDatabase;

namespace UaFootball.AppCode
{
    [Serializable]
    public class MultimediaTagDTO
    {
        public int? Player_ID { get; set; }
        public int? Match_ID { get; set; }
        public int? MatchEvent_ID { get; set; }
        public int? Club_ID { get; set; }
        public int? NationalTeam_ID { get; set; }
        public int? Coach_ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int tmpId { get; set; }
        public int MultimediaTag_ID { get; set; }

        public static MultimediaTagDTO FromDBObject(MultimediaTag mt)
        {
            return new MultimediaTagDTO
            {
                Club_ID = mt.Club_ID,
                Match_ID = mt.Match_ID,
                MatchEvent_ID = mt.MatchEvent_ID,
                NationalTeam_ID = mt.NationalTeam_ID,
                Player_ID = mt.Player_ID,
                Coach_ID = mt.CoachId
            };
        }
    }
    
}