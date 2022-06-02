using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.vpx;

public class VP8Decoder : VideoDecoder
{
	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class SegmentBasedAdjustments : Object
	{
		private int[] segmentProbs;

		private int[] qp;

		private int[] lf;

		private int abs;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 68, 162, 105, 104, 104, 104, 105 })]
		public SegmentBasedAdjustments(int[] segmentProbs, int[] qp, int[] lf, int abs)
		{
			this.segmentProbs = segmentProbs;
			this.qp = qp;
			this.lf = lf;
			this.abs = abs;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(293)]
		internal static int[] access_0024000(SegmentBasedAdjustments x0)
		{
			return x0.segmentProbs;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(293)]
		internal static int[] access_0024100(SegmentBasedAdjustments x0)
		{
			return x0.qp;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(293)]
		internal static int access_0024200(SegmentBasedAdjustments x0)
		{
			return x0.abs;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(293)]
		internal static int[] access_0024300(SegmentBasedAdjustments x0)
		{
			return x0.lf;
		}
	}

	private byte[][] segmentationMap;

	private int[] refLoopFilterDeltas;

	private int[] modeLoopFilterDeltas;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 105, 109, 109 })]
	public VP8Decoder()
	{
		refLoopFilterDeltas = new int[4];
		modeLoopFilterDeltas = new int[4];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 54, 129, 68 })]
	public static string printHexByte(byte b)
	{
		int b2 = (sbyte)b;
		string result = new StringBuilder().append("0x").append(Integer.toHexString(b2 & 0xFF)).toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 65, 66, 104, 136, 99, 99, 100, 103, 104,
		104, 105, 105, 105, 101, 108, 248, 60, 233, 71,
		105, 105, 101, 108, 248, 60, 233, 72, 105, 100,
		105, 105, 101, 143, 235, 59, 233, 72
	})]
	private SegmentBasedAdjustments updateSegmentation(VPXBooleanDecoder headerDecoder)
	{
		int updateMBSegmentationMap = headerDecoder.readBitEq();
		int updateSegmentFeatureData = headerDecoder.readBitEq();
		int[] qp = null;
		int[] lf = null;
		int abs = 0;
		if (updateSegmentFeatureData != 0)
		{
			qp = new int[4];
			lf = new int[4];
			abs = headerDecoder.readBitEq();
			for (int k = 0; k < 4; k++)
			{
				if (headerDecoder.readBitEq() != 0)
				{
					qp[k] = headerDecoder.decodeInt(7);
					qp[k] = ((headerDecoder.readBitEq() == 0) ? qp[k] : (-qp[k]));
				}
			}
			for (int j = 0; j < 4; j++)
			{
				if (headerDecoder.readBitEq() != 0)
				{
					lf[j] = headerDecoder.decodeInt(6);
					lf[j] = ((headerDecoder.readBitEq() == 0) ? lf[j] : (-lf[j]));
				}
			}
		}
		int[] segmentProbs = new int[3];
		if (updateMBSegmentationMap != 0)
		{
			for (int i = 0; i < 3; i++)
			{
				if (headerDecoder.readBitEq() != 0)
				{
					segmentProbs[i] = headerDecoder.decodeInt(8);
				}
				else
				{
					segmentProbs[i] = 255;
				}
			}
		}
		SegmentBasedAdjustments result = new SegmentBasedAdjustments(segmentProbs, qp, lf, abs);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 74, 130, 151, 137, 137, 141 })]
	private int edgeEmu(int mode, int mbCol, int mbRow)
	{
		switch (mode)
		{
		case 1:
			return (mbRow != 0) ? mode : 0;
		case 2:
			return (mbCol != 0) ? mode : 0;
		case 3:
		{
			int result = edgeEmuTm(mode, mbCol, mbRow);
			
			return result;
		}
		default:
			return mode;
		}
	}

	[LineNumberTable(new byte[] { 159, 71, 162, 100, 137 })]
	private int edgeEmuTm(int mode, int mbCol, int mbRow)
	{
		if (mbCol == 0)
		{
			return (mbRow != 0) ? 1 : 0;
		}
		return (mbRow == 0) ? 2 : mode;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		131,
		130,
		104,
		137,
		111,
		100,
		99,
		106,
		112,
		108,
		127,
		30,
		152,
		127,
		1,
		127,
		1,
		107,
		107,
		106,
		170,
		105,
		127,
		21,
		127,
		21,
		108,
		108,
		50,
		41,
		233,
		70,
		105,
		106,
		144,
		112,
		106,
		100,
		104,
		139,
		106,
		106,
		62,
		41,
		233,
		70,
		106,
		107,
		115,
		107,
		106,
		105,
		106,
		105,
		137,
		107,
		146,
		107,
		245,
		58,
		233,
		73,
		137,
		107,
		114,
		107,
		245,
		59,
		233,
		74,
		139,
		107,
		100,
		101,
		112,
		105,
		110,
		139,
		107,
		119,
		119,
		119,
		119,
		119,
		112,
		180,
		104,
		108,
		108,
		108,
		138,
		124,
		107,
		241,
		60,
		41,
		44,
		44,
		236,
		75,
		106,
		107,
		107,
		109,
		109,
		111,
		146,
		123,
		149,
		120,
		101,
		106,
		148,
		117,
		180,
		170,
		101,
		101,
		110,
		109,
		106,
		99,
		202,
		120,
		106,
		153,
		126,
		215,
		102,
		145,
		185,
		110,
		108,
		140,
		112,
		145,
		145,
		byte.MaxValue,
		10,
		57,
		44,
		241,
		81,
		159,
		0,
		100,
		131,
		100,
		131,
		100,
		131,
		100,
		131,
		164,
		121,
		105,
		105,
		53,
		41,
		201,
		249,
		159,
		172,
		44,
		236,
		160,
		89,
		106,
		106,
		111,
		110,
		234,
		61,
		41,
		233,
		72,
		106,
		102,
		107,
		109,
		230,
		69,
		146,
		106,
		138,
		106,
		106,
		111,
		14,
		41,
		233,
		70
	})]
	public override Picture decodeFrame(ByteBuffer frame, byte[][] buffer)
	{
		byte[] firstThree = new byte[3];
		frame.get(firstThree);
		int keyFrame = ((VP8Util.getBitInBytes(firstThree, 0) == 0) ? 1 : 0);
		if (keyFrame == 0)
		{
			return null;
		}
		int version = VP8Util.getBitsInBytes(firstThree, 1, 3);
		int showFrame = ((VP8Util.getBitInBytes(firstThree, 4) > 0) ? 1 : 0);
		int partitionSize = VP8Util.getBitsInBytes(firstThree, 5, 19);
		string threeByteToken = new StringBuilder().append(printHexByte((byte)(sbyte)frame.get())).append(" ").append(printHexByte((byte)(sbyte)frame.get()))
			.append(" ")
			.append(printHexByte((byte)(sbyte)frame.get()))
			.toString();
		int twoBytesWidth = ((sbyte)frame.get() & 0xFF) | (((sbyte)frame.get() & 0xFF) << 8);
		int twoBytesHeight = ((sbyte)frame.get() & 0xFF) | (((sbyte)frame.get() & 0xFF) << 8);
		int width = twoBytesWidth & 0x3FFF;
		int height = twoBytesHeight & 0x3FFF;
		int numberOfMBRows = VP8Util.getMacroblockCount(height);
		int numberOfMBCols = VP8Util.getMacroblockCount(width);
		int[] array;
		int num;
		if (segmentationMap == null)
		{
			array = new int[2];
			num = (array[1] = numberOfMBCols);
			num = (array[0] = numberOfMBRows);
			segmentationMap = (byte[][])ByteCodeHelper.multianewarray(typeof(byte[][]).TypeHandle, array);
		}
		int num2 = numberOfMBRows + 2;
		int num3 = numberOfMBCols + 2;
		array = new int[2];
		num = (array[1] = num3);
		num = (array[0] = num2);
		VPXMacroblock[][] mbs = (VPXMacroblock[][])ByteCodeHelper.multianewarray(typeof(VPXMacroblock[][]).TypeHandle, array);
		for (int row2 = 0; row2 < numberOfMBRows + 2; row2++)
		{
			for (int col2 = 0; col2 < numberOfMBCols + 2; col2++)
			{
				mbs[row2][col2] = new VPXMacroblock(row2, col2);
			}
		}
		int headerOffset = frame.position();
		VPXBooleanDecoder headerDecoder = new VPXBooleanDecoder(frame, 0);
		int isYUVColorSpace = ((headerDecoder.readBitEq() == 0) ? 1 : 0);
		int clampingRequired = ((headerDecoder.readBitEq() == 0) ? 1 : 0);
		int segmentation = headerDecoder.readBitEq();
		SegmentBasedAdjustments segmentBased = null;
		if (segmentation != 0)
		{
			segmentBased = updateSegmentation(headerDecoder);
			for (int row = 0; row < numberOfMBRows; row++)
			{
				for (int col = 0; col < numberOfMBCols; col++)
				{
					mbs[row + 1][col + 1].segment = segmentationMap[row][col];
				}
			}
		}
		int simpleFilter = headerDecoder.readBitEq();
		int filterLevel = headerDecoder.decodeInt(6);
		int filterType = ((filterLevel != 0) ? ((simpleFilter > 0) ? 1 : 2) : 0);
		int sharpnessLevel = headerDecoder.decodeInt(3);
		int loopFilterDeltaFlag = headerDecoder.readBitEq();
		if (loopFilterDeltaFlag == 1)
		{
			int loopFilterDeltaUpdate = headerDecoder.readBitEq();
			if (loopFilterDeltaUpdate == 1)
			{
				for (int k = 0; k < 4; k++)
				{
					if (headerDecoder.readBitEq() > 0)
					{
						refLoopFilterDeltas[k] = headerDecoder.decodeInt(6);
						if (headerDecoder.readBitEq() > 0)
						{
							refLoopFilterDeltas[k] *= -1;
						}
					}
				}
				for (int j = 0; j < 4; j++)
				{
					if (headerDecoder.readBitEq() > 0)
					{
						modeLoopFilterDeltas[j] = headerDecoder.decodeInt(6);
						if (headerDecoder.readBitEq() > 0)
						{
							modeLoopFilterDeltas[j] *= -1;
						}
					}
				}
			}
		}
		int log2OfPartCnt = headerDecoder.decodeInt(2);
		Preconditions.checkState(0 == log2OfPartCnt);
		int partitionsCount = 1;
		long runningSize = 0L;
		long zSize = frame.limit() - (partitionSize + headerOffset);
		ByteBuffer tokenBuffer = frame.duplicate();
		tokenBuffer.position(partitionSize + headerOffset);
		VPXBooleanDecoder decoder = new VPXBooleanDecoder(tokenBuffer, 0);
		int yacIndex = headerDecoder.decodeInt(7);
		int ydcDelta = ((headerDecoder.readBitEq() > 0) ? VP8Util.delta(headerDecoder) : 0);
		int y2dcDelta = ((headerDecoder.readBitEq() > 0) ? VP8Util.delta(headerDecoder) : 0);
		int y2acDelta = ((headerDecoder.readBitEq() > 0) ? VP8Util.delta(headerDecoder) : 0);
		int chromaDCDelta = ((headerDecoder.readBitEq() > 0) ? VP8Util.delta(headerDecoder) : 0);
		int chromaACDelta = ((headerDecoder.readBitEq() > 0) ? VP8Util.delta(headerDecoder) : 0);
		int refreshProbs = ((headerDecoder.readBitEq() == 0) ? 1 : 0);
		VP8Util.QuantizationParams quants = new VP8Util.QuantizationParams(yacIndex, ydcDelta, y2dcDelta, y2acDelta, chromaDCDelta, chromaACDelta);
		int[][][][] coefProbs = VP8Util.getDefaultCoefProbs();
		for (int i = 0; i < 4; i++)
		{
			for (int l = 0; l < 8; l++)
			{
				for (int m = 0; m < 3; m++)
				{
					for (int n = 0; n < 11; n++)
					{
						if (headerDecoder.readBit(VP8Util.vp8CoefUpdateProbs[i][l][m][n]) > 0)
						{
							int newp = headerDecoder.decodeInt(8);
							coefProbs[i][l][m][n] = newp;
						}
					}
				}
			}
		}
		int macroBlockNoCoeffSkip = headerDecoder.readBitEq();
		Preconditions.checkState(1 == macroBlockNoCoeffSkip);
		int probSkipFalse = headerDecoder.decodeInt(8);
		for (int mbRow3 = 0; mbRow3 < numberOfMBRows; mbRow3++)
		{
			for (int mbCol3 = 0; mbCol3 < numberOfMBCols; mbCol3++)
			{
				VPXMacroblock mb3 = mbs[mbRow3 + 1][mbCol3 + 1];
				if (segmentation != 0 && segmentBased != null && SegmentBasedAdjustments.access_0024000(segmentBased) != null)
				{
					mb3.segment = headerDecoder.readTree(VP8Util.segmentTree, SegmentBasedAdjustments.access_0024000(segmentBased));
					segmentationMap[mbRow3][mbCol3] = (byte)(sbyte)mb3.segment;
				}
				if (segmentation != 0 && segmentBased != null && SegmentBasedAdjustments.access_0024100(segmentBased) != null)
				{
					int qIndex = yacIndex;
					qIndex = ((SegmentBasedAdjustments.access_0024200(segmentBased) == 0) ? (qIndex + SegmentBasedAdjustments.access_0024100(segmentBased)[mb3.segment]) : SegmentBasedAdjustments.access_0024100(segmentBased)[mb3.segment]);
					quants = new VP8Util.QuantizationParams(qIndex, ydcDelta, y2dcDelta, y2acDelta, chromaDCDelta, chromaACDelta);
				}
				mb3.quants = quants;
				if (loopFilterDeltaFlag != 0)
				{
					int level = filterLevel;
					level += refLoopFilterDeltas[0];
					level = (mb3.filterLevel = MathUtil.clip(level, 0, 63));
				}
				else
				{
					mb3.filterLevel = filterLevel;
				}
				if (segmentation != 0 && segmentBased != null && SegmentBasedAdjustments.access_0024300(segmentBased) != null)
				{
					if (SegmentBasedAdjustments.access_0024200(segmentBased) != 0)
					{
						mb3.filterLevel = SegmentBasedAdjustments.access_0024300(segmentBased)[mb3.segment];
					}
					else
					{
						mb3.filterLevel += SegmentBasedAdjustments.access_0024300(segmentBased)[mb3.segment];
						mb3.filterLevel = MathUtil.clip(mb3.filterLevel, 0, 63);
					}
				}
				if (macroBlockNoCoeffSkip > 0)
				{
					mb3.skipCoeff = headerDecoder.readBit(probSkipFalse);
				}
				mb3.lumaMode = headerDecoder.readTree(VP8Util.keyFrameYModeTree, VP8Util.keyFrameYModeProb);
				if (mb3.lumaMode == 4)
				{
					for (int sbRow = 0; sbRow < 4; sbRow++)
					{
						for (int sbCol = 0; sbCol < 4; sbCol++)
						{
							VPXMacroblock.Subblock sb = mb3.___003C_003EySubblocks[sbRow][sbCol];
							VPXMacroblock.Subblock A = sb.getAbove(VP8Util.PLANE.___003C_003EY1, mbs);
							VPXMacroblock.Subblock L = sb.getLeft(VP8Util.PLANE.___003C_003EY1, mbs);
							sb.mode = headerDecoder.readTree(VP8Util.SubblockConstants.subblockModeTree, VP8Util.SubblockConstants.keyFrameSubblockModeProb[A.mode][L.mode]);
						}
					}
				}
				else
				{
					int fixedMode = mb3.lumaMode switch
					{
						0 => 0, 
						1 => 2, 
						2 => 3, 
						3 => 1, 
						_ => 0, 
					};
					mb3.lumaMode = edgeEmu(mb3.lumaMode, mbCol3, mbRow3);
					for (int x = 0; x < 4; x++)
					{
						for (int y = 0; y < 4; y++)
						{
							mb3.___003C_003EySubblocks[y][x].mode = fixedMode;
						}
					}
				}
				mb3.chromaMode = headerDecoder.readTree(VP8Util.vp8UVModeTree, VP8Util.vp8KeyFrameUVModeProb);
			}
		}
		for (int mbRow2 = 0; mbRow2 < numberOfMBRows; mbRow2++)
		{
			for (int mbCol2 = 0; mbCol2 < numberOfMBCols; mbCol2++)
			{
				VPXMacroblock mb2 = mbs[mbRow2 + 1][mbCol2 + 1];
				mb2.decodeMacroBlock(mbs, decoder, coefProbs);
				mb2.dequantMacroBlock(mbs);
			}
		}
		if (filterType > 0 && filterLevel != 0)
		{
			switch (filterType)
			{
			case 2:
				FilterUtil.loopFilterUV(mbs, sharpnessLevel, (byte)keyFrame != 0);
				FilterUtil.loopFilterY(mbs, sharpnessLevel, (byte)keyFrame != 0);
				break;
			}
		}
		Picture p = Picture.createPicture(width, height, buffer, ColorSpace.___003C_003EYUV420);
		int mbWidth = VP8Util.getMacroblockCount(width);
		int mbHeight = VP8Util.getMacroblockCount(height);
		for (int mbRow = 0; mbRow < mbHeight; mbRow++)
		{
			for (int mbCol = 0; mbCol < mbWidth; mbCol++)
			{
				VPXMacroblock mb = mbs[mbRow + 1][mbCol + 1];
				mb.put(mbRow, mbCol, p);
			}
		}
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 55, 66, 127, 26, 100 })]
	public static int probe(ByteBuffer data)
	{
		if (((sbyte)data.get(3) & 0xFF) == 157 && ((sbyte)data.get(4) & 0xFF) == 1 && ((sbyte)data.get(5) & 0xFF) == 42)
		{
			return 100;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 53, 162, 137, 127, 0, 127, 0, 105, 137 })]
	public override VideoCodecMeta getCodecMeta(ByteBuffer frame)
	{
		NIOUtils.skip(frame, 6);
		int twoBytesWidth = ((sbyte)frame.get() & 0xFF) | (((sbyte)frame.get() & 0xFF) << 8);
		int twoBytesHeight = ((sbyte)frame.get() & 0xFF) | (((sbyte)frame.get() & 0xFF) << 8);
		int width = twoBytesWidth & 0x3FFF;
		int height = twoBytesHeight & 0x3FFF;
		VideoCodecMeta result = VideoCodecMeta.createSimpleVideoCodecMeta(new Size(width, height), ColorSpace.___003C_003EYUV420);
		
		return result;
	}
}
