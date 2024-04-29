using System.Text.RegularExpressions;
using ManageUsers.Api.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;
using static System.Net.Mime.MediaTypeNames;

namespace ManageUsers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploaderController : ControllerBase
    {
        [HttpPost]
        [Route("UploadDoctorImageFile")]
        
        public IActionResult UploadDoctorImageFile( [FromForm] FileModel file)
        {
            Regex rgRegex = new Regex("(image/(gif|jpe?g|tiff?|png|svg|webp|bmp))");
            //List<string> files =new() {"image/jpeg", "image/png", "image/svg", "image/jpeg" };
            //if (!files.Contains(file.file.ContentType))
            //{
            //    return BadRequest("File not image");
            //}
            if (rgRegex.Matches(file.file.ContentType).Count == 0)
            {
                return BadRequest("File not image");
            }

            
            try
            {
                string path = Path.Combine(@"G:\ClinicApp\Clinic\ManageUsers.Api\Images",file.FileName);
                if (System.IO.File.Exists(path))
                {
                    return BadRequest("Rename file file with this name already exist");
                }
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.file.CopyTo(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(file);
        }

       
    }
}
