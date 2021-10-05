﻿// <auto-generated />
using System;
using KMChartsUpdater.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KMChartsUpdater.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210915115016_reportsFilename")]
    partial class reportsFilename
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.AccessToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccessTokens");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoginNormalized")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SessionKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Artist")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArtistNormalized")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsExplicit")
                        .HasColumnType("bit");

                    b.Property<string>("SavedThumb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subtitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleNormalized")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Audio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Artist")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArtistNormalized")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsExplicit")
                        .HasColumnType("bit");

                    b.Property<string>("SavedThumb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subtitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleNormalized")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Audios");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.AudioTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("AudioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AudioId");

                    b.ToTable("AudioTasks");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Chart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlatformId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("Charts");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.ChartFix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChartId")
                        .HasColumnType("int");

                    b.Property<string>("NormalDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChartId");

                    b.ToTable("ChartFixes");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.LabelToAudio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AudioId")
                        .HasColumnType("int");

                    b.Property<int?>("LabelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AudioId");

                    b.HasIndex("LabelId");

                    b.ToTable("LabelToAudios");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdOnPlatform")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlatformId")
                        .HasColumnType("int");

                    b.Property<string>("Tracks")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.PositionFix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int?>("AudioId")
                        .HasColumnType("int");

                    b.Property<int?>("ChartFixId")
                        .HasColumnType("int");

                    b.Property<int?>("ChartId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<long>("Likes")
                        .HasColumnType("bigint");

                    b.Property<string>("Playlists")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("Shift")
                        .HasColumnType("int");

                    b.Property<long>("Streams")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("AudioId");

                    b.HasIndex("ChartFixId");

                    b.HasIndex("ChartId");

                    b.ToTable("PositionFixes");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AudioTaskId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Playlists")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AudioTaskId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.AudioTask", b =>
                {
                    b.HasOne("KMChartsUpdater.DAL.Entities.Account", "Account")
                        .WithMany("AudioTasks")
                        .HasForeignKey("AccountId");

                    b.HasOne("KMChartsUpdater.DAL.Entities.Audio", "Audio")
                        .WithMany("Reports")
                        .HasForeignKey("AudioId");

                    b.Navigation("Account");

                    b.Navigation("Audio");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Chart", b =>
                {
                    b.HasOne("KMChartsUpdater.DAL.Entities.Platform", "Platform")
                        .WithMany("Charts")
                        .HasForeignKey("PlatformId");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.ChartFix", b =>
                {
                    b.HasOne("KMChartsUpdater.DAL.Entities.Chart", "Chart")
                        .WithMany("ChartFixes")
                        .HasForeignKey("ChartId");

                    b.Navigation("Chart");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.LabelToAudio", b =>
                {
                    b.HasOne("KMChartsUpdater.DAL.Entities.Audio", "Audio")
                        .WithMany("LabelToAudios")
                        .HasForeignKey("AudioId");

                    b.HasOne("KMChartsUpdater.DAL.Entities.Label", "Label")
                        .WithMany("LabelToAudios")
                        .HasForeignKey("LabelId");

                    b.Navigation("Audio");

                    b.Navigation("Label");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Playlist", b =>
                {
                    b.HasOne("KMChartsUpdater.DAL.Entities.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.PositionFix", b =>
                {
                    b.HasOne("KMChartsUpdater.DAL.Entities.Album", "Album")
                        .WithMany("PositionFixes")
                        .HasForeignKey("AlbumId");

                    b.HasOne("KMChartsUpdater.DAL.Entities.Audio", "Audio")
                        .WithMany("PositionFixes")
                        .HasForeignKey("AudioId");

                    b.HasOne("KMChartsUpdater.DAL.Entities.ChartFix", "ChartFix")
                        .WithMany("PositionFixes")
                        .HasForeignKey("ChartFixId");

                    b.HasOne("KMChartsUpdater.DAL.Entities.Chart", "Chart")
                        .WithMany("PositionFixes")
                        .HasForeignKey("ChartId");

                    b.Navigation("Album");

                    b.Navigation("Audio");

                    b.Navigation("Chart");

                    b.Navigation("ChartFix");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Report", b =>
                {
                    b.HasOne("KMChartsUpdater.DAL.Entities.AudioTask", "AudioTask")
                        .WithMany("Reports")
                        .HasForeignKey("AudioTaskId");

                    b.Navigation("AudioTask");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Account", b =>
                {
                    b.Navigation("AudioTasks");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Album", b =>
                {
                    b.Navigation("PositionFixes");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Audio", b =>
                {
                    b.Navigation("LabelToAudios");

                    b.Navigation("PositionFixes");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.AudioTask", b =>
                {
                    b.Navigation("Reports");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Chart", b =>
                {
                    b.Navigation("ChartFixes");

                    b.Navigation("PositionFixes");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.ChartFix", b =>
                {
                    b.Navigation("PositionFixes");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Label", b =>
                {
                    b.Navigation("LabelToAudios");
                });

            modelBuilder.Entity("KMChartsUpdater.DAL.Entities.Platform", b =>
                {
                    b.Navigation("Charts");
                });
#pragma warning restore 612, 618
        }
    }
}
