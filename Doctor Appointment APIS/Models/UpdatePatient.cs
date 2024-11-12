
using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    namespace Doctor_Appointment_App_ApIs.Models
    {
        public class UpdatePatient
        {
            [Key]
            [StringLength(255)]
            public required string Id { get; set; }

            [Required]
            [StringLength(255)]
            public required string Fullname { get; set; }

            [Required]
            [StringLength(50)]
            [EmailAddress]
            public required string Email { get; set; }

            [Required]
            [StringLength(255)]
            public required string Password { get; set; }

            public IFormFile? Image { get; set; }
        }
    }
}
