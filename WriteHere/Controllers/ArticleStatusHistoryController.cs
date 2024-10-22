using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.Model;

namespace WriteHere.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleStatusHistoryController : Controller
    {

        [HttpGet("[action]")]
        public StandardResponse Submit(Guid articleId)
        {
            // update the status
            var ah = new ArticleStatusHistory()
            {
                ArticleID = articleId,
                ArticleStatusID = ArticleStatus.Submitted
            };
            ArticleStatusRepository.SaveArticleStatusHistory(ah);

            var result = new StandardResponse() { Success = true, Message = "" };
            return result;
        }
    }


}
