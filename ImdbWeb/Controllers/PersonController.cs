using ImdbWeb.Models.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImdbWeb.Controllers
{
    [RoutePrefix("Person")]
    public class PersonController : ImdbControllerBase
    {
        // GET: Person
        public ViewResult Actors()
        {
            ViewData.Model = new PersonIndexModel
            {
                Title = "Skuespillere",
                Persons = from person in Db.Persons
                          where person.ActedMovies.Any()
                          select person
            };
            return View("Index");
        }

        public ViewResult Producers()
        {
            ViewData.Model = new PersonIndexModel
            {
                Title = "Produsenter",
                Persons = from person in Db.Persons
                          where person.ProducedMovies.Any()
                          select person
            };
            return View("Index");
        }

        public ViewResult Directors()
        {
            ViewData.Model = new PersonIndexModel
            {
                Title = "Regisører",
                Persons = from person in Db.Persons
                          where person.DirectedMovies.Any()
                          select person
            };
            return View("Index");
        }

        [Route("{id:int}")]
        public ViewResult Details(int id)
        {
            ViewData.Model = Db.Persons.Find(id);
            return View();
        }
    }
}