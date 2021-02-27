using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;
using UaFDatabase;

namespace UaFootball.WebApplication.Public
{
    public partial class Video : System.Web.UI.Page
    {
        protected string FLOWPLAYER_URL
        {
            get { return PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, "", "flowplayer-3.2.18.swf"); }
        }

        protected string AUTOPLAY { get; set; }

        protected string DEFAULT_VIDEO_URL { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            AUTOPLAY = bool.FalseString.ToLower();
            DEFAULT_VIDEO_URL = "";

            List<VideoDTO> videosToPlay = new List<VideoDTO>();
            int matchId = 0;
            int eventId = 0;
            if (Request["MatchId"] != null)
            {
                
                if (int.TryParse(Request["MatchId"], out matchId))
                {
                    using (UaFootball_DBDataContext db = DBManager.GetDB())
                    {
                        var mm = from tag in db.MultimediaTags
                                 where tag.Match_ID == matchId && tag.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.MatchVideo
                                 select tag.Multimedia;
                        videosToPlay = mm.Select(m => new VideoDTO { Description = m.Multimedia_ID.ToString(), URL = PathHelper.GetFullWebPath("Multimedia", m.FilePath, m.FileName) }).ToList();
                    }
                }
            }
            else
            {
                if (Request["EventId"] != null)
                {
                    
                    if (int.TryParse(Request["EventId"], out eventId))
                    {
                        using (UaFootball_DBDataContext db = DBManager.GetDB())
                        {
                            var mm = from tag in db.MultimediaTags
                                     where tag.MatchEvent_ID == eventId && tag.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.MatchVideo
                                     select tag.Multimedia;
                            videosToPlay = mm.Select(m => new VideoDTO { Description = m.Multimedia_ID.ToString(), URL = PathHelper.GetFullWebPath("Multimedia", m.FilePath, m.FileName) }).ToList();
                        }
                    }
                }
            }

            if (eventId > 0)
            {
                if (videosToPlay.Count == 1)
                {
                    AUTOPLAY = bool.TrueString.ToLower();
                    DEFAULT_VIDEO_URL = videosToPlay[0].URL;
                }
            }
            else
            {
                if (videosToPlay.Count > 0)
                {
                    rptVideoSelection.DataSource = videosToPlay;
                    rptVideoSelection.DataBind();
                }
            }

        }
    }

    public class VideoDTO
    {
        public string Description {get; set;}
        public string URL {get; set;}
    }
}