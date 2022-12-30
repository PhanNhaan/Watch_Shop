using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIWatchShop.Controllers
{
    public class ServiceController : ApiController
    {
        // GET api/ServiceController/HelloWebAPI
        [Route("api/ServiceController/HelloWebAPI")]
        [HttpGet]
        public IHttpActionResult HelloWebAPI()
        {
            return Ok("Chào mừng bạn đến với Web API!");
        }

        // GET api/ServiceController/GetDecentralization
        [Route("api/ServiceController/GetDecentralization")]
        [HttpGet]
        public IHttpActionResult GetDecentralization()
        {
            try
            {
                DataTable result = Database.Database.ReadTable("Proc_GetDecentralization");
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
