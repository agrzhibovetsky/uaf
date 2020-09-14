using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using UaFootball.AppCode;
using UaFootball.DB;

namespace UaFootball.WebApplication
{
    public class UaFootballPageBase : System.Web.UI.Page
    {
        protected NameValueCollection Query;

        

        protected string AutoCompletePath
        {
            get
            {
                return ResolveClientUrl(Constants.Pages.Autocomplete);
            }
        }

        protected string FormatDate(object oDate)
        {
            return UIHelper.FormatDate(oDate);
        }

        protected string FormatDate(DateTime? date)
        {
            return UIHelper.FormatDate(date);
        }

        protected string FormatUACity(string cityName, string regionName)
        {
            if (!cityName.IsEmpty() && !regionName.IsEmpty())
            {
                if (regionName.IndexOf(cityName.Substring(0,4)) == 0)
                {
                    return cityName;
                }
                else
                {
                    return string.Concat(cityName, ", ", regionName);
                }
            }
            else
                if (!cityName.IsEmpty())
                {
                    return cityName;
                }
                else if (!regionName.IsEmpty())
                {
                    return regionName;
                }
                else
                    return string.Empty;
        }

        protected string FormatScore(object oHomeScore, object oAwayScore, object oHomePenScore, object oAwayPenScore)
        {
            return UIHelper.FormatScore(oHomeScore, oAwayScore, oHomePenScore, oAwayPenScore);
        }

        protected string FormatInt(object value)
        {
            if (value != null)
            {
                int iValue = (int)value;
                return iValue.ToString("#,#");
            }
            else return string.Empty;
        }

        protected string FormatName(object FirstName, object LastName, object DisplayName, int countryId)
        {
            return UIHelper.FormatName(FirstName, LastName, DisplayName, countryId);
        }

        protected string GetTeamSpanClass(object countryCode)
        {
            return UIHelper.GetTeamSpanClass(countryCode);
        }

        protected string GetQueryString()
        {
            return "?" + string.Join("&", Array.ConvertAll(Query.AllKeys, key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(Query[key]))));
        }

        protected void BindDropdown(UaFootball_DBDataContext db, Constants.ObjectType objectType, DropDownList ddl, bool addEmptyValue, string filter)
        {
            IEnumerable<GenericReferenceObject> dataSource = GetGenericReferenceData(db, objectType);
            if (filter.Length > 0) dataSource = dataSource.Where(d => d.GenericStringValue.Equals(filter));
            ddl.DataSource = dataSource;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Value";
            ddl.DataBind();
            if (addEmptyValue)
            {
                ddl.Items.Insert(0, new ListItem { Text = Constants.UI.DropdownDefaultText, Value = Constants.UI.DropdownDefaultValue });
            }
        }

        protected int? GetDropdownValue(DropDownList ddl)
        {
            int result;
            if (string.Compare(ddl.SelectedValue, Constants.UI.DropdownDefaultValue) == 0)
            {
                return null;
            }
            else
            {
                if (int.TryParse(ddl.SelectedValue, out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        protected IEnumerable<GenericReferenceObject> GetGenericReferenceData(UaFootball_DBDataContext db, Constants.ObjectType objectType)
        {
            IEnumerable<GenericReferenceObject> data = new List<GenericReferenceObject>().AsEnumerable();

            switch (objectType)
            {
                case Constants.ObjectType.FIFAAssociation:
                    {
                        data = db.FIFAAssociations.Select(f => new GenericReferenceObject { Name = f.FIFAAssociation_Name, Value = f.FIFAAssociation_ID });
                        break;
                    }
                case Constants.ObjectType.Country:
                    {
                        data = db.Countries.OrderBy(c=>c.Country_Name).Select(c => new GenericReferenceObject { Name = c.Country_Name, Value = c.Country_ID });
                        break;
                    }
                case Constants.ObjectType.City:
                    {
                        data = db.Cities.OrderBy(c=>c.City_Name).Select(c => new GenericReferenceObject { Name = c.City_Name, Value = c.City_ID });
                        break;
                    }
                case Constants.ObjectType.Competition:
                    {
                        data = db.Competitions.Select(c => new GenericReferenceObject { Name = c.Competition_Name, Value = c.Competition_Id, GenericStringValue = c.CompetitionLevel_Cd });
                        break;
                    }
                case Constants.ObjectType.Season:
                    {
                        data = db.Seasons.OrderByDescending(s=>s.StartDate).Select(s => new GenericReferenceObject { Name = s.Season_Description, Value = s.Season_Id, GenericStringValue = s.CompetitionLevel_Cd });
                        break;
                    }
                case Constants.ObjectType.Stadium:
                    {
                        data = db.Stadiums.OrderBy(s=>s.Stadium_Name).Select(s => new GenericReferenceObject { Name = string.Concat(s.Stadium_Name, " (", s.City.City_Name,")"), Value = s.Stadium_Id });
                        break;
                    }
                case Constants.ObjectType.CompetitionStage:
                    {
                        data = db.CompetitionStages.Select(c => new GenericReferenceObject { Name = c.CompetitionStage_Name, Value = c.CompetitionStage_ID, GenericStringValue = c.Competition_ID.ToString() });
                        break;
                    }
                default: break;
            }

            return data;
        }

        protected IDTOHelper<T> CreateDTOHelper<T>() where T : class, new()
        {
            switch (typeof(T).Name)
            {
                default:
                    {
                        return null;
                    }
                case "CountryDTO":
                    {
                        return new CountryDTOHelper() as IDTOHelper<T>;
                    }
                case "CityDTO":
                    {
                        return new CityDTOHelper() as IDTOHelper<T>;
                    }
                case "StadiumDTO":
                    {
                        return new StadiumDTOHelper() as IDTOHelper<T>;
                    }
                case "ClubDTO":
                    {
                        return new ClubDTOHelper() as IDTOHelper<T>;
                    }
                case "PlayerDTO":
                    {
                        return new PlayerDTOHelper() as IDTOHelper<T>;
                    }
                case "RefereeDTO":
                    {
                        return new RefereeDTOHelper() as IDTOHelper<T>;
                    }
                case "MatchDTO":
                    {
                        return new MatchDTOHelper() as IDTOHelper<T>;
                    }
                case "CoachDTO":
                    {
                        return new CoachDTOHelper() as IDTOHelper<T>;
                    }
            }
        }

        public UaFootballPageBase()
        {
            Query = new NameValueCollection();
        }


    } 
}