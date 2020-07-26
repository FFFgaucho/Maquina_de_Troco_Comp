using MaquinaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaquinaWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            Troco troco = new Troco();
            MaquinaDeTroco maquina = MaquinaDeTroco.getMaquinaDeTroco();
            troco.Moedas = maquina.Moedas;
            return View(troco);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}