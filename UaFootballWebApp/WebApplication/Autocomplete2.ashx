<%@ WebHandler Language="C#" Class="Autocomplete" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using UaFootball.AppCode;


namespace UaFootball.WebApplicaton
{
    public class Autocomplete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";

            string searchTerm = context.Request["term"];
            string searchType = context.Request["type"];
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<AutoCompleteResponse> result = new List<AutoCompleteResponse>();

            if (!searchTerm.IsEmpty() && !searchType.IsEmpty())
            {
                switch (searchType)
                {
                    default:
                        {
                            context.Response.Write(string.Empty);
                            break;
                        }
                    case Constants.AutoCompleteTypes.Logo:
                    case Constants.AutoCompleteTypes.PlayerPhoto:
                        {
                            string pathKey = (searchType == Constants.AutoCompleteTypes.PlayerPhoto) ? Constants.ConfigKeys.LocalPathPlayerPhotos : Constants.ConfigKeys.LocalPathLogos;

                            DirectoryInfo logosDirInfo = new DirectoryInfo(WebConfigurationManager.AppSettings[pathKey]);
                            if (logosDirInfo.Exists)
                            {
                                FileInfo[] files = logosDirInfo.GetFiles(searchTerm + "*");
                                result.AddRange(files.Select(f => new AutoCompleteResponse { id = 1, value = f.Name }));
                                context.Response.Write(serializer.Serialize(result));
                            }
                            else
                            {
                                context.Response.Write(string.Empty);
                            }

                            break;
                        }
                    case Constants.AutoCompleteTypes.Club:
                        {
                            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
                            {
                                IQueryable<AutoCompleteResponse> searchMatches = db.Clubs.Where(c => c.Club_Name.Contains(searchTerm)).Select(c => new AutoCompleteResponse { id = c.Club_ID, value = c.Club_Name });
                                result.AddRange(searchMatches);
                                context.Response.Write(serializer.Serialize(result));
                            }
                            break;
                        }
                    case Constants.AutoCompleteTypes.NationalTeam:
                        {
                            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
                            {
                                var searchMatches =
                                    from natTeam in db.NationalTeams
                                    join country in db.Countries on natTeam.Country_Id equals country.Country_ID
                                    where country.Country_Name.Contains(searchTerm)
                                    select new { id = natTeam.NationalTeam_Id, value = country.Country_Name, code = natTeam.NationalTeamType_Cd };

                                foreach (var item in searchMatches)
                                {
                                    result.Add(new AutoCompleteResponse { id = item.id, value = item.value + " " + UIHelper.FormatNationalTeamType(item.code) });
                                }

                                context.Response.Write(serializer.Serialize(result));
                            }
                            break;
                        }
                }
            }
            else
            {
                context.Response.Write(string.Empty);
            }







        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }

    public class AutoCompleteResponse
    {
        public string value { get; set; }
        public int id { get; set; }
    } 
}