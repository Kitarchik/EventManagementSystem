using System.Collections.Generic;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using EDMTDialog;
using Experiment.DataLayer;
using Experiment.Model;
using Experiment.RestService;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Experiment.Fragments.ProjectFragments
{
    public class ProjectViewFragment : SupportFragment
    {
        private Project _project;
        private bool _inMyProjects;
        private ProjectsSqliteRepository _repository;

        public ProjectViewFragment() { }
        public ProjectViewFragment(Project project)
        {
            _project = project;
            _inMyProjects = false;
        }
        public override async void OnCreate(Bundle savedInstanceState)
        {
            _repository = new ProjectsSqliteRepository(MainActivity.DbConnection);
            if (savedInstanceState != null)
            {
                int projectId = savedInstanceState.GetInt(nameof(_project.Id));
                _inMyProjects = savedInstanceState.GetBoolean(nameof(_inMyProjects));
                if (_inMyProjects)
                {
                    _project = await _repository.GetProject(projectId);
                }
                else
                {
                    var restService =
                        Refit.RestService.For<IRestServiceApiConsumer>(Resources.GetString(Resource.String.api_address));
                    _project = await restService.GetProjectById(projectId);
                }
            }
            else
            {
                _inMyProjects = await _repository.GetProject(_project.Id) != null;
            }
            base.OnCreate(savedInstanceState);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt(nameof(_project.Id), _project.Id);
            outState.PutBoolean(nameof(_inMyProjects), _inMyProjects);
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
            addToMyProjectsTextView.Text = "Добавить в мои проекты: ";

            addToMyProjectsSwitch.Checked = _inMyProjects;
            addToMyProjectsSwitch.CheckedChange += async (sender, args) =>
            {
                if (args.IsChecked)
                {
                    AlertDialog dialog = new EDMTDialogBuilder()
                        .SetContext(Activity)
                        .SetMessage("Please wait...")
                        .Build();

                    if (!dialog.IsShowing)
                    {
                        dialog.Show();
                    }

                    int projectId = await _repository.SaveProject(_project);
                    var restService =
                        Refit.RestService.For<IRestServiceApiConsumer>(Resources.GetString(Resource.String.api_address));
                    var rulesList = await restService.GetRulesByProjectId(projectId);
                    var completeRulesList = new List<Rules>();
                    foreach (var rule in rulesList)
                    {
                        var completeRule = await restService.GetRuleById(rule.Id);
                        completeRulesList.Add(completeRule);
                    }

                    await _repository.SaveRules(completeRulesList);
                    (Activity as MainActivity)?.ActivateProjectSubmenu(_project);

                    if (dialog.IsShowing)
                    {
                        dialog.Dismiss();
                    }
                }
                else
                {
                    AlertDialog dialog = new EDMTDialogBuilder()
                        .SetContext(Activity)
                        .SetMessage("Please wait...")
                        .Build();

                    if (!dialog.IsShowing)
                    {
                        dialog.Show();
                    }

                    await _repository.DeleteProject(_project);
                    await _repository.DeleteRules(_project.Id);
                    (Activity as MainActivity)?.DeactivateProjectSubmenu(_project.Id);

                    if (dialog.IsShowing)
                    {
                        dialog.Dismiss();
                    }
                }
            };

            return view;
        }
    }
}