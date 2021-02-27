using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFDatabase;

namespace UaFootball.AppCode
{
    /// <summary>
    /// Summary description for NationalTeamHelper
    /// </summary>
    public class NationalTeamDTOHelper : IDTOHelper<NationalTeamDTO>
    {
        public NationalTeamDTO ConvertDBObjectToDTO(NationalTeam dbObj)
        {
            NationalTeamDTO dtoObj = new NationalTeamDTO()
            {
                County_ID = dbObj.Country_Id,
                ID = dbObj.NationalTeam_Id,
                Kind = dbObj.NationalTeamType_Cd
            };


            return dtoObj;
        }

        public void CopyDTOToDbObject(NationalTeamDTO dtoObj, NationalTeam dbObj)
        {
            dbObj.Country_Id = dtoObj.County_ID;
            dbObj.NationalTeam_Id = dtoObj.ID;
            dbObj.NationalTeamType_Cd = dtoObj.Kind;
        }

        public NationalTeamDTO GetFromDB(int objectId)
        {
            using (UaFootball_DBDataContext db = DBManager.GetDB())
            {
                var dbData = (from nationalTeam in db.NationalTeams
                             join country in db.Countries on nationalTeam.Country_Id equals country.Country_ID
                             where nationalTeam.NationalTeam_Id == objectId
                             select new { Team = nationalTeam, CountryName = country.Country_Name }).Single();

                NationalTeamDTO dtoObj = ConvertDBObjectToDTO(dbData.Team);
                dtoObj.Country_Name = dbData.CountryName;
                
                Multimedia mLogo = (from tag in db.MultimediaTags
                                    where tag.NationalTeam_ID == objectId && tag.Multimedia.MultimediaSubType_CD == Constants.DB.MutlimediaSubTypes.NationalTeamLogo
                                    select tag.Multimedia).FirstOrDefault();

                if (mLogo != null)
                {
                    dtoObj.Logo = MultimediaDTO.FromDBObject(mLogo);
                    dtoObj.Logo.IsUploaded = true;
                }

                return dtoObj;
            }
        }

        public int SaveToDB(NationalTeamDTO dtoObj)
        {
            return 0;
            
        }

        public void DeleteFromDB(int objectId)
        {
            return;
        }

        public List<NationalTeamDTO> GetAllFromDB()
        {
            return new List<NationalTeamDTO>();
        }

        public NationalTeamDTOHelper()
        {

        }
    } 
}