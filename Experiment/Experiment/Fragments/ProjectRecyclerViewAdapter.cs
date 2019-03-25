using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Experiment.Model;
using System;
using System.Collections.Generic;

namespace Experiment.Fragments
{
    public class ProjectRecyclerViewAdapter : RecyclerView.Adapter
    {
        private Android.App.Activity parent;
        private List<Project> projects;

        public Project GetValueAt(int position)
        {
            return projects[position];
        }
        public ProjectRecyclerViewAdapter(Android.App.Activity context, List<Project> projects)
        {
            parent = context;
            this.projects = projects;
        }

        public override int ItemCount => projects.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolderProject vh = holder as ViewHolderProject;
            vh.Name.Text = projects[position].Name;
            vh.StartDate.Text = projects[position].StartDate.ToLongDateString();
            if (vh.ClickHandler != null)
                vh.View.Click -= vh.ClickHandler;

            vh.ClickHandler = new EventHandler((sender, e) => {
                var activity = parent as MainActivity;
                activity.LoadProjectViewFragment(projects[position]);
            });
            vh.View.Click += vh.ClickHandler;
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