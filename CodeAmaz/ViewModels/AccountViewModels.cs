using CodeAmaz.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace CodeAmaz.ViewModels
{
    //dữ liệu bảng LOGIN đc lấy gián tiếp qua AccountViewModels
	public class AccountViewModels
	{
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string HOVATEN { get; set; }
        public string EMAIL { get; set; }
        public string SDT { get; set; }
        public Nullable<System.DateTime> NGAYSINH { get; set; }
        public string GIOITINH { get; set; }
        public string ID_QUYEN { get; set; }
        public string DIACHI { get; set; }
        public string ANHDAIDIEN { get; set; }

		public virtual QUYEN_USER QUYEN_USER { get; set; }

        public int ID_CHANGE { get; set; }
        public string USERNAME_CHANGE { get; set; }
        public string OLDPASSWORD { get; set; }
        public string NEWPASSWORD { get; set; }
        public string CONFIRMNEWPASSWORD { get; set; }
        public Nullable<System.DateTime> TIMECHANGE { get; set; }
    }
}