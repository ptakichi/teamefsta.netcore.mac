using System;
namespace testgitlab.Model
{
    public class CalendarInfoValue
    {
        public CalendarInfoValue()
        {
        }
        //開始日時〜終了日時
        public String date { get; set; }
        //スケジュール内容
        public String naiyou { get; set; }
        //場所
        public String place { get; set; }
        //詳細
        public String syousai { get; set; }

    }
}