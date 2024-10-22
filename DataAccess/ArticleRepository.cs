using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Model;


namespace DataAccess
{
    public static class ArticleRepository
    {
        public static void IncreaseArticleViewedCount(Guid id)
        {
            SqlConnection conn = new SqlConnection(Const.ConnString);
            var cmd = new SqlCommand("UPDATE Article SET ViewedCount =ViewedCount+1 WHERE Id = '"+ id.ToString() + "'", conn) ;
          
            try
            {
                conn.Open();
                cmd.ExecuteScalar();
                
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
            return ;
        }

        public static Article SaveArticle(Article a)
        {
            // When a.Id is a Guid.Null, this is a create. else this is a update
            SqlConnection conn = new SqlConnection(Const.ConnString);
            var cmd = new SqlCommand("sp_Save_Article", conn) {
                CommandType = System.Data.CommandType.StoredProcedure
             };
            
            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.Parameters.AddWithValue("@title", a.Title);
            cmd.Parameters.AddWithValue("@subtitle", a.Subtitle);
            cmd.Parameters.AddWithValue("@content", a.Content);
            cmd.Parameters.AddWithValue("@abstract", a.Summary );
            cmd.Parameters.AddWithValue("@authorUserID", a.AuthorUserId);
           
            cmd.Parameters.AddWithValue("@editorReviewNote", a.EditorReviewNote);
            cmd.Parameters.AddWithValue("@genreId", a.GenreId);
            try
            {
                conn.Open();
                var retId = (Guid)cmd.ExecuteScalar();
                a.Id = retId;
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
            return a;
        }


        public static void DeleteArticle(Guid id)
        {
            if (id == null || id == Guid.Empty) return;
            
            SqlConnection conn = new SqlConnection(Const.ConnString);
            var cmdStatus = new SqlCommand("DELETE FROM ArticleStatusChangeHistory WHERE ArticleID ='" + id + "'", conn);
            var cmdComment = new SqlCommand("DELETE FROM ArticleComment WHERE ArticleID ='" + id + "'", conn);
            var cmdVote = new SqlCommand("DELETE FROM ArticleVote WHERE ArticleID ='" + id + "'", conn);
            var cmdAssignment = new SqlCommand("DELETE FROM ArticleAssignment WHERE ArticleID ='" + id + "'", conn);
            
            var cmd = new SqlCommand("DELETE FROM Article WHERE id ='" + id + "'", conn);
         
            try
            {
                conn.Open();
                cmdStatus.ExecuteScalar();
                cmdComment.ExecuteScalar();
                cmdVote.ExecuteScalar();
                cmdAssignment.ExecuteScalar();
                cmd.ExecuteScalar();
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
 
            return;
        }
     
            public static Article GetArticle(Guid id)
        {
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.vwArticleList WHERE ID ='" + id + "'", conn);
            Article a = null;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                   a = ReadOneArticle(rdr);
                }
            }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                if (conn != null) { conn.Close(); }
            }
            return a;
        }
        public static List<Guid> GetArticleVotedUpBy(Guid userId)
        {
           var sql = "SELECT ArticleID FROM dbo.ArticleVote where vote =1 AND userId = '" + userId +"'";
          
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            
            var list = new List<Guid>();
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var a =  (Guid)rdr["ArticleID"]; ;
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
        public static List<Guid> GetArticleCommentedBy(Guid userId)
        {
            var sql = "SELECT ArticleID FROM dbo.ArticleComment where CommentOwnerID = '" + userId + "'";

            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            var list = new List<Guid>();
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var a = (Guid)rdr["ID"]; ;
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


        public static List<ArticleRow> GetArticleRowList(ArticleQuery query)
        {
            var sql = "SELECT * FROM dbo.vwArticleList";
            if (query.AuthorUserId.HasValue 
                || query.EditorUserId.HasValue 
                || !string.IsNullOrEmpty(query.StatusName)
                || !string.IsNullOrEmpty(query.Genre)
                )
            {
                sql += " WHERE ";
                if (query.AuthorUserId.HasValue) { sql += " AuthorUserId = @au"; }
                if (query.EditorUserId.HasValue) { sql += " EditorUserId = @eu"; }
                if (!string.IsNullOrEmpty(query.StatusName)) { sql += " ArticleStatus = @s"; }
                if (!string.IsNullOrEmpty(query.Genre)) { sql += " Genre = @g"; }
            }
            sql += " ORDER BY Title ";
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (query.AuthorUserId.HasValue) { cmd.Parameters.AddWithValue("@au", query.AuthorUserId.Value); }
            if (query.EditorUserId.HasValue) { cmd.Parameters.AddWithValue("@eu", query.EditorUserId.Value); }
            if (!string.IsNullOrEmpty(query.StatusName)) { cmd.Parameters.AddWithValue("@s", query.StatusName); }
            if (!string.IsNullOrEmpty(query.Genre)) { cmd.Parameters.AddWithValue("@g", query.Genre); }
            
            var retList = new List<ArticleRow>();
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                var list = new List<ArticleRow>();
                while (rdr.Read())
                {
                    var a = ReadOneArticleRow(rdr);
                    list.Add(a);
                }

                // contine filter
                if (query.VotedUpByUserId.HasValue ||
               query.CommentedByUserId.HasValue)
                {
                    var InGuids = new List<Guid>();
                    if (query.VotedUpByUserId.HasValue)
                    { InGuids = GetArticleVotedUpBy(query.VotedUpByUserId.Value); }
                    if (query.CommentedByUserId.HasValue)
                    { InGuids = GetArticleCommentedBy(query.VotedUpByUserId.Value); }

                    retList = list.Where(x => InGuids.Contains(x.Id)).ToList();
                }
                else
                { retList = list; }
            }
            finally
            {
                if (rdr != null) { rdr.Close(); }
                if (conn != null) { conn.Close(); }
            }

            return retList;
        }

        private static Article ReadOneArticle(SqlDataReader rdr)
        {
            var a = new Article();
            // get the results of each column
            a.Id = (Guid)rdr["ID"];
            a.AuthorUserId = (Guid)rdr["AuthorUserId"];
            a.AuthorPenName = (rdr["AuthorPenName"] == DBNull.Value) ? string.Empty : rdr["AuthorPenName"].ToString();
            a.AuthorIsPublicProfile = (bool)rdr["AuthorIsPublicProfile"];
            a.Title = (string)rdr["Title"];
            a.Subtitle = (rdr["Subtitle"] == DBNull.Value) ? string.Empty : rdr["Subtitle"].ToString();
            a.Content = (rdr["Content"] == DBNull.Value) ? string.Empty : rdr["Content"].ToString();
            a.Summary = (rdr["Abstract"] == DBNull.Value) ? string.Empty : rdr["Abstract"].ToString();
            a.ArticleStatus = (rdr["ArticleStatus"] == DBNull.Value) ? "Unsubmitted" : rdr["ArticleStatus"].ToString();

            a.ViewedCount = (rdr["ViewedCount"] == DBNull.Value) ? 0 : (int)rdr["ViewedCount"];
            a.UpVote = (rdr["ArticleThumbUpCount"] == DBNull.Value) ? 0 : (int)rdr["ArticleThumbUpCount"];
            a.DownVote = (rdr["ArticleThumbDownCount"] == DBNull.Value) ? 0 : (int)rdr["ArticleThumbDownCount"];
            a.CommentCount = (rdr["ArticleCommentCount"] == DBNull.Value) ? 0 : (int)rdr["ArticleCommentCount"];

            a.EditorUserId = (rdr["EditorUserId"] == DBNull.Value) ? (Guid?)null : (Guid?)rdr["EditorUserId"];
            a.EditorUserName = (rdr["EditorUserName"] == DBNull.Value) ? string.Empty : (string)rdr["EditorUserName"];
            a.EditorAssignedDate = (rdr["EditorAssignedDate"] == DBNull.Value) ? (DateTime?)null : (DateTime?)rdr["EditorAssignedDate"];
            a.EditorReviewNote = (rdr["EditorReviewNote"] == DBNull.Value) ? string.Empty : (string)rdr["EditorReviewNote"];

            a.TutorUserId = (rdr["TutorUserId"] == DBNull.Value) ? (Guid?)null : (Guid?)rdr["TutorUserId"];
            a.TutorUserName = (rdr["TutorUserName"] == DBNull.Value) ? string.Empty : (string)rdr["TutorUserName"];
            a.TutorAssignedDate = (rdr["TutorAssignedDate"] == DBNull.Value) ? (DateTime?)null : (DateTime?)rdr["TutorAssignedDate"];
            
            a.DrawerUserId = (rdr["DrawerUserId"] == DBNull.Value) ? (Guid?)null : (Guid?)rdr["DrawerUserId"];
            a.DrawerUserName = (rdr["DrawerUserName"] == DBNull.Value) ? string.Empty : (string)rdr["DrawerUserName"];
            a.DrawerAssignedDate = (rdr["DrawerAssignedDate"] == DBNull.Value) ? (DateTime?)null : (DateTime?)rdr["DrawerAssignedDate"];

            a.Genre = (rdr["Genre"] == DBNull.Value) ? string.Empty : (string)rdr["Genre"];
            a.GenreId = (rdr["GenreID"] == DBNull.Value) ? (short)0 : (short)rdr["GenreID"];
            return a;
        }
        private static ArticleRow ReadOneArticleRow(SqlDataReader rdr)
        {
            var a = new ArticleRow();
            // get the results of each column
            a.Id = (Guid)rdr["ID"];
            a.AuthorUserId = (Guid)rdr["AuthorUserId"];
            a.AuthorPenName = (rdr["AuthorPenName"] == DBNull.Value) ? string.Empty : rdr["AuthorPenName"].ToString();
            a.AuthorIsPublicProfile = (bool)rdr["AuthorIsPublicProfile"] ;
            a.Title = (string)rdr["Title"];
            a.ArticleStatus = (rdr["ArticleStatus"] == DBNull.Value) ? "Unsubmitted" : rdr["ArticleStatus"].ToString();
            
            a.ViewedCount = (rdr["ViewedCount"] == DBNull.Value) ? 0 : (int)rdr["ViewedCount"];
            a.UpVote = (rdr["ArticleThumbUpCount"] == DBNull.Value) ? 0 : (int)rdr["ArticleThumbUpCount"];
            a.DownVote = (rdr["ArticleThumbDownCount"] == DBNull.Value) ? 0 : (int)rdr["ArticleThumbDownCount"];
            a.CommentCount = (rdr["ArticleCommentCount"] == DBNull.Value) ? 0 : (int)rdr["ArticleCommentCount"];
            a.Genre = (rdr["Genre"] == DBNull.Value) ? string.Empty : (string)rdr["Genre"];

            return a;
        }
    }
}


