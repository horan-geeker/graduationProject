using MVC4.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace MVC4.Models
{
    public class Admin
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "账号")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string password { get; set; }

        public string created_at { get; set; }
    }

    public class AdminDbContext : DbContext
    {
        public Admin findByName(string username)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.String, (object)username);
            string strSql = "SELECT * FROM admins where username = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new HttpException(404, "Not Found Admin");
            }
            Admin admin = Util.DataSetToList<Admin>(ds)[0];
            return admin;
        }

        public List<Admin> all()
        {
            string strSql = "SELECT * FROM admins";
            DataSet ds = DBConnect.getDataSet(strSql);
            List<Admin> list = Util.DataSetToList<Admin>(ds);
            return list;
        }

        public int create(Admin user)
        {
            string strSql = "insert into admins (username,password) values(?,?)";

            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.String, (object)user.username);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)user.password);
            int result = DBConnect.getInserts(strSql, sqlPara1, sqlPara2);
            return result;
        }

        public Admin find(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM admins where id = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new HttpException(404, "Not Found Meet");
            }
            Admin admin = Util.DataSetToList<Admin>(ds)[0];
            return admin;
        }

        public bool update(int id, Admin admin)
        {
            string strSql = "update admins set username=?,password=? where id = ?";
            //List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.String, (object)admin.username);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)admin.password);
            SQLiteParameter sqlPara3 = new SQLiteParameter(DbType.Int32, (object)id);
            //paraList.Add(sqlPara1);
            int result = DBConnect.getUpdates(strSql, sqlPara1, sqlPara2, sqlPara3);
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool delete(string id)
        {
            string strSql = "delete from admins where id = ?";
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