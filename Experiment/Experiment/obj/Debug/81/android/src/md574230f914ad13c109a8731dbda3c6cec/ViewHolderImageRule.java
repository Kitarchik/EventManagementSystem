package md574230f914ad13c109a8731dbda3c6cec;


public class ViewHolderImageRule
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Experiment.Fragments.RulesFragments.ViewHolderImageRule, Experiment", ViewHolderImageRule.class, __md_methods);
	}


	public ViewHolderImageRule (android.view.View p0)
	{
		super (p0);
		if (getClass () == ViewHolderImageRule.class)
			mono.android.TypeManager.Activate ("Experiment.Fragments.RulesFragments.ViewHolderImageRule, Experiment", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
