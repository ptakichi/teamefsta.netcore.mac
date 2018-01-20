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

        // GET api/MemoInfo/All
        [HttpGet("All")]
        public List<MemoInfoValue> All()
        {
            //別ドメインからのアクセス対応
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            MemoInfoBusiness business = new MemoInfoBusiness();

            return business.getMemoInfoAll();
        }

        // POST api/MemoInfo/Insert
        [HttpPost("Insert")]
        public Boolean Post(MemoInfoValue info)
        {
            //別ドメインからのアクセス対応
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            MemoInfoBusiness business = new MemoInfoBusiness();
            return business.insertMemoInfo(info);
        }

        // POST api/MemoInfo/Update
        [HttpPost("Update")]
        public Boolean Put(MemoInfoValue info)
        {
            //別ドメインからのアクセス対応
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            MemoInfoBusiness business = new MemoInfoBusiness();

            return business.updateMemoInfo(info);
        }

        // POST api/MemoInfo/Delete/100
        [HttpPost("Delete")]
        public Boolean Delete(int id)
        {
            //別ドメインからのアクセス対応
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            MemoInfoBusiness business = new MemoInfoBusiness();

            return business.deleteMemoInfo(id);
        }
    
    
    }
}
