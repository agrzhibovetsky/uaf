using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    /// <summary>
    /// Summary description for PlayerDTO
    /// </summary>
    [Serializable]
    public class PlayerDTO
    {

        public int Player_Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Middle_Name { get; set; }

        public string Display_Name { get; set; }

        public DateTime? DOB { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public int Country_Id { get; set; }

        public string Country_Name { get; set; }

        public string First_Name_Int { get; set; }

        public string Last_Name_Int { get; set; }

        public string Photo { get; set; }

        public bool? RequiresReview { get; set; }

        public string UACity { get; set; }

        public string UARegion { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public List<PlayerPositionDTO> FieldPositions { get; set; }

        public List<MatchDTO> Matches { get; set; }

        public List<MultimediaDTO> Multimedia { get; set; }

        public string NameSearchString { get; set; }

        public PlayerDTO()
        {
            FieldPositions = new List<PlayerPositionDTO>();
            Matches = new List<MatchDTO>();
            Multimedia = new List<MultimediaDTO>();
        }
    } 
}