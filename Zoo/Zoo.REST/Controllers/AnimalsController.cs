using Microsoft.AspNetCore.Mvc;
using Zoo.REST.Models;
using Zoo.REST.Services;

namespace Zoo.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly ICrudServiceAsync<AnimalModel> _animalService;

        public AnimalsController(ICrudServiceAsync<AnimalModel> animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalModel>>> GetAll()
        {
            var animals = await _animalService.ReadAllAsync();
            var result = animals.Select(a => new AnimalModel
            {
                Id = a.Id,
                Name = a.Name,
                ZookeeperId = a.ZookeeperId,
                ZooId = a.ZooId,
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalModel>> Get(Guid id)
        {
            var animal = await _animalService.ReadAsync(id);
            if (animal == null) return NotFound();

            return Ok(new AnimalModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Age = animal.Age,
                ZooId = animal.ZooId,
                ZookeeperId = animal.ZookeeperId
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateAnimalModel model)
        {
            var animal = new AnimalModel
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Age = model.Age,
                ZookeeperId = model.ZookeeperId,
                ZooId = model.ZooId,
            };

            var created = await _animalService.CreateAsync(animal);
            await _animalService.SaveAsync();

            if (!created) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = animal.Id }, animal);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] CreateAnimalModel model)
        {
            var existing = await _animalService.ReadAsync(id);
            if (existing == null) return NotFound();

            existing.Name = model.Name;
            existing.ZookeeperId = model.ZookeeperId;
            existing.Age = model.Age;
            existing.ZooId = model.ZooId;

            var updated = await _animalService.UpdateAsync(existing);
            await _animalService.SaveAsync();

            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var animal = await _animalService.ReadAsync(id);
            if (animal == null) return NotFound();

            var deleted = await _animalService.RemoveAsync(animal);
            await _animalService.SaveAsync();

            return deleted ? NoContent() : BadRequest();
        }
    }
}
