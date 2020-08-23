using System;

namespace ShopBridge.Contracts
{
    public class Item
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImgUrl { get; set; }
    }
}
