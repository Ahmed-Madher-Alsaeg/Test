using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace g13lec6.Models
{
    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int Id { get; set; }


        [StringLength(255)]
        public string Name { get; set; } = null!;


        public int? DeptId { get; set; }

        [ForeignKey("DeptId")]
        [InverseProperty("Employees")]
        public virtual Dept? Dept { get; set; }
    }
}
