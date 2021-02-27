using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    [Serializable]
    public class CityDTO
    {
        public int City_ID { get; set; }

        public string City_Name { get; set; }

        public int Country_ID { get; set; }

        public string Country_Name { get; set; }

        public CityDTO()
        {

        }
    } 
}