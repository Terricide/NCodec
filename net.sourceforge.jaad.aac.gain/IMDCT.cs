using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.gain;

[Implements(new string[] { "net.sourceforge.jaad.aac.gain.GCConstants", "net.sourceforge.jaad.aac.gain.IMDCTTables", "net.sourceforge.jaad.aac.gain.Windows" })]
internal class IMDCT : java.lang.Object, GCConstants, IMDCTTables, Windows
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

		[LineNumberTable(66)]
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
				_0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[ICSInfo.WindowSequence.___003C_003EEIGHT_SHORT_SEQUENCE.ordinal()] = 2;
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
				_0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[ICSInfo.WindowSequence.___003C_003ELONG_START_SEQUENCE.ordinal()] = 3;
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

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[][] LONG_WINDOWS;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[][] SHORT_WINDOWS;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int frameLen;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int shortFrameLen;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int lbLong;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int lbShort;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int lbMid;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 104, 106, 106, 111, 118 })]
	internal IMDCT(int frameLen)
	{
		this.frameLen = frameLen;
		lbLong = frameLen / 4;
		shortFrameLen = frameLen / 8;
		lbShort = shortFrameLen / 4;
		lbMid = (lbLong - lbShort) / 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 135, 130, 173, 114, 106, 106, 111, 127, 28,
		31, 21, 42, 42, 234, 74, 108, 113, 127, 15,
		31, 7, 44, 236, 72, 105, 48, 169
	})]
	internal virtual void process(float[] _in, float[] @out, int winShape, int winShapePrev, ICSInfo.WindowSequence winSeq)
	{
		float[] buf = new float[frameLen];
		if (winSeq.equals(ICSInfo.WindowSequence.___003C_003EEIGHT_SHORT_SEQUENCE))
		{
			for (int b2 = 0; b2 < 4; b2++)
			{
				for (int k = 0; k < 8; k++)
				{
					for (int j = 0; j < lbShort; j++)
					{
						int num = b2;
						if (2 == -1 || num % 2 == 0)
						{
							buf[lbLong * b2 + lbShort * k + j] = _in[shortFrameLen * k + lbShort * b2 + j];
						}
						else
						{
							buf[lbLong * b2 + lbShort * k + j] = _in[shortFrameLen * k + lbShort * b2 + lbShort - 1 - j];
						}
					}
				}
			}
		}
		else
		{
			for (int b = 0; b < 4; b++)
			{
				for (int i = 0; i < lbLong; i++)
				{
					int num2 = b;
					if (2 == -1 || num2 % 2 == 0)
					{
						buf[lbLong * b + i] = _in[lbLong * b + i];
					}
					else
					{
						buf[lbLong * b + i] = _in[lbLong * b + lbLong - 1 - i];
					}
				}
			}
		}
		for (int b = 0; b < 4; b++)
		{
			process2(buf, @out, winSeq, winShape, winShapePrev, b);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 128, 130, 109, 111, 111, 111, 208, 159, 10,
		113, 112, 27, 233, 70, 113, 112, 123, 113, 252,
		60, 236, 72, 110, 48, 169, 110, 49, 201, 110,
		63, 8, 169, 113, 63, 0, 233, 69, 110, 42,
		169, 110, 55, 169, 110, 56, 169, 110, 63, 15,
		233, 71, 145, 108, 110, 61, 169, 120, 115, 112,
		112, 63, 9, 233, 57, 236, 75, 134, 110, 51,
		169, 112, 112, 60, 201
	})]
	private void process2(float[] _in, float[] @out, ICSInfo.WindowSequence winSeq, int winShape, int winShapePrev, int band)
	{
		float[] bufIn = new float[lbLong];
		float[] bufOut = new float[lbLong * 2];
		float[] window = new float[lbLong * 2];
		float[] window2 = new float[lbShort * 2];
		float[] window3 = new float[lbShort * 2];
		switch (_1._0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[winSeq.ordinal()])
		{
		case 1:
		{
			for (int i = 0; i < lbLong; i++)
			{
				window[i] = LONG_WINDOWS[winShapePrev][i];
				window[lbLong * 2 - 1 - i] = LONG_WINDOWS[winShape][i];
			}
			break;
		}
		case 2:
		{
			for (int j = 0; j < lbShort; j++)
			{
				window2[j] = SHORT_WINDOWS[winShapePrev][j];
				window2[lbShort * 2 - 1 - j] = SHORT_WINDOWS[winShape][j];
				window3[j] = SHORT_WINDOWS[winShape][j];
				window3[lbShort * 2 - 1 - j] = SHORT_WINDOWS[winShape][j];
			}
			break;
		}
		case 3:
		{
			for (int k = 0; k < lbLong; k++)
			{
				window[k] = LONG_WINDOWS[winShapePrev][k];
			}
			for (int k = 0; k < lbMid; k++)
			{
				window[k + lbLong] = 1f;
			}
			for (int k = 0; k < lbShort; k++)
			{
				window[k + lbMid + lbLong] = SHORT_WINDOWS[winShape][lbShort - 1 - k];
			}
			for (int k = 0; k < lbMid; k++)
			{
				window[k + lbMid + lbLong + lbShort] = 0f;
			}
			break;
		}
		case 4:
		{
			for (int l = 0; l < lbMid; l++)
			{
				window[l] = 0f;
			}
			for (int l = 0; l < lbShort; l++)
			{
				window[l + lbMid] = SHORT_WINDOWS[winShapePrev][l];
			}
			for (int l = 0; l < lbMid; l++)
			{
				window[l + lbMid + lbShort] = 1f;
			}
			for (int l = 0; l < lbLong; l++)
			{
				window[l + lbMid + lbShort + lbMid] = LONG_WINDOWS[winShape][lbLong - 1 - l];
			}
			break;
		}
		}
		if (winSeq.equals(ICSInfo.WindowSequence.___003C_003EEIGHT_SHORT_SEQUENCE))
		{
			for (int n = 0; n < 8; n++)
			{
				for (int k2 = 0; k2 < lbShort; k2++)
				{
					bufIn[k2] = _in[band * lbLong + n * lbShort + k2];
				}
				if (n == 0)
				{
					ByteCodeHelper.arraycopy_primitive_4(window2, 0, window, 0, lbShort * 2);
				}
				else
				{
					ByteCodeHelper.arraycopy_primitive_4(window3, 0, window, 0, lbShort * 2);
				}
				imdct(bufIn, bufOut, window, lbShort);
				for (int k2 = 0; k2 < lbShort * 2; k2++)
				{
					@out[band * lbLong * 2 + n * lbShort * 2 + k2] = bufOut[k2] / 32f;
				}
			}
		}
		else
		{
			for (int m = 0; m < lbLong; m++)
			{
				bufIn[m] = _in[band * lbLong + m];
			}
			imdct(bufIn, bufOut, window, lbLong);
			for (int m = 0; m < lbLong * 2; m++)
			{
				@out[band * lbLong * 2 + m] = bufOut[m] / 256f;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159,
		108,
		162,
		134,
		106,
		103,
		137,
		103,
		103,
		137,
		145,
		137,
		105,
		43,
		169,
		106,
		51,
		233,
		69,
		127,
		15,
		108,
		127,
		9,
		31,
		9,
		236,
		70,
		169,
		108,
		159,
		48,
		byte.MaxValue,
		53,
		61,
		236,
		72,
		107,
		110,
		51,
		169,
		113,
		49,
		201,
		106,
		56,
		169
	})]
	private void imdct(float[] _in, float[] @out, float[] window, int n)
	{
		int n2 = n / 2;
		float[][] table;
		float[][] table2;
		switch (n)
		{
		case 256:
			table = IMDCTTables.IMDCT_TABLE_256;
			table2 = IMDCTTables.IMDCT_POST_TABLE_256;
			break;
		case 32:
			table = IMDCTTables.IMDCT_TABLE_32;
			table2 = IMDCTTables.IMDCT_POST_TABLE_32;
			break;
		default:
			
			throw new AACException("gain control: unexpected IMDCT length");
		}
		float[] tmp = new float[n];
		for (int i = 0; i < n2; i++)
		{
			tmp[i] = _in[2 * i];
		}
		for (int i = n2; i < n; i++)
		{
			tmp[i] = 0f - _in[2 * n - 1 - 2 * i];
		}
		int[] array = new int[2];
		int num = (array[1] = 2);
		num = (array[0] = n2);
		float[][] buf = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		for (int i = 0; i < n2; i++)
		{
			buf[i][0] = table[i][0] * tmp[2 * i] - table[i][1] * tmp[2 * i + 1];
			buf[i][1] = table[i][0] * tmp[2 * i + 1] + table[i][1] * tmp[2 * i];
		}
		FFT.process(buf, n2);
		for (int i = 0; i < n2; i++)
		{
			tmp[i] = table2[i][0] * buf[i][0] + table2[i][1] * buf[n2 - 1 - i][0] + table2[i][2] * buf[i][1] + table2[i][3] * buf[n2 - 1 - i][1];
			tmp[n - 1 - i] = table2[i][2] * buf[i][0] - table2[i][3] * buf[n2 - 1 - i][0] - table2[i][0] * buf[i][1] + table2[i][1] * buf[n2 - 1 - i][1];
		}
		ByteCodeHelper.arraycopy_primitive_4(tmp, n2, @out, 0, n2);
		for (int i = n2; i < n * 3 / 2; i++)
		{
			@out[i] = 0f - tmp[n * 3 / 2 - 1 - i];
		}
		for (int i = n * 3 / 2; i < n * 2; i++)
		{
			@out[i] = 0f - tmp[i - n * 3 / 2];
		}
		for (int i = 0; i < n; i++)
		{
			num = i;
			@out[num] *= window[i];
		}
	}

	[LineNumberTable(new byte[] { 159, 138, 98, 124 })]
	static IMDCT()
	{
		LONG_WINDOWS = new float[2][]
		{
			Windows.SINE_256,
			Windows.KBD_256
		};
		SHORT_WINDOWS = new float[2][]
		{
			Windows.SINE_32,
			Windows.KBD_32
		};
	}
}
