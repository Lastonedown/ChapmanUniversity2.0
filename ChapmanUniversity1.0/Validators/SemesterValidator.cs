using System.Linq;
using ChapmanUniversity1._0.DAL;

namespace ChapmanUniversity1._0.Validators
{
    public static class SemesterValidator
    {
        private static readonly UnitOfWork UnitOfWork = new();


        public static bool Validate(int courseId, string courseSeason)
        {
            var semesterList = UnitOfWork.Semesters.Get().ToList();
            if (!semesterList.Any())
            {
                return false;
            }
            foreach (var semester in semesterList)
            {
                if (semester.CourseId == courseId && semester.CourseSeason == courseSeason)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
