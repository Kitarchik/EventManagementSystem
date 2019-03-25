using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Github.Aakira.Expandablelayout;
using Experiment.LogicLayer;
using Experiment.Model;
using System;
using System.Collections.Generic;

namespace Experiment.Fragments.RulesFragments
{
    public class RulesExpandableRecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Rules> ruleSet;
        private RulesSectionsFragment parent;

        public RulesExpandableRecyclerViewAdapter(RulesSectionsFragment parent, List<Rules> ruleSet)
        {
            this.parent = parent;
            this.ruleSet = ruleSet;
        }

        public override int ItemCount => ruleSet.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolderExpandableRule vh = holder as ViewHolderExpandableRule;
            vh.IsRecyclable = false;
            vh.ExpandableLayout.SetInRecyclerView(true);
            vh.Name.Text = ruleSet[position].Name;
            bool isSubCategory = RulesHelper.HasTwoSubLevels(ruleSet[position].ChildRules);
            if (isSubCategory)
            {
                vh.Icon.SetImageResource(Resource.Drawable.arrow_expand);
                SetUpInnerRecyclerView(vh.InnerRecyclerView, ruleSet[position].ChildRules);
            }
            else
            {
                vh.Icon.SetImageResource(Resource.Drawable.arrow_advance);
            }

            if (vh.ClickHandler != null)
                vh.View.Click -= vh.ClickHandler;

            vh.ClickHandler = new EventHandler((sender, e) => {
                if (isSubCategory)
                {
                    if (vh.ExpandableLayout.Expanded)
                    {
                        vh.Icon.SetImageResource(Resource.Drawable.arrow_expand);
                    }
                    else
                    {
                        vh.Icon.SetImageResource(Resource.Drawable.arrow_collapse);
                    }
                    vh.ExpandableLayout.Toggle();
                }
                else
                {
                    var activity = parent.Activity as MainActivity;
                    activity.LoadRulesSubsectionsFragment(ruleSet[position]);
                }
            });
            vh.View.Click += vh.ClickHandler;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rulesSectionsExpandableRow, parent, false);
            return new ViewHolderExpandableRule(itemView);
        }

        public void SetUpInnerRecyclerView(RecyclerView view, List<Rules> rules)
        {
            view.SetLayoutManager(new LinearLayoutManager(parent.Context));
            view.SetAdapter(new RulesInnerExpandableRecyclerViewAdapter(parent, rules));
        }
    }

    public class ViewHolderExpandableRule : RecyclerView.ViewHolder
    {
        public View View { get; set; }
        public TextView Name { get; set; }
        public ExpandableLinearLayout ExpandableLayout { get; set; }
        public RecyclerView InnerRecyclerView { get; set; }
        public ImageView Icon { get; set; }
        public EventHandler ClickHandler { get; set; }

        public ViewHolderExpandableRule(View itemView) : base(itemView)
        {
            View = itemView;
            Name = itemView.FindViewById<TextView>(Resource.Id.rulesExpandableHeader);
            ExpandableLayout = (ExpandableLinearLayout)itemView.FindViewById(Resource.Id.rulesExpandableLayout);
            InnerRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.rulesExpandableRecyclerView);
            Icon = itemView.FindViewById<ImageView>(Resource.Id.rulesSectionExpandableIcon);
        }
    }
}