using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.AppCode;
using UaFDatabase;

namespace UaFootball.WebApplication.Admin
{
    public partial class Multimedia : UaFootballPageBase
    {
        const string _tagTypeClub = "Клуб";
        const string _tagTypeNatTeam = "Сборная";
        const string _tagTypePlayer = "Футболист";
        const string _tagTypeGame = "Матч";
        const string _tagTypeEvent = "Событие";

        const string _tmpUploadPath = "\\tmpUpload\\";

        #region Props
        protected string TmpUploadWebPath
        {
            get
            {
                return PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, _tmpUploadPath, "");
            }
        }

        private string UploadedFileName
        {
            get
            {
                return hfFileName.Value;
            }
        }

        private int MMId
        {
            get
            {
                int multimediaId = 0;

                int.TryParse(Request["id"], out multimediaId);

                return multimediaId;
            }
        }

        private List<MultimediaTagDTO> TagsCache
        {
            get
            {
                if (ViewState["tagsCache"] != null)
                    return ViewState["tagsCache"] as List<MultimediaTagDTO>;
                else
                {
                    List<MultimediaTagDTO> lst = new List<MultimediaTagDTO>();
                    ViewState["tagsCache"] = lst;
                    return lst;
                }
            }
        }

        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlMultimediaSubType.Items.Add(new ListItem(Constants.UI.MutlimediaSubTypes.ClubLogo, Constants.DB.MutlimediaSubTypes.ClubLogo));
                ddlMultimediaSubType.Items.Add(new ListItem(Constants.UI.MutlimediaSubTypes.NationalTeamLogo, Constants.DB.MutlimediaSubTypes.NationalTeamLogo));
                ddlMultimediaSubType.Items.Add(new ListItem(Constants.UI.MutlimediaSubTypes.PlayerLogo, Constants.DB.MutlimediaSubTypes.PlayerLogo));
                ddlMultimediaSubType.Items.Add(new ListItem(Constants.UI.MutlimediaSubTypes.MatchPhoto, Constants.DB.MutlimediaSubTypes.MatchPhoto));
                ddlMultimediaSubType.Items.Add(new ListItem(Constants.UI.MutlimediaSubTypes.MatchVideo, Constants.DB.MutlimediaSubTypes.MatchVideo));

                ddlMultimediaSubType.SelectedIndex = 3;
                ddlMultimediaSubType_SelectedIndexChanged(ddlMultimediaSubType, new EventArgs());

                cbl1.Items.Add(new ListItem(Constants.UI.MultimediaTags.BadQuality, Constants.DB.MultimediaTags.BadQuality.ToString()));
                cbl1.Items.Add(new ListItem(Constants.UI.MultimediaTags.AwayTeamPhoto, Constants.DB.MultimediaTags.AwayTeamPhoto.ToString()));
                cbl1.Items.Add(new ListItem(Constants.UI.MultimediaTags.HomeTeamPhoto, Constants.DB.MultimediaTags.HomeTeamPhoto.ToString()));

                

                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    int multimediaId = MMId;

