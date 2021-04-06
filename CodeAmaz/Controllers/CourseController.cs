using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using CodeAmaz.Models;
using CodeAmaz.Models.Function;
using CodeAmaz.Security;

namespace CodeAmaz.Controllers
{
	public class CourseController : Controller
    {
        // GET: KhoaHoc

        //khởi tạo db       
        DB_CODEAMAZ db = new DB_CODEAMAZ();
        public ActionResult CourseList()
        {
            var khoahoc = db.KHOAHOCs.ToList();         //lấy danh sách khóa học

            var kh = khoahoc
                .Where(p => p.MAKH != null)
                .OrderBy(p => p.TENKH)
                .ToList();

			return View(kh);
        }

        public ActionResult CourseDetails(string id)
		{
            //lấy ID khóa học
            var result = new CourseFunction().CourseSearchByID(id);

            result.MOTAFULL = result.MOTAFULL.Replace("#", "<br/>");    //thay thế # bằng <br/> trong bảng mô tả khóa học
            result.MUCTIEU = result.MUCTIEU.Replace("#", "<br/>");      //thay thế # bằng <br/> trong bảng mục tiêu khóa học       

            return View(result);
        }

        [HttpPost]
        public ActionResult Comment(FormCollection fr, string id)
		{
            try
            {
                BINHLUANKHOAHOC cmt = new BINHLUANKHOAHOC();

                //lưu thông tin dữ liệu khi user bình luận vào db
                cmt.MAKH_CMT = id;
                cmt.USERNAME_CMT = LoginSecurity.UserName.USERNAME;
                cmt.HOVATEN_CMT = LoginSecurity.UserName.HOVATEN;
                cmt.ANHDAIDIEN_CMT = LoginSecurity.UserName.ANHDAIDIEN;
                cmt.NOIDUNG_CMT = fr["textCMT"];              
                cmt.TIME_CMT = DateTime.Now;

                
                var result = new CommentFunction.CMTFunction().Insert(cmt);
                if (result == 0)
                {
                    return View();
                }
                return RedirectToAction("CourseDetails", new RouteValueDictionary(new { Controller = "Course", Action = "CourseDetails", id = id }));
            }
            catch (Exception)
            {
                return RedirectToAction("CourseDetails", new RouteValueDictionary(new { Controller = "Course", Action = "CourseDetails", id = id }));
            }
        }

        public ActionResult ViewCMT()
        {
            var model = new CourseFunction().ViewCMT();
            var query = model.OrderByDescending(p => p.USERNAME);
            return PartialView(query.ToList());
        }

        public JsonResult ListName(string q)
        {
            var data = new CourseFunction().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CourseSearch(string Keyword, int page = 1, int pageSize = 1)
        {
            int totalRecord = 0;
            var model = new CourseFunction().CourseSearch(Keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = Keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        [HttpGet]
        public ActionResult ConfirmCourseRegister(string id)
        {
            //lấy ID khóa học
            var result = new CourseFunction().CourseSearchByID(id);

            return View(result);
        }

        [HttpPost]
        public ActionResult ConfirmCourseRegister(FormCollection collection, DANGKIKHOAHOC dk)
		{
            var makh = collection["MAKH"];      //biến lưu trữ mã khóa học
            var tacgia = collection["TACGIA"];     //biến lưu trữ tác giaz khóa học
            var tenkh = collection["TENKH"];     //biến lưu trữ tên khóa học
            var anhkh = collection["ANHKHOAHOC"];     //biến lưu trữ ảnh khóa học

            //thêm các dữ liệu để khi học viên bấm đăng kí sẽ lưu dữ liệu vào db
            dk.USERNAME_DK = LoginSecurity.UserName.USERNAME;
            dk.MAKHOAHOC = makh;
            dk.TENKHOAHOC = tenkh;
            dk.TG = tacgia;
            dk.ANHKH = anhkh;
            dk.TIME_DK = DateTime.Now;

            //kiểm tra xem user này đã từng đăng kí khóa học này chưa, nếu đã đăng kí rồi thì báo lỗi
            var check = db.DANGKIKHOAHOCs.FirstOrDefault(u => u.MAKHOAHOC == dk.MAKHOAHOC && u.USERNAME_DK == LoginSecurity.UserName.USERNAME);
   
            if (check == null)
			{
                //lưu dữ liệu vào bảng DANGKIKHOAHOC
                db.DANGKIKHOAHOCs.Add(dk);
                db.SaveChanges();               
            }

            return RedirectToAction("MyCourse", "Course");
        }

        public ActionResult MyCourse(KHOAHOC kh)
        {
            var mycourse = db.DANGKIKHOAHOCs.ToList();         //lấy danh sách khóa học đã đăng kí

            return View(mycourse);
        }

        public ActionResult CourseManage()
        {
            return View();
        }

        public ActionResult LessonList()
        {
            var lessonList = db.BAIHOCs.ToList();       //lấy danh sách các bài học của các khóa học           

            return View(lessonList);
        }
    }
}