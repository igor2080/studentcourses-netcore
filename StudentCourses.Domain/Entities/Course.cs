using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourses.Domain.Entities
{
    public class Course : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Description { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
