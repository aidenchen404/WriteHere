using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess
{
    public static class ArticleCommentRepository
    {
       
        public static ArticleComment SaveArticleComment(ArticleComment a)
        {
            // When a.Id is a Guid.Null, this is a create. else this is a update
            SqlConnection conn = new SqlConnection(Const.ConnString);
            var cmd = new SqlCommand("sp_Save_ArticleComment", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.Parameters.AddWithValue("@comment", a.Comment);
            cmd.Parameters.AddWithValue("@commentOwnerID", a.CommentOwnerId);
            cmd.Parameters.AddWithValue("@articleID", a.ArticleId);
            cmd.Parameters.AddWithValue("@showOwner", a.ShowOwner);


            try
            {
                conn.Open();
                var retId = (Guid)cmd.ExecuteScalar();
                a.Id = retId;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
            return a;
        }


        public static void DelteArticleComment(Guid id)
        {
            if (id == null || id == Guid.Empty) return;

            SqlConnection conn = new SqlConnection(Const.ConnString);
            var cmd = new SqlCommand("DELETE FROM ArticleComment WHERE id ='" + id + "'", conn);

            try
            {
                conn.Open();
                cmd.ExecuteScalar();
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return;
        }

        public static ArticleComment GetArticleComment(Guid id)
        {
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.vwArticleCommentList WHERE ID ='" + id + "' ORDER BY CommentDateTime Desc", conn);
            ArticleComment a = null;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                   a = ReadRow(rdr);
                }
            }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                if (conn != null) { conn.Close(); }
            }
            return a;
        }

        public static List<ArticleComment> GetArticleCommentListByQuery(ArticleCommentQuery  query)
        {
            var sql = "SELECT * FROM dbo.[vwArticleCommentList]";
            if (query.UserId.HasValue
                  || query.ArticleId.HasValue)
             {
                sql += " WHERE ";
                if (query.UserId.HasValue) { sql += " CommentOwnerID = @u"; }
                if (query.ArticleId.HasValue) { sql += " ArticleId = @ar"; }
             }

            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (query.UserId.HasValue) { cmd.Parameters.AddWithValue("@u", query.UserId); }
            if (query.ArticleId.HasValue) { cmd.Parameters.AddWithValue("@ar", query.ArticleId); }

            var list = new List<ArticleComment>();
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
       
     
        private static ArticleComment ReadRow(SqlDataReader rdr)
        {
            var a = new ArticleComment();
            // get the results of each column
            a.Id = (Guid)rdr["ID"];
            a.Comment = (rdr["Comment"] == DBNull.Value) ? string.Empty : rdr["Comment"].ToString();
            a.CommentOwnerId = (Guid)rdr["CommentOwnerId"];
            a.CommentOwnerPenName = (rdr["CommentOwnerPenName"] == DBNull.Value) ? string.Empty : rdr["CommentOwnerPenName"].ToString();
            a.ArticleTitle = (rdr["ArticleTitle"] == DBNull.Value) ? string.Empty : rdr["ArticleTitle"].ToString();
            a.ShowOwner = (bool)rdr["ShowOwner"];
            a.CommentOwnerShowProfile = (bool) rdr["CommentOwnerShowProfile"];
            a.CommentDate = (rdr["CommentDateTime"] == DBNull.Value) ? string.Empty : ((DateTime)rdr["CommentDateTime"]).ToString("yyyy-MM-dd");
            return a;
        }
    }
}


