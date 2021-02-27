using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    /// <summary>
    /// Summary description for Extenders
    /// </summary>
    public static class Extenders
    {

        public static bool IsEmpty(this string s)
        {
            if (s == null) return true;
            return s.Length == 0;
        }

        public static int? ParseInt(this string s, bool treatZeroAsNull)
        {
            if (s.IsEmpty()) return null;
            else
            {
                int i = int.Parse(s);
                if (treatZeroAsNull && i == 0) return null;
                else return i;
            }
        }

        public static string ToNormalizedASCIIString(this string s)
        {
            Dictionary<char, char> normalizationDictionary = new Dictionary<char, char>();
            for (int j = 0; j < Constants.normalSymbols.Length; j++)
            {
                normalizationDictionary.Add(Constants.extraSymbols[j], Constants.normalSymbols[j]);
            }

            string normalizedName = s;
            for (int j = 0; j < s.Length; j++)
            {
                normalizedName = normalizedName.Replace(s[j], normalizationDictionary[s[j]]);
            }

            return normalizedName;
        }
    } 
}