using ShopBridge.DAL.Model;
using ShopBridge.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Repository.Repository
{
  public class ItemRepository : IItemRepository
    {
        public ShopBridgeDBContext _context { get; set; }
       
        public ItemRepository(ShopBridgeDBContext context)
        {
            _context = context;
           
        }
        public async Task<List<Item>> GetAllItems()
        {
            var result = await Task.Run(() => _context.Items);
            return result.ToList();
        }

        public async Task<Item> AddItem(Item item)
        {

            if (item == null)
                throw new ArgumentException("entity is null");

            return await Task.Run(() =>
            {
                try
                {
                    _context.Items.Add(item);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
                return item;
            });
        }

        public async Task<int> RemoveItem(Guid id)
        {
            return await Task.Run(() =>
            {
                var item = _context.Items.SingleOrDefault(e => e.Id.Equals(id));
                if (item != null) _context.Items.Remove(item);
                return _context.SaveChanges();
            });

        }

        public async Task<int> RemoveAllItems()
        {
            return await Task.Run(() =>
            {
                var items = _context.Items.ToList();
                if (items != null) _context.Items.RemoveRange(items);
                return _context.SaveChanges();
            });

        }
    }
}
