using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.Model;


namespace WriteHere.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<User> GetUserList()
        {
            var list = UserRepository.GetUserList();
            return list;
        }

        [HttpGet("[action]")]
        public IEnumerable<CommonString> GetEditorList()
        {
            var list = UserRepository.GetUserList()
                .Where(x => x.IsEditor)
                .Select( x=> new CommonString
                {
                    Text = x.PenName,
                    Value=x.Id.ToString() 
                } );
            return list;
        }

        [HttpGet("[action]")]
        public IEnumerable<CommonString> GetDrawerList()
        {
            var list = UserRepository.GetUserList().Where( x=>x.IsDrawer)
                .Select(x => new CommonString
                {
                    Text = x.PenName,
                    Value = x.Id.ToString()
                }); 
            return list;
        }

        [HttpGet("[action]")]
        public IEnumerable<CommonString> GetTutorList()
        {
            var list = UserRepository.GetUserList().Where(x => x.IsTutor)
                .Select(x => new CommonString
{
                Text = x.PenName,
                Value = x.Id.ToString()
            });
            return list;
        }

        [HttpGet("[action]")]
        public IEnumerable<User> GetWritersList()
        {
            var list = UserRepository.GetUserList().Where(x => x.IsWriter);
            return list;
        }
        
        [HttpGet("[action]")]
        public User GetUserById(Guid id)
        {
            var a = UserRepository.GetUserById(id);
            return a;
        }


        [HttpGet("[action]")]
        public User Login(string username, string password)
        {
            var user = UserRepository.GetUserByLogin(username, password);

            return user;
        }

        // Dev Note: Do not change the Post function name. Must be exactly as this.
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User a)
        {
            Guid newid = Guid.Empty;
            // save the article
            if (a != null)
            {
                newid = UserRepository.CreateUser(a.UserName,
                   a.LoginPassword, a.Email, a.PenName);
            }

            return Json(newid);
        }

        [HttpGet("[action]")]
        public DashboardPack GetDashBoardPack(Guid loginId)
        {
            var pack = new DashboardPack();

            pack.WriterArticles = ArticleRepository.GetArticleRowList(new ArticleQuery { AuthorUserId = loginId });
            pack.ReaderLikedArticles = ArticleRepository.GetArticleRowList(new ArticleQuery { VotedUpByUserId = loginId });
            var assigments = AssignmentRepository.GetAssignmentListByAssignedUserId(loginId);
            pack.EditorAssignments = assigments.Where(x => x.AssignPurpose == AssignPurpose.ForEditor);
            pack.TutorAssignments = assigments.Where(x => x.AssignPurpose == AssignPurpose.ForTutor);
            pack.DrawerAssignments = assigments.Where(x => x.AssignPurpose == AssignPurpose.ForDrawer);
            pack.AuditorAssignments = AssignmentRepository.GetAssignmentListWithNoAssignedUserId();
            //pack.AdminRoleRequests = AssignmentRepository.GetArticleRowList(new ArticleQuery { EditorUserId = loginId });

            return pack;
        }
        //WriterArticles;
        //public IEnumerable<ArticleRow> ReaderLikedArticles;
        //public IEnumerable<ArticleRow> EditorArticles;
        //public IEnumerable<ArticleRow> TutorArticles;
        //public IEnumerable<ArticleRow> DrawerArticles;
        //public IEnumerable<ArticleRow> AuditorArticleRequsts;
        //public IEnumerable<ArticleRow> AdminRoleRequest;

        // use Http Get for update purpose, because Post here only allow one Controller one post.
        [HttpPut("[action]")]
        public void UpdateUserName([FromBody] ChangeUserNameRequest request)
        {
            UserRepository.UpdateUserName(request.Id, request.UserName);
            return;
        }

        [HttpPut("[action]")]
        public void UpdatePassword([FromBody]  ChangePasswordRequest request)
        {
            UserRepository.UpdatePassword(request);
            return;
        }

        [HttpPut("[action]")]
        public void  UpdateNameAndVisibility([FromBody] ChangeNameAndVisibilityRequest request)
        {
            UserRepository.UpdateNameAndVisibility(request);
            return;
        }

        [HttpPut("[action]")]
        public void UpdateProfile([FromBody] ChangeProfileRequest request)
        {
            UserRepository.UpdateProfile(request);
            return;
        }

        [HttpPut("[action]")]
        public void UpdateRole([FromBody] ChangeRoleRequest request)
        {
            UserRepository.UpdateRequesting(request);
            return;
        }

        [HttpPut("[action]")]
        public void AdminUpdateRole([FromBody] ChangeRoleRequest request)
        {
            UserRepository.AdminUpdateRole(request);
            return;
        }
    }
}
