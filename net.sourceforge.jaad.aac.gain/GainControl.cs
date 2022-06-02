using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.syntax;
using org.jcodec.platform;

namespace net.sourceforge.jaad.aac.gain;

public class GainControl : java.lang.Object, GCConstants
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

		[LineNumberTable(46)]
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

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int frameLen;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int lbLong;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int lbShort;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private IMDCT imdct;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private IPQF ipqf;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[] buffer1;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[] _function;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] buffer2;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] overlap;

	private int maxBand;

	private int[][][] level;

	private int[][][] levelPrev;

	private int[][][] location;

	private int[][][] locationPrev;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int BANDS = 4;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int MAX_CHANNELS = 5;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int NPQFTAPS = 96;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int NPEPARTS = 64;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public const int ID_GAIN = 16;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[] LN_GAIN
	{
		[HideFromJava]
		get
		{
			return GCConstants.LN_GAIN;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		116,
		130,
		145,
		138,
		147,
		110,
		122,
		24,
		231,
		69,
		111,
		121,
		123,
		253,
		61,
		234,
		70,
		108,
		123,
		159,
		3,
		241,
		60,
		231,
		70,
		127,
		9,
		byte.MaxValue,
		9,
		42,
		234,
		88,
		123,
		223,
		11,
		147,
		112,
		63,
		7,
		201,
		110,
		63,
		7,
		201,
		110,
		63,
		5,
		169,
		116,
		127,
		11,
		159,
		11
	})]
	private void compensate(float[] _in, float[][] @out, ICSInfo.WindowSequence winSeq, int band)
	{
		if (winSeq.equals(ICSInfo.WindowSequence.___003C_003EEIGHT_SHORT_SEQUENCE))
		{
			for (int k = 0; k < 8; k++)
			{
				calculateFunctionData(lbShort * 2, band, winSeq, k);
				for (int j = 0; j < lbShort * 2; j++)
				{
					int a3 = band * lbLong * 2 + k * lbShort * 2 + j;
					int num = a3;
					float[] array = _in;
					array[num] *= _function[j];
				}
				for (int j = 0; j < lbShort; j++)
				{
					int a2 = j + lbLong * 7 / 16 + lbShort * k;
					int b2 = band * lbLong * 2 + k * lbShort * 2 + j;
					float[] obj = overlap[band];
					int num = a2;
					float[] array = obj;
					array[num] += _in[b2];
				}
				for (int j = 0; j < lbShort; j++)
				{
					int a = j + lbLong * 7 / 16 + lbShort * (k + 1);
					int b = band * lbLong * 2 + k * lbShort * 2 + lbShort + j;
					overlap[band][a] = _in[b];
				}
				locationPrev[band][0] = Platform.copyOfInt(location[band][k], location[band][k].Length);
				levelPrev[band][0] = Platform.copyOfInt(level[band][k], level[band][k].Length);
			}
			ByteCodeHelper.arraycopy_primitive_4(overlap[band], 0, @out[band], 0, lbLong);
			ByteCodeHelper.arraycopy_primitive_4(overlap[band], lbLong, overlap[band], 0, lbLong);
		}
		else
		{
			calculateFunctionData(lbLong * 2, band, winSeq, 0);
			for (int i = 0; i < lbLong * 2; i++)
			{
				int num = band * lbLong * 2 + i;
				float[] array = _in;
				array[num] *= _function[i];
			}
			for (int i = 0; i < lbLong; i++)
			{
				@out[band][i] = overlap[band][i] + _in[band * lbLong * 2 + i];
			}
			for (int i = 0; i < lbLong; i++)
			{
				overlap[band][i] = _in[band * lbLong * 2 + lbLong + i];
			}
			int lastBlock = (winSeq.equals(ICSInfo.WindowSequence.___003C_003EONLY_LONG_SEQUENCE) ? 1 : 0);
			locationPrev[band][0] = Platform.copyOfInt(location[band][lastBlock], location[band][lastBlock].Length);
			levelPrev[band][0] = Platform.copyOfInt(level[band][lastBlock], level[band][lastBlock].Length);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 130, 105, 105, 104, 106, 107, 139, 106,
		191, 7, 105, 100, 131, 102, 105, 103, 131, 103,
		105, 230, 70, 177, 117, 180, 123, 242, 69, 100,
		110, 108, 106, 42, 201, 159, 4, 106, 53, 169,
		106, 53, 201, 113, 106, 48, 169, 108, 109, 51,
		201, 110, 106, 51, 233, 70, 105, 53, 169
	})]
	private void calculateFunctionData(int samples, int band, ICSInfo.WindowSequence winSeq, int blockID)
	{
		int[] locA = new int[10];
		float[] levA = new float[10];
		float[] modFunc = new float[samples];
		float[] buf1 = new float[samples / 2];
		float[] buf2 = new float[samples / 2];
		float[] buf3 = new float[samples / 2];
		int maxLocGain0 = 0;
		int maxLocGain1 = 0;
		int maxLocGain2 = 0;
		switch (_1._0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[winSeq.ordinal()])
		{
		case 1:
		case 2:
			maxLocGain0 = (maxLocGain1 = samples / 2);
			maxLocGain2 = 0;
			break;
		case 3:
			maxLocGain0 = samples / 2;
			maxLocGain1 = samples * 7 / 32;
			maxLocGain2 = samples / 16;
			break;
		case 4:
			maxLocGain0 = samples / 16;
			maxLocGain1 = samples * 7 / 32;
			maxLocGain2 = samples / 2;
			break;
		}
		calculateFMD(band, 0, prev: true, maxLocGain0, samples, locA, levA, buf1);
		int block = (winSeq.equals(ICSInfo.WindowSequence.___003C_003EEIGHT_SHORT_SEQUENCE) ? blockID : 0);
		float secLevel = calculateFMD(band, block, prev: false, maxLocGain1, samples, locA, levA, buf2);
		if (winSeq.equals(ICSInfo.WindowSequence.___003C_003ELONG_START_SEQUENCE) || winSeq.equals(ICSInfo.WindowSequence.___003C_003ELONG_STOP_SEQUENCE))
		{
			calculateFMD(band, 1, prev: false, maxLocGain2, samples, locA, levA, buf3);
		}
		int flatLen = 0;
		if (winSeq.equals(ICSInfo.WindowSequence.___003C_003ELONG_STOP_SEQUENCE))
		{
			flatLen = samples / 2 - maxLocGain0 - maxLocGain1;
			for (int j = 0; j < flatLen; j++)
			{
				modFunc[j] = 1f;
			}
		}
		if (winSeq.equals(ICSInfo.WindowSequence.___003C_003EONLY_LONG_SEQUENCE) || winSeq.equals(ICSInfo.WindowSequence.___003C_003EEIGHT_SHORT_SEQUENCE))
		{
			levA[0] = 1f;
		}
		for (int i = 0; i < maxLocGain0; i++)
		{
			modFunc[i + flatLen] = levA[0] * secLevel * buf1[i];
		}
		for (int i = 0; i < maxLocGain1; i++)
		{
			modFunc[i + flatLen + maxLocGain0] = levA[0] * buf2[i];
		}
		if (winSeq.equals(ICSInfo.WindowSequence.___003C_003ELONG_START_SEQUENCE))
		{
			for (int i = 0; i < maxLocGain2; i++)
			{
				modFunc[i + maxLocGain0 + maxLocGain1] = buf3[i];
			}
			flatLen = samples / 2 - maxLocGain1 - maxLocGain2;
			for (int i = 0; i < flatLen; i++)
			{
				modFunc[i + maxLocGain0 + maxLocGain1 + maxLocGain2] = 1f;
			}
		}
		else if (winSeq.equals(ICSInfo.WindowSequence.___003C_003ELONG_STOP_SEQUENCE))
		{
			for (int i = 0; i < maxLocGain2; i++)
			{
				modFunc[i + flatLen + maxLocGain0 + maxLocGain1] = buf3[i];
			}
		}
		for (int i = 0; i < samples; i++)
		{
			_function[i] = 1f / modFunc[i];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 82, 129, 67, 107, 123, 123, 197, 109, 110,
		109, 127, 10, 250, 60, 236, 72, 102, 112, 105,
		167, 106, 173, 106, 102, 108, 48, 9, 233, 71,
		109, 127, 33, 15, 236, 69
	})]
	private float calculateFMD(int bd, int wd, bool prev, int maxLocGain, int samples, int[] loc, float[] lev, float[] fmd)
	{
		int[] k = new int[samples / 2];
		int[] lct = ((!prev) ? location[bd][wd] : locationPrev[bd][wd]);
		int[] lvl = ((!prev) ? level[bd][wd] : levelPrev[bd][wd]);
		int length = lct.Length;
		for (int i = 0; i < length; i++)
		{
			loc[i + 1] = 8 * lct[i];
			int lngain = getGainChangePointID(lvl[i]);
			if (lngain < 0)
			{
				lev[i + 1] = 1f / (float)java.lang.Math.pow(2.0, -lngain);
			}
			else
			{
				lev[i + 1] = (float)java.lang.Math.pow(2.0, lngain);
			}
		}
		loc[0] = 0;
		if (length == 0)
		{
			lev[0] = 1f;
		}
		else
		{
			lev[0] = lev[1];
		}
		float secLevel = lev[0];
		loc[length + 1] = maxLocGain;
		lev[length + 1] = 1f;
		for (int i = 0; i < maxLocGain; i++)
		{
			k[i] = 0;
			for (int j = 0; j <= length + 1; j++)
			{
				if (loc[j] <= i)
				{
					k[i] = j;
				}
			}
		}
		for (int i = 0; i < maxLocGain; i++)
		{
			if (i >= loc[k[i]] && i <= loc[k[i]] + 7)
			{
				fmd[i] = interpolateGain(lev[k[i]], lev[k[i] + 1], i - loc[k[i]]);
			}
			else
			{
				fmd[i] = lev[k[i] + 1];
			}
		}
		return secLevel;
	}

	[LineNumberTable(new byte[] { 159, 71, 162, 104, 45, 167 })]
	private int getGainChangePointID(int lngain)
	{
		for (int i = 0; i < 16; i++)
		{
			if (lngain == GCConstants.LN_GAIN[i])
			{
				return i;
			}
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 67, 66, 122, 122 })]
	private float interpolateGain(float alev0, float alev1, int iloc)
	{
		float a0 = (float)(java.lang.Math.log(alev0) / java.lang.Math.log(2.0));
		float a1 = (float)(java.lang.Math.log(alev1) / java.lang.Math.log(2.0));
		return (float)java.lang.Math.pow(2.0, ((float)(8 - iloc) * a0 + (float)iloc * a1) / 8f);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 105, 104, 106, 111, 109, 108, 109,
		109, 111, 127, 16, 116, 127, 18
	})]
	public GainControl(int frameLen)
	{
		this.frameLen = frameLen;
		lbLong = frameLen / 4;
		lbShort = lbLong / 8;
		imdct = new IMDCT(frameLen);
		ipqf = new IPQF();
		levelPrev = new int[0][][];
		locationPrev = new int[0][][];
		buffer1 = new float[frameLen / 2];
		int num = lbLong;
		int[] array = new int[2];
		int num2 = (array[1] = num);
		num2 = (array[0] = 4);
		buffer2 = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		_function = new float[lbLong * 2];
		int num3 = lbLong * 2;
		array = new int[2];
		num2 = (array[1] = num3);
		num2 = (array[0] = 4);
		overlap = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 132, 162, 144, 99, 159, 7, 99, 99, 99,
		131, 99, 99, 99, 131, 99, 99, 99, 131, 99,
		99, 99, 131, 130, 127, 20, 191, 20, 113, 108,
		106, 116, 116, 106, 119, 107, 248, 61, 233, 60,
		44, 236, 76
	})]
	public virtual void decode(IBitStream _in, ICSInfo.WindowSequence winSeq)
	{
		maxBand = _in.readBits(2) + 1;
		int locBits2 = 0;
		int wdLen;
		int locBits;
		switch (_1._0024SwitchMap_0024net_0024sourceforge_0024jaad_0024aac_0024syntax_0024ICSInfo_0024WindowSequence[winSeq.ordinal()])
		{
		default:
			return;
		case 1:
			wdLen = 1;
			locBits = 5;
			locBits2 = 5;
			break;
		case 2:
			wdLen = 8;
			locBits = 2;
			locBits2 = 2;
			break;
		case 3:
			wdLen = 2;
			locBits = 4;
			locBits2 = 2;
			break;
		case 4:
			wdLen = 2;
			locBits = 4;
			locBits2 = 5;
			break;
		}
		int num = maxBand;
		int num2 = wdLen;
		int[] array = new int[2];
		int num3 = (array[1] = num2);
		num3 = (array[0] = num);
		level = (int[][][])ByteCodeHelper.multianewarray(typeof(int[][][]).TypeHandle, array);
		int num4 = maxBand;
		int num5 = wdLen;
		array = new int[2];
		num3 = (array[1] = num5);
		num3 = (array[0] = num4);
		location = (int[][][])ByteCodeHelper.multianewarray(typeof(int[][][]).TypeHandle, array);
		for (int bd = 1; bd < maxBand; bd++)
		{
			for (int wd = 0; wd < wdLen; wd++)
			{
				int len = _in.readBits(3);
				level[bd][wd] = new int[len];
				location[bd][wd] = new int[len];
				for (int i = 0; i < len; i++)
				{
					level[bd][wd][i] = _in.readBits(4);
					int bits = ((wd != 0) ? locBits2 : locBits);
					location[bd][wd][i] = _in.readBits(bits);
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 120, 98, 151, 103, 54, 199, 127, 2 })]
	public virtual void process(float[] data, int winShape, int winShapePrev, ICSInfo.WindowSequence winSeq)
	{
		imdct.process(data, buffer1, winShape, winShapePrev, winSeq);
		for (int i = 0; i < 4; i++)
		{
			compensate(buffer1, buffer2, winSeq, i);
		}
		ipqf.process(buffer2, frameLen, maxBand, data);
	}
}
