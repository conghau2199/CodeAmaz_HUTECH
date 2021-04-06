using CodeAmaz.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeAmaz.ViewModels
{
    //dữ liệu bảng KHOAHOC đc lấy gián tiếp qua KhoaHocViewModels
	public class CourseViewModels
	{
        public string MAKH { get; set; }
        public string TENKH { get; set; }
        public string TACGIA { get; set; }
        public Nullable<decimal> GIA { get; set; }
        public string ANHKHOAHOC { get; set; }
        public string MUCTIEU { get; set; }
        public string MOTANGAN { get; set; }
        public string MOTAFULL { get; set; }
        public string MACD { get; set; }
        public Nullable<int> SOBAIGIANG { get; set; }
        public string ANHMOTA { get; set; }
        public virtual CHUDE CHUDE { get; set; }

        //dang ki khoa hoc
        public int ID_DANGKI { get; set; }
        public string USERNAME_DK { get; set; }
        public string MAKHOAHOC { get; set; }
        public string TENKHOAHOC { get; set; }
        public string TG { get; set; }
        public string ANHKH { get; set; }
        public Nullable<System.DateTime> TIME_DK { get; set; }

        public virtual KHOAHOC KHOAHOC { get; set; }

        //bai hoc
        public string MABH { get; set; }
        public string TENBH { get; set; }
        public string NOIDUNGBH { get; set; }

        //binh luan khoa hoc
        public int ID_COMMENT { get; set; }      
        public IEnumerable<CommentViewModels> Comment { get; set; }

        //loginviewmodels
        public string USERNAME { get; set; }
    }
}