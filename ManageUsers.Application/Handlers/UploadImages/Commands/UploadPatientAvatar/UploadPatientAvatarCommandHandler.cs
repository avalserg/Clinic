using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Caches.Patients;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Text.RegularExpressions;
namespace ManageUsers.Application.Handlers.UploadImages.Commands.UploadPatientAvatar;

public class UploadPatientAvatarCommandHandler : ICommandHandler<UploadPatientAvatarCommand>
{
    private readonly IBaseWriteRepository<Domain.Patient> _patients;
    private readonly PatientsListMemoryCache _listCache;
    private readonly ILogger<UploadPatientAvatarCommandHandler> _logger;
    private readonly PatientMemoryCache _patientMemoryCache;
    private long _fileSizeLimit;
    private readonly IConfiguration _configuration;
    public UploadPatientAvatarCommandHandler(

        IBaseWriteRepository<Domain.Patient> patients,

        PatientsListMemoryCache listCache,
        ILogger<UploadPatientAvatarCommandHandler> logger,
        PatientMemoryCache patientMemoryCache, IConfiguration configuration)
    {

        _patients = patients;
        _listCache = listCache;
        _logger = logger;
        _patientMemoryCache = patientMemoryCache;
        _configuration = configuration;
    }

    public async Task<Result> Handle(UploadPatientAvatarCommand request, CancellationToken cancellationToken)
    {
        Regex rgRegex = new Regex("(image/(gif|jpe?g|tiff?|png|svg|webp|bmp))");

        if (rgRegex.Matches(request.File.ContentType).Count == 0)
        {
            return Result.Failure(new Error("", "File not an Image"));
        }
        // TODO TryParse
        bool success = long.TryParse(_configuration["FileSizeLimit"]!, out _fileSizeLimit);
        if (!success)
        {
            return Result.Failure(new Error("", "FileSizeLimit cannot be converted to long"));
        }
        if (request.File.Length > _fileSizeLimit)
        {
            return Result.Failure(new Error("", "File size must be less than 2MB"));
        }
        var user = await _patients.AsAsyncRead()
            .FirstOrDefaultAsync(u => u.ApplicationUserId.ToString() == request.Id, cancellationToken);
        if (user == null)
        {
            return Result.Failure<Domain.Patient>(DomainErrors.PatientDomainErrors.NotFoundStringId(request.Id));
        }
        try
        {
            // replace image/extension to extension
            request.FileName = request.Id + "." + request.File.ContentType[6..];
            var path = Path.Combine(@"G:\ClinicApp\Clinic\ManageUsers.Api\Avatars", request.FileName);

            using (var memoryStream = new MemoryStream())
            {

                await request.File.CopyToAsync(memoryStream, cancellationToken);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var img = await Image.LoadAsync(memoryStream, cancellationToken))
                {
                    int width = 100;
                    int height = 100;
                    img.Mutate(x =>
                    {
                        x.Resize(width, height);

                    });

                    await img.SaveAsync(path, cancellationToken);
                }
            }

            user.UpdateAvatar(request.FileName);
            await _patients.UpdateAsync(user, cancellationToken);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        _listCache.Clear();
        _patientMemoryCache.Clear();
        _logger.LogInformation($"Avatar user {request.Id} created.");

        return Result.Success();
    }
}