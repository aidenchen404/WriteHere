using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess
{
    public static class ArticleStatusRepository
    {
        public static void SaveArticleStatusHistory(ArticleStatusHistory a)
        {
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd;
            // When a.Id is a Guid.Null, this is a create. else this is a update

            if (a.Id == null || a.Id == Guid.Empty)
            {
                cmd = new SqlCommand("INSERT INTO ArticleStatusChangeHistory ArticleID,ArticleStatusID,StatusChangeDate OUTPUT INSERTED.ID VALUES (@articleID, @articleStatusID, @statusChangeDate)", conn);
            }
            else
            {
                cmd = new SqlCommand("UPDATE ArticleStatusChangeHistory SET ArticleID=@articleID,ArticleStatusID = @articleStatusID, StatusChangeDate = @statusChangeDate WHERE id=@id", conn);
            }
            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.Parameters.AddWithValue("@articleID", a.ArticleID);
            cmd.Parameters.AddWithValue("@articleStatusID", a.ArticleStatusID);
            cmd.Parameters.AddWithValue("@statusChangeDate", DateTime.Now);

            try
            {
                conn.Open();
                var retId = (Guid)cmd.ExecuteScalar();
                a.Id = retId;
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
            return;
        }

    }
}


