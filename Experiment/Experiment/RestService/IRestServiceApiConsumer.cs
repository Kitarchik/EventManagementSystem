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
    }
}