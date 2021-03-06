﻿using System;
using testgitlab.Model;
using testgitlab.Common;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;


using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace testgitlab.Business
{
    public class CalendarInfoBusiness
    {

        private static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        private static string ApplicationName = "Google Calendar API .NET Quickstart";

        public CalendarInfoBusiness()
        {
            
        }


        public List<CalendarInfoValue> getCalendarInfo(String today){

            List<CalendarInfoValue> result = new List<CalendarInfoValue>();

            if(today.Length < 8 ){
                result.Add(new CalendarInfoValue(){naiyou="日付は８文字で入力してけろ"});
                return result;
            }

            UserCredential credential;

            //using (var stream =
            //    new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            //{

            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        Scopes,
            //        "p.takichi60@gmail.com",
            //        CancellationToken.None).Result;
            //}

            //// Create Google Calendar API service.
            //var service = new CalendarService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = ApplicationName,
            //});


            //TODO:http://kokonotsu.net/log/google-calendar-api-v3-%E3%81%9D%E3%81%AE%EF%BC%92-%E8%AA%8D%E8%A8%BC%E7%B7%A8/

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {

                //local??
                ClientId = "210631258848-9cvh1crkrse03r2nth7ugp1ee9o50f3u.apps.googleusercontent.com",
                ClientSecret = "0D63DQCkUMsNM8r6TJkpeV_9"

                    //Server
                //ClientId = "210631258848-d595s7aek3n6h3g26l1red3pnoplmaoa.apps.googleusercontent.com",
                //ClientSecret = "_SeRQhG7iwXSFpmC7Wpblx_L",

            }, Scopes
                , "p.takichi60@gmail.com"
                , CancellationToken.None
            ).Result;

            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
                ApiKey = "AIzaSyDwmgN5ujZyHQcezrfujOBrjeic78J48NQ",
            });



            // Define parameters of request.
            //EventsResource.ListRequest request = service.Events.List("primary");
            EventsResource.ListRequest request = service.Events.List("rgddvaud5givpebco9g1vgb7ng@group.calendar.google.com");

            int year = Int32.Parse(today.Substring(0, 4));
            int month = Int32.Parse(today.Substring(4, 2));
            int day = Int32.Parse(today.Substring(6, 2));

            DateTime min = new DateTime(year, month, day, 0, 0, 0);
            DateTime max = new DateTime(year, month, day, 23, 59, 59);

            //当日の予定のみ
            request.TimeMin = min;
            request.TimeMax = max;

            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 9999;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                CalendarInfoValue value = null;

                foreach (var eventItem in events.Items)
                {

                    value = new CalendarInfoValue();
                    String from = "";
                    String to = "";
                    String when = "";

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");

                    if(!String.IsNullOrEmpty(eventItem.Start.DateTime.ToString())){
                        
                        from = eventItem.Start.DateTime.Value.ToString("g") ; // YYYY/MM/DD HH:MM:SS
                        when = from; 
                    }
                    if (!String.IsNullOrEmpty(eventItem.End.DateTime.ToString())){
                        
                        to = eventItem.End.DateTime.Value.ToString("g");
                        when = when + " - " + to;
                    }
                    //From - To
                    value.date = when;

                    //スケジュール内容
                    value.naiyou = eventItem.Summary;

                    //場所
                    if (eventItem.Location != null){
                        value.place = eventItem.Location.ToString();
                    }

                    //詳細
                    if (eventItem.Description != null)
                    {
                        value.syousai = eventItem.Description.ToString();
                    }

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
