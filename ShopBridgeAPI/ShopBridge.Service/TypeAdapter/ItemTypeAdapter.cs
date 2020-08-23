using ShopBridge.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Service.TypeAdapter
{
   public class ItemTypeAdapter
    {
        public TTarget Adapt<TTarget>(object requestSource) where TTarget : class, new()
        {
            return Get<TTarget>(requestSource);
        }

        private TTarget Get<TTarget>(object source) where TTarget : class
        {
            if (source is Contracts.Item && typeof(TTarget) == typeof(Item))
            {
                var contractItem = source as Contracts.Item;
                var domainItem = new Item
                {
                    Id = contractItem.Id != null ? contractItem.Id.Value : Guid.NewGuid(),
                    Price = contractItem.Price,
                    Name = contractItem.Name,
                    Description = contractItem.Description,
                    IsDeleted = false,
                    CreatedOn = DateTime.Now,
                    ImgUrl= contractItem.ImgUrl
                };

                return domainItem as TTarget;
            }
            else if (source is Item && typeof(TTarget) == typeof(Contracts.Item))
            {
                Item domainItem = source as Item;

                var contractItem = new Contracts.Item
                {
                    Id = domainItem.Id,
                    Price = domainItem.Price,
                    Name = domainItem.Name,
                    Description = domainItem.Description,
                    ImgUrl = domainItem.ImgUrl
                };

                return contractItem as TTarget;
            }
            return null;
        }

        public List<TTarget> AdaptList<TTarget>(object requestSource) where TTarget : class, new()
        {
            return GetList<TTarget>(requestSource);
        }

        private List<TTarget> GetList<TTarget>(object source) where TTarget : class
        {
            if (source is List<Item> && typeof(TTarget) == typeof(Contracts.Item))
            {
                var contractItemList = new List<Contracts.Item>();
                List<Item> domainItemList = source as List<Item>;
                foreach (var item in domainItemList)
                {
                    contractItemList.Add(new Contracts.Item
                    {
                        Id = item.Id,
                        Price = item.Price,
                        Name = item.Name,
                        Description = item.Description,
                        ImgUrl=item.ImgUrl
                    });
                }
                return contractItemList as List<TTarget>;
            }
            else return null;
        }
    }
}
