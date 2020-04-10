using System;

// Create a class for Google Places data

namespace HackQuarantine.Models
{
    public class StoreDetail
    {
        public string FormattedAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public string PhotoCredit { get; set; }
        public string Website { get; set; }

        public StoreDetail()
        {
        }
    }
}
