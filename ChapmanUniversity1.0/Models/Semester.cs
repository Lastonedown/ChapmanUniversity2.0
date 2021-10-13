using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ChapmanUniversity1._0.Models
{
    public enum Seasons
    {
        Spring
        , Summer
        , Fall
        , Winter
    }
    public class Semester
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Course Season")]
        [Required]
        public string CourseSeason { get; set; }

        public Course Course { get; set; }
    }
}
