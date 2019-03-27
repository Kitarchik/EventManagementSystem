using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Experiment.Model;

namespace Experiment.Fragments.ProjectFragments
{
    public class ProjectRecyclerViewAdapter : RecyclerView.Adapter
    {
        private readonly Android.App.Activity _parent;
        private readonly List<Project> _projects;

        public Project GetValueAt(int position)
        {
            return _projects[position];
        }
        public ProjectRecyclerViewAdapter(Android.App.Activity context, List<Project> projects)
        {
            _parent = context;
            _projects = projects;
        }

        public override int ItemCount => _projects.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is ViewHolderProject vh)
            {
                vh.Name.Text = _projects[position].Name;
                vh.StartDate.Text = _projects[position].StartDate.ToLongDateString();
                if (vh.ClickHandler != null)
                    vh.View.Click -= vh.ClickHandler;

                vh.ClickHandler = (sender, e) =>
                {
                    if (_parent is MainActivity activity) activity.LoadProjectViewFragment(_projects[position]);
                };
                vh.View.Click += vh.ClickHandler;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.projectRow, parent, false);
            return new ViewHolderProject(itemView);
        }
    }

    public class ViewHolderProject : RecyclerView.ViewHolder
    {
        public View View { get; set; }
        public TextView Name { get; set; }
        public TextView StartDate { get; set; }
        public EventHandler ClickHandler { get; set; }

        public ViewHolderProject(View itemView) : base(itemView)
        {
            View = itemView;
            Name = ItemView.FindViewById<TextView>(Resource.Id.projectName);
            StartDate = itemView.FindViewById<TextView>(Resource.Id.projectStartDate);
        }
    }
}