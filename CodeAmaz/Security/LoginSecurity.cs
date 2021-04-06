using CodeAmaz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeAmaz.Security
{
	public class LoginSecurity
	{
        static string usernameSessionvar = "username";
        public static AccountViewModels UserName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }
                var sessionVar = HttpContext.Current.Session[usernameSessionvar];
                if (sessionVar != null)
                {
                    return sessionVar as AccountViewModels;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[usernameSessionvar] = value;
            }
        }
    }
}