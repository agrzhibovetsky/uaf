using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaFDatabaseEF.Migrations
{
    public partial class Initialmodelload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "__MigrationHistory",
            //    columns: table => new
            //    {
            //        MigrationId = table.Column<string>(maxLength: 150, nullable: false),
            //        ContextKey = table.Column<string>(maxLength: 300, nullable: false),
            //        Model = table.Column<byte[]>(nullable: false),
            //        ProductVersion = table.Column<string>(maxLength: 32, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK___MigrationHistory", x => new { x.MigrationId, x.ContextKey });
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Competitions",
            //    columns: table => new
            //    {
            //        Competition_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Competition_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        CompetitionLevel_Cd = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
            //        Competition_Cd = table.Column<string>(unicode: false, maxLength: 10, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Competitions", x => x.Competition_Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CompetitionStages",
            //    columns: table => new
            //    {
            //        CompetitionStage_ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Competition_ID = table.Column<int>(nullable: false),
            //        CompetitionStage_Name = table.Column<string>(maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CompetitionStages", x => x.CompetitionStage_ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FIFAAssociations",
            //    columns: table => new
            //    {
            //        FIFAAssociation_ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        FIFAAssociation_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        FIFAAssociation_Description = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FIFAAssociations", x => x.FIFAAssociation_ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Multimedia",
            //    columns: table => new
            //    {
            //        Multimedia_ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        MultimediaType_CD = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
            //        MultimediaSubType_CD = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
            //        FilePath = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        FileName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        Source = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        Author = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        Description = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
            //        Flags = table.Column<long>(nullable: true),
            //        DateTaken = table.Column<DateTime>(type: "datetime", nullable: true),
            //        DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
            //        DateUpdated = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Multimedia", x => x.Multimedia_ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PhotoGalleryAlbums",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ProviderCode = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
            //        Date = table.Column<DateTime>(type: "datetime", nullable: true),
            //        Name = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        Author = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
            //        Url = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
            //        ProviderInternalId = table.Column<string>(unicode: false, maxLength: 32, nullable: true),
            //        IsUNT = table.Column<bool>(nullable: true),
            //        DateAdded = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PhotoGalleryAlbums", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Seasons",
            //    columns: table => new
            //    {
            //        Season_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Season_Description = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        CompetitionLevel_Cd = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
            //        Season_Cd = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
            //        StartDate = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Seasons", x => x.Season_Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tasks",
            //    columns: table => new
            //    {
            //        Task_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Description = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
            //        Status_CD = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
            //        Type_CD = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
            //        Comments = table.Column<string>(unicode: false, maxLength: 5000, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tasks", x => x.Task_Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Countries",
            //    columns: table => new
            //    {
            //        Country_ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Country_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        Country_Code = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
            //        FIFAAssociation_ID = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Countries", x => x.Country_ID);
            //        table.ForeignKey(
            //            name: "FK_Countries_FIFAAssociations",
            //            column: x => x.FIFAAssociation_ID,
            //            principalTable: "FIFAAssociations",
            //            principalColumn: "FIFAAssociation_ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Cities",
            //    columns: table => new
            //    {
            //        City_ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        City_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        Country_ID = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Cities", x => x.City_ID);
            //        table.ForeignKey(
            //            name: "FK_Cities_Countries",
            //            column: x => x.Country_ID,
            //            principalTable: "Countries",
            //            principalColumn: "Country_ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Coaches",
            //    columns: table => new
            //    {
            //        CoachId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        CountryId = table.Column<int>(nullable: false),
            //        FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        FirstNameInt = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        LastNameInt = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        DOB = table.Column<DateTime>(type: "date", nullable: false),
            //        ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Coaches", x => x.CoachId);
            //        table.ForeignKey(
            //            name: "FK_Coaches_Countries",
            //            column: x => x.CountryId,
            //            principalTable: "Countries",
            //            principalColumn: "Country_ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NationalTeams",
            //    columns: table => new
            //    {
            //        NationalTeam_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Country_Id = table.Column<int>(nullable: false),
            //        Logo = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        NationalTeamType_Cd = table.Column<string>(unicode: false, maxLength: 3, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NationalTeams", x => x.NationalTeam_Id);
            //        table.ForeignKey(
            //            name: "FK_NationalTeams_Countries",
            //            column: x => x.Country_Id,
            //            principalTable: "Countries",
            //            principalColumn: "Country_ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Players",
            //    columns: table => new
            //    {
            //        Player_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        First_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        Last_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        Middle_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        Display_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        DOB = table.Column<DateTime>(type: "date", nullable: true),
            //        Height = table.Column<int>(nullable: true),
            //        Weight = table.Column<int>(nullable: true),
            //        Country_Id = table.Column<int>(nullable: false),
            //        First_Name_Int = table.Column<string>(maxLength: 50, nullable: true),
            //        Last_Name_Int = table.Column<string>(maxLength: 50, nullable: true),
            //        RequiresReview = table.Column<bool>(nullable: true),
            //        UACity_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        UARegion_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        LastUpdate_DT = table.Column<DateTime>(type: "datetime", nullable: true),
            //        NameSearchString = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Players", x => x.Player_Id);
            //        table.ForeignKey(
            //            name: "FK_Players_Countries",
            //            column: x => x.Country_Id,
            //            principalTable: "Countries",
            //            principalColumn: "Country_ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Referees",
            //    columns: table => new
            //    {
            //        Referee_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        Country_Id = table.Column<int>(nullable: false),
            //        DOB = table.Column<DateTime>(type: "date", nullable: true),
            //        FirstName_EN = table.Column<string>(maxLength: 50, nullable: true),
            //        LastName_EN = table.Column<string>(maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Referees", x => x.Referee_Id);
            //        table.ForeignKey(
            //            name: "FK_Referees_Countries",
            //            column: x => x.Country_Id,
            //            principalTable: "Countries",
            //            principalColumn: "Country_ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Clubs",
            //    columns: table => new
            //    {
            //        Club_ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Club_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        Display_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        Logo = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
            //        Year_Found = table.Column<int>(nullable: true),
            //        City_ID = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Clubs", x => x.Club_ID);
            //        table.ForeignKey(
            //            name: "FK_Clubs_Cities",
            //            column: x => x.City_ID,
            //            principalTable: "Cities",
            //            principalColumn: "City_ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Stadiums",
            //    columns: table => new
            //    {
            //        Stadium_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Stadium_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
            //        Capacity = table.Column<int>(nullable: false),
            //        City_Id = table.Column<int>(nullable: false),
            //        Year_Built = table.Column<int>(nullable: false),
            //        Comments = table.Column<string>(unicode: false, maxLength: 1024, nullable: true),
            //        DateAdded = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Stadiums", x => x.Stadium_Id);
            //        table.ForeignKey(
            //            name: "FK_Stadiums_Cities",
            //            column: x => x.City_Id,
            //            principalTable: "Cities",
            //            principalColumn: "City_ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PlayerPositions",
            //    columns: table => new
            //    {
            //        PlayerPosition_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Player_Id = table.Column<int>(nullable: false),
            //        Line_Cd = table.Column<string>(maxLength: 10, nullable: false),
            //        Wing_Cd = table.Column<string>(maxLength: 10, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PlayerPositions", x => x.PlayerPosition_Id);
            //        table.ForeignKey(
            //            name: "FK_PlayerPositions_Players",
            //            column: x => x.Player_Id,
            //            principalTable: "Players",
            //            principalColumn: "Player_Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Matches",
            //    columns: table => new
            //    {
            //        Match_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        HomeClub_Id = table.Column<int>(nullable: true),
            //        AwayClub_Id = table.Column<int>(nullable: true),
            //        HomeNationalTeam_Id = table.Column<int>(nullable: true),
            //        AwayNationalTeam_Id = table.Column<int>(nullable: true),
            //        HomeScore = table.Column<short>(nullable: false),
            //        AwayScore = table.Column<short>(nullable: false),
            //        HomePenaltyScore = table.Column<short>(nullable: true),
            //        AwayPenaltyScore = table.Column<short>(nullable: true),
            //        Competition_Id = table.Column<int>(nullable: false),
            //        CompetitionStage_Id = table.Column<int>(nullable: true),
            //        Season_Id = table.Column<int>(nullable: false),
            //        Stadium_Id = table.Column<int>(nullable: false),
            //        Spectators = table.Column<int>(nullable: true),
            //        Referee_Id = table.Column<int>(nullable: true),
            //        Date = table.Column<DateTime>(type: "datetime", nullable: false),
            //        Flags = table.Column<int>(nullable: true),
            //        SpecialNote = table.Column<string>(unicode: false, maxLength: 4096, nullable: true),
            //        AdminNotes = table.Column<string>(unicode: false, maxLength: 4096, nullable: true),
            //        Sources = table.Column<string>(unicode: false, maxLength: 1024, nullable: true),
            //        CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Matches", x => x.Match_Id);
            //        table.ForeignKey(
            //            name: "FK_Matches_Clubs1",
            //            column: x => x.AwayClub_Id,
            //            principalTable: "Clubs",
            //            principalColumn: "Club_ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Matches_NationalTeams1",
            //            column: x => x.AwayNationalTeam_Id,
            //            principalTable: "NationalTeams",
            //            principalColumn: "NationalTeam_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Matches_Competitions",
            //            column: x => x.Competition_Id,
            //            principalTable: "Competitions",
            //            principalColumn: "Competition_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Matches_CompetitionStages",
            //            column: x => x.CompetitionStage_Id,
            //            principalTable: "CompetitionStages",
            //            principalColumn: "CompetitionStage_ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Matches_Clubs",
            //            column: x => x.HomeClub_Id,
            //            principalTable: "Clubs",
            //            principalColumn: "Club_ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Matches_NationalTeams",
            //            column: x => x.HomeNationalTeam_Id,
            //            principalTable: "NationalTeams",
            //            principalColumn: "NationalTeam_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Matches_Referees",
            //            column: x => x.Referee_Id,
            //            principalTable: "Referees",
            //            principalColumn: "Referee_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Matches_Seasons",
            //            column: x => x.Season_Id,
            //            principalTable: "Seasons",
            //            principalColumn: "Season_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Matches_Stadiums",
            //            column: x => x.Stadium_Id,
            //            principalTable: "Stadiums",
            //            principalColumn: "Stadium_Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MatchEvents",
            //    columns: table => new
            //    {
            //        MatchEvent_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Event_Cd = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
            //        Minute = table.Column<int>(nullable: false),
            //        Player1_Id = table.Column<int>(nullable: false),
            //        Player2_Id = table.Column<int>(nullable: true),
            //        Match_Id = table.Column<int>(nullable: false),
            //        EventFlags = table.Column<long>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MatchEvents", x => x.MatchEvent_Id);
            //        table.ForeignKey(
            //            name: "FK_MatchEvents_Matches",
            //            column: x => x.Match_Id,
            //            principalTable: "Matches",
            //            principalColumn: "Match_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MatchEvents_Players",
            //            column: x => x.Player1_Id,
            //            principalTable: "Players",
            //            principalColumn: "Player_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MatchEvents_Players1",
            //            column: x => x.Player2_Id,
            //            principalTable: "Players",
            //            principalColumn: "Player_Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MatchLineups",
            //    columns: table => new
            //    {
            //        MatchLineup_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Player_Id = table.Column<int>(nullable: true),
            //        ShirtNumber = table.Column<int>(nullable: true),
            //        IsHomeTeamPlayer = table.Column<bool>(nullable: false),
            //        IsSubstitute = table.Column<bool>(nullable: false),
            //        Match_Id = table.Column<int>(nullable: false),
            //        Coach_Id = table.Column<int>(nullable: true),
            //        Flags = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MatchLineups", x => x.MatchLineup_Id);
            //        table.ForeignKey(
            //            name: "FK_MatchLineups_Coaches",
            //            column: x => x.Coach_Id,
            //            principalTable: "Coaches",
            //            principalColumn: "CoachId",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MatchLineups_Matches",
            //            column: x => x.Match_Id,
            //            principalTable: "Matches",
            //            principalColumn: "Match_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MatchLineups_Players",
            //            column: x => x.Player_Id,
            //            principalTable: "Players",
            //            principalColumn: "Player_Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MatchNotes",
            //    columns: table => new
            //    {
            //        MatchNote_Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Match_Id = table.Column<int>(nullable: false),
            //        Code = table.Column<string>(maxLength: 10, nullable: false),
            //        Text = table.Column<string>(unicode: false, maxLength: 2048, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MatchNotes", x => x.MatchNote_Id);
            //        table.ForeignKey(
            //            name: "FK_MatchNotes_Matches",
            //            column: x => x.Match_Id,
            //            principalTable: "Matches",
            //            principalColumn: "Match_Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MultimediaTags",
            //    columns: table => new
            //    {
            //        MultimediaTag_ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Multimedia_ID = table.Column<int>(nullable: false),
            //        Player_ID = table.Column<int>(nullable: true),
            //        Match_ID = table.Column<int>(nullable: true),
            //        MatchEvent_ID = table.Column<int>(nullable: true),
            //        Club_ID = table.Column<int>(nullable: true),
            //        NationalTeam_ID = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MultimediaTags", x => x.MultimediaTag_ID);
            //        table.ForeignKey(
            //            name: "FK_MultimediaTags_Clubs",
            //            column: x => x.Club_ID,
            //            principalTable: "Clubs",
            //            principalColumn: "Club_ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MultimediaTags_MatchEvents",
            //            column: x => x.MatchEvent_ID,
            //            principalTable: "MatchEvents",
            //            principalColumn: "MatchEvent_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MultimediaTags_Matches",
            //            column: x => x.Match_ID,
            //            principalTable: "Matches",
            //            principalColumn: "Match_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MultimediaTags_Multimedia",
            //            column: x => x.Multimedia_ID,
            //            principalTable: "Multimedia",
            //            principalColumn: "Multimedia_ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MultimediaTags_NationalTeams",
            //            column: x => x.NationalTeam_ID,
            //            principalTable: "NationalTeams",
            //            principalColumn: "NationalTeam_Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MultimediaTags_Players",
            //            column: x => x.Player_ID,
            //            principalTable: "Players",
            //            principalColumn: "Player_Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Cities_Country_ID",
            //    table: "Cities",
            //    column: "Country_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Clubs_City_ID",
            //    table: "Clubs",
            //    column: "City_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Coaches_CountryId",
            //    table: "Coaches",
            //    column: "CountryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Countries_FIFAAssociation_ID",
            //    table: "Countries",
            //    column: "FIFAAssociation_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_AwayClub_Id",
            //    table: "Matches",
            //    column: "AwayClub_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_AwayNationalTeam_Id",
            //    table: "Matches",
            //    column: "AwayNationalTeam_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_Competition_Id",
            //    table: "Matches",
            //    column: "Competition_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_CompetitionStage_Id",
            //    table: "Matches",
            //    column: "CompetitionStage_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_HomeClub_Id",
            //    table: "Matches",
            //    column: "HomeClub_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_HomeNationalTeam_Id",
            //    table: "Matches",
            //    column: "HomeNationalTeam_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_Referee_Id",
            //    table: "Matches",
            //    column: "Referee_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_Season_Id",
            //    table: "Matches",
            //    column: "Season_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_Stadium_Id",
            //    table: "Matches",
            //    column: "Stadium_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MatchEvents_Match_Id",
            //    table: "MatchEvents",
            //    column: "Match_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MatchEvents_Player1_Id",
            //    table: "MatchEvents",
            //    column: "Player1_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MatchEvents_Player2_Id",
            //    table: "MatchEvents",
            //    column: "Player2_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MatchLineups_Coach_Id",
            //    table: "MatchLineups",
            //    column: "Coach_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MatchLineups_Match_Id",
            //    table: "MatchLineups",
            //    column: "Match_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MatchLineups_Player_Id",
            //    table: "MatchLineups",
            //    column: "Player_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MatchNotes_Match_Id",
            //    table: "MatchNotes",
            //    column: "Match_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MultimediaTags_Club_ID",
            //    table: "MultimediaTags",
            //    column: "Club_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MultimediaTags_MatchEvent_ID",
            //    table: "MultimediaTags",
            //    column: "MatchEvent_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MultimediaTags_Match_ID",
            //    table: "MultimediaTags",
            //    column: "Match_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MultimediaTags_Multimedia_ID",
            //    table: "MultimediaTags",
            //    column: "Multimedia_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MultimediaTags_NationalTeam_ID",
            //    table: "MultimediaTags",
            //    column: "NationalTeam_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MultimediaTags_Player_ID",
            //    table: "MultimediaTags",
            //    column: "Player_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NationalTeams_Country_Id",
            //    table: "NationalTeams",
            //    column: "Country_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PlayerPositions_Player_Id",
            //    table: "PlayerPositions",
            //    column: "Player_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Players_Country_Id",
            //    table: "Players",
            //    column: "Country_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Referees_Country_Id",
            //    table: "Referees",
            //    column: "Country_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Stadiums_City_Id",
            //    table: "Stadiums",
            //    column: "City_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "__MigrationHistory");

            //migrationBuilder.DropTable(
            //    name: "MatchLineups");

            //migrationBuilder.DropTable(
            //    name: "MatchNotes");

            //migrationBuilder.DropTable(
            //    name: "MultimediaTags");

            //migrationBuilder.DropTable(
            //    name: "PhotoGalleryAlbums");

            //migrationBuilder.DropTable(
            //    name: "PlayerPositions");

            //migrationBuilder.DropTable(
            //    name: "Tasks");

            //migrationBuilder.DropTable(
            //    name: "Coaches");

            //migrationBuilder.DropTable(
            //    name: "MatchEvents");

            //migrationBuilder.DropTable(
            //    name: "Multimedia");

            //migrationBuilder.DropTable(
            //    name: "Matches");

            //migrationBuilder.DropTable(
            //    name: "Players");

            //migrationBuilder.DropTable(
            //    name: "Clubs");

            //migrationBuilder.DropTable(
            //    name: "NationalTeams");

            //migrationBuilder.DropTable(
            //    name: "Competitions");

            //migrationBuilder.DropTable(
            //    name: "CompetitionStages");

            //migrationBuilder.DropTable(
            //    name: "Referees");

            //migrationBuilder.DropTable(
            //    name: "Seasons");

            //migrationBuilder.DropTable(
            //    name: "Stadiums");

            //migrationBuilder.DropTable(
            //    name: "Cities");

            //migrationBuilder.DropTable(
            //    name: "Countries");

            //migrationBuilder.DropTable(
            //    name: "FIFAAssociations");
        }
    }
}
