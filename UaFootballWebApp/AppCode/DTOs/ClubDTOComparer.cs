using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    public class ClubDTOComparer : IEqualityComparer<ClubDTO>
    {
        public bool Equals(ClubDTO x, ClubDTO y)
        {
            return x.Club_ID == y.Club_ID;
        }

        public int GetHashCode(ClubDTO obj)
        {
            return obj.Club_ID;
        }
    }
}