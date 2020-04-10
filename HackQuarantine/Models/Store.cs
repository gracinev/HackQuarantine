using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackQuarantine.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public string StoreType { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.Url)]
        public string Website { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longtitude { get; set; }
        public double Rating { get; set; }
        public List<Item> Items { get; set; }

        public Store()
        {

        }

        public Store(string name, string address, string phone, decimal latitude, decimal longitude)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Latitude = latitude;
            Longtitude = longitude;
        }
    }
}
