using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using org.jcodec.api;
using org.jcodec.common.model;

namespace org.jcodec.codecs.vpx;

public class VPXMacroblock : Object
{
	public class Subblock : Object
	{
		public int[] val;

		public int[] _predict;

		public int[] residue;

		private int col;

		private int row;

		private VP8Util.PLANE plane;

		public int mode;

		public bool someValuePresent;

		private int[] tokens;

		private VPXMacroblock self;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 239, 130, 109, 115, 125, 115, 125, 115, 157,
			136, 125, 105, 106, 149
		})]
		public virtual Subblock getAbove(VP8Util.PLANE plane, VPXMacroblock[][] mbs)
		{
			if (row > 0)
			{
				if (VP8Util.PLANE.___003C_003EY1.equals(this.plane))
				{
					return self.___003C_003EySubblocks[row - 1][col];
				}
				if (VP8Util.PLANE.___003C_003EU.equals(this.plane))
				{
					return self.___003C_003EuSubblocks[row - 1][col];
				}
				if (VP8Util.PLANE.___003C_003EV.equals(this.plane))
				{
					return self.___003C_003EvSubblocks[row - 1][col];
				}
			}
			int x = col;
			VPXMacroblock mb2 = mbs[self.___003C_003ERrow - 1][self.___003C_003Ecolumn];
			if (plane == VP8Util.PLANE.___003C_003EY2)
			{
				while (mb2.lumaMode == 4)
				{
					mb2 = mbs[mb2.___003C_003ERrow - 1][mb2.___003C_003Ecolumn];
				}
			}
			Subblock bottomSubblock = mb2.getBottomSubblock(x, plane);
			
			return bottomSubblock;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 234, 130, 109, 115, 125, 115, 125, 115, 157,
			104, 157, 105, 106, 149
		})]
		public virtual Subblock getLeft(VP8Util.PLANE p, VPXMacroblock[][] mbs)
		{
			if (col > 0)
			{
				if (VP8Util.PLANE.___003C_003EY1.equals(plane))
				{
					return self.___003C_003EySubblocks[row][col - 1];
				}
				if (VP8Util.PLANE.___003C_003EU.equals(plane))
				{
					return self.___003C_003EuSubblocks[row][col - 1];
				}
				if (VP8Util.PLANE.___003C_003EV.equals(plane))
				{
					return self.___003C_003EvSubblocks[row][col - 1];
				}
			}
			int y = row;
			VPXMacroblock mb2 = mbs[self.___003C_003ERrow][self.___003C_003Ecolumn - 1];
			if (p == VP8Util.PLANE.___003C_003EY2)
			{
				while (mb2.lumaMode == 4)
				{
					mb2 = mbs[mb2.___003C_003ERrow][mb2.___003C_003Ecolumn - 1];
				}
			}
			Subblock rightSubBlock = mb2.getRightSubBlock(y, p);
			
			return rightSubBlock;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 229, 162, 115, 209, 146, 125, 115, 136, 152,
			127, 0, 136, 157, 127, 1, 112, 110, 105, 99,
			105, 159, 29, 169, 131, 114, 172, 100, 135, 105,
			105, 105, 105, 105
		})]
		private int[] getAboveRightLowestRow(VPXMacroblock[][] mbs)
		{
			if (!VP8Util.PLANE.___003C_003EY1.equals(plane))
			{
				
				throw new NotImplementedException("Decoder.getAboveRight: not implemented for Y2 and chroma planes");
			}
			int[] aboveRightDistValues;
			if (row == 0 && col < 3)
			{
				VPXMacroblock mb2 = mbs[self.___003C_003ERrow - 1][self.___003C_003Ecolumn];
				Subblock aboveRight2 = mb2.___003C_003EySubblocks[3][col + 1];
				aboveRightDistValues = aboveRight2.val;
			}
			else if (row > 0 && col < 3)
			{
				Subblock aboveRight = self.___003C_003EySubblocks[row - 1][col + 1];
				aboveRightDistValues = aboveRight.val;
			}
			else
			{
				if (row != 0 || col != 3)
				{
					Subblock sb2 = self.___003C_003EySubblocks[0][3];
					int[] aboveRightLowestRow = sb2.getAboveRightLowestRow(mbs);
					
					return aboveRightLowestRow;
				}
				VPXMacroblock aboveRightMb = mbs[self.___003C_003ERrow - 1][self.___003C_003Ecolumn + 1];
				if (aboveRightMb.___003C_003Ecolumn < (nint)mbs[0].LongLength - 1)
				{
					Subblock aboveRightSb = aboveRightMb.___003C_003EySubblocks[3][0];
					aboveRightDistValues = aboveRightSb.val;
				}
				else
				{
					aboveRightDistValues = new int[16];
					int fillVal = ((aboveRightMb.___003C_003ERrow != 0) ? mbs[self.___003C_003ERrow - 1][self.___003C_003Ecolumn].___003C_003EySubblocks[3][3].val[15] : 127);
					Arrays.fill(aboveRightDistValues, fillVal);
				}
			}
			if (aboveRightDistValues == null)
			{
				aboveRightDistValues = VP8Util.___003C_003EPRED_BLOCK_127;
			}
			return new int[4]
			{
				aboveRightDistValues[12],
				aboveRightDistValues[13],
				aboveRightDistValues[14],
				aboveRightDistValues[15]
			};
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 206, 66, 131, 101, 144, 101, 144, 101, 145,
			101, 145, 102, 145, 102, 145, 105, 106, 164
		})]
		private int decodeToken(VPXBooleanDecoder decoder, int initialValue)
		{
			int token = initialValue;
			if (initialValue == 5)
			{
				token = 5 + DCTextra(decoder, VP8Util.SubblockConstants.___003C_003EPcat1);
			}
			if (initialValue == 6)
			{
				token = 7 + DCTextra(decoder, VP8Util.SubblockConstants.___003C_003EPcat2);
			}
			if (initialValue == 7)
			{
				token = 11 + DCTextra(decoder, VP8Util.SubblockConstants.___003C_003EPcat3);
			}
			if (initialValue == 8)
			{
				token = 19 + DCTextra(decoder, VP8Util.SubblockConstants.___003C_003EPcat4);
			}
			if (initialValue == 9)
			{
				token = 35 + DCTextra(decoder, VP8Util.SubblockConstants.___003C_003EPcat5);
			}
			if (initialValue == 10)
			{
				token = 67 + DCTextra(decoder, VP8Util.SubblockConstants.___003C_003EPcat6);
			}
			if (initialValue != 0 && initialValue != 11 && decoder.readBitEq() > 0)
			{
				token = -token;
			}
			return token;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 199, 98, 99, 131, 111, 101, 103 })]
		private int DCTextra(VPXBooleanDecoder decoder, int[] p)
		{
			int v = 0;
			int offset = 0;
			do
			{
				v += v + decoder.readBit(p[offset]);
				offset++;
			}
			while (p[offset] > 0);
			return v;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 9, 162, 105, 104, 104, 104, 105, 110 })]
		public Subblock(VPXMacroblock self, int row, int col, VP8Util.PLANE plane)
		{
			this.self = self;
			this.row = row;
			this.col = col;
			this.plane = plane;
			tokens = new int[16];
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 6, 66, 111, 143, 104, 136, 120, 105, 105,
			105, 105, 126, 104, 104, 105, 105, 176, 145, 103,
			137, 135, 159, 7, 138, 159, 26, 110, 166, 112,
			166, 113, 166, 111, 166, 111, 166, 112, 166, 112,
			163, 111, 163, 112, 163, 109, 163, 191, 12
		})]
		public virtual void predict(VPXMacroblock[][] mbs)
		{
			Subblock aboveSb = getAbove(plane, mbs);
			Subblock leftSb = getLeft(plane, mbs);
			int[] above = new int[4];
			int[] left = new int[4];
			int[] aboveValues = ((aboveSb.val == null) ? VP8Util.___003C_003EPRED_BLOCK_127 : aboveSb.val);
			above[0] = aboveValues[12];
			above[1] = aboveValues[13];
			above[2] = aboveValues[14];
			above[3] = aboveValues[15];
			int[] leftValues = ((leftSb.val == null) ? VP8Util.pickDefaultPrediction(mode) : leftSb.val);
			left[0] = leftValues[3];
			left[1] = leftValues[7];
			left[2] = leftValues[11];
			left[3] = leftValues[15];
			Subblock aboveLeftSb = aboveSb.getLeft(plane, mbs);
			int aboveLeft = ((leftSb.val == null && aboveSb.val == null) ? 127 : ((aboveSb.val != null) ? ((aboveLeftSb.val == null) ? VP8Util.pickDefaultPrediction(mode)[15] : aboveLeftSb.val[15]) : 127));
			int[] ar = getAboveRightLowestRow(mbs);
			switch (mode)
			{
			case 0:
				_predict = VP8Util.predictDC(above, left);
				break;
			case 1:
				_predict = VP8Util.predictTM(above, left, aboveLeft);
				break;
			case 2:
				_predict = VP8Util.predictVE(above, aboveLeft, ar);
				break;
			case 3:
				_predict = VP8Util.predictHE(left, aboveLeft);
				break;
			case 4:
				_predict = VP8Util.predictLD(above, ar);
				break;
			case 5:
				_predict = VP8Util.predictRD(above, left, aboveLeft);
				break;
			case 6:
				_predict = VP8Util.predictVR(above, left, aboveLeft);
				break;
			case 7:
				_predict = VP8Util.predictVL(above, ar);
				break;
			case 8:
				_predict = VP8Util.predictHD(above, left, aboveLeft);
				break;
			case 9:
				_predict = VP8Util.predictHU(left);
				break;
			default:
			{
				string msg = new StringBuilder().append("TODO: unknowwn mode: ").append(mode).toString();
				
				throw new NotSupportedException(msg);
			}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 242, 66, 120, 137, 103, 103, 124, 10, 39,
			231, 71, 104
		})]
		public virtual void reconstruct()
		{
			int[] p = ((val == null) ? _predict : val);
			int[] dest = new int[16];
			for (int aRow = 0; aRow < 4; aRow++)
			{
				for (int aCol = 0; aCol < 4; aCol++)
				{
					int a = (dest[aRow * 4 + aCol] = VP8Util.QuantizationParams.clip255(residue[aRow * 4 + aCol] + p[aRow * 4 + aCol]));
				}
			}
			val = dest;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 217, 161, 68, 99, 100, 99, 99, 99, 132,
			132, 104, 148, 115, 101, 146, 177, 108, 99, 100,
			107, 101, 107, 101, 101, 132, 103, 147, 101, 134,
			106, 108, 8, 169
		})]
		public virtual void decodeSubBlock(VPXBooleanDecoder decoder, int[][][][] allProbs, int ilc, int type, bool withY2)
		{
			int startAt = 0;
			if (withY2)
			{
				startAt = 1;
			}
			int lc = ilc;
			int count = 0;
			int v = 1;
			int skip = 0;
			someValuePresent = false;
			while (v != 11 && count + startAt < 16)
			{
				int[] probs = allProbs[type][VP8Util.SubblockConstants.___003C_003Evp8CoefBands[count + startAt]][lc];
				v = ((skip != 0) ? decoder.readTreeSkip(VP8Util.SubblockConstants.___003C_003Evp8CoefTree, probs, 1) : decoder.readTree(VP8Util.SubblockConstants.___003C_003Evp8CoefTree, probs));
				int dv = decodeToken(decoder, v);
				lc = 0;
				skip = 0;
				switch (dv)
				{
				case -1:
				case 1:
					lc = 1;
					break;
				default:
					lc = 2;
					break;
				case 0:
					if (dv == 0)
					{
						skip = 1;
					}
					break;
				}
				if (v != 11)
				{
					tokens[VP8Util.SubblockConstants.___003C_003Evp8defaultZigZag1d[count + startAt]] = dv;
				}
				count++;
			}
			for (int x = 0; x < 16; x++)
			{
				if (tokens[x] != 0)
				{
					someValuePresent = true;
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 197, 162, 137, 110, 104, 46, 167, 100, 138,
			141
		})]
		public virtual void dequantSubblock(int dc, int ac, Integer Dc)
		{
			int[] adjustedValues = new int[16]
			{
				tokens[0] * dc,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			for (int i = 1; i < 16; i++)
			{
				adjustedValues[i] = tokens[i] * ac;
			}
			if (Dc != null)
			{
				adjustedValues[0] = Dc.intValue();
			}
			residue = VP8DCT.decodeDCT(adjustedValues);
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(522)]
		internal static int[] access_0024000(Subblock x0)
		{
			return x0.tokens;
		}
	}

	public int filterLevel;

	public int chromaMode;

	public int skipCoeff;

	internal Subblock[][] ___003C_003EySubblocks;

	internal Subblock ___003C_003Ey2;

	internal Subblock[][] ___003C_003EuSubblocks;

	internal Subblock[][] ___003C_003EvSubblocks;

	internal int ___003C_003ERrow;

	internal int ___003C_003Ecolumn;

	public int lumaMode;

	internal bool skipFilter;

	public int segment;

	public bool debug;

	public VP8Util.QuantizationParams quants;

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public Subblock[][] ySubblocks
	{
		[HideFromJava]
		get
		{
			return ___003C_003EySubblocks;
		}
		[HideFromJava]
		private set
		{
			___003C_003EySubblocks = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public Subblock y2
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ey2;
		}
		[HideFromJava]
		private set
		{
			___003C_003Ey2 = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public Subblock[][] uSubblocks
	{
		[HideFromJava]
		get
		{
			return ___003C_003EuSubblocks;
		}
		[HideFromJava]
		private set
		{
			___003C_003EuSubblocks = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public Subblock[][] vSubblocks
	{
		[HideFromJava]
		get
		{
			return ___003C_003EvSubblocks;
		}
		[HideFromJava]
		private set
		{
			___003C_003EvSubblocks = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public int Rrow
	{
		[HideFromJava]
		get
		{
			return ___003C_003ERrow;
		}
		[HideFromJava]
		private set
		{
			___003C_003ERrow = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public int column
	{
		[HideFromJava]
		get
		{
			return ___003C_003Ecolumn;
		}
		[HideFromJava]
		private set
		{
			___003C_003Ecolumn = value;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 130, 233, 60, 104, 200, 127, 11, 116,
		127, 11, 159, 11, 104, 104, 103, 105, 58, 41,
		167, 105, 105, 124, 28, 41, 233, 69
	})]
	public VPXMacroblock(int y, int x)
	{
		segment = 0;
		debug = true;
		int[] array = new int[2];
		int num = (array[1] = 4);
		num = (array[0] = 4);
		___003C_003EySubblocks = (Subblock[][])ByteCodeHelper.multianewarray(typeof(Subblock[][]).TypeHandle, array);
		___003C_003Ey2 = new Subblock(this, 0, 0, VP8Util.PLANE.___003C_003EY2);
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 2);
		___003C_003EuSubblocks = (Subblock[][])ByteCodeHelper.multianewarray(typeof(Subblock[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 2);
		___003C_003EvSubblocks = (Subblock[][])ByteCodeHelper.multianewarray(typeof(Subblock[][]).TypeHandle, array);
		___003C_003ERrow = y;
		___003C_003Ecolumn = x;
		for (int row2 = 0; row2 < 4; row2++)
		{
			for (int col2 = 0; col2 < 4; col2++)
			{
				___003C_003EySubblocks[row2][col2] = new Subblock(this, row2, col2, VP8Util.PLANE.___003C_003EY1);
			}
		}
		for (int row = 0; row < 2; row++)
		{
			for (int col = 0; col < 2; col++)
			{
				___003C_003EuSubblocks[row][col] = new Subblock(this, row, col, VP8Util.PLANE.___003C_003EU);
				___003C_003EvSubblocks[row][col] = new Subblock(this, row, col, VP8Util.PLANE.___003C_003EV);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 25, 98, 106, 117, 106, 141, 109 })]
	public virtual void decodeMacroBlock(VPXMacroblock[][] mbs, VPXBooleanDecoder tockenDecoder, int[][][][] coefProbs)
	{
		if (skipCoeff > 0)
		{
			skipFilter = lumaMode != 4;
		}
		else if (lumaMode != 4)
		{
			decodeMacroBlockTokens(withY2: true, mbs, tockenDecoder, coefProbs);
		}
		else
		{
			decodeMacroBlockTokens(withY2: false, mbs, tockenDecoder, coefProbs);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 98, 104, 109, 104, 136, 105, 147, 106,
		53, 233, 70, 114, 105, 105, 63, 23, 41, 201,
		104, 104, 108, 105, 127, 0, 31, 0, 41, 236,
		70, 135, 102, 105, 105, 111, 117, 105, 232, 60,
		41, 233, 72, 104, 105, 105, 111, 117, 232, 61,
		41, 233, 71, 105, 105, 111, 117, 232, 61, 41,
		233, 73
	})]
	public virtual void dequantMacroBlock(VPXMacroblock[][] mbs)
	{
		VP8Util.QuantizationParams p = quants;
		if (lumaMode != 4)
		{
			int acQValue = p.y2AC;
			int dcQValue = p.y2DC;
			int[] input = new int[16]
			{
				Subblock.access_0024000(___003C_003Ey2)[0] * dcQValue,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			for (int x = 1; x < 16; x++)
			{
				input[x] = Subblock.access_0024000(___003C_003Ey2)[x] * acQValue;
			}
			___003C_003Ey2.residue = VP8DCT.decodeWHT(input);
			for (int row5 = 0; row5 < 4; row5++)
			{
				for (int col5 = 0; col5 < 4; col5++)
				{
					___003C_003EySubblocks[row5][col5].dequantSubblock(p.yDC, p.yAC, Integer.valueOf(___003C_003Ey2.residue[row5 * 4 + col5]));
				}
			}
			predictY(mbs);
			predictUV(mbs);
			for (int row4 = 0; row4 < 2; row4++)
			{
				for (int col4 = 0; col4 < 2; col4++)
				{
					___003C_003EuSubblocks[row4][col4].dequantSubblock(p.chromaDC, p.chromaAC, null);
					___003C_003EvSubblocks[row4][col4].dequantSubblock(p.chromaDC, p.chromaAC, null);
				}
			}
			reconstruct();
			return;
		}
		for (int row3 = 0; row3 < 4; row3++)
		{
			for (int col3 = 0; col3 < 4; col3++)
			{
				Subblock sb3 = ___003C_003EySubblocks[row3][col3];
				sb3.dequantSubblock(p.yDC, p.yAC, null);
				sb3.predict(mbs);
				sb3.reconstruct();
			}
		}
		predictUV(mbs);
		for (int row2 = 0; row2 < 2; row2++)
		{
			for (int col2 = 0; col2 < 2; col2++)
			{
				Subblock sb2 = ___003C_003EuSubblocks[row2][col2];
				sb2.dequantSubblock(p.chromaDC, p.chromaAC, null);
				sb2.reconstruct();
			}
		}
		for (int row = 0; row < 2; row++)
		{
			for (int col = 0; col < 2; col++)
			{
				Subblock sb = ___003C_003EvSubblocks[row][col];
				sb.dequantSubblock(p.chromaDC, p.chromaAC, null);
				sb.reconstruct();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 193, 130, 105, 105, 105, 105, 106, 108, 108,
		108, 108, 110, 110, 118, 131, 124, 243, 57, 44,
		44, 44, 236, 77, 108, 108, 108, 108, 110, 110,
		123, 134, 124, 124, 116, 244, 55, 44, 44, 44,
		236, 78
	})]
	public virtual void put(int mbRow, int mbCol, Picture p)
	{
		byte[] luma = p.getPlaneData(0);
		byte[] cb = p.getPlaneData(1);
		byte[] cr = p.getPlaneData(2);
		int strideLuma = p.getPlaneWidth(0);
		int strideChroma = p.getPlaneWidth(1);
		for (int lumaRow = 0; lumaRow < 4; lumaRow++)
		{
			for (int lumaCol = 0; lumaCol < 4; lumaCol++)
			{
				for (int lumaPRow = 0; lumaPRow < 4; lumaPRow++)
				{
					for (int lumaPCol = 0; lumaPCol < 4; lumaPCol++)
					{
						int y2 = (mbRow << 4) + (lumaRow << 2) + lumaPRow;
						int x2 = (mbCol << 4) + (lumaCol << 2) + lumaPCol;
						if (x2 < strideLuma)
						{
							nint num = (nint)luma.LongLength;
							if (y2 < ((strideLuma != -1) ? (num / strideLuma) : (-num)))
							{
								int yy = ___003C_003EySubblocks[lumaRow][lumaCol].val[lumaPRow * 4 + lumaPCol];
								luma[strideLuma * y2 + x2] = (byte)(sbyte)(yy - 128);
							}
						}
					}
				}
			}
		}
		for (int chromaRow = 0; chromaRow < 2; chromaRow++)
		{
			for (int chromaCol = 0; chromaCol < 2; chromaCol++)
			{
				for (int chromaPRow = 0; chromaPRow < 4; chromaPRow++)
				{
					for (int chromaPCol = 0; chromaPCol < 4; chromaPCol++)
					{
						int y = (mbRow << 3) + (chromaRow << 2) + chromaPRow;
						int x = (mbCol << 3) + (chromaCol << 2) + chromaPCol;
						if (x < strideChroma)
						{
							nint num2 = (nint)cb.LongLength;
							if (y < ((strideChroma != -1) ? (num2 / strideChroma) : (-num2)))
							{
								int u = ___003C_003EuSubblocks[chromaRow][chromaCol].val[chromaPRow * 4 + chromaPCol];
								int v = ___003C_003EvSubblocks[chromaRow][chromaCol].val[chromaPRow * 4 + chromaPCol];
								cb[strideChroma * y + x] = (byte)(sbyte)(u - 128);
								cr[strideChroma * y + x] = (byte)(sbyte)(v - 128);
							}
						}
					}
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 32, 162, 105, 108, 105, 108, 105, 108, 105,
		136
	})]
	public virtual Subblock getBottomSubblock(int x, VP8Util.PLANE plane)
	{
		if (plane == VP8Util.PLANE.___003C_003EY1)
		{
			return ___003C_003EySubblocks[3][x];
		}
		if (plane == VP8Util.PLANE.___003C_003EU)
		{
			return ___003C_003EuSubblocks[1][x];
		}
		if (plane == VP8Util.PLANE.___003C_003EV)
		{
			return ___003C_003EvSubblocks[1][x];
		}
		if (plane == VP8Util.PLANE.___003C_003EY2)
		{
			return ___003C_003Ey2;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 28, 66, 105, 108, 105, 108, 105, 108, 105,
		136
	})]
	public virtual Subblock getRightSubBlock(int y, VP8Util.PLANE plane)
	{
		if (plane == VP8Util.PLANE.___003C_003EY1)
		{
			return ___003C_003EySubblocks[y][3];
		}
		if (plane == VP8Util.PLANE.___003C_003EU)
		{
			return ___003C_003EuSubblocks[y][1];
		}
		if (plane == VP8Util.PLANE.___003C_003EV)
		{
			return ___003C_003EvSubblocks[y][1];
		}
		if (plane == VP8Util.PLANE.___003C_003EY2)
		{
			return ___003C_003Ey2;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 66, 162, 115, 147, 159, 2, 105, 166, 104,
		166, 104, 166, 117, 108, 108, 139, 131, 127, 11,
		137
	})]
	private void predictY(VPXMacroblock[][] mbs)
	{
		VPXMacroblock aboveMb = mbs[___003C_003ERrow - 1][___003C_003Ecolumn];
		VPXMacroblock leftMb = mbs[___003C_003ERrow][___003C_003Ecolumn - 1];
		switch (lumaMode)
		{
		case 0:
			predictLumaDC(aboveMb, leftMb);
			break;
		case 1:
			predictLumaV(aboveMb);
			break;
		case 2:
			predictLumaH(leftMb);
			break;
		case 3:
		{
			VPXMacroblock upperLeft = mbs[___003C_003ERrow - 1][___003C_003Ecolumn - 1];
			Subblock ALSb = upperLeft.___003C_003EySubblocks[3][3];
			int aboveLeft = ALSb.val[15];
			predictLumaTM(aboveMb, leftMb, aboveLeft);
			break;
		}
		default:
			java.lang.System.err.println(new StringBuilder().append("TODO predict_mb_y: ").append(lumaMode).toString());
			java.lang.System.exit(0);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 66, 115, 147, 223, 2, 99, 99, 100,
		100, 104, 104, 106, 99, 106, 99, 106, 103, 108,
		110, 110, 105, 115, 19, 233, 61, 236, 74, 103,
		108, 110, 110, 105, 116, 20, 233, 61, 236, 74,
		100, 100, 103, 100, 135, 117, 181, 106, 105, 105,
		45, 41, 201, 106, 105, 105, 45, 41, 201, 105,
		105, 111, 111, 106, 234, 60, 41, 233, 73, 198,
		105, 105, 105, 113, 17, 233, 69, 108, 108, 111,
		111, 106, 106, 140, 108, 127, 12, 31, 12, 12,
		236, 70, 106, 234, 52, 44, 236, 80, 230, 69,
		105, 105, 105, 113, 17, 233, 69, 108, 108, 111,
		111, 106, 106, 108, 108, 127, 16, 31, 16, 44,
		236, 69, 106, 234, 53, 44, 236, 79, 198, 118,
		110, 109, 110, 141, 105, 105, 105, 105, 105, 113,
		113, 113, 241, 60, 233, 71, 108, 108, 108, 116,
		121, 116, 121, 140, 127, 9, 106, 156, 127, 9,
		106, 252, 56, 236, 59, 44, 44, 236, 87, 131,
		127, 11, 137
	})]
	public virtual void predictUV(VPXMacroblock[][] mbs)
	{
		VPXMacroblock aboveMb = mbs[___003C_003ERrow - 1][___003C_003Ecolumn];
		VPXMacroblock leftMb = mbs[___003C_003ERrow][___003C_003Ecolumn - 1];
		switch (chromaMode)
		{
		case 0:
		{
			int up_available = 0;
			int left_available = 0;
			int uAvg = 0;
			int vAvg = 0;
			int expected_udc = 128;
			int expected_vdc = 128;
			if (___003C_003Ecolumn > 1)
			{
				left_available = 1;
			}
			if (___003C_003ERrow > 1)
			{
				up_available = 1;
			}
			if (up_available != 0 || left_available != 0)
			{
				if (up_available != 0)
				{
					for (int l = 0; l < 2; l++)
					{
						Subblock usb3 = aboveMb.___003C_003EuSubblocks[1][l];
						Subblock vsb3 = aboveMb.___003C_003EvSubblocks[1][l];
						for (int j = 0; j < 4; j++)
						{
							uAvg += usb3.val[12 + j];
							vAvg += vsb3.val[12 + j];
						}
					}
				}
				if (left_available != 0)
				{
					for (int k = 0; k < 2; k++)
					{
						Subblock usb2 = leftMb.___003C_003EuSubblocks[k][1];
						Subblock vsb2 = leftMb.___003C_003EvSubblocks[k][1];
						for (int i = 0; i < 4; i++)
						{
							uAvg += usb2.val[i * 4 + 3];
							vAvg += vsb2.val[i * 4 + 3];
						}
					}
				}
				int shift = 2;
				if (up_available != 0)
				{
					shift++;
				}
				if (left_available != 0)
				{
					shift++;
				}
				expected_udc = uAvg + (1 << shift - 1) >> shift;
				expected_vdc = vAvg + (1 << shift - 1) >> shift;
			}
			int[] ufill = new int[16];
			for (int aRow3 = 0; aRow3 < 4; aRow3++)
			{
				for (int aCol3 = 0; aCol3 < 4; aCol3++)
				{
					ufill[aRow3 * 4 + aCol3] = expected_udc;
				}
			}
			int[] vfill = new int[16];
			for (int aRow2 = 0; aRow2 < 4; aRow2++)
			{
				for (int aCol2 = 0; aCol2 < 4; aCol2++)
				{
					vfill[aRow2 * 4 + aCol2] = expected_vdc;
				}
			}
			for (int aRow = 0; aRow < 2; aRow++)
			{
				for (int aCol = 0; aCol < 2; aCol++)
				{
					Subblock usb = ___003C_003EuSubblocks[aRow][aCol];
					Subblock vsb = ___003C_003EvSubblocks[aRow][aCol];
					usb._predict = ufill;
					vsb._predict = vfill;
				}
			}
			break;
		}
		case 1:
		{
			Subblock[] aboveUSb = new Subblock[2];
			Subblock[] aboveVSb = new Subblock[2];
			for (int aCol5 = 0; aCol5 < 2; aCol5++)
			{
				aboveUSb[aCol5] = aboveMb.___003C_003EuSubblocks[1][aCol5];
				aboveVSb[aCol5] = aboveMb.___003C_003EvSubblocks[1][aCol5];
			}
			for (int aRow4 = 0; aRow4 < 2; aRow4++)
			{
				for (int aCol4 = 0; aCol4 < 2; aCol4++)
				{
					Subblock usb4 = ___003C_003EuSubblocks[aRow4][aCol4];
					Subblock vsb4 = ___003C_003EvSubblocks[aRow4][aCol4];
					int[] ublock = new int[16];
					int[] vblock = new int[16];
					for (int pRow = 0; pRow < 4; pRow++)
					{
						for (int pCol = 0; pCol < 4; pCol++)
						{
							ublock[pRow * 4 + pCol] = ((aboveUSb[aCol4].val == null) ? 127 : aboveUSb[aCol4].val[12 + pCol]);
							vblock[pRow * 4 + pCol] = ((aboveVSb[aCol4].val == null) ? 127 : aboveVSb[aCol4].val[12 + pCol]);
						}
					}
					usb4._predict = ublock;
					vsb4._predict = vblock;
				}
			}
			break;
		}
		case 2:
		{
			Subblock[] leftUSb = new Subblock[2];
			Subblock[] leftVSb = new Subblock[2];
			for (int aCol7 = 0; aCol7 < 2; aCol7++)
			{
				leftUSb[aCol7] = leftMb.___003C_003EuSubblocks[aCol7][1];
				leftVSb[aCol7] = leftMb.___003C_003EvSubblocks[aCol7][1];
			}
			for (int aRow5 = 0; aRow5 < 2; aRow5++)
			{
				for (int aCol6 = 0; aCol6 < 2; aCol6++)
				{
					Subblock usb5 = ___003C_003EuSubblocks[aRow5][aCol6];
					Subblock vsb5 = ___003C_003EvSubblocks[aRow5][aCol6];
					int[] ublock2 = new int[16];
					int[] vblock2 = new int[16];
					for (int pRow2 = 0; pRow2 < 4; pRow2++)
					{
						for (int pCol2 = 0; pCol2 < 4; pCol2++)
						{
							ublock2[pRow2 * 4 + pCol2] = ((leftUSb[aRow5].val == null) ? 129 : leftUSb[aRow5].val[pRow2 * 4 + 3]);
							vblock2[pRow2 * 4 + pCol2] = ((leftVSb[aRow5].val == null) ? 129 : leftVSb[aRow5].val[pRow2 * 4 + 3]);
						}
					}
					usb5._predict = ublock2;
					vsb5._predict = vblock2;
				}
			}
			break;
		}
		case 3:
		{
			VPXMacroblock ALMb = mbs[___003C_003ERrow - 1][___003C_003Ecolumn - 1];
			Subblock ALUSb = ALMb.___003C_003EuSubblocks[1][1];
			int alu = ALUSb.val[15];
			Subblock ALVSb = ALMb.___003C_003EvSubblocks[1][1];
			int alv = ALVSb.val[15];
			Subblock[] aboveUSb2 = new Subblock[2];
			Subblock[] leftUSb2 = new Subblock[2];
			Subblock[] aboveVSb2 = new Subblock[2];
			Subblock[] leftVSb2 = new Subblock[2];
			for (int x = 0; x < 2; x++)
			{
				aboveUSb2[x] = aboveMb.___003C_003EuSubblocks[1][x];
				leftUSb2[x] = leftMb.___003C_003EuSubblocks[x][1];
				aboveVSb2[x] = aboveMb.___003C_003EvSubblocks[1][x];
				leftVSb2[x] = leftMb.___003C_003EvSubblocks[x][1];
			}
			for (int sbRow = 0; sbRow < 2; sbRow++)
			{
				for (int pRow3 = 0; pRow3 < 4; pRow3++)
				{
					for (int sbCol = 0; sbCol < 2; sbCol++)
					{
						if (___003C_003EuSubblocks[sbRow][sbCol].val == null)
						{
							___003C_003EuSubblocks[sbRow][sbCol].val = new int[16];
						}
						if (___003C_003EvSubblocks[sbRow][sbCol].val == null)
						{
							___003C_003EvSubblocks[sbRow][sbCol].val = new int[16];
						}
						for (int pCol3 = 0; pCol3 < 4; pCol3++)
						{
							int upred = leftUSb2[sbRow].val[pRow3 * 4 + 3] + aboveUSb2[sbCol].val[12 + pCol3] - alu;
							upred = VP8Util.QuantizationParams.clip255(upred);
							___003C_003EuSubblocks[sbRow][sbCol].val[pRow3 * 4 + pCol3] = upred;
							int vpred = leftVSb2[sbRow].val[pRow3 * 4 + 3] + aboveVSb2[sbCol].val[12 + pCol3] - alv;
							vpred = VP8Util.QuantizationParams.clip255(vpred);
							___003C_003EvSubblocks[sbRow][sbCol].val[pRow3 * 4 + pCol3] = vpred;
						}
					}
				}
			}
			break;
		}
		default:
			java.lang.System.err.println(new StringBuilder().append("TODO predict_mb_uv: ").append(lumaMode).toString());
			java.lang.System.exit(0);
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 162, 103, 103, 48, 39, 231, 69, 103,
		103, 48, 39, 199, 105, 105, 50, 41, 233, 69
	})]
	public virtual void reconstruct()
	{
		for (int row3 = 0; row3 < 4; row3++)
		{
			for (int col3 = 0; col3 < 4; col3++)
			{
				___003C_003EySubblocks[row3][col3].reconstruct();
			}
		}
		for (int row2 = 0; row2 < 2; row2++)
		{
			for (int col2 = 0; col2 < 2; col2++)
			{
				___003C_003EuSubblocks[row2][col2].reconstruct();
			}
		}
		for (int row = 0; row < 2; row++)
		{
			for (int col = 0; col < 2; col++)
			{
				___003C_003EvSubblocks[row][col].reconstruct();
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 58, 98, 111, 143, 135, 106, 99, 100, 105,
		110, 105, 49, 9, 233, 72, 100, 105, 110, 105,
		50, 9, 233, 72, 100, 100, 103, 100, 135, 179,
		106, 106, 39, 169, 105, 105, 52, 41, 169
	})]
	private void predictLumaDC(VPXMacroblock above, VPXMacroblock left)
	{
		int hasAbove = ((___003C_003ERrow > 1) ? 1 : 0);
		int hasLeft = ((___003C_003Ecolumn > 1) ? 1 : 0);
		int expected_dc = 128;
		if (hasAbove != 0 || hasLeft != 0)
		{
			int average = 0;
			if (hasAbove != 0)
			{
				for (int m = 0; m < 4; m++)
				{
					Subblock sb2 = above.___003C_003EySubblocks[3][m];
					for (int k = 0; k < 4; k++)
					{
						average += sb2.val[12 + k];
					}
				}
			}
			if (hasLeft != 0)
			{
				for (int l = 0; l < 4; l++)
				{
					Subblock sb = left.___003C_003EySubblocks[l][3];
					for (int j = 0; j < 4; j++)
					{
						average += sb.val[j * 4 + 3];
					}
				}
			}
			int shift = 3;
			if (hasAbove != 0)
			{
				shift++;
			}
			if (hasLeft != 0)
			{
				shift++;
			}
			expected_dc = average + (1 << shift - 1) >> shift;
		}
		int[] fill = new int[16];
		for (int i = 0; i < 16; i++)
		{
			fill[i] = expected_dc;
		}
		for (int y = 0; y < 4; y++)
		{
			for (int x = 0; x < 4; x++)
			{
				___003C_003EySubblocks[y][x]._predict = fill;
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 37, 162, 104, 103, 46, 167, 106, 106, 109,
		106, 105, 105, 63, 8, 41, 233, 69, 234, 56,
		42, 234, 77
	})]
	private void predictLumaV(VPXMacroblock above)
	{
		Subblock[] aboveYSb = new Subblock[4];
		for (int col2 = 0; col2 < 4; col2++)
		{
			aboveYSb[col2] = above.___003C_003EySubblocks[3][col2];
		}
		for (int row = 0; row < 4; row++)
		{
			for (int col = 0; col < 4; col++)
			{
				Subblock sb = ___003C_003EySubblocks[row][col];
				int[] block = new int[16];
				for (int j = 0; j < 4; j++)
				{
					for (int i = 0; i < 4; i++)
					{
						block[j * 4 + i] = ((aboveYSb[col].val == null) ? 127 : aboveYSb[col].val[12 + i]);
					}
				}
				sb._predict = block;
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 47, 98, 104, 103, 46, 167, 106, 106, 109,
		106, 105, 105, 63, 12, 41, 201, 234, 57, 42,
		234, 74
	})]
	private void predictLumaH(VPXMacroblock leftMb)
	{
		Subblock[] leftYSb = new Subblock[4];
		for (int row2 = 0; row2 < 4; row2++)
		{
			leftYSb[row2] = leftMb.___003C_003EySubblocks[row2][3];
		}
		for (int row = 0; row < 4; row++)
		{
			for (int col = 0; col < 4; col++)
			{
				Subblock sb = ___003C_003EySubblocks[row][col];
				int[] block = new int[16];
				for (int bRow = 0; bRow < 4; bRow++)
				{
					for (int bCol = 0; bCol < 4; bCol++)
					{
						block[bRow * 4 + bCol] = ((leftYSb[row].val == null) ? 129 : leftYSb[row].val[bRow * 4 + 3]);
					}
				}
				sb._predict = block;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		43,
		162,
		104,
		104,
		103,
		46,
		135,
		103,
		46,
		167,
		108,
		108,
		108,
		116,
		153,
		137,
		159,
		6,
		byte.MaxValue,
		2,
		60,
		233,
		60,
		44,
		44,
		236,
		78
	})]
	private void predictLumaTM(VPXMacroblock above, VPXMacroblock left, int aboveLeft)
	{
		Subblock[] aboveYSb = new Subblock[4];
		Subblock[] leftYSb = new Subblock[4];
		for (int col2 = 0; col2 < 4; col2++)
		{
			aboveYSb[col2] = above.___003C_003EySubblocks[3][col2];
		}
		for (int row2 = 0; row2 < 4; row2++)
		{
			leftYSb[row2] = left.___003C_003EySubblocks[row2][3];
		}
		for (int row = 0; row < 4; row++)
		{
			for (int pRow = 0; pRow < 4; pRow++)
			{
				for (int col = 0; col < 4; col++)
				{
					if (___003C_003EySubblocks[row][col].val == null)
					{
						___003C_003EySubblocks[row][col].val = new int[16];
					}
					for (int pCol = 0; pCol < 4; pCol++)
					{
						int pred = leftYSb[row].val[pRow * 4 + 3] + aboveYSb[col].val[12 + pCol] - aboveLeft;
						___003C_003EySubblocks[row][col].val[pRow * 4 + pCol] = VP8Util.QuantizationParams.clip255(pred);
					}
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 23, 129, 67, 104, 100, 159, 0, 127, 0,
		127, 0, 127, 0, 115
	})]
	private void decodeMacroBlockTokens(bool withY2, VPXMacroblock[][] mbs, VPXBooleanDecoder decoder, int[][][][] coefProbs)
	{
		skipFilter = false;
		if (withY2)
		{
			skipFilter |= decodePlaneTokens(1, VP8Util.PLANE.___003C_003EY2, withY2: false, mbs, decoder, coefProbs);
		}
		skipFilter |= decodePlaneTokens(4, VP8Util.PLANE.___003C_003EY1, withY2, mbs, decoder, coefProbs);
		skipFilter |= decodePlaneTokens(2, VP8Util.PLANE.___003C_003EU, withY2: false, mbs, decoder, coefProbs);
		skipFilter |= decodePlaneTokens(2, VP8Util.PLANE.___003C_003EV, withY2: false, mbs, decoder, coefProbs);
		skipFilter = ((!skipFilter) ? true : false);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		20,
		97,
		67,
		99,
		106,
		106,
		100,
		100,
		110,
		114,
		110,
		111,
		110,
		111,
		110,
		139,
		byte.MaxValue,
		7,
		69,
		109,
		141,
		158,
		187,
		235,
		39,
		42,
		234,
		93
	})]
	private bool decodePlaneTokens(int dimentions, VP8Util.PLANE plane, bool withY2, VPXMacroblock[][] mbs, VPXBooleanDecoder decoder, int[][][][] coefProbs)
	{
		int r = 0;
		for (int row = 0; row < dimentions; row++)
		{
			for (int col = 0; col < dimentions; col++)
			{
				int lc = 0;
				Subblock sb = null;
				if (VP8Util.PLANE.___003C_003EY1.equals(plane))
				{
					sb = ___003C_003EySubblocks[row][col];
				}
				else if (VP8Util.PLANE.___003C_003EU.equals(plane))
				{
					sb = ___003C_003EuSubblocks[row][col];
				}
				else if (VP8Util.PLANE.___003C_003EV.equals(plane))
				{
					sb = ___003C_003EvSubblocks[row][col];
				}
				else
				{
					if (!VP8Util.PLANE.___003C_003EY2.equals(plane))
					{
						string s = new StringBuilder().append("unknown plane type ").append(plane).toString();
						
						throw new IllegalStateException(s);
					}
					sb = ___003C_003Ey2;
				}
				Subblock i = sb.getLeft(plane, mbs);
				Subblock a = sb.getAbove(plane, mbs);
				lc = (i.someValuePresent ? 1 : 0) + (a.someValuePresent ? 1 : 0);
				sb.decodeSubBlock(decoder, coefProbs, lc, VP8Util.planeToType(plane, Boolean.valueOf(withY2)), withY2);
				r |= (sb.someValuePresent ? 1 : 0);
			}
		}
		return (byte)r != 0;
	}
}
