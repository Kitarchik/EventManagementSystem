using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Experiment.Model;
using SQLite;

namespace Experiment.DataLayer
{
    public class ProjectsSqliteRepository
    {
        public static readonly string DbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "projects.db");

        private readonly SQLiteAsyncConnection _db;

        protected static ProjectsSqliteRepository Me;

        static ProjectsSqliteRepository()
        {
            Me = new ProjectsSqliteRepository();
        }

        protected ProjectsSqliteRepository()
        {
            _db = new SQLiteAsyncConnection(DbFilePath);
            _db.CreateTableAsync<Project>().Wait();
            _db.CreateTableAsync<Rules>().Wait();
        }

        public static async Task<int> SaveProject(Project project)
        {
            if (project.Rules != null)
            {
                await SaveRules(project.Rules.ChildRules);
            }
            return await Me._db.InsertAsync(project);
        }

        public static async Task<Project> GetProject(int id)
        {
            return await Me._db.GetAsync<Project>(p => p.Id == id);
        }

        public static async Task<List<Project>> GetAllProjects()
        {
            return await Me._db.Table<Project>().ToListAsync();
        }

        public static async Task<int> SaveRules(List<Rules> rules)
        {
            if (rules != null)
            {
                foreach (var rule in rules)
                {
                    await Me._db.InsertAsync(rule);
                    await SaveRules(rule.ChildRules);
                }
            }
            return 0;
        }

        public static async Task<Rules> GetRulesForProject(int projectId)
        {
            var allRules = await Me._db.Table<Rules>().Where(r => r.ProjectId == projectId).ToListAsync();
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

        private static List<Rules> GetChildRulesForRule(int ruleId, List<Rules> allRules)
        {
            var childRules = allRules.Where(r => r.ParentId == ruleId).ToList();
            foreach (var rule in childRules)
            {
                rule.ChildRules = GetChildRulesForRule(rule.Id, allRules);
            }

            return childRules;
        }

        public static async Task<int> DeleteProject(Project project)
        {
            return await Me._db.DeleteAsync(project);
        }

        public static async Task<int> DeleteRules(int projectId)
        {
            int lastDeletedId = 0;
            var rulesList = await Me._db.Table<Rules>().Where(r => r.ProjectId == projectId).ToListAsync();
            foreach (var rule in rulesList)
            {
                lastDeletedId = await Me._db.DeleteAsync(rule);
            }

            return lastDeletedId;
        }
    }
}