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
    public class MemoInfoController : Controller
    {
        // GET api/MemoInfo
        [HttpGet()]
        public List<MemoInfoValue> Get()
        {
            //別ドメインからのアクセス対応
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            MemoInfoBusiness business = new MemoInfoBusiness();

            return business.getMemoInfo();
        }
        // POST api/MemoInfo
        [HttpPost]
        public Boolean Post(MemoInfoValue info)
        {
            //別ドメインからのアクセス対応
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            MemoInfoBusiness business = new MemoInfoBusiness();
            return business.insertMemoInfo(info);
        }
        // GET api/MemoInfo
        [HttpPut("{id}")]
        public Boolean Put(int id, MemoInfoValue info)
        {
            //別ドメインからのアクセス対応
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            MemoInfoBusiness business = new MemoInfoBusiness();

            return business.updateMemoInfo(id, info);
        }
    }
}
