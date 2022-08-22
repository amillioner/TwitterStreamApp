using System.Reactive.Subjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Twitter.StreamApp.Data;
using Twitter.StreamApp.Data.Request;
using Twitter.StreamApp.Stream.Stream;
using Twitter.StreamApp.Stream.Stream.Base.Subscription;
using Twitter.StreamApp.Stream.Stream.Interfaces;

namespace Twitter.StreamApp.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwitterController : ControllerBase
    {
        private readonly ILogger<TwitterController> _logger;
        private readonly IFilteredStreamService _filteredStreamManager;
        private readonly ISampledStreamService _sampledStreamManager;
        private CancellationTokenSource _cancellationSource;
        private CancellationToken _cancellationToken;


        public TwitterController(
            ILogger<TwitterController> logger
            , ISubscriptionService<Tweet> subscriptionService
            , IStreamServiceManager streamManager)
        {
            _logger = logger;
            _cancellationSource = new CancellationTokenSource();
            _cancellationToken = _cancellationSource.Token;
            _filteredStreamManager = (IFilteredStreamService)streamManager.Get(typeof(FilteredStreamService));
            _sampledStreamManager = (ISampledStreamService)streamManager.Get(typeof(SampledStreamService));
        }

        [HttpGet("/api/startSampleStream")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> StartSampleStream()
        {
            await _sampledStreamManager.StartStream(_cancellationToken);
            return Ok();
        }

        [HttpGet("/api/startFilteredStream")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> StartFilteredStream()
        {
            await _filteredStreamManager.StartStream(_cancellationToken);
            return Ok();
        }

        [HttpGet("/api/stopStream")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult StopStream()
        {
            _sampledStreamManager.StopStream(_cancellationSource);
            _filteredStreamManager.StopStream(_cancellationSource);
            return Ok();
        }

        [HttpPost("/api/addRules")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddRules([FromBody] AddRulesRequest addRules)
        {
            var res = await _filteredStreamManager.AddRules(addRules);
            return Ok(res);
        }

        [HttpGet("/api/retrieveRules")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> RetrieveRules()
        {
            var res = await _filteredStreamManager.RetrieveRules();
            return Ok(res);
        }

        [HttpPost("/api/deleteRules")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteRules([FromBody] DeleteRulesRequest deleteRules)
        {
            var res = await _filteredStreamManager.DeleteRules(deleteRules);
            return Ok(res);
        }

    }
}