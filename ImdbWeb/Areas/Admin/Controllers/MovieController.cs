using ImdbWeb.Areas.Admin.Models.MovieModels;
using ImdbWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}