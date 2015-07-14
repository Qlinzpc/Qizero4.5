using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common
{
    /// <summary>
    /// 2014年8月27日09:24:57 
    /// 数据库帮助类 
    /// zpc 
    /// 
    /// </summary>
    public abstract class SQLHelper
    {
        /// <summary>
        /// 连接字符串 
        /// </summary>
        // public static string ConnectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public static string ConnectionString = "Data Source=.;Initial Catalog=Qlin;Integrated Security=True;MultipleActiveResultSets=True";

        /// <summary>
        /// 执行 增删改 操作 
        /// </summary>
        /// <param name="sql"> sql 语句 </param>
        /// <param name="commandType"> 命令类型 </param>
        /// <param name="paras"> 命令参数 </param>
        /// <returns> 受影响的行数 </returns>
        public static int ExecuteNonQuery(string sql, CommandType commandType = CommandType.Text,
            params SqlParameter[] paras)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    var cmd = new SqlCommand();

                    PrepareCommand(cmd, conn, null, sql, commandType, paras);

                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format(@"错误信息: {0} 
sql: {1} 
参数: {2}", ex.Message, sql, paras.ToList().ListToString()));
                    return -1;
                }
            }

        }

        /// <summary>
        /// 执行 增删改 操作 
        /// </summary>
        /// <param name="sql"> sql 语句 </param>
        /// <param name="commandType"> 命令类型 </param>
        /// <param name="trans"> 事务对象 </param>
        /// <param name="paras"> 命令参数 </param>
        /// <returns> 受影响的行数 </returns>
        public static int ExecuteNonQuery(string sql, CommandType commandType,
            SqlTransaction trans, params SqlParameter[] paras)
        {
            try
            {
                var cmd = new SqlCommand();

                PrepareCommand(cmd, trans.Connection, trans, sql, commandType, paras);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format(@"错误信息: {0} 
sql: {1} 
参数: {2}", ex.Message, sql, paras.ToList().ListToString()));
                return -1;
            }

        }

        /// <summary>
        /// 执行查询操作 
        /// </summary>
        /// <param name="sql"> sql语句 </param>
        /// <param name="commandType"> 命令类型 </param>
        /// <param name="paras"> 命令参数 </param>
        /// <returns> SqlDataReader 读取对象 </returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType commandType = CommandType.Text,
            params SqlParameter[] paras)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var cmd = new SqlCommand();

                PrepareCommand(cmd, conn, null, sql, commandType, paras);

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format(@"错误信息: {0} 
sql: {1} 
参数: {2}", ex.Message, sql, paras.ToList().ListToString()));
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行查询操作 
        /// </summary>
        /// <param name="sql"> sql语句 </param>
        /// <param name="commandType"> 命令类型 </param>
        /// <param name="paras"> 命令参数 </param>
        /// <returns> DataSet 数据集对象 </returns>
        public static DataSet ExecuteDataSet(string sql, CommandType commandType = CommandType.Text,
            params SqlParameter[] paras)
        {
            var conn = new SqlConnection(ConnectionString);
            var ds = new DataSet();
            try
            {
                var cmd = new SqlCommand();

                PrepareCommand(cmd, conn, null, sql, commandType, paras);
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                cmd.Dispose();
                return ds;

            }
            catch (Exception ex)
            {
                Log.Error(string.Format(@"错误信息: {0} 
sql: {1} 
参数: {2}", ex.Message, sql, paras.ToList().ListToString()));
                return ds;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行查询单个值操作 
        /// </summary>
        /// <param name="sql"> sql语句 </param>
        /// <param name="commandType"> 命令类型 </param>
        /// <param name="paras"> 命令参数 </param>
        /// <returns> object 对象 </returns>
        public static object ExecuteScalar(string sql, CommandType commandType = CommandType.Text
            , params SqlParameter[] paras)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var cmd = new SqlCommand();

                    PrepareCommand(cmd, conn, null, sql, commandType, paras);

                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format(@"错误信息: {0} 
sql: {1} 
参数: {2}", ex.Message, sql, paras.ToList().ListToString()));
                return null;
            }
        }

        /// <summary>
        /// 准备 SQL 执行命令 
        /// </summary>
        /// <param name="cmd"> 命令对象 </param>
        /// <param name="conn"> 连接对象 </param>
        /// <param name="trans"> 事务对象 </param>
        /// <param name="sql"> 执行sql语句 </param>
        /// <param name="commandType"> 执行命令类型 </param>
        /// <param name="paras"> 命令参数 </param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string sql,
            CommandType commandType, params SqlParameter[] paras)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandTimeout = 10000000;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = commandType;

            if (paras != null)
                cmd.Parameters.AddRange(paras);
        }

        /// <summary>
        /// 验证是否连接成功 
        /// </summary>
        public static void IsOpen()
        {
            var conn = new SqlConnection(SQLHelper.ConnectionString);
            if (conn.State != ConnectionState.Open) conn.Open();
        }

    }
}
