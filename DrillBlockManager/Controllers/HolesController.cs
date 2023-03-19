using DrillBlockManager.Controllers.Models;
using DrillBlockManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrillBlockManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolesController : ControllerBase
    {
        private HoleService HoleService { get; }

        public HolesController(HoleService holeService)
        {
            HoleService = holeService;
        }


        /// <summary>
        /// Create a new hole with hole points
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stoppingToken">Cancellation token</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("[action]")]
        public async Task Create(HoleCreateApiModel model
            , CancellationToken stoppingToken = default)
        {
            await HoleService.CreateAsync(model, stoppingToken);
        }

        /// <summary>
        /// Update a hole by id with hole points
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stoppingToken">Cancellation token</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("[action]")]
        public async Task Update(HoleUpdateApiModel model
            , CancellationToken stoppingToken = default)
        {
            await HoleService.UpdateAsync(model, stoppingToken);
        }

        /// <summary>
        /// Delete some holes
        /// </summary>
        /// <param name="ids">Hole`s ids</param>
        /// <param name="stoppingToken">Cancellation token</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("[action]")]
        public async Task Delete([FromQuery] IEnumerable<Guid> ids
            , CancellationToken stoppingToken = default)
        {
            await HoleService.DeleteAsync(ids, stoppingToken);
        }
    }
}
