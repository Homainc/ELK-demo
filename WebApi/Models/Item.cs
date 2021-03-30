using System;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace WebApi.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public Item()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }
        
        public Item(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Created = DateTime.UtcNow;
        }
    }
}