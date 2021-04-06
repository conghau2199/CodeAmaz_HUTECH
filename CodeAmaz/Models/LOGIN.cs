﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeAmaz.Models
{
    using System;
    using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public partial class LOGIN
    {
        [StringLength(16, ErrorMessage = "Username phải có từ 6 - 16 kí tự", MinimumLength = 6)]
        public string USERNAME { get; set; }
        [DataType(DataType.Password)]
        [StringLength(18, ErrorMessage = "Password phải có từ 6 kí tự trở lên", MinimumLength = 6)]
        public string PASSWORD { get; set; }
        public string HOVATEN { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email không đúng định dạng")]
        public string EMAIL { get; set; }
        public string SDT { get; set; }
        public Nullable<System.DateTime> NGAYSINH { get; set; }
        public string GIOITINH { get; set; }
        public string ID_QUYEN { get; set; }
        public string DIACHI { get; set; }
        public string ANHDAIDIEN { get; set; }
    
        public virtual QUYEN_USER QUYEN_USER { get; set; }
    }
}
