using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using TkoToTipka.Models;
using Tko_to_tipka.Models;



namespace Tko_to_tipka.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            //database.CreateDatabase("database");
            //database.CreateTable("database", "user");
            //int br = database.brojZapisaOsobe("database", "user", "Tomo");
           
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

        public ActionResult Test()
        {
            List<User> userList = ParseDbData.DohvatiSveUsere();
            List<TestModel> results = KNearestNeighbour.Test(userList);
            String accuracy = String.Format("{0:N2}", getAccuracy(results));
            ViewData["accuracy"] = accuracy;
            return View(results);
        }

        public ActionResult LearningForm()
        {
            return View();
        }

        public class Username
        {
            public string username { get; set; }
        }


        [HttpPost]
        public ActionResult SaveUsername(Username data)
        {
            //TODO clean user input
            var username = data.username;

            Boolean inDatabase = false;

            Boolean saved = false;

            if (!inDatabase)
            {
                //TODO save to db
                saved = true;
            }

            Session["Username"] = username;

            var result = new { saved = saved };
            return Json(result);
        }


        [HttpPost]
        public ActionResult SaveFirstInput(UserData data)
        {
            var username = (string)Session["Username"];

            string json = JsonConvert.SerializeObject(data);
            bool saved = true;

            try
            {
                int broj = database.brojZapisaOsobe("database", "user", username.ToString());
                database.insertToDatabase("database", "user", username.ToString(), broj, json);
            }
            catch 
            {
                saved = false;
            }

            var result = new { saved = saved };
            return Json(result);
        }


        public class Input
        {
            public string key_down { get; set; }
            public string time_down { get; set; }
            public string key_up { get; set; }
            public string time_up { get; set; }
        }

        public class UserData
        {
            public List<Input> input { get; set; }
        }

        [HttpPost]
        public ActionResult ParseUserInput(UserData data)
        {
            string json = JsonConvert.SerializeObject(data);
            List<User> userList = ParseDbData.DohvatiSveUsere();
            var query = ParseDbData.parseQuery(json);
            var username = KNearestNeighbour.Recognize(userList, query);
            var result = new { username = username };
            return Json(result);
        }


        private double getAccuracy(List<TestModel> results) 
        {
            int correct = 0;
            foreach (TestModel item in results)
            {
                if (item.correctName.Equals(item.recognizedName))
                    correct++;
            }
            return (correct / (double)results.Count()) * 100;
        }



    }
}