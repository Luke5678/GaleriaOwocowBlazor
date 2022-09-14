using GaleriaOwocowBlazor.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaleriaOwocowBlazor.Shared;

namespace GaleriaOwocowBlazor.Server.Controllers
{
    [Route("api/fruit")]
    [ApiController]
    public class FruitController : ControllerBase
    {
        private readonly FruitsDbContext _context;
        private readonly IHostEnvironment _environment;

        public FruitController(FruitsDbContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/fruit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fruit>>> GetFruits()
        {
            return await _context.Fruits.ToListAsync();
        }

        // GET: api/fruit/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Fruit>> GetFruit(int id)
        {
            var fruit = await _context.Fruits.FindAsync(id);

            if (fruit == null)
            {
                return NotFound();
            }

            return fruit;
        }

        // PUT: api/fruit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFruit(int id, Fruit fruit)
        {
            if (id != fruit.Id)
            {
                return BadRequest();
            }

            _context.Entry(fruit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FruitExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/fruit
        [HttpPost]
        public async Task<IActionResult> PostFruit([FromForm] FruitCreateRequestModel fruitRequestModel)
        {
            var uploadPath = Path.Combine(_environment.ContentRootPath, "wwwroot/upload");
            Directory.CreateDirectory(uploadPath);

            if (fruitRequestModel.File.Length > 0)
            {
                var filePath = Path.Combine(uploadPath, fruitRequestModel.File.FileName);
                await using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await fruitRequestModel.File.CopyToAsync(fileStream);
            }

            _context.Fruits.Add(fruitRequestModel.ToFruit());
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/fruit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFruit(int id)
        {
            var fruit = await _context.Fruits.FindAsync(id);
            if (fruit == null)
            {
                return NotFound();
            }

            _context.Fruits.Remove(fruit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FruitExists(int id)
        {
            return (_context.Fruits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}