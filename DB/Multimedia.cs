using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFootball.AppCode;

namespace UaFootball.DB
{
    public partial class Multimedia
    {
        public MultimediaDTO ToDTO()
        {
            return new MultimediaDTO
            {
                Multimedia_ID = Multimedia_ID,
                FileName = FileName,
                FilePath = FilePath,
                MultimediaSubType_CD = MultimediaSubType_CD,
                MultimediaType_CD = MultimediaType_CD,
                Tags = MultimediaTags.Select(mt=>mt.ToDTO()).ToList()
            };
        }
    }
}