using System;
using System.Collections.Generic;

namespace UaFDatabaseEF.Models
{
    public partial class PhotoGalleryAlbums
    {
        public int Id { get; set; }
        public string ProviderCode { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public string ProviderInternalId { get; set; }
        public bool? IsUnt { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
