using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class StorageService : IStorageService
    {
        private readonly List<Item> _items = new()
        {
            new Item("Knife"),
            new Item("Apple"),
            new Item("Coconut"),
            new Item("Mobile phone")
        };
        private readonly ILogger _logger;

        public StorageService(ILogger<StorageService> logger)
        {
            _logger = logger;
        }

        public IList<Item> GetAll()
        {
            _logger.LogDebug("Getting all storage items");
            
            return _items.ToList();
        }

        public Item GetById(Guid id)
        {
            return _items.Find(x => x.Id == id);
        }

        public Guid? Add(Item item)
        {
            if (_items.Any(x => x.Name.Equals(item.Name)))
            {
                _logger.LogInformation("Item with name {Name} already exists", item.Name);
                return null;
            }

            _items.Add(item);
            
            _logger.LogDebug("Item {@Item} was successfully added", item);
            return item.Id;
        }

        public bool Update(Guid id, Item item)
        {
            var storageItem = _items.Find(x => x.Id == id);
            if (storageItem is null)
            {
                _logger.LogInformation("Item with id {Id} does not exist", id);
                return false;
            }

            storageItem.Name = item.Name;
            
            _logger.LogDebug("Item with id {Id} was successfully updated", item.Id);
            return true;
        }

        public bool Delete(Guid id)
        {
            var item = _items.Find(x => x.Id == id);
            if (item is null)
            {
                _logger.LogInformation("Item with id {Id} does not exist", id);
                return false;
            }

            _items.Remove(item);
            
            _logger.LogDebug("Item {@Item} was successfully deleted", item);
            return true;
        }
    }
}