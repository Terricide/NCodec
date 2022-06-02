using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.decode;

public class MBlock : Object
{
	internal class IPCM : Object
	{
		public int[] samplesLuma;

		public int[] samplesChroma;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 98, 98, 105, 113, 112, 144, 113 })]
		public IPCM(ColorSpace chromaFormat)
		{
			samplesLuma = new int[256];
			int MbWidthC = 16 >> chromaFormat.compWidth[1];
			int MbHeightC = 16 >> chromaFormat.compHeight[1];
			samplesChroma = new int[2 * MbWidthC * MbHeightC];
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 96, 130, 109, 111 })]
		public virtual void clean()
		{
			Arrays.fill(samplesLuma, 0);
			Arrays.fill(samplesChroma, 0);
		}
	}

	internal class PB168x168 : Object
	{
		public int[] refIdx1;

		public int[] refIdx2;

		public int[] mvdX1;

		public int[] mvdY1;

		public int[] mvdX2;

		public int[] mvdY2;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 117, 66, 105, 109, 109, 109, 109, 109, 109 })]
		public PB168x168()
		{
			refIdx1 = new int[2];
			refIdx2 = new int[2];
			mvdX1 = new int[2];
			mvdY1 = new int[2];
			mvdX2 = new int[2];
			mvdY2 = new int[2];
		}

		[LineNumberTable(new byte[] { 159, 115, 130, 121, 153, 121, 121, 121, 121 })]
		public virtual void clean()
		{
			int[] array = refIdx1;
			int[] array2 = refIdx1;
			int num = 0;
			int num2 = 1;
			int[] array3 = array2;
			int num3 = num;
			array3[num2] = num;
			array[0] = num3;
			int[] array4 = refIdx2;
			int[] array5 = refIdx2;
			num = 0;
			num2 = 1;
			array3 = array5;
			int num4 = num;
			array3[num2] = num;
			array4[0] = num4;
			int[] array6 = mvdX1;
			int[] array7 = mvdX1;
			num = 0;
			num2 = 1;
			array3 = array7;
			int num5 = num;
			array3[num2] = num;
			array6[0] = num5;
			int[] array8 = mvdY1;
			int[] array9 = mvdY1;
			num = 0;
			num2 = 1;
			array3 = array9;
			int num6 = num;
			array3[num2] = num;
			array8[0] = num6;
			int[] array10 = mvdX2;
			int[] array11 = mvdX2;
			num = 0;
			num2 = 1;
			array3 = array11;
			int num7 = num;
			array3[num2] = num;
			array10[0] = num7;
			int[] array12 = mvdY2;
			int[] array13 = mvdY2;
			num = 0;
			num2 = 1;
			array3 = array13;
			int num8 = num;
			array3[num2] = num;
			array12[0] = num8;
		}
	}

	internal class PB16x16 : Object
	{
		public int[] refIdx;

		public int[] mvdX;

		public int[] mvdY;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 123, 162, 105, 109, 109, 109 })]
		public PB16x16()
		{
			refIdx = new int[2];
			mvdX = new int[2];
			mvdY = new int[2];
		}

		[LineNumberTable(new byte[] { 159, 121, 130, 121, 121, 121 })]
		public virtual void clean()
		{
			int[] array = refIdx;
			int[] array2 = refIdx;
			int num = 0;
			int num2 = 1;
			int[] array3 = array2;
			int num3 = num;
			array3[num2] = num;
			array[0] = num3;
			int[] array4 = mvdX;
			int[] array5 = mvdX;
			num = 0;
			num2 = 1;
			array3 = array5;
			int num4 = num;
			array3[num2] = num;
			array4[0] = num4;
			int[] array6 = mvdY;
			int[] array7 = mvdY;
			num = 0;
			num2 = 1;
			array3 = array7;
			int num5 = num;
			array3[num2] = num;
			array6[0] = num5;
		}
	}

	internal class PB8x8 : Object
	{
		public int[][] refIdx;

		public int[] subMbTypes;

		public int[][] mvdX1;

		public int[][] mvdY1;

		public int[][] mvdX2;

		public int[][] mvdY2;

		public int[][] mvdX3;

		public int[][] mvdY3;

		public int[][] mvdX4;

		public int[][] mvdY4;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 109, 66, 105, 127, 11, 109, 127, 11, 127,
			11, 127, 11, 127, 11, 127, 11, 127, 11, 127,
			11, 127, 11
		})]
		public PB8x8()
		{
			int[] array = new int[2];
			int num = (array[1] = 4);
			num = (array[0] = 2);
			refIdx = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			subMbTypes = new int[4];
			array = new int[2];
			num = (array[1] = 4);
			num = (array[0] = 2);
			mvdX1 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			array = new int[2];
			num = (array[1] = 4);
			num = (array[0] = 2);
			mvdY1 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			array = new int[2];
			num = (array[1] = 4);
			num = (array[0] = 2);
			mvdX2 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			array = new int[2];
			num = (array[1] = 4);
			num = (array[0] = 2);
			mvdY2 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			array = new int[2];
			num = (array[1] = 4);
			num = (array[0] = 2);
			mvdX3 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			array = new int[2];
			num = (array[1] = 4);
			num = (array[0] = 2);
			mvdY3 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			array = new int[2];
			num = (array[1] = 4);
			num = (array[0] = 2);
			mvdX4 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
			array = new int[2];
			num = (array[1] = 4);
			num = (array[0] = 2);
			mvdY4 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		}

		[LineNumberTable(new byte[]
		{
			159, 106, 130, 127, 32, 127, 32, 127, 32, 159,
			32, 127, 32, 127, 32, 127, 32, 159, 32, 127,
			32, 127, 32, 127, 32, 159, 32, 127, 32, 127,
			32, 127, 32, 159, 32, 127, 24, 127, 32, 127,
			32
		})]
		public virtual void clean()
		{
			int[] obj = mvdX1[0];
			int[] obj2 = mvdX1[0];
			int[] obj3 = mvdX1[0];
			int[] obj4 = mvdX1[0];
			int num = 0;
			int num2 = 3;
			int[] array = obj4;
			int num3 = num;
			array[num2] = num;
			num = num3;
			num2 = 2;
			array = obj3;
			int num4 = num;
			array[num2] = num;
			num = num4;
			num2 = 1;
			array = obj2;
			int num5 = num;
			array[num2] = num;
			obj[0] = num5;
			int[] obj5 = mvdX2[0];
			int[] obj6 = mvdX2[0];
			int[] obj7 = mvdX2[0];
			int[] obj8 = mvdX2[0];
			num = 0;
			num2 = 3;
			array = obj8;
			int num6 = num;
			array[num2] = num;
			num = num6;
			num2 = 2;
			array = obj7;
			int num7 = num;
			array[num2] = num;
			num = num7;
			num2 = 1;
			array = obj6;
			int num8 = num;
			array[num2] = num;
			obj5[0] = num8;
			int[] obj9 = mvdX3[0];
			int[] obj10 = mvdX3[0];
			int[] obj11 = mvdX3[0];
			int[] obj12 = mvdX3[0];
			num = 0;
			num2 = 3;
			array = obj12;
			int num9 = num;
			array[num2] = num;
			num = num9;
			num2 = 2;
			array = obj11;
			int num10 = num;
			array[num2] = num;
			num = num10;
			num2 = 1;
			array = obj10;
			int num11 = num;
			array[num2] = num;
			obj9[0] = num11;
			int[] obj13 = mvdX4[0];
			int[] obj14 = mvdX4[0];
			int[] obj15 = mvdX4[0];
			int[] obj16 = mvdX4[0];
			num = 0;
			num2 = 3;
			array = obj16;
			int num12 = num;
			array[num2] = num;
			num = num12;
			num2 = 2;
			array = obj15;
			int num13 = num;
			array[num2] = num;
			num = num13;
			num2 = 1;
			array = obj14;
			int num14 = num;
			array[num2] = num;
			obj13[0] = num14;
			int[] obj17 = mvdY1[0];
			int[] obj18 = mvdY1[0];
			int[] obj19 = mvdY1[0];
			int[] obj20 = mvdY1[0];
			num = 0;
			num2 = 3;
			array = obj20;
			int num15 = num;
			array[num2] = num;
			num = num15;
			num2 = 2;
			array = obj19;
			int num16 = num;
			array[num2] = num;
			num = num16;
			num2 = 1;
			array = obj18;
			int num17 = num;
			array[num2] = num;
			obj17[0] = num17;
			int[] obj21 = mvdY2[0];
			int[] obj22 = mvdY2[0];
			int[] obj23 = mvdY2[0];
			int[] obj24 = mvdY2[0];
			num = 0;
			num2 = 3;
			array = obj24;
			int num18 = num;
			array[num2] = num;
			num = num18;
			num2 = 2;
			array = obj23;
			int num19 = num;
			array[num2] = num;
			num = num19;
			num2 = 1;
			array = obj22;
			int num20 = num;
			array[num2] = num;
			obj21[0] = num20;
			int[] obj25 = mvdY3[0];
			int[] obj26 = mvdY3[0];
			int[] obj27 = mvdY3[0];
			int[] obj28 = mvdY3[0];
			num = 0;
			num2 = 3;
			array = obj28;
			int num21 = num;
			array[num2] = num;
			num = num21;
			num2 = 2;
			array = obj27;
			int num22 = num;
			array[num2] = num;
			num = num22;
			num2 = 1;
			array = obj26;
			int num23 = num;
			array[num2] = num;
			obj25[0] = num23;
			int[] obj29 = mvdY4[0];
			int[] obj30 = mvdY4[0];
			int[] obj31 = mvdY4[0];
			int[] obj32 = mvdY4[0];
			num = 0;
			num2 = 3;
			array = obj32;
			int num24 = num;
			array[num2] = num;
			num = num24;
			num2 = 2;
			array = obj31;
			int num25 = num;
			array[num2] = num;
			num = num25;
			num2 = 1;
			array = obj30;
			int num26 = num;
			array[num2] = num;
			obj29[0] = num26;
			int[] obj33 = mvdX1[1];
			int[] obj34 = mvdX1[1];
			int[] obj35 = mvdX1[1];
			int[] obj36 = mvdX1[1];
			num = 0;
			num2 = 3;
			array = obj36;
			int num27 = num;
			array[num2] = num;
			num = num27;
			num2 = 2;
			array = obj35;
			int num28 = num;
			array[num2] = num;
			num = num28;
			num2 = 1;
			array = obj34;
			int num29 = num;
			array[num2] = num;
			obj33[0] = num29;
			int[] obj37 = mvdX2[1];
			int[] obj38 = mvdX2[1];
			int[] obj39 = mvdX2[1];
			int[] obj40 = mvdX2[1];
			num = 0;
			num2 = 3;
			array = obj40;
			int num30 = num;
			array[num2] = num;
			num = num30;
			num2 = 2;
			array = obj39;
			int num31 = num;
			array[num2] = num;
			num = num31;
			num2 = 1;
			array = obj38;
			int num32 = num;
			array[num2] = num;
			obj37[0] = num32;
			int[] obj41 = mvdX3[1];
			int[] obj42 = mvdX3[1];
			int[] obj43 = mvdX3[1];
			int[] obj44 = mvdX3[1];
			num = 0;
			num2 = 3;
			array = obj44;
			int num33 = num;
			array[num2] = num;
			num = num33;
			num2 = 2;
			array = obj43;
			int num34 = num;
			array[num2] = num;
			num = num34;
			num2 = 1;
			array = obj42;
			int num35 = num;
			array[num2] = num;
			obj41[0] = num35;
			int[] obj45 = mvdX4[1];
			int[] obj46 = mvdX4[1];
			int[] obj47 = mvdX4[1];
			int[] obj48 = mvdX4[1];
			num = 0;
			num2 = 3;
			array = obj48;
			int num36 = num;
			array[num2] = num;
			num = num36;
			num2 = 2;
			array = obj47;
			int num37 = num;
			array[num2] = num;
			num = num37;
			num2 = 1;
			array = obj46;
			int num38 = num;
			array[num2] = num;
			obj45[0] = num38;
			int[] obj49 = mvdY1[1];
			int[] obj50 = mvdY1[1];
			int[] obj51 = mvdY1[1];
			int[] obj52 = mvdY1[1];
			num = 0;
			num2 = 3;
			array = obj52;
			int num39 = num;
			array[num2] = num;
			num = num39;
			num2 = 2;
			array = obj51;
			int num40 = num;
			array[num2] = num;
			num = num40;
			num2 = 1;
			array = obj50;
			int num41 = num;
			array[num2] = num;
			obj49[0] = num41;
			int[] obj53 = mvdY2[1];
			int[] obj54 = mvdY2[1];
			int[] obj55 = mvdY2[1];
			int[] obj56 = mvdY2[1];
			num = 0;
			num2 = 3;
			array = obj56;
			int num42 = num;
			array[num2] = num;
			num = num42;
			num2 = 2;
			array = obj55;
			int num43 = num;
			array[num2] = num;
			num = num43;
			num2 = 1;
			array = obj54;
			int num44 = num;
			array[num2] = num;
			obj53[0] = num44;
			int[] obj57 = mvdY3[1];
			int[] obj58 = mvdY3[1];
			int[] obj59 = mvdY3[1];
			int[] obj60 = mvdY3[1];
			num = 0;
			num2 = 3;
			array = obj60;
			int num45 = num;
			array[num2] = num;
			num = num45;
			num2 = 2;
			array = obj59;
			int num46 = num;
			array[num2] = num;
			num = num46;
			num2 = 1;
			array = obj58;
			int num47 = num;
			array[num2] = num;
			obj57[0] = num47;
			int[] obj61 = mvdY4[1];
			int[] obj62 = mvdY4[1];
			int[] obj63 = mvdY4[1];
			int[] obj64 = mvdY4[1];
			num = 0;
			num2 = 3;
			array = obj64;
			int num48 = num;
			array[num2] = num;
			num = num48;
			num2 = 2;
			array = obj63;
			int num49 = num;
			array[num2] = num;
			num = num49;
			num2 = 1;
			array = obj62;
			int num50 = num;
			array[num2] = num;
			obj61[0] = num50;
			int[] array2 = subMbTypes;
			int[] array3 = subMbTypes;
			int[] array4 = subMbTypes;
			int[] array5 = subMbTypes;
			num = 0;
			num2 = 3;
			array = array5;
			int num51 = num;
			array[num2] = num;
			num = num51;
			num2 = 2;
			array = array4;
			int num52 = num;
			array[num2] = num;
			num = num52;
			num2 = 1;
			array = array3;
			int num53 = num;
			array[num2] = num;
			array2[0] = num53;
			int[] obj65 = refIdx[0];
			int[] obj66 = refIdx[0];
			int[] obj67 = refIdx[0];
			int[] obj68 = refIdx[0];
			num = 0;
			num2 = 3;
			array = obj68;
			int num54 = num;
			array[num2] = num;
			num = num54;
			num2 = 2;
			array = obj67;
			int num55 = num;
			array[num2] = num;
			num = num55;
			num2 = 1;
			array = obj66;
			int num56 = num;
			array[num2] = num;
			obj65[0] = num56;
			int[] obj69 = refIdx[1];
			int[] obj70 = refIdx[1];
			int[] obj71 = refIdx[1];
			int[] obj72 = refIdx[1];
			num = 0;
			num2 = 3;
			array = obj72;
			int num57 = num;
			array[num2] = num;
			num = num57;
			num2 = 2;
			array = obj71;
			int num58 = num;
			array[num2] = num;
			num = num58;
			num2 = 1;
			array = obj70;
			int num59 = num;
			array[num2] = num;
			obj69[0] = num59;
		}
	}

	public int chromaPredictionMode;

	public int mbQPDelta;

	public int[] dc;

	public int[][][] ac;

	public bool transform8x8Used;

	public int[] lumaModes;

	public int[] dc1;

	public int[] dc2;

	public int _cbp;

	public int mbType;

	public MBType curMbType;

	internal PB16x16 ___003C_003Epb16x16;

	internal PB168x168 ___003C_003Epb168x168;

	internal PB8x8 ___003C_003Epb8x8;

	internal IPCM ___003C_003Eipcm;

	public int mbIdx;

	public bool fieldDecoding;

	public MBType prevMbType;

	public int luma16x16Mode;

	public H264Utils.MvList x;

	public H264Const.PartPred[] partPreds;

	public bool skipped;

	public int[] nCoeff;

	[Modifiers(Modifiers.Public)]
	public object pb16x16
	{
		[HideFromJava]
		get
		{
			return ___003C_003Epb16x16;
		}
		[HideFromJava]
		set
		{
			___003C_003Epb16x16 = (PB16x16)value;
		}
	}

	[Modifiers(Modifiers.Public)]
	public object pb168x168
	{
		[HideFromJava]
		get
		{
			return ___003C_003Epb168x168;
		}
		[HideFromJava]
		set
		{
			___003C_003Epb168x168 = (PB168x168)value;
		}
	}

	[Modifiers(Modifiers.Public)]
	public object pb8x8
	{
		[HideFromJava]
		get
		{
			return ___003C_003Epb8x8;
		}
		[HideFromJava]
		set
		{
			___003C_003Epb8x8 = (PB8x8)value;
		}
	}

	[Modifiers(Modifiers.Public)]
	public object ipcm
	{
		[HideFromJava]
		get
		{
			return ___003C_003Eipcm;
		}
		[HideFromJava]
		set
		{
			___003C_003Eipcm = (IPCM)value;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 130, 105, 108, 108, 140, 110, 127, 100,
		110, 110, 127, 7, 127, 7, 109, 110, 109
	})]
	public MBlock(ColorSpace chromaFormat)
	{
		___003C_003Epb8x8 = new PB8x8();
		___003C_003Epb16x16 = new PB16x16();
		___003C_003Epb168x168 = new PB168x168();
		dc = new int[16];
		int[][][] array = new int[3][][];
		int[] array2 = new int[2];
		int num = (array2[1] = 64);
		num = (array2[0] = 16);
		array[0] = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array2);
		array2 = new int[2];
		num = (array2[1] = 16);
		num = (array2[0] = 4);
		array[1] = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array2);
		array2 = new int[2];
		num = (array2[1] = 16);
		num = (array2[0] = 4);
		array[2] = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array2);
		ac = array;
		lumaModes = new int[16];
		nCoeff = new int[16];
		dc1 = new int[16 >> chromaFormat.compWidth[1] >> chromaFormat.compHeight[1]];
		dc2 = new int[16 >> chromaFormat.compWidth[2] >> chromaFormat.compHeight[2]];
		___003C_003Eipcm = new IPCM(chromaFormat);
		x = new H264Utils.MvList(16);
		partPreds = new H264Const.PartPred[4];
	}

	[LineNumberTable(63)]
	public virtual int cbpLuma()
	{
		return _cbp & 0xF;
	}

	[LineNumberTable(67)]
	public virtual int cbpChroma()
	{
		return _cbp >> 4;
	}

	[LineNumberTable(new byte[] { 159, 125, 162, 111 })]
	public virtual void cbp(int cbpLuma, int cbpChroma)
	{
		_cbp = (cbpLuma & 0xF) | (cbpChroma << 4);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 94, 66, 104, 104, 109, 109, 106, 104, 42,
		7, 231, 70, 104, 109, 109, 109, 109, 104, 104,
		108, 108, 108, 110, 108, 104, 104, 104, 104, 104,
		104, 108, 127, 30
	})]
	public virtual void clear()
	{
		chromaPredictionMode = 0;
		mbQPDelta = 0;
		Arrays.fill(dc, 0);
		for (int i = 0; i < (nint)ac.LongLength; i++)
		{
			int[][] aci = ac[i];
			for (int j = 0; j < (nint)aci.LongLength; j++)
			{
				Arrays.fill(aci[j], 0);
			}
		}
		transform8x8Used = false;
		Arrays.fill(lumaModes, 0);
		Arrays.fill(dc1, 0);
		Arrays.fill(dc2, 0);
		Arrays.fill(nCoeff, 0);
		_cbp = 0;
		mbType = 0;
		___003C_003Epb16x16.clean();
		___003C_003Epb168x168.clean();
		___003C_003Epb8x8.clean();
		if (curMbType == MBType.___003C_003EI_PCM)
		{
			___003C_003Eipcm.clean();
		}
		mbIdx = 0;
		fieldDecoding = false;
		prevMbType = null;
		luma16x16Mode = 0;
		skipped = false;
		curMbType = null;
		x.clear();
		H264Const.PartPred[] array = partPreds;
		H264Const.PartPred[] array2 = partPreds;
		H264Const.PartPred[] array3 = partPreds;
		H264Const.PartPred[] array4 = partPreds;
		
		int num = 3;
		H264Const.PartPred[] array5 = array4;
		
		array5[num] = null;
		num = 2;
		array5 = array3;
		
		array5[num] = null;
		num = 1;
		array5 = array2;
		array5[num] = null;
		array[0] = null;
	}
}
