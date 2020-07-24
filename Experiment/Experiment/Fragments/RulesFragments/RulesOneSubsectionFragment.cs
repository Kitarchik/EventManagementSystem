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
        private RecyclerView _rulesSubsectionExpandableRecyclerView;

        public RulesOneSubsectionFragment() { }
        public RulesOneSubsectionFragment(Rules rules) : base(rules) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.rulesOneSubsectionFragment, container, false);
            _rulesSubsectionExpandableRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.rulesSubsectionExpandableRecyclerView);
            SetUpRecyclerView(_rulesSubsectionExpandableRecyclerView);
            return view;
        }

        private void SetUpRecyclerView(RecyclerView view)
        {
            bool isOneRule = false;

            view.SetLayoutManager(new LinearLayoutManager(view.Context));
            var items = new List<Rules>();

            if (Rules.ChildRules == null || Rules.ChildRules.Count == 0)
            {
                items.Add(Rules);
                DataLayer.RulesDataAccess.DownloadImages(Context, Rules);
                isOneRule = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(Rules.Content))
                {
                    items.Add(new Rules { Name = "Описание", Content = Rules.Content });
                }

                var children = Rules.ChildRules.Where(c => c.ChildRules == null || c.ChildRules.Count == 0).ToList();
                foreach (var child in children)
                {
                    DataLayer.RulesDataAccess.DownloadImages(Context, child);
                }
                items.AddRange(children);
            }

            view.SetAdapter(new RulesSubsectionExpandableRecyclerViewAdapter(this, items, isOneRule));
        }
    }
}