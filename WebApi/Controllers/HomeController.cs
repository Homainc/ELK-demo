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
        
        public HomeController(IStorageService storage, ILogger<HomeController> logger)
        {
            _storage = storage;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IList<Item>> GetAll()
        {
            return Ok(_storage.GetAll());
        }

        [HttpPost]
        public ActionResult<Guid> Create(Item item)
        {
            var id = _storage.Add(item);
            if (!id.HasValue)
            {
                return Conflict();
            }
            
            return CreatedAtAction(nameof(Create), item);
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
    }
}