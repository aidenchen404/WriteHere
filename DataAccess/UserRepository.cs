using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess
{
    public static class UserRoleColName
    {
        public const string Reader = "Reader";
        public const string Writer = "Writer";
        public const string Editor = "Editor";
        public const string Auditor = "Auditor";
        public const string Drawer = "Drawer";
        public const string Tutor = "Tutor";
        public const string Admin = "Admin";
    }

    public static class UserRepository
    {
        // when User create an account
        public static Guid CreateUser(string username, string password,
            string email, string penName)
        {
            var retId = Guid.Empty;

            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.[User] UserName,
                LoginPassword, Email,PenName
                OUTPUT INSERTED.ID 
                VALUES (@username, @password, @email, @penName)", conn);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@penName", penName);
            try
            {
                conn.Open();
                retId = (Guid)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
            return retId;
        }

        // when User manage his account settings, need to check duplication
        public static void UpdateUserName(Guid id, string username)
        {
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand("UPDATE dbo.[User] SET Username = @username WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@username",username);
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
            return;
        }

        // when User manage his account settings, need to check psw strength
        public static void UpdatePassword(ChangePasswordRequest request)
        { 
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand("UPDATE dbo.[User] SET Password = @password WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", request.Id);
            cmd.Parameters.AddWithValue("@password", request.Password);
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
            return;
        }

       
        // when User manage his account settings, need to check duplication
        public static void UpdateNameAndVisibility(ChangeNameAndVisibilityRequest request)
        {
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(@"UPDATE dbo.[User] SET FirstName = @firstName , 
                LastName = @lastName , ShowName = @showName, Inactive = @inactive
                ShowProfile = @showProfile, ShowInHall = @showInHall WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", request.Id);
            cmd.Parameters.AddWithValue("@firstName", request.FirstName);
            cmd.Parameters.AddWithValue("@lastName", request.LastName);
            cmd.Parameters.AddWithValue("@showName", request.ShowName);
            cmd.Parameters.AddWithValue("@inactive", request.Inactive);
            cmd.Parameters.AddWithValue("@showProfile", request.ShowProfile);
            cmd.Parameters.AddWithValue("@showInHall", request.ShowInHall);
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
            return;
        }

        // when User manage his account settings
    public static void UpdateProfile(ChangeProfileRequest request )
        {
            var yearOfSchool = DateTime.Today.Year- request.Grade ;
               SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(@"UPDATE dbo.[User] SET 
                PenName = @penname,
                Email = @email,ShowEmail = @showEmail,  
                yearOfSchool = @yearOfSchool,  ShowGrade = @showGrade, 
                Country = @country,  ShowCountry = @showCountry,  
                State = @state,ShowState = @showState
                WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", request.Id);
            cmd.Parameters.AddWithValue("@penname", request.PenName);
            
            cmd.Parameters.AddWithValue("@email", request.Email);
            cmd.Parameters.AddWithValue("@showEmail", request.ShowEmail);
            cmd.Parameters.AddWithValue("@yearOfSchool", yearOfSchool);
            cmd.Parameters.AddWithValue("@showGrade", request.ShowGrade);
            cmd.Parameters.AddWithValue("@country", request.Country);
            cmd.Parameters.AddWithValue("@showCountry", request.ShowCountry);
            cmd.Parameters.AddWithValue("@state", request.State);
            cmd.Parameters.AddWithValue("@showState", request.ShowState);
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
            return;
        }

        // when Admin manage a user's account settings
        public static void UpdateRole(ChangeRoleRequest request)
        {
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(@"UPDATE dbo.[User] SET 
                IsAdmin = @isAdmin, IsReader = @isReader, 
                IsWriter = @isWriter, IsEditor = @isEditor, IsAuditor = @isAuditor, 
                IsDrawer = @isDrawer, IsTutor = @isTutor WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", request.Id);
            cmd.Parameters.AddWithValue("@isAdmin", request.IsAdmin);
            cmd.Parameters.AddWithValue("@isReader", request.IsReader);
            cmd.Parameters.AddWithValue("@isWriter", request.IsWriter);
            cmd.Parameters.AddWithValue("@isEditor", request.IsEditor);
            cmd.Parameters.AddWithValue("@isAuditor", request.IsAuditor);
            cmd.Parameters.AddWithValue("@isDrawer", request.IsDrawer);
            cmd.Parameters.AddWithValue("@isTutor", request.IsTutor);

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
            return;
        }
        
        // when Admin accept a user's request of role
        public static void UpdateOneRole(Guid id, string role, bool value)
        {
            string sql = "UPDATE dbo.[User] SET is" + role + " = @value WHERE Id = @id";
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@value", value);
        }

        // when User manage  his account settings
        public static void UpdateRequesting(ChangeRoleRequest request)
        {
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(@"UPDATE dbo.[User] SET 
                RequestingWriter = @requestingWriter, WriterAd = @writerAd,
                RequestingEditor = @requestingEditor, EditorAd = @editorAd,
                RequestingAuditor = @requestingAuditor, 
                RequestingDrawer = @requestingDrawer,  DrawerAd = @drawerAd,
                RequestingTutor = @requestingTutor , TutorAd = @tutorAd WHERE id = @id", conn);
            
            cmd.Parameters.AddWithValue("@id", request.Id);
            cmd.Parameters.AddWithValue("@requestingWriter", request.RequestingWriter);
            cmd.Parameters.AddWithValue("@requestingEditor", request.RequestingEditor);
            cmd.Parameters.AddWithValue("@requestingAuditor", request.RequestingAuditor);
            cmd.Parameters.AddWithValue("@requestingDrawer", request.RequestingDrawer);
            cmd.Parameters.AddWithValue("@requestingTutor", request.RequestingTutor);
            cmd.Parameters.AddWithValue("@writerAd", request.WriterAd);
            cmd.Parameters.AddWithValue("@editorAd", request.EditorAd);
            cmd.Parameters.AddWithValue("@drawerAd", request.DrawerAd);
            cmd.Parameters.AddWithValue("@tutorAd", request.TutorAd);

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
            return;
        }

        // when Admin manage a user's account settings
        public static void AdminUpdateRole(ChangeRoleRequest request)
        {
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(@"UPDATE dbo.[User] SET 
                IsAdmin= @isAdmin,isReader= @isReader,IsWriter= @isWriter,
                IsEditor= @isEditor,isAuditor= @isAuditor,IsDrawer= @isDrawer,IsTutor= @isTutor,
                RequestingWriter = @requestingWriter, WriterAd = @writerAd,
                RequestingEditor = @requestingEditor, EditorAd = @editorAd,
                RequestingAuditor = @requestingAuditor, 
                RequestingDrawer = @requestingDrawer,  DrawerAd = @drawerAd,
                RequestingTutor = @requestingTutor , TutorAd = @tutorAd
                RequestWriterDeclined = @requestWriterDeclined, RequestEditorDeclined = @requestEditorDeclined, 
                RequestAuditorDeclined = @requestAuditorDeclined, RequestDrawerDeclined = @requestDrawerDeclined, 
                RequestTutorDeclined = @requestTutorDeclined WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@id", request.Id);
            cmd.Parameters.AddWithValue("@requestWriterDeclined", request.RequestWriterDeclined);
            cmd.Parameters.AddWithValue("@requestEditorDeclined", request.RequestEditorDeclined);
            cmd.Parameters.AddWithValue("@requestAuditorDeclined", request.RequestAuditorDeclined);
            cmd.Parameters.AddWithValue("@requestDrawerDeclined", request.RequestDrawerDeclined);
            cmd.Parameters.AddWithValue("@requestTutorDeclined", request.RequestTutorDeclined);
            
            cmd.Parameters.AddWithValue("@requestingWriter", request.RequestingWriter);
            cmd.Parameters.AddWithValue("@requestingEditor", request.RequestingEditor);
            cmd.Parameters.AddWithValue("@requestingAuditor", request.RequestingAuditor);
            cmd.Parameters.AddWithValue("@requestingDrawer", request.RequestingDrawer);
            cmd.Parameters.AddWithValue("@requestingTutor", request.RequestingTutor);
           
            cmd.Parameters.AddWithValue("@writerAd", request.WriterAd);
            cmd.Parameters.AddWithValue("@editorAd", request.EditorAd);
            cmd.Parameters.AddWithValue("@drawerAd", request.DrawerAd);
            cmd.Parameters.AddWithValue("@tutorAd", request.TutorAd);
            
            cmd.Parameters.AddWithValue("@isWriter", request.IsWriter);
            cmd.Parameters.AddWithValue("@isEditor", request.IsEditor);
            cmd.Parameters.AddWithValue("@isAuditor", request.IsAuditor);
            cmd.Parameters.AddWithValue("@isDrawer", request.IsDrawer);
            cmd.Parameters.AddWithValue("@isTutor", request.IsTutor);
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
            return;
        }

        // when Admin decline a user's request of role
        public static void UpdateOneRequestDeclined(Guid id, string role, bool value)
        {
            string sql = "UPDATE dbo.[User] SET Request" + role + "Declined = @value WHERE Id = @id";
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@value", value);
        }
    

        public static User GetUserByLogin(string username, string password)
        {
            SqlDataReader rdrUser = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmdUser = new SqlCommand("SELECT * FROM dbo.[User] WHERE UserName = @u AND LoginPassword = @p", conn);
            cmdUser.Parameters.AddWithValue("@u", username);
            cmdUser.Parameters.AddWithValue("@p", password);

            User user = null;
            try
            {
                conn.Open();
                rdrUser = cmdUser.ExecuteReader();
                if (rdrUser.Read())
                {
                    user = ReadRow(rdrUser);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (rdrUser != null) { rdrUser.Close(); }
                if (conn != null) { conn.Close(); }
            }
            return user;
        }
        public static bool ExistSamePenName(string penName)
        {
            bool ret = false;
            SqlDataReader rdrUser = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmdUser = new SqlCommand("SELECT * FROM dbo.[User] WHERE PenName = @u", conn);
            cmdUser.Parameters.AddWithValue("@u", penName);
            try
            {
                conn.Open();
                rdrUser = cmdUser.ExecuteReader();
                if (rdrUser.Read())
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (rdrUser != null) { rdrUser.Close(); }
                if (conn != null) { conn.Close(); }
            }
            return ret;
        }
        public static bool ExistSameUserName(string username)
        {
            bool ret = false;
            SqlDataReader rdrUser = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmdUser = new SqlCommand("SELECT * FROM dbo.[User] WHERE UserName = @u", conn);
            cmdUser.Parameters.AddWithValue("@u", username);
            try
            {
                conn.Open();
                rdrUser = cmdUser.ExecuteReader();
                if (rdrUser.Read())
                {
                    ret= true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (rdrUser != null) { rdrUser.Close(); }
                if (conn != null) { conn.Close(); }
            }
            return ret;
        }

        public static User GetUserById(Guid id)
        {
            SqlDataReader rdrUser = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmdUser = new SqlCommand("SELECT * FROM dbo.[User] WHERE ID ='" + id+"'" , conn);

            User user =null;
            try
            {
                conn.Open();
                rdrUser = cmdUser.ExecuteReader();
                if (rdrUser.Read())
                {
                    user = ReadRow(rdrUser);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (rdrUser != null) { rdrUser.Close(); }
                if (conn != null) { conn.Close(); }
            }
            return user;
        }

        public static List<User> GetUserList()
        {
            SqlDataReader rdrUser = null;
            SqlConnection conn = new SqlConnection(Const.ConnString);
            SqlCommand cmdUser = new SqlCommand("SELECT * FROM dbo.[User] ", conn);
            var list = new List<User>();
            try
            {
                conn.Open();
                rdrUser = cmdUser.ExecuteReader();
                while (rdrUser.Read())
                {
                    var user = ReadRow(rdrUser);
                    list.Add(user);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (rdrUser != null) { rdrUser.Close(); }
                if (conn != null) { conn.Close(); }
            }
            return list;
        }

        private static User ReadRow(SqlDataReader rdr)
        {
            var user = new User();

            user.Id = (Guid)rdr["ID"];
            user.UserName = (string)rdr["UserName"];
            user.FirstName = (rdr["FirstName"] == DBNull.Value) ? string.Empty : rdr["FirstName"].ToString();
            user.LastName = (rdr["LastName"] == DBNull.Value) ? string.Empty : rdr["LastName"].ToString();
            user.ShowName = (rdr["ShowName"] == DBNull.Value) ? false : (bool)rdr["ShowName"];
            user.PenName = (rdr["PenName"] == DBNull.Value) ? string.Empty : rdr["PenName"].ToString();
            user.Email = (rdr["Email"] == DBNull.Value) ? string.Empty : rdr["Email"].ToString();
            user.ShowEmail = (rdr["ShowEmail"] == DBNull.Value) ? false : (bool)rdr["ShowEmail"];
            
            var yearOfSchool = (rdr["YearOfSchool"] == DBNull.Value) ? 0 : (int)rdr["YearOfSchool"];
            user.Grade = yearOfSchool==0? 99:  DateTime.Today.Year - yearOfSchool;
            user.ShowGrade = (rdr["ShowGrade"] == DBNull.Value) ? false : (bool)rdr["ShowGrade"];
            user.Country = (rdr["Country"] == DBNull.Value) ? string.Empty : rdr["Country"].ToString();
            user.ShowCountry = (rdr["ShowCountry"] == DBNull.Value) ? false : (bool)rdr["ShowCountry"];
            user.State = (rdr["State"] == DBNull.Value) ? string.Empty : rdr["State"].ToString();
            user.ShowState = (rdr["ShowState"] == DBNull.Value) ? false : (bool)rdr["ShowState"];

            user.ShowProfile = (rdr["ShowProfile"] == DBNull.Value) ? false : (bool)rdr["ShowProfile"];
            user.ShowInHall = (rdr["ShowInHall"] == DBNull.Value) ? false : (bool)rdr["ShowInHall"];
            
            user.IsAdmin = (rdr["IsAdmin"] == DBNull.Value) ? false : (bool)rdr["IsAdmin"];
            user.IsReader = (rdr["IsReader"] == DBNull.Value) ? false : (bool)rdr["IsReader"];
            user.IsWriter = (rdr["IsWriter"] == DBNull.Value) ? false : (bool)rdr["IsWriter"];
            user.WriterAd = (rdr["WriterAd"] == DBNull.Value) ? string.Empty : rdr["WriterAd"].ToString();
            user.RequestingWriter = (rdr["RequestingWriter"] == DBNull.Value) ? false : (bool)rdr["RequestingWriter"];
            user.RequestWriterDeclined = (rdr["RequestWriterDeclined"] == DBNull.Value) ? false : (bool)rdr["RequestWriterDeclined"];
            user.IsEditor = (rdr["IsEditor"] == DBNull.Value) ? false : (bool)rdr["IsEditor"];
            user.EditorAd = (rdr["EditorAd"] == DBNull.Value) ? string.Empty : rdr["EditorAd"].ToString();
            user.RequestingEditor = (rdr["RequestingEditor"] == DBNull.Value) ? false : (bool)rdr["RequestingEditor"];
            user.RequestEditorDeclined = (rdr["RequestEditorDeclined"] == DBNull.Value) ? false : (bool)rdr["RequestEditorDeclined"];
            user.IsAuditor = (rdr["IsAuditor"] == DBNull.Value) ? false : (bool)rdr["IsAuditor"];
            user.RequestingAuditor = (rdr["RequestingAuditor"] == DBNull.Value) ? false : (bool)rdr["RequestingAuditor"];
            user.RequestAuditorDeclined = (rdr["RequestAuditorDeclined"] == DBNull.Value) ? false : (bool)rdr["RequestAuditorDeclined"];
            user.IsDrawer = (rdr["IsDrawer"] == DBNull.Value) ? false : (bool)rdr["IsDrawer"];
            user.DrawerAd = (rdr["DrawerAd"] == DBNull.Value) ? string.Empty : rdr["DrawerAd"].ToString();
            user.RequestingDrawer = (rdr["RequestingDrawer"] == DBNull.Value) ? false : (bool)rdr["RequestingDrawer"];
            user.RequestDrawerDeclined = (rdr["RequestDrawerDeclined"] == DBNull.Value) ? false : (bool)rdr["RequestDrawerDeclined"];
            user.IsTutor = (rdr["IsTutor"] == DBNull.Value) ? false : (bool)rdr["IsTutor"];
            user.TutorAd = (rdr["TutorAd"] == DBNull.Value) ? string.Empty : rdr["TutorAd"].ToString();
            user.RequestingTutor = (rdr["RequestingTutor"] == DBNull.Value) ? false : (bool)rdr["RequestingTutor"];
            user.RequestTutorDeclined = (rdr["RequestTutorDeclined"] == DBNull.Value) ? false : (bool)rdr["RequestTutorDeclined"];

            return user;
        }
    }
}


