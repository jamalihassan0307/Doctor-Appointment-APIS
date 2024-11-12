using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class PatientModel
    {
        [Key]
        [StringLength(255)]
        public required string Id { get; set; }

        [StringLength(225)]
        public required string Fullname { get; set; }

        [StringLength(100)]
        public required string PhoneNumber { get; set; }

        [StringLength(100)]
        public required string Password { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public required string Email { get; set; }

        public required IFormFile Image { get; set; }
    }

}
