using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImdbWeb.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public string Index()
        {
            return "MovieController.Index()";
        }

        public string Details(string id)
        {
            return $"MovieController.Details({id})";
        }

        public string Genres()
        {
            return "MovieController.Genres()";
        }

        public string MoviesByGenre(string genrename)
        {
            return $"MovieController.MoviesByGenre({genrename})";
        }
    }
}