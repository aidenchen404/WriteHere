using System;
using System.Collections.Generic;

namespace DataAccess.Model
{
    public class ArticleVote
    {
        public Guid? Id;
        public int Vote;
        public Guid UserId;
        public Guid ArticleId;
    }
    public class ArticleVoteQuery
    {
        public Guid? UserId;
        public Guid? ArticleId;
    }

    public class ArticleComment
    {
        public Guid? Id;
        public string Comment;
        public Guid CommentOwnerId;
        public string CommentOwnerPenName;
        public Guid ArticleId;
        public string ArticleTitle;
        public bool ShowOwner;
        public bool CommentOwnerShowProfile;
        public string CommentDate;
    }
    public class ArticleCommentQuery
    {
        public Guid? UserId;
        public Guid? ArticleId;
        public short PastMonths;
    }

    public class Article
    {
        public IEnumerable< ArticleComment> Comments;
        public Guid? Id;
        public string Title;
        public string Subtitle;
        public string Summary;
        public string Content;
        public string EditorReviewNote;
        public string Genre;
        public short GenreId;

        public Guid? AuthorUserId;
        public bool AuthorIsPublicProfile;
        public string AuthorPenName;
        public string ArticleStatus;

        public int ViewedCount;
        public int UpVote;
        public int DownVote;
        public int CommentCount;

        public short  ViewerVote;       // 1 or -1

        public Guid? EditorUserId;
        public string EditorUserName;
        public DateTime? EditorAssignedDate;
        public Guid? TutorUserId;
        public string TutorUserName;
        public DateTime? TutorAssignedDate;
        public Guid? DrawerUserId;
        public string DrawerUserName;
        public DateTime? DrawerAssignedDate;
    }

    public class ArticleRow     // for UI side readonly purpose
    {
        public Guid Id;
        public string Title;
        public string Genre;

        public string AuthorPenName;
        public Guid AuthorUserId;
        public bool AuthorIsPublicProfile;
        public string ArticleStatus;

        public int ViewedCount;
        public int UpVote;
        public int DownVote;
        public int CommentCount;
    }

    public class ArticleQuery
    {
        public string Genre;
        public Guid? AuthorUserId;
        public Guid? EditorUserId;
        public Guid? VotedUpByUserId;
        public Guid? CommentedByUserId;
        public string StatusName;
        public bool IsViewerAdmin;
    }

    public class ArticleSearch
    {
        public string field; // title, subtitle, summary, authorPenName, genre, editorUserName, viewedCount, upvote, downVote
        public string optor; // Contain, greater than, equal, less than
        public string value;
    }

    public class ArticleOrderby
    {
        public string field; // title, subtitle, summary, authorPenName, genre, editorUserName, viewedCount, upvote, downVote
        public bool asc; // 1: asc, 0: desc
    }
}
