package md5a2a30dbeafc63548d0f5b185a334d048;


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
		mono.android.Runtime.register ("Experiment.Fragments.ViewHolderImageRule, Experiment", ViewHolderImageRule.class, __md_methods);
	}


	public ViewHolderImageRule (android.view.View p0)
	{
		super (p0);
		if (getClass () == ViewHolderImageRule.class)
			mono.android.TypeManager.Activate ("Experiment.Fragments.ViewHolderImageRule, Experiment", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
