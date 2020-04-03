using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackQuarantine.Models
{
    public class Comment
    {
        public DateTime Date { get; set; }
        public Item Item { get; set; }
        public Store Store { get; set; }
        public User User { get; set; }

        public Comment()
        {

        }

        public Comment(DateTime date, Item item, Store store, User user)
        {
            Date = date;
            Item = item;
            Store = store;
            User = user;
        }
    }
}
