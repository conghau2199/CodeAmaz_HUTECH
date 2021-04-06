using CodeAmaz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeAmaz.Models.Function
{
	public class CourseFunction
	{
		DB_CODEAMAZ db;

		public CourseFunction()
		{
			db = new DB_CODEAMAZ();
		}

        public CourseViewModels CourseSearchByID(string id)
        {
            //comment user
            var resultCMT = (from a in db.KHOAHOCs
                            join b in db.BINHLUANKHOAHOCs on a.MAKH equals b.MAKH_CMT
                            join c in db.LOGINs on b.USERNAME_CMT equals c.USERNAME
                            where a.MAKH == id
                            select new CommentViewModels { USERNAME_CMT = c.USERNAME, MAKH_CMT = a.MAKH, ANHDAIDIEN_CMT = c.ANHDAIDIEN, HOVATEN_CMT = c.HOVATEN ,TIME_CMT = b.TIME_CMT, NOIDUNG_CMT = b.NOIDUNG_CMT }
                         );

            //danh sách khóa học
            var resultKH = (from a in db.KHOAHOCs
                            where a.MAKH == id
                            select new CourseViewModels
                            {
                                MAKH = a.MAKH,
                                TENKH = a.TENKH,
                                TACGIA = a.TACGIA,
                                GIA = a.GIA,
                                ANHKHOAHOC = a.ANHKHOAHOC,
                                SOBAIGIANG = a.SOBAIGIANG,
                                MUCTIEU = a.MUCTIEU,
                                MOTANGAN = a.MOTANGAN,                            
                                MOTAFULL = a.MOTAFULL,
                                ANHMOTA = a.ANHMOTA,
                                CHUDE = a.CHUDE,
                                MACD = a.MACD                               
                            }).FirstOrDefault();
            resultKH.Comment = resultCMT;
            return resultKH;
        }

        public List<string> ListName(string keyword)
        {
            return db.KHOAHOCs.Where(x => x.TENKH.Contains(keyword)).Select(x => x.TENKH).ToList();
        }
        public List<CourseViewModels> CourseSearch(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.KHOAHOCs.Where(x => x.TENKH.Contains(keyword)).Count();
            var model = (from a in db.KHOAHOCs
                             //join b in db.CHUDEs
                             //on a.MACD equals b.MACD
                         where a.TENKH.Contains(keyword)
                         select new
                         {
                             MAKH = a.MAKH,
                             TENKH = a.TENKH,
                             TACGIA = a.TACGIA,
                             GIA = a.GIA,
                             ANHKHOAHOC = a.ANHKHOAHOC,
                             SOBAIGIANG = a.SOBAIGIANG,
                             MUCTIEU = a.MUCTIEU,
                             MOTANGAN = a.MOTANGAN,
                             MOTAFULL = a.MOTAFULL,
                             ANHMOTA = a.ANHMOTA,
                             CHUDE = a.CHUDE,
                             MACD = a.MACD,

                         }).AsEnumerable().Select(x => new CourseViewModels()
                         {
                             MAKH = x.MAKH,
                             TENKH = x.TENKH,
                             TACGIA = x.TACGIA,
                             GIA = x.GIA,
                             ANHKHOAHOC = x.ANHKHOAHOC,
                             SOBAIGIANG = x.SOBAIGIANG,
                             MUCTIEU = x.MUCTIEU,
                             MOTANGAN = x.MOTANGAN,
                             MOTAFULL = x.MOTAFULL,
                             ANHMOTA = x.ANHMOTA,
                             CHUDE = x.CHUDE,
                             MACD = x.MACD
                         });
            model.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        //xem các bình luận
        public List<CourseViewModels> ViewCMT()
        {
            var model = (from a in db.LOGINs
                         join c in db.BINHLUANKHOAHOCs on a.USERNAME equals c.USERNAME_CMT
                         join b in db.KHOAHOCs on c.MAKH_CMT equals b.MAKH
                         where (a.USERNAME != null)
                         select new CourseViewModels
                         {
                             MAKH = b.MAKH,
                             USERNAME = a.USERNAME
                             
                         });
            return model.ToList<CourseViewModels>();
        }
    }
}