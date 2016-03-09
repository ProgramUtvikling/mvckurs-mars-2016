using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ImdbWeb.Controllers
{
    public class ImdbApiController : ImdbControllerBase
    {
        public ActionResult Movies()
        {
            var movies = Db.Movies.ToList();
            var doc = new XElement("Movies", from movie in movies
                                             select new XElement("movie",
                                                new XAttribute("id", movie.MovieId),
                                                new XAttribute("title", movie.Title)
                                             )
                                  );

            return Content(doc.ToString());
        }

        #region *** TOP SECRET STUFF ***
        //public ActionResult MoviesAsExcel()
        //{
        //    var movies = Db.Movies.ToList();

        //    var lines = from movie in movies
        //                select $"{movie.MovieId}\t{movie.Title}";
        //    var doc = "id\ttitle\r\n" + string.Join("\r\n", lines.ToArray());

        //    Response.ContentEncoding = Encoding.Default;
        //    //Response.AddHeader("content-disposition", "attachment;filename=report.xls");
        //    return Content(doc, "application/vnd.ms-excel");
        //} 
        #endregion

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
            return Content(doc.ToString());
        }
    }
}