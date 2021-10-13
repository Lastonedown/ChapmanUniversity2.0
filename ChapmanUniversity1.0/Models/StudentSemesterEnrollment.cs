using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ChapmanUniversity1._0.Models
{
    public class StudentSemesterEnrollment
    {
        [Required]
        public int Id { get; set; }

        public Semester Semester { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}