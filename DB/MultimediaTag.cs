using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UaFootball.AppCode;

namespace UaFootball.DB
{
    public partial class MultimediaTag
    {
        public MultimediaTagDTO ToDTO()
        {
            return new MultimediaTagDTO
            {
                Club_ID = this.Club_ID,
                Match_ID = this.Match_ID,
                MatchEvent_ID = this.MatchEvent_ID,
                NationalTeam_ID = this.NationalTeam_ID,
                Player_ID = this.Player_ID
            };
        }
    }
}