using Android.OS;
using Android.Views;
using Android.Widget;
using Experiment.LogicLayer;
using Experiment.Model;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Experiment.Fragments.ProjectFragments
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
                    .Find(p => p.Name == savedInstanceState.GetString(nameof(_project.Name)));
            }
            base.OnCreate(savedInstanceState);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString(nameof(_project.Name), _project.Name);
            base.OnSaveInstanceState(outState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.projectViewFragment, container, false);
            TextView nameView = view.FindViewById<TextView>(Resource.Id.txtProjectName);
            TextView statusView = view.FindViewById<TextView>(Resource.Id.txtProjectStatus);
            TextView datesView = view.FindViewById<TextView>(Resource.Id.projectDatesTextView);
            TextView addToMyProjectsTextView = view.FindViewById<TextView>(Resource.Id.txtAddToMyProjects);
            Switch addToMyProjectsSwitch = view.FindViewById<Switch>(Resource.Id.addToMyProjectsSwitch);

            nameView.Text = _project.Name;
            statusView.Text = _project.Status;
            datesView.Text = _project.StartDate + " - " + _project.EndDate;

            return view;
        }
    }
}