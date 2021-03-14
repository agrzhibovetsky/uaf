using System;
using System.Collections.Generic;
using System.Text;

namespace UaFDatabaseEF.DTO
{
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
    }
}
