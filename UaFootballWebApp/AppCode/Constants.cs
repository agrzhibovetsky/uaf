using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UaFootball.AppCode
{
    /// <summary>
    /// Summary description for Constants
    /// </summary>
    public static class Constants
    {
        public enum ObjectType
        {
            Country,
            City,
            FIFAAssociation,
            Stadium,
            Club,
            Competition,
            CompetitionStage,
            Season
        }

        public enum QueryType
        {
            StartsWith,
            Contains,
            All
        }

        public const string normalSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ' -aaaaaaaaAEеeeeeeeeeuuuuUoooooOOOOiiiiIIccccCCCCsssssSSnnrlllLyDZzzttdgRZ";
        public const string extraSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ' -āäăáãâàąÁÉæēéëèěęêėúūüůÜőóöôøÖÓØŐıîíïÍİćčçҫČĆÇҪšßşșśŠŞñńřļľłŁýĎŽźžţțđğŘŻ";

        public const string CountryCodeUA = "UA";

        public static class Pages
        {
            public const string List_Country = "~/WebApplication/Admin/CountryList.aspx";
            public const string List_City = "~/WebApplication/Admin/CityList.aspx";
            public const string Edit_Country = "~/WebApplication/Admin/CountryEdit.aspx";
            public const string Edit_City = "~/WebApplication/Admin/CityEdit.aspx";
            public const string List_Stadium = "~/WebApplication/Admin/StadiumList.aspx";
            public const string List_Club = "~/WebApplication/Admin/ClubList.aspx";
            public const string Edit_Stadium = "~/WebApplication/Admin/StadiumEdit.aspx";
            public const string Edit_Club = "~/WebApplication/Admin/ClubEdit.aspx";
            public const string List_Player = "~/WebApplication/Admin/PlayerList.aspx";
            public const string List_Player_Review = "~/WebApplication/Admin/PlayerReviewList.aspx";
            public const string Edit_Player = "~/WebApplication/Admin/PlayerEdit.aspx";
            public const string List_Referee = "~/WebApplication/Admin/RefereeList.aspx";
            public const string Edit_Referee = "~/WebApplication/Admin/RefereeEdit.aspx";
            public const string List_Coach = "~/WebApplication/Admin/CoachList.aspx";
            public const string Edit_Coach = "~/WebApplication/Admin/CoachEdit.aspx";
            public const string List_Match = "~/WebApplication/Admin/MatchList.aspx";
            public const string Edit_Match = "~/WebApplication/Admin/MatchEdit.aspx";
            public const string Autocomplete = "~/WebApplication/Autocomplete.ashx";
            public const string AdminApi = "/UaFootballAPI/Admin";
        }

        public static class ConfigKeys
        {

        }

        public static class DB
        {
            public const string CompetitionLevelCd_Club = "C";
            public const string CompetitionLevelCd_NationalTeam = "N";
            public const string NationalTeamTypeCd_National = "NAT";
            public const string NationalTeamTypeCd_U21 = "U21";

            public const string UACountryCode = "UA";

            public static class MutlimediaTypes
            {
                public const char Image = 'I';
                public const char Video = 'V';
            }

            public static class MutlimediaSubTypes
            {
                public const string PlayerLogo = "PL";
                public const string ClubLogo = "CL";
                public const string NationalTeamLogo = "NL";
                public const string MatchPhoto = "MP";
                public const string MatchVideo = "MV";
            }
            
            public static class EventTypeCodes
            {
                public const string YellowCard = "Y1";
                public const string SecondYellowCard = "Y2";
                public const string RedCard = "R";
                public const string Goal = "G";
                public const string Substitution = "S";
                public const string Penalty = "P";
                public const string MissedPenalty = "MP";
                public const string CoachYellowCard = "YC";
                //public const string PenaltyShootoutScored = "PSS";
                //public const string PenaltyShootoutMissed = "PSM";
            }
            
            public static class EventFlags
            {
                // Yellow, 2nd yellow, red card
                public const int Arm = 0x01;
                public const int RoughPlay = 0x02;
                public const int Simulation = 0x04;
                public const int GameDelay = 0x08;
                public const int UnsportingBehavior = 0x10;
                public const int Talks = 0x20;
                public const int FoulOfLastHope = 0x40;
                public const int AfterFinalWhistle = 0x80;

                //Goal, PenaltyShootoutScored
                public const int LeftLeg = 0x01;
                public const int RightLeg = 0x02;
                public const int Head = 0x04;
                //public const int Penalty = 0x08;
                
                public const int GoalClass1 = 0x10;
                public const int GoalClass2 = 0x20;
                public const int GoalClass3 = 0x40;
                public const int OwnGoal = 0x80;
                public const int LongDistance = 0x100;
                public const int MiddleDistance = 0x200;
                public const int ShortDistance = 0x400;
                public const int Corner = 0x800;
                public const int FreeKick = 0x1000;
                public const int OneToOne = 0x2000;
                public const int Rikoshet = 0x4000;
                public const int CounterAttack = 0x8000;
                public const int Solo = 0x10000;
                public const int Dobivanie = 0x20000;
                public const int Combinacia = 0x40000;
                public const int GoalClass4 = 0x80000;
                public const int OtherBodyPart = 0x100000;
                public const int GoalPass = 0x200000;
                public const int Pyatka = 0x400000;
                public const int Razvorot = 0x800000;
                public const int GKFault = 0x1000000;
                public const int Dribble = 0x2000000;
                public const int BetweenLegs = 0x4000000;

                //Substitution
                public const int Injury = 0x01;

                //Missed penalty
                public const int Post = 0x01;
                public const int Crossbar = 0x02;
                public const int SavedbyKeeper = 0x04;
                public const int Miss = 0x08;
                //public const int PostMatchPenalty = 0x20;

                //Penalty
                public const int LeftBottom = 0x04;
                public const int LeftTop = 0x08;
                public const int RightBottom = 0x10;
                public const int RightTop = 0x20;
                public const int Center = 0x100;
                public const int PanenkaStle = 0x40;
                public const int PostMatchPenalty = 0x80;
            }

            public static class MultimediaTags
            {
                public const int BadQuality = 0x01;
                public const int AwayTeamPhoto = 0x02;
                public const int HomeTeamPhoto = 0x04;
            }

            public static class MatchFlags
            {
                public const int HomeTeamTechnicalDefeat = 0x01;
                public const int AwayTeamTechnicalDefeat = 0x02;
                public const int StadiumDisqualifiedNoSpectators = 0x04;
                public const int NeutralField = 0x08;
                public const int Duration120Minutes = 0x10;
                public const int IWasOnStadium = 0x20;
                public const int IWatchedMatchOnTV = 0x40;
                public const int GoldenGoalRule = 0x80;
            }

            public static class LineupFlags
            {
                public const int Goalkeeper = 0x01;
                public const int Captain = 0x02;
                public const int Debut = 0x04;
                public const int CoachInCharge = 0x08;
            }
        }

        public static class UI
        {
            public const string DropdownDefaultValue = "0";
            public const string DropdownDefaultText = "Выбрать..";

            public const string NationalTeamType_National = "";
            public const string NationalTeamType_U21 = "U-21";

            public static class MutlimediaSubTypes
            {
                public const string PlayerLogo = "Футболист - лого";
                public const string ClubLogo = "Клуб - лого";
                public const string NationalTeamLogo = "Сборная - лого";
                public const string MatchPhoto = "Фото - матч";
                public const string MatchVideo = "Видео - матч";
            }

            public static class EventTypeCodes
            {
                public const string YellowCard = "Желтая карточка";
                public const string SecondYellowCard = "2-я желтая карточка";
                public const string RedCard = "Красная карточка";
                public const string Goal = "Гол!";
                public const string Substitution = "Замена";
                public const string MissedPenalty = "Незабитый пенальти";
                public const string Penalty = "Пенальти";
                public const string CoachYellowCard = "Желтая карточка (тренер)";
                //public const string PenaltyShootoutScored = "Послематчевый пенальти - гол";
                //public const string PenaltyShootoutMissed = "Послематчевый пенальти - мимо";
            }

            public static class EventFlags
            {
                // Yellow, 2nd yellow, red card
                public const string Arm = "Игра рукой";
                public const string RoughPlay = "Грубая игра";
                public const string Simulation = "Симуляция";
                public const string GameDelay = "Задержка игры";
                public const string UnsportingBehavior = "Неспортивное поведение";
                public const string Talks = "Разговоры с судьей";
                public const string FoulOfLastHope = "Фол последней надежды";
                public const string AfterFinalWhistle = "После финального свистка";

                //Goal, PenaltyShootoutScored
                public const string LeftLeg = "Левой ногой";
                public const string RightLeg = "Правой ногой";
                public const string Head = "Головой";
                public const string Penalty = "Пенальти";
                public const string Corner = "Штрафной (навес), угловой";
                public const string FreeKick = "Штрафной (прямой)";
                public const string GoalClass1 = "Необычный гол";
                public const string GoalClass2 = "Отличный гол";
                public const string GoalClass3 = "Шедевральный гол";
                public const string OwnGoal = "Автогол";
                public const string LongDistance = "С дальней дистанции";
                public const string MiddleDistance = "Со средней дистанции";
                public const string ShortDistance = "С близкой дистанции";
                public const string OneToOne = "Выход один на один";
                public const string Rikoshet = "Рикошет";
                public const string CounterAttack = "Контратака";
                public const string Solo = "Сольный проход";
                public const string Dobivanie = "Добивание";
                public const string Combinacia = "Комбинация";
                public const string GoalClass4 = "Хороший гол";
                public const string OtherBodyPart = "Другой частью";
                public const string GoalPass = "Отличный голевой пас";
                public const string Pyatka = "Пас/удар пяткой";
                public const string Razvorot = "Удар с разворота/через себя";
                public const string GKFault = "Ошибка вратаря";
                public const string Dribble = "Дриблинг, техника";
                public const string BetweenLegs = "Между ног вратарю";

                //Substitution
                public const string Injury = "Травма";

                //Missed penalty, PenaltyShootoutMissed
                public const string Post = "Штанга";
                public const string Crossbar = "Перекладина";
                public const string SavedbyKeeper = "Вратарь"  ;
                public const string Miss = "Мимо";

                //Penalty
                public const string LeftBottom = "Левый нижний";
                public const string LeftTop = "Левый верхний";
                public const string RightBottom = "Правый нижний";
                public const string RightTop = "Правый верхний";
                public const string Center = "По центру";
                public const string PanenkaStle = "В стиле Паненки";
                public const string PostMatchPenalty = "Послематчевый";
            }

            public static class MultimediaTags
            {
                public const string BadQuality = "Плохое качество";
                public const string AwayTeamPhoto = "Командное фото (гость)";
                public const string HomeTeamPhoto = "Командное фото (хозяин)";
            }

            public static class MatchFlags
            {
                public const string HomeTeamTechnicalDefeat = "Тех. пор - хозяева";
                public const string AwayTeamTechnicalDefeat = "Тех. пор - гости";
                public const string StadiumDisqualifiedNoSpectators = "При пустых трибунах";
                public const string NeutralField = "Нейтральное поле";
                public const string Duration120Minutes = "Длина матча - 120 минут";
                public const string GoldenGoalRule = "Правило золотого гола";
                public const string IWasOnStadium = "Я был на стадионе";
                public const string IWatchedMatchOnTV = "Я видел матч по ТВ";
            }
        }

        public static class QueryParam
        {
            public const string ObjectId = "objectId";
            public const string NationalTeam = "nt";
            public const string PlayerId = "playerId";
        }

        public static class Paths
        {
            public const string FullWebRoot = "/UaFootball/WebApplication/";
            //scripts
            
            public const string AutocompletePath = "~/WebApplication/Scripts/autocomplete.js";
            public const string ColorboxPath = "~/WebApplication/Scripts/jquery.colorbox.js";

            //web
            public const string RootWebPath = "~/WebApplication/";
            public const string DropBoxWebPath = "~/WebApplication/Dropbox/";
            public const string MutlimediaWebRoot = "~/WebApplication/Multimedia/";

            

            //file system
            public const string DropBoxPath = @"D:\Andrey\Projects\git\UaFootball\UaFootballWebApp\WebApplication\Dropbox\";
            public const string MultimediaStorageRoot = @"D:\Andrey\Projects\git\UaFootball\UaFootballWebApp\WebApplication\Multimedia\";

            public const string JQueryKey = "JQuery";
            public const string JQueryUIKey = "JQueryUI";
            public const string AutocompleteKey = "autocomplete";
            public const string ColorboxKey = "Colorbox";
        }

        public static List<MatchNoteSetup> MatchNoteSetups
        {
            get;
        }
        



        static Constants()
        {
            MatchNoteSetups = new List<MatchNoteSetup>();
            MatchNoteSetup genericMatch = new MatchNoteSetup { Code = "genMatch", Description = "Общее о матче" };
            MatchNoteSetup noSpectators = new MatchNoteSetup { Code = "noSpect", Description = "Отсутвие зрителей", Options = new List<string>() };
            noSpectators.Options.AddRange(new string[] { "Дисквалифицирован - выбегание зрителей", "Дисквалифицирован - расизм", "Запрет на пристуствие зрителей по причине эпидемиологического характера" });
            MatchNoteSetup genericSpectators = new MatchNoteSetup { Code = "genSpect", Description = "Зрители" };
            MatchNoteSetup genHomeLineup = new MatchNoteSetup { Code = "genHmeLnup", Description = "Состав хозяев" };
            MatchNoteSetup genAwayLineup = new MatchNoteSetup { Code = "genAwyLnup", Description = "Состав гостей" };
            MatchNoteSetup genEvents = new MatchNoteSetup { Code = "genEvents", Description = "События матча" };
            MatchNoteSetup genStadium = new MatchNoteSetup { Code = "genStadium", Description = "Стадион" };
            MatchNoteSetup homeCoach = new MatchNoteSetup { Code = "homeCoach", Description = "Тренер хозяев" };
            MatchNoteSetup awayCoach = new MatchNoteSetup { Code = "awayCoach", Description = "Тренер гостей" };
            MatchNoteSetups.AddRange(new MatchNoteSetup[] { genericMatch, noSpectators, genericSpectators, genHomeLineup, genAwayLineup, genEvents, genStadium, homeCoach, awayCoach });
        }
    } 
}