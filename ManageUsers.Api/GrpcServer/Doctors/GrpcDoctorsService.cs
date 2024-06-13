using DotorsGrpc;
using Grpc.Core;
using ManageUsers.Application.Handlers.Doctor.Queries.GetDoctor;
using MediatR;

namespace ManageUsers.Api.GrpcServer.Doctors
{
    public class GrpcDoctorService : DoctorsService.DoctorsServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcDoctorService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<DoctorReply> GetDoctor(GetDoctorRequest request, ServerCallContext context)
        {
            var query = new GetDoctorQuery()
            {
                Id = Guid.Parse(request.Id),
            };
            var dto = await _mediator.Send(query, context.CancellationToken);
            return new DoctorReply()
            {
                FirstName = dto.Value.FirstName,
                LastName = dto.Value.LastName,
                Patronymic = dto.Value.Patronymic,
                CabinetNumber = dto.Value.CabinetNumber,
                Speciality = dto.Value.Speciality,

            };
        }
    }
}
