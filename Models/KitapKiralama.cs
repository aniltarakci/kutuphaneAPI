//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kutuphane.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KitapKiralama
    {
        public Nullable<int> Calisan_id { get; set; }
        public int Musteri_id { get; set; }
        public Nullable<int> Urun_id { get; set; }
        public Nullable<System.DateTime> Kiralama_tarihi { get; set; }
        public Nullable<System.DateTime> TeslimAlma_tarihi { get; set; }
    
        public virtual Calisanlar Calisanlar { get; set; }
        public virtual Uyeler Uyeler { get; set; }
        public virtual Urunler Urunler { get; set; }
        public virtual TeslimAlindi TeslimAlindi { get; set; }
    }
}
