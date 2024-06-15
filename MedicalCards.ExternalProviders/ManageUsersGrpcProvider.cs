using DotorsGrpc;
using Grpc.Core;
using Grpc.Net.Client;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.DTOs.ExternalProviders;
using MedicalCards.ExternalProviders.Exceptions;
using Microsoft.Extensions.Configuration;
using PatientsGrpc;

namespace MedicalCards.ExternalProviders
{
    internal class ManageUsersGrpcProvider : IManageUsersProviders

    {
        private readonly IConfiguration _configuration;



        public ManageUsersGrpcProvider(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<GetPatientDto?> GetPatientByIdAsync(Guid id, CancellationToken cancellationToken)
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
                throw new ExternalServiceNotAvailable("ManageUsers", $"{_configuration["ManageUsersServiceGrpcUrl"]}/{nameof(client.GetPatient)}"!);

            }

        }

        public async Task<GetDoctorDto?> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
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
                    dto.DoctorFirstName = reply.FirstName;
                    dto.DoctorLastName = reply.LastName;
                    dto.DoctorPatronymic = reply.Patronymic;
                    dto.Speciality = reply.Speciality;
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
