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
    public class ShelterInfoController : Controller
    {
        // GET api/ShelterInfo/5.0/
        [HttpGet("{ido}/{keido}")]
        public List<ShelterInfoValue> Get(decimal ido,decimal keido)
        {
            ShelterInfoBusiness business = new ShelterInfoBusiness();

            return business.getShelterInfo(ido,keido);
        }
    }
}
