package md571fc11570f84190763accb25a82e69a7;


public class RenderizadorCustomizadoTextboxSemBorda
	extends md51558244f76c53b6aeda52c8a337f2c37.EntryRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Blib.Interfaces.Renderers.RenderizadorCustomizadoTextboxSemBorda, Blib.Droid", RenderizadorCustomizadoTextboxSemBorda.class, __md_methods);
	}


	public RenderizadorCustomizadoTextboxSemBorda (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == RenderizadorCustomizadoTextboxSemBorda.class)
			mono.android.TypeManager.Activate ("Blib.Interfaces.Renderers.RenderizadorCustomizadoTextboxSemBorda, Blib.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public RenderizadorCustomizadoTextboxSemBorda (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RenderizadorCustomizadoTextboxSemBorda.class)
			mono.android.TypeManager.Activate ("Blib.Interfaces.Renderers.RenderizadorCustomizadoTextboxSemBorda, Blib.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public RenderizadorCustomizadoTextboxSemBorda (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RenderizadorCustomizadoTextboxSemBorda.class)
			mono.android.TypeManager.Activate ("Blib.Interfaces.Renderers.RenderizadorCustomizadoTextboxSemBorda, Blib.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
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
