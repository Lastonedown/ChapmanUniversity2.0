using Microsoft.EntityFrameworkCore.Migrations;

namespace ChapmanUniversity1._0.Migrations
{
    public partial class init10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollments_Semesters_FK_Course",
                table: "StudentEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_StudentEnrollments_FK_Course",
                table: "StudentEnrollments");

            migrationBuilder.DropColumn(
                name: "FK_Course",
                table: "StudentEnrollments");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_SemesterId",
                table: "StudentEnrollments",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollments_Semesters_SemesterId",
                table: "StudentEnrollments",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollments_Semesters_SemesterId",
                table: "StudentEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_StudentEnrollments_SemesterId",
                table: "StudentEnrollments");

            migrationBuilder.AddColumn<int>(
                name: "FK_Course",
                table: "StudentEnrollments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_FK_Course",
                table: "StudentEnrollments",
                column: "FK_Course");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollments_Semesters_FK_Course",
                table: "StudentEnrollments",
                column: "FK_Course",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
