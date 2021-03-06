﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IParkDAL
    {
        IList<Park> GetAllParks();
        Park GetPark(string code);
        IList<Weather> GetParkWeather(string code);
        bool AddSurvey(Survey survey);
        IList<SurveyResult> GetSurveyResults();
    }
}
