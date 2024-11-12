using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class VisterUser
    {
        [Key]
        [StringLength(255)]
        public required string Id { get; set; }

        [StringLength(255)]
        public required string PatientId { get; set; }

        [StringLength(255)]
        public required string DoctorId { get; set; }
    }

}
