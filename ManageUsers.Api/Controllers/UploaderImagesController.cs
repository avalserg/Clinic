using System.Drawing;
using System.Drawing.Drawing2D;
using ManageUsers.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using ManageUsers.Api.Abstractions;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;
using ManageUsers.Application.Handlers.UploadImages.Commands.UploadPatientAvatar;
using ManageUsers.Domain;
using MediatR;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Threading;
using ManageUsers.Application.Handlers.UploadImages.Commands.UploadDoctorImage;


namespace ManageUsers.Api.Controllers
{
    
    [Route("[controller]")]
    public class UploaderImagesController : ApiController
    {
        
       

        public UploaderImagesController(
            ISender sender
           
            ):base(sender)
        {
            
            
        }   
        /// <summary>
        /// Upload image for doctor profile
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadDoctorImageFile/{id}")]
        
        public async Task<IActionResult> UploadDoctorImageFile( [FromForm] FileModel file, string id, CancellationToken cancellationToken)
        {
            var command = new UploadDoctorImageCommand(id, file.FileName, file.File);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.IsSuccess);

           
        }
        [HttpPost]
        [Route("UploadPatientAvatarFile/{id}")]
        
        public async Task<IActionResult> UploadPatientAvatarFile( [FromForm] FileModel file, string id, CancellationToken cancellationToken)
        {
            var command = new UploadPatientAvatarCommand(id, file.FileName, file.File);
            var result =await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.IsSuccess);
        }

       
    }
}
