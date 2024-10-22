using System;
using System.Collections.Generic;


namespace DataAccess.Model
{
    public class HallOfFame
    {
        public string HallOfFameType;
        public Guid ArticleId;
        public Guid AuthorUserId;
		public string ArticleTitle;
        public string PersonName;
        public int RankCount;
        public long RowNumber;
    }

    public class HallOfFamePack
    {
        public IEnumerable<HallOfFame> MostVoteUp;
        public IEnumerable<HallOfFame> MostVoteDown;
        public IEnumerable<HallOfFame> MostViewed;
        public IEnumerable<HallOfFame> MostCommented;
        public IEnumerable<HallOfFame> MostPublished;
        public IEnumerable<HallOfFame> MostRejected;
    }


    public class HallOfFameSearch
    {
        public string field; // title, subtitle, summary, authorPenName, genre, editorUserName, viewedCount, upvote, downVote
        public string optor; //  Contain, greater than, equal, less than
        public string value;
    }

}
