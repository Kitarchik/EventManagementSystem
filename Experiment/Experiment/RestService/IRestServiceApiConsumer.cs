using System.Collections.Generic;
using System.Threading.Tasks;
using Experiment.Model;
using Refit;

namespace Experiment.RestService
{
    public interface IRestServiceApiConsumer
    {
        [Get("/projects")]
        Task<List<Project>> GetProjects();

        [Get("/projects/{id}")]
        Task<Project> GetProjectById(int id);

        [Get("/rules/{id}")]
        Task<Rules> GetRuleById(int id);

        [Get("/rules/project/{id}")]
        Task<List<Rules>> GetRulesByProjectId(int id);
    }
}