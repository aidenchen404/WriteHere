using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess
{
    public static class HallOfFameRepository
    {
        public static List<HallOfFame> GetHallOfFameList()
        {
            var sql = "SELECT * FROM dbo.vwHallOfFame";
           
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
         
            var list = new List<HallOfFame>();
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var a = ReadRow(rdr);
                    list.Add(a);
                }
            }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                if (conn != null) { conn.Close(); }
            }
            return list;
        }

        private static HallOfFame ReadRow(SqlDataReader rdr)
        {
            var a = new HallOfFame();
            // get the results of each column

            a.HallOfFameType = (rdr["HallOfFameType"] == DBNull.Value) ? string.Empty : rdr["HallOfFameType"].ToString();
            a.ArticleId = (rdr["ArticleId"] == DBNull.Value) ? Guid.Empty:(Guid)rdr["ArticleId"];
            a.AuthorUserId = (Guid)rdr["AuthorUserId"];
            a.ArticleTitle = (rdr["ArticleTitle"] == DBNull.Value) ? string.Empty : rdr["ArticleTitle"].ToString();
            a.PersonName = (rdr["PenName"] == DBNull.Value) ? string.Empty : rdr["PenName"].ToString();
            a.RankCount = (rdr["RankCount"] == DBNull.Value) ? (int)0 : (int)rdr["RankCount"];
            a.RowNumber = (rdr["RowNumber"] == DBNull.Value) ?(long) 0 : (long)rdr["RowNumber"];
           return a;
        }
    }
}


