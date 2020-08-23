using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopBridge.Contracts;

namespace ShopBridge.Service.IService
{
   public interface IItemService
    {
        Task<Result<List<Item>>> GetAllItems();

        Task<Result<Item>> AddItem(Item item, IFormFile image);

        Task<Result<bool>> RemoveItem(Guid id);

        Task<Result<bool>> RemoveAllItems();
    }
}
