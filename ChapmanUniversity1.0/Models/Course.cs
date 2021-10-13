using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace ChapmanUniversity1._0.Models
{
    public class Course
    {

        [Required]
        public int Id { get; set; }

        [Display(Name = "Course Identification Number")]
        [Required]
        public int CourseNumber { get; set; }
        
        [Display(Name = "Course Name")]
        [Required]
        public string CourseName { get; set; }

        [Required]
        public int Credits { get; set; }

        [Display(Name = "Course Description")]
        [Required]
        public string CourseDescription { get; set; }


        public ICollection<StudentSemesterEnrollment> Enrollments { get; set; }

    }
}
