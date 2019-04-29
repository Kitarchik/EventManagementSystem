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
        private int _projectId;
        private bool _deleteInProgress;
        private bool _saveInProgress;
        private ProjectsSqliteRepository _repository;

        public ProjectViewFragment() { }
        public ProjectViewFragment(Project project)
        {
            _project = project;
            _projectId = _project.Id;
            _inMyProjects = false;
        }
        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _repository = new ProjectsSqliteRepository(MainActivity.DbConnection);
            if (savedInstanceState != null)
            {
                _projectId = savedInstanceState.GetInt(nameof(_projectId));
                _inMyProjects = savedInstanceState.GetBoolean(nameof(_inMyProjects));
                if (_inMyProjects)
                {
                    _project = _repository.GetProject(_projectId);
                }
                else
                {
                    var restService =
                        Refit.RestService.For<IRestServiceApiConsumer>(Resources.GetString(Resource.String.api_address));
                    _project = await restService.GetProjectById(_projectId);
                }
            }
            else
            {
                _inMyProjects = _repository.GetProject(_projectId) != null;
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt(nameof(_projectId), _project.Id);
            outState.PutBoolean(nameof(_inMyProjects), _inMyProjects);
            if (_deleteInProgress)
            {
                _repository.DeleteProject(_project);
                _repository.DeleteRules(_project.Id);
            }

            if (_saveInProgress)
            {
                _repository.SaveProject(_project);
            }
            base.OnSaveInstanceState(outState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.projectViewFragment, container, false);
            TextView nameView = view.FindViewById<TextView>(Resource.Id.txtProjectName);
            TextView statusView = view.FindViewById<TextView>(Resource.Id.txtProjectStatus);
            TextView datesView = view.FindViewById<TextView>(Resource.Id.txtProjectDates);
            TextView addToMyProjectsTextView = view.FindViewById<TextView>(Resource.Id.txtAddToMyProjects);
            Switch addToMyProjectsSwitch = view.FindViewById<Switch>(Resource.Id.addToMyProjectsSwitch);
            Button makeCurrentProjectBtn = view.FindViewById<Button>(Resource.Id.makeCurrentProjectBtn);

            nameView.Text = _project.Name;
            statusView.Text = _project.Status;
            datesView.Text = _project.StartDate + " - " + _project.EndDate;
            addToMyProjectsTextView.Text = "Добавить в мои проекты: ";
            makeCurrentProjectBtn.Visibility = _inMyProjects ? ViewStates.Visible : ViewStates.Invisible;

            addToMyProjectsSwitch.Checked = _inMyProjects;
            addToMyProjectsSwitch.CheckedChange += async (sender, args) =>
            {
                if (args.IsChecked)
                {
                    _saveInProgress = true;
                    AlertDialog dialog = new EDMTDialogBuilder()
                        .SetContext(Activity)
                        .SetMessage("Please wait...")
                        .Build();

                    if (!dialog.IsShowing)
                    {
                        dialog.Show();
                    }

                    _repository.SaveProject(_project);
                    var restService =
                        Refit.RestService.For<IRestServiceApiConsumer>(Resources.GetString(Resource.String.api_address));
                    var rulesList = await restService.GetRulesByProjectId(_projectId);
                    var completeRulesList = new List<Rules>();
                    foreach (var rule in rulesList)
                    {
                        var completeRule = await restService.GetRuleById(rule.Id);
                        completeRulesList.Add(completeRule);
                    }

                    _repository.SaveRules(completeRulesList);

                    if (dialog.IsShowing)
                    {
                        dialog.Dismiss();
                    }

                    _inMyProjects = true;
                    makeCurrentProjectBtn.Visibility = ViewStates.Visible;
                }
                else
                {
                    _deleteInProgress = true;
                    AlertDialog dialog = new EDMTDialogBuilder()
                        .SetContext(Activity)
                        .SetMessage("Please wait...")
                        .Build();

                    if (!dialog.IsShowing)
                    {
                        dialog.Show();
                    }

                    _repository.DeleteProject(_project);
                    _repository.DeleteRules(_project.Id);

                    if (dialog.IsShowing)
                    {
                        dialog.Dismiss();
                    }

                    (Activity as MainActivity)?.DeactivateProjectSubmenu(_project);
                    _inMyProjects = false;
                    makeCurrentProjectBtn.Visibility = ViewStates.Invisible;
                }
            };

            makeCurrentProjectBtn.Click += (sender, args) =>
            {

                var activity = (MainActivity)Activity;
                activity.CurrentProject = _project;
                activity.ActivateProjectSubmenu(_project);
            };

            return view;
        }
    }
}