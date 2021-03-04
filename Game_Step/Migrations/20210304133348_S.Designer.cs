﻿// <auto-generated />
using System;
using Game_Step.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Game_Step.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210304133348_S")]
    partial class S
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Game_Step.Models.Developer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("Game_Step.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Article")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Developer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Features")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Game_Step.Models.MinimumSystemRequirements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DirectX")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("HDD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RAM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoCard")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("MinimumSystemRequirements");
                });

            modelBuilder.Entity("Game_Step.Models.RecommendedSystemRequirements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DirectX")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("HDD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RAM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoCard")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("RecommendedSystemRequirements");
                });

            modelBuilder.Entity("Game_Step.Models.MinimumSystemRequirements", b =>
                {
                    b.HasOne("Game_Step.Models.Game", "Game")
                        .WithMany("MinSysReq")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Game_Step.Models.RecommendedSystemRequirements", b =>
                {
                    b.HasOne("Game_Step.Models.Game", "Game")
                        .WithMany("RecSysReq")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Game_Step.Models.Game", b =>
                {
                    b.Navigation("MinSysReq");

                    b.Navigation("RecSysReq");
                });
#pragma warning restore 612, 618
        }
    }
}
