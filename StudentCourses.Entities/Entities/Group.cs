using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourses.Domain.Entities
{
    public class Group : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Name { get; set; }
        public virtual int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
