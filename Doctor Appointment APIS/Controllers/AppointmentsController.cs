using Doctor_Appointment_APIS.Models;
using Patient_Appointment_APIS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Doctor_Appointment_APIS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentsController :ControllerBase
    {
        [HttpGet]
        public DataTable GetAllAppointments()
        {
            
            var user = SQLDatabase.GetDataTable("select * from AppointmentModel");

            return user;
        }
        [HttpPost]
        public ActionResult CreateAppointment( AppointmentModel model)
        {
            try
            {
                string query = $"INSERT INTO AppointmentModel (id, patientid, doctorid, slotsid, time, createdtime, status, bio, rating) " +
                               $"VALUES ('{model.Id}', '{model.PatientId}', '{model.DoctorId}', '{model.SlotsId}', '{model.Time}', {model.CreatedTime}, {model.Status}, '{model.Bio}', {model.Rating})";

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


        


        [HttpGet("{id}")]
        public DataTable GetAllAppointmentsByDoctorId(string id)
        {
            
            var user = SQLDatabase.GetDataTable($"select * from AppointmentModel where doctorid='{id}'");

            return user;
        } 
        [HttpGet("{id}")]
        public DataTable GetAllAppointmentsByPatientId(string id)
        {
            
            var user = SQLDatabase.GetDataTable($"select * from AppointmentModel where patientid='{id}'");

            return user;
        } 

        [HttpGet("{doctorId}/{queryType}")]
        public DataTable SelectJoinTypeDoctorId(string queryType,string doctorId)
        {
            string query = "";
            switch (queryType)
            {
                case "WHERE":
                    query = $"SELECT* FROM AppointmentModel WHERE status = 2 AND doctorid = '{doctorId}'";
                break;

                case "LIMIT":
                   
                        query = $"SELECT* FROM AppointmentModel  WHERE doctorid = '{doctorId}' ORDER BY createdtime DESC LIMIT 10;";
     
                break;

                case "ORDER BY":
                    query = $"SELECT* FROM AppointmentModel WHERE doctorid = '{doctorId}' ORDER BY rating DESC";
                break;

                case "GROUP BY":
                    query = $"SELECT patientname, COUNT(*) AS appointment_count  FROM AppointmentModel WHERE doctorid = '{doctorId}'  GROUP BY patientname";
                break;

                case "HAVING":
                    query = $"SELECT patientname, COUNT(rating) AS appointment_count FROM AppointmentModel WHERE doctorid = '{doctorId}' GROUP BY patientname HAVING AVG(rating) > 3";
                break;
                default:
                    query = $"SELECT* FROM AppointmentModel WHERE doctorid = '{doctorId}'";
                    break;
            }
            var user = SQLDatabase.GetDataTable(query);

            return user;
        } 
        [HttpGet("{doctorId}/{queryType}")]
        public DataTable SelectJoinTypePatientId(string queryType,string patientId)
        {
            string query = "";
            switch (queryType)
            {
                case "WHERE":
                    query = $"SELECT* FROM AppointmentModel WHERE status = 2 AND patientId = '{patientId}'";
                break;

                case "LIMIT":
                   
                        query = $"SELECT* FROM AppointmentModel  WHERE patientId = '{patientId}' ORDER BY createdtime DESC LIMIT 10;";
     
                break;

                case "ORDER BY":
                    query = $"SELECT* FROM AppointmentModel WHERE patientId = '{patientId}' ORDER BY rating DESC";
                break;

                case "GROUP BY":
                    query = $"SELECT patientname, COUNT(*) AS appointment_count  FROM AppointmentModel WHERE patientId = '{patientId}'  GROUP BY patientname";
                break;

                case "HAVING":
                    query = $"SELECT patientname, COUNT(rating) AS appointment_count FROM AppointmentModel WHERE patientId = '{patientId}' GROUP BY patientname HAVING AVG(rating) > 3";
                break;
                default:
                    query = $"SELECT* FROM AppointmentModel WHERE patientId = '{patientId}'";
                    break;
            }
            var user = SQLDatabase.GetDataTable(query);

            return user;
        }
        [HttpPut]
        public ActionResult UpdateAppointmentStatus(string id, int status)
        {
            try
            {
                string query = $"UPDATE AppointmentModel SET status = {status} WHERE id = '{id}'";
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
        [HttpPut("{id}/{rating}")]
        public ActionResult UpdateAppointmentRating(string id, float rating)
        {
            try
            {
                string query = $"UPDATE AppointmentModel SET rating = {rating} WHERE id = '{id}'";
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
        [HttpDelete("{id}")]
        public ActionResult DeleteAppointment(string id)
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM AppointmentModel where id='{id}'");


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
        public ActionResult DeleteAllAppointment()
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM AppointmentModel ");


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
