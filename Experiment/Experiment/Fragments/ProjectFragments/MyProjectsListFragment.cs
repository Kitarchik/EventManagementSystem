﻿using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.LogicLayer;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Experiment.Fragments.ProjectFragments
{
    public class MyProjectListFragment : SupportFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var projectsRecyclerView = inflater.Inflate(Resource.Layout.projectListFragment, container, false) as RecyclerView;
            SetUpRecyclerView(projectsRecyclerView);
            return projectsRecyclerView;
        }

        private void SetUpRecyclerView(RecyclerView projectsRecyclerView)
        {
            var projects = ProjectsLogic.DownloadProjects();
            projectsRecyclerView.SetLayoutManager(new LinearLayoutManager(projectsRecyclerView.Context));
            projectsRecyclerView.SetAdapter(new ProjectRecyclerViewAdapter(Activity, projects));
        }
    }
}