using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.Model;
using System.Collections.Generic;
using System.Linq;

namespace Experiment.Fragments.RulesFragments
{
    public class RulesOneSubsectionFragment : BaseRulesFragment
    {
        private Rules rules;
        private RecyclerView rulesSubsectionExpandableRecyclerView;

        public RulesOneSubsectionFragment(Rules rules)
        {
            this.rules = rules;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.rulesOneSubsectionFragment, container, false);
            rulesSubsectionExpandableRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.rulesSubsectionExpandableRecyclerView);
            SetUpRecyclerView(rulesSubsectionExpandableRecyclerView);
            return view;
        }

        private void SetUpRecyclerView(RecyclerView view)
        {
            bool isOneRule = false;

            view.SetLayoutManager(new LinearLayoutManager(view.Context));
            var items = new List<Rules>();
            if (rules.ChildRules==null || rules.ChildRules.Count == 0)
            {
                items.Add(rules);
                DataLayer.RulesDataAccess.DownloadImages(Context, rules);
                isOneRule = true;
            }
            else if (!string.IsNullOrEmpty(rules.Description))
            {
                items.Add(new Rules() { Name = "Описание", Description = rules.Description });
            }

            var children = rules.ChildRules.Where(c => c.ChildRules == null || c.ChildRules.Count == 0).ToList();
            foreach (var child in children)
            {
                DataLayer.RulesDataAccess.DownloadImages(Context, child);
            }
            items.AddRange(children);

            view.SetAdapter(new RulesSubsectionExpandableRecyclerViewAdapter(this, items, isOneRule));
        }
    }
}