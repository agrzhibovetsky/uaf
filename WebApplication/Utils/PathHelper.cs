using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using UaFootball.DB;

namespace UaFootball.AppCode
{
    public class PathHelper
    {

        public static string GetFullWebPath(string rootPath, string relativePath, string fileName)
        {
            string path = string.Format("{0}/{1}/{2}/{3}", Constants.Paths.FullWebRoot, rootPath, relativePath.Trim(), fileName.Trim());
            path = path.Replace(@"\", "/");
            return path;
        }

        public static string GetWebPath(Page sender, string rootPath, string relativePath, string fileName)
        {
            string path = string.Format("{0}/{1}/{2}", rootPath, relativePath.Trim(), fileName.Trim());
            path = path.Replace(@"\", "/");
            return sender.ResolveClientUrl(path);
        }

        public static string GetFileSystemPath(string rootPath, string relativePath, string fileName)
        {
            string path = string.Format("{0}\\{1}\\{2}", rootPath, relativePath.Trim(), fileName.Trim());
            path = path.Replace(@"\\", @"\");
            return path;
        }

        public static string GetMultimediaRelativePath(Multimedia mmedia)
        {
            string path = string.Empty;

            switch (mmedia.MultimediaSubType_CD)
            {
                case Constants.DB.MutlimediaSubTypes.MatchPhoto:
                case Constants.DB.MutlimediaSubTypes.MatchVideo:
                    {
                        if (mmedia.MultimediaTags.Count(mm => mm.Match_ID != null) == 1)
                        {
                            int matchId = mmedia.MultimediaTags.Single(mm => mm.Match_ID != null).Match_ID.Value;
                            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
                            {
                                DataLoadOptions dlo = new DataLoadOptions();
                                dlo.LoadWith<DB.Match>(m => m.Competition);
                                dlo.LoadWith<DB.Match>(m => m.Season);
                                DB.Match match = db.Matches.Single(m => m.Match_Id == matchId);
                                string clubTypeString = match.HomeNationalTeam_Id.HasValue ? "NationalTeam" : "Eurocups";

                                path = string.Format("\\{0}\\{1}\\{2}\\{3}\\", "Matches", clubTypeString, match.Season.Season_Description, match.Competition.Competition_Cd);
                            }
                        }
                        break;
                    }
                case Constants.DB.MutlimediaSubTypes.ClubLogo:
                    return "\\CL\\";
                case Constants.DB.MutlimediaSubTypes.PlayerLogo:
                    return "\\PL\\";
                case Constants.DB.MutlimediaSubTypes.NationalTeamLogo:
                    return "\\NTL\\";
            }

            return path;
        }
    }
}