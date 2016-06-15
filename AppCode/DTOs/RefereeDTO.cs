using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{

    /// <summary>
    /// Summary description for RefereeDTO
    /// </summary>
    [Serializable]
    public class RefereeDTO
    {
        public int Referee_Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FirstName_EN { get; set; }

        public string LastName_EN { get; set; }

        public DateTime? DOB { get; set; }

        public int Country_Id { get; set; }

        public string CountryName { get; set; }

        public List<MatchDTO> Matches { get; set; }
        public RefereeDTO()
        {
            Matches = new List<MatchDTO>();
        }
    } 
}