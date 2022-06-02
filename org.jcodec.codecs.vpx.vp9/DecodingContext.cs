using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.codecs.vpx.vp9;

public class DecodingContext : Object
{
	private int profile;

	private int showExistingFrame;

	private int frameToShowMapIdx;

	private int frameType;

	private int showFrame;

	private int errorResilientMode;

	private int refreshFrameFlags;

	private int frameIsIntra;

	private int intraOnly;

	private int resetFrameContext;

	private int colorSpace;

	internal int subsamplingX;

	internal int subsamplingY;

	internal int bitDepth;

	internal int frameWidth;

	internal int frameHeight;

	private int renderWidth;

	private int renderHeight;

	private int[] refFrameWidth;

	private int[] refFrameHeight;

	private int[] refFrameIdx;

	private int[] m_refFrameSignBias;

	private int allowHighPrecisionMv;

	internal int interpFilter;

	private int frameParallelDecodingMode;

	private int refreshFrameContext;

	private int frameContextIdx;

	private int[] loopFilterRefDeltas;

	private int[] loopFilterModeDeltas;

	private int baseQIdx;

	private int deltaQYDc;

	private int deltaQUvDc;

	private int deltaQUvAc;

	private bool lossless;

	private bool segmentationEnabled;

	private int[] segmentationTreeProbs;

	private int[] segmentationPredProbs;

	private int[][] featureEnabled;

	private int[][] featureData;

	private int tileRowsLog2;

	private int tileColsLog2;

	internal int txMode;

	private int compFixedRef;

	private int compVarRef0;

	private int compVarRef1;

	internal int refMode;

	internal int[][] tx8x8Probs;

	internal int[][] tx16x16Probs;

	internal int[][] tx32x32Probs;

	internal int[][][][][][] coefProbs;

	private int[] skipProbs;

	internal int[][] interModeProbs;

	internal int[][] interpFilterProbs;

	private int[] isInterProbs;

	private int[] compModeProbs;

	private int[][] singleRefProbs;

	private int[] compRefProbs;

	internal int[][] yModeProbs;

	internal int[][] partitionProbs;

	public int[][] uvModeProbs;

	private int[] mvJointProbs;

	private int[] mvSignProbs;

	private int[][] mvClassProbs;

	private int[] mvClass0BitProbs;

	private int[][] mvBitsProbs;

	private int[][][] mvClass0FrProbs;

	private int[][] mvFrProbs;

	private int[] mvClass0HpProb;

	private int[] mvHpProbs;

	private int filterLevel;

	private int sharpnessLevel;

	internal int[] leftPartitionSizes;

	internal int[] abovePartitionSizes;

	internal int tileHeight;

	internal int tileWidth;

	internal bool[] leftSkipped;

	internal bool[] aboveSkipped;

	internal int[][] aboveNonzeroContext;

	internal int[][] leftNonzeroContext;

	internal int[] aboveModes;

	internal int[] leftModes;

	private int colorRange;

	internal int[] aboveRefs;

	internal int[] leftRefs;

	internal int[] leftInterpFilters;

	internal int[] aboveInterpFilters;

	internal int miTileStartCol;

	internal int[] leftTxSizes;

	internal int[] aboveTxSizes;

	internal bool[] leftCompound;

