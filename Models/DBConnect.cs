using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace MVC4.Models
{
    #region 封装与数据库的连接和访问的类
    /// <summary>
    /// 封装与数据库的连接和访问的类
    /// </summary>
    public static class DBConnect
    {

        public static string sqlCon = ConfigurationManager.ConnectionStrings["DefaultDBContext"].ToString();

        private static SQLiteConnection conn;
        #region 1 连接通道 属性
        /// <summary>
        /// 连接通道 属性
        /// </summary>
        public static SQLiteConnection Conn
        {
            get
            {
                if (conn == null || conn.State == ConnectionState.Broken)
                {
                    conn = new SQLiteConnection(sqlCon);

                }
                return conn;
            }
        }
        #endregion

        public static void openCon()
        {
            conn.Open();
        }
        public static void closeCon()
        {
            conn.Close();
        }

        public static int getUpdates(string sqlStr, params SQLiteParameter[] paras)
        {
            SQLiteCommand cmd = new SQLiteCommand(sqlStr, Conn);
            if (paras != null && paras.Length > 0)
            {
                cmd.Parameters.AddRange(paras);
            }
            int res = 0;
            try
            {
                openCon();
                res = cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                closeCon();
            }
            return res;
        }

        public static int getInserts(string sqlStr, params SQLiteParameter[] paras)
        {
            int res = -1;
            SQLiteCommand sqlCom = new SQLiteCommand(sqlStr, Conn);
            if (paras != null && paras.Length > 0)
            {
                sqlCom.Parameters.AddRange(paras);//为命令对象添加参数数组
            }
            try
            {
                openCon();
               res = sqlCom.ExecuteNonQuery();
                SQLiteCommand sqlCom2 = new SQLiteCommand("select last_insert_rowid();", Conn);
                res = Convert.ToInt32(sqlCom2.ExecuteScalar());
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                closeCon();
            }
            return res;
        }

        #region 2获取数据表
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="sqlStr">查询语句</param>
        /// <param name="paras">参数数组</param>
        /// <returns>结果表</returns>
        public static DataTable getDataTable(string sqlStr, params SQLiteParameter[] paras)
        {
            //select * from U where ic>@naaaa
            //创建命令对象(查询语句，连接通道的属性)
            SQLiteCommand cmd = new SQLiteCommand(sqlStr, Conn);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (paras != null && paras.Length > 0)
            {
                cmd.Parameters.AddRange(paras);//为命令对象添加参数数组
            }

            try
            {
                openCon();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                closeCon();
            }
            return dt;
        }
        #endregion

        //返回一行一列的值
        public static object ExecuteScalare(string sql, params SQLiteParameter[] paras)
        {
            int res = 0;
            SQLiteCommand sqlCom = new SQLiteCommand(sql, Conn);
            if (paras != null)
            {
                sqlCom.Parameters.AddRange(paras);
            }
            try
            {
                openCon();
                res = Convert.ToInt32(sqlCom.ExecuteScalar());
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                closeCon();
            }
            return res;
        }

        public static object getScalare(string sql, params SQLiteParameter[] paras)
        {
            string res = "";
            SQLiteCommand sqlCom = new SQLiteCommand(sql, Conn);
            if (paras != null)
            {
                sqlCom.Parameters.AddRange(paras);
            }
            try
            {
                openCon();
                res = sqlCom.ExecuteScalar().ToString();
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                closeCon();
            }
            return res;
        }

        public static DataSet getDataSet(string sqlStr)
        {

            DataSet ds = new DataSet();
            SQLiteDataAdapter da = new SQLiteDataAdapter(sqlStr, Conn);
            try
            {
                openCon();
                da.Fill(ds);
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                closeCon();
            }
            return ds;
        }

        public static DataSet getDataSet(string sqlStr, params SQLiteParameter[] paras)
        {
            DataSet ds = new DataSet();
            SQLiteCommand sqlCom = new SQLiteCommand(sqlStr, Conn);
            if (paras != null)
            {
                sqlCom.Parameters.AddRange(paras);
            }
            try
            {
                openCon();
                SQLiteDataAdapter da = new SQLiteDataAdapter(sqlCom);
                da.Fill(ds);
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            finally
            {
                closeCon();
            }
            return ds;
        }
    }
    #endregion
}