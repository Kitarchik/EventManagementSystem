using Android.OS;
using Experiment.LogicLayer;
using Experiment.Model;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Experiment.Fragments.RulesFragments
{
    public class BaseRulesFragment : SupportFragment
    {
        protected Rules Rules;

        public BaseRulesFragment() { }
        public BaseRulesFragment(Rules rules)
        {
            Rules = rules;
        }
        public override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("rulesName", Rules.Name);
            base.OnSaveInstanceState(outState);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
            {
                var rulesName = savedInstanceState.GetString("rulesName");
                Rules = (Activity as MainActivity)?.CurrentProject.ProjectRules.FindRuleByName(rulesName);
            }
            base.OnCreate(savedInstanceState);
        }
    }
}