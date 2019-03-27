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
        private List<string> _originalData;
        private List<string> _ruleNames;
        private readonly RulesSearchFragment _parent;
        
        public Filter Filter { get; }

        public RulesSearchRecyclerViewAdapter(List<string> names, RulesSearchFragment parent)
        {
            _ruleNames = names;
            _parent = parent;
            Filter = new RulesFilter(this);
        }

        public override int ItemCount => _ruleNames.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is RulesSearchHolder vh)) return;
            vh.Name.Text = _ruleNames[position];
            if (vh.ClickHandler != null)
                vh.View.Click -= vh.ClickHandler;

            vh.ClickHandler = (sender, e) =>
            {
                if (_parent.Activity is MainActivity activity)
                {
                    var rule = activity.CurrentProject.ProjectRules.FindRuleByName(_ruleNames[position]);
                    if (rule.IsCategory())
                    {
                        activity.LoadRulesSectionsFragment(rule);
                    }
                    else
                    {
                        activity.LoadRulesSubsectionsFragment(rule);
                    }
                }
            };
            vh.View.Click += vh.ClickHandler;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rulesSearchRow, parent, false);
            return new RulesSearchHolder(itemView);
        }

        private class RulesFilter : Filter
        {
            private readonly RulesSearchRecyclerViewAdapter _adapter;

            public RulesFilter(RulesSearchRecyclerViewAdapter adapter)
            {
                _adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<string>();
                if (_adapter._originalData == null)
                {
                    _adapter._originalData = _adapter._ruleNames;
                }
                if (constraint == null)
                {
                    return returnObj;
                }
                if (_adapter._originalData!=null && _adapter._originalData.Any())
                {
                    results.AddRange(_adapter._originalData.Where(name => name.ToLower().Contains(constraint.ToString())));
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
                    _adapter._ruleNames = values.ToArray<Java.Lang.String>()
                        .Select(name => (string)name).ToList();
                }

                _adapter.NotifyDataSetChanged();
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