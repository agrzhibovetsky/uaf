using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UaFootball.DB;

namespace UaFootball.WebApplication.Temp
{
    

    public partial class NameSymbols : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string normalSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ' -ecaisocuocuenaraazoSlACsOyeoeinsuELDsOIaZsezttCCOd";
            string extraSymbols =  "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ' -éćáíšöčúóçüëñãřäăžôŠłÁČşÖýěøèïńßůÉŁĎśÓÍâŽșęźţțĆÇØđ";
            Dictionary<char, int> stats = new Dictionary<char, int>();

            using (UaFootball_DBDataContext db = new UaFootball_DBDataContext())
            {
                List<UaFootball.DB.Player> pls = db.Players.ToList();
                
                //for (int i = 0; i < pls.Count; i++)
                //{
                //    var p = pls[i];
                //    string fullName = (p.First_Name_Int.Trim() + ' ' + p.Last_Name_Int.Trim());

                //    for (int j = 0; j < fullName.Length; j++)
                //    {
                //        if (!extraSymbols.Contains(fullName[j]))
                //        {
                //            if (!stats.ContainsKey(fullName[j]))
                //            {
                //                stats.Add(fullName[j], 1);
                //            }
                //            else
                //            {
                //                stats[fullName[j]]++;
                //            }
                //        }
                //    }
                //}

                Dictionary<char, char> normalizationDictionary = new Dictionary<char, char>();
                for (int j = 0; j < normalSymbols.Length; j++)
                {
                    normalizationDictionary.Add(extraSymbols[j], normalSymbols[j]);
                }

                for (int i = 0; i < pls.Count; i++)
                {
                    var p = pls[i];
                    string fullName = (p.First_Name_Int.Trim() + ' ' + p.Last_Name_Int.Trim());
                    
                    string normalizedName = fullName;
                    for (int j = 0; j < fullName.Length; j++)
                    {
                        normalizedName = normalizedName.Replace(fullName[j], normalizationDictionary[fullName[j]]);
                    }

                    if (fullName.Length > 1)
                    {
                        //db.Players.Attach(p);
                        p.NameSearchString = normalizedName;
                    }
                }

                db.SubmitChanges();
            }

            lvSymb.DataSource = stats.OrderBy(kv => kv.Value);
            lvSymb.DataBind();

            lbl.Text = string.Join("",stats.OrderByDescending(kv => kv.Value).Select(s=>s.Key.ToString()).ToArray());
        }
    }
}