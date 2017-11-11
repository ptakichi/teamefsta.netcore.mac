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
    public class CalendarInfoController : Controller
    {
        // GET api/CalendarInfo/20171111
        [HttpGet("{today}")]
        public List<CalendarInfoValue>Get(String today)
        {
//            { "web":{ "client_id":"210631258848-abp3kgu2q2sg60vnkm59ll2vrd0lkc8f.apps.googleusercontent.com","project_id":"teamefsta-1509869802139","auth_uri":"https://accounts.google.com/o/oauth2/auth","token_uri":"https://accounts.google.com/o/oauth2/token","auth_provider_x509_cert_url":"https://www.googleapis.com/oauth2/v1/certs","client_secret":"7vYG53BNsEH2SpLoHaQAGqEs","redirect_uris":["http://teamefstanetcoremac.azurewebsites.net"]
    //}
//}
            //別ドメインからのアクセス対応
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            CalendarInfoBusiness business = new CalendarInfoBusiness();

            return business.getCalendarInfo(today);

        }
 
    }
}
