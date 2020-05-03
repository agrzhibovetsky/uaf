using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFootball.DB;

namespace UaFootball.AppCode
{
    /// <summary>
    /// Summary description for ClubDTOHelper
    /// </summary>
    public class ClubDTOHelper : IDTOHelper<ClubDTO>
    {
        public ClubDTO ConvertDBObjectToDTO(vwClub dbObj)
        {
            ClubDTO dtoObj = new ClubDTO()
            {
                Club_ID = dbObj.Club_ID,
                Club_Name = dbObj.Club_Name,
                City_ID = dbObj.City_ID,
                Year_Found = dbObj.Year_Found,
                City_Name = dbObj.City_Name
            };

            return dtoObj;
        }

        public void CopyDTOToDbObject(ClubDTO dtoObj, Club dbObj)
        {
            dbObj.Club_ID = dtoObj.Club_ID;
            dbObj.Club_Name = dtoObj.Club_Name;
            dbObj.City_ID = dtoObj.City_ID;
            dbObj.Display_Name = dtoObj.Display_Name;
            //dbObj.Logo = dtoObj.Logo;
            dbObj.Year_Found = dtoObj.Year_Found;
        }

        public ClubDTO GetFromDB(int objectId)
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                vwClub c = db.vwClubs.Single(cc => cc.Club_ID == objectId);
                ClubDTO dtoObj = ConvertDBObjectToDTO(c);
                
                Multimedia mLogo = (from tag in db.MultimediaTags
                                    where tag.Club_ID == objectId && tag.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.ClubLogo
                                    select tag.Multimedia).FirstOrDefault();

                if (mLogo != null)
                {
                    dtoObj.Logo = mLogo.ToDTO();
                    dtoObj.Logo.IsUploaded = true;
                }

                return dtoObj;
            }
        }

        public int SaveToDB(ClubDTO dtoObj)
        {
            Club dbObj = new Club();

            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                if (dtoObj.Club_ID > 0)
                {
                    dbObj = db.Clubs.Single(cc => cc.Club_ID == dtoObj.Club_ID);
                }
                else
                {
                    db.Clubs.InsertOnSubmit(dbObj);
                }

                CopyDTOToDbObject(dtoObj, dbObj);


                IEnumerable<MultimediaTag> mTagsToDel = db.MultimediaTags.Where(t => t.Club_ID == dtoObj.Club_ID && t.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.ClubLogo);
                db.MultimediaTags.DeleteAllOnSubmit(mTagsToDel);


                db.SubmitChanges();

                return dbObj.Club_ID;
            }
        }

        public void DeleteFromDB(int objectId)
        {
            using (var db = new UaFootball_DBDataContext())
            {
                Club c = db.Clubs.Single(cc => cc.Club_ID == objectId);
                db.Clubs.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
        }

        public List<ClubDTO> GetAllFromDB()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                IEnumerable<ClubDTO> objects = from s in db.Clubs
                                               orderby s.Club_Name
                                               select new ClubDTO
                                               {
                                                   Club_ID = s.Club_ID,
                                                   Club_Name = s.Club_Name,
                                                   City_ID = s.City_ID,
                                                   City_Name = s.City.City_Name,
                                                   //Logo = s.Logo,
                                                   Display_Name = s.Display_Name,
                                                   Year_Found = s.Year_Found
                                               };
                return objects.ToList();
            }
        }

        public ClubDTOHelper()
        {

        }
    } 
}