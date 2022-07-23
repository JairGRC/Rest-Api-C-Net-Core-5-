using System;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Interfaces
{
    public interface IInMemItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        void CreateItem (Item item);
    }
}