using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackQuarantine.Models;

namespace HackQuarantine.Models
{
    public static class Repository
    {
        public static List<Store> _stores = GetTempStores().ToList();
        public static int ItemRequestCount { get; set; } = 3;
        public static StockdDbContext DbContext { get; set; }

        public static void AddStore(GoogleStore storeRequest)
        {
            Store store = new Store()
            {
                PlaceId = storeRequest.PlaceId,
                Name = storeRequest.Name,
                Address = storeRequest.Address,
                Rating = storeRequest.Rating
            };
            _stores.Add(store);
        }

        public static void AddItem(ItemRequest itemRequest)
        {
            Store store = _stores.FirstOrDefault(s => s.Id == itemRequest.StoreId);
            Item item = new Item()
            {
                Id = 3,
                Name = itemRequest.ItemName,
                Comments = new List<Comment>()
            };
            item.Price = double.Parse(itemRequest.Price.ToString());
            switch (itemRequest.SaleStatus)
            {
                case "LowQuantity":
                    item.SaleStatus = SaleStatus.LowQuantity;
                    break;
                case "MediumQuantity":
                    item.SaleStatus = SaleStatus.MediumQuantity;
                    break;
                case "HighQuantity":
                    item.SaleStatus = SaleStatus.HighQuantity;
                    break;
            }
            item.InStock = itemRequest.InStock == "true" ? true : false;
            Comment comment = new Comment()
            {
                Date = DateTime.Now,
                Notes = itemRequest.Notes,
                //Item = item,
                //Store = store
            };

            store.Items.Add(item);
            item.Comments.Add(comment);
            ItemRequestCount++;
        }


        public static void AddComment(CommentRequest commentRequest)
        {
            Store store = _stores.FirstOrDefault(s => s.Id == commentRequest.StoreId);
            Item item = store.Items.FirstOrDefault(i => i.Id == commentRequest.ItemId);
            item.Price = double.Parse(commentRequest.Price.ToString());
            switch (commentRequest.SaleStatus)
            {
                case "LowQuantity":
                    item.SaleStatus = SaleStatus.LowQuantity;
                    break;
                case "MediumQuantity":
                    item.SaleStatus = SaleStatus.MediumQuantity;
                    break;
                case "HighQuantity":
                    item.SaleStatus = SaleStatus.HighQuantity;
                    break;
            }
            item.InStock = commentRequest.InStock == "true" ? true : false;
            Comment tempComment = new Comment()
            {
                Id = commentRequest.Id,
                Date = DateTime.Now,
                Notes = commentRequest.Notes,
                //Item = item,
                //Store = store
            };
            item.Comments.Add(tempComment);
            DbContext.Comment.Add(tempComment);
        }

        public static IEnumerable<Store> GetTempStores()
        {
            return new List<Store>
            {
                new Store()
                {
                    Id = 0,
                    PlaceId = "Test1",
                    Name = "Costco",
                    City = "San Francisco",
                    Address = "123 Huckleberry Drive, New York City, New York",
                    Phone = "1234567890",
                    Rating = 3


                },
                new Store()
                {
                    Id = 1,
                    PlaceId = "Test2",
                    Name = "Target",
                    City = "San Francisco",
                    Address = "Somewhere in California",
                    Phone = "1234567890",
                    Rating = 5,

                }
            };
        }
    }
}
