using MVC4.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace MVC4.Models
{
    public class Question
    {
        public int id { get; set; }
        public int questionnaire_id { get; set; }
        // public int question_seq { get; set; }
        public int type_id { get; set; }
        //1 单选题
        //2 问答题
        //3 论述题
        public string question_content { get; set; }
        public int is_required { get; set; }
        //1 必答
        //0 不必答
        public List<QuestionChoice> questionChoices;

        public List<Answer> answers;
    }

    public class QuestionDbContext
    {

        public List<QuestionChoice> questionChoices(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM question_choices where question_id = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            List<QuestionChoice> list = Util.DataSetToList<QuestionChoice>(ds);
            return list;
        }

        public int create(Question que)
        {
            string strSql = "insert into questions (questionnaire_id,question_content,type_id,is_required) values(?,?,?,?)";

            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.Int32, (object)que.questionnaire_id);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)que.question_content);
            SQLiteParameter sqlPara3 = new SQLiteParameter(DbType.String, (object)que.type_id);
            SQLiteParameter sqlPara4 = new SQLiteParameter(DbType.String, (object)que.is_required);
            int result = DBConnect.getInserts(strSql, sqlPara1, sqlPara2, sqlPara3, sqlPara4);
            return result;
        }

        public Question find(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM questions where id = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new HttpException(404, "Not Found Meet");
            }
            Question question = Util.DataSetToList<Question>(ds)[0];
            return question;
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


