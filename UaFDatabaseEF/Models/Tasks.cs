using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Tasks
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        public string StatusCd { get; set; }
        public string TypeCd { get; set; }
        public string Comments { get; set; }
    }
}
