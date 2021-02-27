using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFootball.AppCode;

/// <summary>
/// Summary description for ObjectEditPageBase
/// </summary>
namespace UaFootball.WebApplication
{
    public abstract class ObjectEditPageBase<T> : UaFootballPageBase where T : class, new()
    {
        protected T DataItem
        {
            get
            {
                return ViewState["DataItem"] as T;
            }
            set
            {
                ViewState["DataItem"] = value;
            }
        }

        protected bool isNewObject;

        protected abstract string ObjectListPage { get; }

        protected IDTOHelper<T> DTOHelper { get; set; }

        protected abstract void DTOToUI(T dtoObj);

        protected abstract T UIToDTO();

        protected abstract void PrepareUI();

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                T dtoObj = new T();

                isNewObject = (Request[Constants.QueryParam.ObjectId] == null);

                if (!isNewObject)
                {
                    int objectId = int.Parse(Request[Constants.QueryParam.ObjectId]);
                    dtoObj = DTOHelper.GetFromDB(objectId);
                }

                DataItem = dtoObj;

                PrepareUI();

                if (!isNewObject)
                {
                    DTOToUI(dtoObj);
                }
            }

            base.OnLoad(e);
        }

        protected void SaveObject(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                T dtoObj = UIToDTO();
                DTOHelper.SaveToDB(dtoObj);
                Response.Redirect(ObjectListPage);
            }
        }

        protected void ReturnToObjectList(object sender, EventArgs e)
        {
            Response.Redirect(ObjectListPage);
        }

        public ObjectEditPageBase()
        {
            DTOHelper = CreateDTOHelper<T>();
        }
    } 
}