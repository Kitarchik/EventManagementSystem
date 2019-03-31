using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Experiment.Model;
using SQLite;

namespace Experiment.DataLayer
{
    public class ProjectsSqliteRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public ProjectsSqliteRepository(SQLiteAsyncConnection connection)
        {
            _db = connection;
            _db.CreateTableAsync<Project>();
            _db.CreateTableAsync<Rules>();
        }

        public async Task<int> SaveProject(Project project)
        {
            if (project.Rules != null)
            {
                await SaveRules(project.Rules.ChildRules);
            }
            return await _db.InsertAsync(project);
        }

        public async Task<Project> GetProject(int id)
        {
            return await _db.GetAsync<Project>(p => p.Id == id);
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _db.Table<Project>().ToListAsync();
        }

        public async Task<int> SaveRules(List<Rules> rules)
        {
            if (rules != null)
            {
                foreach (var rule in rules)
                {
                    await _db.InsertAsync(rule);
                    await SaveRules(rule.ChildRules);
                }
            }
            return 0;
        }

        public async Task<Rules> GetRulesForProject(int projectId)
        {
            var allRules = await _db.Table<Rules>().Where(r => r.ProjectId == projectId).ToListAsync();
            var mainRule = new Rules
            {
                ProjectId = projectId,
                ChildRules = allRules.Where(r=>r.ParentId==null).ToList(),
            };
            foreach (var childRule in mainRule.ChildRules)
            {
                childRule.ChildRules = GetChildRulesForRule(childRule.Id, allRules);
            }

            return mainRule;
        }

        private List<Rules> GetChildRulesForRule(int ruleId, List<Rules> allRules)
        {
            var childRules = allRules.Where(r => r.ParentId == ruleId).ToList();
            foreach (var rule in childRules)
            {
                rule.ChildRules = GetChildRulesForRule(rule.Id, allRules);
            }

            return childRules;
        }

        public async Task<int> DeleteProject(Project project)
        {
            return await _db.DeleteAsync(project);
        }

        public async Task<int> DeleteRules(int projectId)
        {
            int lastDeletedId = 0;
            var rulesList = await _db.Table<Rules>().Where(r => r.ProjectId == projectId).ToListAsync();
            foreach (var rule in rulesList)
            {
                lastDeletedId = await _db.DeleteAsync(rule);
            }

            return lastDeletedId;
        }
    }
}