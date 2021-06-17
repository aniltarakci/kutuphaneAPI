using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kutuphane.ViewModel
{
    public class FirmalarModel
    {
        public int Firma_id { get; set; }
        public string Firma_adi { get; set; }
        public Nullable<int> Tel { get; set; }
        public Nullable<int> Fax { get; set; }
        public string Eposta_adresi { get; set; }
    }
}