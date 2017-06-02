using MVC4.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace MVC4.Models
{
    public class MeetUser
    {
        public int meet_id { get; set; }
        public int user_id { get; set; }
        public string signin_status { get; set; }

        [Display(Name = "用户名")]
        public string username { get; set; }

        [Display(Name = "性别")]
        public string gender { get; set; }

        [Display(Name = "用户密码")]
        public string password { get; set; }

        [Display(Name = "最后签到ip")]
        public string last_login_ip { get; set; }

        [Display(Name = "最后签到时间")]
        public string last_login_time { get; set; }
    }

    public class MeetUserDbContext
    {
        public int create(MeetUser pivot)
        {
            string strSql = "insert into meet_users (meet_id,user_id) values(?,?)";

            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.Int32, (object)pivot.meet_id);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.Int32, (object)pivot.user_id);
            int result = DBConnect.getInserts(strSql, sqlPara1, sqlPara2);
            return result;
        }

        public bool signin(MeetUser pivot)
        {
            string strSql = "update meet_users set signin_status=1 where meet_id=? and user_id=?";
            //List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.Int32, (object)pivot.meet_id);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.Int32, (object)pivot.user_id);
            //paraList.Add(sqlPara1);
            int result = DBConnect.getUpdates(strSql, sqlPara1, sqlPara2);
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool delete(int id)
        {
            string strSql = "delete from meet_users where meet_id = ?";
            //List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.Int32, (object)id);
            //paraList.Add(sqlPara1);
            int result = DBConnect.getUpdates(strSql, sqlPara1);
            if (result > 0)
                return true;
            else
                return false;
        }
    }

}