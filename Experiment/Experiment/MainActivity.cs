using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Experiment.Fragments;
using Experiment.Fragments.RulesFragments;
using Experiment.Model;
using Experiment.Search;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace Experiment
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.DesignDemo", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;
        private IMenuItem previousMenuItem;
        private IMenuItem searchMenuItem;
        private SearchView searchView;

        public Project currentProject { get; set; }
        public Android.Widget.IFilterable searchAdapter { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            SupportToolbar toolbar = FindViewById<SupportToolbar>(Resource.Id.mainToolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            if (navigationView != null)
            {
                SetupDrawerContent(navigationView);
            }

            LoadProjectsListFragment();
        }

        private void SetupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                if (previousMenuItem != null)
                {
                    previousMenuItem.SetChecked(false);
                }

                e.MenuItem.SetChecked(true);
                previousMenuItem = e.MenuItem;
                ShowFragment(e.MenuItem.ItemId);
                drawerLayout.CloseDrawers();
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbarMenu, menu);
            searchMenuItem = menu.FindItem(Resource.Id.action_search);
            searchView = (SearchView)searchMenuItem.ActionView;
            searchMenuItem.SetOnActionExpandListener(new SearchExpandListener(this));

            searchView.QueryTextChange += (s, e) =>
            {
                if (searchAdapter != null)
                {
                    searchAdapter.Filter.InvokeFilter(e.NewText);
                }
            };

            searchView.QueryTextSubmit += (s, e) =>
            {
                if (searchAdapter != null)
                {
                    e.Handled = true;
                }
            };

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void ShowFragment(int id)
        {
            switch (id)
            {
                case Resource.Id.nav_projects:
                    {
                        LoadProjectsListFragment();
                        break;
                    }
                case Resource.Id.rules:
                    {
                        if (currentProject != null)
                        {
                            if (currentProject.ProjectRules == null)
                            {
                                DownloadRules();
                            }

                            PopFragmentsOfType(typeof(BaseRulesFragment));
                            LoadRulesSectionsFragment(currentProject.ProjectRules);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void DownloadRules()
        {
            AssetManager assets = Assets;
            using (StreamReader sr = new StreamReader(assets.Open("Rules.xml")))
            {
                XmlSerializer s = new XmlSerializer(typeof(Rules));
                var rules = (Rules)s.Deserialize(sr);
                currentProject.ProjectRules = rules;
            }
        }

        private void RefreshSearch()
        {
            if (searchView != null && !searchView.Iconified)
            {
                searchMenuItem.CollapseActionView();
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
            var projectMenu = navigationView.Menu.FindItem(Resource.Id.projectMenu);
            projectMenu.SetVisible(true);
            projectMenu.SetTitle(project.Name);
            currentProject = project;
            
            ProjectViewFragment projectViewFragment = new ProjectViewFragment(project);
            PushFragment(projectViewFragment);
        }

        private void LoadProjectsListFragment()
        {
            ProjectListFragment projectListFragment = new ProjectListFragment();
            PushFragment(projectListFragment);
        }

        public void LoadRulesSectionsFragment(Rules ruleSet)
        {
            RulesSectionsFragment rulesSectionsFragment = new RulesSectionsFragment(ruleSet);
            PushFragment(rulesSectionsFragment);
        }

        public void LoadRulesSubsectionsFragment(Rules ruleSet)
        {
            RulesSubsectionsFragment rulesSubsectionsFragment = new RulesSubsectionsFragment(this, ruleSet);
            PushFragment(rulesSubsectionsFragment);
        }

        public void LoadRulesSearchFragment()
        {
            RulesSearchFragment rulesSearchFragment = new RulesSearchFragment(currentProject.ProjectRules, this);
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