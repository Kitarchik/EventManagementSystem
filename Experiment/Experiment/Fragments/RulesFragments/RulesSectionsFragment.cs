using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.Model;

namespace Experiment.Fragments.RulesFragments
{
    public class RulesSectionsFragment : BaseRulesFragment
    {
        private RecyclerView _rulesExpandableRecyclerView;

        public RulesSectionsFragment() { }
        public RulesSectionsFragment(Rules rules) : base(rules) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.rulesSectionsFragment, container, false);
            _rulesExpandableRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.rulesExpandableRecyclerView);
            SetUpExpandableRules(_rulesExpandableRecyclerView);
            return view;
        }

        private void SetUpExpandableRules(RecyclerView view)
        {
            view.SetLayoutManager(new LinearLayoutManager(view.Context));
            view.SetAdapter(new RulesExpandableRecyclerViewAdapter(this, Rules.ChildRules));
        }
    }
}