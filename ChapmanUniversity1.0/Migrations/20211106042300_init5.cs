using Microsoft.EntityFrameworkCore.Migrations;

namespace ChapmanUniversity1._0.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "StudentEnrollments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_CourseId",
                table: "StudentEnrollments",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollments_Courses_CourseId",
                table: "StudentEnrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollments_Courses_CourseId",
                table: "StudentEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_StudentEnrollments_CourseId",
                table: "StudentEnrollments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "StudentEnrollments");
        }
    }
}
