using Microsoft.EntityFrameworkCore.Migrations;

namespace ChapmanUniversity1._0.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollments_Courses_CourseId",
                table: "StudentEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollments_Semesters_SemesterId",
                table: "StudentEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollments_Students_StudentId",
                table: "StudentEnrollments");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentEnrollments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "StudentEnrollments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "StudentEnrollments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollments_Courses_CourseId",
                table: "StudentEnrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollments_Semesters_SemesterId",
                table: "StudentEnrollments",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollments_Students_StudentId",
                table: "StudentEnrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollments_Courses_CourseId",
                table: "StudentEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollments_Semesters_SemesterId",
                table: "StudentEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollments_Students_StudentId",
                table: "StudentEnrollments");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentEnrollments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "StudentEnrollments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "StudentEnrollments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollments_Courses_CourseId",
                table: "StudentEnrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollments_Semesters_SemesterId",
                table: "StudentEnrollments",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollments_Students_StudentId",
                table: "StudentEnrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
