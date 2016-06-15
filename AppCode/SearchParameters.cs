using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    public class SearchParameters
    {
        public class Match
        {
            public string CompetitionCode { get; set; }

            public int Competition_Id { get; set; }

            public int Season_Id { get; set; }

            public int Referee_Id { get; set; }

            public int Stadium_Id { get; set; }
        }
    }
}