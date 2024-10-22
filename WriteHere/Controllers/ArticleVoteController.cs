using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.Model;


namespace WriteHere.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleVoteController : Controller
    {

        // Dev Note: Do not change the Post function name. Must be exactly as this.
        [HttpPost]
        public async Task<IActionResult> PostArticleVote([FromBody] ArticleVote a)
        {
            if (a != null)
            {
                ArticleVoteRepository.SaveArticleVote(a);
            }
            return Json(a);
        }

    }
}
