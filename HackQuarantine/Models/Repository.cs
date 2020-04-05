using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackQuarantine.Models
{
    public static class Repository
    {
        public static List<Store> _stores = GetTempStores().ToList();
        public static int RequestCount { get; set; } = 0;

        public static void AddComment(CommentRequest comment)
        {
            Store store = _stores.FirstOrDefault(s => s.Id == comment.StoreId);
            Item item = store.Items.FirstOrDefault(i => i.Id == comment.ItemId);
            item.Price = double.Parse(comment.Price.ToString());
            switch (comment.SaleStatus)
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
            item.InStock = comment.InStock == "true" ? true : false;
            Comment tempComment = new Comment() { 
                Id = comment.Id,
                Date = DateTime.Now,
                Notes = comment.Notes,
                Item = item,
                Store = store
            };
            item.Comments.Add(tempComment);
        }

        public static IEnumerable<Store> GetTempStores()
        {
            return new List<Store>
            {
                new Store()
                {
                    Id = 0,
                    Name = "Costco",
                    Address = "123 Huckleberry Drive, New York City, New York",
                    Phone = "1234567890",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Soap",
                            InStock = true,
                            SaleStatus = SaleStatus.MediumQuantity,
                            Price = 10.99,
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Date = DateTime.Now,
                                    Notes = "This is cool!",
                                    Item = new Item() {
                                        Name = "Soap",
                                        InStock = true,
                                        SaleStatus = SaleStatus.MediumQuantity,
                                        Price = 10.99
                                    }
                                }
                            }
                        }
                    }
                },
                new Store()
                {
                    Id = 1,
                    Name = "Target",
                    Address = "Somewhere in California",
                    Phone = "1234567890",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Toilet Paper",
                            InStock = false,
                            SaleStatus = SaleStatus.LowQuantity,
                            Price = 8.99,
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Date = DateTime.Now,
                                    Notes = "Test Comment",
                                    Item = new Item()
                                    {
                                        Name = "Toilet Paper",
                                        InStock = false,
                                        SaleStatus = SaleStatus.LowQuantity,
                                        Price = 8.99
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
