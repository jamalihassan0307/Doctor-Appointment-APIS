using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class AppointmentModel
    {
        [Key]
        [StringLength(255)]
        public required string Id { get; set; }

        [Required]
        [StringLength(255)]
        public required string PatientId { get; set; }

        [Required]
        [StringLength(255)]
        public required string DoctorId { get; set; }

        [Required]
        [StringLength(255)]
        public required string SlotsId { get; set; }

        [Required]
        [StringLength(255)]
        public required string Time { get; set; }

        [Required]
        public int CreatedTime { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        [StringLength(255)]
        public required string Bio { get; set; }

        public float? Rating { get; set; }
    }
}
