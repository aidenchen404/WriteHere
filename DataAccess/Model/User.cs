using System;

namespace DataAccess.Model
{
    public class User
    {
        public Guid Id;
        public string UserName;
        public string LoginPassword;
        public string FirstName;
        public string LastName;
        public bool ShowName;
        public string PenName;
        public string  Email;
        public bool ShowEmail;
        public int Grade;
        public bool ShowGrade;

        public string Country;
        public bool ShowCountry;
        public string State;
        public bool ShowState;
        public bool ShowProfile;
        public bool ShowInHall;

        public bool IsAdmin;
        public bool IsReader;

        public bool IsWriter;
        public string WriterAd;
        public bool RequestingWriter;
        public bool RequestWriterDeclined;
        public bool IsEditor;
        public string EditorAd;
        public bool RequestingEditor;
        public bool RequestEditorDeclined;
        public bool IsAuditor;
        public bool RequestingAuditor;
        public bool RequestAuditorDeclined;
        public bool IsDrawer;
        public string DrawerAd;
        public bool RequestingDrawer;
        public bool RequestDrawerDeclined;
        public bool IsTutor;
        public string TutorAd;
        public bool RequestingTutor;
        public bool RequestTutorDeclined;
    }

    public class ChangeUserNameRequest
    {
        public Guid Id; public string UserName;
    }

    public class ChangePasswordRequest
    {
        public Guid Id; public string Password;
    }

    public class ChangeNameAndVisibilityRequest
    {
        public Guid Id;
        public string FirstName;
        public string LastName;
        public bool ShowName;
        public bool Inactive;
        public bool ShowProfile;
        public bool ShowInHall;
    }

    public class ChangeProfileRequest
    {
        public Guid Id;
        public string PenName;
        
        public string Email;
        public bool ShowEmail;
        public int Grade;
        public bool ShowGrade;
        public string Country;
        public bool ShowCountry;
        public string State;
        public bool ShowState;
    }
    public class ChangeRoleRequest
    {
        public Guid Id;
        public bool IsAdmin;
        public bool IsReader;
        public bool IsWriter;
        public string WriterAd;
        public bool RequestingWriter;
        public bool IsEditor;
        public string EditorAd;
        public bool RequestingEditor;
        public bool IsAuditor;
        public bool RequestingAuditor;
        public bool IsDrawer;
        public string DrawerAd;
        public bool RequestingDrawer;
        public bool IsTutor;
        public string TutorAd;
        public bool RequestingTutor;
        public bool RequestWriterDeclined;
        public bool RequestEditorDeclined;
        public bool RequestAuditorDeclined;
        public bool RequestDrawerDeclined;
        public bool RequestTutorDeclined;

    }

    public class ChangeRoleRequestingRequest
    {
        public Guid Id;
        public bool RequestingWriter;
        public string WriterAd;
        public bool RequestingEditor;
        public string EditorAd;
        public bool RequestingAuditor;
        public string DrawerAd;
        public bool RequestingDrawer;
        public string TutorAd;
        public bool RequestingTutor;

    }

}
