using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace MVC4.Utils
{
    public static class Util
    {
        public static long DateTimeToStamp(DateTime datetime)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            return Convert.ToInt64((datetime - dateStart).TotalSeconds);
        }

        public static DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan((lTime));
            return dateStart.Add(toNow);
        }

        public static List<T> DataSetToList<T>(DataSet ds)
            where T : class, new()
        {
            List<T> list = null;

            try
            {
                Type type = typeof(T);

                list = new List<T>();
                ds.Tables[0].TableName = type.Name;
                foreach (DataRow r in ds.Tables[type.Name].Rows)
                {
                    T f = new T();

                    foreach (PropertyInfo p in type.GetProperties())
                    {
                        object obj = null;
                        if (r[p.Name] != Convert.DBNull)
                        {
                            obj = Convert.ChangeType(r[p.Name], p.PropertyType);
                        }
                        else
                        {
                            // obj = "";
                            obj = null;
                        }

                        p.SetValue(f, obj, null);
                    }

                    list.Add(f);
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            { }

            return list;
        }

        public static DataSet ListToDataSet<T>(List<T> ls)
            where T : class, new()
        {
            DataSet ds = null;

            try
            {
                Type type = typeof(T);
                ds = new DataSet();
                DataTable dt = new DataTable(type.Name);
                ds.Tables.Add(dt);

                foreach (PropertyInfo p in type.GetProperties())
                {
                    DataColumn dc = new DataColumn(p.Name, p.PropertyType);
                    ds.Tables[type.Name].Columns.Add(dc);
                }

                foreach (T t in ls)
                {
                    DataRow dr = ds.Tables[type.Name].NewRow();
                    foreach (PropertyInfo p in type.GetProperties())
                    {
                        dr[p.Name] = p.GetValue(t, null);
                    }
                    ds.Tables[type.Name].Rows.Add(dr);
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            { }

            return ds;
        }

        public static List<T> DataTableToList<T>(DataTable dt)
           where T : class, new()
        {
            List<T> list = null;

            try
            {
                Type type = typeof(T);

                list = new List<T>();
                dt.TableName = type.Name;
                foreach (DataRow r in dt.Rows)
                {
                    T f = new T();

                    foreach (PropertyInfo p in type.GetProperties())
                    {
                        object obj = null;
                        if (r[p.Name] != Convert.DBNull)
                        {
                            obj = Convert.ChangeType(r[p.Name], p.PropertyType);
                        }
                        else
                        {
                            //obj = "";
                            obj = null;
                        }

                        p.SetValue(f, obj, null);
                    }

                    list.Add(f);
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            { }

            return list;
        }

        public static DataTable ListToDataTable<T>(List<T> ls)
            where T : class, new()
        {
            DataTable dt = null;

            try
            {
                Type type = typeof(T);
                dt = new DataTable(type.Name);

                foreach (PropertyInfo p in type.GetProperties())
                {
                    DataColumn dc = new DataColumn(p.Name, p.PropertyType);
                    dt.Columns.Add(dc);
                }

                foreach (T t in ls)
                {
                    DataRow dr = dt.NewRow();
                    foreach (PropertyInfo p in type.GetProperties())
                    {
                        dr[p.Name] = p.GetValue(t, null);
                    }
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            { }

            return dt;
        }

        public static string getMd5(string input)//liule
        {

            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}