using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    [Serializable]
    public class ClubDTO
    {
        public int Club_ID { get; set; }

        public string Club_Name { get; set; }

        public string Display_Name { get; set; }

        //public string Logo { get; set; }

        public int? Year_Found { get; set; }

        public int City_ID { get; set; }

        public string City_Name { get; set; }

        public MultimediaDTO Logo { get; set; }

        public bool IsUA { get; set; }

        public ClubDTO()
        {
        }
    } 
}