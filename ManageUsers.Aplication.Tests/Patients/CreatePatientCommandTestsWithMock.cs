using AutoMapper;
using FluentAssertions;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Caches.Patients;
using ManageUsers.Application.Handlers.Patient.Commands.CreatePatient;
using ManageUsers.Domain;
using ManageUsers.Domain.Errors;
using MassTransit;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Linq.Expressions;

namespace ManageUsers.Application.Tests.Patients
{
    public class CreatePatientCommandTestsWithMock
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
        private static readonly ApplicationUser ApplicationUser = ApplicationUser.Create(
            new Guid(),
            "patientLogin",
            "patientPassword",
            1
        );


        private readonly Mock<IBaseWriteRepository<Patient>> _patientsRepositoryMock;
        private readonly Mock<IBaseReadRepository<ApplicationUserRole>> _userRoleRepositoryMock;
        private readonly Mock<IBaseWriteRepository<ApplicationUser>> _usersRepositoryMock;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<PatientsListMemoryCache> _listCache;
        private readonly NullLogger<CreatePatientCommandHandler> _logger = new();
        private readonly Mock<PatientsCountMemoryCache> _countCache;
        private readonly Mock<PatientMemoryCache> _patientMemoryCache;
        private readonly Mock<IPublishEndpoint> _publish;
        public CreatePatientCommandTestsWithMock()
        {
            _patientsRepositoryMock = new();
            _userRoleRepositoryMock = new();
            _usersRepositoryMock = new();
            _mapper = new();
            _listCache = new();
            _patientMemoryCache = new();
            _logger = new();
            _countCache = new();
            _patientMemoryCache = new();
            _publish = new();

        }
        [Fact]
        public async Task HandleShould_ReturnFailureResult_WhenLoginAlreadyInUse()
        {
            //Arrange
            _usersRepositoryMock.Setup(
                    x => x.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            var command = Command;
            var handler = new CreatePatientCommandHandler(
                _patientsRepositoryMock.Object,
                _usersRepositoryMock.Object,
                _mapper.Object,
                _listCache.Object,
                _logger,
                _countCache.Object,
                _patientMemoryCache.Object,
                _userRoleRepositoryMock.Object,
                _publish.Object
                );
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DomainErrors.ApplicationUserDomainErrors.LoginAlreadyInUse(command.Login));
        }
        [Fact]
        public async Task HandleShould_ReturnFailureResult_WhenPassportNumberInUse()
        {
            //Arrange
            _usersRepositoryMock.Setup(
                    x => x.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _patientsRepositoryMock.Setup(
                    x => x.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<Patient, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            var command = Command;
            var handler = new CreatePatientCommandHandler(
                _patientsRepositoryMock.Object,
                _usersRepositoryMock.Object,
                _mapper.Object,
                _listCache.Object,
                _logger,
                _countCache.Object,
                _patientMemoryCache.Object,
                _userRoleRepositoryMock.Object,
                _publish.Object
                );
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DomainErrors.PatientDomainErrors.PassportNumberAlreadyInUse(command.PassportNumber));
        }
        [Fact]
        public async Task HandleShould_ReturnFailureResult_WhenPatientRoleNotFound()
        {
            //Arrange
            _usersRepositoryMock.Setup(
                    x => x.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default))
                .ReturnsAsync(false);
            _patientsRepositoryMock.Setup(
                    x => x.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<Patient, bool>>>(), default))
                .ReturnsAsync(false);
            var role = ApplicationUserRole.Create(1, "");
            role = null;
            _userRoleRepositoryMock.Setup(x => x.AsAsyncRead().FirstOrDefaultAsync(It.IsAny<Expression<Func<ApplicationUserRole, bool>>>(), default))
                .ReturnsAsync(role);
            var command = Command;
            var handler = new CreatePatientCommandHandler(
                _patientsRepositoryMock.Object,
                _usersRepositoryMock.Object,
                _mapper.Object,
                _listCache.Object,
                _logger,
                _countCache.Object,
                _patientMemoryCache.Object,
                _userRoleRepositoryMock.Object,
                _publish.Object
                );
            //Act
            var result = await handler.Handle(command, default);
            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DomainErrors.PatientDomainErrors.PatientRoleNotFound);
        }

    }
}
