using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_APIS.Models
{
    public class MessageModel
    {

        [StringLength(255)]
        public string ToId { get; set; }
        public string Msg { get; set; }

        [StringLength(255)]
        public string Readn { get; set; }

        [StringLength(255)]
        public string FromId { get; set; }

        [StringLength(255)]
        public string Sent { get; set; }
    }
}
