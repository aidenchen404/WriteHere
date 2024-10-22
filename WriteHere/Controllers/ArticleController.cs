using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.Model;
using Newtonsoft.Json;

namespace WriteHere.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<ArticleRow> GetArticleRows(string querystring)
        {
            var articleQuery = new ArticleQuery();
            if (!string.IsNullOrEmpty(querystring))
            {
                articleQuery = JsonConvert.DeserializeObject<ArticleQuery>(querystring);
            }
            var list = ArticleRepository.GetArticleRowList(articleQuery);

         
            return list;
        }

        [HttpGet("[action]")]
        public HallOfFamePack GetHallOfFamePack(string querystring)
        {
         
            var list = HallOfFameRepository.GetHallOfFameList();
            var pack = new HallOfFamePack
            {
                MostVoteUp = list.Where(x => x.HallOfFameType == "MostVoteUp")
                        .OrderBy(x => x.RowNumber),
                MostVoteDown = list.Where(x => x.HallOfFameType == "MostVoteDown")
                        .OrderBy(x => x.RowNumber),
                MostViewed = list.Where(x => x.HallOfFameType == "MostViewed")
                        .OrderBy(x => x.RowNumber),
                MostCommented = list.Where(x => x.HallOfFameType == "MostCommented")
                        .OrderBy(x => x.RowNumber),
                MostPublished = list.Where(x => x.HallOfFameType == "MostPublished")
                        .OrderBy(x => x.RowNumber),
                MostRejected = list.Where(x => x.HallOfFameType == "MostRejected")
                        .OrderBy(x => x.RowNumber),

            };
            return pack;
        }

        [HttpGet("[action]")]
        public Article GetArticle(Guid id, Guid byId)
        {

            var a = ArticleRepository.GetArticle(id);
            if (a == null)
            {
                a = new Article
                {
                    Id = Guid.Empty,
                    Title = "ArticleTitle"
                };
            }
            else
            {
                // load comments
                var ac = ArticleCommentRepository.GetArticleCommentListByQuery
                    (new ArticleCommentQuery { ArticleId = id });
                a.Comments = ac;

                // load viewer's vote
                a.ViewerVote = ArticleVoteRepository.GetArticleVote(new ArticleVoteQuery() { UserId = byId, ArticleId = id });
            }

            // increase the viewCount when the viewer is not the author
            if (a.AuthorUserId != byId)
            {
                ArticleRepository.IncreaseArticleViewedCount(id);
                a.ViewedCount++;
            }
            return a;
        }


        [HttpGet("[action]")]
        public Article AnyFuncName(Guid id)
        {
         
            var a = ArticleRepository.GetArticle(id);
            return a;
        }

        [HttpDelete("[action]")]
        public StandardResponse Delete(Guid id)
        {
            ArticleRepository.DeleteArticle(id);
            var result = new StandardResponse() { Success = true, Message = "" };
            return result;
        }


        // Dev Note: Do not change the Post function name. Must be exactly as this.
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] Article a)
        {
            // save the article
            if (a != null)
            {
                a = ArticleRepository.SaveArticle(a);
            }

            return Json(a);
        }


    }
}
