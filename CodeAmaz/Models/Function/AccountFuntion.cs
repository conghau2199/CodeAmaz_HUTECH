using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeAmaz.ViewModels;

namespace CodeAmaz.Models.Function
{
	public class AccountFuntion
	{
		DB_CODEAMAZ db;
		public AccountFuntion()
		{
			db = new DB_CODEAMAZ();
		}

		public AccountViewModels DangNhap(string username, string password)
		{
			var result = (from a in db.LOGINs
						  where (a.USERNAME.Equals(username) && a.PASSWORD.Equals(password))
						  select new AccountViewModels
						  {						  
							  ID_QUYEN = a.ID_QUYEN,
							  USERNAME = a.USERNAME,
							  PASSWORD = a.PASSWORD,
							  HOVATEN = a.HOVATEN,
							  DIACHI = a.DIACHI,
							  EMAIL = a.EMAIL,
							  GIOITINH = a.GIOITINH,
							  NGAYSINH = a.NGAYSINH,
							  SDT = a.SDT,
							  ANHDAIDIEN = a.ANHDAIDIEN,
							  QUYEN_USER = a.QUYEN_USER

						  }).FirstOrDefault();
			return result;
		}

		public AccountViewModels ChangePassword(string username, string oldPassword, string newPassword)
		{
			var result = (from a in db.CHANGE_PASS
						  where (a.USERNAME_CHANGE.Equals(username) && a.OLDPASSWORD.Equals(oldPassword) && a.NEWPASSWORD.Equals(newPassword))
						  select new AccountViewModels
						  {			
							  ID_CHANGE = a.ID_CHANGE,
							  USERNAME_CHANGE = a.USERNAME_CHANGE,
							  OLDPASSWORD = a.OLDPASSWORD,
							  NEWPASSWORD = a.NEWPASSWORD,
							  CONFIRMNEWPASSWORD = a.CONFIRMNEWPASSWORD,
							  TIMECHANGE = a.TIMECHANGE
						  }).FirstOrDefault();
			return result;
		}

		public AccountViewModels SearchUsernameByID(string id)
		{

			var resultUser = (from a in db.LOGINs
							where a.USERNAME == id
							select new AccountViewModels
							{
								USERNAME = a.USERNAME,
								PASSWORD = a.PASSWORD,
								HOVATEN = a.HOVATEN,
								EMAIL = a.EMAIL,
								SDT = a.SDT,
								NGAYSINH = a.NGAYSINH,
								GIOITINH = a.GIOITINH,
								ANHDAIDIEN = a.ANHDAIDIEN,
								ID_QUYEN = a.ID_QUYEN							
							}).FirstOrDefault();
			return resultUser;
		}
	}
}