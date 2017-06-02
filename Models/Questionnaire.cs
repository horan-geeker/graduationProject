using MVC4.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace MVC4.Models
{
    public class Questionnaire
    {
        public int id { get; set; }

        public string meet_id { get; set; }

        public string title { get; set; }

        public string direction { get; set; }

        public string end_at { get; set; }

        public string type { get; set; }

        public List<Question> questions;
    }

    public class QuestionnaireDbContext
    {

        public Questionnaire findByMeet(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM questionnaires where meet_id = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new HttpException(404, "Not Found Questionnaire");
            }
            Questionnaire que = Util.DataSetToList<Questionnaire>(ds)[0];
            return que;
        }

        public List<Question> questions(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM questions where questionnaire_id=?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            List<Question> list = Util.DataSetToList<Question>(ds);
            return list;
        }

        public List<Questionnaire> all()
        {
            string strSql = "SELECT * FROM questionnaires";
            DataSet ds = DBConnect.getDataSet(strSql);
            List<Questionnaire> list = Util.DataSetToList<Questionnaire>(ds);
            return list;
        }

        public int create(Questionnaire que)
        {
            string strSql = "insert into questionnaires (meet_id,title,direction,end_at,type) values(?,?,?,?,?)";

            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.Int32, (object)que.meet_id);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)que.title);
            SQLiteParameter sqlPara3 = new SQLiteParameter(DbType.String, (object)que.direction);
            SQLiteParameter sqlPara4 = new SQLiteParameter(DbType.String, (object)que.end_at);
            SQLiteParameter sqlPara5 = new SQLiteParameter(DbType.String, (object)que.type);
            int result = DBConnect.getInserts(strSql, sqlPara1, sqlPara2, sqlPara3, sqlPara4, sqlPara5);
            return result;
        }

        public Questionnaire find(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM questionnaires where id = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new HttpException(404, "Not Found Questionnaire");
            }
            Questionnaire que = Util.DataSetToList<Questionnaire>(ds)[0];
            return que;
        }


        public bool delete(int id)
        {
            string strSql = "delete from questionnaires where id = ?";
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