using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
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
            return View();          
        }


        public ActionResult Recognize()
        {
            return View();
        }


        public ActionResult LearningForm()
        {
            return View();
        }


        [HttpGet]
        public ActionResult SaveUsername(string data)
        {
            //TODO clean user input
            var username = data;

            //TODO check if user is in database
            Boolean inDatabase = false;

            Boolean saved = false;

            if (!inDatabase)
            {
                //TODO save to db
                saved = true;
            }

            var result = new { saved = saved };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult SaveFirstInput(string data)
        {
            var serializer = new JavaScriptSerializer();
            var measuredInput = serializer.Deserialize<UserData>(data);

            //TODO parse user data
            //parseData(measuredInput);

            // save to db
            bool saved = true;

            var result = new { saved = saved };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public class UserInput
        {
            public string key_down { get; set; }
            public double time_down { get; set; }
            public string key_up { get; set; }
            public double? time_up { get; set; }
        }


        public class UserData
        {
            public List<UserInput> userInput { get; set; }
        }


        [HttpGet]
        public ActionResult ParseUserInput(string data)
        {
            var serializer = new JavaScriptSerializer();
            var measuredInput = serializer.Deserialize<UserData>(data);

            //TODO parse user data
            //parseData(measuredInput);

            //test ajax
            String username = "Arijana";
            double score = 90;

            var result = new { username = username, score = score};
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private void parseData(UserData measuredInput)
        {
            throw new NotImplementedException();
        }


    }
}