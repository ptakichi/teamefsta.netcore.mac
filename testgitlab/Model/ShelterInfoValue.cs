using System;
namespace testgitlab.Model
{
    public class ShelterInfoValue
    {
        public ShelterInfoValue()
        {
        }

        //避難所名
        public String Name { get; set; }
        //避難所住所
        public String Address { get; set; }
        //緯度
        public Decimal Ido { get; set; }
        //経度
        public Decimal Keido { get; set; }

    }
}