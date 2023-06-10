using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using UaFootball.AppCode;
using UaFDatabase;

namespace UaFootball.WebApplication
{
    public static class UIHelper
    {
        private static Dictionary<string, string> _nationalTeamTypeMap;
        private static Dictionary<string, string> _eventCodeMap;
        private static Dictionary<int, string> _goalEventFlagsMap;
        private static Dictionary<int, string> _cardEventFlagsMap;
        private static Dictionary<int, string> _substitutionEventFlagsMap;
        private static Dictionary<int, string> _missedPenaltyEventFlagsMap;
        private static Dictionary<int, string> _penaltyEventFlagsMap;
        private static Dictionary<string, Dictionary<int, string>> _eventCodeEventFlagsMap;
        private static Dictionary<int, string> _matchFlagsMap;

        public static Dictionary<int, string> PenaltyEventFlagsMap
        {
            get { return UIHelper._penaltyEventFlagsMap; }
            set { UIHelper._penaltyEventFlagsMap = value; }
        }

        public static Dictionary<int, string> MatchFlagsMap
        {
            get { return UIHelper._matchFlagsMap; }
        }

        public static Dictionary<string, Dictionary<int, string>> EventCodeEventFlagsMap
        {
            get { return UIHelper._eventCodeEventFlagsMap; }
        }

        public static Dictionary<string, string> EventCodeMap
        {
            get { return UIHelper._eventCodeMap; }
        }

        public static string FormatNationalTeamType(string code)
        {
            if (_nationalTeamTypeMap.ContainsKey(code)) return _nationalTeamTypeMap[code]; else return string.Empty;
        }

        public static string FormatDate(object oDate)
        {
            if (oDate is DateTime?)
                return FormatDate((DateTime?)oDate);
            else return string.Empty;
        }

        public static string FormatDate(DateTime? date)
        {
            if (date.HasValue) return date.Value.ToString("dd.MM.yyyy"); else return string.Empty;
        }

        public static string FormatName(PlayerDTO player)
        {
            if (player != null)
            {
                return FormatName(player.First_Name, player.Last_Name, player.Display_Name, player.Country_Id);
            }
            else return string.Empty;
        }

        public static string FormatName(object FirstName, object LastName, object DisplayName, int? countryId)
        {
            string fNameStg = FirstName as string;
            string sNameStg = LastName as string;
            string dNameStg = DisplayName as string;
            if (dNameStg != null && dNameStg.Length > 0)
            {
                return dNameStg.ToUpper();
            }
            else
            {
                if (sNameStg != null)
                {
                    if (fNameStg != null && fNameStg.Length > 0 && sNameStg.Length > 0)
                    {
                        if (countryId == 50)
                        {
                            return string.Concat(sNameStg.ToUpper(), " ", fNameStg);
                        }
                        return string.Concat(fNameStg, " ", sNameStg.ToUpper());
                    }
                    else
                        if (sNameStg.Length > 0) return sNameStg.ToUpper();
                        else return string.Empty;
                }
                else return string.Empty;
            }
        }


        public static string FormatScore(object oHomeScore, object oAwayScore, object oHomePenScore, object oAwayPenScore)
        {
            short homeScore = (short)oHomeScore;
            short awayScore = (short)oAwayScore;
            short? homePenScore = (short?)oHomePenScore;
            short? awayPenScore = (short?)oAwayPenScore;
            if (homePenScore.HasValue && awayPenScore.HasValue)
                return string.Format("{0}:{1} ({2}:{3})", homeScore, awayScore, homePenScore, awayPenScore);
            else return string.Format("{0}:{1}", homeScore, awayScore);
        }

        public static string FormatMatch(vwMatch match)
        {
            return string.Format("{0} - {1} ({2}:{3})", match.HomeTeam, match.AwayTeam, match.HomeScore, match.AwayScore);
        }

        public static string GetTeamSpanClass(object countryCode)
        {
            string countryCodeStg = countryCode as string;

            if (countryCodeStg.Equals(Constants.DB.UACountryCode))
            {
                return "uaTeam";
            }
            else return string.Empty;
        }

        public static string FormatLineupFlags(int flags)
        {
            string val = string.Empty;
            if ((flags & Constants.DB.LineupFlags.Captain) != 0)
            {
                val = "(К)";
            }

            return val;
        }


