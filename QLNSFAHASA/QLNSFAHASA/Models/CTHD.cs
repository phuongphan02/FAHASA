//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLNSFAHASA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTHD
    {
        public string MaHD { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> SoLuongBan { get; set; }
        public Nullable<decimal> TongTien { get; set; }
    
        public virtual HoaDon HoaDon { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
