using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpeakMore.Application.Features.CalculateCallValue.Models;
using SpeakMore.Application.Shared.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SpeakMore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SpeakMoreController : Controller
    {
        private readonly IMediator _mediator;

        public SpeakMoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Output<CalculateCallValueOutput>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateActionsConfigurationAsync([FromQuery][Required] int origin, 
                                                                         [FromQuery][Required] int destination,
                                                                         [FromQuery][Required] int timeOfCall,
                                                                         [FromQuery][Required] string planName, 
                                                                         CancellationToken cancellationToken)
        {
            
                var input = new CalculateCallValueInput {
                                                            Origin = origin, 
                                                            Destination = destination, 
                                                            TimeOfCall = timeOfCall, 
                                                            PlanName = planName 
                                                        };

                return Ok(await _mediator.Send(input, cancellationToken));
        }
    }
}
