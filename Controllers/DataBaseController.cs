using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using System.Reflection;
using System.Data.SQLite;
using System.Text;
using System.Linq;
using System.Configuration;
using MVC4.Models;
using MVC4.Utils;

namespace MVC4.Controllers
{
     // [ExceptionFilter]
    public class DataBaseController : Controller
    {
        public List<Meet> getMeets()
        {
            string strSql = "SELECT * FROM meets";
            DataSet ds = DBConnect.getDataSet(strSql);
            List<Meet> list = Util.DataSetToList<Meet>(ds);
            return list;
        }

        public int createMeets(Meet meet)
        {
            string strSql = "insert into meets (title,description,begin_at,created_at) values(?,?,?,?)";

            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.String, (object)meet.title);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)meet.description);
            SQLiteParameter sqlPara3 = new SQLiteParameter(DbType.DateTime, (object)meet.begin_at);
            SQLiteParameter sqlPara4 = new SQLiteParameter(DbType.DateTime, (object)DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss"));
            int result = DBConnect.getInserts(strSql, sqlPara1, sqlPara2, sqlPara3, sqlPara4);
            return result;
        }
    }
}