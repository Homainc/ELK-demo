﻿using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IStorageService
    {
        IList<Item> GetAll();
        Item GetById(Guid id);
        Guid? Add(Item item);
        bool Update(Guid id, Item item);
        bool Delete(Guid id);
    }
}