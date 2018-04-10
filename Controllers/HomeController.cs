using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotingDojo.Models;
using DbConnection;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("quotes")]
        public IActionResult GetQuotes()
        {            
            ViewBag.allQuotes = DbConnector.Query("SELECT quote, name, created_at FROM quotes ORDER BY created_at DESC");

            return View("Quotes");
        }

        [HttpPost]
        [Route("quotes")]
        public IActionResult PostQuotes(string name, string quote)
        {
            if(name==null||quote==null){
                TempData["errors"] = "Name and Quote must not be blank!";
                return RedirectToAction("Index");
            }else{
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                DbConnector.Query($"INSERT INTO quotes (name, quote, created_at, updated_at) VALUES ('{name.Replace("'", "''")}', '{quote.Replace("'", "''")}', now(), now())");
                return RedirectToAction("GetQuotes");
            }
        }
    }
}
