using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private IParkDAL _dal;
        public SurveyController(IParkDAL dal)
        {
            _dal = dal;
        } 

        public ActionResult Index()
        {
            IList<Park> parks = _dal.GetAllParks();
            return View("Index", parks);
        }
    }
}