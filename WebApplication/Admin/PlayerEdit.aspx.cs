using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Data.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using UaFootball.AppCode;
using UaFootball.DB;
using UaFootball.WebApplication.Controls;

namespace UaFootball.WebApplication
{
    public partial class PlayerEdit : ObjectEditPageBase<PlayerDTO>
    {
        protected override string ObjectListPage
        {
            get { return DataItem.RequiresReview.GetValueOrDefault(false) ? Constants.Pages.List_Player_Review : Constants.Pages.List_Player; }
        }

        protected override void PrepareUI()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                BindDropdown(db, Constants.ObjectType.Country, ddlCountries, false, string.Empty);
                for (int i = 1; i < 32; i++)
                {
                    ddlDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                string[] months = new string[12] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

                for (int i = 0; i < 12; i++)
                {
                    ddlMonth.Items.Add(new ListItem(months[i], (i+1).ToString()));
                }

                for (int i = DateTime.Now.Year - 62; i < DateTime.Now.Year-15; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                ddlYear.SelectedValue = (DateTime.Now.Year - 25).ToString();

            }
        }

        protected string PlayerPhotoPath
        {
            get
            {
                return ResolveClientUrl(Constants.Paths.DropBoxWebPath);
            }
        }

        protected override void DTOToUI(PlayerDTO dtoObj)
        {
            if (dtoObj.Player_Id > 0)
            {

                tbFirstName.Text = dtoObj.First_Name;
                tbLastName.Text = dtoObj.Last_Name;
                tbMiddleName.Text = dtoObj.Middle_Name;
                if (dtoObj.DOB.HasValue)
                {
                    ddlDay.SelectedValue = dtoObj.DOB.Value.Day.ToString();
                    ddlMonth.SelectedValue = dtoObj.DOB.Value.Month.ToString();
                    ddlYear.SelectedValue = dtoObj.DOB.Value.Year.ToString();
                }
                tbHeight.Text = dtoObj.Height.HasValue ? dtoObj.Height.ToString() : string.Empty;
                tbWeight.Text = dtoObj.Weight.HasValue ? dtoObj.Weight.ToString() : string.Empty;
                tbFirstNameInt.Text = dtoObj.First_Name_Int;
                tbLastNameInt.Text = dtoObj.Last_Name_Int;
                tbDisplayName.Text = dtoObj.Display_Name;
                ddlCountries.SelectedValue = dtoObj.Country_Id.ToString();
                
                rptLogos.DataSource = dtoObj.Multimedia;
                rptLogos.DataBind();
                tbCity.Text = dtoObj.UACity;
                if (!dtoObj.UARegion.IsEmpty())
                {
                    ddlObl.Items.FindByText(dtoObj.UARegion).Selected = true;
                }
            }
        }

        protected override PlayerDTO UIToDTO()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            

            if (tbFirstNameInt.Text.IndexOf(' ') > 0 && tbLastNameInt.Text.Length == 0)
            {
                string[] names = tbFirstNameInt.Text.Split(' ');
                tbFirstNameInt.Text = names[0];
                tbLastNameInt.Text = names[1];
            }
            PlayerDTO playerToSave = new PlayerDTO()
            {
                Country_Id = int.Parse(ddlCountries.SelectedValue),
                First_Name = tbFirstName.Text.Trim(),
                Last_Name = tbLastName.Text.Trim(),
                Middle_Name = tbMiddleName.Text.Trim(),
                //Photo = tbPhoto.Text.Trim(),
                Weight = tbWeight.Text.IsEmpty() ? null : (int?)int.Parse(tbWeight.Text),
                Height = tbHeight.Text.IsEmpty() ? null : (int?)int.Parse(tbHeight.Text),
                Player_Id = DataItem.Player_Id,
                //Multimedia = DataItem.Multimedia,
                RequiresReview = false,
                First_Name_Int = textInfo.ToTitleCase(tbFirstNameInt.Text.Trim().ToLower()),
                Last_Name_Int = textInfo.ToTitleCase(tbLastNameInt.Text.Trim().ToLower()),
                UACity = tbCity.Text.IsEmpty() ? null : tbCity.Text,
                UARegion = ddlObl.SelectedValue.Equals("0") ? null : ddlObl.SelectedItem.Text,
                Display_Name = tbDisplayName.Text.IsEmpty() ? null : tbDisplayName.Text
                
            };

            string fullName = playerToSave.First_Name_Int.Trim() + ' ' + playerToSave.Last_Name_Int.Trim();
            if (fullName.Length > 1)
            {
                playerToSave.NameSearchString = fullName.ToNormalizedASCIIString();
            }

