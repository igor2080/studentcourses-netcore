using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentCourses.Common.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        [Display(Name ="Group name")]
        public string Name { get; set; }
        public CourseModel Course { get; set; }
        public StudentModel[] Students { get; set; }

    }
}
