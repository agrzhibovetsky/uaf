using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public string Type { get; set; }
        public string Description { get; set; }
        public int tmpId { get; set; }
        public int MultimediaTag_ID { get; set; }
    }
}