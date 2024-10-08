using Microsoft.AspNetCore.Mvc;
using PrecoDaBanana.API.Models;
using PrecoDaBanana.API.Repositories;

namespace PrecoDaBanana.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BananasController : ControllerBase
    {
        private readonly IBananaRepository _bananaRepository;

        public BananasController(IBananaRepository bananaRepository)
        {
            _bananaRepository = bananaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bananas = await _bananaRepository.GetAllAsync();
            return Ok(bananas);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var banana = await _bananaRepository.GetByIdAsync(id);

            if (banana is null)
            {
                return NotFound();
            }

            return Ok(banana);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Banana newBanana)
        {
            await _bananaRepository.CreateAsync(newBanana);
            return CreatedAtAction(nameof(GetById), new { id = newBanana.Id }, newBanana);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Banana updatedBanana)
        {
            var banana = await _bananaRepository.GetByIdAsync(id);

            if (banana is null)
            {
                return NotFound();
            }

            updatedBanana.Id = banana.Id;
            await _bananaRepository.UpdateAsync(id, updatedBanana);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var banana = await _bananaRepository.GetByIdAsync(id);

            if (banana is null)
            {
                return NotFound();
            }

            await _bananaRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
