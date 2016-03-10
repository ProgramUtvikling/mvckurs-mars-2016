using ImdbWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ImdbWeb.Controllers
{
    //[Authorize]
    [RoutePrefix("Movie")]
    public class MovieController : ImdbControllerBase
    {
        [OutputCache(CacheProfile = "Long")]
        public async Task<ViewResult> Index()
        {
            ViewData.Model = await Db.Movies.ToListAsync();
            return View();
        }

        public async Task<ActionResult> Details(string id)
        {
            var movie = await Db.Movies.FindAsync(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = movie;
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }

        public async Task<ViewResult> Genres()
        {
            ViewData.Model = await Db.Genres.ToListAsync();
            return View();
        }

        [OutputCache(CacheProfile = "Short")]
        [Route("Genre/{genrename}")]
        public async Task<ViewResult> MoviesByGenre(string genrename)
        {
            //ViewData.Model = Db.Movies
            //    .Where(m => m.Genre.Name == genrename);

            var movies = from movie in Db.Movies
                         where movie.Genre.Name == genrename
                         select movie;

            ViewData.Model = await movies.ToListAsync();
            ViewBag.GenreName = genrename;
            return View("Index");
        }
    }
}