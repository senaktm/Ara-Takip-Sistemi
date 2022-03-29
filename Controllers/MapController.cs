using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using yazlab2_1.Models;


namespace yazlab2_1.Controllers
{
    public class MapController : Controller
    {
        string connection = "server=localhost;port=5432;Database=database;" +
              " user ID=postgres;" + "password=1234";

        static string carRadio="";
        static List<int> carID = new List<int>();
        DateTime start = new DateTime();
        DateTime finish = new DateTime();
        DateTime d;
        DateTime now;
        



        public IActionResult Map()
        {
            LoginDatabase Ld = new LoginDatabase();
            Car carInfo = new Car();
            string connection = "server=localhost;port=5432;Database=database;" +
              " user ID=postgres;" + "password=1234";
           
            IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("carDatabase");
            var collection = database.GetCollection<Car>("carCollection");
           
         /*   var carList = carInfo.ReadCSVFile();
            foreach (var item in carList)
            {
                collection.InsertOne(item);
            }*/
            // var productResult = collection.Find<Car>(a => true).ToList();
            //  return View(productResult);

          




            using (var connect = new NpgsqlConnection(connection))
            {
                try
                {
                    connect.Open();
                    NpgsqlDataReader reader;
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT car_id FROM userinfo WHERE user_id ='" + LoginInfo.user_id + "'", connect);
                    reader = cmd.ExecuteReader();
                    Console.WriteLine("Basarili");
                    while (reader.Read())
                    {
                        carID.Add(reader.GetInt32(0));

                    }



                }
                catch
                {

                }

            }
            List<Car> productResult2 = new List<Car>();
            ViewBag.ID = carID;
            now = DateTime.Now;
            ViewBag.now = now;
            productResult2 = collection.Find<Car>(a => carID.Contains(a.carID)).ToList();
            
            
            return View(productResult2);
        }


        [HttpPost]
        public IActionResult Map(string asdasd)
        {
            ViewBag.ID = carID;
            string car1=Request.Form["car1"];
           string car2 = Request.Form["car2"];
            var startBox = Request.Form["startBox"];
            var finishBox = Request.Form["finishBox"];
            now = DateTime.Now;
             ViewBag.now = now;




            List<Car> productResult2 = new List<Car>();
            IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("carDatabase");
            var collection = database.GetCollection<Car>("carCollection");
            if (car1=="car1")
            {
               
               
                if (DateTime.TryParse(startBox,out start) && DateTime.TryParse(finishBox,out finish))
                {
                   productResult2 = (List<Car>)collection.Find(a => a.date <= finish.AddHours(3) && a.date >= start.AddHours(3) && a.carID == carID[0]).ToList();
                  
                }
          
                else
                {
                   // productResult2 = collection.Find<Car>(a => carID.Contains(a.carID) && a.carID == carID[0]).ToList();
                    var lastTime = (List<Car>)collection.Find(a => a.carID == carID[0]).SortByDescending(a => a.id).Limit(1).ToList();
                    productResult2 = (List<Car>)collection.Find(a => a.date <= lastTime[0].date && a.date >= lastTime[0].date.AddMinutes(-30) && a.carID == carID[0]).ToList();
                }
            }
            else
            {
                if (DateTime.TryParse(startBox, out start) && DateTime.TryParse(finishBox, out finish))
                {
                     productResult2 = (List<Car>)collection.Find(a => a.date <= finish.AddHours(3) && a.date >= start.AddHours(3) && a.carID == carID[1]).ToList();

                }
                else
                {
                  // productResult2 = collection.Find<Car>(a => carID.Contains(a.carID) && a.carID == carID[1]).ToList();
                    var lastTime = (List<Car>)collection.Find(a => a.carID == carID[1]).SortByDescending(a => a.id).Limit(1).ToList();
                    productResult2 = (List<Car>)collection.Find(a => a.date <=lastTime[0].date && a.date >=lastTime[0].date.AddMinutes(-30) && a.carID==carID[1]).ToList();
                }

            

            }
            return View(productResult2);
        }

        [HttpPost]
        public ActionResult logout()
        {
            d = DateTime.Now;
            update(d);
            return RedirectToAction("Login", "Login");
        }
        public void update(DateTime d)
        {
            using (var connect = new NpgsqlConnection(connection))
            {


                try
                {
                    connect.Open();
                    NpgsqlDataReader reader;
                    NpgsqlCommand cmd = new NpgsqlCommand("Update userinfo SET logout_date='" + d + "' WHERE user_id ='" + LoginInfo.user_id + "'", connect);
                    reader = cmd.ExecuteReader();
                }
                catch { }
            }

        }



    }
}