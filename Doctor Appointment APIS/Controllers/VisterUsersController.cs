using Doctor_Appointment_APIS.Models;
using Microsoft.AspNetCore.Mvc;
using Patient_Appointment_APIS.Models;
using System.Data;

namespace Doctor_Appointment_APIS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VisterUsersController : ControllerBase
    {
        [HttpGet]
        public DataTable GetAllVisterUser()
        {

            var user = SQLDatabase.GetDataTable($"select * from visteruser");

            return user;
        }
          [HttpGet("{id}")]
        public DataTable GetAllVisterUserbyId(string id)
        {

            var user = SQLDatabase.GetDataTable($"SELECT * FROM VisterUser where doctorid='${id}' OR patientid='${id}'");

            return user;
        }
        [HttpPost]
        public ActionResult CreateVisterUser(VisterUser model)
        {
            try
            {
                string query = $"INSERT INTO `visteruser`(`id`, `patientid`, `doctorid`) VALUES ('{model.Id}','{model.PatientId}','{model.DoctorId}')";
                    
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

        [HttpDelete("{name}")]
        public ActionResult DeleteVisterUserTable(string name)
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM {name} ");


                if (user > 0)
                    return Ok("DELETE Sucessfully");
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
