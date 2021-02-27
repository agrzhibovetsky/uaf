using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{

    /// <summary>
    /// Summary description for PlayerPositionDTO
    /// </summary>
    [Serializable]
    public class PlayerPositionDTO
    {
        public int PleayerPosition_Id { get; set; }

        public int Player_Id { get; set; }

        public string Line_Cd { get; set; }

        public string Wing_Cd { get; set; }

        public PlayerPositionDTO()
        {

        }
    } 
}