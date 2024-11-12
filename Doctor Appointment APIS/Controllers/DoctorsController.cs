using Doctor_Appointment_APIS.Models;
using Patient_Appointment_APIS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Doctor_Appointment_APIS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {



        private readonly IWebHostEnvironment _environment;

        public DoctorsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public DataTable GetAllDoctors()
        {
            var doctors = SQLDatabase.GetDataTable("SELECT * FROM DoctorModel");
            return doctors;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDoctor([FromForm] DoctorModel model)
        {
            try
            {
                // Ensure directory exists for image storage
                string basePath = Path.Combine(_environment.WebRootPath, "DoctorImage");
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                // Generate unique file name for the image
                string uniqueName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(model.Image.FileName);
                string newFilePath = Path.Combine(basePath, uniqueName + fileExtension);

                // Save the image to the directory
                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                string returnPath = "DoctorImage/" + uniqueName + fileExtension;

                // Construct SQL query string
                string query = $"INSERT INTO DoctorModel (id, fullname, phonenumber, email, password, image, bio, specialty, starttime, endtime, about, address, maxAppointmentDuration, totalrating, ratingperson, fee) " +
                              $"VALUES ('{model.Id}', '{model.Fullname}', '{model.PhoneNumber}', '{model.Email}', '{model.Password}', '{returnPath}', " +
                              $"'{model.Bio}', '{model.Specialty}', '{model.StartTime}', '{model.EndTime}', '{model.About}', '{model.Address}', " +
                              $"{model.MaxAppointmentDuration}, {model.TotalRating}, {model.RatingPerson}, {model.Fee})";
                // Execute the SQL query
                int result = SQLDatabase.ExecNonQuery(query);

                // Return result
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
        public ActionResult GetDoctorById(string id)
        {
            try
            {
                var doctor = SQLDatabase.GetDataTable($"SELECT * FROM DoctorModel WHERE id='{id}'");

                //if (doctor!=null)
                //{
                    return Ok(doctor);
               
                //    return BadRequest("User not Found");
                //}
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }
        }  

        [HttpGet("{id}")]
        public ActionResult GetDoctorImageFullNameById(string id)
        {
            try
            {
                var doctor = SQLDatabase.GetDataTable($"SELECT fullname,image FROM DoctorModel WHERE id='{id}'");

                //if (doctor != null)
                //{
                    return Ok(doctor);
                //}
                //else
                //{
                //    return BadRequest("User not Found");
                //}
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }
        }  
        [HttpGet("{email}/{pass}")]
        public ActionResult GetDoctorByEmailPass(string email, string pass)
        {
            try
            {
                var doctor = SQLDatabase.GetDataTable($"SELECT * FROM DoctorModel WHERE email='{email}'  and password='{pass}'");

                //if (doctor != null)
                //{
                    return Ok(doctor);
                //}
                //else
                //{
                //    return BadRequest("User not Found");
                //}
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDoctor( UpdateDoctor model)
        {
            try
            {
                var updateValues = new Dictionary<string, object>
        {
            { "fullname", model.Fullname },
            { "email", model.Email },
            { "password", model.Password },
            { "phonenumber", model.PhoneNumber },
            { "address", model.Address },
            { "specialty", model.Specialty },
            { "bio", model.Bio },
            { "fee", model.Fee },
            { "about", model.About },
            { "starttime", model.StartTime },
            { "endtime", model.EndTime },
            { "maxAppointmentDuration", model.MaxAppointmentDuration }
        };

               
                    try
                    {
                        string BasePath = Path.Combine(_environment.WebRootPath, "DoctorImage");
                        if (!Directory.Exists(BasePath))
                        {
                            Directory.CreateDirectory(BasePath);
                        }

                        string UniqueName = Guid.NewGuid().ToString();
                        string FileExtension = Path.GetExtension(model.Image.FileName);

                        string NewFilePath = Path.Combine(BasePath, UniqueName + FileExtension);

                        using (var fileStream = new FileStream(NewFilePath, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(fileStream);
                        }

                        string ReturnPath = "DoctorImage/" + UniqueName + FileExtension;
                        updateValues.Add("image", ReturnPath);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Image Exception");
                    }
                

                int result = SQLDatabase.ExecUpdate("DoctorModel", updateValues, model.Id);

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
        [HttpPut("{fullrating}/{total}/{id}")]
        public ActionResult UpdateDoctortotalRating(float fullrating, int total, string id)
        {
            try
            {
                string query = $"UPDATE DoctorModel  SET totalrating = '{fullrating}' ratingperson= '{total}' WHERE id = '{id}'";
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
        public ActionResult DeleteDoctorById(string id)
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM DoctorModel where id='{id}'");


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
        public ActionResult DeleteAllDoctor()
        {
            try
            {
                var user = SQLDatabase.ExecNonQuery($"DELETE FROM DoctorModel ");


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
    

