﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using zdravstvena_ustanova.Exception;

namespace Repository
{
    public class ItemWarehouseRepository
    {
        private const string NOT_FOUND_ERROR = "ITEMWAREHOUSE NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _itemWarehouseMaxId;

        public ItemWarehouseRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _itemWarehouseMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<ItemWarehouse> itemWarehouses)
        {
            return itemWarehouses.Count() == 0 ? 0 : itemWarehouses.Max(itemWarehouse => itemWarehouse.Id);
        }

        public IEnumerable<ItemWarehouse> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToItemWarehouse)
                .ToList();
        }

        public ItemWarehouse Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(itemWarehouse => itemWarehouse.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public ItemWarehouse Create(ItemWarehouse itemWarehouse)
        {
            itemWarehouse.Id = ++_itemWarehouseMaxId;
            AppendLineToFile(_path, ItemWarehouseToCSVFormat(itemWarehouse));
            return itemWarehouse;
        }

        public bool Update(ItemWarehouse itemWarehouse)
        {
            var itemWarehouses = GetAll();

            foreach (ItemWarehouse iw in itemWarehouses)
            {
                if (iw.Id == itemWarehouse.Id)
                {
                    iw.WarehouseId = itemWarehouse.WarehouseId;
                    iw.Item.Id = itemWarehouse.Item.Id;
                    iw.Quantity = itemWarehouse.Quantity;
                    iw.Item = itemWarehouse.Item;
                    WriteLinesToFile(_path, ItemWarehousesToCSVFormat((List<ItemWarehouse>)itemWarehouses));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long itemWarehouseId)
        {
            var itemWarehouses = (List<ItemWarehouse>)GetAll();

            foreach (ItemWarehouse iw in itemWarehouses)
            {
                if (iw.Id == itemWarehouseId)
                {
                    itemWarehouses.Remove(iw);
                    WriteLinesToFile(_path, ItemWarehousesToCSVFormat((List<ItemWarehouse>)itemWarehouses));
                    return true;
                }
            }
            return false;
        }

        private string ItemWarehouseToCSVFormat(ItemWarehouse itemWarehouse)
        {
            return string.Join(_delimiter,
                itemWarehouse.Id,
                itemWarehouse.WarehouseId,
                itemWarehouse.Item.Id,
                itemWarehouse.Quantity);
        }
        private List<string> ItemWarehousesToCSVFormat(List<ItemWarehouse> itemWarehouses)
        {
            List<string> lines = new List<string>();

            foreach (ItemWarehouse itemWarehouse in itemWarehouses)
            {
                lines.Add(ItemWarehouseToCSVFormat(itemWarehouse));
            }
            return lines;
        }

        private ItemWarehouse CSVFormatToItemWarehouse(string itemCSVFormat)
        {
            var tokens = itemCSVFormat.Split(_delimiter.ToCharArray());
            return new ItemWarehouse(
                long.Parse(tokens[0]),
                long.Parse(tokens[1]),
                int.Parse(tokens[2]),
                long.Parse(tokens[3]));
        }
        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }
    }
}