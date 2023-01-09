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
    public class CartController : ApiController
    {
        [Route("api/CartController/ThemVaoGioHang")]
        [HttpPost]
        public IHttpActionResult ThemVaoGioHang(ChiTietGioHang gh)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("masp", gh.MASP);
                param.Add("mand", gh.MAND);
                param.Add("sl", gh.SOLUONG);
                
                string kq = Database.Database.Exec_Command("ThemVaoGioHang", param).ToString();
                
                if (kq == null || kq == "")
                    gh.MAND = kq;

                return Ok(gh);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/CartController/LayGioHang")]
        [HttpGet]

        public IHttpActionResult LayGioHang(string mand)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("mand", mand);

                DataTable tb = Database.Database.ReadTable("LayGioHang", param);
                
                return Ok(tb);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/CartController/CapNhatGioHang")]
        [HttpPost]
        public IHttpActionResult CapNhatGioHang(ChiTietGioHang sp)
        {
            try
            {
                //int kq = Database.ThemLoaiHoa(lh);

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("masp", sp.MASP);
                param.Add("mand", sp.MAND);
                param.Add("sl", sp.SOLUONG);
                string kq = Database.Database.Exec_Command("CapNhatGioHang", param).ToString();
                if (kq == null || kq == "")
                    sp.MASP = kq;
                //return kq;

                return Ok(sp);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/CartController/XoaGioHang")]
        [HttpPost]
        public IHttpActionResult XoaGioHang(ChiTietGioHang sp)
        {
            try
            {
                //int kq = Database.ThemLoaiHoa(lh);

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("masp", sp.MASP);
                param.Add("mand", sp.MAND);
                string kq = Database.Database.Exec_Command("XoaGioHang", param).ToString();
                if (kq == null || kq == "")
                    sp.MASP = kq;
                //return kq;

                return Ok(sp);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/CartController/ThemDonHang")]
        [HttpPost]
        public IHttpActionResult ThemDonHang(DonHang dh)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                
                param.Add("mand", dh.MAND);
                param.Add("giatri", dh.GIATRI);

                string kq = Database.Database.Exec_Command("ThemDonHang", param).ToString();

                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/CartController/ThemCTDH")]
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

        [Route("api/CartController/XoaCTGH")]
        [HttpPost]
        public IHttpActionResult XoaCTGH(NguoiDung nd)
        {
            try
            {
                //int kq = Database.ThemLoaiHoa(lh);

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("mand", nd.MAND);
                string kq = Database.Database.Exec_Command("XoaCTGH", param).ToString();

                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
