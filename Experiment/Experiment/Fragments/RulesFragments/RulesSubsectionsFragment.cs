using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Experiment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;

namespace Experiment.Fragments.RulesFragments
{
    public class RulesSubsectionsFragment : BaseRulesFragment
    {
        private Rules rules;
        private AppCompatActivity parent;
        private Android.Support.V4.View.ViewPager viewPager;

        public RulesSubsectionsFragment(AppCompatActivity parent, Rules rules)
        {
            this.parent = parent;
            this.rules = rules;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.rulesSubsectionsFragment, container, false);
            viewPager = view.FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.rulesSubsectionsPager);
            SetUpViewPager(viewPager);
            return view;
        }

        private void SetUpViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new RulesSubsectionsViewPagerAdapter(parent.SupportFragmentManager);
            adapter.AddFragment(new RulesOneSubsectionFragment(rules), rules.Name);
            var children = rules.ChildRules.Where(c => c.ChildRules != null && c.ChildRules.Count > 0).ToList();
            foreach (var child in children)
            {
                adapter.AddFragment(new RulesOneSubsectionFragment(child), child.Name);
            }
            viewPager.Adapter = adapter;
        }
    }

    internal class RulesSubsectionsViewPagerAdapter : Android.Support.V4.App.FragmentStatePagerAdapter
    {
        List<SupportFragment> fragments = new List<SupportFragment>();
        List<string> fragmentTitles = new List<string>();

        public RulesSubsectionsViewPagerAdapter(SupportFragmentManager fm) : base(fm)
        {
        }

        public void AddFragment(SupportFragment fragment, String title)
        {
            fragments.Add(fragment);
            fragmentTitles.Add(title);
        }

        public override SupportFragment GetItem(int position)
        {
            return fragments[position];
        }

        public override int Count
        {
            get { return fragments.Count; }
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(fragmentTitles[position]);
        }
    }
}