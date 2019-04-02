using System.Collections.Generic;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.DataLayer;
using Experiment.Model;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Experiment.Fragments.ProjectFragments
{
    public class MyProjectListFragment : SupportFragment
    {
        private List<Project> _projects;
        private RecyclerView _projectsRecyclerView;
        private ProjectsSqliteRepository _repository;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _repository = new ProjectsSqliteRepository(MainActivity.DbConnection);
        }

        public override void OnResume()
        {
            base.OnResume();
            _projects = _repository.GetAllProjects();
            if (_projectsRecyclerView.GetAdapter() is ProjectRecyclerViewAdapter adapter) adapter.Refresh(_projects);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _projectsRecyclerView = inflater.Inflate(Resource.Layout.projectListFragment, container, false) as RecyclerView;
            SetUpRecyclerView(_projectsRecyclerView);
            return _projectsRecyclerView;
        }

        private void SetUpRecyclerView(RecyclerView projectsRecyclerView)
        {
            projectsRecyclerView.SetLayoutManager(new LinearLayoutManager(projectsRecyclerView.Context));
            projectsRecyclerView.SetAdapter(new ProjectRecyclerViewAdapter(Activity, _projects));
        }
    }
}