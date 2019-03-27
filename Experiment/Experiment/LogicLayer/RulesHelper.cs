using Experiment.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Android.Content.Res;

namespace Experiment.LogicLayer
{
    public static class RulesHelper
    {
        public static bool HasTwoSubLevels(List<Rules> rules)
        {
            return rules.Any(r => r.ChildRules.Any(c => c.ChildRules != null && c.ChildRules.Count > 0));
        }

        public static bool IsCategory(this Rules rules)
        {
            return rules.ChildRules.Any(r => HasTwoSubLevels(r.ChildRules));
        }

        public static List<string> RulesNamesCompleteSet(this Rules rules)
        {
            List<string> result = new List<string> {rules.Name};
            foreach (var child in rules.ChildRules)
            {
                result.AddRange(RulesNamesCompleteSet(child));
            }
            result.Sort();
            return result;
        }

        public static Rules FindRuleByName(this Rules rules, string name)
        {
            if (rules.Name == name)
            {
                return rules;
            }

            foreach (var child in rules.ChildRules)
            {
                var rule = child.FindRuleByName(name);
                if (rule != null)
                {
                    return rule;
                }
            }

            return null;
        }

        public static Rules DownloadRules(AssetManager assets)
        {
            Rules rules;
            using (StreamReader sr = new StreamReader(assets.Open("Rules.xml")))
            {
                XmlSerializer s = new XmlSerializer(typeof(Rules));
                rules = (Rules)s.Deserialize(sr);
            }

            return rules;
        }
    }
}