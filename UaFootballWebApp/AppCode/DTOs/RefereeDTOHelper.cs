using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using UaFDatabase;

namespace UaFootball.AppCode
{

    /// <summary>
    /// Summary description for RefereeDTOHelper
    /// </summary>
    public class RefereeDTOHelper : IDTOHelper<RefereeDTO>
    {
        public RefereeDTO ConvertDBObjectToDTO(Referee dbObj)
        {
            RefereeDTO dtoObj = new RefereeDTO()
            {
                Country_Id = dbObj.Country_Id,
                DOB = dbObj.DOB,
                FirstName = dbObj.FirstName,
                LastName = dbObj.LastName,
                Referee_Id = dbObj.Referee_Id,
                LastName_EN = dbObj.LastName_EN,
                FirstName_EN = dbObj.FirstName_EN
            };

            return dtoObj;
        }

        public void CopyDTOToDbObject(RefereeDTO dtoObj, Referee dbObj)
        {
            dbObj.Referee_Id = dtoObj.Referee_Id;
            dbObj.FirstName = dtoObj.FirstName;
            dbObj.DOB = dtoObj.DOB;
            dbObj.LastName = dtoObj.LastName;
            dbObj.Country_Id = dtoObj.Country_Id;
            dbObj.FirstName_EN = dtoObj.FirstName_EN;
            dbObj.LastName_EN = dtoObj.LastName_EN;
        }

        public RefereeDTO GetFromDB(int objectId)
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                var dbData = (from Referee in db.Referees
                              where Referee.Referee_Id == objectId
                              select new { p = Referee, c = Referee.Country.Country_Name }).Single();
                RefereeDTO ret = ConvertDBObjectToDTO(dbData.p);
                ret.CountryName = dbData.c;
                return (ret);
            }
        }

        //public RefereeDTO GetFullFromDB(int objectId)
        //{
        //    using (UaFootball_DBDataContext db = DBManager.GetDB())
        //    {
        //        DataLoadOptions options = new DataLoadOptions();
        //        options.LoadWith<Match>(m => m.MatchEvents);
        //        db.LoadOptions = options;

        //        var dbData = (from Referee in db.Referees
        //                      where Referee.Referee_Id == objectId
        //                      select new { p = Referee, c = Referee.Country.Country_Name }).Single();
        //        RefereeDTO ret = ConvertDBObjectToDTO(dbData.p);
        //        ret.CountryName = dbData.c;

        //        //var matchData = 
                

        //        var matchesData = from m in db.Matches where m.Referee_Id == objectId select m;
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

        public int SaveToDB(RefereeDTO dtoObj)
        {
            Referee dbObj = new Referee();

            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                if (dtoObj.Referee_Id > 0)
                {
                    dbObj = db.Referees.Single(cc => cc.Referee_Id == dtoObj.Referee_Id);
                }
                else
                {
                    db.Referees.InsertOnSubmit(dbObj);
                }

                CopyDTOToDbObject(dtoObj, dbObj);

                db.SubmitChanges();

                return dbObj.Referee_Id;
            }
        }

        public void DeleteFromDB(int objectId)
        {
            using (var db = DBManager.GetDB())
            {
                Referee c = db.Referees.Single(cc => cc.Referee_Id == objectId);
                db.Referees.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
        }

        public List<RefereeDTO> GetAllFromDB()
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {

                List<RefereeDTO> Referees = new List<RefereeDTO>();
                var dbObjects = from dbObj in db.Referees
                                orderby dbObj.Country.Country_Name, dbObj.LastName
                                select new { r = dbObj, c = dbObj.Country.Country_Name };
                foreach (var dbObj in dbObjects)
                {
                    RefereeDTO newReferee = ConvertDBObjectToDTO(dbObj.r);
                    newReferee.CountryName = dbObj.c;
                    Referees.Add(newReferee);
                }

                return Referees.ToList();
            }
        }

        public RefereeDTOHelper()
        {

        }
    } 
}