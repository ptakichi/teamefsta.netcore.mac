using System;
namespace testgitlab.Model
{
    public class MemoInfoValue
    {
        public MemoInfoValue()
        {
        }
        //メモ内容
        public String id { get; set; }

        //メモ内容
        public String Naiyou { get; set; }
        //完了フラグ
        public String Endflg { get; set; }
        //期限日時
        public String Kigendate { get; set; }
        //登録日時
        public String Entrydate { get; set; }
        //更新日時
        public String Updatedate { get; set; }

    }
}