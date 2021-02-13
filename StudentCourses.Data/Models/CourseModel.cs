using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCourses.Common.Models
{
    public class CourseModel:IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public GroupModel[] Groups { get; set; }
    }
}
