package md5c4adeb44b620b0eea12551b0d181ac57;


public class RulesSearchHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Experiment.Search.RulesSearchHolder, Experiment", RulesSearchHolder.class, __md_methods);
	}


	public RulesSearchHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == RulesSearchHolder.class)
			mono.android.TypeManager.Activate ("Experiment.Search.RulesSearchHolder, Experiment", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
