using System;

namespace DataAccess.Model
{

    /// <summary>
    /// match the values to table [ArticleStatus]
    /// </summary>
    public enum ArticleStatus
    {
        Submitted = 1,
        Assigned = 3,
        ReadyToPublish = 5,
        ReadyToReject = 7,
        Published = 9,
        Rejected = 11,
        RequestEditing = 13,
        RequestToClosed = 15,
        Closed = 5
    }

    public class ArticleStatusHistory
    {
        public Guid? Id;
        public Guid ArticleID;
        public ArticleStatus ArticleStatusID;
        public DateTime? StatusChangeDate;
    }

}
