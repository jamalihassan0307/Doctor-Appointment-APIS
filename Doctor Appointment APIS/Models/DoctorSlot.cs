
using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class DoctorSlot
    {
        [Key]
        [StringLength(255)]
        public required string Id { get; set; }

        public int? Indexn { get; set; }

        [StringLength(255)]
        public required string PatientId { get; set; }


        [Required]
        [StringLength(255)]
        public required string DoctorName { get; set; }

        [Required]
        [StringLength(255)]
        public required string DoctorId { get; set; }

        [Required]
        [StringLength(50)]
        public required string StartTime { get; set; }

        [Required]
        [StringLength(50)]
        public required string EndTime { get; set; }

        [StringLength(255)]
        public required string PatientName { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [StringLength(50)]
        public required string Date { get; set; }
    }

}
