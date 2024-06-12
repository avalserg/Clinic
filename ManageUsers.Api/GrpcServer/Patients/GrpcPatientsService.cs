using Grpc.Core;
using ManageUsers.Application.Handlers.Patient.Queries.GetPatient;
using MediatR;
using PatientsGrpc;
using System.Globalization;

namespace ManageUsers.Api.GrpcServer.Patients
{
    public class GrpcPatientsService : PatientsService.PatientsServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcPatientsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<PatientReply> GetPatient(GetPatientRequest request, ServerCallContext context)
        {
            var query = new GetPatientQuery()
            {
                Id = Guid.Parse(request.Id),
            };
            var dto = await _mediator.Send(query, context.CancellationToken);
            return new PatientReply()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Patronymic = dto.Patronymic,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber.Value,
                DateBirthday = dto.DateBirthday.ToString(CultureInfo.CurrentCulture)

            };
        }
    }
}