            if (!cbUnknownDOB.Checked)
            {
                playerToSave.DOB = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue), int.Parse(ddlDay.SelectedValue));
            }
            return playerToSave;
        }

        protected void rptLogos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Image iLogo = e.Item.FindControl("imgLogo") as Image;
            MultimediaDTO mLogo = e.Item.DataItem as MultimediaDTO;
            if (iLogo != null && mLogo != null)
            {
                iLogo.ImageUrl = PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, mLogo.FilePath, mLogo.FileName);
            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            int multimediaToDeleteID = int.Parse(e.CommandArgument.ToString());
            MultimediaDTO multimediaToDelete = DataItem.Multimedia.First(p => p.Multimedia_ID == multimediaToDeleteID);
            DataItem.Multimedia.Remove(multimediaToDelete);
            rptLogos.DataSource = DataItem.Multimedia;
            rptLogos.DataBind();
        }

        protected void rptDuplicates_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DB.Player data = e.Item.DataItem as DB.Player;
            HighlightedLabel hl = e.Item.FindControl("hl") as HighlightedLabel;
            
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            string First_Name_Int = textInfo.ToTitleCase(tbFirstNameInt.Text.Trim().ToLower());
            string Last_Name_Int = textInfo.ToTitleCase(tbLastNameInt.Text.Trim().ToLower());
            string LastName = textInfo.ToTitleCase(tbLastName.Text.Trim().ToLower());
            if (hl != null && data != null)
            {
                //hl.Text = data.NameSearchString;
                if (string.IsNullOrWhiteSpace(tbLastNameInt.Text))
                {
                    hl.Text = data.Last_Name + ", " + data.First_Name;
                    hl.TextToHighlight = (LastName);
                }
                else
                {
                    hl.Text = (data.Last_Name_Int.ToNormalizedASCIIString() + ", " + data.First_Name_Int.ToNormalizedASCIIString());
                    hl.TextToHighlight = (First_Name_Int + ' ' + Last_Name_Int).ToNormalizedASCIIString();
                }
                
            }
        }

        protected void btnSearchByCountry_Click(object sender, EventArgs e)
        {
            int countryId = int.Parse(ddlCountries.SelectedValue);
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                var playersList = from player in db.Players where player.Country_Id == countryId && !(player.RequiresReview ?? false) orderby player.Last_Name_Int, player.Last_Name select player;
                rptSearchPlayers.Visible = true;
                rptSearchPlayers.DataSource = playersList;
                rptSearchPlayers.DataBind();
                rptDuplicates.Visible = false;
                rptDuplicates.DataSource = null;
                rptDuplicates.DataBind();
            }
        }

        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                PlayerDTO playerToSave = UIToDTO();
                List<PlayerDTO> duplicatecandidates = new List<PlayerDTO>();
                using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
                {
                    DataLoadOptions opt = new DataLoadOptions();
                    opt.LoadWith<UaFootball.DB.Player>(p => p.Country);
                                       
                    // первые кандидаты - совпадение по дате рождения
                    List<UaFootball.DB.Player> candidates = db.Players.Where(p => p.DOB == playerToSave.DOB).ToList();

                    List<string> nameChecks = new List<string>();
                    //ищем по имени только если оно составное
                    if (playerToSave.First_Name_Int.Length > 0 && playerToSave.First_Name_Int.Trim().IndexOf(' ') > 0)
                    {
                        nameChecks.AddRange(playerToSave.First_Name_Int.ToNormalizedASCIIString().Split(' '));
                    }

                    if (playerToSave.Last_Name_Int.Length > 0)
                    {
                        nameChecks.AddRange(playerToSave.Last_Name_Int.ToNormalizedASCIIString().Split(' ').Where(s => s.Length > 3));
                    }
                    
                    foreach (string name in nameChecks)
                    {
                        candidates.AddRange(db.Players.Where(p => (p.NameSearchString.Contains(name))));
                    }
                    //для кириллических имен - поиск по кириллическому имени
                    if (string.IsNullOrWhiteSpace(playerToSave.Last_Name_Int))
                    {
                        candidates.AddRange(db.Players.Where(p=>p.Last_Name == playerToSave.Last_Name));
                    }

                    candidates.RemoveAll(p => p.Player_Id == playerToSave.Player_Id);

                    foreach (DB.Player pl in candidates)
                    {
                        pl.Weight = 0;
                        
                        if (!string.IsNullOrEmpty(pl.NameSearchString))
                        {
                            string[] nameSplit = pl.NameSearchString.Split(' ');
                            int intersectCount = (nameSplit.Intersect(nameChecks)).Count();
                            
                            if (pl.DOB == playerToSave.DOB)
                            {
                                pl.Weight +=2;
                            }
                            if (pl.Country_Id.ToString() == ddlCountries.SelectedValue)
                            {
                                pl.Weight++;
                            }
                            pl.Weight += intersectCount > 0 ?  intersectCount : -1;
                            if (pl.First_Name_Int == tbFirstNameInt.Text.Trim() && pl.Last_Name_Int == tbLastNameInt.Text.Trim())
                            {
                                pl.Weight++;
                            }

                            if (pl.Weight < 0) pl.Weight = 0;
                        }
                    }
                    if (candidates.Count > 0)
                    {
                        rptDuplicates.Visible = true;
                        rptDuplicates.DataSource = candidates.Distinct().OrderByDescending(c => c.Weight).ThenBy(c=>c.Last_Name_Int).ThenBy(c=>c.First_Name_Int);
                        rptDuplicates.DataBind();
                        btnSaveOverride.Visible = true;
                        rptSearchPlayers.DataSource = null;
                        rptSearchPlayers.DataBind();

                    }
                    else
                    {
                        SaveObject(btnSave, new EventArgs());
                    }

                }
            }
        }

        protected string CheckAndDisplayDOB(object date)
        {
            if (date != null)
            {
                PlayerDTO p = UIToDTO();
                DateTime dt = (DateTime)date;
                if (p.DOB.Equals(dt)) return dt.ToShortDateString();
            }
            return string.Empty;
        }

        protected void cvDOB_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int day = int.Parse(ddlDay.SelectedValue);
            int month = int.Parse(ddlMonth.SelectedValue);
            int year = int.Parse(ddlYear.SelectedValue);
            try
            {
                DateTime DOB = new DateTime(year, month, day);
                args.IsValid = true;
            }
            catch (Exception ex)
            {
                args.IsValid = false;
            }

        }

        protected void cvSymbols_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string name = tbFirstNameInt.Text.Trim() + ' ' + tbLastNameInt.Text.Trim();
            bool isValid = true;
            int i = 0;
            while (isValid && i < name.Length)
            {
                if (!Constants.extraSymbols.Contains(name[i])) isValid = false;
                i++;
            }
            args.IsValid = isValid;
        }
    } 
}