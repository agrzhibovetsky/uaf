using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFootball.DB;

namespace UaFootball.AppCode
{
    [Serializable]
    public class MultimediaDTO
    {
        public int Multimedia_ID { get; set; }

        public char MultimediaType_CD { get; set; }

        public string MultimediaSubType_CD { get; set; }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public bool IsUploaded { get; set; }

        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateTaken { get; set; }

        public List<MultimediaTagDTO> Tags { get; set; }

        public MultimediaDTO()
        {
            Tags = new List<MultimediaTagDTO>();
        }

        public void CopyDTOToDbObject(Multimedia dbObj)
        {
            dbObj.Multimedia_ID = Multimedia_ID;
            dbObj.MultimediaType_CD = MultimediaType_CD;
            dbObj.MultimediaSubType_CD = MultimediaSubType_CD;
            dbObj.FileName = FileName;
            dbObj.FilePath = FilePath;
            dbObj.DateAdded = DateAdded;
            dbObj.DateTaken = DateTaken;
            dbObj.DateUpdated = DateUpdated;
        }
    }
}