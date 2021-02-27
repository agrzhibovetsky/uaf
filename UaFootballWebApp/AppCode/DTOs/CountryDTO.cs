using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace UaFootball.AppCode
{

    /// <summary>
    /// Summary description for Country
    /// </summary>

    [Serializable]
    public class CountryDTO
    {

        public int Country_ID { get; set; }

        public string Country_Code { get; set; }

        public string Country_Name { get; set; }

        public int FIFAAssociation_ID { get; set; }

        public string FIFAAssociation_Name { get; set; }

        public CountryDTO()
        {

        }


    } 
}