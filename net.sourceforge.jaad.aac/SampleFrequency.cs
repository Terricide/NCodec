using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace net.sourceforge.jaad.aac;

public class SampleFrequency : Object
{
	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_96000;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_88200;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_64000;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_48000;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_44100;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_32000;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_24000;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_22050;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_16000;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_12000;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_11025;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_8000;

	internal static SampleFrequency ___003C_003ESAMPLE_FREQUENCY_NONE;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static SampleFrequency[] _values;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int index;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int frequency;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int[] prediction;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int[] maxTNS_SFB;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_96000
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_96000;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_88200
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_88200;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_64000
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_64000;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_48000
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_48000;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_44100
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_44100;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_32000
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_32000;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_24000
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_24000;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_22050
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_22050;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_16000
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_16000;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_12000
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_12000;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_11025
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_11025;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_8000
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_8000;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static SampleFrequency SAMPLE_FREQUENCY_NONE
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESAMPLE_FREQUENCY_NONE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 162, 135, 99, 107, 108, 5, 231, 69,
		100, 103
	})]
	public static SampleFrequency forFrequency(int i)
	{
		SampleFrequency[] all = values();
		SampleFrequency freq = null;
		int j = 0;
		while (freq == null && j < 12)
		{
			if (i == all[j].frequency)
			{
				freq = all[j];
			}
			j++;
		}
		if (freq == null)
		{
			freq = ___003C_003ESAMPLE_FREQUENCY_NONE;
		}
		return freq;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 162, 106, 139, 103 })]
	public static SampleFrequency forInt(int i)
	{
		if (i >= 0 && i < 12)
		{
			return values()[i];
		}
		return ___003C_003ESAMPLE_FREQUENCY_NONE;
	}

	[LineNumberTable(97)]
	public virtual int getIndex()
	{
		return index;
	}

	[LineNumberTable(107)]
	public virtual int getFrequency()
	{
		return frequency;
	}

	[LineNumberTable(48)]
	public static SampleFrequency[] values()
	{
		return _values;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 162, 105, 104, 104, 104, 105 })]
	private SampleFrequency(int index, int freqency, int[] prediction, int[] maxTNS_SFB)
	{
		this.index = index;
		frequency = freqency;
		this.prediction = prediction;
		this.maxTNS_SFB = maxTNS_SFB;
	}

	[LineNumberTable(117)]
	public virtual int getMaximalPredictionSFB()
	{
		return prediction[0];
	}

	[LineNumberTable(127)]
	public virtual int getPredictorCount()
	{
		return prediction[1];
	}

	[LineNumberTable(new byte[] { 159, 108, 97, 67 })]
	public virtual int getMaximalTNS_SFB(bool shortWindow)
	{
		return maxTNS_SFB[shortWindow ? 1 : 0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(148)]
	public override string toString()
	{
		string result = Integer.toString(frequency);
		return result;
	}

	[LineNumberTable(new byte[]
	{
		159, 139, 162, 159, 21, 159, 21, 159, 21, 159,
		21, 159, 21, 159, 21, 159, 21, 159, 21, 159,
		21, 159, 22, 159, 22, 159, 22, 191, 10
	})]
	static SampleFrequency()
	{
		___003C_003ESAMPLE_FREQUENCY_96000 = new SampleFrequency(0, 96000, new int[2] { 33, 512 }, new int[2] { 31, 9 });
		___003C_003ESAMPLE_FREQUENCY_88200 = new SampleFrequency(1, 88200, new int[2] { 33, 512 }, new int[2] { 31, 9 });
		___003C_003ESAMPLE_FREQUENCY_64000 = new SampleFrequency(2, 64000, new int[2] { 38, 664 }, new int[2] { 34, 10 });
		___003C_003ESAMPLE_FREQUENCY_48000 = new SampleFrequency(3, 48000, new int[2] { 40, 672 }, new int[2] { 40, 14 });
		___003C_003ESAMPLE_FREQUENCY_44100 = new SampleFrequency(4, 44100, new int[2] { 40, 672 }, new int[2] { 42, 14 });
		___003C_003ESAMPLE_FREQUENCY_32000 = new SampleFrequency(5, 32000, new int[2] { 40, 672 }, new int[2] { 51, 14 });
		___003C_003ESAMPLE_FREQUENCY_24000 = new SampleFrequency(6, 24000, new int[2] { 41, 652 }, new int[2] { 46, 14 });
		___003C_003ESAMPLE_FREQUENCY_22050 = new SampleFrequency(7, 22050, new int[2] { 41, 652 }, new int[2] { 46, 14 });
		___003C_003ESAMPLE_FREQUENCY_16000 = new SampleFrequency(8, 16000, new int[2] { 37, 664 }, new int[2] { 42, 14 });
		___003C_003ESAMPLE_FREQUENCY_12000 = new SampleFrequency(9, 12000, new int[2] { 37, 664 }, new int[2] { 42, 14 });
		___003C_003ESAMPLE_FREQUENCY_11025 = new SampleFrequency(10, 11025, new int[2] { 37, 664 }, new int[2] { 42, 14 });
		___003C_003ESAMPLE_FREQUENCY_8000 = new SampleFrequency(11, 8000, new int[2] { 34, 664 }, new int[2] { 39, 14 });
		___003C_003ESAMPLE_FREQUENCY_NONE = new SampleFrequency(-1, 0, new int[2] { 0, 0 }, new int[2] { 0, 0 });
		_values = new SampleFrequency[13]
		{
			___003C_003ESAMPLE_FREQUENCY_96000, ___003C_003ESAMPLE_FREQUENCY_88200, ___003C_003ESAMPLE_FREQUENCY_64000, ___003C_003ESAMPLE_FREQUENCY_48000, ___003C_003ESAMPLE_FREQUENCY_44100, ___003C_003ESAMPLE_FREQUENCY_32000, ___003C_003ESAMPLE_FREQUENCY_24000, ___003C_003ESAMPLE_FREQUENCY_22050, ___003C_003ESAMPLE_FREQUENCY_16000, ___003C_003ESAMPLE_FREQUENCY_12000,
			___003C_003ESAMPLE_FREQUENCY_11025, ___003C_003ESAMPLE_FREQUENCY_8000, ___003C_003ESAMPLE_FREQUENCY_NONE
		};
	}
}
