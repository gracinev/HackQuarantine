using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackQuarantine.Models
{
    public static class Repository
    {
        private static List<Store> _stores = new List<Store>();
        public static int RequestCount { get; set; } = 0;
        public static IEnumerable<Store> Stores
        {
            get
            {
                return _stores;
            }
        }

        public static void AddStore(Store store)
        {
            _stores.Add(store);
            store.Id = RequestCount;
            RequestCount += 1;
        }

        public static IEnumerable<Store> GetStores()
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
                                    Notes = "This is cool!"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
