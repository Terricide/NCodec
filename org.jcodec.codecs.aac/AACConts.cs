using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.aac;

public class AACConts : Object
{
	internal static short[] ___003C_003EAAC_CHANNEL_COUNT;

	public static int[] AAC_SAMPLE_RATES;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static short[] AAC_CHANNEL_COUNT
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAAC_CHANNEL_COUNT;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public AACConts()
	{
	}

	[LineNumberTable(new byte[] { 159, 139, 66, 127, 13 })]
	static AACConts()
	{
		___003C_003EAAC_CHANNEL_COUNT = new short[8] { 0, 1, 2, 3, 4, 5, 6, 8 };
		AAC_SAMPLE_RATES = new int[13]
		{
			96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000,
			11025, 8000, 7350
		};
	}
}
