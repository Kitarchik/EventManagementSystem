using Android.OS;
using Android.Views;
using Android.Widget;
using Experiment.LogicLayer;
using Experiment.Model;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Experiment.Fragments
{
    public class ProjectViewFragment : SupportFragment
    {
        private Project _project;

        public ProjectViewFragment() { }
        public ProjectViewFragment(Project project)
        {
            _project = project;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
            {
                _project = ProjectsLogic.DownloadProjects()
                    .Find(p => p.Name == savedInstanceState.GetString("projectName"));
            }
            base.OnCreate(savedInstanceState);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("projectName", _project.Name);
            base.OnSaveInstanceState(outState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.projectViewFragment, container, false);
            TextView nameView = view.FindViewById<TextView>(Resource.Id.txtProjectName);
            TextView startDateView = view.FindViewById<TextView>(Resource.Id.txtProjectStartDate);
            nameView.Text = _project.Name;
            startDateView.Text = _project.StartDate.ToLongDateString();
            return view;
        }
    }
}