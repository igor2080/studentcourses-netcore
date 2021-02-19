using System;
using System.ComponentModel.DataAnnotations;

namespace StudentCourses.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}