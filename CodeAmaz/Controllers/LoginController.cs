using CodeAmaz.Models;
using CodeAmaz.Models.Function;
using CodeAmaz.Security;
using CodeAmaz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;

namespace CodeAmaz.Controllers
{
    public class LoginController : Controller
    {
        DB_CODEAMAZ db = new DB_CODEAMAZ();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Login(FormCollection collection, LOGIN lg)
		{
			//tạo biến để lưu username khi đã đăng nhập
			AccountFuntion user = new AccountFuntion();
			AccountViewModels acc = user.DangNhap(lg.USERNAME, lg.PASSWORD);

			// Gán các giá trị người dùng nhập liệu cho các biến 
			var username = collection["USERNAME"];
			var password = collection["PASSWORD"];

			//các ràng buộc dữ liệu
			if (String.IsNullOrEmpty(username))
			{
				ViewData["Loi1"] = "(*) Username không được bỏ trống";
			}
			else if (String.IsNullOrEmpty(password))
			{
				ViewData["Loi2"] = "(*) Password không được bỏ trống";
			}
			else
			{
				//so sánh giá trị người dùng nhập với db
				if (db.LOGINs.SingleOrDefault(n => n.USERNAME == username && n.PASSWORD == password) != null)
				{
					lg = db.LOGINs.SingleOrDefault(n => n.USERNAME == username && n.PASSWORD == password);
					LoginSecurity.UserName = acc;                   //lưu username đã đăng nhập
					return RedirectToAction("Home", "Home");        //sau khi đăng nhập thành công trả về trang ~/Home/Home
				}
				else
				{
					ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
				}
			}
			return View();
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
        public ActionResult Register(FormCollection collection, LOGIN lg)
        {

			// Gán các giá trị người dùng nhập liệu cho các biến 
			var fullname = collection["HOVATEN"];
			var email = collection["EMAIL"];
			var username = collection["USERNAME"];
			var password = collection["PASSWORD"];

			//các ràng buộc dữ liệu
			if (String.IsNullOrEmpty(fullname))
			{
				ViewData["Loi1"] = "(*) Họ tên không được bỏ trống";
			}
			else if (String.IsNullOrEmpty(email))
			{
				ViewData["Loi2"] = "(*) Email không được bỏ trống";
			}
			else if (String.IsNullOrEmpty(username))
			{
				ViewData["Loi3"] = "(*) Username không được bỏ trống";
			}
			else if (String.IsNullOrEmpty(password))
			{
				ViewData["Loi4"] = "(*) Password không được bỏ trống";
			}
			else if (ModelState.IsValid)
            {
                var check = db.LOGINs.FirstOrDefault(s => s.USERNAME == lg.USERNAME);
                if (check == null)
                {
					db.Configuration.ValidateOnSaveEnabled = false;
					lg.ID_QUYEN = "MEMBER";
                    db.LOGINs.Add(lg);
                    db.SaveChanges();
					ViewData["Success"] = "Đăng kí tài khoản thành công!";
				}
                else
                {
                    ViewBag.error = "(*) ID / Username đã tồn tại";
                    return View();
                }
            }
            return View();
        }

		[HttpGet]
		public ActionResult UpdateProfile()
		{
			return View();
		}

		[HttpPost]
		public ActionResult UpdateProfile(FormCollection collection, LOGIN lg)
		{
			// Gán các giá trị người dùng nhập liệu cho các biến 
			var fullname = collection["HOVATEN"];
			var email = collection["EMAIL"];
			var phone = collection["SDT"];
			//var ngaysinh = String.Format("{0:dd/MM/yyyy}", collection["NGAYSINH"]);
			var sex = collection["GIOITINH"];
			var address = collection["DIACHI"];

			if (ModelState.IsValid)
			{
				lg.USERNAME = LoginSecurity.UserName.USERNAME;
				lg.PASSWORD = LoginSecurity.UserName.PASSWORD;
				lg.ANHDAIDIEN = LoginSecurity.UserName.ANHDAIDIEN;
				lg.ID_QUYEN = LoginSecurity.UserName.ID_QUYEN;
				lg.NGAYSINH = LoginSecurity.UserName.NGAYSINH;

				LoginSecurity.UserName.HOVATEN = fullname;
				LoginSecurity.UserName.EMAIL = email;
				LoginSecurity.UserName.SDT = phone;
				//lg.NGAYSINH = DateTime.Parse(ngaysinh);
				LoginSecurity.UserName.GIOITINH = sex;
				LoginSecurity.UserName.DIACHI = address;

				db.Entry(lg).State = EntityState.Modified;

				db.SaveChanges();

				return RedirectToAction("AccountInfor", "Login");
			}
			return RedirectToAction("AccountInfor", "Login");
		}

		public ActionResult Logout()
        {
            LoginSecurity.UserName = null;
            return RedirectToAction("Home", "Home");
        }    		

		public ActionResult AccountInfor()
		{
            return View();
		}

		[HttpGet]
		public ActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ForgotPassword(FormCollection collection, LOGIN lg)
		{
			//biến gán username khi người dùng nhập vào
			var username = collection["USERNAME"];

			//các ràng buộc
			if (String.IsNullOrEmpty(username))
			{
				ViewData["Loi1"] = "(*) Vui lòng nhập Username/ID của bạn";
			}
			else if (ModelState.IsValid)
			{
				var check = db.LOGINs.FirstOrDefault(u => u.USERNAME == lg.USERNAME);
				if (check == null)
				{
					ViewBag.error = "ID / Username không tồn tại";
				}
				else
				{
					ViewData["Success"] = "Mật khẩu của bạn đã được gửi về email đăng kí trước đó. Vui lòng kiểm tra hộp thư để tiến hành đặt lại mật khẩu.";
				}
			}
			return View();
		}

		public ActionResult ChangePassword()
		{
			return View();
		}

		[HttpGet]
		public ActionResult AccountManage()
        {
			//lấy danh sách các user hiện tại
			var account = db.LOGINs.ToList();
            return View(account);
        }

		[HttpPost]
		public ActionResult AccountManage(FormCollection collection, LOGIN lg)
		{
			return View();
		}



	}
}