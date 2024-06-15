using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PatientsGrpc;
using Reviews.Application.Abstractions.ExternalProviders;
using Reviews.Application.DTOs.ExternalProviders;

namespace Reviews.ExternalProviders
{
    internal class ManageUsersGrpcProvider : IManageUsersProviders

    {
        private readonly IConfiguration _configuration;



        public ManageUsersGrpcProvider(

            IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public async Task<GetPatientDto> GetPatientByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using var channel = GrpcChannel.ForAddress(_configuration["ManageUsersServiceGrpcUrl"]!);
            var client = new PatientsService.PatientsServiceClient(channel);
            try
            {

                var patientReply = client.GetPatient(new GetPatientRequest()
                {
                    Id = id.ToString(),
                }, cancellationToken: cancellationToken);

                var dto = new GetPatientDto();
                await foreach (var reply in patientReply.ResponseStream.ReadAllAsync(cancellationToken))
                {

                    dto.FirstName = reply.FirstName;
                    dto.LastName = reply.LastName;
                    dto.Patronymic = reply.Patronymic;
                    dto.Address = reply.Address;
                    dto.PhoneNumber = reply.PhoneNumber;
                    dto.DateBirthday = DateTime.Parse(reply.DateBirthday);
                }

                return dto;
            }
            catch (Exception)
            {
                return null;

            }

        }
    }
}
