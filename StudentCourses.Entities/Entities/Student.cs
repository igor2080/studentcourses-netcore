using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourses.Domain.Entities
{
    public class Student : BaseEntity
    {
        [Column(TypeName = "nvarchar(25)")]
        [Required]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(25)")]
        [Required]
        public string LastName { get; set; }
        [Required]
        public int GroupId { get; set; }
        public Group Group { get; set; }

    }
}
