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
    
    public partial class TeslimAlindi
    {
        public int Musteri_id { get; set; }
        public Nullable<int> Urun_id { get; set; }
        public string Teslim_Alindi { get; set; }
    
        public virtual KitapKiralama KitapKiralama { get; set; }
    }
}