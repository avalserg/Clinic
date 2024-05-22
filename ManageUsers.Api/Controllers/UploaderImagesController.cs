using ManageUsers.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ManageUsers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploaderImagesController : ControllerBase
    {
        /// <summary>
        /// Upload image for doctor profile
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadDoctorImageFile")]
        
        public async Task<IActionResult> UploadDoctorImageFile( [FromForm] FileModel file)
        {
            Regex rgRegex = new Regex("(image/(gif|jpe?g|tiff?|png|svg|webp|bmp))");
           
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
                await using Stream stream = new FileStream(path, FileMode.Create);
                await file.file.CopyToAsync(stream);
                // TODO add doctor image path to table
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
