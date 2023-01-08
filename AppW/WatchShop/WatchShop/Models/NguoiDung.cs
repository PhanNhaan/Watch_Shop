using System;
using System.Collections.Generic;
using System.Text;

namespace WatchShop.Models
{
    public class NguoiDung
    {
        public string MAND { get; set; }
        public string QUYEN { get; set; }
        public string PASS { get; set; }
        public string TENND { get; set; }
        public string TENDN { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public static NguoiDung nguoidung;
    }
}
