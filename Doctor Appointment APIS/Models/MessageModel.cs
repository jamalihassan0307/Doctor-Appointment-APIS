using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class MessageModel
    {

        [StringLength(255)]
        public required string ToId { get; set; }
        public required string Msg { get; set; }

        [StringLength(255)]
        public required string Readn { get; set; }

        [StringLength(255)]
        public required string FromId { get; set; }

        [StringLength(255)]
        public required string Sent { get; set; }
    }
}
