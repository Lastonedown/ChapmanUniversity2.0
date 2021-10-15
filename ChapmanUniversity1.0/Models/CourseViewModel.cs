using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChapmanUniversity1._0.Models
{
    public class CourseViewModel
    {
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

        [Display(Name = "Course Season")]
        [Required]
        public string CourseSeason { get; set; }
    }
}
