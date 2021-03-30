﻿// <auto-generated />
using System;
using BackEnd;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(HamsterDayCareContext))]
    partial class HamsterDayCareContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEnd.Cage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxSize")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Cages");
                });

            modelBuilder.Entity("BackEnd.ExerciseArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxSize")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ExerciseArea");
                });

            modelBuilder.Entity("BackEnd.Hamster", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("CageID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CheckedInTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExerciseAreaID")
                        .HasColumnType("int");

                    b.Property<bool>("IsFemale")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastExercise")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ownername")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CageID");

                    b.HasIndex("ExerciseAreaID");

                    b.ToTable("Hamsters");
                });

            modelBuilder.Entity("BackEnd.Hamster", b =>
                {
                    b.HasOne("BackEnd.Cage", "Cage")
                        .WithMany("Hamsters")
                        .HasForeignKey("CageID");

                    b.HasOne("BackEnd.ExerciseArea", "ExerciseArea")
                        .WithMany("Hamsters")
                        .HasForeignKey("ExerciseAreaID");

                    b.Navigation("Cage");

                    b.Navigation("ExerciseArea");
                });

            modelBuilder.Entity("BackEnd.Cage", b =>
                {
                    b.Navigation("Hamsters");
                });

            modelBuilder.Entity("BackEnd.ExerciseArea", b =>
                {
                    b.Navigation("Hamsters");
                });
#pragma warning restore 612, 618
        }
    }
}
