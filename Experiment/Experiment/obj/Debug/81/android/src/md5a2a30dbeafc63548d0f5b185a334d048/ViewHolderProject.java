package md5a2a30dbeafc63548d0f5b185a334d048;


public class ViewHolderProject
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Experiment.Fragments.ViewHolderProject, Experiment", ViewHolderProject.class, __md_methods);
	}


	public ViewHolderProject (android.view.View p0)
	{
		super (p0);
		if (getClass () == ViewHolderProject.class)
			mono.android.TypeManager.Activate ("Experiment.Fragments.ViewHolderProject, Experiment", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
