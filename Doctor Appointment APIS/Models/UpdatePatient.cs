
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
            public string Id { get; set; }

            [Required]
            [StringLength(255)]
            public string Fullname { get; set; }

            [Required]
            [StringLength(50)]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(255)]
            public string Password { get; set; }

            public IFormFile? Image { get; set; }
        }
    }
}
