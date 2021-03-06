﻿using Microsoft.AspNetCore.Http;
using ShopBridge.Contracts;
using ShopBridge.Repository.IRepository;
using ShopBridge.Service.IService;
using ShopBridge.Service.TypeAdapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Service.Service
{
   public class ItemService: IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<Result<List<Item>>> GetAllItems()
        {
            Result<List<Item>> returnModel = new Result<List<Item>>();
            var domainItems = await _itemRepository.GetAllItems();
            var items = domainItems.OrderBy(c => c.CreatedOn).ToList();
            var contractItems = new ItemTypeAdapter().AdaptList<Item>(items);
            returnModel.ResultObject = contractItems;
            returnModel.isSuccess = true;
            return returnModel;
        }

        public async Task<Result<Item>> AddItem(Item model, IFormFile image)
        {
            Result<Item> returnModel = new Result<Item>();
            string base64String = String.Empty;
            if (model==null)
            {
                returnModel.isSuccess = false;
                return returnModel;
            }
            if (image!=null)
            {
                using (var ms = new MemoryStream())
                {
                    await image.CopyToAsync(ms);
                    var imageBytes = ms.ToArray();

                    // Convert byte[] to Base64 String
                     base64String = Convert.ToBase64String(imageBytes);
                }
                model.ImgUrl = base64String;
            }
            var domainItem = new ItemTypeAdapter().Adapt<ShopBridge.DAL.Model.Item>(model);
            var item = await _itemRepository.AddItem(domainItem);
            returnModel.isSuccess = true;
            return returnModel;
        }

        public async Task<Result<bool>> RemoveAllItems()
        {
            Result<bool> returnModel = new Result<bool>();
            try
            {
                await _itemRepository.RemoveAllItems();
                returnModel.isSuccess = true;
            }
            catch (Exception ex)
            {
                returnModel.ErrorDetail = returnModel.ErrorMessage = ex.Message;
                returnModel.isSuccess = false;
            }
            return returnModel;
        }

        public async Task<Result<bool>> RemoveItem(Guid id)
        {
            Result<bool> returnModel = new Result<bool>();
            if (id==Guid.Empty)
            {
                returnModel.isSuccess = false;
                return returnModel;
            }
            try
            {
                await _itemRepository.RemoveItem(id);
                returnModel.isSuccess = true;
            }
            catch (Exception ex)
            {
                returnModel.ErrorDetail = returnModel.ErrorMessage = ex.Message;
                returnModel.isSuccess = false;
            }
            return returnModel;
        }
    }
}
