using DotorsGrpc;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PatientTickets.Application.Abstractions.ExternalProviders;
using PatientTickets.Application.DTOs.ExternalProviders;
using PatientTickets.ExternalProviders.Exceptions;


namespace PatientTickets.ExternalProviders
{
    internal class ManageUsersGrpcProvider : IManageUsersProviders

    {
        private readonly IConfiguration _configuration;



        public ManageUsersGrpcProvider(

            IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public async Task<GetDoctorDto> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using var channel = GrpcChannel.ForAddress(_configuration["ManageUsersServiceGrpcUrl"]!);
            var client = new DoctorsService.DoctorsServiceClient(channel);
            try
            {

                var doctorReply = client.GetDoctor(new GetDoctorRequest()
                {
                    Id = id.ToString(),
                }, cancellationToken: cancellationToken);
                var dto = new GetDoctorDto();
                await foreach (var reply in doctorReply.ResponseStream.ReadAllAsync(cancellationToken))
                {
                    dto.FirstName = reply.FirstName;
                    dto.LastName = reply.LastName;
                    dto.Patronymic = reply.Patronymic;
                    dto.CabinetNumber = reply.CabinetNumber;
                    dto.Speciality = reply.Speciality;
                }

                return dto;
            }
            catch (Exception)
            {
                throw new ExternalServiceNotAvailable("ManageUsers", $"{_configuration["ManageUsersServiceGrpcUrl"]}/{nameof(client.GetDoctor)}"!);

            }

        }
    }
}
