using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackQuarantine.Models
{
    public enum SaleStatus
    {
        LowQuantity,
        MediumQuantity,
        HighQuantity
    }
    public class Item
    {
        public string Name { get; set; }
        public bool InStock { get; set; }
        public SaleStatus SaleStatus { get; set; }
        public List<Comment> Comments { get; set; }

        public Item()
        {

        }

        public Item(string name, bool instock, SaleStatus saleStatus)
        {
            Name = name;
            InStock = instock;
            SaleStatus = saleStatus;
        }
    }
}
