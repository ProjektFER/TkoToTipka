using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TkoToTipka.Models;

namespace Tko_to_tipka.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            int brojac = 0;
            database.CreateDatabase("database");
            database.CreateTable("database", "mjerenja");
            database.insertToDatabase("database", "mjerenja", "Tomo", brojac, "1.22", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "2.26", "26.22");
            brojac++;
            database.insertToDatabase("database", "mjerenja", "Tomo", brojac, "8.566", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "2.26", "28.5655");
            brojac++;
            database.insertToDatabase("database", "mjerenja", "Jelena", brojac, "5.5236555", "3", "3", "3", "3", "3", "3", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "2.26", "66.52455");
            brojac++;
            float i = database.selectAVGfromTable("database", "mjerenja", "Tomo", "TimeTyping");
            float j = i;
            i = database.selectAVGfromTable("database", "mjerenja", "Tomo", "A");
            j = i;
            i = database.selectAVGfromTable("database", "mjerenja", "Jelena", "TimeTyping");
            j = i;
            i = database.selectAVGfromTable("database", "mjerenja", "Jelena", "B");
            j = i;
            int broj = database.brojZapisaOsobe("database", "mjerenja", "Tomo");
            j = broj;
            bool nesto = database.provjera("database", "mjerenja", "Tomo");
            nesto = database.provjera("database", "mjerenja", "Tomo1");
            broj = database.brojZapisaOsobe("database", "mjerenja", "Tomo");

            broj = database.brojZapisaOsobe("database", "mjerenja", "Jelena");

            broj = database.brojZapisaOsobe("database", "mjerenja", "Tomo1");

            database.smanjiRedniBrojZapisa("database", "mjerenja", "Tomo");
            i = database.selectAVGfromTable("database", "mjerenja", "Tomo", "redniBrojZapisa");
            j = i;
            database.izbrisiRedak("database", "mjerenja", "Tomo", -1);
            i = database.selectAVGfromTable("database", "mjerenja", "Tomo", "redniBrojZapisa");
            broj = database.brojZapisaOsobe("database", "mjerenja", "Tomo");


            ViewBag.Message = "Home page jeeej!";

            return View();
        }

        public ActionResult Learn()
        {
            ViewBag.InDatabase = false;

            return View();
        }

        public ActionResult Recognize()
        {
            ViewBag.Message = "Recognize";

            return View();
        }
    }
}