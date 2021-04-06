using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeAmaz.Models.Function
{
	public class CommentFunction
	{
        public class CMTFunction
        {
            private DB_CODEAMAZ db;
            public CMTFunction()
            {
                db = new DB_CODEAMAZ();
            }
            public IQueryable<BINHLUANKHOAHOC> BINHLUANKHOAHOCs
            {
                get { return db.BINHLUANKHOAHOCs; }
            }
            public int Insert(BINHLUANKHOAHOC model)
            {
                BINHLUANKHOAHOC dbEntry = db.BINHLUANKHOAHOCs.Find(model.ID_COMMENT);
                if (dbEntry != null)
                {
                    return 0;

                }
                db.BINHLUANKHOAHOCs.Add(model);
                db.SaveChanges();
                return 1;
            }
            
        }
    }
}