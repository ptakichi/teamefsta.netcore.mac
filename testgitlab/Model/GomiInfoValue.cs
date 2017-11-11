using System;
namespace testgitlab.Model
{
    public class GomiInfoValue
    {
        public GomiInfoValue()
        {
        }
    //id int not null identity(1, 1) primary key,
    //gomidate nvarchar(8) not null,
    //youbi nvarchar(10) not null,
    //naiyou nvarchar(100),
    //code nvarchar(10) not null,
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