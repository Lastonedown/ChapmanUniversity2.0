using Microsoft.EntityFrameworkCore.Migrations;

namespace ChapmanUniversity1._0.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Students_StudentId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_StudentId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Semesters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_StudentId",
                table: "Semesters",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Students_StudentId",
                table: "Semesters",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
