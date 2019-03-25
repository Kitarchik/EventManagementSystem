using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.Model;

namespace Experiment.Fragments.RulesFragments
{
    public class RulesSectionsFragment : BaseRulesFragment
    {
        private Rules rules;
        private RecyclerView rulesExpandableRecyclerView;

        public RulesSectionsFragment(Rules rules)
        {
            this.rules = rules;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.rulesSectionsFragment, container, false);
            rulesExpandableRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.rulesExpandableRecyclerView);
            SetUpExpandableRules(rulesExpandableRecyclerView);
            return view;
        }

        private void SetUpExpandableRules(RecyclerView view)
        {
            view.SetLayoutManager(new LinearLayoutManager(view.Context));
            view.SetAdapter(new RulesExpandableRecyclerViewAdapter(this, rules.ChildRules));
        }
    }
}