package md574230f914ad13c109a8731dbda3c6cec;


public class BaseRulesFragment
	extends android.support.v4.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Experiment.Fragments.RulesFragments.BaseRulesFragment, Experiment", BaseRulesFragment.class, __md_methods);
	}


	public BaseRulesFragment ()
	{
		super ();
		if (getClass () == BaseRulesFragment.class)
			mono.android.TypeManager.Activate ("Experiment.Fragments.RulesFragments.BaseRulesFragment, Experiment", "", this, new java.lang.Object[] {  });
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
