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
    public class User
    {

        public int id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "用户名")]
        public string username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "用户密码")]
        public string password { get; set; }

        [Display(Name = "性别")]
        public string gender { get; set; }

        [Display(Name = "最后签到ip")]
        public string last_login_ip { get; set; }

        [Display(Name = "最后签到时间")]
        public string last_login_time { get; set; }

        public string created_at { get; set; }
    }

    public class UserDbContext
    {
        public User findByName(string username)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.String, (object)username);
            string strSql = "SELECT * FROM users where username = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new HttpException(404, "Not Found User");
            }
            User user = Util.DataSetToList<User>(ds)[0];
            return user;
        }

        public List<User> all()
        {
            string strSql = "SELECT * FROM users";
            DataSet ds = DBConnect.getDataSet(strSql);
            List<User> list = Util.DataSetToList<User>(ds);
            return list;
        }

        public int create(User user)
        {
            string strSql = "insert into users (username,password,gender,last_login_ip) values(?,?,?,?)";

            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.String, (object)user.username);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)user.password);
            SQLiteParameter sqlPara3 = new SQLiteParameter(DbType.String, (object)user.gender);
            SQLiteParameter sqlPara4 = new SQLiteParameter(DbType.String, (object)System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            int result = DBConnect.getInserts(strSql, sqlPara1, sqlPara2, sqlPara3, sqlPara4);
            return result;
        }

        public User find(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM users where id = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new HttpException(404, "Not Found Meet");
            }
            User user = Util.DataSetToList<User>(ds)[0];
            return user;
        }

        public bool update(int id, User user)
        {
            string strSql = "update users set username=?,password=?,gender=? where id = ?";
            //List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.String, (object)user.username);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)user.password);
            SQLiteParameter sqlPara3 = new SQLiteParameter(DbType.String, (object)user.gender);
            SQLiteParameter sqlPara4 = new SQLiteParameter(DbType.Int32, (object)id);
            //paraList.Add(sqlPara1);
            int result = DBConnect.getUpdates(strSql, sqlPara1, sqlPara2, sqlPara3, sqlPara4);
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool delete(string id)
        {
            string strSql = "delete from users where id = ?";
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