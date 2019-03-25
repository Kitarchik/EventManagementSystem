using Experiment.DataLayer;
using Experiment.Model;
using System.Collections.Generic;

namespace Experiment.LogicLayer
{
    public static class ProjectsLogic
    {
        public static List<Project> DownloadProjects()
        {
            return ProjectsDataAccess.DownloadProjects();
        }
    }
}