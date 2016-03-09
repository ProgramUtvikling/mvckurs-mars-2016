using ImdbWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImdbWeb.Controllers
{
    //[Authorize]
    [RoutePrefix("Movie")]
    public class MovieController : ImdbControllerBase
    {
        [OutputCache(CacheProfile = "Long")]
        public ViewResult Index()
        {
            ViewData.Model = Db.Movies;
            return View();
        }

        public ActionResult Details(string id)
        {
            var movie = Db.Movies.Find(id);
            if(movie == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = movie;
            return View();
        }

        public ViewResult Genres()
        {
            ViewData.Model = Db.Genres;
            return View();
        }

        [OutputCache(CacheProfile = "Short")]
        [Route("Genre/{genrename}")]
        public ViewResult MoviesByGenre(string genrename)
        {
            //ViewData.Model = Db.Movies
            //    .Where(m => m.Genre.Name == genrename);

            ViewData.Model = from movie in Db.Movies
                             where movie.Genre.Name == genrename
                             select movie;
            ViewBag.GenreName = genrename;
            return View("Index");
        }
    }
}