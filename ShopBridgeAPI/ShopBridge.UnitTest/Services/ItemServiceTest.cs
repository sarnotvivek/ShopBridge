using Moq;
using ShopBridge.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using ShopBridge.Repository.IRepository;
using ShopBridge.Service.Service;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.UnitTest.Services
{
    public class ItemServiceTest
    {
        private readonly Mock<IItemRepository> _itemRepoMock;
        private readonly ItemService _itemService;
        public ItemServiceTest()
        {
            _itemRepoMock = new Mock<IItemRepository>();
            _itemService = new ItemService(_itemRepoMock.Object);
        }

        [Fact]
        public async Task GetAllItems_ReturnsOkResult()
        {
            //Arrange 
            List<Item> itemList = new List<Item> {
            new Item{
                Id=new Guid("75BE801F-E7E0-4B5C-AB55-6A806300969E"),
                Name="Cable",
                   CreatedOn = DateTime.Now,
                   Description = "Fast Charging Cable",
                   Price = 25,
                   IsDeleted = false,
                   ImgUrl = string.Empty
            },
             new Item{
                Id=new Guid("75BE801F-E7E0-4B5C-AB55-6A806300969E"),
                Name="WiFi Router",
                   CreatedOn = DateTime.Now,
                   Description = "On Sale",
                   Price = 67,
                   IsDeleted = false,
                   ImgUrl = string.Empty
            }

            };
            _itemRepoMock
.Setup(repo => repo.GetAllItems())
.Returns(Task.FromResult(itemList));

            // Act
            var result = await _itemService.GetAllItems();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ResultObject.Count);
            Assert.IsType<List<Contracts.Item>>(result.ResultObject);
        }

        [Fact]
        public async Task GetAllItems_ReturnsEmptyList()
        {
            //Arrange 
            List<Item> itemList = new List<Item> {};
            _itemRepoMock
.Setup(repo => repo.GetAllItems())
.Returns(Task.FromResult(itemList));

            // Act
            var result = await _itemService.GetAllItems();

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result.ResultObject);
            Assert.IsType<List<Contracts.Item>>(result.ResultObject);
        }

        [Fact]
        public async Task AddItem_ValidObjectPassed_ReturnsSuccess()
        {
            // Arrange
            Contracts.Item item = new Contracts.Item
            {
                Name = "WiFi Router",
                Description = "On Sale",
                Price = 67,
                ImgUrl = string.Empty
            };

            // Act
            var result = await _itemService.AddItem(item);

            // Assert
            Assert.True(result.isSuccess);
            Assert.NotNull(result);

        }

        [Fact]
        public async Task AddItem_NullObjectPassed_ReturnsSuccessFalse()
        {
            // Act
            var result = await _itemService.AddItem(null);

            // Assert
            Assert.False(result.isSuccess);
            Assert.NotNull(result);

        }

        [Fact]
        public async Task RemoveAllItems_ReturnsSuccess()
        {
            // Act
            var result = await _itemService.RemoveAllItems();

            // Assert
            Assert.True(result.isSuccess);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task RemoveItem_NullObjectPassed_ReturnsSuccessFalse()
        {
            // Act
            var result = await _itemService.RemoveItem(Guid.Empty);

            // Assert
            Assert.False(result.isSuccess);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task RemoveItem_ValidIdPassed_ReturnsSuccess()
        {
            // Arrange
             Guid itemId= new Guid("6F3B1E20-9637-46A7-8957-CFE0E0D10A70");

            // Act
            var result = await _itemService.RemoveItem(itemId);

            // Assert
            Assert.True(result.isSuccess);
            Assert.NotNull(result);
        }
    }
}
