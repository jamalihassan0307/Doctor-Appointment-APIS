using Doctor_Appointment_APIS.Models;
using Microsoft.AspNetCore.Mvc;
using Patient_Appointment_APIS.Models;
using System.Data;

namespace Doctor_Appointment_APIS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public DataTable GetAllMessage()
        {

            var user = SQLDatabase.GetDataTable($"select * from Message");

            return user;
        }
        [HttpPost]
        public ActionResult InsertMessage( MessageModel model)
        {
            try
            {
                string query = $"INSERT INTO Message (toId, msg, readn, fromId, sent) " +
                               $"VALUES ('{model.ToId}', '{model.Msg}', '{model.Readn}', '{model.FromId}', '{model.Sent}')";

                int result = SQLDatabase.ExecNonQuery(query);

                if (result > 0)
                {
                    return Ok("Successfully Inserted");
                }
                else
                {
                    return BadRequest("Insertion Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Database Error: {ex.Message}");
            }
        }

        [HttpPut("{sent}")]
        public ActionResult UpdateMessageReadStatus( string sent)
        {
            try
            {
                string query = $"UPDATE Message SET readn = '{DateTime.Now.ToFileTime()}' WHERE sent = '{sent}'";
                int result = SQLDatabase.ExecNonQuery(query);

                if (result > 0)
                {
                    return Ok("Successfully Updated");
                }
                else
                {
                    return BadRequest("Update Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Database Error: {ex.Message}");
            }
        }
        [HttpDelete("{sent}")]
        public ActionResult DeleteMessageById(string sent)
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM Message WHERE sent='{sent}'; ");


                if (user > 0)
                    return Ok("DELETE User Sucessfully");
                else
                    return BadRequest("User not Found");
            }
            catch (Exception ex)
            {
                return BadRequest("Some Thing Want Wrong !");
            }
        }
      [HttpDelete]
        public ActionResult DeleteAllMessage()
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM Message ");


                if (user > 0)
                    return Ok("DELETE User Sucessfully");
                else
                    return BadRequest("User not Found");
            }
            catch (Exception ex)
            {
                return BadRequest("Some Thing Want Wrong !");
            }
        } 
    }
}