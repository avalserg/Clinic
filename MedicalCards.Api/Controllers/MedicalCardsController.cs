using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCards.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicalCardsController : ControllerBase
    {
      

        private readonly ILogger<MedicalCardsController> _logger;

        public MedicalCardsController(ILogger<MedicalCardsController> logger)
        {
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalCardByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
           

            return Ok();
        }

    }
}
