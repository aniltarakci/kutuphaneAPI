using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kutuphane.ViewModel
{
    public class KitapKiralamaModel
    {
        public Nullable<int> Calisan_id { get; set; }
        public int Musteri_id { get; set; }
        public Nullable<int> Urun_id { get; set; }
        public Nullable<System.DateTime> Kiralama_tarihi { get; set; }
        public Nullable<System.DateTime> TeslimAlma_tarihi { get; set; }
    }
}