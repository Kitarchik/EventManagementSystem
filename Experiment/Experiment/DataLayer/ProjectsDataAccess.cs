using Experiment.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public static async Task<bool> IsInMyProjects(Project project)
        {
            return await ProjectsSqliteRepository.GetProject(project.Id) != null;
        }

        public static async Task<int> SaveProject(Project project)
        {
            return await ProjectsSqliteRepository.SaveProject(project);
        }

        public static async Task<List<Project>> GetAllProjects()
        {
            return await ProjectsSqliteRepository.GetAllProjects();
        }

        public static async Task<Project> GetProject(int id)
        {
            return await ProjectsSqliteRepository.GetProject(id);
        }

        public static async Task<Rules> GetRulesByProjectId(int id)
        {
            return await ProjectsSqliteRepository.GetRulesForProject(id);
        }

        public static async Task<int> SaveRules(List<Rules> rules)
        {
            return await ProjectsSqliteRepository.SaveRules(rules);
        }

        public static async Task<int> DeleteProject(Project project)
        {
            int projectId = await ProjectsSqliteRepository.DeleteProject(project);
            return await ProjectsSqliteRepository.DeleteRules(projectId);
        }
    }
}