        static UIHelper()
        {
            _nationalTeamTypeMap = new Dictionary<string, string>();
            _nationalTeamTypeMap.Add(Constants.DB.NationalTeamTypeCd_National, Constants.UI.NationalTeamType_National);
            _nationalTeamTypeMap.Add(Constants.DB.NationalTeamTypeCd_U21, Constants.UI.NationalTeamType_U21);

            _eventCodeMap = new Dictionary<string, string>();
            _eventCodeMap.Add(Constants.DB.EventTypeCodes.Goal, Constants.UI.EventTypeCodes.Goal);
            _eventCodeMap.Add(Constants.DB.EventTypeCodes.Penalty, Constants.UI.EventTypeCodes.Penalty);
            _eventCodeMap.Add(Constants.DB.EventTypeCodes.Substitution, Constants.UI.EventTypeCodes.Substitution);
            _eventCodeMap.Add(Constants.DB.EventTypeCodes.YellowCard, Constants.UI.EventTypeCodes.YellowCard);
            _eventCodeMap.Add(Constants.DB.EventTypeCodes.SecondYellowCard, Constants.UI.EventTypeCodes.SecondYellowCard);
            _eventCodeMap.Add(Constants.DB.EventTypeCodes.RedCard, Constants.UI.EventTypeCodes.RedCard);
            _eventCodeMap.Add(Constants.DB.EventTypeCodes.MissedPenalty, Constants.UI.EventTypeCodes.MissedPenalty);
            _eventCodeMap.Add(Constants.DB.EventTypeCodes.CoachYellowCard, Constants.UI.EventTypeCodes.CoachYellowCard);
            //_eventCodeMap.Add(Constants.DB.EventTypeCodes.PenaltyShootoutScored, Constants.UI.EventTypeCodes.PenaltyShootoutScored);
            //_eventCodeMap.Add(Constants.DB.EventTypeCodes.PenaltyShootoutMissed, Constants.UI.EventTypeCodes.PenaltyShootoutMissed);

            _goalEventFlagsMap = new Dictionary<int, string>();
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.LeftLeg, Constants.UI.EventFlags.LeftLeg);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.RightLeg, Constants.UI.EventFlags.RightLeg);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.Head, Constants.UI.EventFlags.Head);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.OtherBodyPart, Constants.UI.EventFlags.OtherBodyPart);
            //_goalEventFlagsMap.Add(Constants.DB.EventFlags.Penalty, Constants.UI.EventFlags.Penalty);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.Corner, Constants.UI.EventFlags.Corner);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.FreeKick, Constants.UI.EventFlags.FreeKick);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.GoalClass1, Constants.UI.EventFlags.GoalClass1);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.GoalClass4, Constants.UI.EventFlags.GoalClass4);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.GoalClass2, Constants.UI.EventFlags.GoalClass2);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.GoalClass3, Constants.UI.EventFlags.GoalClass3);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.OwnGoal, Constants.UI.EventFlags.OwnGoal);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.LongDistance, Constants.UI.EventFlags.LongDistance);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.MiddleDistance, Constants.UI.EventFlags.MiddleDistance);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.ShortDistance, Constants.UI.EventFlags.ShortDistance);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.OneToOne, Constants.UI.EventFlags.OneToOne);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.Rikoshet, Constants.UI.EventFlags.Rikoshet);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.CounterAttack, Constants.UI.EventFlags.CounterAttack);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.Solo, Constants.UI.EventFlags.Solo);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.Dobivanie, Constants.UI.EventFlags.Dobivanie);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.Combinacia, Constants.UI.EventFlags.Combinacia);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.GoalPass, Constants.UI.EventFlags.GoalPass);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.Pyatka, Constants.UI.EventFlags.Pyatka);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.Razvorot, Constants.UI.EventFlags.Razvorot);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.GKFault, Constants.UI.EventFlags.GKFault);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.Dribble, Constants.UI.EventFlags.Dribble);
            _goalEventFlagsMap.Add(Constants.DB.EventFlags.BetweenLegs, Constants.UI.EventFlags.BetweenLegs);


            _cardEventFlagsMap = new Dictionary<int, string>();
            _cardEventFlagsMap.Add(Constants.DB.EventFlags.Arm, Constants.UI.EventFlags.Arm);
            _cardEventFlagsMap.Add(Constants.DB.EventFlags.RoughPlay, Constants.UI.EventFlags.RoughPlay);
            _cardEventFlagsMap.Add(Constants.DB.EventFlags.Simulation, Constants.UI.EventFlags.Simulation);
            _cardEventFlagsMap.Add(Constants.DB.EventFlags.GameDelay, Constants.UI.EventFlags.GameDelay);
            _cardEventFlagsMap.Add(Constants.DB.EventFlags.UnsportingBehavior, Constants.UI.EventFlags.UnsportingBehavior);
            _cardEventFlagsMap.Add(Constants.DB.EventFlags.Talks, Constants.UI.EventFlags.Talks);
            _cardEventFlagsMap.Add(Constants.DB.EventFlags.FoulOfLastHope, Constants.UI.EventFlags.FoulOfLastHope);
            _cardEventFlagsMap.Add(Constants.DB.EventFlags.AfterFinalWhistle, Constants.UI.EventFlags.AfterFinalWhistle);
            _cardEventFlagsMap.Add(Constants.DB.EventFlags.PlayerOnBench, Constants.UI.EventFlags.PlayerOnBench);

            _substitutionEventFlagsMap = new Dictionary<int, string>();
            _substitutionEventFlagsMap.Add(Constants.DB.EventFlags.Injury, Constants.UI.EventFlags.Injury);

            _penaltyEventFlagsMap = new Dictionary<int, string>();
            _penaltyEventFlagsMap.Add(Constants.DB.EventFlags.LeftLeg, Constants.UI.EventFlags.LeftLeg);
            _penaltyEventFlagsMap.Add(Constants.DB.EventFlags.RightLeg, Constants.UI.EventFlags.RightLeg);
            _penaltyEventFlagsMap.Add(Constants.DB.EventFlags.LeftBottom, Constants.UI.EventFlags.LeftBottom);
            _penaltyEventFlagsMap.Add(Constants.DB.EventFlags.LeftTop, Constants.UI.EventFlags.LeftTop);
            _penaltyEventFlagsMap.Add(Constants.DB.EventFlags.RightBottom, Constants.UI.EventFlags.RightBottom);
            _penaltyEventFlagsMap.Add(Constants.DB.EventFlags.RightTop, Constants.UI.EventFlags.RightTop);
            _penaltyEventFlagsMap.Add(Constants.DB.EventFlags.Center, Constants.UI.EventFlags.Center);
            _penaltyEventFlagsMap.Add(Constants.DB.EventFlags.PanenkaStle, Constants.UI.EventFlags.PanenkaStle);
            _penaltyEventFlagsMap.Add(Constants.DB.EventFlags.PostMatchPenalty, Constants.UI.EventFlags.PostMatchPenalty);

            _missedPenaltyEventFlagsMap = new Dictionary<int, string>();
            _missedPenaltyEventFlagsMap.Add(Constants.DB.EventFlags.Post, Constants.UI.EventFlags.Post);
            _missedPenaltyEventFlagsMap.Add(Constants.DB.EventFlags.Crossbar, Constants.UI.EventFlags.Crossbar);
            _missedPenaltyEventFlagsMap.Add(Constants.DB.EventFlags.SavedbyKeeper, Constants.UI.EventFlags.SavedbyKeeper);
            _missedPenaltyEventFlagsMap.Add(Constants.DB.EventFlags.Miss, Constants.UI.EventFlags.Miss);
            _missedPenaltyEventFlagsMap.Add(Constants.DB.EventFlags.PostMatchPenalty, Constants.UI.EventFlags.PostMatchPenalty);

            _eventCodeEventFlagsMap = new Dictionary<string, Dictionary<int, string>>();
            _eventCodeEventFlagsMap.Add(Constants.DB.EventTypeCodes.Goal, _goalEventFlagsMap);
            _eventCodeEventFlagsMap.Add(Constants.DB.EventTypeCodes.MissedPenalty, _missedPenaltyEventFlagsMap);
            _eventCodeEventFlagsMap.Add(Constants.DB.EventTypeCodes.Penalty, _penaltyEventFlagsMap);
            _eventCodeEventFlagsMap.Add(Constants.DB.EventTypeCodes.RedCard, _cardEventFlagsMap);
            _eventCodeEventFlagsMap.Add(Constants.DB.EventTypeCodes.SecondYellowCard, _cardEventFlagsMap);
            _eventCodeEventFlagsMap.Add(Constants.DB.EventTypeCodes.Substitution, _substitutionEventFlagsMap);
            _eventCodeEventFlagsMap.Add(Constants.DB.EventTypeCodes.YellowCard, _cardEventFlagsMap);
            

            _matchFlagsMap = new Dictionary<int, string>();
            _matchFlagsMap.Add(Constants.DB.MatchFlags.HomeTeamTechnicalDefeat, Constants.UI.MatchFlags.HomeTeamTechnicalDefeat);
            _matchFlagsMap.Add(Constants.DB.MatchFlags.AwayTeamTechnicalDefeat, Constants.UI.MatchFlags.AwayTeamTechnicalDefeat);
            _matchFlagsMap.Add(Constants.DB.MatchFlags.Duration120Minutes, Constants.UI.MatchFlags.Duration120Minutes);
            _matchFlagsMap.Add(Constants.DB.MatchFlags.GoldenGoalRule, Constants.UI.MatchFlags.GoldenGoalRule);
            _matchFlagsMap.Add(Constants.DB.MatchFlags.IWasOnStadium, Constants.UI.MatchFlags.IWasOnStadium);
            _matchFlagsMap.Add(Constants.DB.MatchFlags.IWatchedMatchOnTV, Constants.UI.MatchFlags.IWatchedMatchOnTV);
            _matchFlagsMap.Add(Constants.DB.MatchFlags.NeutralField, Constants.UI.MatchFlags.NeutralField);
            _matchFlagsMap.Add(Constants.DB.MatchFlags.StadiumDisqualifiedNoSpectators, Constants.UI.MatchFlags.StadiumDisqualifiedNoSpectators);
        }

    }
}