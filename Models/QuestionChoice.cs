using MVC4.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace MVC4.Models
{
    public class QuestionChoice
    {
        public int id { get; set; }

        public int question_id { get; set; }

        public string choice_tag { get; set; }

        public string choice_content { get; set; }

        // public int is_writed { get; set; }

        public int count = 0;
    }

    public class QuestionChoiceDbContext
    {

        public List<Meet> all()
        {
            string strSql = "SELECT * FROM questionnaires";
            DataSet ds = DBConnect.getDataSet(strSql);
            List<Meet> list = Util.DataSetToList<Meet>(ds);
            return list;
        }

        public int create(QuestionChoice choice)
        {
            string strSql = "insert into question_choices (question_id,choice_tag,choice_content) values(?,?,?)";

            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.Int32, (object)choice.question_id);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)choice.choice_tag);
            SQLiteParameter sqlPara3 = new SQLiteParameter(DbType.String, (object)choice.choice_content);
            // SQLiteParameter sqlPara4 = new SQLiteParameter(DbType.String, (object)choice.is_writed);
            int result = DBConnect.getInserts(strSql, sqlPara1, sqlPara2, sqlPara3);
            return result;
        }

        public List<QuestionChoice> findByQuestion(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM question_choices where question_id = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new HttpException(404, "Not Found Meet");
            }
            List<QuestionChoice> questionChoices = Util.DataSetToList<QuestionChoice>(ds);
            return questionChoices;
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