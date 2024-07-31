
using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class DoctorSlot
    {
        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        public int? Indexn { get; set; }

        [StringLength(255)]
        public string PatientId { get; set; }


        [Required]
        [StringLength(255)]
        public string DoctorName { get; set; }

        [Required]
        [StringLength(255)]
        public string DoctorId { get; set; }

        [Required]
        [StringLength(50)]
        public string StartTime { get; set; }

        [Required]
        [StringLength(50)]
        public string EndTime { get; set; }

        [StringLength(255)]
        public string PatientName { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [StringLength(50)]
        public string Date { get; set; }
    }

}
