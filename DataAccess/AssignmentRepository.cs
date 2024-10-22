using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess
{
    public static class AssignmentRepository
    {
        public static void Create(AssignmentRequest a)
        {
            SqlConnection conn = new SqlConnection(Const.ConnString);
            conn.Open();
           
            SqlCommand cmdEditor = new SqlCommand(@"INSERT INTO dbo.[Assignment]
                AssignedDate, ArticleID, AssignPurpose
                OUTPUT INSERTED.ID 
                VALUES (@assignedDate, @articleID, @assignPurpose)", conn);
            cmdEditor.Parameters.AddWithValue("@assignedDate", DateTime.Today);
            cmdEditor.Parameters.AddWithValue("@dueDate", DateTime.Today.AddDays(14));
            cmdEditor.Parameters.AddWithValue("@articleID", a.ArticleId);
            cmdEditor.Parameters.AddWithValue("@assignPurpose", AssignPurpose.ForEditor);

            SqlCommand cmdTutor = new SqlCommand(@"INSERT INTO dbo.[Assignment] 
                AssignedDate, ArticleID, AssignPurpose
                OUTPUT INSERTED.ID 
                VALUES (@assignedDate, @articleID, @assignPurpose)", conn);
            cmdTutor.Parameters.AddWithValue("@assignedDate", DateTime.Today);
            cmdTutor.Parameters.AddWithValue("@dueDate", DateTime.Today.AddDays(14));
            cmdTutor.Parameters.AddWithValue("@articleID", a.ArticleId);
            cmdTutor.Parameters.AddWithValue("@assignPurpose", AssignPurpose.ForTutor);

            SqlCommand cmdDrawer = new SqlCommand(@"INSERT INTO dbo.[Assignment]
                AssignedDate, ArticleID, AssignPurpose
                OUTPUT INSERTED.ID 
                VALUES (@assignedDate, @articleID, @assignPurpose)", conn);
            cmdDrawer.Parameters.AddWithValue("@assignedDate", DateTime.Today);
            cmdDrawer.Parameters.AddWithValue("@dueDate", DateTime.Today.AddDays(14));
            cmdDrawer.Parameters.AddWithValue("@articleID", a.ArticleId);
            cmdDrawer.Parameters.AddWithValue("@assignPurpose", AssignPurpose.ForDrawer);

            try
            {
                if (a.RequestEditor) { cmdEditor.ExecuteScalar(); }
                if (a.RequestTutor) { cmdTutor.ExecuteScalar(); }
                if (a.RequestDrawer) { cmdDrawer.ExecuteScalar(); }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
           
            return;
        }


        public static Assignment Update(Assignment a)
        {
            return a;
        }


        public static void Delete(Guid id)
        {

            return;
        }

        public static Assignment GetAssignment(Guid id)
        {
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.vwAssignmentList WHERE ID ='" + id + "'", conn);
            Assignment a = null;

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

        public static List<Assignment> GetAssignmentListByAssignedUserId(Guid? userId)
        {
            var sql = "SELECT * FROM dbo.vwAssignmentList";
            if (userId.HasValue)
            {
                sql += " WHERE ";
                if (userId.HasValue) { sql += " AssignedUserId = @u"; }

            }

            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (userId.HasValue) { cmd.Parameters.AddWithValue("@u", userId); }

            var list = new List<Assignment>();
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

        public static List<Assignment> GetAssignmentListWithNoAssignedUserId()
        {
            var sql = "SELECT * FROM dbo.vwAssignmentList WHERE AssignedUserId IS NULL"; 

            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
          
            var list = new List<Assignment>();
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

        private static Assignment ReadRow(SqlDataReader rdr)
        {
            var a = new Assignment();
            // get the results of each column
            a.Id = (Guid)rdr["ID"];
            a.AuthorPenName = (rdr["AuthorPenName"] == DBNull.Value) ? string.Empty : rdr["AuthorDisplayName"].ToString();
            a.Title = (string)rdr["Title"];
            a.Subtitle = (rdr["Subtitle"] == DBNull.Value) ? string.Empty : rdr["Subtitle"].ToString();
            a.ArticleStatus = (rdr["ArticleStatus"] == DBNull.Value) ? string.Empty : rdr["ArticleStatus"].ToString();
            a.AssignedDate = (DateTime)rdr["AssignedDate"];
            a.AuthorUserId = (Guid)rdr["AuthorUserId"];
            a.EditorUserId = (Guid)rdr["EditorUserId"];
            a.EditorUserName = (rdr["EditorUserName"] == DBNull.Value) ? string.Empty : rdr["EditorUserName"].ToString();
            a.Content = (rdr["Content"] == DBNull.Value) ? string.Empty : rdr["Content"].ToString();
            a.Summary = (rdr["Abstract"] == DBNull.Value) ? string.Empty : rdr["Abstract"].ToString();
            a.EditorReasonNote = (rdr["EditorReasonNote"] == DBNull.Value) ? string.Empty : rdr["EditorReasonNote"].ToString();
            a.AcceptDecline =(short) rdr["AcceptDecline"];
      
            return a;
        }
    }
}


