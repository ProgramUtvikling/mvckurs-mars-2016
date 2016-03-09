using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ImdbWeb.Controllers
{
    public class ImdbApiController : ImdbControllerBase
    {
        public ActionResult Movies(string fmt = "xml")
        {
            switch (fmt.ToLower())
            {
                case "xml": return MoviesAsXml();
                case "json": return MoviesAsJson();
                case "xls": return MoviesAsExcel();
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        private ActionResult MoviesAsJson()
        {
            var data = from movie in Db.Movies
                       select new { id = movie.MovieId, title = movie.Title };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private ActionResult MoviesAsXml()
        {
            var doc = new XElement("movies", from movie in Db.Movies.ToList()
                                             select new XElement("movie",
                                                new XAttribute("id", movie.MovieId),
                                                new XAttribute("title", movie.Title)
                                             )
                                  );

            return Content(doc.ToString(), "application/xml");
        }

        public ActionResult MoviesAsExcel()
        {
            var movies = Db.Movies.ToList();

            var lines = from movie in movies
                        select $"{movie.MovieId}\t{movie.Title}";
            var doc = "id\ttitle\r\n" + string.Join("\r\n", lines.ToArray());

            Response.ContentEncoding = Encoding.Default;
            Response.AddHeader("content-disposition", "attachment;filename=report.xls");
            return Content(doc, "application/vnd.ms-excel");
        }

        [Route("Movie/Details/{id}.xml")]
        public ActionResult MovieDetails(string id)
        {
            var movie = Db.Movies.Find(id);
            var doc = new XElement("movie",
                                        new XAttribute("id", movie.MovieId),
                                        new XAttribute("title", movie.Title),
                                        new XAttribute("runLength", movie.RunningLength),
                                        new XAttribute("prodYear", movie.ProductionYear),
                                        from p in movie.Directors select new XElement("director", p.Name),
                                        from p in movie.Producers select new XElement("producer", p.Name),
                                        from p in movie.Actors select new XElement("actor", p.Name),
                                        new XCData(movie.Description)
                                  );
            return Content(doc.ToString(), "application/xml");
        }
    }
}