                    if (multimediaId>0)
                    {
                        btnDelete.Visible = true;

                        using (UaFootball_DBDataContext db = DBManager.GetDB())
                        {
                            UaFDatabase.Multimedia mm = db.Multimedias.SingleOrDefault(m => m.Multimedia_ID == multimediaId);
                            if (mm != null)
                            {
                                ddlMultimediaSubType.Enabled = false;
                                afuUploader.Enabled = false;
                                hfFileName.Value = mm.FileName;
                                ddlMultimediaSubType.SelectedValue = mm.MultimediaSubType_CD;
                                ddlMultimediaSubType_SelectedIndexChanged(ddlMultimediaSubType, new EventArgs());

                                List<MultimediaTag> tags = db.MultimediaTags.Where(m => m.Multimedia_ID == multimediaId).ToList();
                                if (tags.Count > 0)
                                {
                                    List<MultimediaTagDTO> tagsToBind = tags.Select(t => new MultimediaTagDTO { Club_ID = t.Club_ID, MatchEvent_ID = t.MatchEvent_ID, Match_ID = t.Match_ID, NationalTeam_ID = t.NationalTeam_ID, Player_ID = t.Player_ID, tmpId = t.MultimediaTag_ID, MultimediaTag_ID = t.MultimediaTag_ID }).ToList();
                                    foreach (MultimediaTagDTO mt in tagsToBind)
                                    {
                                        if (mt.Match_ID.HasValue)
                                        {
                                            mt.Type = _tagTypeGame;
                                            vwMatch match = db.vwMatches.SingleOrDefault(m => m.Match_ID == mt.Match_ID.Value);
                                            mt.Description = UIHelper.FormatMatch(match);
                                        }

                                        if (mt.Player_ID.HasValue)
                                        {
                                            mt.Type = _tagTypePlayer;
                                            PlayerDTO player = new PlayerDTOHelper().GetPlayer(mt.Player_ID.Value);
                                            mt.Description = UIHelper.FormatName(player);
                                        }

                                        if (mt.MatchEvent_ID.HasValue)
                                        {
                                            mt.Type = _tagTypeEvent;
                                            DataLoadOptions opt = new DataLoadOptions();
                                            opt.LoadWith<MatchEvent>(m => m.Player1);
                                            UaFDatabase.MatchEvent mEvent = db.MatchEvents.SingleOrDefault(m => m.MatchEvent_Id == mt.MatchEvent_ID.Value);

                                            mt.Description = string.Format("{0} мин - {1} - {2}", mEvent.Minute, UIHelper.EventCodeMap[mEvent.Event_Cd], FormatName(mEvent.Player.First_Name, mEvent.Player.Last_Name, mEvent.Player.Display_Name, mEvent.Player.Country_Id));

                                        }

                                        if (mt.Club_ID.HasValue)
                                        {
                                            mt.Type = _tagTypeClub;
                                            ClubDTO club = new ClubDTOHelper().GetFromDB(mt.Club_ID.Value);
                                            mt.Description = string.Format("{0} ({1})", club.Club_Name, club.City_Name);
                                        }

                                        if (mt.NationalTeam_ID.HasValue)
                                        {
                                            mt.Type = _tagTypeNatTeam;
                                            mt.Description = "Украина";
                                        }

                                        
                                    }
                                    TagsCache.AddRange(tagsToBind);
                                    rptTags.DataSource = tagsToBind;
                                    rptTags.DataBind();
                                }

                                if (mm.Flags.HasValue)
                                {
                                    if (mm.Flags > 0)
                                    {
                                        foreach (ListItem li in cbl1.Items)
                                        {
                                            if ((long.Parse(li.Value) & mm.Flags.Value) > 0)
                                            {
                                                li.Selected = true;
                                            }
                                        }
                                    }
                                }

                                tbAuthor.Text = mm.Author;
                                tbSource.Text = mm.Source;
                                tbDescription.Text = mm.Description;
                                tbPhotoDate.Text = FormatDate(mm.DateTaken);
                                if (mm.MultimediaType_CD == Constants.DB.MutlimediaTypes.Image)
                                {
                                    imgMultimedia.ImageUrl = PathHelper.GetWebPath(this, Constants.Paths.MutlimediaWebRoot, mm.FilePath, mm.FileName);
                                }

                            }
                        }
                    }
                }
            }
        }

        #region UIInteraction - dropdowns, tb postbacks
        protected void ddlMultimediaSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlMMSubType = sender as DropDownList;
            ddlTagType.Items.Clear();
            switch (ddlMultimediaSubType.SelectedValue)
            {
                case Constants.DB.MutlimediaSubTypes.ClubLogo:
                    {
                        ddlTagType.Items.Add(new ListItem("Клуб", _tagTypeClub));
                        ddlTagType.Enabled = false;
                        break;
                    }
                case Constants.DB.MutlimediaSubTypes.NationalTeamLogo:
                    {
                        ddlTagType.Items.Add(new ListItem("Сборная", _tagTypeNatTeam));
                        ddlTagType.Enabled = false;
                        break;
                    }
                case Constants.DB.MutlimediaSubTypes.PlayerLogo:
                    {
                        ddlTagType.Items.Add(new ListItem("Футболист", _tagTypePlayer));
                        ddlTagType.Items.Add(new ListItem("Клуб", _tagTypeClub));
                        ddlTagType.Items.Add(new ListItem("Сборная", _tagTypeNatTeam));
                        ddlTagType.Enabled = true;
                        break;
                    }
                case Constants.DB.MutlimediaSubTypes.MatchPhoto:
                case Constants.DB.MutlimediaSubTypes.MatchVideo:
                    {
                        ddlTagType.Items.Add(new ListItem("Матч", _tagTypeGame));
                        ddlTagType.Items.Add(new ListItem("Футболист", _tagTypePlayer));
                        ListItem eventListItem = new ListItem("Событие", _tagTypeEvent);
                        eventListItem.Attributes.Add("isEventType","true");
                        ddlTagType.Items.Add(eventListItem);
                        
                        ddlTagType.Enabled = true;
                        break;
                    }
            }

            ddlTagType_SelectedIndexChanged(ddlTagType, new EventArgs());
        }

        
        protected void ddlTagType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlTagType = sender as DropDownList;
            ddlTagValue.Items.Clear();
            List<GenericReferenceObject> lTagValueSource = new List<GenericReferenceObject>();
            switch (ddlTagType.SelectedValue)
            {
                case _tagTypeClub:
                    {
                        using (UaFootball_DBDataContext db = DBManager.GetDB())
                        {
                            if (ddlMultimediaSubType.SelectedValue == Constants.DB.MutlimediaSubTypes.PlayerLogo)
                            {
                                MultimediaTagDTO playerTag = TagsCache.SingleOrDefault(tc => tc.Type == _tagTypePlayer);
                                if (playerTag != null)
                                {
                                    List<Player_GetClubsResult> clubs = db.Player_GetClubs(playerTag.Player_ID).ToList();
                                    if (clubs != null)
                                    {
                                        foreach (Player_GetClubsResult club in clubs)
                                        {
                                            lTagValueSource.Add(new GenericReferenceObject { Name = string.Format("{0} ({1})", club.Club, club.City), Value = club.Club_Id, });
                                        }
                                    }
                                }
                            }
                            else
                            {
                                lTagValueSource = db.Clubs.OrderByDescending(c => c.Club_ID).Select(c => new GenericReferenceObject { Name = c.Club_Name, GenericStringValue = c.City.City_Name, Value = c.Club_ID }).Take(20).ToList();
                                foreach (GenericReferenceObject go in lTagValueSource)
                                {
                                    go.Name = string.Format("{0} ({1})", go.Name, go.GenericStringValue);
                                }
                            }
                        }
                        break;
                    }
                case _tagTypeNatTeam:
                    {
                        using (UaFootball_DBDataContext db = DBManager.GetDB())
                        {
                            if (ddlMultimediaSubType.SelectedValue == Constants.DB.MutlimediaSubTypes.PlayerLogo)
                            {
                                lTagValueSource = new List<GenericReferenceObject>();
                                lTagValueSource = db.NationalTeams.Where(nt => nt.NationalTeamType_Cd == Constants.DB.NationalTeamTypeCd_National && nt.Country.Country_Code == Constants.CountryCodeUA).Select(c => new GenericReferenceObject { Name = c.Country.Country_Name, Value = c.NationalTeam_Id }).ToList();
                            }
                            else
                            {
                                lTagValueSource = db.NationalTeams.Where(nt => nt.NationalTeamType_Cd == Constants.DB.NationalTeamTypeCd_National).OrderBy(c => c.Country.Country_Name).Select(c => new GenericReferenceObject { Name = c.Country.Country_Name, Value = c.NationalTeam_Id }).ToList();
                            }
                                
                        }
                        break;
                    }
                case _tagTypePlayer:
                    {
                        using (UaFootball_DBDataContext db = DBManager.GetDB())
                        {
                            List<UaFDatabase.Player> lPlayers = new List<UaFDatabase.Player>();
                            if (ddlMultimediaSubType.SelectedValue == Constants.DB.MutlimediaSubTypes.PlayerLogo)
                            {
                                lPlayers = db.Players.OrderByDescending(c => c.Player_Id).Take(50).OrderBy(p=>p.Last_Name).ToList();
                                foreach (UaFDatabase.Player p in lPlayers)
                                {
                                    string fName = p.First_Name ?? "";
                                    string lName = p.Last_Name ?? "";
                                    string dName = string.IsNullOrEmpty(p.Display_Name) ? "" : string.Concat("(", p.Display_Name, ")");
                                    string name = Regex.Replace(string.Concat(fName, " ", lName, " ", dName), @"\s+", " ");
                                    lTagValueSource.Add(new GenericReferenceObject { Name = name, Value = p.Player_Id });
                                }
                            }
                            else
                            {
                                MultimediaTagDTO matchTag = TagsCache.SingleOrDefault(tc => tc.Type == _tagTypeGame);
                                if (matchTag != null)
                                {
                                    vwMatch match = db.vwMatches.Single(v => v.Match_ID == matchTag.Match_ID);
                                    bool homeTeamPriority = match.HomeTeamCountryCode == Constants.CountryCodeUA;
                                    var players = from matchLineup in db.MatchLineups
                                                  where matchLineup.Match_Id == matchTag.Match_ID && matchLineup.Player_Id!=null
                                                  select new { Player = matchLineup.Player, IsHomeTeam = matchLineup.IsHomeTeamPlayer, IsSubstitute = matchLineup.IsSubstitute, No = matchLineup.ShirtNumber };

                                    if (homeTeamPriority)
                                    {
                                        players = players.OrderByDescending(pl => pl.IsHomeTeam).ThenBy(pl=>pl.No);
                                    }
                                    else
                                    {
                                        players = players.OrderBy(pl => pl.IsHomeTeam).ThenBy(pl => pl.No);
                                    }

                                    foreach (var player in players)
                                    {
                                        GenericReferenceObject go = new GenericReferenceObject();
                                        go.Name = string.Format("{0} - {1}", player.No.ToString().PadLeft(2,' '), FormatName(player.Player.First_Name, player.Player.Last_Name, player.Player.Display_Name, player.Player.Country_Id));
                                        go.Value = player.Player.Player_Id;
                                        lTagValueSource.Add(go);
                                    }
                                }
                                
                            }

                            
                        }
                        break;
                    }
                case _tagTypeGame:
                    {
                        using (UaFootball_DBDataContext db = DBManager.GetDB())
                        {
                            var lst = (from match in db.vwMatches orderby match.Match_ID descending select match).Take(50);
                            lTagValueSource = lst.Select(l => new GenericReferenceObject { Name = string.Format("{0} | {1} - {2}", l.Date.ToShortDateString(), l.HomeTeam, l.AwayTeam), Value = l.Match_ID }).ToList();
                        }
                        break;
                    }
                case _tagTypeEvent:
                    {
                        MultimediaTagDTO matchTag = TagsCache.SingleOrDefault(tc => tc.Type == _tagTypeGame);
                        if (matchTag != null)
                        {
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                DataLoadOptions opt = new DataLoadOptions();
                                opt.LoadWith<MatchEvent>(me => me.Player1);

                                List<UaFDatabase.MatchEvent> events = db.MatchEvents.Where(me => me.Match_Id == matchTag.Match_ID).OrderBy(me=>me.Event_Cd).ThenBy(me=>me.Minute).ToList();
                                
                                foreach (UaFDatabase.MatchEvent mEvent in events)
                                {
                                    GenericReferenceObject go = new GenericReferenceObject();
                                    go.Name = string.Format("{0} мин - {1} - {2}", mEvent.Minute, UIHelper.EventCodeMap[mEvent.Event_Cd], FormatName(mEvent.Player.First_Name, mEvent.Player.Last_Name, mEvent.Player.Display_Name, mEvent.Player.Country_Id));
                                    go.Value = mEvent.MatchEvent_Id;
                                    go.GenericStringValue = mEvent.Event_Cd;
                                    lTagValueSource.Add(go);
                                }
                            }
                            
                        }
                        break;
                    }
            }

            ddlTagValue.Items.Clear();
            foreach (GenericReferenceObject o in lTagValueSource)
            {
                ListItem li = new ListItem(o.Name, o.Value.ToString());
                li.Attributes.Add("eventType", o.GenericStringValue);
                ddlTagValue.Items.Add(li);
            }
        }

        protected void tbObjectId_TextChanged(object sender, EventArgs e)
        {
            TextBox tbId = sender as TextBox;
            int id = 0;
            if (int.TryParse(tbId.Text, out id))
            {
                using (UaFootball_DBDataContext db = DBManager.GetDB())
                {
                    switch (ddlTagType.SelectedValue)
                    {
                        case _tagTypeClub:
                            {
                                Club c = db.Clubs.SingleOrDefault(cl=>cl.Club_ID == id);
                                if (c != null)
                                {
                                    ListItem li = new ListItem();
                                    li.Text = c.Club_Name;
                                    li.Value = c.Club_ID.ToString();
                                    ddlTagValue.Items.Insert(0, li);
                                    ddlTagValue.SelectedIndex = 0;

                                }
                                break;
                            }
                        case _tagTypeGame:
                            {
                                vwMatch match = db.vwMatches.SingleOrDefault(m => m.Match_ID == id);
                                if (match != null)
                                {
                                    ListItem li = new ListItem();
                                    li.Text = string.Format("{0} | {1} - {2}", match.Date.ToShortDateString(), match.HomeTeam, match.AwayTeam);
                                    li.Value = match.Match_ID.ToString();
                                    ddlTagValue.Items.Insert(0,li);
                                    ddlTagValue.SelectedIndex = 0;
                                    
                                }
                                break;
                            }
                        case _tagTypePlayer:
                            {
                                UaFDatabase.Player p = db.Players.SingleOrDefault(m => m.Player_Id == id);
                                if (p != null)
                                {
                                    ListItem li = new ListItem();
                                    li.Text = string.Format("{0} {1}", p.First_Name, p.Last_Name);
                                    li.Value = p.Player_Id.ToString();
                                    ddlTagValue.Items.Insert(0, li);
                                    ddlTagValue.SelectedIndex = 0;

                                }
                                break;
                            }
                    }
                }
            }
        }

        #endregion
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            MultimediaTagDTO newTagExt = new MultimediaTagDTO();
            newTagExt.Type = ddlTagType.SelectedValue;
            newTagExt.Description = ddlTagValue.SelectedItem.Text;
            bool isValid = false;
            switch (ddlTagType.SelectedValue)
            {
                case _tagTypeGame:
                    {
                        if (!TagsCache.Any(tc => tc.Type == _tagTypeGame))
                        {
                            newTagExt.Match_ID = int.Parse(ddlTagValue.SelectedValue);
                            isValid = true;
                        }
                        break;
                    }
                case _tagTypePlayer:
                    {
                        if (!TagsCache.Any(tc => tc.Player_ID.ToString() == ddlTagValue.SelectedItem.Value))
                        {
                            newTagExt.Player_ID = int.Parse(ddlTagValue.SelectedValue);
                            isValid = true;
                        }
                        break;
                    }
                case _tagTypeEvent:
                    {
                        if (!TagsCache.Any(tc => tc.MatchEvent_ID.ToString() == ddlTagValue.SelectedItem.Value))
                        {
                            newTagExt.MatchEvent_ID = int.Parse(ddlTagValue.SelectedValue);
                            isValid = true;
                        }
                        break;
                    }
                case _tagTypeNatTeam:
                    {
                        if (!TagsCache.Any(tc => tc.NationalTeam_ID.ToString() == ddlTagValue.SelectedItem.Value))
                        {
                            newTagExt.NationalTeam_ID = int.Parse(ddlTagValue.SelectedValue);
                            isValid = true;
                        }
                        break;
                    }
                case _tagTypeClub:
                    {
                        if (!TagsCache.Any(tc => tc.Club_ID.ToString() == ddlTagValue.SelectedItem.Value))
                        {
                            newTagExt.Club_ID = int.Parse(ddlTagValue.SelectedValue);
                            isValid = true;
                        }
                        break;
                    }
            }

            if (isValid)
            {
                if (TagsCache.Count == 0)
                {
                    newTagExt.tmpId = 0;
                }
                else
                {
                    newTagExt.tmpId = TagsCache.Max(tc => tc.tmpId) + 1;
                }
                TagsCache.Add(newTagExt);
                rptTags.DataSource = TagsCache;
                rptTags.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                bool isValid = ValidateTags();

                if (isValid)
                {
                    if (string.IsNullOrEmpty(Request["id"]))
                    {
                        //Get mmedia information from UI
                        UaFDatabase.Multimedia newMM = new UaFDatabase.Multimedia();
                        newMM.MultimediaType_CD = isImage(UploadedFileName) ? Constants.DB.MutlimediaTypes.Image : Constants.DB.MutlimediaTypes.Video;
                        newMM.MultimediaSubType_CD = ddlMultimediaSubType.SelectedValue;
                        newMM.MultimediaTags = new EntitySet<MultimediaTag>();
                        newMM.Author = tbAuthor.Text;
                        newMM.Source = tbSource.Text;
                        newMM.Description = tbDescription.Text;
                        newMM.Flags = 0;
                        foreach (ListItem li in cbl1.Items)
                        {
                            if (li.Selected)
                            {
                                newMM.Flags = newMM.Flags | long.Parse(li.Value);
                            }
                        }

                        foreach (MultimediaTagDTO mmTag in TagsCache)
                        {
                            MultimediaTag mt = new MultimediaTag
                            {
                                Club_ID = mmTag.Club_ID,
                                Match_ID = mmTag.Match_ID,
                                NationalTeam_ID = mmTag.NationalTeam_ID,
                                Player_ID = mmTag.Player_ID,
                                MatchEvent_ID = mmTag.MatchEvent_ID
                            };
                            newMM.MultimediaTags.Add(mt);
                        }

                        newMM.FilePath = PathHelper.GetMultimediaRelativePath(newMM);
                        newMM.FileName = UploadedFileName;
                        newMM.DateAdded = DateTime.Now;
                        newMM.DateUpdated = DateTime.Now;
                        newMM.DateTaken = getDateTaken();
                        
                        //1. Save file to permanent location

                        if (newMM.FilePath.Length > 0)
                        {
                            string destinationDirectoryPath = PathHelper.GetFileSystemPath(Constants.Paths.MultimediaStorageRoot, newMM.FilePath, "");
                            if (!Directory.Exists(destinationDirectoryPath)) Directory.CreateDirectory(destinationDirectoryPath);

                            string uploadedFilePath = PathHelper.GetFileSystemPath(Constants.Paths.MultimediaStorageRoot, _tmpUploadPath, UploadedFileName);
                            string destinationFilePath = PathHelper.GetFileSystemPath(Constants.Paths.MultimediaStorageRoot, newMM.FilePath, newMM.FileName);

                            if (!File.Exists(destinationFilePath))
                            {
                                File.Copy(uploadedFilePath, destinationFilePath);

                                //Create thumbnail for image
                                if (ddlMultimediaSubType.SelectedValue == Constants.DB.MutlimediaSubTypes.MatchPhoto ||
                                    ddlMultimediaSubType.SelectedValue == Constants.DB.MutlimediaSubTypes.NationalTeamLogo ||
                                    ddlMultimediaSubType.SelectedValue == Constants.DB.MutlimediaSubTypes.ClubLogo ||
                                    ddlMultimediaSubType.SelectedValue == Constants.DB.MutlimediaSubTypes.PlayerLogo)
                                {
                                    string thumbnailDirectoryPath = PathHelper.GetFileSystemPath(destinationDirectoryPath, "thumb", "");
                                    if (!Directory.Exists(thumbnailDirectoryPath)) Directory.CreateDirectory(thumbnailDirectoryPath);

                                    string destinationThumbFilePath = PathHelper.GetFileSystemPath(thumbnailDirectoryPath, "", UploadedFileName);

                                    int thumbnailMaxHeight = 150;
                                    switch (ddlMultimediaSubType.SelectedValue)
                                    {
                                        case Constants.DB.MutlimediaSubTypes.MatchPhoto:
                                        default:
                                            {
                                                thumbnailMaxHeight = 150; break;
                                            }
                                        case Constants.DB.MutlimediaSubTypes.NationalTeamLogo:
                                        case Constants.DB.MutlimediaSubTypes.ClubLogo:
                                            {
                                                thumbnailMaxHeight = 150; break;
                                            }
                                        case Constants.DB.MutlimediaSubTypes.PlayerLogo:
                                            {
                                                thumbnailMaxHeight = 300; break;
                                            }
                                    }
                                    if (ddlMultimediaSubType.SelectedValue == Constants.DB.MutlimediaSubTypes.MatchPhoto) thumbnailMaxHeight = 100;
                                    isValid = new BitmapHelper().ResizeImage(uploadedFilePath, destinationThumbFilePath, thumbnailMaxHeight, null);
                                }

                                if (isValid)
                                {
                                    lblError.Text = "File " + UploadedFileName + " successfully saved";
                                    tbDescription.Text = "";
                                    File.Delete(uploadedFilePath);
                                }
                                else
                                {
                                    isValid = false;
                                    lblError.Text = "Error creating thumbnail";
                                }
                            }
                            else
                            {
                                isValid = false;
                                lblError.Text = "File already exists";
                            }

                        }


                        //2. Save tags to DB
                        if (isValid)
                        {
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                db.Multimedias.InsertOnSubmit(newMM);
                                db.SubmitChanges();
                            }

                            TagsCache.RemoveAll(mt => mt.Type != _tagTypeGame);
                            rptTags.DataSource = TagsCache;
                            rptTags.DataBind();

                            foreach (ListItem li in cbl1.Items)
                            {
                                li.Selected = false;
                            }
                        }
                    }
                    else //update mm
                    {
                        int multimediaId = 0;

                        if (int.TryParse(Request["id"], out multimediaId))
                        {
                            using (UaFootball_DBDataContext db = DBManager.GetDB())
                            {
                                UaFDatabase.Multimedia mm = db.Multimedias.SingleOrDefault(m => m.Multimedia_ID == multimediaId);
                                if (mm!=null)
                                {
                                    mm.Author = tbAuthor.Text;
                                    mm.Description = tbDescription.Text;
                                    mm.Source = tbSource.Text;
                                    mm.MultimediaTags.Clear();

                                    db.MultimediaTags.DeleteAllOnSubmit(db.MultimediaTags.Where(m=>m.Multimedia_ID == multimediaId));
                                    foreach (MultimediaTagDTO mmTag in TagsCache)
                                    {
                                        MultimediaTag mt = new MultimediaTag
                                        {
                                            Club_ID = mmTag.Club_ID,
                                            Match_ID = mmTag.Match_ID,
                                            NationalTeam_ID = mmTag.NationalTeam_ID,
                                            Player_ID = mmTag.Player_ID,
                                            MatchEvent_ID = mmTag.MatchEvent_ID
                                        };
                                        mm.MultimediaTags.Add(mt);
                                    }

                                    mm.Flags = 0;
                                    foreach (ListItem li in cbl1.Items)
                                    {
                                        if (li.Selected)
                                        {
                                            mm.Flags = mm.Flags | long.Parse(li.Value);
                                        }
                                    }

                                    mm.DateUpdated = DateTime.Now;
                                    mm.DateTaken = getDateTaken();
                                    db.SubmitChanges();
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblError.Text = "Ошибка в тегах";
                }
            }
        }

        protected bool ValidateTags()
        {
            string fileName = UploadedFileName;
            bool isImg = isImage(fileName);
            bool isValid = true;
            switch (ddlMultimediaSubType.SelectedValue)
            {
                case Constants.DB.MutlimediaSubTypes.ClubLogo:
                    {
                        isValid = isImg && (TagsCache.Count == 1) && (TagsCache[0].Club_ID > 0);
                        break;
                    }
                case Constants.DB.MutlimediaSubTypes.MatchPhoto:
                    {
                        int matchTagCount = TagsCache.Count(tc => tc.Type == _tagTypeGame);
                        isValid = isImg && matchTagCount == 1;
                        break;
                    }
                case Constants.DB.MutlimediaSubTypes.MatchVideo:
                    {
                        int matchTagCount = TagsCache.Count(tc => tc.Type == _tagTypeGame);
                        isValid = !isImg && matchTagCount == 1;
                        break;
                    }
                case Constants.DB.MutlimediaSubTypes.NationalTeamLogo:
                    {
                        isValid = isImg && (TagsCache.Count == 1) && (TagsCache[0].NationalTeam_ID > 0);
                        break;
                    }
                case Constants.DB.MutlimediaSubTypes.PlayerLogo:
                    {

                        int playerTagsCount = TagsCache.Count(c => c.Player_ID.HasValue);
                        int clubTagsCount = TagsCache.Count(c => c.Club_ID.HasValue);
                        int nteamTagsCount = TagsCache.Count(c => c.NationalTeam_ID.HasValue);
                        isValid = isImg && (playerTagsCount == 1 && clubTagsCount + nteamTagsCount < 2);
                        break;
                    }
            }

            return isValid;
        }

        private bool isImage(string fileName)
        {
            fileName = fileName.ToUpper();
            return fileName.Contains(".JPG") || fileName.Contains(".GIF") || fileName.Contains(".PNG");
        }

        private DateTime? getDateTaken ()
        {
            if (tbPhotoDate.Text.Length > 0)
            {
                if (tbPhotoDate.Text.Length == 4)
                {
                    return new DateTime(int.Parse(tbPhotoDate.Text), 1, 1);
                }
                else
                {
                    return DateTime.Parse(tbPhotoDate.Text);
                }
            }
            else return null;
        }

        protected void rptTags_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            MultimediaTagDTO itemToDelete = TagsCache.Single(tc=>tc.tmpId.ToString() == e.CommandArgument.ToString());
            TagsCache.Remove(itemToDelete);
            rptTags.DataSource = TagsCache;
            rptTags.DataBind();
        }

        protected void afuUploader_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            AsyncFileUpload afuUploader = sender as AsyncFileUpload;
            string fileName = afuUploader.FileName;
            if (afuUploader != null)
            {
                string uploadedFilePath = PathHelper.GetFileSystemPath(Constants.Paths.MultimediaStorageRoot, _tmpUploadPath, fileName);
                afuUploader.SaveAs(uploadedFilePath);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int multimediaId = MMId;
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                if (multimediaId>0)
                {
                    UaFDatabase.Multimedia mm = db.Multimedias.SingleOrDefault(m => m.Multimedia_ID == multimediaId);
                    if (mm != null)
                    {
                        db.MultimediaTags.DeleteAllOnSubmit(db.MultimediaTags.Where(m => m.Multimedia_ID == multimediaId));
                        db.Multimedias.DeleteOnSubmit(mm);

                        string filePath = PathHelper.GetFileSystemPath(Constants.Paths.MultimediaStorageRoot, mm.FilePath, mm.FileName);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }

                        db.SubmitChanges();
                    }
                }
            }
        }

        protected void rptTags_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MultimediaTagDTO tag = e.Item.DataItem as MultimediaTagDTO;
                if (!tag.MatchEvent_ID.HasValue)
                {
                    Control btnEdit = e.Item.FindControl("btnEventEdit");
                    btnEdit.Visible = false;
                }
            }
        }
    }

    
}