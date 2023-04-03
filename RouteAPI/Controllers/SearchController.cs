using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteAPI.Requests;
using RouteAPI.Responses;

namespace RouteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SearchController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Ping
        /// </summary>
        /// <remarks>Method shows the status of API</remarks>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Empty Response</returns>
        /// <response code="200">API alive</response>
        /// <response code="500">API is not working</response>
        [HttpGet("ping")]
        public async Task<IActionResult> Ping(CancellationToken cancellationToken)
        {
           return Ok();
        }

        /// <summary>
        /// Search routes
        /// </summary>
        /// <remarks>Method returns the aggregate search result using several search services</remarks>
        /// <param name="request">request body</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Search result</returns>
        /// <response code="200">Search result</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("search")]
        [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<SearchResponse>> Search(SearchRequest request)
        {
            return Ok(await _mediator.Send(_mapper.Map<SearchRequestMain>(request)));
        }
    }

}