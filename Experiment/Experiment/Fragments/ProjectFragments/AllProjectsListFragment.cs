using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.Model;
using Experiment.RestService;
using System.Collections.Generic;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Experiment.Fragments.ProjectFragments
{
    public class AllProjectsListFragment : SupportFragment
    {
        private List<Project> _projects;
        private RecyclerView _projectsRecyclerView;

        public override async void OnResume()
        {
            base.OnResume();
            if (_projects == null)
            {
                var restService = Refit.RestService.For<IRestServiceApiConsumer>("http://cjiohoed.pythonanywhere.com/api/");
                _projects = await restService.GetProjects();
                if (_projectsRecyclerView.GetAdapter() is ProjectRecyclerViewAdapter adapter) adapter.Refresh(_projects);
            }
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