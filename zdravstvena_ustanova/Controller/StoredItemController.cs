using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Service;

namespace Controller
{
   public class StoredItemController
   {
        private readonly StoredItemService _storedItemService;

        public StoredItemController(StoredItemService storedItemService)
        {
            _storedItemService = storedItemService;
        }

        public IEnumerable<StoredItem> GetAll()
        {
            return _storedItemService.GetAll();
        }

        public StoredItem Create(StoredItem storedItem)
        {
            return _storedItemService.Create(storedItem);
        }
        public bool Update(StoredItem storedItem)
        {
            return _storedItemService.Update(storedItem);
        }
        public bool Delete(long storedItemId)
        {
            return _storedItemService.Delete(storedItemId);
        }

        public int GetTotalItemCount(Item item)
        {
            return _storedItemService.GetTotalItemCount(item);
        }

        public bool MoveItemFromTo(Warehouse fromWarehouse, Room toRoom, Item item, int quantity)
        {
            return _storedItemService.MoveItemFromTo(fromWarehouse, toRoom, item, quantity);
        }
        public bool MoveItemFromTo(Room fromRoom, Room toRoom, Item item, int quantity)
        {
            return _storedItemService.MoveItemFromTo(fromRoom, toRoom, item, quantity);
        }
        public bool MoveItemFromTo(Room fromRoom, Warehouse toWarehouse, Item item, int quantity)
        {
            return _storedItemService.MoveItemFromTo(fromRoom, toWarehouse, item, quantity);
        }
    }
}