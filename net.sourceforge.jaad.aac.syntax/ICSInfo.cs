using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.filterbank;
using net.sourceforge.jaad.aac.tools;
using org.jcodec.platform;

namespace net.sourceforge.jaad.aac.syntax;

[Implements(new string[] { "net.sourceforge.jaad.aac.syntax.SyntaxConstants", "net.sourceforge.jaad.aac.syntax.ScaleFactorBands" })]
public class ICSInfo : java.lang.Object, SyntaxConstants, ScaleFactorBands
{
	public class LTPrediction : java.lang.Object, SyntaxConstants
	{
		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static float[] CODEBOOK;

		[Modifiers(Modifiers.Private | Modifiers.Final)]
		private int frameLength;

		[Modifiers(Modifiers.Private | Modifiers.Final)]
		private int[] states;

		private int coef;

		private int lag;

		private int lastBand;

		private bool lagUpdate;

		private bool[] shortUsed;

		private bool[] shortLagPresent;

		private bool[] longUsed;

		private int[] shortLag;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
		[LineNumberTable(new byte[]
		{
			159, 130, 98, 104, 110, 109, 153, 111, 127, 28,
			142, 136, 108, 109, 109, 109, 103, 122, 111, 250,
			61, 236, 72, 116, 114, 110, 48, 201
		})]
		public virtual void decode(IBitStream _in, ICSInfo info, Profile profile)
		{
			lag = 0;
			if (java.lang.Object.instancehelper_equals(profile, Profile.___003C_003EAAC_LD))
			{
				lagUpdate = _in.readBool();
				if (lagUpdate)
				{
					lag = _in.readBits(10);
				}
			}
			else
			{
				lag = _in.readBits(11);
			}
			if (lag > frameLength << 1)
			{
				string message = new StringBuilder().append("LTP lag too large: ").append(lag).toString();
				throw new AACException(message);
			}
			coef = _in.readBits(3);
			int windowCount = info.getWindowCount();
			if (info.isEightShortFrame())
			{
				shortUsed = new bool[windowCount];
				shortLagPresent = new bool[windowCount];
				shortLag = new int[windowCount];
				for (int w = 0; w < windowCount; w++)
				{
					bool[] array = shortUsed;
					int num = w;
					int num2 = (_in.readBool() ? 1 : 0);
					int num3 = num;
					bool[] array2 = array;
					array2[num3] = (byte)num2 != 0;
					if (num2 != 0)
					{
						shortLagPresent[w] = _in.readBool();
						if (shortLagPresent[w])
						{
							shortLag[w] = _in.readBits(4);
						}
					}
				}
			}
			else
			{
				lastBand = java.lang.Math.min(info.getMaxSFB(), 40);
				longUsed = new bool[lastBand];
				for (int i = 0; i < lastBand; i++)
				{
					longUsed[i] = _in.readBool();
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 132, 162, 105, 104, 111 })]
		public LTPrediction(int frameLength)
		{
			this.frameLength = frameLength;
			states = new int[4 * frameLength];
		}

