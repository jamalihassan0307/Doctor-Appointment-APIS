using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class VisterUser
    {
        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [StringLength(255)]
        public string PatientId { get; set; }

        [StringLength(255)]
        public string DoctorId { get; set; }
    }

}
