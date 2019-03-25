using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.LogicLayer;
using Experiment.Model;
using System.Collections.Generic;

namespace Experiment.Search
{
    public class RulesSearchFragment : BaseSearchFragment
    {
        private readonly List<string> ruleNames;
        private readonly AppCompatActivity parent;
        private RecyclerView rulesSearchRecyclerView;

        public RulesSearchFragment(Rules rules, AppCompatActivity parent)
        {
            ruleNames = RulesHelper.RulesNamesCompleteSet(rules);
            this.parent = parent;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.rulesSearchFragment, container, false);
            rulesSearchRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.rulesSearchRecyclerView);
            SetUpRecyclerView(rulesSearchRecyclerView);
            return view;
        }

        private void SetUpRecyclerView(RecyclerView view)
        {
            view.SetLayoutManager(new LinearLayoutManager(view.Context));
            var adapter = new RulesSearchRecyclerViewAdapter(ruleNames, this);
            var activity = parent as MainActivity;
            activity.searchAdapter = adapter;
            view.SetAdapter(adapter);
        }
    }
}