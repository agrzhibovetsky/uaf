using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class Multimedia
    {
        public Multimedia()
        {
            MultimediaTags = new HashSet<MultimediaTags>();
        }

        public int MultimediaId { get; set; }
        public string MultimediaTypeCd { get; set; }
        public string MultimediaSubTypeCd { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Source { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public long? Flags { get; set; }
        public DateTime? DateTaken { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public ICollection<MultimediaTags> MultimediaTags { get; set; }
    }
}
