using Android.OS;
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
        private Android.Support.V4.View.ViewPager _viewPager;

        public RulesSubsectionsFragment() { }
        public RulesSubsectionsFragment(Rules rules) : base(rules) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.rulesSubsectionsFragment, container, false);
            _viewPager = view.FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.rulesSubsectionsPager);
            SetUpViewPager(_viewPager);
            return view;
        }

        private void SetUpViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new RulesSubsectionsViewPagerAdapter(ChildFragmentManager);
            adapter.AddFragment(new RulesOneSubsectionFragment(Rules), Rules.Name);
            var children = Rules.ChildRules.Where(c => c.ChildRules != null && c.ChildRules.Count > 0).ToList();
            foreach (var child in children)
            {
                adapter.AddFragment(new RulesOneSubsectionFragment(child), child.Name);
            }
            viewPager.Adapter = adapter;
        }
    }

    internal class RulesSubsectionsViewPagerAdapter : Android.Support.V4.App.FragmentStatePagerAdapter
    {
        readonly List<SupportFragment> _fragments = new List<SupportFragment>();
        readonly List<string> _fragmentTitles = new List<string>();

        public RulesSubsectionsViewPagerAdapter(SupportFragmentManager fm) : base(fm)
        {
        }

        public void AddFragment(SupportFragment fragment, String title)
        {
            _fragments.Add(fragment);
            _fragmentTitles.Add(title);
        }

        public override SupportFragment GetItem(int position)
        {
            return _fragments[position];
        }

        public override int Count => _fragments.Count;

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(_fragmentTitles[position]);
        }
    }
}