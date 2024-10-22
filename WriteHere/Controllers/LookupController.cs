using DataAccess;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Model;

namespace WriteHere.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class LookupController : ControllerBase
    {

        //[HttpGet]
        //public LookupPack GetLookupList(string lookupType)
        //{
        //    var pack = new LookupPack();

        //    pack.Genre = LookupRepository.GenreList();
        //    pack.AssignPurpose = LookupRepository.AssignPurposeList();
        //    return pack;

        
        [HttpGet]
        [ActionName("GetPack")]
        public LookupPack GetPack()
        {
            var pack = new LookupPack();
            pack.Genre = new List<CommonValue>() { new CommonValue() { Text = "test1", Value = 1 } };
            pack.AssignPurpose = new List<CommonValue>() { new CommonValue() { Text = "test1AssignPurpose", Value = 2 } };
            //pack.Genre = LookupRepository.GenreList();
            //pack.AssignPurpose = LookupRepository.AssignPurposeList();
            return pack;
        }

        [HttpGet]
        [ActionName("GetGenre")]
        public IEnumerable<CommonValue> GetGenre()
        {
            var pack = new LookupPack();
            pack.Genre = new List<CommonValue>() { new CommonValue() { Text = "test1", Value = 1 } };
            pack.AssignPurpose = new List<CommonValue>() { new CommonValue() { Text = "test1AssignPurpose", Value = 2 } };
            var g = LookupRepository.GenreList();
            //pack.AssignPurpose = LookupRepository.AssignPurposeList();
            return g;
            // pack.Genre;
        }

        //[HttpGet]
        //public IEnumerable<CommonValue> Get()
        //{

        //    var genre = new List<CommonValue>()
        //    {
        //        new CommonValue() { Text = "test1", Value = 1 },
        //        new CommonValue() { Text = "test2", Value = 2 },
        //        new CommonValue() { Text = "test3", Value = 3 },
        //    };
        //    return genre.ToArray();
        //    //pack.Genre = LookupRepository.GenreList();
        //    //pack.AssignPurpose = LookupRepository.AssignPurposeList();
        //    return genre.Select(
        //        index => new CommonValue
        //        {
        //            Text = "testA",
        //            Value = 111,
        //        }
        //    ).ToArray();
        //}


        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Text = "testA",
        //        Value = 111,
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //        .ToArray();
        //}

    }
}
