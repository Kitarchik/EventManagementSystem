using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.Fragments.RulesFragments;
using Experiment.LogicLayer;
using Experiment.Model;

namespace Experiment.Search
{
    public class RulesSearchFragment : BaseRulesFragment
    {
        private RecyclerView _rulesSearchRecyclerView;

        public RulesSearchFragment() { }
        public RulesSearchFragment(Rules rules) : base(rules) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.rulesSearchFragment, container, false);
            _rulesSearchRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.rulesSearchRecyclerView);
            SetUpRecyclerView(_rulesSearchRecyclerView);
            return view;
        }

        private void SetUpRecyclerView(RecyclerView view)
        {
            view.SetLayoutManager(new LinearLayoutManager(view.Context));
            var adapter = new RulesSearchRecyclerViewAdapter(Rules.RulesNamesCompleteSet(), this);
            if (Activity is MainActivity activity) activity.SearchAdapter = adapter;
            view.SetAdapter(adapter);
        }
    }
}