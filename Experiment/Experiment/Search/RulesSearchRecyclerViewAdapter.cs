using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Experiment.LogicLayer;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiment.Search
{
    public class RulesSearchRecyclerViewAdapter : RecyclerView.Adapter, IFilterable
    {
        private List<string> originalData;
        private List<string> ruleNames;
        private readonly RulesSearchFragment parent;
        
        public Filter Filter { get; private set; }

        public RulesSearchRecyclerViewAdapter(List<string> names, RulesSearchFragment parent)
        {
            ruleNames = names;
            this.parent = parent;
            Filter = new RulesFilter(this);
        }

        public override int ItemCount => ruleNames.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RulesSearchHolder vh = holder as RulesSearchHolder;
            vh.Name.Text = ruleNames[position];
            if (vh.ClickHandler != null)
                vh.View.Click -= vh.ClickHandler;

            vh.ClickHandler = new EventHandler((sender, e) =>
            {
                var activity = parent.Activity as MainActivity;
                var rule = activity.currentProject.ProjectRules.FindRuleByName(ruleNames[position]);
                if (rule.IsCategory())
                {
                    activity.LoadRulesSectionsFragment(rule);
                }
                else
                {
                    activity.LoadRulesSubsectionsFragment(rule);
                }
            });
            vh.View.Click += vh.ClickHandler;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rulesSearchRow, parent, false);
            return new RulesSearchHolder(itemView);
        }

        private class RulesFilter : Filter
        {
            private readonly RulesSearchRecyclerViewAdapter adapter;

            public RulesFilter(RulesSearchRecyclerViewAdapter adapter)
            {
                this.adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<string>();
                if (adapter.originalData == null)
                {
                    adapter.originalData = adapter.ruleNames;
                }
                if (constraint == null)
                {
                    return returnObj;
                }
                if (adapter.originalData!=null && adapter.originalData.Any())
                {
                    results.AddRange(adapter.originalData.Where(name => name.ToLower().Contains(constraint.ToString())));
                }

                returnObj.Values = FromArray(results.Select(r => new Java.Lang.String(r)).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                {
                    adapter.ruleNames = values.ToArray<Java.Lang.String>()
                        .Select(name => (string)name).ToList();
                }

                adapter.NotifyDataSetChanged();
                constraint.Dispose();
                results.Dispose();
            }
        }
    }

    public class RulesSearchHolder : RecyclerView.ViewHolder
    {
        public View View { get; set; }
        public TextView Name { get; set; }
        public EventHandler ClickHandler { get; set; }

        public RulesSearchHolder(View itemView) : base(itemView)
        {
            View = itemView;
            Name = itemView.FindViewById<TextView>(Resource.Id.rulesSearchTextView);
        }
    }
}