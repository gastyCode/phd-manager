using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;

namespace PhDManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThesisController(IThesisService thesisService) : Controller
    {
        private readonly IThesisService _thesisService = thesisService;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateThesis(Thesis thesis)
        {
            await _thesisService.CreateThesis(thesis);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteThesis(int id)
        {
            await _thesisService.DeleteThesis(id);
            return NoContent();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Thesis>?>> GetTheses() => await _thesisService.GetTheses();

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Thesis>> GetThesis(int id)
        {
            var thesis = await _thesisService.GetThesis(id);

            if (thesis is null) return NotFound();

            return thesis;
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateThesis(int id, Thesis thesis)
        {
            await _thesisService.UpdateThesis(id, thesis);
            return NoContent();
        }
    }
}
