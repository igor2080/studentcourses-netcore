using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentCourses.Common.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Display(Name = "Course name")]
        public string Name { get; set; }
        [Display(Name = "Course description")]
        public string Description { get; set; }

        public GroupModel[] Groups { get; set; }
    }
}
