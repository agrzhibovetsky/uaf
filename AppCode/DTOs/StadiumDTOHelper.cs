using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFootball.DB;

namespace UaFootball.AppCode
{
    /// <summary>
    /// Summary description for StadiumDTOHelper
    /// </summary>
    public class StadiumDTOHelper : IDTOHelper<StadiumDTO>
    {

        public StadiumDTO ConvertDBObjectToDTO(Stadium dbObj)
        {
            StadiumDTO dtoObj = new StadiumDTO();
            dtoObj.Stadium_ID = dbObj.Stadium_Id;
            dtoObj.Stadium_Name = dbObj.Stadium_Name;
            dtoObj.City_ID = dbObj.City_Id;
            dtoObj.Capacity = dbObj.Capacity;
            dtoObj.YearBuilt = dbObj.Year_Built;
            dtoObj.Comments = dbObj.Comments;
            return dtoObj;
        }

        public void CopyDTOToDbObject(StadiumDTO dtoObj, Stadium dbObj)
        {
            dbObj.Stadium_Id = dtoObj.Stadium_ID;
            dbObj.Stadium_Name = dtoObj.Stadium_Name;
            dbObj.City_Id = dtoObj.City_ID;
            dbObj.Capacity = dtoObj.Capacity;
            dbObj.Year_Built = dtoObj.YearBuilt;
            dbObj.Comments = dtoObj.Comments;
        }

        public StadiumDTO GetFromDB(int objectId)
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                Stadium c = db.Stadiums.Single(cc => cc.Stadium_Id == objectId);
                return ConvertDBObjectToDTO(c);
            }
        }

        public int SaveToDB(StadiumDTO dtoObj)
        {
            Stadium dbObj = new Stadium();

            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                if (dtoObj.Stadium_ID > 0)
                {
                    dbObj = db.Stadiums.Single(cc => cc.Stadium_Id == dtoObj.Stadium_ID);
                }
                else
                {
                    dbObj.DateAdded = DateTime.Now;
                    db.Stadiums.InsertOnSubmit(dbObj);
                }

                CopyDTOToDbObject(dtoObj, dbObj);

                db.SubmitChanges();

                return dbObj.Stadium_Id;
            }
        }

        public void DeleteFromDB(int objectId)
        {
            using (var db = new UaFootball_DBDataContext())
            {
                Stadium c = db.Stadiums.Single(cc => cc.Stadium_Id == objectId);
                db.Stadiums.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
        }

        public List<StadiumDTO> GetAllFromDB()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                IEnumerable<StadiumDTO> objects = from s in db.Stadiums
                                                  
                                                  select new StadiumDTO
                                                  {
                                                      Stadium_ID = s.Stadium_Id,
                                                      Stadium_Name = s.Stadium_Name,
                                                      Capacity = s.Capacity,
                                                      YearBuilt = s.Year_Built,
                                                      City_ID = s.City_Id,
                                                      City_Name = s.City.City_Name,
                                                      Country_Name = s.City.Country.Country_Name
                                                  };
                return objects.OrderBy(o=>o.Country_Name).ThenBy(o=>o.City_Name).ToList();
            }
        }

        public StadiumDTOHelper()
        {

        }
    } 
}