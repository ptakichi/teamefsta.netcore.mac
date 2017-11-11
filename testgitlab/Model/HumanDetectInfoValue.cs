using System;
namespace testgitlab.Model
{
    public class HumanDetectInfoValue
    {
        public HumanDetectInfoValue()
        {
        }

        //検出状態
        public String Detect { get; set; }
        //検出したもの
        public String Detecttime { get; set; }
        //名前
        public String Datatype { get; set; }
        //検出時の写真のURL
        public String Imageurl { get; set; }

    }
}