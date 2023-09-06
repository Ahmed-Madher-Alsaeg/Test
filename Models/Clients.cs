using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaBeratung.Models
{
    [Table("clients")]
    [Index("Id", Name = "client_index_on_id", IsUnique = true)]
    public class Clients
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Please provide your Name")]
        public string Name { get; set; } = null!;

        [Column("email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please provide your Email")]
        public string Email { get; set; } = null!;

        [Column("message")]
        [Required(ErrorMessage = "Please provide a message")]
        public string Message { get; set; } = null!;

        [Column("appointment")]
        [Required(ErrorMessage = "Please choose an appointment date")]
        public DateTime Appointment { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please provide your PhoneNumber")]
        public string PhoneNumber { get; set; } = null!;

        [Column("agree")]
        [Required(ErrorMessage = "Please check true if you want to continue and get our services")]
        public Boolean Agree { get; set; }

        [Column("sent_time")]
        
        public DateTime Sent_Time { get; set; }

    }
}
