using System;
using System.Collections.Generic;
using System.Text;

namespace UaFDatabaseEF.DTO
{
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
       
    }
}
