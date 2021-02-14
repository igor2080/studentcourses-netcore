using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCourses.Common.Models
{
    public class GroupModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseModel Course { get; set; }
        public StudentModel[] Students { get; set; }

    }
}
