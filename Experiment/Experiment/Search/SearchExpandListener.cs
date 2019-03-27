using Android.Support.V7.App;
using Android.Views;
using Experiment.Fragments.RulesFragments;
using System.Linq;

namespace Experiment.Search
{
    public class SearchExpandListener : Java.Lang.Object, IMenuItemOnActionExpandListener
    {
        private readonly AppCompatActivity _parent;

        public SearchExpandListener(AppCompatActivity parent)
        {
            _parent = parent;
        }
        public bool OnMenuItemActionCollapse(IMenuItem item)
        {
            if (!(_parent is MainActivity activity)) return true;
            activity.SupportFragmentManager.PopBackStackImmediate();
            activity.SearchAdapter = null;
            return true;
        }

        public bool OnMenuItemActionExpand(IMenuItem item)
        {
            var currentFragmentType = _parent.SupportFragmentManager.Fragments.Last().GetType();
            if(currentFragmentType.IsSubclassOf(typeof(BaseRulesFragment)))
            {
                if (_parent is MainActivity activity) activity.LoadRulesSearchFragment();
            }
            return true;
        }
    }
}