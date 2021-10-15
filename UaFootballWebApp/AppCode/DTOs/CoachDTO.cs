using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    [Serializable]
    public class CoachDTO
    {
        public int CoachId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FirstName_EN { get; set; }

        public string LastName_EN { get; set; }

        public DateTime DOB { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int Country_Id { get; set; }

        public string CountryName { get; set; }

        public List<MultimediaDTO> Multimedia { get; set; }

        public List<MatchDTO> Matches { get; set; }
        public CoachDTO()
        {
            Matches = new List<MatchDTO>();
            Multimedia = new List<MultimediaDTO>();
        }
    }
}