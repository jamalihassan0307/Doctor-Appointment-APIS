using Doctor_Appointment_APIS.Models;
using Patient_Appointment_APIS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Doctor_Appointment_APIS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorSlotController : ControllerBase
    {
        [HttpGet]
        public DataTable GetAllDoctorSlot()
        {

            var user = SQLDatabase.GetDataTable($"select * from DoctorSlot");

            return user;
        } 
        [HttpGet("{id}")]
        public DataTable GetAllDoctorSlotbyId(string id)
        {

            var user = SQLDatabase.GetDataTable($"select * from DoctorSlot where DoctorId='{id}'");

            return user;
        }
        [HttpPost]
        public ActionResult CreateDoctorlot(DoctorSlot model)
        {
            try
            {
                string query = $"INSERT INTO DoctorSlot (id, indexn, patientid, doctorname, doctorid, startTime, endTime, patientName, isAvailable, date) " +
                      $"VALUES ('{model.Id}', {(model.Indexn.HasValue ? model.Indexn.Value.ToString() : "NULL")}, " +
                      $"'{model.PatientId}', '{model.DoctorName}', '{model.DoctorId}', '{model.StartTime}', '{model.EndTime}', " +
                      $"'{model.PatientName}', {(model.IsAvailable ? 1 : 0)}, '{model.Date}')";
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
        [HttpPut("{id}/.{status}")]
        public ActionResult UpdateDoctorSlotStatus(int status,string id)
        {
            try
            {
                string query = $"UPDATE DoctorSlot SET isAvailable = '{status}' WHERE id = '{id}'";
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
        [HttpDelete]
        public ActionResult DeleteDoctorSlotTable()
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM DoctorSlot ");


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
