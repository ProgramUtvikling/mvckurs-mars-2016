using ImdbWeb.Models.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImdbWeb.Controllers
{
    [RoutePrefix("Person")]
    public class PersonController : Controller
    {
        // GET: Person
        public ViewResult Actors()
        {
            var db = new MovieDAL.ImdbContext();
            ViewData.Model = new PersonIndexModel
            {
                Title = "Skuespillere",
                Persons = from person in db.Persons
                          where person.ActedMovies.Any()
                          select person
            };
            return View("Index");
        }

        public ViewResult Producers()
        {
            var db = new MovieDAL.ImdbContext();
            ViewData.Model = new PersonIndexModel
            {
                Title = "Produsenter",
                Persons = from person in db.Persons
                          where person.ProducedMovies.Any()
                          select person
            };
            return View("Index");
        }

        public ViewResult Directors()
        {
            var db = new MovieDAL.ImdbContext();
            ViewData.Model = new PersonIndexModel
            {
                Title = "Regisører",
                Persons = from person in db.Persons
                          where person.DirectedMovies.Any()
                          select person
            };
            return View("Index");
        }

        [Route("{id:int}")]
        public ViewResult Details(int id)
        {
            var db = new MovieDAL.ImdbContext();
            ViewData.Model = db.Persons.Find(id);
            return View();
        }
    }
}