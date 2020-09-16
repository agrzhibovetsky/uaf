using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    [Serializable]
    public class NationalTeamDTO
    {
        public int ID { get; set; }

        public string Country_Name { get; set; }

        public int County_ID { get; set; }

        public string Kind { get; set; }

        public MultimediaDTO Logo { get; set; }

        public NationalTeamDTO()
        {
        }
    } 
}