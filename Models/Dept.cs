using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace g13lec6.Models
{
    [Table("Dept")]
    public partial class Dept
    {
        public Dept()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }


        [StringLength(255)]
        public string Name { get; set; } = null!;


        [StringLength(255)]
        public string Location { get; set; } = null!;



        [InverseProperty("Dept")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
