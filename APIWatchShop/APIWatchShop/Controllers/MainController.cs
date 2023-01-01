using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
    }
}
