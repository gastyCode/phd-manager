using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "study_programs",
                columns: table => new
                {
                    study_program_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_study_programs", x => x.study_program_id);
                });

            migrationBuilder.CreateTable(
                name: "study_program_subject",
                columns: table => new
                {
                    study_program_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subjects_subject_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_study_program_subject", x => new { x.study_program_id, x.subjects_subject_id });
                    table.ForeignKey(
                        name: "fk_study_program_subject_study_programs_study_program_id",
                        column: x => x.study_program_id,
                        principalTable: "study_programs",
                        principalColumn: "study_program_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    subject_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    semester = table.Column<string>(type: "text", nullable: false),
                    credits = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subjects", x => x.subject_id);
                });

            migrationBuilder.CreateTable(
                name: "theses",
                columns: table => new
                {
                    thesis_id = table.Column<Guid>(type: "uuid", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    supervisor_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_theses", x => x.thesis_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    first_login = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    study_program_id = table.Column<Guid>(type: "uuid", nullable: true),
                    student_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_users_study_programs_study_program_id",
                        column: x => x.study_program_id,
                        principalTable: "study_programs",
                        principalColumn: "study_program_id");
                    table.ForeignKey(
                        name: "fk_users_theses_student_id",
                        column: x => x.student_id,
                        principalTable: "theses",
                        principalColumn: "thesis_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_study_program_subject_subjects_subject_id",
                table: "study_program_subject",
                column: "subjects_subject_id");

            migrationBuilder.CreateIndex(
                name: "ix_subjects_user_id",
                table: "subjects",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_theses_supervisor_user_id",
                table: "theses",
                column: "supervisor_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_student_id",
                table: "users",
                column: "student_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_study_program_id",
                table: "users",
                column: "study_program_id");

            migrationBuilder.AddForeignKey(
                name: "fk_study_program_subject_subjects_subjects_subject_id",
                table: "study_program_subject",
                column: "subjects_subject_id",
                principalTable: "subjects",
                principalColumn: "subject_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_subjects_users_user_id",
                table: "subjects",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_theses_users_supervisor_user_id",
                table: "theses",
                column: "supervisor_user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_study_programs_study_program_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_theses_users_supervisor_user_id",
                table: "theses");

            migrationBuilder.DropTable(
                name: "study_program_subject");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "study_programs");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "theses");
        }
    }
}
