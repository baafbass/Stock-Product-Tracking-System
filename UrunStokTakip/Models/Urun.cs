//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UrunStokTakip.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Urun
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public string Fiyat { get; set; }
        public Nullable<int> Stok { get; set; }
        public Nullable<bool> Populer { get; set; }
        public string Resim { get; set; }
        public Nullable<int> KategoriId { get; set; }
    
        public virtual Kategori Kategori { get; set; }
    }
}
