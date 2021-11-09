using System.Linq;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories.SemesterRepositories
{
    public class SemesterRepository : Repository<Semester>, ISemesterRepository
    {
        public SemesterRepository(SchoolContext context) : base(context)
        {
        }

        public int GetSemesterId(int courseId, string courseSeason)
        {
            var semesters = Context.Semesters.ToList();

            foreach (var semester in semesters)
            {
                if (semester.CourseId == courseId && semester.CourseSeason == courseSeason)
                {
                    return semester.Id;
                }
            }

            return -0;
        }

        public bool ValidateSemester(int courseId, string courseSeason)
        {
            var semesterList = Context.Semesters.ToList();
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
