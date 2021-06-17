using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kutuphane.ViewModel
{
    public class UrunlerModel
    {
        public int Urun_id { get; set; }
        public string Urun_adi { get; set; }
        public Nullable<int> Kategori_id { get; set; }
        public Nullable<int> Firma_id { get; set; }
        public Nullable<int> Alis_fiyat { get; set; }
    }
}