using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class UpdateDoctor
    {
        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        [StringLength(225)]
        public string Bio { get; set; }

        [Required]
        [StringLength(255)]
        public string Specialty { get; set; }

        [Required]
        [StringLength(50)]
        public string StartTime { get; set; }

        [Required]
        [StringLength(50)]
        public string EndTime { get; set; }

        [Required]
        [StringLength(225)]
        public string About { get; set; }

        [Required]
        [StringLength(225)]
        public string Address { get; set; }

        [Required]
        public int MaxAppointmentDuration { get; set; }


        [Required]
        public float Fee { get; set; }
    }
}
