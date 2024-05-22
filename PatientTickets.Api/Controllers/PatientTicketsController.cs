using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PatientTickets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientTicketsController : ControllerBase
    {
        

        public PatientTicketsController()
        {
            
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientTicketByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {


            return Ok();
        }
    }
}
