using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess
{
    public static class ArticleVoteRepository
    {
        public static void SaveArticleVote(ArticleVote a)
        {
            // When a.Id is a Guid.Null, this is a create. else this is a update
            SqlConnection conn = new SqlConnection(Const.ConnString);
            var cmd = new SqlCommand("sp_Save_ArticleVote", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Vote", a.Vote);
            cmd.Parameters.AddWithValue("@UserID", a.UserId);
            cmd.Parameters.AddWithValue("@articleID", a.ArticleId);
           
            try
            {
                conn.Open();
                cmd.ExecuteScalar();
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
            return ;
        }


        public static void DelteArticleVote(ArticleVoteQuery a)
        {
            if (a.UserId.HasValue || a.ArticleId.HasValue)
            {
                var sql = "DELETE FROM ArticleVote WHERE ";
                if (a.UserId.HasValue && a.ArticleId.HasValue)
                { sql += "UserId = '" + a.UserId + "' AND ArticleId = '" + a.ArticleId + "'"; }
                else
                {
                    if (a.UserId.HasValue)
                    { sql += "UserId = '" + a.UserId + "'"; }
                    if (a.ArticleId.HasValue)
                    { sql += "ArticleId = '" + a.ArticleId + "'"; }
                }
                    
               SqlConnection conn = new SqlConnection(Const.ConnString);
                var cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteScalar();
                }
                finally
                {
                    if (conn != null) { conn.Close(); }
                }
            }
            return;
        }

        public static short GetArticleVote(ArticleVoteQuery a)
        {
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ArticleVote WHERE UserId = '" + a.UserId + "' AND ArticleId = '" + a.ArticleId + "'", conn);
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    var vote = (rdr["Vote"] == DBNull.Value) ?(short)0 : (short)rdr["Vote"];
                    return vote;
                }
            }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                if (conn != null) { conn.Close(); }
            }
            return 0;
        }

    }
}


