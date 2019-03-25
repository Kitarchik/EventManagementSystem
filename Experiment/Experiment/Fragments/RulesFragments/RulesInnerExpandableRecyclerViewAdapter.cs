using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Experiment.Model;
using System;
using System.Collections.Generic;

namespace Experiment.Fragments.RulesFragments
{
    public class RulesInnerExpandableRecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Rules> ruleSet;
        private RulesSectionsFragment parent;

        public RulesInnerExpandableRecyclerViewAdapter(RulesSectionsFragment parent, List<Rules> ruleSet)
        {
            this.parent = parent;
            this.ruleSet = ruleSet;
        }

        public override int ItemCount => ruleSet.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolderInnerRecyclerRule vh = holder as ViewHolderInnerRecyclerRule;
            vh.Name.Text = ruleSet[position].Name;
            if (vh.ClickHandler != null)
                vh.View.Click -= vh.ClickHandler;

            vh.ClickHandler = new EventHandler((sender, e) => {
                var activity = parent.Activity as MainActivity;
                activity.LoadRulesSubsectionsFragment(ruleSet[position]);
            });
            vh.View.Click += vh.ClickHandler;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rulesSectionsInnerExpandableRow, parent, false);
            return new ViewHolderInnerRecyclerRule(itemView);
        }
    }

    public class ViewHolderInnerRecyclerRule : RecyclerView.ViewHolder
    {
        public View View { get; set; }
        public TextView Name { get; set; }
        public EventHandler ClickHandler { get; set; }

        public ViewHolderInnerRecyclerRule(View itemView) : base(itemView)
        {
            View = itemView;
            Name = ItemView.FindViewById<TextView>(Resource.Id.rulesInnerExpandableView);
        }
    }
}