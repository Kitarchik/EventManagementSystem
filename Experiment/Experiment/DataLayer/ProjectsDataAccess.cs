using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Experiment.Model;

namespace Experiment.DataLayer
{
    public static class ProjectsDataAccess
    {
        public static List<Project> DownloadProjects()
        {
            return new List<Project>()
            {
                new Project(){Name = "Днд 2018", StartDate = new DateTime(2018, 5, 9)},
                new Project(){Name = "Мордхейм 2019", StartDate = new DateTime(2019, 5, 10)},
                new Project(){Name = "Днд 2020", StartDate = new DateTime(2020, 5, 11)}
            };
        }
    }
}