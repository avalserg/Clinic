using System.Text.RegularExpressions;
using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Caches.Doctors;
using ManageUsers.Application.Handlers.UploadImages.Commands.UploadPatientAvatar;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ManageUsers.Application.Handlers.UploadImages.Commands.UploadDoctorImage;

internal class UploadDoctorImageCommandHandler : ICommandHandler<UploadDoctorImageCommand>
{
    private readonly IBaseWriteRepository<Domain.Doctor> _doctors;
    private readonly DoctorsListMemoryCache _listCache;
    private readonly ILogger<UploadPatientAvatarCommandHandler> _logger;
    private readonly DoctorMemoryCache _doctorMemoryCache;
    private  long _fileSizeLimit;
    private readonly IConfiguration _configuration;
    public UploadDoctorImageCommandHandler(
        
        IBaseWriteRepository<Domain.Doctor> doctors,
        DoctorsListMemoryCache listCache,
        ILogger<UploadPatientAvatarCommandHandler> logger,
        DoctorMemoryCache doctorMemoryCache, IConfiguration configuration)
    {

        _doctors = doctors;
        _listCache = listCache;
        _logger = logger;
        _doctorMemoryCache = doctorMemoryCache;
        _configuration = configuration;
    }

    public async Task<Result> Handle(UploadDoctorImageCommand request, CancellationToken cancellationToken)
    {
        Regex rgRegex = new Regex("(image/(gif|jpe?g|tiff?|png|svg|webp|bmp))");

        if (rgRegex.Matches(request.File.ContentType).Count == 0)
        {
            return Result.Failure(new Error("","File not an Image"));
        }
        // TODO TryParse
        bool success = long.TryParse(_configuration["FileSizeLimit"]!,out _fileSizeLimit);
        if (!success)
        {
            return Result.Failure(new Error("", "FileSizeLimit cannot be converted to long"));
        }
        if (request.File.Length > _fileSizeLimit)
        {
            return Result.Failure(new Error("", "File size must be less than 2MB")); 
        }
        var user = await _doctors.AsAsyncRead()
            .FirstOrDefaultAsync(u => u.ApplicationUserId.ToString() == request.Id, cancellationToken);
        if (user == null)
        {
            return Result.Failure<Domain.Patient>(DomainErrors.PatientDomainErrors.NotFoundStringId(request.Id));
        }
        try
        {
            request.FileName= request.Id+"."+ request.File.ContentType[6..];
            string path = Path.Combine(@"G:\ClinicApp\Clinic\ManageUsers.Api\Images", request.FileName);
            
            using (var memoryStream = new MemoryStream())
            {

                await request.File.CopyToAsync(memoryStream, cancellationToken);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var img = await Image.LoadAsync(memoryStream, cancellationToken))
                {
                    int width = 600;
                    int height = 1000;
                    img.Mutate(x =>
                    {
                        x.Resize(width, height);
                        
                    });
                    
                    await img.SaveAsync(path,cancellationToken);
                }
            }

            user.UpdatePhoto(request.FileName);
            await _doctors.UpdateAsync(user, cancellationToken);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        _listCache.Clear();
        _doctorMemoryCache.Clear();
        _logger.LogInformation($"Avatar user {request.Id} created.");

        return Result.Success();
    }
}