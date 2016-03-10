using ImdbWeb.Models.RatingModels;
using MovieDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImdbWeb.Controllers
{
    public class RatingController : ImdbControllerBase
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Vote(string id, int vote)
        {
            if (vote < 1 || vote > 5)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var movie = await Db.Movies.FindAsync(id);
            if(movie == null)
            {
                return HttpNotFound();
            }

            movie.Ratings.Add(new Rating { Vote = vote });
            await Db.SaveChangesAsync();

            ViewData.Model = new VoteResultModel
            {
                MovieId = id,
                YourVote = vote,
                VoteCount = movie.Ratings.Count(),
                AverageVote = movie.Ratings.Average(r => r.Vote)
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_VoteResult");
            }
            return View("VoteResult");
        }
    }
}