using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kutuphane.ViewModel
{
    public class CalisanlarModel
    {
        public int Calisan_id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public Nullable<int> Tel_no { get; set; }
        public string Adres { get; set; }
    }
}