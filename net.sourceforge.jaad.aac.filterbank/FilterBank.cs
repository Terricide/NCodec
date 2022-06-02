using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.filterbank;

[Implements(new string[] { "net.sourceforge.jaad.aac.syntax.SyntaxConstants", "net.sourceforge.jaad.aac.filterbank.SineWindows", "net.sourceforge.jaad.aac.filterbank.KBDWindows" })]
public class FilterBank : java.lang.Object, SyntaxConstants, SineWindows, KBDWindows
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : java.lang.Object
	{
		[Modifiers(Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		internal static int[] _0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[LineNumberTable(49)]
		static _1()
		{
			_0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence = new int[(nint)ICSInfo.WindowSequence.values().LongLength];
			NoSuchFieldError noSuchFieldError2;
			try
			{
				_0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[ICSInfo.WindowSequence.___003C_003EONLY_LONG_SEQUENCE.ordinal()] = 1;
			}
			catch (System.Exception x)
			{
				NoSuchFieldError noSuchFieldError = ByteCodeHelper.MapException<NoSuchFieldError>(x, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError == null)
				{
					throw;
				}
				noSuchFieldError2 = noSuchFieldError;
				goto IL_0037;
			}
			goto IL_003d;
			IL_0037:
			NoSuchFieldError noSuchFieldError3 = noSuchFieldError2;
			goto IL_003d;
			IL_003d:
			NoSuchFieldError noSuchFieldError5;
			try
			{
				_0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[ICSInfo.WindowSequence.___003C_003ELONG_START_SEQUENCE.ordinal()] = 2;
			}
			catch (System.Exception x2)
			{
				NoSuchFieldError noSuchFieldError4 = ByteCodeHelper.MapException<NoSuchFieldError>(x2, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError4 == null)
				{
					throw;
				}
				noSuchFieldError5 = noSuchFieldError4;
				goto IL_0062;
			}
			goto IL_0068;
			IL_0062:
			NoSuchFieldError noSuchFieldError6 = noSuchFieldError5;
			goto IL_0068;
			IL_0068:
			NoSuchFieldError noSuchFieldError8;
			try
			{
				_0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[ICSInfo.WindowSequence.___003C_003EEIGHT_SHORT_SEQUENCE.ordinal()] = 3;
			}
			catch (System.Exception x3)
			{
				NoSuchFieldError noSuchFieldError7 = ByteCodeHelper.MapException<NoSuchFieldError>(x3, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError7 == null)
				{
					throw;
				}
				noSuchFieldError8 = noSuchFieldError7;
				goto IL_008e;
			}
			goto IL_0096;
			IL_008e:
			NoSuchFieldError noSuchFieldError9 = noSuchFieldError8;
			goto IL_0096;
			IL_0096:
			NoSuchFieldError noSuchFieldError11;
			try
			{
				_0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[ICSInfo.WindowSequence.___003C_003ELONG_STOP_SEQUENCE.ordinal()] = 4;
				return;
			}
			catch (System.Exception x4)
			{
				NoSuchFieldError noSuchFieldError10 = ByteCodeHelper.MapException<NoSuchFieldError>(x4, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError10 == null)
				{
					throw;
				}
				noSuchFieldError11 = noSuchFieldError10;
			}
			NoSuchFieldError noSuchFieldError12 = noSuchFieldError11;
		}

		_1()
		{
			throw null;
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] LONG_WINDOWS;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] SHORT_WINDOWS;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int length;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int shortLen;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int mid;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int trans;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private MDCT mdctShort;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private MDCT mdctLong;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[] buf;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] overlaps;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] SINE_1024
	{
		[HideFromJava]
		get
		{
			return SineWindows.SINE_1024;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] SINE_128
	{
		[HideFromJava]
		get
		{
			return SineWindows.SINE_128;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] SINE_960
	{
		[HideFromJava]
		get
		{
			return SineWindows.SINE_960;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] SINE_120
	{
		[HideFromJava]
		get
		{
			return SineWindows.SINE_120;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] KBD_1024
	{
		[HideFromJava]
		get
		{
			return KBDWindows.KBD_1024;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] KBD_128
	{
		[HideFromJava]
		get
		{
			return KBDWindows.KBD_128;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] KBD_960
	{
		[HideFromJava]
		get
		{
			return KBDWindows.KBD_960;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] KBD_120
	{
		[HideFromJava]
		get
		{
			return KBDWindows.KBD_120;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 137, 161, 67, 105, 100, 108, 105, 125, 191,
		0, 108, 108, 125, 157, 118, 143, 116, 148, 127,
		16, 116
	})]
	public FilterBank(bool smallFrames, int channels)
	{
		if (smallFrames)
		{
			length = 960;
			shortLen = 120;
			LONG_WINDOWS = new float[2][]
			{
				SineWindows.SINE_960,
				KBDWindows.KBD_960
			};
			SHORT_WINDOWS = new float[2][]
			{
				SineWindows.SINE_120,
				KBDWindows.KBD_120
			};
		}
		else
		{
			length = 1024;
			shortLen = 128;
			LONG_WINDOWS = new float[2][]
			{
				SineWindows.SINE_1024,
				KBDWindows.KBD_1024
			};
			SHORT_WINDOWS = new float[2][]
			{
				SineWindows.SINE_128,
				KBDWindows.KBD_128
			};
		}
		mid = (length - shortLen) / 2;
		trans = shortLen / 2;
		mdctShort = new MDCT(shortLen * 2);
		mdctLong = new MDCT(length * 2);
		int num = length;
		int[] array = new int[2];
		int num2 = (array[1] = num);
		num2 = (array[0] = channels);
		overlaps = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		buf = new float[2 * length];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		130,
		66,
		107,
		159,
		10,
		150,
		108,
		62,
		231,
		69,
		111,
		63,
		9,
		231,
		69,
		150,
		108,
		62,
		231,
		69,
		108,
		51,
		167,
		108,
		63,
		23,
		167,
		111,
		55,
		231,
		69,
		103,
		63,
		7,
		231,
		69,
		108,
		40,
		167,
		111,
		127,
		13,
		127,
		80,
		127,
		80,
		127,
		80,
		byte.MaxValue,
		92,
		59,
		234,
		73,
		111,
		127,
		74,
		127,
		67,
		127,
		67,
		127,
		67,
		byte.MaxValue,
		35,
		59,
		234,
		71,
		111,
		55,
		231,
		69,
		182,
		110,
		42,
		169,
		110,
		63,
		24,
		169,
		110,
		63,
		32,
		201,
		110,
		63,
		12,
		233,
		69
	})]
	public virtual void process(ICSInfo.WindowSequence windowSequence, int windowShape, int windowShapePrev, float[] _in, float[] @out, int channel)
	{
		float[] overlap = overlaps[channel];
		switch (_1._0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[windowSequence.ordinal()])
		{
		case 1:
		{
			mdctLong.process(_in, 0, buf, 0);
			for (int i = 0; i < length; i++)
			{
				@out[i] = overlap[i] + buf[i] * LONG_WINDOWS[windowShapePrev][i];
			}
			for (int i = 0; i < length; i++)
			{
				overlap[i] = buf[length + i] * LONG_WINDOWS[windowShape][length - 1 - i];
			}
			break;
		}
		case 2:
		{
			mdctLong.process(_in, 0, buf, 0);
			for (int j = 0; j < length; j++)
			{
				@out[j] = overlap[j] + buf[j] * LONG_WINDOWS[windowShapePrev][j];
			}
			for (int j = 0; j < mid; j++)
			{
				overlap[j] = buf[length + j];
			}
			for (int j = 0; j < shortLen; j++)
			{
				overlap[mid + j] = buf[length + mid + j] * SHORT_WINDOWS[windowShape][shortLen - j - 1];
			}
			for (int j = 0; j < mid; j++)
			{
				overlap[mid + shortLen + j] = 0f;
			}
			break;
		}
		case 3:
		{
			for (int k = 0; k < 8; k++)
			{
				mdctShort.process(_in, k * shortLen, buf, 2 * k * shortLen);
			}
			for (int k = 0; k < mid; k++)
			{
				@out[k] = overlap[k];
			}
			for (int k = 0; k < shortLen; k++)
			{
				@out[mid + k] = overlap[mid + k] + buf[k] * SHORT_WINDOWS[windowShapePrev][k];
				@out[mid + 1 * shortLen + k] = overlap[mid + shortLen * 1 + k] + buf[shortLen * 1 + k] * SHORT_WINDOWS[windowShape][shortLen - 1 - k] + buf[shortLen * 2 + k] * SHORT_WINDOWS[windowShape][k];
				@out[mid + 2 * shortLen + k] = overlap[mid + shortLen * 2 + k] + buf[shortLen * 3 + k] * SHORT_WINDOWS[windowShape][shortLen - 1 - k] + buf[shortLen * 4 + k] * SHORT_WINDOWS[windowShape][k];
				@out[mid + 3 * shortLen + k] = overlap[mid + shortLen * 3 + k] + buf[shortLen * 5 + k] * SHORT_WINDOWS[windowShape][shortLen - 1 - k] + buf[shortLen * 6 + k] * SHORT_WINDOWS[windowShape][k];
				if (k < trans)
				{
					@out[mid + 4 * shortLen + k] = overlap[mid + shortLen * 4 + k] + buf[shortLen * 7 + k] * SHORT_WINDOWS[windowShape][shortLen - 1 - k] + buf[shortLen * 8 + k] * SHORT_WINDOWS[windowShape][k];
				}
			}
			for (int k = 0; k < shortLen; k++)
			{
				if (k >= trans)
				{
					overlap[mid + 4 * shortLen + k - length] = buf[shortLen * 7 + k] * SHORT_WINDOWS[windowShape][shortLen - 1 - k] + buf[shortLen * 8 + k] * SHORT_WINDOWS[windowShape][k];
				}
				overlap[mid + 5 * shortLen + k - length] = buf[shortLen * 9 + k] * SHORT_WINDOWS[windowShape][shortLen - 1 - k] + buf[shortLen * 10 + k] * SHORT_WINDOWS[windowShape][k];
				overlap[mid + 6 * shortLen + k - length] = buf[shortLen * 11 + k] * SHORT_WINDOWS[windowShape][shortLen - 1 - k] + buf[shortLen * 12 + k] * SHORT_WINDOWS[windowShape][k];
				overlap[mid + 7 * shortLen + k - length] = buf[shortLen * 13 + k] * SHORT_WINDOWS[windowShape][shortLen - 1 - k] + buf[shortLen * 14 + k] * SHORT_WINDOWS[windowShape][k];
				overlap[mid + 8 * shortLen + k - length] = buf[shortLen * 15 + k] * SHORT_WINDOWS[windowShape][shortLen - 1 - k];
			}
			for (int k = 0; k < mid; k++)
			{
				overlap[mid + shortLen + k] = 0f;
			}
			break;
		}
		case 4:
		{
			mdctLong.process(_in, 0, buf, 0);
			for (int l = 0; l < mid; l++)
			{
				@out[l] = overlap[l];
			}
			for (int l = 0; l < shortLen; l++)
			{
				@out[mid + l] = overlap[mid + l] + buf[mid + l] * SHORT_WINDOWS[windowShapePrev][l];
			}
			for (int l = 0; l < mid; l++)
			{
				@out[mid + shortLen + l] = overlap[mid + shortLen + l] + buf[mid + shortLen + l];
			}
			for (int l = 0; l < length; l++)
			{
				overlap[l] = buf[length + l] * LONG_WINDOWS[windowShape][length - 1 - l];
			}
			break;
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 130, 159, 10, 113, 121, 31, 17, 231,
		71, 108, 57, 167, 108, 59, 167, 108, 63, 31,
		167, 111, 63, 4, 231, 70, 108, 46, 167, 108,
		63, 8, 167, 108, 63, 10, 167, 108, 63, 17,
		231, 69, 118
	})]
	public virtual void processLTP(ICSInfo.WindowSequence windowSequence, int windowShape, int windowShapePrev, float[] _in, float[] @out)
	{
		switch (_1._0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[windowSequence.ordinal()])
		{
		case 1:
		{
			for (int i = length - 1; i >= 0; i += -1)
			{
				buf[i] = _in[i] * LONG_WINDOWS[windowShapePrev][i];
				buf[i + length] = _in[i + length] * LONG_WINDOWS[windowShape][length - 1 - i];
			}
			break;
		}
		case 2:
		{
			for (int j = 0; j < length; j++)
			{
				buf[j] = _in[j] * LONG_WINDOWS[windowShapePrev][j];
			}
			for (int j = 0; j < mid; j++)
			{
				buf[j + length] = _in[j + length];
			}
			for (int j = 0; j < shortLen; j++)
			{
				buf[j + length + mid] = _in[j + length + mid] * SHORT_WINDOWS[windowShape][shortLen - 1 - j];
			}
			for (int j = 0; j < mid; j++)
			{
				buf[j + length + mid + shortLen] = 0f;
			}
			break;
		}
		case 4:
		{
			for (int k = 0; k < mid; k++)
			{
				buf[k] = 0f;
			}
			for (int k = 0; k < shortLen; k++)
			{
				buf[k + mid] = _in[k + mid] * SHORT_WINDOWS[windowShapePrev][k];
			}
			for (int k = 0; k < mid; k++)
			{
				buf[k + mid + shortLen] = _in[k + mid + shortLen];
			}
			for (int k = 0; k < length; k++)
			{
				buf[k + length] = _in[k + length] * LONG_WINDOWS[windowShape][length - 1 - k];
			}
			break;
		}
		}
		mdctLong.processForward(buf, @out);
	}

	[LineNumberTable(176)]
	public virtual float[] getOverlap(int channel)
	{
		return overlaps[channel];
	}
}
