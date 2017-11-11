using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testgitlab.Business;
using testgitlab.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testgitlab.Controllers
{
    [Route("api/[controller]")]
    public class GomiInfoController : Controller
    {
        // GET api/ShelterInfo/20171111
        [HttpGet("{today}")]
        public GomiInfoValue Get(String today)
        {

            //別ドメインからのアクセス対応
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            GomiInfoBusiness business = new GomiInfoBusiness();

            return business.getGomiInfo(today);

        }
 
    }
}
