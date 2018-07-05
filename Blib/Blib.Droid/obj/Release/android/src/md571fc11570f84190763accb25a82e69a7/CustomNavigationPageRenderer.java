package md571fc11570f84190763accb25a82e69a7;


public class CustomNavigationPageRenderer
	extends md58432a647068b097f9637064b8985a5e0.NavigationPageRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onConfigurationChanged:(Landroid/content/res/Configuration;)V:GetOnConfigurationChanged_Landroid_content_res_Configuration_Handler\n" +
			"n_onViewRemoved:(Landroid/view/View;)V:GetOnViewRemoved_Landroid_view_View_Handler\n" +
			"n_onViewAdded:(Landroid/view/View;)V:GetOnViewAdded_Landroid_view_View_Handler\n" +
			"";
		mono.android.Runtime.register ("Blib.Interfaces.Renderers.CustomNavigationPageRenderer, Blib.Droid", CustomNavigationPageRenderer.class, __md_methods);
	}


	public CustomNavigationPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomNavigationPageRenderer.class)
			mono.android.TypeManager.Activate ("Blib.Interfaces.Renderers.CustomNavigationPageRenderer, Blib.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CustomNavigationPageRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomNavigationPageRenderer.class)
			mono.android.TypeManager.Activate ("Blib.Interfaces.Renderers.CustomNavigationPageRenderer, Blib.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CustomNavigationPageRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomNavigationPageRenderer.class)
			mono.android.TypeManager.Activate ("Blib.Interfaces.Renderers.CustomNavigationPageRenderer, Blib.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onConfigurationChanged (android.content.res.Configuration p0)
	{
		n_onConfigurationChanged (p0);
	}

	private native void n_onConfigurationChanged (android.content.res.Configuration p0);


	public void onViewRemoved (android.view.View p0)
	{
		n_onViewRemoved (p0);
	}

	private native void n_onViewRemoved (android.view.View p0);


	public void onViewAdded (android.view.View p0)
	{
		n_onViewAdded (p0);
	}

	private native void n_onViewAdded (android.view.View p0);

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
