using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UaFDatabaseEF.Models
{
    public partial class UaFootballContext : DbContext
    {
        public UaFootballContext()
        {
        }

        public UaFootballContext(DbContextOptions<UaFootballContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Clubs> Clubs { get; set; }
        public virtual DbSet<Coaches> Coaches { get; set; }
        public virtual DbSet<Competitions> Competitions { get; set; }
        public virtual DbSet<CompetitionStages> CompetitionStages { get; set; }
        public virtual DbSet<CompetitionTypes> CompetitionTypes { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Fifaassociations> Fifaassociations { get; set; }
        public virtual DbSet<Matches> Matches { get; set; }
        public virtual DbSet<MatchEvents> MatchEvents { get; set; }
        public virtual DbSet<MatchLineups> MatchLineups { get; set; }
        public virtual DbSet<MatchNotes> MatchNotes { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Multimedia> Multimedia { get; set; }
        public virtual DbSet<MultimediaTags> MultimediaTags { get; set; }
        public virtual DbSet<NationalTeams> NationalTeams { get; set; }
        public virtual DbSet<PhotoGalleryAlbums> PhotoGalleryAlbums { get; set; }
        public virtual DbSet<PlayerPositions> PlayerPositions { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Referees> Referees { get; set; }
        public virtual DbSet<Seasons> Seasons { get; set; }
        public virtual DbSet<Stadiums> Stadiums { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }

        // Unable to generate entity type for table 'dbo.MatchLineups_Backup'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=UaFootball;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityId).HasColumnName("City_ID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnName("City_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("Country_ID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_Countries");
            });

            modelBuilder.Entity<Clubs>(entity =>
            {
                entity.HasKey(e => e.ClubId);

                entity.Property(e => e.ClubId).HasColumnName("Club_ID");

                entity.Property(e => e.CityId).HasColumnName("City_ID");

                entity.Property(e => e.ClubName)
                    .IsRequired()
                    .HasColumnName("Club_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasColumnName("Display_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.YearFound).HasColumnName("Year_Found");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clubs_Cities");
            });

            modelBuilder.Entity<Coaches>(entity =>
            {
                entity.HasKey(e => e.CoachId);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstNameInt)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastNameInt)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Coaches)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coaches_Countries");
            });

            modelBuilder.Entity<Competitions>(entity =>
            {
                entity.HasKey(e => e.CompetitionId);

                entity.Property(e => e.CompetitionId).HasColumnName("Competition_Id");

                entity.Property(e => e.CompetitionCd)
                    .HasColumnName("Competition_Cd")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompetitionLevelCd)
                    .IsRequired()
                    .HasColumnName("CompetitionLevel_Cd")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CompetitionName)
                    .IsRequired()
                    .HasColumnName("Competition_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompetitionStages>(entity =>
            {
                entity.HasKey(e => e.CompetitionStageId);

                entity.Property(e => e.CompetitionStageId).HasColumnName("CompetitionStage_ID");

                entity.Property(e => e.CompetitionId).HasColumnName("Competition_ID");

                entity.Property(e => e.CompetitionStageName)
                    .IsRequired()
                    .HasColumnName("CompetitionStage_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.Property(e => e.CountryId).HasColumnName("Country_ID");

                entity.Property(e => e.CountryCode)
                    .HasColumnName("Country_Code")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnName("Country_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FifaassociationId).HasColumnName("FIFAAssociation_ID");

                entity.HasOne(d => d.Fifaassociation)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.FifaassociationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Countries_FIFAAssociations");
            });

            modelBuilder.Entity<Fifaassociations>(entity =>
            {
                entity.HasKey(e => e.FifaassociationId);

                entity.ToTable("FIFAAssociations");

                entity.Property(e => e.FifaassociationId).HasColumnName("FIFAAssociation_ID");

                entity.Property(e => e.FifaassociationDescription)
                    .HasColumnName("FIFAAssociation_Description")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FifaassociationName)
                    .IsRequired()
                    .HasColumnName("FIFAAssociation_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Matches>(entity =>
            {
                entity.HasKey(e => e.MatchId);

                entity.Property(e => e.MatchId).HasColumnName("Match_Id");

                entity.Property(e => e.AdminNotes)
                    .HasMaxLength(4096)
                    .IsUnicode(false);

                entity.Property(e => e.AwayClubId).HasColumnName("AwayClub_Id");

                entity.Property(e => e.AwayNationalTeamId).HasColumnName("AwayNationalTeam_Id");

                entity.Property(e => e.CompetitionId).HasColumnName("Competition_Id");

                entity.Property(e => e.CompetitionStageId).HasColumnName("CompetitionStage_Id");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.HomeClubId).HasColumnName("HomeClub_Id");

                entity.Property(e => e.HomeNationalTeamId).HasColumnName("HomeNationalTeam_Id");

                entity.Property(e => e.RefereeId).HasColumnName("Referee_Id");

                entity.Property(e => e.SeasonId).HasColumnName("Season_Id");

                entity.Property(e => e.Sources)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(4096)
                    .IsUnicode(false);

                entity.Property(e => e.StadiumId).HasColumnName("Stadium_Id");

                entity.HasOne(d => d.AwayClub)
                    .WithMany(p => p.MatchesAwayClub)
                    .HasForeignKey(d => d.AwayClubId)
                    .HasConstraintName("FK_Matches_Clubs1");

                entity.HasOne(d => d.AwayNationalTeam)
                    .WithMany(p => p.MatchesAwayNationalTeam)
                    .HasForeignKey(d => d.AwayNationalTeamId)
                    .HasConstraintName("FK_Matches_NationalTeams1");

                entity.HasOne(d => d.Competition)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matches_Competitions");

                entity.HasOne(d => d.CompetitionStage)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.CompetitionStageId)
                    .HasConstraintName("FK_Matches_CompetitionStages");

                entity.HasOne(d => d.HomeClub)
                    .WithMany(p => p.MatchesHomeClub)
                    .HasForeignKey(d => d.HomeClubId)
                    .HasConstraintName("FK_Matches_Clubs");

                entity.HasOne(d => d.HomeNationalTeam)
                    .WithMany(p => p.MatchesHomeNationalTeam)
                    .HasForeignKey(d => d.HomeNationalTeamId)
                    .HasConstraintName("FK_Matches_NationalTeams");

                entity.HasOne(d => d.Referee)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.RefereeId)
                    .HasConstraintName("FK_Matches_Referees");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matches_Seasons");

                entity.HasOne(d => d.Stadium)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.StadiumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matches_Stadiums");
            });

            modelBuilder.Entity<MatchEvents>(entity =>
            {
                entity.HasKey(e => e.MatchEventId);

                entity.Property(e => e.MatchEventId).HasColumnName("MatchEvent_Id");

                entity.Property(e => e.EventCd)
                    .IsRequired()
                    .HasColumnName("Event_Cd")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.MatchId).HasColumnName("Match_Id");

                entity.Property(e => e.Player1Id).HasColumnName("Player1_Id");

                entity.Property(e => e.Player2Id).HasColumnName("Player2_Id");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchEvents)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchEvents_Matches");

                entity.HasOne(d => d.Player1)
                    .WithMany(p => p.MatchEventsPlayer1)
                    .HasForeignKey(d => d.Player1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchEvents_Players");

                entity.HasOne(d => d.Player2)
                    .WithMany(p => p.MatchEventsPlayer2)
                    .HasForeignKey(d => d.Player2Id)
                    .HasConstraintName("FK_MatchEvents_Players1");
            });

            modelBuilder.Entity<MatchLineups>(entity =>
            {
                entity.HasKey(e => e.MatchLineupId);

                entity.Property(e => e.MatchLineupId).HasColumnName("MatchLineup_Id");

                entity.Property(e => e.CoachId).HasColumnName("Coach_Id");

                entity.Property(e => e.MatchId).HasColumnName("Match_Id");

                entity.Property(e => e.PlayerId).HasColumnName("Player_Id");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.MatchLineups)
                    .HasForeignKey(d => d.CoachId)
                    .HasConstraintName("FK_MatchLineups_Coaches");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchLineups)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchLineups_Matches");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.MatchLineups)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_MatchLineups_Players");
            });

            modelBuilder.Entity<MatchNotes>(entity =>
            {
                entity.HasKey(e => e.MatchNoteId);

                entity.Property(e => e.MatchNoteId).HasColumnName("MatchNote_Id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.MatchId).HasColumnName("Match_Id");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchNotes)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchNotes_Matches");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Multimedia>(entity =>
            {
                entity.Property(e => e.MultimediaId).HasColumnName("Multimedia_ID");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTaken).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MultimediaSubTypeCd)
                    .IsRequired()
                    .HasColumnName("MultimediaSubType_CD")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.MultimediaTypeCd)
                    .IsRequired()
                    .HasColumnName("MultimediaType_CD")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MultimediaTags>(entity =>
            {
                entity.HasKey(e => e.MultimediaTagId);

                entity.Property(e => e.MultimediaTagId).HasColumnName("MultimediaTag_ID");

                entity.Property(e => e.ClubId).HasColumnName("Club_ID");

                entity.Property(e => e.MatchEventId).HasColumnName("MatchEvent_ID");

                entity.Property(e => e.MatchId).HasColumnName("Match_ID");

                entity.Property(e => e.MultimediaId).HasColumnName("Multimedia_ID");

                entity.Property(e => e.NationalTeamId).HasColumnName("NationalTeam_ID");

                entity.Property(e => e.PlayerId).HasColumnName("Player_ID");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.MultimediaTags)
                    .HasForeignKey(d => d.ClubId)
                    .HasConstraintName("FK_MultimediaTags_Clubs");

                entity.HasOne(d => d.MatchEvent)
                    .WithMany(p => p.MultimediaTags)
                    .HasForeignKey(d => d.MatchEventId)
                    .HasConstraintName("FK_MultimediaTags_MatchEvents");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MultimediaTags)
                    .HasForeignKey(d => d.MatchId)
                    .HasConstraintName("FK_MultimediaTags_Matches");

                entity.HasOne(d => d.Multimedia)
                    .WithMany(p => p.MultimediaTags)
                    .HasForeignKey(d => d.MultimediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MultimediaTags_Multimedia");

                entity.HasOne(d => d.NationalTeam)
                    .WithMany(p => p.MultimediaTags)
                    .HasForeignKey(d => d.NationalTeamId)
                    .HasConstraintName("FK_MultimediaTags_NationalTeams");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.MultimediaTags)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_MultimediaTags_Players");
            });

            modelBuilder.Entity<NationalTeams>(entity =>
            {
                entity.HasKey(e => e.NationalTeamId);

                entity.Property(e => e.NationalTeamId).HasColumnName("NationalTeam_Id");

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.Logo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NationalTeamTypeCd)
                    .IsRequired()
                    .HasColumnName("NationalTeamType_Cd")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.NationalTeams)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NationalTeams_Countries");
            });

            modelBuilder.Entity<PhotoGalleryAlbums>(entity =>
            {
                entity.Property(e => e.Author)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.IsUnt).HasColumnName("IsUNT");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderInternalId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlayerPositions>(entity =>
            {
                entity.HasKey(e => e.PlayerPositionId);

                entity.Property(e => e.PlayerPositionId).HasColumnName("PlayerPosition_Id");

                entity.Property(e => e.LineCd)
                    .IsRequired()
                    .HasColumnName("Line_Cd")
                    .HasMaxLength(10);

                entity.Property(e => e.PlayerId).HasColumnName("Player_Id");

                entity.Property(e => e.WingCd)
                    .HasColumnName("Wing_Cd")
                    .HasMaxLength(10);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerPositions)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlayerPositions_Players");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.PlayerId).HasColumnName("Player_Id");

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.DisplayName)
                    .HasColumnName("Display_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasColumnName("First_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstNameInt)
                    .HasColumnName("First_Name_Int")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastNameInt)
                    .HasColumnName("Last_Name_Int")
                    .HasMaxLength(50);

                entity.Property(e => e.LastUpdateDt)
                    .HasColumnName("LastUpdate_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.MiddleName)
                    .HasColumnName("Middle_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameSearchString)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UacityName)
                    .HasColumnName("UACity_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UaregionName)
                    .HasColumnName("UARegion_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Players_Countries");
            });

            modelBuilder.Entity<Referees>(entity =>
            {
                entity.HasKey(e => e.RefereeId);

                entity.Property(e => e.RefereeId).HasColumnName("Referee_Id");

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstNameEn)
                    .HasColumnName("FirstName_EN")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastNameEn)
                    .HasColumnName("LastName_EN")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Referees)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Referees_Countries");
            });

            modelBuilder.Entity<Seasons>(entity =>
            {
                entity.HasKey(e => e.SeasonId);

                entity.Property(e => e.SeasonId).HasColumnName("Season_Id");

                entity.Property(e => e.CompetitionLevelCd)
                    .IsRequired()
                    .HasColumnName("CompetitionLevel_Cd")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SeasonCd)
                    .HasColumnName("Season_Cd")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SeasonDescription)
                    .IsRequired()
                    .HasColumnName("Season_Description")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Stadiums>(entity =>
            {
                entity.HasKey(e => e.StadiumId);

                entity.Property(e => e.StadiumId).HasColumnName("Stadium_Id");

                entity.Property(e => e.CityId).HasColumnName("City_Id");

                entity.Property(e => e.Comments)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.StadiumName)
                    .IsRequired()
                    .HasColumnName("Stadium_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.YearBuilt).HasColumnName("Year_Built");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Stadiums)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stadiums_Cities");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.Property(e => e.TaskId).HasColumnName("Task_Id");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StatusCd)
                    .IsRequired()
                    .HasColumnName("Status_CD")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TypeCd)
                    .IsRequired()
                    .HasColumnName("Type_CD")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });
        }
    }
}
