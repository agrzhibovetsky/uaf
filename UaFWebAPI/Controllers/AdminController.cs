using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UaFDatabaseEF.DTO;
using UaFDatabaseEF.Models;


namespace UaFWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("GetMatchEvent")]
        public ActionResult<MatchEventDTO> GetMatchEvent(int matchEventId)
        {
            MatchEventDTO result = new MatchEventDTO();

            using (UaFootballContext db = new UaFootballContext())
            {
                MatchEvents me = new MatchEvents();

                var o = (from matchEvent in db.MatchEvents
                        where matchEvent.MatchEventId == matchEventId
                        select new { A = matchEvent, B = matchEvent.Player1, C = matchEvent.Player2 }).Single();
                me = o.A;
                me.Player1 = o.B;
                me.Player2 = o.C;

                if (me != null)
                {
                    result = new MatchEventDTO
                    {
                        EventFlags = me.EventFlags,
                        Event_Cd = me.EventCd,
                        MatchEvent_Id = me.MatchEventId,
                        Match_Id = me.MatchId,
                        Minute = me.Minute,
                        Player1_Id = me.Player1Id,
                        Player2_Id = me.Player2Id,
                        Player1 = new PlayerDTO
                        {
                            First_Name = me.Player1.FirstName,
                            Last_Name = me.Player1.LastName,
                            Display_Name = me.Player1.DisplayName,
                            Country_Id = me.Player1.CountryId
                        }
                    };
                }
                if (me.Player2Id.HasValue)
                {
                    result.Player2 = new PlayerDTO
                    {
                        First_Name = me.Player2.FirstName,
                        Last_Name = me.Player2.LastName,
                        Display_Name = me.Player2.DisplayName,
                        Country_Id = me.Player2.CountryId
                    };
                }
            }

            return result;
        }

        [HttpGet("UpdateMatchEvent")]
        public ActionResult<string> UpdateMatchEvent(int matchEventId, int player2Id, long flags)
        {
            string response = "success";
            using (UaFootballContext db = new UaFootballContext())
            {
                try
                {
                    MatchEvents me = db.MatchEvents.Find(matchEventId);
                    me.Player2Id = player2Id;
                    me.EventFlags = flags;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    response = "Error: " + ex.Message;
                }
            }
            
            return string.Format("{{\"result\":\"{0}\"}}", response);
        }
    }
}