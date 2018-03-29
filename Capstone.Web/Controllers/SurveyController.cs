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
            if (Session["tempType"] == null)
            {
                Session["tempType"] = "F";
            }

            IList<Park> parks = _dal.GetAllParks();
            return View("Index", parks);
        }

        [HttpPost]
        public ActionResult Submit(Survey survey)
        {
            if (Session["tempType"] == null)
            {
                Session["tempType"] = "F";
            }

            bool isSubmitted = _dal.AddSurvey(survey);

            if (isSubmitted)
            {
                IList<SurveyResult> results = _dal.GetSurveyResults();
                return RedirectToAction("Results", results);
            }

            return Index();
        }

        public ActionResult Results()
        {
            if (Session["tempType"] == null)
            {
                Session["tempType"] = "F";
            }

            IList<SurveyResult> results = _dal.GetSurveyResults();
            return View("Results", results);
        }
    }
}