using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourses.Models
{
    public class Group:IEntityWithId
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Name { get; set; }
        public virtual int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
