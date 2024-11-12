using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class UpdateDoctor
    {
        [Key]
        [StringLength(255)]
        public required string Id { get; set; }

        [Required]
        [StringLength(255)]
        public required string Fullname { get; set; }

        [Required]
        [StringLength(50)]
        public required string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(255)]
        public required string Password { get; set; }

        public required IFormFile Image { get; set; }

        [Required]
        [StringLength(225)]
        public required string Bio { get; set; }

        [Required]
        [StringLength(255)]
        public required string Specialty { get; set; }

        [Required]
        [StringLength(50)]
        public required string StartTime { get; set; }

        [Required]
        [StringLength(50)]
        public required string EndTime { get; set; }

        [Required]
        [StringLength(225)]
        public required string About { get; set; }

        [Required]
        [StringLength(225)]
        public required string Address { get; set; }

        [Required]
        public int MaxAppointmentDuration { get; set; }


        [Required]
        public float Fee { get; set; }
    }
}
