using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIWatchShop.Models
{
    public class DonHang
    {
        public string MADH { get; set; }
        public string MAND { get; set; }
        public string TENND { get; set; }
        public DateTime NGAYLAP { get; set; }
        public int GIATRI { get; set; }
    }
}