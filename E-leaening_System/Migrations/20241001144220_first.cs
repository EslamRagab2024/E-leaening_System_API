using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_leaening_System.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Students_studentid",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_TheQuizes_quizessid",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_quizessid",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_studentid",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "quizessid",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "studentid",
                table: "Instructors");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Instructors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_AccountId",
                table: "Instructors",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_AspNetUsers_AccountId",
                table: "Instructors",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_AspNetUsers_AccountId",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_AccountId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Instructors");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Instructors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "quizessid",
                table: "Instructors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "studentid",
                table: "Instructors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_quizessid",
                table: "Instructors",
                column: "quizessid",
                unique: true,
                filter: "[quizessid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_studentid",
                table: "Instructors",
                column: "studentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Students_studentid",
                table: "Instructors",
                column: "studentid",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_TheQuizes_quizessid",
                table: "Instructors",
                column: "quizessid",
                principalTable: "TheQuizes",
                principalColumn: "Id");
        }
    }
}
