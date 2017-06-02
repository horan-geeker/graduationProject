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
    public class Answer
    {
        public int id { get; set; }

        [Required]
        public int questionnaire_id { get; set; }

        [Required]
        public int user_id { get; set; }

        [Required]
        public int question_id { get; set; }

        [Required]
        public string content { get; set; }

        public User user;
    }

    public class AnswerDbContext : DbContext
    {

        public List<Answer> findByQuestion(int id)
        {
            SQLiteParameter par1 = new SQLiteParameter(DbType.Int32, (object)id);
            string strSql = "SELECT * FROM answers where question_id = ?";
            DataSet ds = DBConnect.getDataSet(strSql, par1);
            if (ds.Tables[0].Rows.Count == 0)
            {
               throw new HttpException(404, "Not Found Answer");
            }
            List<Answer> ans = Util.DataSetToList<Answer>(ds);
            return ans;
        }

        public int create(Answer answer)
        {
            string strSql = "insert into answers (questionnaire_id, user_id, question_id, content) values(?,?,?,?)";

            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            SQLiteParameter sqlPara1 = new SQLiteParameter(DbType.String, (object)answer.questionnaire_id);
            SQLiteParameter sqlPara2 = new SQLiteParameter(DbType.String, (object)answer.user_id);
            SQLiteParameter sqlPara3 = new SQLiteParameter(DbType.String, (object)answer.question_id);
            SQLiteParameter sqlPara4 = new SQLiteParameter(DbType.String, (object)answer.content);
            int result = DBConnect.getInserts(strSql, sqlPara1, sqlPara2, sqlPara3, sqlPara4);
            return result;
        }

    }
}