using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackQuarantine.Models
{
    public class ItemRequest
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        [Required(ErrorMessage = "Please enter the item name.")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "Please enter the item price.")]
        [Range(1, Int64.MaxValue, ErrorMessage = "Please enter a price that is greater than zero.")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Please enter the item sale status.")]
        public string SaleStatus { get; set; }
        [Required(ErrorMessage = "Please enter if the item is in stock.")]
        public string InStock { get; set; }
        public string Notes { get; set; }
    }
}
