using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zdravstena_ustanova.Exception;

namespace Repository
{
    public class ItemRepository
    {
        private const string NOT_FOUND_ERROR = "ITEM NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _itemMaxId;

        public ItemRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _itemMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Item> items)
        {
            return items.Count() == 0 ? 0 : items.Max(item => item.Id);
        }

        public IEnumerable<Item> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToItem)
                .ToList();
        }

        public Item GetById(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(item => item.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Item Create(Item item)
        {
            item.Id = ++_itemMaxId;
            AppendLineToFile(_path, ItemToCSVFormat(item));
            return item;
        }

        private string ItemToCSVFormat(Item item)
        {
            return string.Join(_delimiter,
                item.Id,
                item.Name,
                item.Description);
        }

        private Item CSVFormatToItem(string itemCSVFormat)
        {
            var tokens = itemCSVFormat.Split(_delimiter.ToCharArray());
            return new Item(
                long.Parse(tokens[0]),
                tokens[1], tokens[2]);
        }
        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }
    }
}