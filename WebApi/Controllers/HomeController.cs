using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IStorageService _storage;
        private readonly ILogger _logger;
        private readonly Random _random;
        
        public HomeController(IStorageService storage, ILogger<HomeController> logger)
        {
            _storage = storage;
            _logger = logger;
            _random = new Random();
        }

        [HttpGet]
        public ActionResult<IList<Item>> GetAll()
        {
            return Ok(_storage.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetById(Guid id)
        {
            var item = _storage.GetById(id);

            if (item is null)
            {
                _logger.LogError("Item with {Id} not found", id);
                return NotFound();
            }
            
            _logger.LogInformation("Item with id {Id} was retrieved", id);
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<Guid> Create(Item item)
        {
            var id = _storage.Add(item);
            if (!id.HasValue)
            {
                return Conflict();
            }
            
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Item item)
        {
            if(!_storage.Update(id, item))
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_storage.Delete(id))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("Random")]
        public IActionResult Random()
        {
            var exId = _random.Next() % 5;
            switch (exId)
            {
                case 0:
                    throw new ArgumentException(nameof(exId));
                case 1:
                    throw new NullReferenceException(nameof(exId));
                case 2:
                    throw new ApplicationException();
                case 3:
                    throw new ArithmeticException();
                default:
                    _logger.LogInformation("No exception");
                    break;
            }

            return Ok();
        }
    }
}