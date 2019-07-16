using DVDLibraryWebAPI.Data;
using DVDWebAPI.Data;
using DVDWebAPI.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace DVDLibraryWebAPI.Controllers
{
    //Home controller which handles all data request
    public class HomeController : ApiController
    {
        [Route("DVDs")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            IDVDRepository repo = Settings.GetRepository();
            List<DVD> result = repo.GetAll();
            return Ok(result);
        }

        [Route("DVD/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchByID(int id)
        {
            IDVDRepository repo = Settings.GetRepository();
            DVD result = repo.SearchByID(id);
            return Ok(result);
        }

        [Route("DVD/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DVDDelete(int id)
        {
            IDVDRepository repo = Settings.GetRepository();
            repo.Delete(id);
            return Ok();
        }

        [Route("DVD/{id}")]
                [AcceptVerbs("PUT")]
                public IHttpActionResult DVDUpdate(DVD DVD)
                {
                    IDVDRepository repo = Settings.GetRepository();
                    repo.Update(DVD);
                    return Ok(DVD);
                }
        [Route("DVD")]
        [AcceptVerbs("POST")]
        public IHttpActionResult DVDInsert(DVD DVD)
        {
            IDVDRepository repo = Settings.GetRepository();
            return Ok(repo.Add(DVD));
        }
        [Route("DVDs/director/{directorName}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchByDirector(string directorName)
        {
            IDVDRepository repo = Settings.GetRepository();
            List<DVD> result = repo.SearchByDirector(directorName);
            return Ok(result);
        }
        [Route("DVDs/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchByRating(string rating)
        {
            IDVDRepository repo = Settings.GetRepository();
            List<DVD> result = repo.SearchByRating(rating);
            return Ok(result);
        }
        [Route("DVDs/releasedate/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchByReleaseYear(int releaseYear)
        {
            IDVDRepository repo = Settings.GetRepository();
            List<DVD> result =  repo.SearchByReleaseYear(releaseYear);
            
            return Ok(result);
        }
        [Route("DVDs/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchByReleaseTitle(string title)
        {
            IDVDRepository repo = Settings.GetRepository();
            List<DVD> result = repo.SearchByTitle(title);
            return Ok(result);
        }
        //private List<DVD> List<DVD> list)
        //{
        //    foreach (DVD d in list)
        //    {
        //        d.director = d.director == null ? d.Directors.DirectorName : d.director;
        //        d.rating = d.rating == null ? d.Ratings.RatingName : d.rating;
        //    }
        //    return list;
        //}
    }
}