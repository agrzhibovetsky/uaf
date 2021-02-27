using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{

    [Serializable]
    public class StadiumDTO
    {

        public int Stadium_ID { get; set; }

        public string Stadium_Name { get; set; }

        public int Capacity { get; set; }

        public int YearBuilt { get; set; }

        public int City_ID { get; set; }

        public string City_Name { get; set; }

        public string Country_Name { get; set; }

        public string Comments { get; set; }

        public DateTime? DateAdded { get; set; }

        public StadiumDTO()
        {

        }
    } 
}