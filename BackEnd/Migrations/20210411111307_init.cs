using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    MaxSize = table.Column<int>(type: "int", nullable: false),
                    HasFemale = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseArea",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxSize = table.Column<int>(type: "int", nullable: false),
                    HasFemale = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseArea", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Hamsters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ownername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFemale = table.Column<bool>(type: "bit", nullable: false),
                    CheckedInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastExercise = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CageID = table.Column<int>(type: "int", nullable: true),
                    ExerciseAreaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hamsters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Hamsters_Cages_CageID",
                        column: x => x.CageID,
                        principalTable: "Cages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hamsters_ExerciseArea_ExerciseAreaID",
                        column: x => x.ExerciseAreaID,
                        principalTable: "ExerciseArea",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    ActivityLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HamsterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.ActivityLogID);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Hamsters_HamsterID",
                        column: x => x.HamsterID,
                        principalTable: "Hamsters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_HamsterID",
                table: "ActivityLogs",
                column: "HamsterID");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_CageID",
                table: "Hamsters",
                column: "CageID");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_ExerciseAreaID",
                table: "Hamsters",
                column: "ExerciseAreaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Hamsters");

            migrationBuilder.DropTable(
                name: "Cages");

            migrationBuilder.DropTable(
                name: "ExerciseArea");
        }
    }
}
