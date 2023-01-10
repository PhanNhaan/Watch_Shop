using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using APIWatchShop.Models;
using APIWatchShop.Database;

namespace APIWatchShop.Controllers
{
    public class MainController : ApiController
    {
        // GET api/MainController/GetAllProduct
        [Route("api/MainController/GetAllProduct")]
        [HttpGet]
        public IHttpActionResult GetDecentralization()
        {
            try
            {
                DataTable result = Database.Database.ReadTable("Proc_GetAllProduct");
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/MainController/ThemSanPham")]
        [HttpPost]
        public IHttpActionResult ThemSanPham(SanPham sp)
        {
            try
            {
                //int kq = Database.ThemLoaiHoa(lh);

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("tensanpham", sp.TENSP);
                param.Add("Gia", sp.DONGIA);
                param.Add("mota", sp.MOTA);
                param.Add("hinh", sp.HINH);
                param.Add("namsanxuat", sp.NAMSX);
                param.Add("hangsx", sp.HANGSX);
                param.Add("gioitinh", sp.GIOITINH);
                string kq = Database.Database.Exec_Command("ThemSanPham", param).ToString();
                if (kq != null || kq != "")
                    sp.MASP = kq;
                //return kq;

                return Ok(sp);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/MainController/CapNhatSanPham")]
        [HttpPost]
        public IHttpActionResult CapNhatSanPham(SanPham sp)
        {
            try
            {
                //int kq = Database.ThemLoaiHoa(lh);

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("masp", sp.MASP);
                param.Add("tensanpham", sp.TENSP);
                param.Add("Gia", sp.DONGIA);
                param.Add("mota", sp.MOTA);
                param.Add("hinh", sp.HINH);
                param.Add("namsanxuat", sp.NAMSX);
                param.Add("hangsx", sp.HANGSX);
                param.Add("gioitinh", sp.GIOITINH);
                string kq = Database.Database.Exec_Command("CapNhatSanPham", param).ToString();
                if (kq != null || kq != "")
                    sp.MASP = kq;
                //return kq;

                return Ok(sp);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/MainController/XoaSanPham")]
        [HttpPost]
        public IHttpActionResult XoaSanPham(SanPham sp)
        {
            try
            {
                //int kq = Database.ThemLoaiHoa(lh);

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("masp", sp.MASP);
                string kq = Database.Database.Exec_Command("XoaSanPham", param).ToString();
                if (kq != null && kq != "")
                    sp.MASP = kq;
                //return kq;

                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
