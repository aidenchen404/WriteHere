using System;
using System.Collections.Generic;
namespace DataAccess.Model
{ 
    public class DashboardPack
    {
        public IEnumerable<ArticleRow> WriterArticles;
        public IEnumerable<ArticleRow> ReaderLikedArticles;
        public IEnumerable<Assignment> EditorAssignments;
        public IEnumerable<Assignment> TutorAssignments;
        public IEnumerable<Assignment> DrawerAssignments;
        public IEnumerable<Assignment> AuditorAssignments;
        public IEnumerable<Assignment> AdminRoleRequests;
    }

}
