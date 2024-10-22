using System;

namespace DataAccess.Model
{

    // must be consistant to [LUAssignPurpose]
    public enum AssignPurpose
    {
        ForEditor = 1,
        ForTutor = 3,
        ForDrawer = 5,
    }
    public class AssignmentRequest
    {
        public Guid ArticleId;
        public bool RequestEditor;
        public bool RequestDrawer;
        public bool RequestTutor;
    }
    public class Assignment
    {
        public Guid Id;
        public string Title;
        public string Subtitle;
        public string ArticleStatus;
        public string AuthorPenName;
        public DateTime AssignedDate;
        public DateTime DueDate;
        public string AssignMessage;
        public Guid AuthorUserId;
        public Guid EditorUserId;
        public string EditorUserName;
        public string Content;
        public string Summary;
        public string EditorReasonNote;
        public short AcceptDecline;
        public AssignPurpose AssignPurpose;
    }
}