		[LineNumberTable(new byte[] { 159, 122, 98, 114 })]
		public virtual void setPredictionUnused(int sfb)
		{
			if (longUsed != null)
			{
				longUsed[sfb] = false;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 121, 98, 136, 108, 106, 108, 140, 105, 63,
			7, 201, 113, 40, 166, 153, 105, 137, 113, 108,
			104, 145, 107, 56, 233, 59, 236, 75
		})]
		public virtual void process(ICStream ics, float[] data, FilterBank filterBank, SampleFrequency sf)
		{
			ICSInfo info = ics.getInfo();
			if (info.isEightShortFrame())
			{
				return;
			}
			int samples = frameLength << 1;
			float[] _in = new float[2048];
			float[] @out = new float[2048];
			for (int i = 0; i < samples; i++)
			{
				_in[i] = (float)states[samples + i - lag] * CODEBOOK[coef];
			}
			filterBank.processLTP(info.getWindowSequence(), info.getWindowShape(1), info.getWindowShape(0), _in, @out);
			if (ics.isTNSDataPresent())
			{
				ics.getTNS().process(ics, @out, sf, decode: true);
			}
			int[] swbOffsets = info.getSWBOffsets();
			int swbOffsetMax = info.getSWBOffsetMax();
			for (int sfb = 0; sfb < lastBand; sfb++)
			{
				if (longUsed[sfb])
				{
					int low = swbOffsets[sfb];
					int high = java.lang.Math.min(swbOffsets[sfb + 1], swbOffsetMax);
					for (int bin = low; bin < high; bin++)
					{
						int num = bin;
						data[num] += @out[bin];
					}
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 113, 162, 113, 111, 120, 127, 2, 122, 250,
			60, 234, 72, 108, 120, 120, 250, 61, 231, 70
		})]
		public virtual void updateState(float[] time, float[] overlap, Profile profile)
		{
			if (java.lang.Object.instancehelper_equals(profile, Profile.___003C_003EAAC_LD))
			{
				for (int j = 0; j < frameLength; j++)
				{
					states[j] = states[j + frameLength];
					states[frameLength + j] = states[j + frameLength * 2];
					states[frameLength * 2 + j] = java.lang.Math.round(time[j]);
					states[frameLength * 3 + j] = java.lang.Math.round(overlap[j]);
				}
			}
			else
			{
				for (int i = 0; i < frameLength; i++)
				{
					states[i] = states[i + frameLength];
					states[frameLength + i] = java.lang.Math.round(time[i]);
					states[frameLength * 2 + i] = java.lang.Math.round(overlap[i]);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(137)]
		public static bool isLTPProfile(Profile profile)
		{
			return (java.lang.Object.instancehelper_equals(profile, Profile.___003C_003EAAC_LTP) || java.lang.Object.instancehelper_equals(profile, Profile.___003C_003EER_AAC_LTP) || java.lang.Object.instancehelper_equals(profile, Profile.___003C_003EAAC_LD)) ? true : false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 107, 98, 123, 109, 109, 109, 109, 121, 121,
			121, 121
		})]
		public virtual void copy(LTPrediction ltp)
		{
			ByteCodeHelper.arraycopy_primitive_4(ltp.states, 0, states, 0, states.Length);
			coef = ltp.coef;
			lag = ltp.lag;
			lastBand = ltp.lastBand;
			lagUpdate = ltp.lagUpdate;
			shortUsed = Platform.copyOfBool(ltp.shortUsed, ltp.shortUsed.Length);
			shortLagPresent = Platform.copyOfBool(ltp.shortLagPresent, ltp.shortLagPresent.Length);
			shortLag = Platform.copyOfInt(ltp.shortLag, ltp.shortLag.Length);
			longUsed = Platform.copyOfBool(ltp.longUsed, ltp.longUsed.Length);
		}

