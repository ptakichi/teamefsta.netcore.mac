using System;
using testgitlab.Model;
using testgitlab.Common;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;


using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace testgitlab.Business
{
    public class CalendarInfoBusiness
    {

        //private static String searchSql = "select * from GomiInfo where gomidate = '@today';";

        //private static String ClientID = "210631258848-abp3kgu2q2sg60vnkm59ll2vrd0lkc8f.apps.googleusercontent.com";
        //private static String  ClientSecret = "7vYG53BNsEH2SpLoHaQAGqEs";
        //private static String  calendarID = "rgddvaud5givpebco9g1vgb7ng@group.calendar.google.com";

        //{ "web":{ "client_id":"210631258848-abp3kgu2q2sg60vnkm59ll2vrd0lkc8f.apps.googleusercontent.com","project_id":"teamefsta-1509869802139","auth_uri":"https://accounts.google.com/o/oauth2/auth","token_uri":"https://accounts.google.com/o/oauth2/token","auth_provider_x509_cert_url":"https://www.googleapis.com/oauth2/v1/certs","client_secret":"7vYG53BNsEH2SpLoHaQAGqEs","redirect_uris":["http://teamefstanetcoremac.azurewebsites.net"]
        //}
        //}

        public CalendarInfoBusiness()
        {
            
        }

        private static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        private static string ApplicationName = "Google Calendar API .NET Quickstart";

        public List<CalendarInfoValue> getCalendarInfo(String today){


            List<CalendarInfoValue> result = new List<CalendarInfoValue>();

            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/calendar-dotnet-quickstart.json

                UserCredential credential;

                using (var stream =
                    new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                {

                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "p.takichi60@gmail.com",
                        CancellationToken.None).Result;
                //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    //GoogleClientSecrets.Load(stream).Secrets,
                    //Scopes,
                    //"p.takichi60@gmail.com",
                    //CancellationToken.None,
                    //new FileDataStore(credPath, true)).Result;
                    //Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define parameters of request.
            //EventsResource.ListRequest request = service.Events.List("primary");
            EventsResource.ListRequest request = service.Events.List("rgddvaud5givpebco9g1vgb7ng@group.calendar.google.com");

                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                // List events.
                Events events = request.Execute();
                Console.WriteLine("Upcoming events:");
                if (events.Items != null && events.Items.Count > 0)
                {
                    CalendarInfoValue value = null;

                    foreach (var eventItem in events.Items)
                    {
                        string when = eventItem.Start.DateTime.ToString();
                        if (String.IsNullOrEmpty(when))
                        {
                            when = eventItem.Start.Date;
                        }

                        value = new CalendarInfoValue();
                        value.date = when + ":" + eventItem.Summary;
                        Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                        result.Add(value);
                    }
                }
                else
                {
                    Console.WriteLine("No upcoming events found.");
                }



            return result;
        }

    }
}
