using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFootball.DB;

namespace UaFootball.AppCode
{
    public class CoachDTOHelper : IDTOHelper<CoachDTO>
    {
        public CoachDTO ConvertDBObjectToDTO(Coach dbObj)
        {
            CoachDTO dtoObj = new CoachDTO()
            {
                Country_Id = dbObj.CountryId,
                DOB = dbObj.DOB,
                FirstName = dbObj.FirstName,
                LastName = dbObj.LastName,
                CoachId = dbObj.CoachId,
                LastName_EN = dbObj.LastNameInt,
                FirstName_EN = dbObj.FirstNameInt,
                ModifiedDate = dbObj.ModifiedDate
            };

            return dtoObj;
        }

        public void CopyDTOToDbObject(CoachDTO dtoObj, Coach dbObj)
        {
            dbObj.CoachId = dtoObj.CoachId;
            dbObj.FirstName = dtoObj.FirstName;
            dbObj.DOB = dtoObj.DOB;
            dbObj.LastName = dtoObj.LastName;
            dbObj.CountryId = dtoObj.Country_Id;
            dbObj.FirstNameInt = dtoObj.FirstName_EN;
            dbObj.LastNameInt = dtoObj.LastName_EN;
            dbObj.ModifiedDate = dtoObj.ModifiedDate;
        }

        public CoachDTO GetFromDB(int objectId)
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                var dbData = (from Coach in db.Coaches
                              where Coach.CoachId == objectId
                              select new { p = Coach, c = Coach.Country.Country_Name }).Single();
                CoachDTO ret = ConvertDBObjectToDTO(dbData.p);
                ret.CountryName = dbData.c;
                return (ret);
            }
        }

        //public CoachDTO GetFullFromDB(int objectId)
        //{
        //    using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
        //    {
        //        DataLoadOptions options = new DataLoadOptions();
        //        options.LoadWith<Match>(m => m.MatchEvents);
        //        db.LoadOptions = options;

        //        var dbData = (from Coach in db.Coachs
        //                      where Coach.Coach_Id == objectId
        //                      select new { p = Coach, c = Coach.Country.Country_Name }).Single();
        //        CoachDTO ret = ConvertDBObjectToDTO(dbData.p);
        //        ret.CountryName = dbData.c;

        //        //var matchData = 
                

        //        var matchesData = from m in db.Matches where m.Coach_Id == objectId select m;
        //        MatchDTOHelper mDTOHelper = new MatchDTOHelper();
        //        MatchEventDTO meDTO = new MatchEventDTO();
        //        foreach (Match dbMatch in matchesData)
        //        {
        //            MatchDTO mDTO = mDTOHelper.ConvertDBObjectToDTO(dbMatch);
        //            foreach (MatchEvent me in dbMatch.MatchEvents)
        //            {
        //                mDTO.Events.Add(meDTO.ConvertDBObjectToDTO(me));
        //            }
        //            ret.Matches.Add(mDTO);
        //        }
        //        return (ret);
        //    }
        //}

        public int SaveToDB(CoachDTO dtoObj)
        {
            Coach dbObj = new Coach();
            dtoObj.ModifiedDate = DateTime.Now;
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                if (dtoObj.CoachId > 0)
                {
                    dbObj = db.Coaches.Single(cc => cc.CoachId == dtoObj.CoachId);
                }
                else
                {
                    db.Coaches.InsertOnSubmit(dbObj);
                }

                CopyDTOToDbObject(dtoObj, dbObj);

                db.SubmitChanges();

                return dbObj.CoachId;
            }
        }

        public void DeleteFromDB(int objectId)
        {
            using (var db = new UaFootball_DBDataContext())
            {
                Coach c = db.Coaches.Single(cc => cc.CoachId == objectId);
                db.Coaches.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
        }

        public List<CoachDTO> GetAllFromDB()
        {
            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {

                List<CoachDTO> Coachs = new List<CoachDTO>();
                var dbObjects = from dbObj in db.Coaches
                                orderby dbObj.Country.Country_Name, dbObj.LastName
                                select new { r = dbObj, c = dbObj.Country.Country_Name };
                foreach (var dbObj in dbObjects)
                {
                    CoachDTO newCoach = ConvertDBObjectToDTO(dbObj.r);
                    newCoach.CountryName = dbObj.c;
                    Coachs.Add(newCoach);
                }

                return Coachs.ToList();
            }
        }

        public CoachDTOHelper()
        {

        }
    }
}