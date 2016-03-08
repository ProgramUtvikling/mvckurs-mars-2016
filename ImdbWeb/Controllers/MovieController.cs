using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImdbWeb.Controllers
{
    [RoutePrefix("Movie")]
    public class MovieController : Controller
    {
        public ViewResult Index()
        {
            var db = new MovieDAL.ImdbContext();
            ViewData.Model = db.Movies;
            return View();
        }

        public ViewResult Details(string id)
        {
            var db = new MovieDAL.ImdbContext();
            ViewData.Model = db.Movies.Find(id);
            return View();
        }

        public ViewResult Genres()
        {
            var db = new MovieDAL.ImdbContext();
            ViewData.Model = db.Genres;
            return View();
        }

        [Route("Genre/{genrename}")]
        public ViewResult MoviesByGenre(string genrename)
        {
            var db = new MovieDAL.ImdbContext();

            //ViewData.Model = db.Movies
            //    .Where(m => m.Genre.Name == genrename);

            ViewData.Model = from movie in db.Movies
                             where movie.Genre.Name == genrename
                             select movie;
            ViewBag.GenreName = genrename;
            return View("Index");
        }
    }
}