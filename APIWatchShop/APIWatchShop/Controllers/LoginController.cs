using APIWatchShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace APIWatchShop.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/LoginController/GetAccount
        [Route("api/LoginController/DangNhap")]
        [HttpGet]
        
        public IHttpActionResult DangNhap(string TenDangNhap, string MatKhau)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("tendangnhap", TenDangNhap);
                param.Add("matkhau", MatKhau);

                DataTable tb = Database.Database.ReadTable("Proc_DangNhap", param);
                //return Ok(tb);   
                NguoiDung kq = new NguoiDung();
                if (tb.Rows.Count > 0)
                {
                    kq.MAND = tb.Rows[0]["MAND"].ToString();
                    kq.TENND = tb.Rows[0]["TENND"].ToString();
                    kq.TENDN = tb.Rows[0]["TENDN"].ToString();
                    kq.EMAIL = tb.Rows[0]["EMAIL"].ToString();
                    kq.QUYEN = tb.Rows[0]["QUYEN"].ToString();
                    kq.PASS = tb.Rows[0]["PASS"].ToString();
                    kq.SDT = tb.Rows[0]["SDT"].ToString();
                }
                else //return Ok("Chào mừng bạn đến với Web API!");
                    kq.MAND = "";
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/LoginController/ThemNguoiDung")]
        [HttpPost]
        public IHttpActionResult ThemNguoiDung(NguoiDung nd)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("Tennguoidung", nd.TENND);
                param.Add("tendangnhap", nd.TENDN);
                param.Add("matkhau", nd.PASS);
                param.Add("Email", nd.EMAIL);
                param.Add("sdt", nd.SDT);
                param.Add("Quyen", nd.QUYEN);
                //return Ok("Chào mừng bạn đến với Web API!");
                string kq = Database.Database.Exec_Command("ThemNguoiDung", param).ToString();
                //return Ok("Chào mừng bạn đến với Web API!");
                if (kq != null && kq!= "")
                    nd.MAND = kq;

                //NguoiDung kq = Database.ThemNguoiDung(nd);
                //return Ok("Chào mừng bạn đến với Web API!");
                return Ok(nd) ;
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/LoginController/TatCaNguoiDung")]
        [HttpGet]
        public IHttpActionResult GetDecentralization()
        {
            try
            {
                DataTable result = Database.Database.ReadTable("TatCaNguoiDung");
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/LoginController/XoaNguoiDung")]
        [HttpPost]
        public IHttpActionResult XoaNguoiDung(NguoiDung nd)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("mand", nd.MAND);
                string kq = Database.Database.Exec_Command("XoaNguoiDung", param).ToString();
                if (kq != null && kq != "")
                    nd.MAND = kq;

                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
