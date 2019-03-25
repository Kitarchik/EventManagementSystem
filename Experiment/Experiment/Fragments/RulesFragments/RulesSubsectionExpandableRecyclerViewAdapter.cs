using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Github.Aakira.Expandablelayout;
using Experiment.Model;
using System;
using System.Collections.Generic;

namespace Experiment.Fragments.RulesFragments
{
    public class RulesSubsectionExpandableRecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Rules> ruleSet;
        private RulesOneSubsectionFragment parent;
        private readonly bool isExpanded;

        public RulesSubsectionExpandableRecyclerViewAdapter(RulesOneSubsectionFragment parent, 
            List<Rules> ruleSet, bool isExpanded)
        {
            this.parent = parent;
            this.ruleSet = ruleSet;
            this.isExpanded = isExpanded;
        }

        public override int ItemCount => ruleSet.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolderExpandableSubsectionRule vh = holder as ViewHolderExpandableSubsectionRule;
            vh.IsRecyclable = false;
            vh.ExpandableLayout.SetInRecyclerView(true);
            if (isExpanded)
            {
                vh.ExpandableLayout.Expanded = true;
            }

            vh.Name.Text = ruleSet[position].Name;
            vh.Description.Text = ruleSet[position].Description;
            if (ruleSet[position].Images != null && ruleSet[position].Images.Count > 0)
            {
                SetUpImagesRecyclerView(vh.ImagesRecyclerView, ruleSet[position].Images);
            }
            if (vh.ClickHandler != null)
                vh.View.Click -= vh.ClickHandler;

            vh.ClickHandler = new EventHandler((sender, e) =>
            {
                if (!isExpanded)
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
            });
            vh.View.Click += vh.ClickHandler;
        }

        private void SetUpImagesRecyclerView(RecyclerView imagesRecyclerView, List<ImageSigned> images)
        {
            imagesRecyclerView.SetLayoutManager(new LinearLayoutManager(parent.Context));
            imagesRecyclerView.SetAdapter(new RulesImagesRecyclerViewAdapter(parent, images));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rulesSubsectionsExpandableRow, parent, false);
            return new ViewHolderExpandableSubsectionRule(itemView);
        }
    }

    public class ViewHolderExpandableSubsectionRule : RecyclerView.ViewHolder
    {
        public View View { get; set; }
        public TextView Name { get; set; }
        public ExpandableLinearLayout ExpandableLayout { get; set; }
        public TextView Description { get; set; }
        public ImageView Icon { get; set; }
        public RecyclerView ImagesRecyclerView { get; set; }
        public EventHandler ClickHandler { get; set; }

        public ViewHolderExpandableSubsectionRule(View itemView) : base(itemView)
        {
            View = itemView;
            Name = itemView.FindViewById<TextView>(Resource.Id.rulesSubsectionExpandableHeader);
            ExpandableLayout = (ExpandableLinearLayout)itemView.FindViewById(Resource.Id.rulesSubsectionExpandableLayout);
            Description = itemView.FindViewById<TextView>(Resource.Id.rulesSubsectionExpandableDescription);
            Icon = itemView.FindViewById<ImageView>(Resource.Id.rulesSubsectionExpandableIcon);
            ImagesRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.rulesImagesRecyclerView);
        }
    }
}