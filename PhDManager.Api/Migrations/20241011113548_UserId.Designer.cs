﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PhDManager.Api.Data;

#nullable disable

namespace PhDManager.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241011113548_UserId")]
    partial class UserId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PhDManager.Core.Models.StudyProgram", b =>
                {
                    b.Property<int>("StudyProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("study_program_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StudyProgramId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("StudyProgramId")
                        .HasName("pk_study_programs");

                    b.ToTable("study_programs", (string)null);
                });

            modelBuilder.Entity("PhDManager.Core.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("subject_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SubjectId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<int>("Credits")
                        .HasColumnType("integer")
                        .HasColumnName("credits");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("semester");

                    b.HasKey("SubjectId")
                        .HasName("pk_subjects");

                    b.ToTable("subjects", (string)null);
                });

            modelBuilder.Entity("PhDManager.Core.Models.Thesis", b =>
                {
                    b.Property<int>("ThesisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("thesis_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ThesisId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("ThesisId")
                        .HasName("pk_theses");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_theses_user_id");

                    b.ToTable("theses", (string)null);
                });

            modelBuilder.Entity("PhDManager.Core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.Property<DateTime?>("FirstLogin")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("first_login");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("uid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("UserId")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("PhDManager.Core.Models.Thesis", b =>
                {
                    b.HasOne("PhDManager.Core.Models.User", "User")
                        .WithMany("Theses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_theses_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PhDManager.Core.Models.User", b =>
                {
                    b.Navigation("Theses");
                });
#pragma warning restore 612, 618
        }
    }
}
