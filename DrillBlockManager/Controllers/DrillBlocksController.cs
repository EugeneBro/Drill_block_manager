using DrillBlockManager.Controllers.Models;
using DrillBlockManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrillBlockManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrillBlocksController : ControllerBase
    {
        private DrillBlockService DrillBlockService { get; }

        public DrillBlocksController(DrillBlockService drillBlockService)
        {
            DrillBlockService = drillBlockService;
        }

        /// <summary>
        /// Create a new drill block with points
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stoppingToken">Cancellation token</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("[action]")]
        public async Task Create(DrillBlockCreateApiModel model
            , CancellationToken stoppingToken = default)
        {
            await DrillBlockService.CreateAsync(model, stoppingToken);
        }

        /// <summary>
        /// Update a drill bclock by id with points
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stoppingToken">Cancellation token</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("[action]")]
        public async Task Update(DrillBlockUpdateApiModel model
            , CancellationToken stoppingToken = default)
        {
            await DrillBlockService.UpdateAsync(model, stoppingToken);
        }

        /// <summary>
        /// Delete some drill blocks
        /// </summary>
        /// <param name="ids">Drill block`s ids</param>
        /// <param name="stoppingToken">Cancellation token</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("[action]")]
        public async Task Delete([FromQuery] IEnumerable<Guid> ids
            , CancellationToken stoppingToken = default)
        {
            await DrillBlockService.DeleteAsync(ids, stoppingToken);
        }
    }
}
