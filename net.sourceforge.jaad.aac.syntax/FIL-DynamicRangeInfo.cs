using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace net.sourceforge.jaad.aac.syntax;

[NonNestedOuterClass("net.sourceforge.jaad.aac.syntax.FIL")]
[InnerClass("net.sourceforge.jaad.aac.syntax.FIL$DynamicRangeInfo", Modifiers.Public | Modifiers.Static)]
public class FIL_0024DynamicRangeInfo : Object
{
	private const int MAX_NBR_BANDS = 7;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private bool[] excludeMask;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private bool[] additionalExcludedChannels;

	private bool pceTagPresent;

	private int pceInstanceTag;

	private int tagReservedBits;

	private bool excludedChannelsPresent;

	private bool bandsPresent;

	private int bandsIncrement;

	private int interpolationScheme;

	private int[] bandTop;

	private bool progRefLevelPresent;

	private int progRefLevel;

	private int progRefLevelReservedBits;

	private bool[] dynRngSgn;

	private int[] dynRngCtl;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 105, 109, 109 })]
	public FIL_0024DynamicRangeInfo()
	{
		excludeMask = new bool[7];
		additionalExcludedChannels = new bool[7];
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(new byte[] { 159, 139, 161, 67 })]
	internal static bool access_0024002(FIL_0024DynamicRangeInfo x0, bool x1)
	{
		x0.pceTagPresent = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int access_0024102(FIL_0024DynamicRangeInfo x0, int x1)
	{
		x0.pceInstanceTag = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int access_0024202(FIL_0024DynamicRangeInfo x0, int x1)
	{
		x0.tagReservedBits = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(new byte[] { 159, 139, 161, 67 })]
	internal static bool access_0024302(FIL_0024DynamicRangeInfo x0, bool x1)
	{
		x0.excludedChannelsPresent = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(new byte[] { 159, 139, 161, 67 })]
	internal static bool access_0024402(FIL_0024DynamicRangeInfo x0, bool x1)
	{
		x0.bandsPresent = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int access_0024502(FIL_0024DynamicRangeInfo x0, int x1)
	{
		x0.bandsIncrement = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int access_0024602(FIL_0024DynamicRangeInfo x0, int x1)
	{
		x0.interpolationScheme = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int access_0024500(FIL_0024DynamicRangeInfo x0)
	{
		return x0.bandsIncrement;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int[] access_0024702(FIL_0024DynamicRangeInfo x0, int[] x1)
	{
		x0.bandTop = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int[] access_0024700(FIL_0024DynamicRangeInfo x0)
	{
		return x0.bandTop;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(new byte[] { 159, 139, 161, 67 })]
	internal static bool access_0024802(FIL_0024DynamicRangeInfo x0, bool x1)
	{
		x0.progRefLevelPresent = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int access_0024902(FIL_0024DynamicRangeInfo x0, int x1)
	{
		x0.progRefLevel = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int access_00241002(FIL_0024DynamicRangeInfo x0, int x1)
	{
		x0.progRefLevelReservedBits = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static bool[] access_00241102(FIL_0024DynamicRangeInfo x0, bool[] x1)
	{
		x0.dynRngSgn = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int[] access_00241202(FIL_0024DynamicRangeInfo x0, int[] x1)
	{
		x0.dynRngCtl = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static bool[] access_00241100(FIL_0024DynamicRangeInfo x0)
	{
		return x0.dynRngSgn;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static int[] access_00241200(FIL_0024DynamicRangeInfo x0)
	{
		return x0.dynRngCtl;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(15)]
	internal static bool[] access_00241300(FIL_0024DynamicRangeInfo x0)
	{
		return x0.excludeMask;
	}
}
