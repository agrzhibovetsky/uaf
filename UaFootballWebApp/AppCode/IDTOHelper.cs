using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace UaFootball.AppCode
{
    public interface IDTOHelper<T> where T : class, new()
    {
        T GetFromDB(int objectId);

        int SaveToDB(T dtoObj);

        void DeleteFromDB(int objectId);

        List<T> GetAllFromDB();
    } 
}