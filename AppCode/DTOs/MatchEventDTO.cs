using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFDatabase;

namespace UaFootball.AppCode
{
    [Serializable]
    public class MatchEventDTO
    {
        public int MatchEvent_Id { get; set; }

        public string Event_Cd { get; set; }

        public int Minute { get; set; }

        public int Player1_Id { get; set; }

        public int? Player2_Id { get; set; }

        public int Match_Id { get; set; }

        public long? EventFlags { get; set; }


        public PlayerDTO Player1 { get; set; }

        public PlayerDTO Player2 { get; set; }
        //public string Player1_FName { get; set; }

        //public string Player2_FName { get; set; }

        //public string Player1_SName { get; set; }

        //public string Player2_SName { get; set; }

        //public string Player1_DName { get; set; }

        //public string Player2_DName { get; set; }

        public bool AppliesToSecondPlayer { get; set; }

        public bool HasVideo { get; set; }

        public MatchEventDTO()
        {
            Player1 = new PlayerDTO();
            Player2 = new PlayerDTO();
        }

        public void CopyDTOToDbObject(MatchEvent dbObj)
        {
            dbObj.Match_Id = Match_Id;
            dbObj.MatchEvent_Id = MatchEvent_Id;
            dbObj.Event_Cd = Event_Cd;
            dbObj.Player1_Id = Player1_Id;
            dbObj.Player2_Id = Player2_Id;
            dbObj.EventFlags = EventFlags;
            dbObj.Minute = Minute;
        }

        public MatchEventDTO ConvertDBObjectToDTO(MatchEvent dbObj)
        {
            return new MatchEventDTO
            {
                Match_Id = dbObj.Match_Id,
                MatchEvent_Id = dbObj.MatchEvent_Id,
                EventFlags = dbObj.EventFlags,
                Event_Cd = dbObj.Event_Cd,
                Player2_Id = dbObj.Player2_Id,
                Player1_Id = dbObj.Player1_Id,
                Minute = dbObj.Minute
            };
        }
    }
}