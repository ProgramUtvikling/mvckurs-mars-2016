using ImdbWeb.Areas.Admin.Models.MovieModels;
using ImdbWeb.Controllers;
using MovieDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ImdbWeb.Areas.Admin.Controllers
{
    [Authorize]
    public class MovieController : ImdbControllerBase
    {
        public ActionResult Index()
        {
            ViewData.Model = Db.Movies.Select(m => new IndexModel
            {
                Id = m.MovieId,
                Title = m.Title,
                RunningLength = m.RunningLength
            });
            return View();
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Genres = new SelectList(await Db.Genres.ToListAsync(), "GenreId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                var newMovie = new Movie
                {
                    MovieId = model.MovieId,
                    Title = model.Title,
                    OriginalTitle = model.OriginalTitle,
                    Description = model.Description,
                    ProductionYear = model.ProductionYear,
                    RunningLength = model.RunningLengthHours * 60 + model.RunningLengthMinutes,
                    Genre = Db.Genres.Find(model.GenreId)
                };
                Db.Movies.Add(newMovie);
                await Db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return await Create();
        }

        public async Task<ActionResult> Edit(string id)
        {
            var movie = await Db.Movies.FindAsync(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = new EditModel
            {
                Title = movie.Title,
                OriginalTitle = movie.OriginalTitle,
                ProductionYear = movie.ProductionYear,
                RunningLengthHours = movie.RunningLength / 60,
                RunningLengthMinutes = movie.RunningLength % 60,
                Description = movie.Description,
                GenreId = movie.Genre.GenreId
            };
            ViewBag.Genres = new SelectList(await Db.Genres.ToListAsync(), "GenreId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, EditModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = await Db.Movies.FindAsync(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }

                movie.Title = model.Title;
                movie.OriginalTitle = model.OriginalTitle;
                movie.Description = model.Description;
                movie.ProductionYear = model.ProductionYear;
                movie.RunningLength = model.RunningLengthHours * 60 + model.RunningLengthMinutes;
                movie.Genre = Db.Genres.Find(model.GenreId);
                await Db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.Genres = new SelectList(await Db.Genres.ToListAsync(), "GenreId", "Name");
            return View();
        }

        public static ValidationResult CheckId(string movieId)
        {
            using (var db = new ImdbContext())
            {
                if (db.Movies.Any(m => m.MovieId == movieId))
                {
                    return new ValidationResult("Filmen er allerede registrert (custom validtion)");
                }
                return ValidationResult.Success;
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            var movie = await Db.Movies.FindAsync(id);
            if(movie == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = new DeleteModel
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                OriginalTitle = movie.OriginalTitle,
                ProductionYear = movie.ProductionYear
            };
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id, string søppel)
        {
            var movie = await Db.Movies.FindAsync(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            Db.Movies.Remove(movie);
            await Db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}