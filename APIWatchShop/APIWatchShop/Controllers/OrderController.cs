using APIWatchShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIWatchShop.Controllers
{
    public class OrderController : ApiController
    {
        [Route("api/OrderController/ThemDonHang")]
        [HttpPost]
        public IHttpActionResult ThemDonHang(DonHang dh)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();

                param.Add("mand", dh.MAND);
                param.Add("giatri", dh.GIATRI);

                string kq = Database.Database.Exec_Command("ThemDonHang", param).ToString();
                if (kq != null && kq != "")
                    dh.MADH = kq;

                return Ok(dh);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/OrderController/ThemCTDH")]
        [HttpPost]
        public IHttpActionResult ThemCTDH(ChiTietDonHang ctdh)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("masp", ctdh.MASP);
                param.Add("madh", ctdh.MADH);
                param.Add("sl", ctdh.SL);

                string kq = Database.Database.Exec_Command("ThemCTDH", param).ToString();

                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/OrderController/TatCaDonHang")]
        [HttpGet]
        public IHttpActionResult TatCaDonHang()
        {
            try
            {
                DataTable result = Database.Database.ReadTable("TatCaDonHang");
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/OrderController/TatCaDonHangTheoNguoiDung")]
        [HttpGet]
        public IHttpActionResult TatCaDonHangTheoNguoiDung(string mand)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("mand", mand);

                DataTable result = Database.Database.ReadTable("TatCaDonHangTheoNguoiDung", param);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }
        
        [Route("api/OrderController/LayCTDH")]
        [HttpGet]
        public IHttpActionResult LayCTDH(string madh)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("madh", madh);

                DataTable result = Database.Database.ReadTable("LayCTDH", param);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
