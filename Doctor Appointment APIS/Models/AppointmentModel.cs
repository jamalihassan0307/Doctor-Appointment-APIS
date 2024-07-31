using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class AppointmentModel
    {
        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string PatientId { get; set; }

        [Required]
        [StringLength(255)]
        public string DoctorId { get; set; }

        [Required]
        [StringLength(255)]
        public string SlotsId { get; set; }

        [Required]
        [StringLength(255)]
        public string Time { get; set; }

        [Required]
        public int CreatedTime { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        [StringLength(255)]
        public string Bio { get; set; }

        public float? Rating { get; set; }
    }
}
