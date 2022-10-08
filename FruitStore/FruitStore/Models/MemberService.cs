using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//才可以用data table
using System.Data;
//才可以用資料庫相關連線字串
using System.Data.SqlClient;

namespace FruitStore.Models
{
    public class MemberService
    {
        /// <summary>
        /// 取得資料庫連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        /// <summary>
        /// 將會員資料加到資料庫中
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public void AddBook(Models.BookData Data)
        {
            string sql = @" INSERT INTO Members(
                            MemberName, MemberEmail, MemberAddress, MemberPhone) 
			                VALUES(
                            @MemberName, @MemberEmail, @MemberAddress, @MemberPhone);";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", Data.BookName));
                cmd.Parameters.Add(new SqlParameter("@ClassId", Data.ClassId));
                cmd.Parameters.Add(new SqlParameter("@Author", Data.BookAuthor));
                cmd.Parameters.Add(new SqlParameter("@BoughtDate", Convert.ToDateTime(Data.BoughtDate)));
                cmd.Parameters.Add(new SqlParameter("@Publisher", Data.Publisher));
                cmd.Parameters.Add(new SqlParameter("@BookNote", Data.BookNote));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", "A"));
                cmd.Parameters.Add(new SqlParameter("@BooKeeper", ""));
                cmd.Parameters.Add(new SqlParameter("@BoughtFee", 10000));
                cmd.Parameters.Add(new SqlParameter("@CreateDate", Today));
                cmd.Parameters.Add(new SqlParameter("@CreateUser", User));
                cmd.Parameters.Add(new SqlParameter("@ModifyDate", Today));
                cmd.Parameters.Add(new SqlParameter("@ModifyUser", User));

                SqlTransaction Transaction = conn.BeginTransaction();
                cmd.Transaction = Transaction;

                try
                {
                    cmd.ExecuteNonQuery();
                    Transaction.Commit();
                }
                catch
                {
                    Transaction.Rollback();
                    throw;//用來重新擲回 catch 陳述式攔截到的例外狀況
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}