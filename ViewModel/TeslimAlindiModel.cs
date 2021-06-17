using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kutuphane.ViewModel
{
    public class TeslimAlindiModel
    {
        public int Musteri_id { get; set; }
        public Nullable<int> Urun_id { get; set; }
        public string Teslim_Alindi { get; set; }
    }
}