using Patient_Appointment_APIS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Doctor_Appointment_APIS.Models;
using Doctor_Appointment_APIS.Models.Doctor_Appointment_App_ApIs.Models;

namespace Patient_Appointment_APIS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public PatientsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public DataTable GetAllPatients()
        {
            var Patients = SQLDatabase.GetDataTable("SELECT * FROM PatientModel");
            return Patients;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePatient([FromForm] PatientModel model)
        {
            try
            {
                string basePath = Path.Combine(_environment.WebRootPath, "PatientImage");
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                string uniqueName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(model.Image.FileName);
                string newFilePath = Path.Combine(basePath, uniqueName + fileExtension);
                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                string returnPath = "PatientImage/" + uniqueName + fileExtension;
                string query = $"INSERT INTO PatientModel (id, fullname, phonenumber, password, email, image) VALUES ('{model.Id}', '{model.Fullname}', '{model.PhoneNumber}', '{model.Password}', '{model.Email}', '{returnPath}')";
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
        public ActionResult GetPatientById(string id)
        {
            try
            {
                var Patient = SQLDatabase.GetDataTable($"SELECT * FROM PatientModel WHERE id='{id}'");

                if (Patient != null)
                {
                    return Ok(Patient);
                }
                else
                {
                    return BadRequest("User not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }
        }  [HttpGet]

        [HttpGet("{id}")]
        public ActionResult GetPatientImageFullNameById(string id)
        {
            try
            {
                var Patient = SQLDatabase.GetDataTable($"SELECT fullname,image FROM PatientModel WHERE id='{id}'");

                if (Patient != null)
                {
                    return Ok(Patient);
                }
                else
                {
                    return BadRequest("User not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }
        }  [HttpGet("{email}/{pass}")]
        public ActionResult GetPatientByEmailPass(string email,string pass)
        {
            try
            {
                var Patient = SQLDatabase.GetDataTable($"SELECT * FROM PatientModel WHERE email='{email}'  and password='{pass}'");

                if (Patient != null)
                {
                    return Ok(Patient);
                }
                else
                {
                    return BadRequest("User not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePatinet([FromForm] UpdatePatient model)
        {
            try
            {
                var updateValues = new Dictionary<string, object>
                {
                    { "fullname", model.Fullname },
                    { "email", model.Email },
                    { "password", model.Password }
                };

                if (model.Image != null)
                {
                    try
                    {
                        string basePath = Path.Combine(_environment.WebRootPath, "PatientImage");
                        if (!Directory.Exists(basePath))
                        {
                            Directory.CreateDirectory(basePath);
                        }

                        string uniqueName = Guid.NewGuid().ToString();
                        string fileExtension = Path.GetExtension(model.Image.FileName);
                        string newFilePath = Path.Combine(basePath, uniqueName + fileExtension);

                        using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(fileStream);
                        }

                        string returnPath = "PatientImage/" + uniqueName + fileExtension;
                        updateValues.Add("image", returnPath);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Image Exception: " + ex.Message);
                    }
                }

                int result = SQLDatabase.ExecUpdate("PatientModel", updateValues, model.Id);

                if (result > 0)
                {
                    return Ok("Successfully Updated");
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePatientById(string id)
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM PatientModel where id='{id}'");


                if (user > 0)
                    return Ok("DELETE User Sucessfully");
                else
                    return BadRequest("User not Found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Database Error: {ex.Message}");
            }
        }
        [HttpDelete]
        public ActionResult DeleteAllPatient()
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM PatientModel ");


                if (user > 0)
                    return Ok("DELETE User Sucessfully");
                else
                    return BadRequest("User not Found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Database Error: {ex.Message}");
            }
        }
    }
}
