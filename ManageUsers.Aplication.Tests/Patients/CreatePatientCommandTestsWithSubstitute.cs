using AutoMapper;
using FluentAssertions;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Caches.Patients;
using ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;
using ManageUsers.Domain;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Shared;
using MassTransit;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;

namespace ManageUsers.Application.Tests.Patients
{
    public class CreatePatientCommandTestsWithSubstitute
    {

        private static readonly CreatePatientCommand Command =
            new("address",
                DateTime.Now,
                "firstName",
                "lastName",
                "patientLogin",
                "patientPassword",
                "patientPatronymic",
                "+375298132256",
                "BM2222222",
                "avatar");

        private readonly CreatePatientCommandHandler _handler;
        private readonly IBaseWriteRepository<Patient> _patientsRepositoryMock;
        private readonly IBaseReadRepository<ApplicationUserRole> _userRoleRepositoryMock;
        private readonly IBaseWriteRepository<ApplicationUser> _usersRepositoryMock;
        private readonly IMapper _mapper;
        private readonly PatientsListMemoryCache _listCache;
        private readonly NullLogger<CreatePatientCommandHandler> _logger = new();
        private readonly PatientsCountMemoryCache _countCache;
        private readonly PatientMemoryCache _patientMemoryCache;
        private readonly IPublishEndpoint _publish;
        public CreatePatientCommandTestsWithSubstitute()
        {
            _patientsRepositoryMock = Substitute.For<IBaseWriteRepository<Patient>>();
            _userRoleRepositoryMock = Substitute.For<IBaseReadRepository<ApplicationUserRole>>();
            _usersRepositoryMock = Substitute.For<IBaseWriteRepository<ApplicationUser>>();
            ;
            _mapper = Substitute.For<IMapper>();
            _listCache = Substitute.For<PatientsListMemoryCache>();
            _countCache = Substitute.For<PatientsCountMemoryCache>();
            _patientMemoryCache = Substitute.For<PatientMemoryCache>();
            _publish = Substitute.For<IPublishEndpoint>();
            _handler = new CreatePatientCommandHandler(
                _patientsRepositoryMock,
                _usersRepositoryMock,
                _mapper,
                _listCache,
                _logger,
                _countCache,
                _patientMemoryCache,
                _userRoleRepositoryMock,
                _publish);
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenFirstNameIsInvalid()
        {
            //Arrange
            var invalidCommand = Command with { FirstName = "" };
            //Act
            Result result = await _handler.Handle(invalidCommand, default);
            //Assert
            result.Error.Should().Be(DomainErrors.FirstNameDomainErrors.Empty);
        }
        [Fact]
        public async Task Handle_Should_ReturnError_WhenPhoneNumberIsInvalid()
        {
            //Arrange
            var invalidCommand = Command with { PhoneNumber = "" };
            //Act
            Result result = await _handler.Handle(invalidCommand, default);
            //Assert
            result.Error.Should().Be(DomainErrors.PhoneNumberDomainErrors.Empty);
        }
        [Fact]
        public async Task Handle_Should_ReturnError_WhenPhoneNumberIsInvalidFormat()
        {
            //Arrange
            var invalidCommand = Command with { PhoneNumber = "invalid" };
            //Act
            Result result = await _handler.Handle(invalidCommand, default);
            //Assert
            result.Error.Should().Be(DomainErrors.PhoneNumberDomainErrors.InvalidFormat);
        }


    }

}
