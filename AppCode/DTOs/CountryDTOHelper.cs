using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFootball.DB;

namespace UaFootball.AppCode
{

    /// <summary>
    /// Summary description for CountryDTOHelper
    /// </summary>
    public class CountryDTOHelper : IDTOHelper<CountryDTO>
    {
        public CountryDTO ConvertDBObjectToDTO(Country dbObj)
        {
            CountryDTO dtoObj = new CountryDTO();
            dtoObj.Country_ID = dbObj.Country_ID;
            dtoObj.Country_Code = dbObj.Country_Code;
            dtoObj.Country_Name = dbObj.Country_Name;
            dtoObj.FIFAAssociation_ID = dbObj.FIFAAssociation_ID;

            return dtoObj;
        }

        public void CopyDTOToDbObject(CountryDTO dtoObj, Country dbObj)
        {
            dbObj.Country_ID = dtoObj.Country_ID;
            dbObj.Country_Name = dtoObj.Country_Name;
            dbObj.Country_Code = dtoObj.Country_Code;
            dbObj.FIFAAssociation_ID = dtoObj.FIFAAssociation_ID;
        }

        public CountryDTO GetFromDB(int objectId)
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                Country c = db.Countries.Single(cc => cc.Country_ID == objectId);
                return ConvertDBObjectToDTO(c);
            }
        }

        public int SaveToDB(CountryDTO dtoObj)
        {
            Country dbObj = new Country();

            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                if (dtoObj.Country_ID > 0)
                {
                    dbObj = db.Countries.Single(cc => cc.Country_ID == dtoObj.Country_ID);
                }
                else
                {
                    db.Countries.InsertOnSubmit(dbObj);
                    NationalTeam newNationalTeam = new NationalTeam { Country = dbObj, NationalTeamType_Cd = Constants.DB.NationalTeamTypeCd_National };
                    NationalTeam newNationalU21Team = new NationalTeam { Country = dbObj, NationalTeamType_Cd = Constants.DB.NationalTeamTypeCd_U21 };
                    db.NationalTeams.InsertOnSubmit(newNationalTeam);
                    db.NationalTeams.InsertOnSubmit(newNationalU21Team);
                }

                CopyDTOToDbObject(dtoObj, dbObj);

                db.SubmitChanges();

                return dbObj.Country_ID;
            }
        }

        public void DeleteFromDB(int objectId)
        {
            using (var db = new UaFootball_DBDataContext())
            {
                Country c = db.Countries.Single(cc => cc.Country_ID == objectId);
                db.Countries.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
        }

        public List<CountryDTO> GetAllFromDB()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                IEnumerable<CountryDTO> countries = from country in db.Countries
                                                    orderby country.FIFAAssociation_ID, country.Country_Name
                                                    select new CountryDTO
                                                    {
                                                        Country_ID = country.Country_ID,
                                                        Country_Name = country.Country_Name,
                                                        Country_Code = country.Country_Code,
                                                        FIFAAssociation_ID = country.FIFAAssociation_ID,
                                                        FIFAAssociation_Name = country.FIFAAssociation.FIFAAssociation_Name
                                                    };
                return countries.ToList();
            }
        }

        public CountryDTOHelper()
        {

        }
    } 
}