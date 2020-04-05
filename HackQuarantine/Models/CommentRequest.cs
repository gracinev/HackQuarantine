using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HackQuarantine.Models
{
    public class CommentRequest
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ItemId { get; set; }
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
