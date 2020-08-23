using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopBridge.Contracts;
using ShopBridge.Service.IService;

namespace ShopBridgeAPI.Controllers
{

    [Route("api/[controller]")]
    public class ShopBridgeController : Controller
    {
        private readonly IItemService _itemService;
        public ShopBridgeController(IItemService itemService) 
        {
            _itemService = itemService;
        }

        [Route("GetAllItems")]
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            Result<List<Item>> returnModel = new Result<List<Item>>();
            try
            {
                returnModel = await _itemService.GetAllItems();
            }
            catch (Exception ex)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorMessage = ex.Message;
                returnModel.ErrorDetail = ex.GetBaseException().Message;

            }
            return Ok(returnModel);
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem(string jsonString, IFormFile image)
        {
            Result<Item> returnModel = new Result<Item>();
            Item item = null;
            try
            {
                item = JsonConvert.DeserializeObject<Item>(jsonString);

                returnModel = await _itemService.AddItem(item, image);
            }
            catch (Exception ex)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorMessage = ex.Message;
                returnModel.ErrorDetail = ex.GetBaseException().Message;
            }

            return Ok(returnModel);
        }

        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            Result<bool> returnModel = new Result<bool>();
            try
            {
                returnModel = await _itemService.RemoveItem(id);
            }
            catch (Exception ex)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorMessage = ex.Message;
                returnModel.ErrorDetail = ex.GetBaseException().Message;
            }
            return Ok(returnModel);
        }

        [HttpDelete]
        [Route("DeleteAllItems")]
        public async Task<IActionResult> DeleteAllItems()
        {
            Result<bool> returnModel = new Result<bool>();
            try
            {
                returnModel = await _itemService.RemoveAllItems();
            }
            catch (Exception ex)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorMessage = ex.Message;
                returnModel.ErrorDetail = ex.GetBaseException().Message;
            }
            return Ok(returnModel);
        }
    }
}