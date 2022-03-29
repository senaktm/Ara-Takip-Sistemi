using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using yazlab2_1.Models;

namespace yazlab2_1.Controllers
{
    
    public class LoginController : Controller
    {
        static int count=0;
        static int attempt = 3;
        [HttpGet]
        
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public RedirectResult Login(string username, string password)
        { 

            LoginDatabase db = new LoginDatabase();
            LoginInfo.username = Request.Form["username"];
            LoginInfo.password = Request.Form["password"];
            if (db.LoginCheck())
            {
                count = 0;
                attempt = 3;

              
                return Redirect("Map/Map");
                
            }
            else
            {
                if (count < 3)
                {
                    TempData.Clear();
                    attempt--;
                    TempData["msg"] = "Invalid username or password \r\nYou have left " + attempt + " attempt";
                    count++;
                }
               
                

               else if (count == 3)
                {
                   
                    TempData.Clear();
                    TempData["msg"] = "You tryed 3 times. Please wait a while";
                    

                    count = 0;
                    attempt = 3;
               


                }
               

            }
            return Redirect("/");
            
        }


    }
}
