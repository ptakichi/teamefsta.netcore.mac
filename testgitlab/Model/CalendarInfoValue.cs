using System;
namespace testgitlab.Model
{
    public class CalendarInfoValue
    {
        public CalendarInfoValue()
        {
        }
        //日付
        public String date { get; set; }
        //曜日
        public String youbi { get; set; }
        //ゴミ出し内容
        public String naiyou { get; set; }
        //コード
        public String code { get; set; }

    }
}