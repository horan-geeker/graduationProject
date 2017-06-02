using MVC4.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Models
{
    public class Meet
    {
        public int id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "会议主题")]
        public string title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "会议内容")]
        public string description { get; set; }

        [Required]
        [Display(Name = "会议开始时间")]
        public string begin_at { get; set; }

        [Display(Name = "会议创建时间")]
        public string created_at { get; set; }

    }

    public class MeetDbContext
    {
        public List<MeetUser> users(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT users.*,meet_users.* FROM users left join meet_users on meet_id=? where users.id=meet_users.user_id";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            List<MeetUser> list = Util.DataSetToList<MeetUser>(ds);
            return list;
        }

        public List<Meet> all()
        {
            string strSql = "SELECT * FROM meets";
            DataSet ds = DBConnect.getDataSet(strSql);
            List<Meet> list = Util.DataSetToList<Meet>(ds);
            return list;
        }

        public int create(Meet meet)
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

        public Meet find(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM meets where id = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if(ds.Tables[0].Rows.Count == 0)
            {
                throw new HttpException(404,"Not Found Meet");
            }
            Meet meet = Util.DataSetToList<Meet>(ds)[0];
            return meet;
        }

        public bool update(int id, Meet meet)
        {
            string strSql = "update meets set title=?,description=?,begin_at=? where id = ?";
            //List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.String, (object)meet.title);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)meet.description);
            SQLiteParameter sqlPara3 = new SQLiteParameter(DbType.DateTime, (object)meet.begin_at);
            SQLiteParameter sqlPara4 = new SQLiteParameter(DbType.Int32, (object)id);
            //paraList.Add(sqlPara1);
            int result = DBConnect.getUpdates(strSql, sqlPara1, sqlPara2, sqlPara3, sqlPara4);
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool delete(int id)
        {
            string strSql = "delete from meets where id = ?";
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