		[LineNumberTable(26)]
		static LTPrediction()
		{
			CODEBOOK = new float[8] { 0.570829f, 0.696616f, 0.813004f, 0.911304f, 0.9849f, 1.067894f, 1.194601f, 1.369533f };
		}
	}

	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lnet/sourceforge/jaad/aac/syntax/ICSInfo$WindowSequence;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class WindowSequence : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			ONLY_LONG_SEQUENCE,
			LONG_START_SEQUENCE,
			EIGHT_SHORT_SEQUENCE,
			LONG_STOP_SEQUENCE
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static WindowSequence ___003C_003EONLY_LONG_SEQUENCE;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static WindowSequence ___003C_003ELONG_START_SEQUENCE;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static WindowSequence ___003C_003EEIGHT_SHORT_SEQUENCE;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static WindowSequence ___003C_003ELONG_STOP_SEQUENCE;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static WindowSequence[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static WindowSequence ONLY_LONG_SEQUENCE
		{
			[HideFromJava]
			get
			{
				return ___003C_003EONLY_LONG_SEQUENCE;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static WindowSequence LONG_START_SEQUENCE
		{
			[HideFromJava]
			get
			{
				return ___003C_003ELONG_START_SEQUENCE;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static WindowSequence EIGHT_SHORT_SEQUENCE
		{
			[HideFromJava]
			get
			{
				return ___003C_003EEIGHT_SHORT_SEQUENCE;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static WindowSequence LONG_STOP_SEQUENCE
		{
			[HideFromJava]
			get
			{
				return ___003C_003ELONG_STOP_SEQUENCE;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(158)]
		public static WindowSequence[] values()
		{
			return (WindowSequence[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(158)]
		public static WindowSequence valueOf(string name)
		{
			return (WindowSequence)java.lang.Enum.valueOf(ClassLiteral<WindowSequence>.Value, name);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(158)]
		private WindowSequence(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[LineNumberTable(new byte[] { 159, 103, 162, 113, 113, 113, 241, 60 })]
		static WindowSequence()
		{
			___003C_003EONLY_LONG_SEQUENCE = new WindowSequence("ONLY_LONG_SEQUENCE", 0);
			___003C_003ELONG_START_SEQUENCE = new WindowSequence("LONG_START_SEQUENCE", 1);
			___003C_003EEIGHT_SHORT_SEQUENCE = new WindowSequence("EIGHT_SHORT_SEQUENCE", 2);
			___003C_003ELONG_STOP_SEQUENCE = new WindowSequence("LONG_STOP_SEQUENCE", 3);
			_0024VALUES = new WindowSequence[4] { ___003C_003EONLY_LONG_SEQUENCE, ___003C_003ELONG_START_SEQUENCE, ___003C_003EEIGHT_SHORT_SEQUENCE, ___003C_003ELONG_STOP_SEQUENCE };
		}
	}

	public const int WINDOW_SHAPE_SINE = 0;

	public const int WINDOW_SHAPE_KAISER = 1;

	public const int PREVIOUS = 0;

	public const int CURRENT = 1;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int frameLength;

	private WindowSequence windowSequence;

	private int[] windowShape;

	private int maxSFB;

	private bool predictionDataPresent;

	private ICPrediction icPredict;

	internal bool ltpData1Present;

	internal bool ltpData2Present;

	private LTPrediction ltPredict1;

	private LTPrediction ltPredict2;

	private int windowCount;

	private int windowGroupCount;

	private int[] windowGroupLength;

	private int swbCount;

	private int[] swbOffsets;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_LONG_WINDOW_COUNT
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_LONG_WINDOW_COUNT;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_1024_96
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_1024_96;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_1024_64
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_1024_64;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_1024_48
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_1024_48;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_1024_32
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_1024_32;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_1024_24
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_1024_24;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_1024_16
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_1024_16;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_1024_8
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_1024_8;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] SWB_OFFSET_LONG_WINDOW
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_LONG_WINDOW;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_SHORT_WINDOW_COUNT
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_SHORT_WINDOW_COUNT;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_128_96
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_128_96;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_128_64
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_128_64;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_128_48
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_128_48;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_128_24
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_128_24;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_128_16
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_128_16;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] SWB_OFFSET_128_8
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_128_8;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] SWB_OFFSET_SHORT_WINDOW
	{
		[HideFromJava]
		get
		{
			return ScaleFactorBands.SWB_OFFSET_SHORT_WINDOW;
		}
	}

	[LineNumberTable(284)]
	public virtual int getWindowGroupCount()
	{
		return windowGroupCount;
	}

	[LineNumberTable(264)]
	public virtual int getMaxSFB()
	{
		return maxSFB;
	}

	[LineNumberTable(272)]
	public virtual int[] getSWBOffsets()
	{
		return swbOffsets;
	}

	[LineNumberTable(276)]
	public virtual int getSWBOffsetMax()
	{
		return swbOffsets[swbCount];
	}

	[LineNumberTable(288)]
	public virtual int getWindowGroupLength(int g)
	{
		return windowGroupLength[g];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(296)]
	public virtual bool isEightShortFrame()
	{
		bool result = windowSequence.equals(WindowSequence.___003C_003EEIGHT_SHORT_SEQUENCE);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 92, 65, 67, 104, 158, 103, 115, 113, 143,
		104, 106, 118, 142, 103, 159, 7, 111, 241, 60,
		231, 71, 104, 115, 115, 104, 131, 110, 104, 115,
		115, 109, 154
	})]
	public virtual void decode(IBitStream _in, AACDecoderConfig conf, bool commonWindow)
	{
		SampleFrequency sf = conf.getSampleFrequency();
		if (java.lang.Object.instancehelper_equals(sf, SampleFrequency.___003C_003ESAMPLE_FREQUENCY_NONE))
		{
			throw new AACException("invalid sample frequency");
		}
		_in.skipBit();
		windowSequence = windowSequenceFromInt(_in.readBits(2));
		windowShape[0] = windowShape[1];
		windowShape[1] = _in.readBit();
		windowGroupCount = 1;
		windowGroupLength[0] = 1;
		if (windowSequence.equals(WindowSequence.___003C_003EEIGHT_SHORT_SEQUENCE))
		{
			maxSFB = _in.readBits(4);
			for (int i = 0; i < 7; i++)
			{
				if (_in.readBool())
				{
					int[] array = windowGroupLength;
					int num = windowGroupCount - 1;
					int[] array2 = array;
					array2[num]++;
				}
				else
				{
					windowGroupCount++;
					windowGroupLength[windowGroupCount - 1] = 1;
				}
			}
			windowCount = 8;
			swbOffsets = ScaleFactorBands.SWB_OFFSET_SHORT_WINDOW[sf.getIndex()];
			swbCount = ScaleFactorBands.SWB_SHORT_WINDOW_COUNT[sf.getIndex()];
			predictionDataPresent = false;
		}
		else
		{
			maxSFB = _in.readBits(6);
			windowCount = 1;
			swbOffsets = ScaleFactorBands.SWB_OFFSET_LONG_WINDOW[sf.getIndex()];
			swbCount = ScaleFactorBands.SWB_LONG_WINDOW_COUNT[sf.getIndex()];
			predictionDataPresent = _in.readBool();
			if (predictionDataPresent)
			{
				readPredictionData(_in, conf.getProfile(), sf, commonWindow);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 59, 130, 119, 113, 113, 109, 109, 117, 109,
		105, 114, 146, 109, 109, 121, 109, 121
	})]
	public virtual void setData(ICSInfo info)
	{
		windowSequence = WindowSequence.valueOf(info.windowSequence.name());
		windowShape[0] = windowShape[1];
		windowShape[1] = info.windowShape[1];
		maxSFB = info.maxSFB;
		predictionDataPresent = info.predictionDataPresent;
		if (predictionDataPresent)
		{
			icPredict = info.icPredict;
		}
		ltpData1Present = info.ltpData1Present;
		if (ltpData1Present)
		{
			ltPredict1.copy(info.ltPredict1);
			ltPredict2.copy(info.ltPredict2);
		}
		windowCount = info.windowCount;
		windowGroupCount = info.windowGroupCount;
		windowGroupLength = Platform.copyOfInt(info.windowGroupLength, info.windowGroupLength.Length);
		swbCount = info.swbCount;
		swbOffsets = Platform.copyOfInt(info.swbOffsets, info.swbOffsets.Length);
	}

	[LineNumberTable(312)]
	public virtual bool isLTPrediction1Present()
	{
		return ltpData1Present;
	}

	[LineNumberTable(324)]
	public virtual LTPrediction getLTPrediction2()
	{
		return ltPredict2;
	}

	[LineNumberTable(280)]
	public virtual int getWindowCount()
	{
		return windowCount;
	}

	[LineNumberTable(292)]
	public virtual WindowSequence getWindowSequence()
	{
		return windowSequence;
	}

	[LineNumberTable(300)]
	public virtual int getWindowShape(int index)
	{
		return windowShape[index];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 101, 130, 103, 102, 145 })]
	public static WindowSequence windowSequenceFromInt(int i)
	{
		WindowSequence[] values = WindowSequence.values();
		if (i >= (nint)values.LongLength)
		{
			throw new AACException("unknown window sequence type");
		}
		return values[i];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 83, 65, 68, 105, 116, 121, 108, 116, 127,
		0, 143, 103, 119, 127, 0, 180, 105, 100, 116,
		127, 0, 209, 159, 7
	})]
	private void readPredictionData(IBitStream _in, Profile profile, SampleFrequency sf, bool commonWindow)
	{
		if (Profile.___003C_003EAAC_MAIN == profile)
		{
			if (icPredict == null)
			{
				icPredict = new ICPrediction();
			}
			icPredict.decode(_in, maxSFB, sf);
			return;
		}
		if (Profile.___003C_003EAAC_LTP == profile)
		{
			int num = (_in.readBool() ? 1 : 0);
			int num2 = num;
			ltpData1Present = (byte)num != 0;
			if (num2 != 0)
			{
				if (ltPredict1 == null)
				{
					LTPrediction.___003Cclinit_003E();
					ltPredict1 = new LTPrediction(frameLength);
				}
				ltPredict1.decode(_in, this, profile);
			}
			if (!commonWindow)
			{
				return;
			}
			num = (_in.readBool() ? 1 : 0);
			int num3 = num;
			ltpData2Present = (byte)num != 0;
			if (num3 != 0)
			{
				if (ltPredict2 == null)
				{
					LTPrediction.___003Cclinit_003E();
					ltPredict2 = new LTPrediction(frameLength);
				}
				ltPredict2.decode(_in, this, profile);
			}
			return;
		}
		if (Profile.___003C_003EER_AAC_LTP == profile)
		{
			if (commonWindow)
			{
				return;
			}
			int num = (_in.readBool() ? 1 : 0);
			int num4 = num;
			ltpData1Present = (byte)num != 0;
			if (num4 != 0)
			{
				if (ltPredict1 == null)
				{
					LTPrediction.___003Cclinit_003E();
					ltPredict1 = new LTPrediction(frameLength);
				}
				ltPredict1.decode(_in, this, profile);
			}
			return;
		}
		string message = new StringBuilder().append("unexpected profile for LTP: ").append(profile).toString();
		throw new AACException(message);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 95, 98, 105, 104, 109, 108, 109, 104, 104 })]
	public ICSInfo(int frameLength)
	{
		this.frameLength = frameLength;
		windowShape = new int[2];
		windowSequence = WindowSequence.___003C_003EONLY_LONG_SEQUENCE;
		windowGroupLength = new int[8];
		ltpData1Present = false;
		ltpData2Present = false;
	}

	[LineNumberTable(268)]
	public virtual int getSWBCount()
	{
		return swbCount;
	}

	[LineNumberTable(304)]
	public virtual bool isICPredictionPresent()
	{
		return predictionDataPresent;
	}

	[LineNumberTable(308)]
	public virtual ICPrediction getICPrediction()
	{
		return icPredict;
	}

	[LineNumberTable(316)]
	public virtual LTPrediction getLTPrediction1()
	{
		return ltPredict1;
	}

	[LineNumberTable(320)]
	public virtual bool isLTPrediction2Present()
	{
		return ltpData2Present;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 60, 66, 117, 117, 119 })]
	public virtual void unsetPredictionSFB(int sfb)
	{
		if (predictionDataPresent)
		{
			icPredict.setPredictionUnused(sfb);
		}
		if (ltpData1Present)
		{
			ltPredict1.setPredictionUnused(sfb);
		}
		if (ltpData2Present)
		{
			ltPredict2.setPredictionUnused(sfb);
		}
	}
}
