using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class PlayerPositions
    {
        public int PlayerPositionId { get; set; }
        public int PlayerId { get; set; }
        public string LineCd { get; set; }
        public string WingCd { get; set; }

        public Players Player { get; set; }
    }
}
