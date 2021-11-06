using Microsoft.EntityFrameworkCore.Migrations;

namespace ChapmanUniversity1._0.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_StudentEnrollments_StudentSemesterEnrollmentId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentEnrollments_StudentSemesterEnrollmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentSemesterEnrollmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_StudentSemesterEnrollmentId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "StudentSemesterEnrollmentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentSemesterEnrollmentId",
                table: "Semesters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentSemesterEnrollmentId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentSemesterEnrollmentId",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentSemesterEnrollmentId",
                table: "Students",
                column: "StudentSemesterEnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_StudentSemesterEnrollmentId",
                table: "Semesters",
                column: "StudentSemesterEnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_StudentEnrollments_StudentSemesterEnrollmentId",
                table: "Semesters",
                column: "StudentSemesterEnrollmentId",
                principalTable: "StudentEnrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentEnrollments_StudentSemesterEnrollmentId",
                table: "Students",
                column: "StudentSemesterEnrollmentId",
                principalTable: "StudentEnrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
