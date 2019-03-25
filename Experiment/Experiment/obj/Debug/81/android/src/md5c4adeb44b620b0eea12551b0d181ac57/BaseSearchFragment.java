package md5c4adeb44b620b0eea12551b0d181ac57;


public class BaseSearchFragment
	extends android.support.v4.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Experiment.Search.BaseSearchFragment, Experiment", BaseSearchFragment.class, __md_methods);
	}


	public BaseSearchFragment ()
	{
		super ();
		if (getClass () == BaseSearchFragment.class)
			mono.android.TypeManager.Activate ("Experiment.Search.BaseSearchFragment, Experiment", "", this, new java.lang.Object[] {  });
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
