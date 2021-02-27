using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFDatabase;

namespace UaFootball.AppCode
{
    /// <summary>
    /// Summary description for CityDTOHelper
    /// </summary>
    public class CityDTOHelper : IDTOHelper<CityDTO>
    {
        public CityDTO ConvertDBObjectToDTO(City dbObj)
        {
            CityDTO dtoObj = new CityDTO();
            dtoObj.City_ID = dbObj.City_ID;
            dtoObj.City_Name = dbObj.City_Name;
            dtoObj.Country_ID = dbObj.Country_ID;
            return dtoObj;
        }

        public void CopyDTOToDbObject(CityDTO dtoObj, City dbObj)
        {
            dbObj.City_ID = dtoObj.City_ID;
            dbObj.City_Name = dtoObj.City_Name;
            dbObj.Country_ID = dtoObj.Country_ID;
        }

        public CityDTO GetFromDB(int objectId)
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                City c = db.Cities.Single(cc => cc.City_ID == objectId);
                return ConvertDBObjectToDTO(c);
            }
        }

        public int SaveToDB(CityDTO dtoObj)
        {
            City dbObj = new City();

            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                if (dtoObj.City_ID > 0)
                {
                    dbObj = db.Cities.Single(cc => cc.City_ID == dtoObj.City_ID);
                }
                else
                {
                    db.Cities.InsertOnSubmit(dbObj);
                }

                CopyDTOToDbObject(dtoObj, dbObj);

                db.SubmitChanges();

                return dbObj.City_ID;
            }
        }

        public void DeleteFromDB(int objectId)
        {
            using (var db = DBManager.GetDB())
            {
                City c = db.Cities.Single(cc => cc.City_ID == objectId);
                db.Cities.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
        }

        public List<CityDTO> GetAllFromDB()
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                IEnumerable<CityDTO> cities = from city in db.Cities
                                              orderby city.Country.Country_Name, city.City_Name
                                              select new CityDTO
                                              {
                                                  City_ID = city.City_ID,
                                                  City_Name = city.City_Name,
                                                  Country_ID = city.Country_ID,
                                                  Country_Name = city.Country.Country_Name
                                              };
                return cities.ToList();
            }
        }

        public CityDTOHelper()
        {

        }
    } 
}