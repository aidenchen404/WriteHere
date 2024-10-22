using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.Model;


namespace WriteHere.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : Controller
    {
 

        // Dev Note: Do not change the Post function name. Must be exactly as this.
        [HttpPut]
        public async void Create([FromBody] AssignmentRequest a)
        {
            AssignmentRepository.Create(a);
            return ;
        }
        [HttpPut]
        public async void Update([FromBody] Assignment a)
        {
            AssignmentRepository.Update(a);
            return;
        }
    }
}