	internal bool[] aboveCompound;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] defaultSkipProb;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[][] defaultTxProbs8x8;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[][] defaultTxProbs16x16;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[][] defaultTxProbs32x32;

	internal static int[][][][][][] ___003C_003EdefaultCoefProbs;

	internal static int[] ___003C_003EdefaultMvJointProbs;

	internal static int[][] ___003C_003EdefaultMvBitsProb;

	internal static int[] ___003C_003EdefaultMvClass0BitProb;

	internal static int[] ___003C_003EdefaultMvClass0HpProb;

	internal static int[] ___003C_003EdefaultMvSignProb;

	internal static int[][] ___003C_003EdefaultMvClassProbs;

	internal static int[][][] ___003C_003EdefaultMvClass0FrProbs;

	internal static int[][] ___003C_003EdefaultMvFrProbs;

	internal static int[] ___003C_003EdefaultMvHpProb;

	internal static int[][] ___003C_003EdefaultInterModeProbs;

	internal static int[][] ___003C_003EdefaultInterpFilterProbs;

	internal static int[] ___003C_003EdefaultIsInterProbs;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[][] defaultPartitionProbs;

	internal static int[][][] ___003C_003EkfYmodeProbs;

	internal static int[][] ___003C_003EkfUvModeProbs;

	internal static int[][] ___003C_003EdefaultYModeProbs;

	internal static int[][] ___003C_003EdefaultUvModeProbs;

	internal static int[][] ___003C_003EdefaultSingleRefProb;

	internal static int[] ___003C_003EdefaultCompRefProb;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][][][][][] defaultCoefProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultCoefProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultMvJointProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultMvJointProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] defaultMvBitsProb
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultMvBitsProb;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultMvClass0BitProb
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultMvClass0BitProb;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultMvClass0HpProb
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultMvClass0HpProb;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultMvSignProb
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultMvSignProb;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] defaultMvClassProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultMvClassProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][][] defaultMvClass0FrProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultMvClass0FrProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] defaultMvFrProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultMvFrProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultMvHpProb
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultMvHpProb;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] defaultInterModeProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultInterModeProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] defaultInterpFilterProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultInterpFilterProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultIsInterProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultIsInterProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][][] kfYmodeProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkfYmodeProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] kfUvModeProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EkfUvModeProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] defaultYModeProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultYModeProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] defaultUvModeProbs
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultUvModeProbs;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[][] defaultSingleRefProb
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultSingleRefProb;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] defaultCompRefProb
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdefaultCompRefProb;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(713)]
	public virtual bool isKeyIntraFrame()
	{
		return false;
	}

	[LineNumberTable(809)]
	public virtual int getMiTileHeight()
	{
		return tileHeight;
	}

	[LineNumberTable(813)]
	public virtual int getMiTileWidth()
	{
		return tileWidth;
	}

	[LineNumberTable(865)]
	public virtual int[] getLeftPartitionSizes()
	{
		return leftPartitionSizes;
	}

	[LineNumberTable(869)]
	public virtual int[] getAbovePartitionSizes()
	{
		return abovePartitionSizes;
	}

	[LineNumberTable(1514)]
	public virtual int[][] getPartitionProbs()
	{
		return partitionProbs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		232,
		162,
		233,
		157,
		140,
		109,
		109,
		109,
		237,
		70,
		109,
		237,
		71,
		109,
		109,
		127,
		11,
		byte.MaxValue,
		11,
		74,
		127,
		11,
		127,
		11,
		159,
		11,
		109,
		127,
		11,
		127,
		11,
		141,
		109,
		127,
		11,
		141,
		127,
		12,
		159,
		12,
		159,
		13,
		109,
		109,
		127,
		12,
		109,
		127,
		12,
		127,
		17,
		127,
		11,
		109,
		237,
		162,
		61,
		113,
		113,
		113,
		145,
		127,
		23,
		106,
		108,
		108,
		127,
		21,
		105,
		63,
		22,
		9,
		44,
		44,
		234,
		75,
		145,
		113,
		113,
		113,
		113,
		113,
		113,
		113,
		113,
		145,
		113,
		145,
		145,
		145,
		113,
		145,
		145,
		115
	})]
	protected internal DecodingContext()
	{
		refFrameWidth = new int[4];
		refFrameHeight = new int[4];
		refFrameIdx = new int[3];
		this.m_refFrameSignBias = new int[3];
		loopFilterRefDeltas = new int[4];
		loopFilterModeDeltas = new int[2];
		segmentationTreeProbs = new int[7];
		segmentationPredProbs = new int[3];
		int[] array = new int[2];
		int num = (array[1] = 4);
		num = (array[0] = 8);
		featureEnabled = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 4);
		num = (array[0] = 8);
		featureData = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 1);
		num = (array[0] = 2);
		tx8x8Probs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 2);
		tx16x16Probs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 3);
		num = (array[0] = 2);
		tx32x32Probs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		skipProbs = new int[3];
		array = new int[2];
		num = (array[1] = 3);
		num = (array[0] = 7);
		interModeProbs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 4);
		interpFilterProbs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		isInterProbs = new int[4];
		compModeProbs = new int[5];
		array = new int[2];
		num = (array[1] = 2);
		num = (array[0] = 5);
		singleRefProbs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		compRefProbs = new int[5];
		array = new int[2];
		num = (array[1] = 9);
		num = (array[0] = 4);
		yModeProbs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 3);
		num = (array[0] = 16);
		partitionProbs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 9);
		num = (array[0] = 10);
		uvModeProbs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		mvJointProbs = new int[3];
		mvSignProbs = new int[2];
		array = new int[2];
		num = (array[1] = 10);
		num = (array[0] = 2);
		mvClassProbs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		mvClass0BitProbs = new int[2];
		array = new int[2];
		num = (array[1] = 10);
		num = (array[0] = 2);
		mvBitsProbs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 3);
		num = (array[1] = 2);
		num = (array[0] = 2);
		mvClass0FrProbs = (int[][][])ByteCodeHelper.multianewarray(typeof(int[][][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 3);
		num = (array[0] = 2);
		mvFrProbs = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		mvClass0HpProb = new int[2];
		mvHpProbs = new int[2];
		ArrayUtil.copy1D(skipProbs, defaultSkipProb);
		ArrayUtil.copy2D(tx8x8Probs, defaultTxProbs8x8);
		ArrayUtil.copy2D(tx16x16Probs, defaultTxProbs16x16);
		ArrayUtil.copy2D(tx32x32Probs, defaultTxProbs32x32);
		array = new int[4];
		num = (array[3] = 6);
		num = (array[2] = 2);
		num = (array[1] = 2);
		num = (array[0] = 4);
		coefProbs = (int[][][][][][])ByteCodeHelper.multianewarray(typeof(int[][][][][][]).TypeHandle, array);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < 2; k++)
				{
					int[][][] obj = coefProbs[i][j][k];
					array = new int[2];
					num = (array[1] = 3);
					num = (array[0] = 3);
					obj[0] = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
					for (int l = 1; l < 6; l++)
					{
						int[][][] obj2 = coefProbs[i][j][k];
						int num2 = l;
						array = new int[2];
						num = (array[1] = 3);
						num = (array[0] = 6);
						obj2[num2] = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
					}
				}
			}
		}
		ArrayUtil.copy6D(coefProbs, ___003C_003EdefaultCoefProbs);
		ArrayUtil.copy1D(mvJointProbs, ___003C_003EdefaultMvJointProbs);
		ArrayUtil.copy1D(mvSignProbs, ___003C_003EdefaultMvSignProb);
		ArrayUtil.copy2D(mvClassProbs, ___003C_003EdefaultMvClassProbs);
		ArrayUtil.copy1D(mvClass0BitProbs, ___003C_003EdefaultMvClass0BitProb);
		ArrayUtil.copy2D(mvBitsProbs, ___003C_003EdefaultMvBitsProb);
		ArrayUtil.copy3D(mvClass0FrProbs, ___003C_003EdefaultMvClass0FrProbs);
		ArrayUtil.copy2D(mvFrProbs, ___003C_003EdefaultMvFrProbs);
		ArrayUtil.copy1D(mvClass0HpProb, ___003C_003EdefaultMvClass0HpProb);
		ArrayUtil.copy1D(mvHpProbs, ___003C_003EdefaultMvHpProb);
		ArrayUtil.copy2D(interModeProbs, ___003C_003EdefaultInterModeProbs);
		ArrayUtil.copy2D(interpFilterProbs, ___003C_003EdefaultInterpFilterProbs);
		ArrayUtil.copy1D(isInterProbs, ___003C_003EdefaultIsInterProbs);
		ArrayUtil.copy2D(singleRefProbs, ___003C_003EdefaultSingleRefProb);
		ArrayUtil.copy2D(yModeProbs, ___003C_003EdefaultYModeProbs);
		ArrayUtil.copy2D(uvModeProbs, ___003C_003EdefaultUvModeProbs);
		ArrayUtil.copy2D(partitionProbs, defaultPartitionProbs);
		ArrayUtil.copy1D(compRefProbs, ___003C_003EdefaultCompRefProb);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 176, 162, 136, 105, 118, 106, 104, 109, 106,
		142, 109, 109, 109, 105, 103, 104, 104, 104, 108,
		141, 104, 105, 141, 104, 105, 142, 106, 103, 106,
		138, 104, 104, 104, 136, 110, 104, 141, 137, 103,
		112, 17, 199, 104, 109, 168, 104, 105, 109, 141,
		110, 104, 104, 104, 104, 107, 135
	})]
	protected internal virtual int readUncompressedHeader(ByteBuffer bb)
	{
		BitReader br = BitReader.createBitReader(bb);
		int frame_marker = br.readNBit(2);
		profile = br.read1Bit() | (br.read1Bit() << 1);
		if (profile == 3)
		{
			br.read1Bit();
		}
		showExistingFrame = br.read1Bit();
		if (showExistingFrame == 1)
		{
			frameToShowMapIdx = br.readNBit(3);
		}
		frameType = br.read1Bit();
		showFrame = br.read1Bit();
		errorResilientMode = br.read1Bit();
		if (frameType == 0)
		{
			frame_sync_code(br);
			readColorConfig(br);
			readFrameSize(br);
			readRenderSize(br);
			this.refreshFrameFlags = 255;
			frameIsIntra = 1;
		}
		else
		{
			intraOnly = 0;
			if (showFrame == 0)
			{
				intraOnly = br.read1Bit();
			}
			resetFrameContext = 0;
			if (errorResilientMode == 0)
			{
				resetFrameContext = br.readNBit(2);
			}
			if (intraOnly == 1)
			{
				frame_sync_code(br);
				if (profile > 0)
				{
					readColorConfig(br);
				}
				else
				{
					colorSpace = 1;
					subsamplingX = 1;
					subsamplingY = 1;
					bitDepth = 8;
				}
				this.refreshFrameFlags = br.readNBit(8);
				readFrameSize(br);
				readRenderSize(br);
			}
			else
			{
				int refreshFrameFlags = br.readNBit(8);
				for (int i = 0; i < 3; i++)
				{
					refFrameIdx[i] = br.readNBit(3);
					this.m_refFrameSignBias[1 + i] = br.read1Bit();
				}
				readFrameSizeWithRefs(br);
				allowHighPrecisionMv = br.read1Bit();
				readInterpolationFilter(br);
			}
		}
		refreshFrameContext = 0;
		if (errorResilientMode == 0)
		{
			refreshFrameContext = br.read1Bit();
			frameParallelDecodingMode = br.read1Bit();
		}
		frameContextIdx = br.readNBit(2);
		readLoopFilterParams(br);
		readQuantizationParams(br);
		readSegmentationParams(br);
		readTileInfo(br);
		int headerSizeInBytes = br.readNBit(16);
		br.terminate();
		return headerSizeInBytes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 112, 98, 137, 105, 145, 168, 106, 136, 104,
		104, 105, 104, 106, 104, 104, 104, 104, 104, 104,
		138
	})]
	protected internal virtual void readCompressedHeader(ByteBuffer compressedHeader)
	{
		VPXBooleanDecoder boolDec = new VPXBooleanDecoder(compressedHeader, 0);
		if (boolDec.readBitEq() != 0)
		{
			
			throw new RuntimeException("Invalid marker bit");
		}
		readTxMode(boolDec);
		if (txMode == 4)
		{
			readTxModeProbs(boolDec);
		}
		readCoefProbs(boolDec);
		readSkipProb(boolDec);
		if (frameIsIntra == 0)
		{
			readInterModeProbs(boolDec);
			if (interpFilter == 3)
			{
				readInterpFilterProbs(boolDec);
			}
			readIsInterProbs(boolDec);
			frameReferenceMode(boolDec);
			frameReferenceModeProbs(boolDec);
			readYModeProbs(boolDec);
			readPartitionProbs(boolDec);
			mvProbs(boolDec);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 115, 162, 106 })]
	private static void frame_sync_code(BitReader br)
	{
		int code = br.readNBit(24);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 122, 162, 106, 104, 113, 99, 136, 105, 101,
		109, 115, 109, 109, 104, 99, 104, 170, 104, 115,
		104, 104, 168
	})]
	private void readColorConfig(BitReader br)
	{
		if (profile >= 2)
		{
			int ten_or_twelve_bit = br.read1Bit();
			bitDepth = ((ten_or_twelve_bit != 1) ? 10 : 12);
		}
		else
		{
			bitDepth = 8;
		}
		int colorSpace = br.readNBit(3);
		if (colorSpace != 7)
		{
			colorRange = br.read1Bit();
			if (profile == 1 || profile == 3)
			{
				subsamplingX = br.read1Bit();
				subsamplingY = br.read1Bit();
				int num = br.read1Bit();
			}
			else
			{
				subsamplingX = 1;
				subsamplingY = 1;
			}
		}
		else
		{
			colorRange = 1;
			if (profile == 1 || profile == 3)
			{
				subsamplingX = 0;
				subsamplingY = 0;
				int num2 = br.read1Bit();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 123, 130, 113, 113 })]
	private void readFrameSize(BitReader br)
	{
		frameWidth = br.readNBit(16) + 1;
		frameHeight = br.readNBit(16) + 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 125, 66, 106, 113, 147, 109, 141 })]
	private void readRenderSize(BitReader br)
	{
		if (br.read1Bit() == 1)
		{
			renderWidth = br.readNBit(16) + 1;
			renderHeight = br.readNBit(16) + 1;
		}
		else
		{
			renderWidth = frameWidth;
			renderHeight = frameHeight;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 129, 130, 103, 106, 118, 118, 227, 60, 231,
		71, 101, 136, 106
	})]
	private void readFrameSizeWithRefs(BitReader br)
	{
		int i;
		for (i = 0; i < 3; i++)
		{
			if (br.read1Bit() == 1)
			{
				frameWidth = refFrameWidth[refFrameIdx[i]];
				frameHeight = refFrameHeight[refFrameIdx[i]];
				break;
			}
		}
		if (i == 3)
		{
			readFrameSize(br);
		}
		readRenderSize(br);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 131, 130, 104, 105, 148 })]
	private void readInterpolationFilter(BitReader br)
	{
		interpFilter = 3;
		if (br.read1Bit() == 0)
		{
			interpFilter = Consts.___003C_003ELITERAL_TO_FILTER_TYPE[br.readNBit(2)];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 136, 162, 110, 110, 104, 104, 104, 104, 103,
		106, 16, 199, 103, 106, 16, 231, 70
	})]
	private void readLoopFilterParams(BitReader br)
	{
		filterLevel = br.readNBit(6);
		sharpnessLevel = br.readNBit(3);
		int modeRefDeltaEnabled = br.read1Bit();
		if (modeRefDeltaEnabled != 1)
		{
			return;
		}
		int modeRefDeltaUpdate = br.read1Bit();
		if (modeRefDeltaUpdate != 1)
		{
			return;
		}
		for (int j = 0; j < 4; j++)
		{
			if (br.read1Bit() == 1)
			{
				loopFilterRefDeltas[j] = br.readNBitSigned(6);
			}
		}
		for (int i = 0; i < 2; i++)
		{
			if (br.read1Bit() == 1)
			{
				loopFilterModeDeltas[i] = br.readNBitSigned(6);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 138, 162, 110, 109, 109, 109, 127, 12 })]
	private void readQuantizationParams(BitReader br)
	{
		baseQIdx = br.readNBit(8);
		deltaQYDc = readDeltaQ(br);
		deltaQUvDc = readDeltaQ(br);
		deltaQUvAc = readDeltaQ(br);
		lossless = ((baseQIdx == 0 && deltaQYDc == 0 && deltaQUvDc == 0 && deltaQUvAc == 0) ? true : false);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 147, 66, 112, 108, 109, 103, 47, 135, 104,
		103, 58, 167, 109, 104, 108, 108, 106, 110, 107,
		107, 108, 106, 135, 239, 55, 44, 236, 80
	})]
	private void readSegmentationParams(BitReader br)
	{
		segmentationEnabled = br.read1Bit() == 1;
		if (!segmentationEnabled)
		{
			return;
		}
		if (br.read1Bit() == 1)
		{
			for (int k = 0; k < 7; k++)
			{
				segmentationTreeProbs[k] = readProb(br);
			}
			int segmentationTemporalUpdate = br.read1Bit();
			for (int j = 0; j < 3; j++)
			{
				segmentationPredProbs[j] = ((segmentationTemporalUpdate != 1) ? 255 : readProb(br));
			}
		}
		if (br.read1Bit() != 1)
		{
			return;
		}
		int segmentationAbsOrDeltaUpdate = br.read1Bit();
		for (int i = 0; i < 8; i++)
		{
			for (int l = 0; l < 4; l++)
			{
				if (br.read1Bit() == 1)
				{
					featureEnabled[i][l] = 1;
					int bits_to_read = Consts.___003C_003ESEGMENTATION_FEATURE_BITS[l];
					int value = br.readNBit(bits_to_read);
					if (Consts.___003C_003ESEGMENTATION_FEATURE_SIGNED[l] == 1 && br.read1Bit() == 1)
					{
						value *= -1;
					}
					featureData[i][l] = value;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 154, 130, 104, 104, 104, 106, 104, 101, 175,
		99, 109, 106, 104, 143
	})]
	private void readTileInfo(BitReader br)
	{
		int minLog2TileCols = calc_min_log2_tile_cols();
		int maxLog2TileCols = calc_max_log2_tile_cols();
		for (tileColsLog2 = minLog2TileCols; tileColsLog2 < maxLog2TileCols; tileColsLog2++)
		{
			int increment_tile_cols_log2 = br.read1Bit();
			if (increment_tile_cols_log2 != 1)
			{
				break;
			}
		}
		tileRowsLog2 = br.read1Bit();
		if (tileRowsLog2 == 1)
		{
			int increment_tile_rows_log2 = br.read1Bit();
			tileRowsLog2 += increment_tile_rows_log2;
		}
	}

	[LineNumberTable(new byte[] { 158, 158, 130, 109, 99, 107, 103 })]
	internal virtual int calc_min_log2_tile_cols()
	{
		int sb64Cols = frameWidth + 63 >> 6;
		int minLog2;
		for (minLog2 = 0; 64 << minLog2 < sb64Cols; minLog2++)
		{
		}
		return minLog2;
	}

	[LineNumberTable(new byte[] { 158, 156, 130, 109, 99, 106, 103 })]
	internal virtual int calc_max_log2_tile_cols()
	{
		int sb64Cols = frameWidth + 63 >> 6;
		int maxLog2;
		for (maxLog2 = 1; sb64Cols >> maxLog2 >= 4; maxLog2++)
		{
		}
		return maxLog2 - 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 149, 66, 106, 139 })]
	private static int readProb(BitReader br)
	{
		if (br.read1Bit() == 1)
		{
			int result = br.readNBit(8);
			
			return result;
		}
		return 255;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 140, 130, 104, 101, 139 })]
	private static int readDeltaQ(BitReader br)
	{
		int delta_coded = br.read1Bit();
		if (delta_coded == 1)
		{
			int result = br.readNBitSigned(4);
			
			return result;
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 105, 66, 105, 138, 110, 106, 181 })]
	private void readTxMode(VPXBooleanDecoder boolDec)
	{
		if (lossless)
		{
			txMode = 0;
			return;
		}
		txMode = boolDec.decodeInt(2);
		if (txMode == 3)
		{
			txMode += boolDec.decodeInt(1);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 103, 162, 103, 103, 60, 39, 199, 103, 103,
		60, 39, 199, 105, 105, 63, 1, 41, 201
	})]
	private void readTxModeProbs(VPXBooleanDecoder boolDec)
	{
		for (int k = 0; k < 2; k++)
		{
			for (int n = 0; n < 1; n++)
			{
				tx8x8Probs[k][n] = diffUpdateProb(boolDec, tx8x8Probs[k][n]);
			}
		}
		for (int j = 0; j < 2; j++)
		{
			for (int m = 0; m < 2; m++)
			{
				tx16x16Probs[j][m] = diffUpdateProb(boolDec, tx16x16Probs[j][m]);
			}
		}
		for (int i = 0; i < 2; i++)
		{
			for (int l = 0; l < 3; l++)
			{
				tx32x32Probs[i][l] = diffUpdateProb(boolDec, tx32x32Probs[i][l]);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 87, 162, 110, 106, 104, 104, 106, 108, 108,
		107, 109, 105, 63, 21, 41, 12, 44, 44, 234,
		61, 234, 82
	})]
	private void readCoefProbs(VPXBooleanDecoder boolDec)
	{
		int maxTxSize = Consts.___003C_003Etx_mode_to_biggest_tx_size[txMode];
		for (int txSz = 0; txSz <= maxTxSize; txSz++)
		{
			int update_probs = boolDec.readBitEq();
			if (update_probs != 1)
			{
				continue;
			}
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 6; k++)
					{
						int maxL = ((k != 0) ? 6 : 3);
						for (int l = 0; l < maxL; l++)
						{
							for (int m = 0; m < 3; m++)
							{
								coefProbs[txSz][i][j][k][l][m] = diffUpdateProb(boolDec, coefProbs[txSz][i][j][k][l][m]);
							}
						}
					}
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 81, 98, 103, 56, 167 })]
	private void readSkipProb(VPXBooleanDecoder boolDec)
	{
		for (int i = 0; i < 3; i++)
		{
			skipProbs[i] = diffUpdateProb(boolDec, skipProbs[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 80, 162, 103, 103, 60, 39, 167 })]
	private void readInterModeProbs(VPXBooleanDecoder boolDec)
	{
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				interModeProbs[i][j] = diffUpdateProb(boolDec, interModeProbs[i][j]);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 78, 98, 103, 103, 60, 39, 167 })]
	private void readInterpFilterProbs(VPXBooleanDecoder boolDec)
	{
		for (int j = 0; j < 4; j++)
		{
			for (int i = 0; i < 2; i++)
			{
				interpFilterProbs[j][i] = diffUpdateProb(boolDec, interpFilterProbs[j][i]);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 77, 162, 103, 56, 135 })]
	private void readIsInterProbs(VPXBooleanDecoder boolDec)
	{
		for (int i = 0; i < 4; i++)
		{
			isInterProbs[i] = diffUpdateProb(boolDec, isInterProbs[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 75, 66, 99, 103, 115, 3, 167, 101, 104,
		100, 138, 104, 100, 138, 104, 135, 99, 136
	})]
	private void frameReferenceMode(VPXBooleanDecoder boolDec)
	{
		int compoundReferenceAllowed = 0;
		for (int i = 1; i < 3; i++)
		{
			if (this.m_refFrameSignBias[i] != this.m_refFrameSignBias[0])
			{
				compoundReferenceAllowed = 1;
			}
		}
		if (compoundReferenceAllowed == 1)
		{
			if (boolDec.readBitEq() == 0)
			{
				refMode = 0;
				return;
			}
			if (boolDec.readBitEq() == 0)
			{
				refMode = 1;
			}
			else
			{
				refMode = 2;
			}
			setupCompoundReferenceMode();
		}
		else
		{
			refMode = 0;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 70, 130, 106, 103, 56, 167, 106, 103, 124,
		28, 231, 69, 105, 103, 56, 167
	})]
	private void frameReferenceModeProbs(VPXBooleanDecoder boolDec)
	{
		if (refMode == 2)
		{
			for (int k = 0; k < 5; k++)
			{
				compModeProbs[k] = diffUpdateProb(boolDec, compModeProbs[k]);
			}
		}
		if (refMode != 1)
		{
			for (int j = 0; j < 5; j++)
			{
				singleRefProbs[j][0] = diffUpdateProb(boolDec, singleRefProbs[j][0]);
				singleRefProbs[j][1] = diffUpdateProb(boolDec, singleRefProbs[j][1]);
			}
		}
		if (refMode != 0)
		{
			for (int i = 0; i < 5; i++)
			{
				compRefProbs[i] = diffUpdateProb(boolDec, compRefProbs[i]);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 66, 162, 103, 104, 60, 39, 167 })]
	private void readYModeProbs(VPXBooleanDecoder boolDec)
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				yModeProbs[i][j] = diffUpdateProb(boolDec, yModeProbs[i][j]);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 64, 98, 104, 103, 60, 39, 167 })]
	private void readPartitionProbs(VPXBooleanDecoder boolDec)
	{
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				partitionProbs[i][j] = diffUpdateProb(boolDec, partitionProbs[i][j]);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 63, 162, 103, 56, 135, 106, 120, 104, 60,
		135, 120, 104, 60, 231, 59, 234, 72, 108, 105,
		105, 63, 7, 41, 169, 105, 63, 1, 233, 60,
		236, 71, 106, 105, 122, 26, 233, 69
	})]
	private void mvProbs(VPXBooleanDecoder boolDec)
	{
		for (int j2 = 0; j2 < 3; j2++)
		{
			mvJointProbs[j2] = updateMvProb(boolDec, mvJointProbs[j2]);
		}
		for (int k = 0; k < 2; k++)
		{
			mvSignProbs[k] = updateMvProb(boolDec, mvSignProbs[k]);
			for (int n = 0; n < 10; n++)
			{
				mvClassProbs[k][n] = updateMvProb(boolDec, mvClassProbs[k][n]);
			}
			mvClass0BitProbs[k] = updateMvProb(boolDec, mvClass0BitProbs[k]);
			for (int m = 0; m < 10; m++)
			{
				mvBitsProbs[k][m] = updateMvProb(boolDec, mvBitsProbs[k][m]);
			}
		}
		for (int j = 0; j < 2; j++)
		{
			for (int l = 0; l < 2; l++)
			{
				for (int k3 = 0; k3 < 3; k3++)
				{
					mvClass0FrProbs[j][l][k3] = updateMvProb(boolDec, mvClass0FrProbs[j][l][k3]);
				}
			}
			for (int k2 = 0; k2 < 3; k2++)
			{
				mvFrProbs[j][k2] = updateMvProb(boolDec, mvFrProbs[j][k2]);
			}
		}
		if (allowHighPrecisionMv == 1)
		{
			for (int i = 0; i < 2; i++)
			{
				mvClass0HpProb[i] = updateMvProb(boolDec, mvClass0HpProb[i]);
				mvHpProbs[i] = updateMvProb(boolDec, mvHpProbs[i]);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 99, 130, 109, 101, 105, 139 })]
	private int diffUpdateProb(VPXBooleanDecoder boolDec, int prob)
	{
		int update_prob = boolDec.readBit(252);
		if (update_prob == 1)
		{
			int deltaProb = decodeTermSubexp(boolDec);
			prob = invRemapProb(deltaProb, prob);
		}
		return prob;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 97, 162, 104, 100, 139, 104, 100, 140, 104,
		100, 140, 105, 102, 102, 104
	})]
	private int decodeTermSubexp(VPXBooleanDecoder boolDec)
	{
		if (boolDec.readBitEq() == 0)
		{
			int result = boolDec.decodeInt(4);
			
			return result;
		}
		if (boolDec.readBitEq() == 0)
		{
			return boolDec.decodeInt(4) + 16;
		}
		if (boolDec.readBitEq() == 0)
		{
			return boolDec.decodeInt(5) + 32;
		}
		int v = boolDec.decodeInt(7);
		if (v < 65)
		{
			return v + 64;
		}
		int bit = boolDec.readBitEq();
		return (v << 1) - 1 + bit;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 92, 162, 99, 99, 105, 101, 107, 142, 118 })]
	private int invRemapProb(int deltaProb, int prob)
	{
		int i = prob;
		int v = deltaProb;
		v = Consts.___003C_003EINV_REMAP_TABLE[v];
		i += -1;
		if (i << 1 <= 255)
		{
			return 1 + invRecenterNonneg(v, i);
		}
		return 255 - invRecenterNonneg(v, 254 - i);
	}

	[LineNumberTable(new byte[] { 158, 89, 162, 103, 99, 102, 105 })]
	private int invRecenterNonneg(int v, int m)
	{
		if (v > 2 * m)
		{
			return v;
		}
		if (((uint)v & (true ? 1u : 0u)) != 0)
		{
			return m - (v + 1 >> 1);
		}
		return m + (v >> 1);
	}

	[LineNumberTable(new byte[]
	{
		158, 54, 130, 115, 104, 104, 106, 115, 104, 104,
		138, 104, 104, 136
	})]
	private void setupCompoundReferenceMode()
	{
		if (this.m_refFrameSignBias[1] == this.m_refFrameSignBias[3])
		{
			compFixedRef = 2;
			compVarRef0 = 1;
			compVarRef1 = 3;
		}
		else if (this.m_refFrameSignBias[1] == this.m_refFrameSignBias[2])
		{
			compFixedRef = 3;
			compVarRef0 = 1;
			compVarRef1 = 2;
		}
		else
		{
			compFixedRef = 1;
			compVarRef0 = 3;
			compVarRef1 = 2;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 56, 98, 109, 101, 105, 136 })]
	private int updateMvProb(VPXBooleanDecoder boolDec, int prob)
	{
		int update_mv_prob = boolDec.readBit(252);
		if (update_mv_prob == 1)
		{
			int mv_prob = boolDec.decodeInt(7);
			prob = (mv_prob << 1) | 1;
		}
		return prob;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 233, 98, 103, 105, 110 })]
	public static DecodingContext createFromHeaders(ByteBuffer bb)
	{
		DecodingContext dc = new DecodingContext();
		int compressedHeaderSize = dc.readUncompressedHeader(bb);
		dc.readCompressedHeader(NIOUtils.read(bb, compressedHeaderSize));
		return dc;
	}

	[LineNumberTable(717)]
	public virtual bool isSegmentationEnabled()
	{
		return segmentationEnabled;
	}

	[LineNumberTable(721)]
	public virtual bool isUpdateSegmentMap()
	{
		return false;
	}

	[LineNumberTable(725)]
	public virtual bool isSegmentFeatureActive(int segmentId, int segLvlSkip)
	{
		return false;
	}

	[LineNumberTable(729)]
	public virtual bool isSegmentMapConditionalUpdate()
	{
		return false;
	}

	[LineNumberTable(733)]
	public virtual int getSegmentFeature(int segmentId, int segLvlRefFrame)
	{
		return 0;
	}

	[LineNumberTable(737)]
	public virtual int getCompFixedRef()
	{
		return 0;
	}

	[LineNumberTable(741)]
	public virtual int refFrameSignBias(int fixedRef)
	{
		return 0;
	}

	[LineNumberTable(745)]
	public virtual int getInterpFilter()
	{
		return interpFilter;
	}

	[LineNumberTable(749)]
	public virtual int getRefMode()
	{
		return refMode;
	}

	[LineNumberTable(753)]
	public virtual long[][] getLeftMVs()
	{
		return null;
	}

	[LineNumberTable(757)]
	public virtual long[][] getAboveMVs()
	{
		return null;
	}

	[LineNumberTable(761)]
	public virtual long[][] getAboveLeftMVs()
	{
		return null;
	}

	[LineNumberTable(765)]
	public virtual long[] getLeft4x4MVs()
	{
		return null;
	}

	[LineNumberTable(769)]
	public virtual long[] getAbove4x4MVs()
	{
		return null;
	}

	[LineNumberTable(773)]
	public virtual bool[] getAboveCompound()
	{
		return aboveCompound;
	}

	[LineNumberTable(777)]
	public virtual bool[] getLeftCompound()
	{
		return leftCompound;
	}

	[LineNumberTable(781)]
	public virtual bool isAllowHpMv()
	{
		return false;
	}

	[LineNumberTable(785)]
	public virtual bool isUsePrevFrameMvs()
	{
		return false;
	}

	[LineNumberTable(789)]
	public virtual long[][] getPrevFrameMv()
	{
		return null;
	}

	[LineNumberTable(793)]
	public virtual int getMiFrameWidth()
	{
		return frameWidth + 7 >> 3;
	}

	[LineNumberTable(797)]
	public virtual int getMiFrameHeight()
	{
		return frameHeight + 7 >> 3;
	}

	[LineNumberTable(801)]
	public virtual int[] getLeftInterpFilters()
	{
		return leftInterpFilters;
	}

	[LineNumberTable(805)]
	public virtual int[] getAboveInterpFilters()
	{
		return aboveInterpFilters;
	}

	[LineNumberTable(817)]
	public virtual int getCompVarRef(int i)
	{
		return 0;
	}

	[LineNumberTable(821)]
	public virtual int[] getAboveModes()
	{
		return aboveModes;
	}

	[LineNumberTable(825)]
	public virtual int[] getLeftModes()
	{
		return leftModes;
	}

	[LineNumberTable(829)]
	public virtual int getTxMode()
	{
		return txMode;
	}

	[LineNumberTable(833)]
	public virtual bool[] getAboveSegIdPredicted()
	{
		return null;
	}

	[LineNumberTable(837)]
	public virtual bool[] getLeftSegIdPredicted()
	{
		return null;
	}

	[LineNumberTable(841)]
	public virtual int[][] getPrevSegmentIds()
	{
		return null;
	}

	[LineNumberTable(845)]
	public virtual int getSubX()
	{
		return subsamplingX;
	}

	[LineNumberTable(849)]
	public virtual int getSubY()
	{
		return subsamplingY;
	}

	[LineNumberTable(853)]
	public virtual int getBitDepth()
	{
		return bitDepth;
	}

	[LineNumberTable(857)]
	public virtual int[][] getAboveNonzeroContext()
	{
		return aboveNonzeroContext;
	}

	[LineNumberTable(861)]
	public virtual int[][] getLeftNonzeroContext()
	{
		return leftNonzeroContext;
	}

	[LineNumberTable(873)]
	public virtual bool[] getLeftSkipped()
	{
		return leftSkipped;
	}

	[LineNumberTable(877)]
	public virtual bool[] getAboveSkipped()
	{
		return aboveSkipped;
	}

	[LineNumberTable(1394)]
	public virtual int getFrameContextIdx()
	{
		return frameContextIdx;
	}

	[LineNumberTable(1398)]
	public virtual int getTileColsLog2()
	{
		return tileColsLog2;
	}

	[LineNumberTable(1402)]
	public virtual int getTileRowsLog2()
	{
		return tileRowsLog2;
	}

	[LineNumberTable(1406)]
	public virtual int getFrameWidth()
	{
		return frameWidth;
	}

	[LineNumberTable(1410)]
	public virtual int getFrameHeight()
	{
		return frameHeight;
	}

	[LineNumberTable(1414)]
	public virtual int getBaseQIdx()
	{
		return baseQIdx;
	}

	[LineNumberTable(1418)]
	public virtual int getDeltaQYDc()
	{
		return deltaQYDc;
	}

	[LineNumberTable(1422)]
	public virtual int getDeltaQUvDc()
	{
		return deltaQUvDc;
	}

	[LineNumberTable(1426)]
	public virtual int getDeltaQUvAc()
	{
		return deltaQUvAc;
	}

	[LineNumberTable(1430)]
	public virtual int getFilterLevel()
	{
		return filterLevel;
	}

	[LineNumberTable(1434)]
	public virtual int getSharpnessLevel()
	{
		return sharpnessLevel;
	}

	[LineNumberTable(1438)]
	public virtual int[] getSkipProbs()
	{
		return skipProbs;
	}

	[LineNumberTable(1442)]
	public virtual int[][] getTx8x8Probs()
	{
		return tx8x8Probs;
	}

	[LineNumberTable(1446)]
	public virtual int[][] getTx16x16Probs()
	{
		return tx16x16Probs;
	}

	[LineNumberTable(1450)]
	public virtual int[][] getTx32x32Probs()
	{
		return tx32x32Probs;
	}

	[LineNumberTable(1454)]
	public virtual int[][][][][][] getCoefProbs()
	{
		return coefProbs;
	}

	[LineNumberTable(1458)]
	public virtual int[] getMvJointProbs()
	{
		return mvJointProbs;
	}

	[LineNumberTable(1462)]
	public virtual int[] getMvSignProb()
	{
		return mvSignProbs;
	}

	[LineNumberTable(1466)]
	public virtual int[][] getMvClassProbs()
	{
		return mvClassProbs;
	}

	[LineNumberTable(1470)]
	public virtual int[] getMvClass0BitProbs()
	{
		return mvClass0BitProbs;
	}

	[LineNumberTable(1474)]
	public virtual int[][] getMvBitsProb()
	{
		return mvBitsProbs;
	}

	[LineNumberTable(1478)]
	public virtual int[][][] getMvClass0FrProbs()
	{
		return mvClass0FrProbs;
	}

	[LineNumberTable(1482)]
	public virtual int[][] getMvFrProbs()
	{
		return mvFrProbs;
	}

	[LineNumberTable(1486)]
	public virtual int[] getMvClass0HpProbs()
	{
		return mvClass0HpProb;
	}

	[LineNumberTable(1490)]
	public virtual int[] getMvHpProbs()
	{
		return mvHpProbs;
	}

	[LineNumberTable(1494)]
	public virtual int[][] getInterModeProbs()
	{
		return interModeProbs;
	}

	[LineNumberTable(1498)]
	public virtual int[][] getInterpFilterProbs()
	{
		return interpFilterProbs;
	}

	[LineNumberTable(1502)]
	public virtual int[] getIsInterProbs()
	{
		return isInterProbs;
	}

	[LineNumberTable(1506)]
	public virtual int[][] getSingleRefProbs()
	{
		return singleRefProbs;
	}

	[LineNumberTable(1510)]
	public virtual int[][] getYModeProbs()
	{
		return yModeProbs;
	}

	[LineNumberTable(1518)]
	public virtual int[][] getUvModeProbs()
	{
		return uvModeProbs;
	}

	[LineNumberTable(1522)]
	public virtual int[] getCompRefProbs()
	{
		return compRefProbs;
	}

	[LineNumberTable(1526)]
	public virtual int[][][] getKfYModeProbs()
	{
		return ___003C_003EkfYmodeProbs;
	}

	[LineNumberTable(1530)]
	public virtual int[][] getKfUVModeProbs()
	{
		return ___003C_003EkfUvModeProbs;
	}

	[LineNumberTable(1534)]
	public virtual int[] getSegmentationTreeProbs()
	{
		return segmentationTreeProbs;
	}

	[LineNumberTable(1538)]
	public virtual int[] getSegmentationPredProbs()
	{
		return segmentationPredProbs;
	}

	[LineNumberTable(1542)]
	public virtual int[] getCompModeProb()
	{
		return compModeProbs;
	}

	[LineNumberTable(1546)]
	public virtual int[] getAboveRefs()
	{
		return aboveRefs;
	}

	[LineNumberTable(1550)]
	public virtual int[] getLeftRefs()
	{
		return leftRefs;
	}

	[LineNumberTable(1554)]
	public virtual int getMiTileStartCol()
	{
		return miTileStartCol;
	}

	[LineNumberTable(1558)]
	public virtual int[] getAboveTxSizes()
	{
		return aboveTxSizes;
	}

	[LineNumberTable(1562)]
	public virtual int[] getLeftTxSizes()
	{
		return leftTxSizes;
	}

	[LineNumberTable(new byte[]
	{
		159,
		113,
		162,
		159,
		2,
		127,
		9,
		127,
		22,
		127,
		30,
		byte.MaxValue,
		208,
		61,
		142,
		161,
		75,
		155,
		191,
		160,
		99,
		156,
		156,
		156,
		191,
		160,
		99,
		159,
		107,
		127,
		29,
		156,
		191,
		160,
		89,
		159,
		71,
		159,
		7,
		byte.MaxValue,
		161,
		78,
		87,
		byte.MaxValue,
		184,
		102,
		160,
		112,
		byte.MaxValue,
		162,
		41,
		75,
		byte.MaxValue,
		160,
		182,
		70,
		byte.MaxValue,
		162,
		27,
		76,
		191,
		94
	})]
	static DecodingContext()
	{
		defaultSkipProb = new int[3] { 192, 128, 64 };
		defaultTxProbs8x8 = new int[2][]
		{
			new int[1] { 100 },
			new int[1] { 66 }
		};
		defaultTxProbs16x16 = new int[2][]
		{
			new int[2] { 20, 152 },
			new int[2] { 15, 101 }
		};
		defaultTxProbs32x32 = new int[2][]
		{
			new int[3] { 3, 136, 37 },
			new int[3] { 5, 52, 13 }
		};
		___003C_003EdefaultCoefProbs = new int[4][][][][][]
		{
			new int[2][][][][]
			{
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 195, 29, 183 },
							new int[3] { 84, 49, 136 },
							new int[3] { 8, 42, 71 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 31, 107, 169 },
							new int[3] { 35, 99, 159 },
							new int[3] { 17, 82, 140 },
							new int[3] { 8, 66, 114 },
							new int[3] { 2, 44, 76 },
							new int[3] { 1, 19, 32 }
						},
						new int[6][]
						{
							new int[3] { 40, 132, 201 },
							new int[3] { 29, 114, 187 },
							new int[3] { 13, 91, 157 },
							new int[3] { 7, 75, 127 },
							new int[3] { 3, 58, 95 },
							new int[3] { 1, 28, 47 }
						},
						new int[6][]
						{
							new int[3] { 69, 142, 221 },
							new int[3] { 42, 122, 201 },
							new int[3] { 15, 91, 159 },
							new int[3] { 6, 67, 121 },
							new int[3] { 1, 42, 77 },
							new int[3] { 1, 17, 31 }
						},
						new int[6][]
						{
							new int[3] { 102, 148, 228 },
							new int[3] { 67, 117, 204 },
							new int[3] { 17, 82, 154 },
							new int[3] { 6, 59, 114 },
							new int[3] { 2, 39, 75 },
							new int[3] { 1, 15, 29 }
						},
						new int[6][]
						{
							new int[3] { 156, 57, 233 },
							new int[3] { 119, 57, 212 },
							new int[3] { 58, 48, 163 },
							new int[3] { 29, 40, 124 },
							new int[3] { 12, 30, 81 },
							new int[3] { 3, 12, 31 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 191, 107, 226 },
							new int[3] { 124, 117, 204 },
							new int[3] { 25, 99, 155 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 29, 148, 210 },
							new int[3] { 37, 126, 194 },
							new int[3] { 8, 93, 157 },
							new int[3] { 2, 68, 118 },
							new int[3] { 1, 39, 69 },
							new int[3] { 1, 17, 33 }
						},
						new int[6][]
						{
							new int[3] { 41, 151, 213 },
							new int[3] { 27, 123, 193 },
							new int[3] { 3, 82, 144 },
							new int[3] { 1, 58, 105 },
							new int[3] { 1, 32, 60 },
							new int[3] { 1, 13, 26 }
						},
						new int[6][]
						{
							new int[3] { 59, 159, 220 },
							new int[3] { 23, 126, 198 },
							new int[3] { 4, 88, 151 },
							new int[3] { 1, 66, 114 },
							new int[3] { 1, 38, 71 },
							new int[3] { 1, 18, 34 }
						},
						new int[6][]
						{
							new int[3] { 114, 136, 232 },
							new int[3] { 51, 114, 207 },
							new int[3] { 11, 83, 155 },
							new int[3] { 3, 56, 105 },
							new int[3] { 1, 33, 65 },
							new int[3] { 1, 17, 34 }
						},
						new int[6][]
						{
							new int[3] { 149, 65, 234 },
							new int[3] { 121, 57, 215 },
							new int[3] { 61, 49, 166 },
							new int[3] { 28, 36, 114 },
							new int[3] { 12, 25, 76 },
							new int[3] { 3, 16, 42 }
						}
					}
				},
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 214, 49, 220 },
							new int[3] { 132, 63, 188 },
							new int[3] { 42, 65, 137 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 85, 137, 221 },
							new int[3] { 104, 131, 216 },
							new int[3] { 49, 111, 192 },
							new int[3] { 21, 87, 155 },
							new int[3] { 2, 49, 87 },
							new int[3] { 1, 16, 28 }
						},
						new int[6][]
						{
							new int[3] { 89, 163, 230 },
							new int[3] { 90, 137, 220 },
							new int[3] { 29, 100, 183 },
							new int[3] { 10, 70, 135 },
							new int[3] { 2, 42, 81 },
							new int[3] { 1, 17, 33 }
						},
						new int[6][]
						{
							new int[3] { 108, 167, 237 },
							new int[3] { 55, 133, 222 },
							new int[3] { 15, 97, 179 },
							new int[3] { 4, 72, 135 },
							new int[3] { 1, 45, 85 },
							new int[3] { 1, 19, 38 }
						},
						new int[6][]
						{
							new int[3] { 124, 146, 240 },
							new int[3] { 66, 124, 224 },
							new int[3] { 17, 88, 175 },
							new int[3] { 4, 58, 122 },
							new int[3] { 1, 36, 75 },
							new int[3] { 1, 18, 37 }
						},
						new int[6][]
						{
							new int[3] { 141, 79, 241 },
							new int[3] { 126, 70, 227 },
							new int[3] { 66, 58, 182 },
							new int[3] { 30, 44, 136 },
							new int[3] { 12, 34, 96 },
							new int[3] { 2, 20, 47 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 229, 99, 249 },
							new int[3] { 143, 111, 235 },
							new int[3] { 46, 109, 192 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 82, 158, 236 },
							new int[3] { 94, 146, 224 },
							new int[3] { 25, 117, 191 },
							new int[3] { 9, 87, 149 },
							new int[3] { 3, 56, 99 },
							new int[3] { 1, 33, 57 }
						},
						new int[6][]
						{
							new int[3] { 83, 167, 237 },
							new int[3] { 68, 145, 222 },
							new int[3] { 10, 103, 177 },
							new int[3] { 2, 72, 131 },
							new int[3] { 1, 41, 79 },
							new int[3] { 1, 20, 39 }
						},
						new int[6][]
						{
							new int[3] { 99, 167, 239 },
							new int[3] { 47, 141, 224 },
							new int[3] { 10, 104, 178 },
							new int[3] { 2, 73, 133 },
							new int[3] { 1, 44, 85 },
							new int[3] { 1, 22, 47 }
						},
						new int[6][]
						{
							new int[3] { 127, 145, 243 },
							new int[3] { 71, 129, 228 },
							new int[3] { 17, 93, 177 },
							new int[3] { 3, 61, 124 },
							new int[3] { 1, 41, 84 },
							new int[3] { 1, 21, 52 }
						},
						new int[6][]
						{
							new int[3] { 157, 78, 244 },
							new int[3] { 140, 72, 231 },
							new int[3] { 69, 58, 184 },
							new int[3] { 31, 44, 137 },
							new int[3] { 14, 38, 105 },
							new int[3] { 8, 23, 61 }
						}
					}
				}
			},
			new int[2][][][][]
			{
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 125, 34, 187 },
							new int[3] { 52, 41, 133 },
							new int[3] { 6, 31, 56 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 37, 109, 153 },
							new int[3] { 51, 102, 147 },
							new int[3] { 23, 87, 128 },
							new int[3] { 8, 67, 101 },
							new int[3] { 1, 41, 63 },
							new int[3] { 1, 19, 29 }
						},
						new int[6][]
						{
							new int[3] { 31, 154, 185 },
							new int[3] { 17, 127, 175 },
							new int[3] { 6, 96, 145 },
							new int[3] { 2, 73, 114 },
							new int[3] { 1, 51, 82 },
							new int[3] { 1, 28, 45 }
						},
						new int[6][]
						{
							new int[3] { 23, 163, 200 },
							new int[3] { 10, 131, 185 },
							new int[3] { 2, 93, 148 },
							new int[3] { 1, 67, 111 },
							new int[3] { 1, 41, 69 },
							new int[3] { 1, 14, 24 }
						},
						new int[6][]
						{
							new int[3] { 29, 176, 217 },
							new int[3] { 12, 145, 201 },
							new int[3] { 3, 101, 156 },
							new int[3] { 1, 69, 111 },
							new int[3] { 1, 39, 63 },
							new int[3] { 1, 14, 23 }
						},
						new int[6][]
						{
							new int[3] { 57, 192, 233 },
							new int[3] { 25, 154, 215 },
							new int[3] { 6, 109, 167 },
							new int[3] { 3, 78, 118 },
							new int[3] { 1, 48, 69 },
							new int[3] { 1, 21, 29 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 202, 105, 245 },
							new int[3] { 108, 106, 216 },
							new int[3] { 18, 90, 144 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 33, 172, 219 },
							new int[3] { 64, 149, 206 },
							new int[3] { 14, 117, 177 },
							new int[3] { 5, 90, 141 },
							new int[3] { 2, 61, 95 },
							new int[3] { 1, 37, 57 }
						},
						new int[6][]
						{
							new int[3] { 33, 179, 220 },
							new int[3] { 11, 140, 198 },
							new int[3] { 1, 89, 148 },
							new int[3] { 1, 60, 104 },
							new int[3] { 1, 33, 57 },
							new int[3] { 1, 12, 21 }
						},
						new int[6][]
						{
							new int[3] { 30, 181, 221 },
							new int[3] { 8, 141, 198 },
							new int[3] { 1, 87, 145 },
							new int[3] { 1, 58, 100 },
							new int[3] { 1, 31, 55 },
							new int[3] { 1, 12, 20 }
						},
						new int[6][]
						{
							new int[3] { 32, 186, 224 },
							new int[3] { 7, 142, 198 },
							new int[3] { 1, 86, 143 },
							new int[3] { 1, 58, 100 },
							new int[3] { 1, 31, 55 },
							new int[3] { 1, 12, 22 }
						},
						new int[6][]
						{
							new int[3] { 57, 192, 227 },
							new int[3] { 20, 143, 204 },
							new int[3] { 3, 96, 154 },
							new int[3] { 1, 68, 112 },
							new int[3] { 1, 42, 69 },
							new int[3] { 1, 19, 32 }
						}
					}
				},
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 212, 35, 215 },
							new int[3] { 113, 47, 169 },
							new int[3] { 29, 48, 105 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 74, 129, 203 },
							new int[3] { 106, 120, 203 },
							new int[3] { 49, 107, 178 },
							new int[3] { 19, 84, 144 },
							new int[3] { 4, 50, 84 },
							new int[3] { 1, 15, 25 }
						},
						new int[6][]
						{
							new int[3] { 71, 172, 217 },
							new int[3] { 44, 141, 209 },
							new int[3] { 15, 102, 173 },
							new int[3] { 6, 76, 133 },
							new int[3] { 2, 51, 89 },
							new int[3] { 1, 24, 42 }
						},
						new int[6][]
						{
							new int[3] { 64, 185, 231 },
							new int[3] { 31, 148, 216 },
							new int[3] { 8, 103, 175 },
							new int[3] { 3, 74, 131 },
							new int[3] { 1, 46, 81 },
							new int[3] { 1, 18, 30 }
						},
						new int[6][]
						{
							new int[3] { 65, 196, 235 },
							new int[3] { 25, 157, 221 },
							new int[3] { 5, 105, 174 },
							new int[3] { 1, 67, 120 },
							new int[3] { 1, 38, 69 },
							new int[3] { 1, 15, 30 }
						},
						new int[6][]
						{
							new int[3] { 65, 204, 238 },
							new int[3] { 30, 156, 224 },
							new int[3] { 7, 107, 177 },
							new int[3] { 2, 70, 124 },
							new int[3] { 1, 42, 73 },
							new int[3] { 1, 18, 34 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 225, 86, 251 },
							new int[3] { 144, 104, 235 },
							new int[3] { 42, 99, 181 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 85, 175, 239 },
							new int[3] { 112, 165, 229 },
							new int[3] { 29, 136, 200 },
							new int[3] { 12, 103, 162 },
							new int[3] { 6, 77, 123 },
							new int[3] { 2, 53, 84 }
						},
						new int[6][]
						{
							new int[3] { 75, 183, 239 },
							new int[3] { 30, 155, 221 },
							new int[3] { 3, 106, 171 },
							new int[3] { 1, 74, 128 },
							new int[3] { 1, 44, 76 },
							new int[3] { 1, 17, 28 }
						},
						new int[6][]
						{
							new int[3] { 73, 185, 240 },
							new int[3] { 27, 159, 222 },
							new int[3] { 2, 107, 172 },
							new int[3] { 1, 75, 127 },
							new int[3] { 1, 42, 73 },
							new int[3] { 1, 17, 29 }
						},
						new int[6][]
						{
							new int[3] { 62, 190, 238 },
							new int[3] { 21, 159, 222 },
							new int[3] { 2, 107, 172 },
							new int[3] { 1, 72, 122 },
							new int[3] { 1, 40, 71 },
							new int[3] { 1, 18, 32 }
						},
						new int[6][]
						{
							new int[3] { 61, 199, 240 },
							new int[3] { 27, 161, 226 },
							new int[3] { 4, 113, 180 },
							new int[3] { 1, 76, 129 },
							new int[3] { 1, 46, 80 },
							new int[3] { 1, 23, 41 }
						}
					}
				}
			},
			new int[2][][][][]
			{
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 7, 27, 153 },
							new int[3] { 5, 30, 95 },
							new int[3] { 1, 16, 30 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 50, 75, 127 },
							new int[3] { 57, 75, 124 },
							new int[3] { 27, 67, 108 },
							new int[3] { 10, 54, 86 },
							new int[3] { 1, 33, 52 },
							new int[3] { 1, 12, 18 }
						},
						new int[6][]
						{
							new int[3] { 43, 125, 151 },
							new int[3] { 26, 108, 148 },
							new int[3] { 7, 83, 122 },
							new int[3] { 2, 59, 89 },
							new int[3] { 1, 38, 60 },
							new int[3] { 1, 17, 27 }
						},
						new int[6][]
						{
							new int[3] { 23, 144, 163 },
							new int[3] { 13, 112, 154 },
							new int[3] { 2, 75, 117 },
							new int[3] { 1, 50, 81 },
							new int[3] { 1, 31, 51 },
							new int[3] { 1, 14, 23 }
						},
						new int[6][]
						{
							new int[3] { 18, 162, 185 },
							new int[3] { 6, 123, 171 },
							new int[3] { 1, 78, 125 },
							new int[3] { 1, 51, 86 },
							new int[3] { 1, 31, 54 },
							new int[3] { 1, 14, 23 }
						},
						new int[6][]
						{
							new int[3] { 15, 199, 227 },
							new int[3] { 3, 150, 204 },
							new int[3] { 1, 91, 146 },
							new int[3] { 1, 55, 95 },
							new int[3] { 1, 30, 53 },
							new int[3] { 1, 11, 20 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 19, 55, 240 },
							new int[3] { 19, 59, 196 },
							new int[3] { 3, 52, 105 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 41, 166, 207 },
							new int[3] { 104, 153, 199 },
							new int[3] { 31, 123, 181 },
							new int[3] { 14, 101, 152 },
							new int[3] { 5, 72, 106 },
							new int[3] { 1, 36, 52 }
						},
						new int[6][]
						{
							new int[3] { 35, 176, 211 },
							new int[3] { 12, 131, 190 },
							new int[3] { 2, 88, 144 },
							new int[3] { 1, 60, 101 },
							new int[3] { 1, 36, 60 },
							new int[3] { 1, 16, 28 }
						},
						new int[6][]
						{
							new int[3] { 28, 183, 213 },
							new int[3] { 8, 134, 191 },
							new int[3] { 1, 86, 142 },
							new int[3] { 1, 56, 96 },
							new int[3] { 1, 30, 53 },
							new int[3] { 1, 12, 20 }
						},
						new int[6][]
						{
							new int[3] { 20, 190, 215 },
							new int[3] { 4, 135, 192 },
							new int[3] { 1, 84, 139 },
							new int[3] { 1, 53, 91 },
							new int[3] { 1, 28, 49 },
							new int[3] { 1, 11, 20 }
						},
						new int[6][]
						{
							new int[3] { 13, 196, 216 },
							new int[3] { 2, 137, 192 },
							new int[3] { 1, 86, 143 },
							new int[3] { 1, 57, 99 },
							new int[3] { 1, 32, 56 },
							new int[3] { 1, 13, 24 }
						}
					}
				},
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 211, 29, 217 },
							new int[3] { 96, 47, 156 },
							new int[3] { 22, 43, 87 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 78, 120, 193 },
							new int[3] { 111, 116, 186 },
							new int[3] { 46, 102, 164 },
							new int[3] { 15, 80, 128 },
							new int[3] { 2, 49, 76 },
							new int[3] { 1, 18, 28 }
						},
						new int[6][]
						{
							new int[3] { 71, 161, 203 },
							new int[3] { 42, 132, 192 },
							new int[3] { 10, 98, 150 },
							new int[3] { 3, 69, 109 },
							new int[3] { 1, 44, 70 },
							new int[3] { 1, 18, 29 }
						},
						new int[6][]
						{
							new int[3] { 57, 186, 211 },
							new int[3] { 30, 140, 196 },
							new int[3] { 4, 93, 146 },
							new int[3] { 1, 62, 102 },
							new int[3] { 1, 38, 65 },
							new int[3] { 1, 16, 27 }
						},
						new int[6][]
						{
							new int[3] { 47, 199, 217 },
							new int[3] { 14, 145, 196 },
							new int[3] { 1, 88, 142 },
							new int[3] { 1, 57, 98 },
							new int[3] { 1, 36, 62 },
							new int[3] { 1, 15, 26 }
						},
						new int[6][]
						{
							new int[3] { 26, 219, 229 },
							new int[3] { 5, 155, 207 },
							new int[3] { 1, 94, 151 },
							new int[3] { 1, 60, 104 },
							new int[3] { 1, 36, 62 },
							new int[3] { 1, 16, 28 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 233, 29, 248 },
							new int[3] { 146, 47, 220 },
							new int[3] { 43, 52, 140 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 100, 163, 232 },
							new int[3] { 179, 161, 222 },
							new int[3] { 63, 142, 204 },
							new int[3] { 37, 113, 174 },
							new int[3] { 26, 89, 137 },
							new int[3] { 18, 68, 97 }
						},
						new int[6][]
						{
							new int[3] { 85, 181, 230 },
							new int[3] { 32, 146, 209 },
							new int[3] { 7, 100, 164 },
							new int[3] { 3, 71, 121 },
							new int[3] { 1, 45, 77 },
							new int[3] { 1, 18, 30 }
						},
						new int[6][]
						{
							new int[3] { 65, 187, 230 },
							new int[3] { 20, 148, 207 },
							new int[3] { 2, 97, 159 },
							new int[3] { 1, 68, 116 },
							new int[3] { 1, 40, 70 },
							new int[3] { 1, 14, 29 }
						},
						new int[6][]
						{
							new int[3] { 40, 194, 227 },
							new int[3] { 8, 147, 204 },
							new int[3] { 1, 94, 155 },
							new int[3] { 1, 65, 112 },
							new int[3] { 1, 39, 66 },
							new int[3] { 1, 14, 26 }
						},
						new int[6][]
						{
							new int[3] { 16, 208, 228 },
							new int[3] { 3, 151, 207 },
							new int[3] { 1, 98, 160 },
							new int[3] { 1, 67, 117 },
							new int[3] { 1, 41, 74 },
							new int[3] { 1, 17, 31 }
						}
					}
				}
			},
			new int[2][][][][]
			{
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 17, 38, 140 },
							new int[3] { 7, 34, 80 },
							new int[3] { 1, 17, 29 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 37, 75, 128 },
							new int[3] { 41, 76, 128 },
							new int[3] { 26, 66, 116 },
							new int[3] { 12, 52, 94 },
							new int[3] { 2, 32, 55 },
							new int[3] { 1, 10, 16 }
						},
						new int[6][]
						{
							new int[3] { 50, 127, 154 },
							new int[3] { 37, 109, 152 },
							new int[3] { 16, 82, 121 },
							new int[3] { 5, 59, 85 },
							new int[3] { 1, 35, 54 },
							new int[3] { 1, 13, 20 }
						},
						new int[6][]
						{
							new int[3] { 40, 142, 167 },
							new int[3] { 17, 110, 157 },
							new int[3] { 2, 71, 112 },
							new int[3] { 1, 44, 72 },
							new int[3] { 1, 27, 45 },
							new int[3] { 1, 11, 17 }
						},
						new int[6][]
						{
							new int[3] { 30, 175, 188 },
							new int[3] { 9, 124, 169 },
							new int[3] { 1, 74, 116 },
							new int[3] { 1, 48, 78 },
							new int[3] { 1, 30, 49 },
							new int[3] { 1, 11, 18 }
						},
						new int[6][]
						{
							new int[3] { 10, 222, 223 },
							new int[3] { 2, 150, 194 },
							new int[3] { 1, 83, 128 },
							new int[3] { 1, 48, 79 },
							new int[3] { 1, 27, 45 },
							new int[3] { 1, 11, 17 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 36, 41, 235 },
							new int[3] { 29, 36, 193 },
							new int[3] { 10, 27, 111 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 85, 165, 222 },
							new int[3] { 177, 162, 215 },
							new int[3] { 110, 135, 195 },
							new int[3] { 57, 113, 168 },
							new int[3] { 23, 83, 120 },
							new int[3] { 10, 49, 61 }
						},
						new int[6][]
						{
							new int[3] { 85, 190, 223 },
							new int[3] { 36, 139, 200 },
							new int[3] { 5, 90, 146 },
							new int[3] { 1, 60, 103 },
							new int[3] { 1, 38, 65 },
							new int[3] { 1, 18, 30 }
						},
						new int[6][]
						{
							new int[3] { 72, 202, 223 },
							new int[3] { 23, 141, 199 },
							new int[3] { 2, 86, 140 },
							new int[3] { 1, 56, 97 },
							new int[3] { 1, 36, 61 },
							new int[3] { 1, 16, 27 }
						},
						new int[6][]
						{
							new int[3] { 55, 218, 225 },
							new int[3] { 13, 145, 200 },
							new int[3] { 1, 86, 141 },
							new int[3] { 1, 57, 99 },
							new int[3] { 1, 35, 61 },
							new int[3] { 1, 13, 22 }
						},
						new int[6][]
						{
							new int[3] { 15, 235, 212 },
							new int[3] { 1, 132, 184 },
							new int[3] { 1, 84, 139 },
							new int[3] { 1, 57, 97 },
							new int[3] { 1, 34, 56 },
							new int[3] { 1, 14, 23 }
						}
					}
				},
				new int[2][][][]
				{
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 181, 21, 201 },
							new int[3] { 61, 37, 123 },
							new int[3] { 10, 38, 71 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 47, 106, 172 },
							new int[3] { 95, 104, 173 },
							new int[3] { 42, 93, 159 },
							new int[3] { 18, 77, 131 },
							new int[3] { 4, 50, 81 },
							new int[3] { 1, 17, 23 }
						},
						new int[6][]
						{
							new int[3] { 62, 147, 199 },
							new int[3] { 44, 130, 189 },
							new int[3] { 28, 102, 154 },
							new int[3] { 18, 75, 115 },
							new int[3] { 2, 44, 65 },
							new int[3] { 1, 12, 19 }
						},
						new int[6][]
						{
							new int[3] { 55, 153, 210 },
							new int[3] { 24, 130, 194 },
							new int[3] { 3, 93, 146 },
							new int[3] { 1, 61, 97 },
							new int[3] { 1, 31, 50 },
							new int[3] { 1, 10, 16 }
						},
						new int[6][]
						{
							new int[3] { 49, 186, 223 },
							new int[3] { 17, 148, 204 },
							new int[3] { 1, 96, 142 },
							new int[3] { 1, 53, 83 },
							new int[3] { 1, 26, 44 },
							new int[3] { 1, 11, 17 }
						},
						new int[6][]
						{
							new int[3] { 13, 217, 212 },
							new int[3] { 2, 136, 180 },
							new int[3] { 1, 78, 124 },
							new int[3] { 1, 50, 83 },
							new int[3] { 1, 29, 49 },
							new int[3] { 1, 14, 23 }
						}
					},
					new int[6][][]
					{
						new int[6][]
						{
							new int[3] { 197, 13, 247 },
							new int[3] { 82, 17, 222 },
							new int[3] { 25, 17, 162 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 },
							new int[3] { 0, 0, 0 }
						},
						new int[6][]
						{
							new int[3] { 126, 186, 247 },
							new int[3] { 234, 191, 243 },
							new int[3] { 176, 177, 234 },
							new int[3] { 104, 158, 220 },
							new int[3] { 66, 128, 186 },
							new int[3] { 55, 90, 137 }
						},
						new int[6][]
						{
							new int[3] { 111, 197, 242 },
							new int[3] { 46, 158, 219 },
							new int[3] { 9, 104, 171 },
							new int[3] { 2, 65, 125 },
							new int[3] { 1, 44, 80 },
							new int[3] { 1, 17, 91 }
						},
						new int[6][]
						{
							new int[3] { 104, 208, 245 },
							new int[3] { 39, 168, 224 },
							new int[3] { 3, 109, 162 },
							new int[3] { 1, 79, 124 },
							new int[3] { 1, 50, 102 },
							new int[3] { 1, 43, 102 }
						},
						new int[6][]
						{
							new int[3] { 84, 220, 246 },
							new int[3] { 31, 177, 231 },
							new int[3] { 2, 115, 180 },
							new int[3] { 1, 79, 134 },
							new int[3] { 1, 55, 77 },
							new int[3] { 1, 60, 79 }
						},
						new int[6][]
						{
							new int[3] { 43, 243, 240 },
							new int[3] { 8, 180, 217 },
							new int[3] { 1, 115, 166 },
							new int[3] { 1, 84, 121 },
							new int[3] { 1, 51, 67 },
							new int[3] { 1, 16, 6 }
						}
					}
				}
			}
		};
		___003C_003EdefaultMvJointProbs = new int[3] { 32, 64, 96 };
		___003C_003EdefaultMvBitsProb = new int[2][]
		{
			new int[10] { 136, 140, 148, 160, 176, 192, 224, 234, 234, 240 },
			new int[10] { 136, 140, 148, 160, 176, 192, 224, 234, 234, 240 }
		};
		___003C_003EdefaultMvClass0BitProb = new int[2] { 216, 208 };
		___003C_003EdefaultMvClass0HpProb = new int[2] { 160, 160 };
		___003C_003EdefaultMvSignProb = new int[2] { 128, 128 };
		___003C_003EdefaultMvClassProbs = new int[2][]
		{
			new int[10] { 224, 144, 192, 168, 192, 176, 192, 198, 198, 245 },
			new int[10] { 216, 128, 176, 160, 176, 176, 192, 198, 198, 208 }
		};
		___003C_003EdefaultMvClass0FrProbs = new int[2][][]
		{
			new int[2][]
			{
				new int[3] { 128, 128, 64 },
				new int[3] { 96, 112, 64 }
			},
			new int[2][]
			{
				new int[3] { 128, 128, 64 },
				new int[3] { 96, 112, 64 }
			}
		};
		___003C_003EdefaultMvFrProbs = new int[2][]
		{
			new int[3] { 64, 96, 64 },
			new int[3] { 64, 96, 64 }
		};
		___003C_003EdefaultMvHpProb = new int[2] { 128, 128 };
		___003C_003EdefaultInterModeProbs = new int[7][]
		{
			new int[3] { 2, 173, 34 },
			new int[3] { 7, 145, 85 },
			new int[3] { 7, 166, 63 },
			new int[3] { 7, 94, 66 },
			new int[3] { 8, 64, 46 },
			new int[3] { 17, 81, 31 },
			new int[3] { 25, 29, 30 }
		};
		___003C_003EdefaultInterpFilterProbs = new int[4][]
		{
			new int[2] { 235, 162 },
			new int[2] { 36, 255 },
			new int[2] { 34, 3 },
			new int[2] { 149, 144 }
		};
		___003C_003EdefaultIsInterProbs = new int[4] { 9, 102, 187, 225 };
		defaultPartitionProbs = new int[16][]
		{
			new int[3] { 199, 122, 141 },
			new int[3] { 147, 63, 159 },
			new int[3] { 148, 133, 118 },
			new int[3] { 121, 104, 114 },
			new int[3] { 174, 73, 87 },
			new int[3] { 92, 41, 83 },
			new int[3] { 82, 99, 50 },
			new int[3] { 53, 39, 39 },
			new int[3] { 177, 58, 59 },
			new int[3] { 68, 26, 63 },
			new int[3] { 52, 79, 25 },
			new int[3] { 17, 14, 12 },
			new int[3] { 222, 34, 30 },
			new int[3] { 72, 16, 44 },
			new int[3] { 58, 32, 12 },
			new int[3] { 10, 7, 6 }
		};
		___003C_003EkfYmodeProbs = new int[10][][]
		{
			new int[10][]
			{
				new int[9] { 137, 30, 42, 148, 151, 207, 70, 52, 91 },
				new int[9] { 92, 45, 102, 136, 116, 180, 74, 90, 100 },
				new int[9] { 73, 32, 19, 187, 222, 215, 46, 34, 100 },
				new int[9] { 91, 30, 32, 116, 121, 186, 93, 86, 94 },
				new int[9] { 72, 35, 36, 149, 68, 206, 68, 63, 105 },
				new int[9] { 73, 31, 28, 138, 57, 124, 55, 122, 151 },
				new int[9] { 67, 23, 21, 140, 126, 197, 40, 37, 171 },
				new int[9] { 86, 27, 28, 128, 154, 212, 45, 43, 53 },
				new int[9] { 74, 32, 27, 107, 86, 160, 63, 134, 102 },
				new int[9] { 59, 67, 44, 140, 161, 202, 78, 67, 119 }
			},
			new int[10][]
			{
				new int[9] { 63, 36, 126, 146, 123, 158, 60, 90, 96 },
				new int[9] { 43, 46, 168, 134, 107, 128, 69, 142, 92 },
				new int[9] { 44, 29, 68, 159, 201, 177, 50, 57, 77 },
				new int[9] { 58, 38, 76, 114, 97, 172, 78, 133, 92 },
				new int[9] { 46, 41, 76, 140, 63, 184, 69, 112, 57 },
				new int[9] { 38, 32, 85, 140, 46, 112, 54, 151, 133 },
				new int[9] { 39, 27, 61, 131, 110, 175, 44, 75, 136 },
				new int[9] { 52, 30, 74, 113, 130, 175, 51, 64, 58 },
				new int[9] { 47, 35, 80, 100, 74, 143, 64, 163, 74 },
				new int[9] { 36, 61, 116, 114, 128, 162, 80, 125, 82 }
			},
			new int[10][]
			{
				new int[9] { 82, 26, 26, 171, 208, 204, 44, 32, 105 },
				new int[9] { 55, 44, 68, 166, 179, 192, 57, 57, 108 },
				new int[9] { 42, 26, 11, 199, 241, 228, 23, 15, 85 },
				new int[9] { 68, 42, 19, 131, 160, 199, 55, 52, 83 },
				new int[9] { 58, 50, 25, 139, 115, 232, 39, 52, 118 },
				new int[9] { 50, 35, 33, 153, 104, 162, 64, 59, 131 },
				new int[9] { 44, 24, 16, 150, 177, 202, 33, 19, 156 },
				new int[9] { 55, 27, 12, 153, 203, 218, 26, 27, 49 },
				new int[9] { 53, 49, 21, 110, 116, 168, 59, 80, 76 },
				new int[9] { 38, 72, 19, 168, 203, 212, 50, 50, 107 }
			},
			new int[10][]
			{
				new int[9] { 103, 26, 36, 129, 132, 201, 83, 80, 93 },
				new int[9] { 59, 38, 83, 112, 103, 162, 98, 136, 90 },
				new int[9] { 62, 30, 23, 158, 200, 207, 59, 57, 50 },
				new int[9] { 67, 30, 29, 84, 86, 191, 102, 91, 59 },
				new int[9] { 60, 32, 33, 112, 71, 220, 64, 89, 104 },
				new int[9] { 53, 26, 34, 130, 56, 149, 84, 120, 103 },
				new int[9] { 53, 21, 23, 133, 109, 210, 56, 77, 172 },
				new int[9] { 77, 19, 29, 112, 142, 228, 55, 66, 36 },
				new int[9] { 61, 29, 29, 93, 97, 165, 83, 175, 162 },
				new int[9] { 47, 47, 43, 114, 137, 181, 100, 99, 95 }
			},
			new int[10][]
			{
				new int[9] { 69, 23, 29, 128, 83, 199, 46, 44, 101 },
				new int[9] { 53, 40, 55, 139, 69, 183, 61, 80, 110 },
				new int[9] { 40, 29, 19, 161, 180, 207, 43, 24, 91 },
				new int[9] { 60, 34, 19, 105, 61, 198, 53, 64, 89 },
				new int[9] { 52, 31, 22, 158, 40, 209, 58, 62, 89 },
				new int[9] { 44, 31, 29, 147, 46, 158, 56, 102, 198 },
				new int[9] { 35, 19, 12, 135, 87, 209, 41, 45, 167 },
				new int[9] { 55, 25, 21, 118, 95, 215, 38, 39, 66 },
				new int[9] { 51, 38, 25, 113, 58, 164, 70, 93, 97 },
				new int[9] { 47, 54, 34, 146, 108, 203, 72, 103, 151 }
			},
			new int[10][]
			{
				new int[9] { 64, 19, 37, 156, 66, 138, 49, 95, 133 },
				new int[9] { 46, 27, 80, 150, 55, 124, 55, 121, 135 },
				new int[9] { 36, 23, 27, 165, 149, 166, 54, 64, 118 },
				new int[9] { 53, 21, 36, 131, 63, 163, 60, 109, 81 },
				new int[9] { 40, 26, 35, 154, 40, 185, 51, 97, 123 },
				new int[9] { 35, 19, 34, 179, 19, 97, 48, 129, 124 },
				new int[9] { 36, 20, 26, 136, 62, 164, 33, 77, 154 },
				new int[9] { 45, 18, 32, 130, 90, 157, 40, 79, 91 },
				new int[9] { 45, 26, 28, 129, 45, 129, 49, 147, 123 },
				new int[9] { 38, 44, 51, 136, 74, 162, 57, 97, 121 }
			},
			new int[10][]
			{
				new int[9] { 75, 17, 22, 136, 138, 185, 32, 34, 166 },
				new int[9] { 56, 39, 58, 133, 117, 173, 48, 53, 187 },
				new int[9] { 35, 21, 12, 161, 212, 207, 20, 23, 145 },
				new int[9] { 56, 29, 19, 117, 109, 181, 55, 68, 112 },
				new int[9] { 47, 29, 17, 153, 64, 220, 59, 51, 114 },
				new int[9] { 46, 16, 24, 136, 76, 147, 41, 64, 172 },
				new int[9] { 34, 17, 11, 108, 152, 187, 13, 15, 209 },
				new int[9] { 51, 24, 14, 115, 133, 209, 32, 26, 104 },
				new int[9] { 55, 30, 18, 122, 79, 179, 44, 88, 116 },
				new int[9] { 37, 49, 25, 129, 168, 164, 41, 54, 148 }
			},
			new int[10][]
			{
				new int[9] { 82, 22, 32, 127, 143, 213, 39, 41, 70 },
				new int[9] { 62, 44, 61, 123, 105, 189, 48, 57, 64 },
				new int[9] { 47, 25, 17, 175, 222, 220, 24, 30, 86 },
				new int[9] { 68, 36, 17, 106, 102, 206, 59, 74, 74 },
				new int[9] { 57, 39, 23, 151, 68, 216, 55, 63, 58 },
				new int[9] { 49, 30, 35, 141, 70, 168, 82, 40, 115 },
				new int[9] { 51, 25, 15, 136, 129, 202, 38, 35, 139 },
				new int[9] { 68, 26, 16, 111, 141, 215, 29, 28, 28 },
				new int[9] { 59, 39, 19, 114, 75, 180, 77, 104, 42 },
				new int[9] { 40, 61, 26, 126, 152, 206, 61, 59, 93 }
			},
			new int[10][]
			{
				new int[9] { 78, 23, 39, 111, 117, 170, 74, 124, 94 },
				new int[9] { 48, 34, 86, 101, 92, 146, 78, 179, 134 },
				new int[9] { 47, 22, 24, 138, 187, 178, 68, 69, 59 },
				new int[9] { 56, 25, 33, 105, 112, 187, 95, 177, 129 },
				new int[9] { 48, 31, 27, 114, 63, 183, 82, 116, 56 },
				new int[9] { 43, 28, 37, 121, 63, 123, 61, 192, 169 },
				new int[9] { 42, 17, 24, 109, 97, 177, 56, 76, 122 },
				new int[9] { 58, 18, 28, 105, 139, 182, 70, 92, 63 },
				new int[9] { 46, 23, 32, 74, 86, 150, 67, 183, 88 },
				new int[9] { 36, 38, 48, 92, 122, 165, 88, 137, 91 }
			},
			new int[10][]
			{
				new int[9] { 65, 70, 60, 155, 159, 199, 61, 60, 81 },
				new int[9] { 44, 78, 115, 132, 119, 173, 71, 112, 93 },
				new int[9] { 39, 38, 21, 184, 227, 206, 42, 32, 64 },
				new int[9] { 58, 47, 36, 124, 137, 193, 80, 82, 78 },
				new int[9] { 49, 50, 35, 144, 95, 205, 63, 78, 59 },
				new int[9] { 41, 53, 52, 148, 71, 142, 65, 128, 51 },
				new int[9] { 40, 36, 28, 143, 143, 202, 40, 55, 137 },
				new int[9] { 52, 34, 29, 129, 183, 227, 42, 35, 43 },
				new int[9] { 42, 44, 44, 104, 105, 164, 64, 130, 80 },
				new int[9] { 43, 81, 53, 140, 169, 204, 68, 84, 72 }
			}
		};
		___003C_003EkfUvModeProbs = new int[10][]
		{
			new int[9] { 144, 11, 54, 157, 195, 130, 46, 58, 108 },
			new int[9] { 118, 15, 123, 148, 131, 101, 44, 93, 131 },
			new int[9] { 113, 12, 23, 188, 226, 142, 26, 32, 125 },
			new int[9] { 120, 11, 50, 123, 163, 135, 64, 77, 103 },
			new int[9] { 113, 9, 36, 155, 111, 157, 32, 44, 161 },
			new int[9] { 116, 9, 55, 176, 76, 96, 37, 61, 149 },
			new int[9] { 115, 9, 28, 141, 161, 167, 21, 25, 193 },
			new int[9] { 120, 12, 32, 145, 195, 142, 32, 38, 86 },
			new int[9] { 116, 12, 64, 120, 140, 125, 49, 115, 121 },
			new int[9] { 102, 19, 66, 162, 182, 122, 35, 59, 128 }
		};
		___003C_003EdefaultYModeProbs = new int[4][]
		{
			new int[9] { 65, 32, 18, 144, 162, 194, 41, 51, 98 },
			new int[9] { 132, 68, 18, 165, 217, 196, 45, 40, 78 },
			new int[9] { 173, 80, 19, 176, 240, 193, 64, 35, 46 },
			new int[9] { 221, 135, 38, 194, 248, 121, 96, 85, 29 }
		};
		___003C_003EdefaultUvModeProbs = new int[10][]
		{
			new int[9] { 120, 7, 76, 176, 208, 126, 28, 54, 103 },
			new int[9] { 48, 12, 154, 155, 139, 90, 34, 117, 119 },
			new int[9] { 67, 6, 25, 204, 243, 158, 13, 21, 96 },
			new int[9] { 97, 5, 44, 131, 176, 139, 48, 68, 97 },
			new int[9] { 83, 5, 42, 156, 111, 152, 26, 49, 152 },
			new int[9] { 80, 5, 58, 178, 74, 83, 33, 62, 145 },
			new int[9] { 86, 5, 32, 154, 192, 168, 14, 22, 163 },
			new int[9] { 85, 5, 32, 156, 216, 148, 19, 29, 73 },
			new int[9] { 77, 7, 64, 116, 132, 122, 37, 126, 120 },
			new int[9] { 101, 21, 107, 181, 192, 103, 19, 67, 125 }
		};
		___003C_003EdefaultSingleRefProb = new int[5][]
		{
			new int[2] { 33, 16 },
			new int[2] { 77, 74 },
			new int[2] { 142, 142 },
			new int[2] { 172, 170 },
			new int[2] { 238, 247 }
		};
		___003C_003EdefaultCompRefProb = new int[5] { 50, 126, 123, 221, 226 };
	}
}
