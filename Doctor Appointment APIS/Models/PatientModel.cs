using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class PatientModel
    {
        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [StringLength(225)]
        public string Fullname { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public IFormFile Image { get; set; }
    }

}
