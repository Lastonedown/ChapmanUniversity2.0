using System.Linq;
using ChapmanUniversity1._0.DAL;

namespace ChapmanUniversity1._0.BusinessLogic
{
    public static class SemesterBusinessLogic
    {
        private static readonly UnitOfWork UnitOfWork = new();

        public static int FindSemesterId(int courseId, string courseSeason)
        {
            var semesters = UnitOfWork.Semesters.Get().ToList();
            foreach (var semester in semesters)
            {

                if (semester.CourseId == courseId && semester.CourseSeason == courseSeason)
                {
                    return semester.Id;
                }
            }

            return 0;
        }
    }
}
