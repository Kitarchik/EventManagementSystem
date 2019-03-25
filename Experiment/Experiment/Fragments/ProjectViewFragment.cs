using Android.OS;
using Android.Views;
using Android.Widget;
using Experiment.Model;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Experiment.Fragments
{
    public class ProjectViewFragment : SupportFragment
    {
        private Project project;

        public ProjectViewFragment(Project project)
        {
            this.project = project;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.projectViewFragment, container, false);
            TextView nameView = view.FindViewById<TextView>(Resource.Id.txtProjectName);
            TextView startDateView = view.FindViewById<TextView>(Resource.Id.txtProjectStartDate);
            nameView.Text = project.Name;
            startDateView.Text = project.StartDate.ToLongDateString();
            return view;
        }
    }
}