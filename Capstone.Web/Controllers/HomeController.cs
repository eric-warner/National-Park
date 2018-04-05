using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAL _dal;
        public HomeController(IParkDAL dal)
        {
            _dal = dal;
        }

        // GET: Home
        public ActionResult Index()
        {
            if(Session["tempType"] == null)
            {
                Session["tempType"] = "F";
            }
            
            IList<Park> parks = _dal.GetAllParks();
            return View("Index", parks);
        }

        public ActionResult Detail(string parkCode)
        {
            if (parkCode == null)
            {
                return RedirectToAction("Index");
            }

            Park park = _dal.GetPark(parkCode);
            IList<Weather> weather = _dal.GetParkWeather(parkCode);

            ParkAndWeather parkAndWeather = new ParkAndWeather()
            {
                Park = park,
                Weather = weather
            };

            return View("Detail", parkAndWeather);
        }

        [HttpPost]
        public ActionResult ChangeTemp(bool temp)
        {
            if(!temp)
            {
                Session["tempType"] = "F";
            }
            else if (temp)
            {
                Session["tempType"] = "C";
            }

            return Redirect(Request.UrlReferrer.ToString() + "#weather");
        }
    }
}