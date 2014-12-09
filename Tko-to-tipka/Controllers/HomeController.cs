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
            //test ajax
            String username = "Arijana";
            double score = 90;

            parseData(data);

            var result = new { username = username, score = score};
            return Json(result);
        }


        private void parseData(UserData measuredInput)
        {
            foreach (Input item in measuredInput.input)
            {
                var key_down = item.key_down;
                var key_up = item.key_up;
                var time_down = item.time_down;
                var time_up = item.time_up;
            }

            throw new NotImplementedException();
        }


    }
}