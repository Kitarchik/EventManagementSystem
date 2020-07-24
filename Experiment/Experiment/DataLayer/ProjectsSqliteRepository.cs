using System.Collections.Generic;
using System.Linq;
using Experiment.Model;
using SQLite;

namespace Experiment.DataLayer
{
    public class ProjectsSqliteRepository
    {
        private readonly SQLiteConnection _db;

        public ProjectsSqliteRepository(SQLiteConnection connection)
        {
            _db = connection;
            _db.CreateTable<Project>();
            _db.CreateTable<Rules>();
        }

        public void SaveProject(Project project)
        {
            if (project.Rules != null)
            {
                SaveRules(project.Rules.ChildRules);
            }
            _db.Insert(project);
        }

        public Project GetProject(int id)
        {
            return _db.Table<Project>().FirstOrDefault(x => x.Id == id);
        }

        public List<Project> GetAllProjects()
        {
            return _db.Table<Project>().ToList();
        }

        public void SaveRules(List<Rules> rules)
        {
            if (rules != null)
            {
                foreach (var rule in rules)
                {
                    _db.Insert(rule);
                    SaveRules(rule.ChildRules);
                }
            }
        }

        public Rules GetRulesForProject(int projectId)
        {
            var allRules = _db.Table<Rules>().Where(r => r.ProjectId == projectId).ToList();
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

        public void DeleteProject(Project project)
        {
            if (GetProject(project.Id) != null)
            {
                _db.Delete(project);
            }
        }

        public void DeleteRules(int projectId)
        {
            var rulesList = _db.Table<Rules>().Where(r => r.ProjectId == projectId).ToList();
            foreach (var rule in rulesList)
            {
                _db.Delete(rule);
            }
        }
    }
}