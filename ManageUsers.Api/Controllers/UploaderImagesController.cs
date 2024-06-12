using ManageUsers.Api.Abstractions;
using ManageUsers.Api.Model;
using ManageUsers.Application.Handlers.UploadImages.Commands.UploadDoctorImage;
using ManageUsers.Application.Handlers.UploadImages.Commands.UploadPatientAvatar;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace ManageUsers.Api.Controllers
{

    [Route("[controller]")]
    public class UploaderImagesController : ApiController
    {
        public UploaderImagesController(ISender sender) : base(sender) { }

        /// <summary>
        /// Upload image for doctor profile
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadDoctorImageFile/{id}")]

        public async Task<IActionResult> UploadDoctorImageFile([FromForm] FileModel file, string id, CancellationToken cancellationToken)
        {
            var command = new UploadDoctorImageCommand(id, file.FileName, file.File);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.IsSuccess);


        }

        /// <summary>
        /// Upload avatar for patient profile
        /// </summary>
        /// <param name="file"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadPatientAvatarFile/{id}")]

        public async Task<IActionResult> UploadPatientAvatarFile([FromForm] FileModel file, string id, CancellationToken cancellationToken)
        {
            var command = new UploadPatientAvatarCommand(id, file.FileName, file.File);
            var result = await Sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.IsSuccess);
        }


    }
}
