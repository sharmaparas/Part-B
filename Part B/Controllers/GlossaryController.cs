using Microsoft.AspNetCore.Mvc;
using Part_B.Model;
using Part_B.Service.Contract;

namespace Part_B.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GlossaryController : ControllerBase
    {
        private readonly ILogger<GlossaryController> _logger;
        private readonly IGlossaryService _service;

        public GlossaryController(ILogger<GlossaryController> logger, IGlossaryService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetGlossaryAsync(id);
            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] GlossaryFilter filter)
        {
            var result = await _service.GetPagedGlossaryListAsync(filter);
            return Ok(result);
        }

        [HttpPost("upsert")]
        public async Task<IActionResult> UpSert([FromBody] GlossaryView item)
        {
            var result = await _service.UpSertAsync(item);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteGlossaryAsync(id);
            return Ok(result);
        }
    }
}
