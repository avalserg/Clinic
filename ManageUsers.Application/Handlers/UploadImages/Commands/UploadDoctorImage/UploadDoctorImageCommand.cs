using ManageUsers.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace ManageUsers.Application.Handlers.UploadImages.Commands.UploadDoctorImage;

public class UploadDoctorImageCommand : ICommand
{
    public UploadDoctorImageCommand(string id, string fileName, IFormFile file)
    {
        Id = id;
        FileName = fileName;
        File = file;
    }

    public string Id { get; set; }
    public string FileName { get; set; }
    public IFormFile File { get; set; }
   
};
