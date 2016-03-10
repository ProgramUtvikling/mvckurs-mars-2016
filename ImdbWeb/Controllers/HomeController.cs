using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImdbWeb.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Demo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Demo(DemoModel model)
        {
            model.Artikkel = Sanitizer.GetSafeHtmlFragment(model.Artikkel);

            ViewData.Model = model;
            return PartialView("DemoResult");
        }
    }

    public class DemoModel
    {
        public string Overskrift { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Artikkel { get; set; }
    }
}