using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.Fragments.RulesFragments;
using Experiment.Model;
using Experiment.Search;
using System;
using System.IO;
using System.Linq;
using Experiment.DataLayer;
using Experiment.Fragments.ProjectFragments;
using SQLite;
using Environment = System.Environment;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Experiment
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.DesignDemo", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static SQLiteAsyncConnection DbConnection;
        private ProjectsSqliteRepository _repository;
        private static readonly string DbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "projects.db3");
        private DrawerLayout _drawerLayout;
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;
        private IMenuItem _searchMenuItem;
        private SearchView _searchView;
        private string _searchQuery = string.Empty;

        public Project CurrentProject { get; set; }
        public Android.Widget.IFilterable SearchAdapter { get; set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            DbConnection = new SQLiteAsyncConnection(DbFilePath);
            _repository = new ProjectsSqliteRepository(DbConnection);
            SetContentView(Resource.Layout.activity_main);

            SupportToolbar toolbar = FindViewById<SupportToolbar>(Resource.Id.mainToolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            if (_navigationView != null)
            {
                SetupDrawerContent(_navigationView);
            }

            var projectId = savedInstanceState?.GetInt(nameof(CurrentProject.Id));
            var previousMenuItem = savedInstanceState?.GetInt("menuItem");
            if (projectId != null)
            {
                CurrentProject = await _repository.GetProject((int)projectId);
                CurrentProject.Rules = await _repository.GetRulesForProject((int)projectId);
                ActivateProjectSubmenu(CurrentProject);
            }

            if (previousMenuItem != null && _navigationView !=null)
            {
                 _previousMenuItem = _navigationView.Menu.FindItem((int)previousMenuItem);
                _previousMenuItem.SetChecked(true);
            }

            _searchQuery = savedInstanceState?.GetString("searchQuery") ?? string.Empty;

            base.OnCreate(savedInstanceState);
            if (savedInstanceState == null)
            {
                LoadMyProjectsListFragment();
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (CurrentProject != null)
            {
                outState.PutInt(nameof(CurrentProject.Id),CurrentProject.Id);
            }

            if (_previousMenuItem != null)
            {
                outState.PutInt("menuItem", _previousMenuItem.ItemId);
            }

            if (_searchView != null)
            {
                outState.PutString("searchQuery", _searchView.Query);
            }

            base.OnSaveInstanceState(outState);
        }

        private void SetupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                _previousMenuItem?.SetChecked(false);

                e.MenuItem.SetChecked(true);
                _previousMenuItem = e.MenuItem;
                ShowFragment(e.MenuItem.ItemId);
                _drawerLayout.CloseDrawers();
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbarMenu, menu);
            _searchMenuItem = menu.FindItem(Resource.Id.action_search);
            _searchView = (SearchView)_searchMenuItem.ActionView;
            _searchView.MaxWidth = int.MaxValue;
            _searchMenuItem.SetOnActionExpandListener(new SearchExpandListener(this));

            _searchView.QueryTextChange += (s, e) =>
            {
                SearchAdapter?.Filter.InvokeFilter(e.NewText);
            };

            _searchView.QueryTextSubmit += (s, e) =>
            {
                if (SearchAdapter != null)
                {
                    e.Handled = true;
                }
            };

            if (!string.IsNullOrEmpty(_searchQuery))
            {
                _searchMenuItem.ExpandActionView();
                _searchView.Post(() => _searchView.SetQuery(_searchQuery, false));
            }

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private async void ShowFragment(int id)
        {
            switch (id)
            {
                case Resource.Id.nav_allProjects:
                {
                    LoadAllProjectsListFragment();
                    break;
                }
                case Resource.Id.nav_myProjects:
                {
                    LoadMyProjectsListFragment();
                    break;
                }
                case Resource.Id.rules:
                {
                    if (CurrentProject != null)
                    {
                        if (CurrentProject.Rules == null)
                        {
                            CurrentProject.Rules = await _repository.GetRulesForProject(CurrentProject.Id);
                        }

                        PopFragmentsOfType(typeof(BaseRulesFragment));
                        LoadRulesSectionsFragment(CurrentProject.Rules);
                    }
                    break;
                }
            }
        }

        private void RefreshSearch()
        {
            if (_searchView != null && !_searchView.Iconified)
            {
                _searchMenuItem.CollapseActionView();
            }
        }

        public void PopFragmentsOfType(Type type)
        {
            while (SupportFragmentManager.Fragments.Last().GetType().IsSubclassOf(type))
            {
                SupportFragmentManager.PopBackStackImmediate();
            }
        }

        public void LoadProjectViewFragment(Project project)
        {
            var projectViewFragment = new ProjectViewFragment(project);
            PushFragment(projectViewFragment);
        }

        public void ActivateProjectSubmenu(Project project)
        {
            var projectMenu = _navigationView.Menu.FindItem(Resource.Id.projectMenu);
            projectMenu.SetVisible(true);
            projectMenu.SetTitle(project.Name);
            CurrentProject = project;
        }

        public void DeactivateProjectSubmenu(int projectId)
        {
            if (projectId == CurrentProject.Id)
            {
                var projectMenu = _navigationView.Menu.FindItem(Resource.Id.projectMenu);
                projectMenu.SetVisible(false);
                CurrentProject = null;
            }
        }

        private void LoadAllProjectsListFragment()
        {
            var allProjectsListFragment = new AllProjectsListFragment();
            PushFragment(allProjectsListFragment);
        }

        private void LoadMyProjectsListFragment()
        {
            var projectListFragment = new MyProjectListFragment();
            PushFragment(projectListFragment);
        }

        public void LoadRulesSectionsFragment(Rules ruleSet)
        {
            var rulesSectionsFragment = new RulesSectionsFragment(ruleSet);
            PushFragment(rulesSectionsFragment);
        }

        public void LoadRulesSubsectionsFragment(Rules ruleSet)
        {
            var rulesSubsectionsFragment = new RulesSubsectionsFragment(ruleSet);
            PushFragment(rulesSubsectionsFragment);
        }

        public void LoadRulesSearchFragment()
        {
            var rulesSearchFragment = new RulesSearchFragment(CurrentProject.Rules);
            PushFragment(rulesSearchFragment);
        }

        private void PushFragment(SupportFragment fragment)
        {
            RefreshSearch();
            Android.Support.V4.App.FragmentTransaction fragmentTx = SupportFragmentManager.BeginTransaction();
            fragmentTx.Replace(Resource.Id.fragment_frame, fragment);
            fragmentTx.AddToBackStack(null);
            fragmentTx.Commit();
        }
    }
}