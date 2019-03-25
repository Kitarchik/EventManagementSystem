package md5c4adeb44b620b0eea12551b0d181ac57;


public class SearchExpandListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.MenuItem.OnActionExpandListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMenuItemActionCollapse:(Landroid/view/MenuItem;)Z:GetOnMenuItemActionCollapse_Landroid_view_MenuItem_Handler:Android.Views.IMenuItemOnActionExpandListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onMenuItemActionExpand:(Landroid/view/MenuItem;)Z:GetOnMenuItemActionExpand_Landroid_view_MenuItem_Handler:Android.Views.IMenuItemOnActionExpandListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Experiment.Search.SearchExpandListener, Experiment", SearchExpandListener.class, __md_methods);
	}


	public SearchExpandListener ()
	{
		super ();
		if (getClass () == SearchExpandListener.class)
			mono.android.TypeManager.Activate ("Experiment.Search.SearchExpandListener, Experiment", "", this, new java.lang.Object[] {  });
	}

	public SearchExpandListener (android.support.v7.app.AppCompatActivity p0)
	{
		super ();
		if (getClass () == SearchExpandListener.class)
			mono.android.TypeManager.Activate ("Experiment.Search.SearchExpandListener, Experiment", "Android.Support.V7.App.AppCompatActivity, Xamarin.Android.Support.v7.AppCompat", this, new java.lang.Object[] { p0 });
	}


	public boolean onMenuItemActionCollapse (android.view.MenuItem p0)
	{
		return n_onMenuItemActionCollapse (p0);
	}

	private native boolean n_onMenuItemActionCollapse (android.view.MenuItem p0);


	public boolean onMenuItemActionExpand (android.view.MenuItem p0)
	{
		return n_onMenuItemActionExpand (p0);
	}

	private native boolean n_onMenuItemActionExpand (android.view.MenuItem p0);

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
