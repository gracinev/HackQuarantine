using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackQuarantine.Models
{
    public class StockdDbContext : DbContext
    {
        public StockdDbContext(DbContextOptions<StockdDbContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comment { get; set; }
        public DbSet<Item> Item { get; set; }
    }
}
