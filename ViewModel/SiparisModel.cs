using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kutuphane.ViewModel
{
    public class SiparisModel
    {
        public int Siparis_id { get; set; }
        public Nullable<int> Urun_id { get; set; }
        public Nullable<int> Adet { get; set; }
    }
}