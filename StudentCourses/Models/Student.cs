using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourses.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }        
        [Column(TypeName = "nvarchar(25)")]
        [Required]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(25)")]
        [Required]
        public string LastName { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public Group Group { get; set; }

    }
}
