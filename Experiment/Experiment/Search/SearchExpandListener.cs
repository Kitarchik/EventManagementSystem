using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Experiment.Fragments.RulesFragments;
using System.Linq;

namespace Experiment.Search
{
    public class SearchExpandListener : Java.Lang.Object, IMenuItemOnActionExpandListener
    {
        private readonly AppCompatActivity parent;

        public SearchExpandListener(AppCompatActivity parent)
        {
            this.parent = parent;
        }
        public bool OnMenuItemActionCollapse(IMenuItem item)
        {
            var activity = parent as MainActivity;
            activity.PopFragmentsOfType(typeof(BaseSearchFragment));
            activity.searchAdapter = null;
            return true;
        }

        public bool OnMenuItemActionExpand(IMenuItem item)
        {
            var currentFragmentType = parent.SupportFragmentManager.Fragments.Last().GetType();
            if(currentFragmentType.IsSubclassOf(typeof(BaseRulesFragment)))
            {
                var activity = parent as MainActivity;
                activity.LoadRulesSearchFragment();
            }
            return true;
        }
    }
}