using ShopBridge.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Repository.IRepository
{
   public interface IItemRepository
    {
        Task<List<Item>> GetAllItems();

        Task<Item> AddItem(Item item);

        Task<int> RemoveItem(Guid id);

        Task<int> RemoveAllItems();
    }
}
