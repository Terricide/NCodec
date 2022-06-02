using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.io.model;

public class AspectRatio : Object
{
	internal static AspectRatio ___003C_003EExtended_SAR;

	private int value;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static AspectRatio Extended_SAR
	{
		[HideFromJava]
		get
		{
			return ___003C_003EExtended_SAR;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104 })]
	private AspectRatio(int value)
	{
		this.value = value;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 110, 135 })]
	public static AspectRatio fromValue(int value)
	{
		if (value == ___003C_003EExtended_SAR.value)
		{
			return ___003C_003EExtended_SAR;
		}
		AspectRatio result = new AspectRatio(value);
		
		return result;
	}

	[LineNumberTable(32)]
	public virtual int getValue()
	{
		return value;
	}

	[LineNumberTable(16)]
	static AspectRatio()
	{
		___003C_003EExtended_SAR = new AspectRatio(255);
	}
}
