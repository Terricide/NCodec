using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpeg4;

public class MPEG4DecodingContext : java.lang.Object
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : java.lang.Object
	{
		_1()
		{
			throw null;
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class Estimation : java.lang.Object
	{
		public int method;

		public bool opaque;

		public bool transparent;

		public bool intraCae;

		public bool interCae;

		public bool noUpdate;

		public bool upsampling;

		public bool intraBlocks;

		public bool interBlocks;

		public bool inter4vBlocks;

		public bool notCodedBlocks;

		public bool dctCoefs;

		public bool dctLines;

		public bool vlcSymbols;

		public bool vlcBits;

		public bool apm;

		public bool npm;

		public bool interpolateMcQ;

		public bool forwBackMcQ;

		public bool halfpel2;

		public bool halfpel4;

		public bool sadct;

		public bool quarterpel;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Synthetic)]
		[LineNumberTable(71)]
		internal Estimation(_1 x0)
			: this()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(71)]
		private Estimation()
		{
		}
	}

	public int width;

	public int height;

	public int horiz_mc_ref;

	public int vert_mc_ref;

	public short[] intraMpegQuantMatrix;

	public short[] interMpegQuantMatrix;

	public int[][] gmcWarps;

	public int mbWidth;

	public int mbHeight;

	public int spriteEnable;

	public int shape;

	public int quant;

	public int quantBits;

	public int timeIncrementBits;

	public int intraDCThreshold;

	public int spriteWarpingPoints;

	public bool reducedResolutionEnable;

	public int fcodeForward;

	public int fcodeBackward;

	public bool newPredEnable;

	public bool rounding;

	public bool quarterPel;

	public bool cartoonMode;

	public int lastTimeBase;

	public int timeBase;

	public int time;

	public int lastNonBTime;

	public int pframeTs;

	public int bframeTs;

	public bool topFieldFirst;

	public bool alternateVerticalScan;

	internal int volVersionId;

	internal int timestampMSB;

	internal int timestampLSB;

	internal bool complexityEstimationDisable;

	internal bool interlacing;

	internal bool spriteBrightnessChange;

	internal bool scalability;

	internal Estimation estimation;

	private const int VIDOBJ_START_CODE = 256;

	private const int VIDOBJLAY_START_CODE = 288;

	private const int VISOBJSEQ_START_CODE = 432;

	private const int VISOBJSEQ_STOP_CODE = 433;

	private const int USERDATA_START_CODE = 434;

	private const int GRPOFVOP_START_CODE = 435;

	private const int VISOBJ_START_CODE = 437;

	private const int VISOBJ_TYPE_VIDEO = 1;

	private const int VIDOBJLAY_AR_EXTPAR = 15;

	private const int VIDOBJLAY_SHAPE_RECTANGULAR = 0;

	private const int VIDOBJLAY_SHAPE_BINARY = 1;

	private const int VIDOBJLAY_SHAPE_BINARY_ONLY = 2;

	private const int VIDOBJLAY_SHAPE_GRAYSCALE = 3;

	private const int VOP_START_CODE = 438;

	private const int VIDOBJ_START_CODE_MASK = 31;

	private const int VIDOBJLAY_START_CODE_MASK = 15;

	private const int SPRITE_STATIC = 1;

	private const int SPRITE_GMC = 2;

	private const int VLC_CODE = 0;

	private const int VLC_LEN = 1;

	private int timeIncrementResolution;

	private bool packedMode;

	public int codingType;

	public bool quantType;

	public int bsVersion;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 66, 233, 160, 78, 236, 159, 179, 110,
		110, 127, 11, 109
	})]
	public MPEG4DecodingContext()
	{
		bsVersion = 65535;
		intraMpegQuantMatrix = new short[64];
		interMpegQuantMatrix = new short[64];
		int[] array = new int[2];
		int num = (array[1] = 2);
		num = (array[0] = 3);
		gmcWarps = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		estimation = new Estimation(null);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 130, 141, 109, 104, 119, 101, 179, 169,
		137, 115, 172, 136, 105, 137, 139, 164, 106, 102,
		163, 105, 105, 137, 105, 105, 105, 169, 103, 118,
		111, 105, 138, 138, 106, 143, 140, 168, 139, 103,
		106, 170, 109, 138, 138, 141, 111, 106, 111, 138,
		110, 106, 142, 111, 106, 111, 202, 143, 169, 115,
		170, 138, 144, 106, 158, 168, 138, 106, 175, 109,
		105, 106, 112, 106, 112, 106, 167, 142, 170, 155,
		118, 106, 107, 106, 107, 106, 107, 106, 107, 170,
		111, 106, 142, 106, 202, 114, 170, 106, 111, 140,
		168, 106, 106, 106, 170, 142, 108, 106, 144, 218,
		106, 144, 218, 106, 195, 106, 144, 168, 110, 105,
		175, 138, 106, 170, 106, 142, 105, 106, 170, 144,
		104, 168, 142, 108, 106, 106, 106, 106, 106, 106,
		106, 138, 106, 106, 106, 106, 106, 106, 170, 163,
		106, 142, 105, 106, 106, 106, 106, 138, 195, 138,
		104, 110, 201, 107, 107, 106, 139, 106, 106, 104,
		110, 99, 108, 141, 100, 115, 159, 6, 144, 141,
		111, 113, 125, 138, 212, 210, 143, 102, 207, 114,
		124, 115, 191, 7, 35, 163, 99, 139, 134
	})]
	public virtual bool readHeaders(ByteBuffer bb)
	{
		bb.order(ByteOrder.BIG_ENDIAN);
		while (bb.remaining() >= 4)
		{
			int startCode = bb.getInt();
			while ((startCode & -256) != 256 && bb.hasRemaining())
			{
				startCode <<= 8;
				startCode |= (sbyte)bb.get() & 0xFF;
			}
			switch (startCode)
			{
			case 433:
				continue;
			case 432:
			{
				int num = (sbyte)bb.get();
				continue;
			}
			case 437:
			{
				BitReader br3 = BitReader.createBitReader(bb);
				if (br3.readBool())
				{
					int verId2 = br3.readNBit(4);
					br3.skip(3);
				}
				else
				{
					int verId = 1;
				}
				int visual_object_type = br3.readNBit(4);
				if (visual_object_type != 1)
				{
					return false;
				}
				if (br3.readBool())
				{
					br3.skip(3);
					br3.skip(1);
					if (br3.readBool())
					{
						br3.skip(8);
						br3.skip(8);
						br3.skip(8);
					}
				}
				br3.terminate();
				continue;
			}
			}
			if ((startCode & -32) == 256)
			{
				continue;
			}
			if ((startCode & -16) == 288)
			{
				BitReader br2 = BitReader.createBitReader(bb);
				br2.skip(1);
				br2.skip(8);
				if (br2.readBool())
				{
					volVersionId = br2.readNBit(4);
					br2.skip(3);
				}
				else
				{
					volVersionId = 1;
				}
				int aspectRatio = br2.readNBit(4);
				if (aspectRatio == 15)
				{
					br2.readNBit(8);
					br2.readNBit(8);
				}
				if (br2.readBool())
				{
					br2.skip(2);
					int lowDelay = (br2.readBool() ? 1 : 0);
					if (br2.readBool())
					{
						int bitrate = br2.readNBit(15) << 15;
						br2.skip(1);
						bitrate |= br2.readNBit(15);
						br2.skip(1);
						int bufferSize = br2.readNBit(15) << 3;
						br2.skip(1);
						bufferSize |= br2.readNBit(3);
						int occupancy = br2.readNBit(11) << 15;
						br2.skip(1);
						occupancy |= br2.readNBit(15);
						br2.skip(1);
					}
				}
				shape = br2.readNBit(2);
				if (shape != 0)
				{
				}
				if (shape == 3 && volVersionId != 1)
				{
					br2.skip(4);
				}
				br2.skip(1);
				timeIncrementResolution = br2.readNBit(16);
				if (timeIncrementResolution > 0)
				{
					timeIncrementBits = java.lang.Math.max(MathUtil.log2(timeIncrementResolution - 1) + 1, 1);
				}
				else
				{
					timeIncrementBits = 1;
				}
				br2.skip(1);
				if (br2.readBool())
				{
					br2.skip(timeIncrementBits);
				}
				if (shape != 2)
				{
					if (shape == 0)
					{
						br2.skip(1);
						width = br2.readNBit(13);
						br2.skip(1);
						height = br2.readNBit(13);
						br2.skip(1);
						calcSizes();
					}
					interlacing = br2.readBool();
					if (!br2.readBool())
					{
					}
					spriteEnable = br2.readNBit((volVersionId == 1) ? 1 : 2);
					if (spriteEnable == 1 || spriteEnable == 2)
					{
						if (spriteEnable != 2)
						{
							br2.readNBit(13);
							br2.skip(1);
							br2.readNBit(13);
							br2.skip(1);
							br2.readNBit(13);
							br2.skip(1);
							br2.readNBit(13);
							br2.skip(1);
						}
						spriteWarpingPoints = br2.readNBit(6);
						br2.readNBit(2);
						spriteBrightnessChange = br2.readBool();
						if (spriteEnable != 2)
						{
							br2.readNBit(1);
						}
					}
					if (volVersionId != 1 && shape != 0)
					{
						br2.skip(1);
					}
					if (br2.readBool())
					{
						quantBits = br2.readNBit(4);
						br2.skip(4);
					}
					else
					{
						quantBits = 5;
					}
					if (shape == 3)
					{
						br2.skip(1);
						br2.skip(1);
						br2.skip(1);
					}
					quantType = br2.readBool();
					if (quantType)
					{
						if (br2.readBool())
						{
							getMatrix(br2, intraMpegQuantMatrix);
						}
						else
						{
							ByteCodeHelper.arraycopy_primitive_2(MPEG4Consts.DEFAULT_INTRA_MATRIX, 0, intraMpegQuantMatrix, 0, intraMpegQuantMatrix.Length);
						}
						if (br2.readBool())
						{
							getMatrix(br2, interMpegQuantMatrix);
						}
						else
						{
							ByteCodeHelper.arraycopy_primitive_2(MPEG4Consts.DEFAULT_INTER_MATRIX, 0, interMpegQuantMatrix, 0, interMpegQuantMatrix.Length);
						}
						if (shape == 3)
						{
							return false;
						}
					}
					if (volVersionId != 1)
					{
						quarterPel = br2.readBool();
					}
					else
					{
						quarterPel = false;
					}
					complexityEstimationDisable = br2.readBool();
					if (!complexityEstimationDisable)
					{
						readVolComplexityEstimationHeader(br2, estimation);
					}
					br2.skip(1);
					if (br2.readBool())
					{
						br2.skip(1);
					}
					if (volVersionId != 1)
					{
						newPredEnable = br2.readBool();
						if (newPredEnable)
						{
							br2.skip(2);
							br2.skip(1);
						}
						reducedResolutionEnable = br2.readBool();
					}
					else
					{
						newPredEnable = false;
						reducedResolutionEnable = false;
					}
					scalability = br2.readBool();
					if (scalability)
					{
						br2.skip(1);
						br2.skip(4);
						br2.skip(1);
						br2.skip(5);
						br2.skip(5);
						br2.skip(5);
						br2.skip(5);
						br2.skip(1);
						if (shape == 1)
						{
							br2.skip(1);
							br2.skip(1);
							br2.skip(5);
							br2.skip(5);
							br2.skip(5);
							br2.skip(5);
						}
						return false;
					}
				}
				else
				{
					if (volVersionId != 1)
					{
						scalability = br2.readBool();
						if (scalability)
						{
							br2.skip(4);
							br2.skip(5);
							br2.skip(5);
							br2.skip(5);
							br2.skip(5);
							return false;
						}
					}
					br2.skip(1);
				}
				br2.terminate();
				continue;
			}
			java.lang.Exception ex2;
			java.lang.Exception ex3;
			switch (startCode)
			{
			case 435:
			{
				BitReader br = BitReader.createBitReader(bb);
				int hours = br.readNBit(5);
				int minutes = br.readNBit(6);
				br.skip(1);
				int seconds = br.readNBit(6);
				br.skip(1);
				br.skip(1);
				br.terminate();
				break;
			}
			case 438:
				return true;
			case 434:
			{
				byte[] tmp = new byte[256];
				int i = 0;
				int num2 = i;
				i++;
				tmp[num2] = (byte)(sbyte)bb.get();
				while (true)
				{
					int num3 = i;
					int num4 = (sbyte)bb.get();
					int num5 = num3;
					byte[] array = tmp;
					array[num5] = (byte)num4;
					if (num4 == 0)
					{
						break;
					}
					i++;
				}
				bb.position(bb.position() - 1);
				string userData = java.lang.String.newhelper(tmp, 0, i);
				if (java.lang.String.instancehelper_startsWith(userData, "XviD"))
				{
					if (tmp[java.lang.String.instancehelper_length(userData) - 1] == 67)
					{
						bsVersion = Integer.parseInt(java.lang.String.instancehelper_substring(userData, 4, java.lang.String.instancehelper_length(userData) - 1));
						cartoonMode = true;
					}
					else
					{
						bsVersion = Integer.parseInt(java.lang.String.instancehelper_substring(userData, 4));
					}
				}
				if (!java.lang.String.instancehelper_startsWith(userData, "DivX"))
				{
					break;
				}
				int buildIndex = java.lang.String.instancehelper_indexOf(userData, "Build");
				if (buildIndex == -1)
				{
					buildIndex = java.lang.String.instancehelper_indexOf(userData, "b");
				}
				try
				{
					int version = Integer.parseInt(java.lang.String.instancehelper_substring(userData, 4, buildIndex));
					int build = Integer.parseInt(java.lang.String.instancehelper_substring(userData, buildIndex + 1, java.lang.String.instancehelper_length(userData) - 1));
					int packed = java.lang.String.instancehelper_charAt(userData, java.lang.String.instancehelper_length(userData) - 1);
					packedMode = packed == 112;
				}
				catch (System.Exception x)
				{
					java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
					if (ex == null)
					{
						throw;
					}
					ex2 = ex;
					goto IL_08dc;
				}
				break;
			}
			default:
				{
					Logger.debug("Unknown");
					break;
				}
				IL_08dc:
				ex3 = ex2;
				break;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 7, 130, 104, 136, 142, 105, 177, 137, 105,
		179, 137, 105, 163, 201, 151, 105, 183, 169, 159,
		6, 173, 159, 3, 201, 108, 114, 111, 105, 111,
		105, 111, 105, 111, 105, 167, 137, 105, 169, 136,
		106, 105, 181, 148, 105, 109, 205, 127, 3, 143,
		135, 138, 102, 139, 109, 208, 137, 138, 102, 139,
		109, 208, 137, 109, 237, 35, 234, 96, 137, 234,
		69, 126, 168, 105, 174, 106, 174, 105, 113, 201,
		106, 109, 116, 123, 116, 143, 127, 3, 155
	})]
	public virtual bool readVOPHeader(BitReader br)
	{
		rounding = false;
		quant = 2;
		codingType = br.readNBit(2);
		while (br.readBool())
		{
			timestampMSB++;
		}
		br.skip(1);
		if (getTimeIncrementBits() != 0)
		{
			timestampLSB = br.readNBit(getTimeIncrementBits());
		}
		br.skip(1);
		if (!br.readBool())
		{
			return false;
		}
		if (newPredEnable)
		{
			int vopId = br.readNBit(java.lang.Math.min(getTimeIncrementBits() + 3, 15));
			if (br.readBool())
			{
				int num = br.readNBit(java.lang.Math.min(getTimeIncrementBits() + 3, 15));
			}
			br.skip(1);
		}
		if (shape != 2 && (codingType == 1 || (codingType == 3 && spriteEnable == 2)))
		{
			rounding = br.readBool();
		}
		if (!reducedResolutionEnable || shape != 0 || (codingType != 1 && codingType != 0) || br.readBool())
		{
		}
		if (shape != 0)
		{
			if (spriteEnable != 1 || codingType != 0)
			{
				width = br.readNBit(13);
				br.skip(1);
				height = br.readNBit(13);
				br.skip(1);
				horiz_mc_ref = br.readNBit(13);
				br.skip(1);
				vert_mc_ref = br.readNBit(13);
				br.skip(1);
				calcSizes();
			}
			br.skip(1);
			if (br.readBool())
			{
				br.skip(8);
			}
		}
		Estimation estimation = new Estimation(null);
		if (shape != 2)
		{
			if (!complexityEstimationDisable)
			{
				readVopComplexityEstimationHeader(br, estimation, spriteEnable, codingType);
			}
			intraDCThreshold = MPEG4Consts.INTRA_DC_THRESHOLD_TABLE[br.readNBit(3)];
			if (interlacing)
			{
				topFieldFirst = br.readBool();
				alternateVerticalScan = br.readBool();
			}
		}
		if ((spriteEnable == 1 || spriteEnable == 2) && codingType == 3)
		{
			for (int i = 0; i < spriteWarpingPoints; i++)
			{
				int x = 0;
				int y = 0;
				int length = getSpriteTrajectory(br);
				if (length > 0)
				{
					x = br.readNBit(length);
					if (x >> length - 1 == 0)
					{
						x = -(x ^ ((1 << length) - 1));
					}
				}
				br.skip(1);
				length = getSpriteTrajectory(br);
				if (length > 0)
				{
					y = br.readNBit(length);
					if (y >> length - 1 == 0)
					{
						y = -(y ^ ((1 << length) - 1));
					}
				}
				br.skip(1);
				gmcWarps[i][0] = x;
				gmcWarps[i][1] = y;
			}
			if (spriteBrightnessChange)
			{
			}
			if (spriteEnable != 1)
			{
			}
		}
		int num2 = br.readNBit(quantBits);
		quant = num2;
		if (num2 < 1)
		{
			quant = 1;
		}
		if (codingType != 0)
		{
			fcodeForward = br.readNBit(3);
		}
		if (codingType == 2)
		{
			fcodeBackward = br.readNBit(3);
		}
		if (!scalability && shape != 0 && codingType != 0)
		{
			br.skip(1);
		}
		if (codingType != 2)
		{
			lastTimeBase = timeBase;
			timeBase += timestampMSB;
			time = timeBase * getTimeIncrementResolution() + timestampLSB;
			pframeTs = time - lastNonBTime;
			lastNonBTime = time;
		}
		else
		{
			time = (lastTimeBase + timestampMSB) * getTimeIncrementResolution() + timestampLSB;
			bframeTs = pframeTs - (lastNonBTime - time);
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 66, 103, 106, 99 })]
	public static MPEG4DecodingContext readFromHeaders(ByteBuffer bb)
	{
		MPEG4DecodingContext ret = new MPEG4DecodingContext();
		if (ret.readHeaders(bb))
		{
			return ret;
		}
		return null;
	}

	[LineNumberTable(new byte[] { 159, 20, 66, 115, 115 })]
	private void calcSizes()
	{
		mbWidth = (width + 15) / 16;
		mbHeight = (height + 15) / 16;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 98, 99, 163, 99, 105, 114, 137, 133,
		102, 148
	})]
	public static void getMatrix(BitReader br, short[] matrix)
	{
		int value = 0;
		int i = 0;
		int last;
		do
		{
			last = value;
			value = br.readNBit(8);
			short[] obj = MPEG4Consts.SCAN_TABLES[0];
			int num = i;
			i++;
			matrix[obj[num]] = (short)value;
		}
		while (value != 0 && i < 64);
		i += -1;
		while (i < 64)
		{
			short[] obj2 = MPEG4Consts.SCAN_TABLES[0];
			int num2 = i;
			i++;
			matrix[obj2[num2]] = (short)last;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 19, 98, 142, 117, 105, 109, 109, 109, 109,
		109, 173, 105, 109, 109, 109, 205, 137, 105, 109,
		109, 109, 173, 105, 109, 109, 109, 109, 109, 173,
		137, 106, 105, 109, 173
	})]
	private void readVolComplexityEstimationHeader(BitReader br, Estimation estimation)
	{
		estimation.method = br.readNBit(2);
		if (estimation.method == 0 || estimation.method == 1)
		{
			if (!br.readBool())
			{
				estimation.opaque = br.readBool();
				estimation.transparent = br.readBool();
				estimation.intraCae = br.readBool();
				estimation.interCae = br.readBool();
				estimation.noUpdate = br.readBool();
				estimation.upsampling = br.readBool();
			}
			if (!br.readBool())
			{
				estimation.intraBlocks = br.readBool();
				estimation.interBlocks = br.readBool();
				estimation.inter4vBlocks = br.readBool();
				estimation.notCodedBlocks = br.readBool();
			}
		}
		br.skip(1);
		if (!br.readBool())
		{
			estimation.dctCoefs = br.readBool();
			estimation.dctLines = br.readBool();
			estimation.vlcSymbols = br.readBool();
			estimation.vlcBits = br.readBool();
		}
		if (!br.readBool())
		{
			estimation.apm = br.readBool();
			estimation.npm = br.readBool();
			estimation.interpolateMcQ = br.readBool();
			estimation.forwBackMcQ = br.readBool();
			estimation.halfpel2 = br.readBool();
			estimation.halfpel4 = br.readBool();
		}
		br.skip(1);
		if (estimation.method == 1 && !br.readBool())
		{
			estimation.sadct = br.readBool();
			estimation.quarterpel = br.readBool();
		}
	}

	[LineNumberTable(865)]
	public virtual int getTimeIncrementBits()
	{
		return timeIncrementBits;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 222, 98, 117, 104, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		169, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 169, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 169, 112,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
		105, 105, 105, 105, 105, 105, 105, 169
	})]
	private void readVopComplexityEstimationHeader(BitReader br, Estimation estimation, int spriteEnable, int codingType)
	{
		if (estimation.method != 0 && estimation.method != 1)
		{
			return;
		}
		if (codingType == 0)
		{
			if (estimation.opaque)
			{
				br.skip(8);
			}
			if (estimation.transparent)
			{
				br.skip(8);
			}
			if (estimation.intraCae)
			{
				br.skip(8);
			}
			if (estimation.interCae)
			{
				br.skip(8);
			}
			if (estimation.noUpdate)
			{
				br.skip(8);
			}
			if (estimation.upsampling)
			{
				br.skip(8);
			}
			if (estimation.intraBlocks)
			{
				br.skip(8);
			}
			if (estimation.notCodedBlocks)
			{
				br.skip(8);
			}
			if (estimation.dctCoefs)
			{
				br.skip(8);
			}
			if (estimation.dctLines)
			{
				br.skip(8);
			}
			if (estimation.vlcSymbols)
			{
				br.skip(8);
			}
			if (estimation.vlcBits)
			{
				br.skip(8);
			}
			if (estimation.sadct)
			{
				br.skip(8);
			}
		}
		if (codingType == 1)
		{
			if (estimation.opaque)
			{
				br.skip(8);
			}
			if (estimation.transparent)
			{
				br.skip(8);
			}
			if (estimation.intraCae)
			{
				br.skip(8);
			}
			if (estimation.interCae)
			{
				br.skip(8);
			}
			if (estimation.noUpdate)
			{
				br.skip(8);
			}
			if (estimation.upsampling)
			{
				br.skip(8);
			}
			if (estimation.intraBlocks)
			{
				br.skip(8);
			}
			if (estimation.notCodedBlocks)
			{
				br.skip(8);
			}
			if (estimation.dctCoefs)
			{
				br.skip(8);
			}
			if (estimation.dctLines)
			{
				br.skip(8);
			}
			if (estimation.vlcSymbols)
			{
				br.skip(8);
			}
			if (estimation.vlcBits)
			{
				br.skip(8);
			}
			if (estimation.interBlocks)
			{
				br.skip(8);
			}
			if (estimation.inter4vBlocks)
			{
				br.skip(8);
			}
			if (estimation.apm)
			{
				br.skip(8);
			}
			if (estimation.npm)
			{
				br.skip(8);
			}
			if (estimation.forwBackMcQ)
			{
				br.skip(8);
			}
			if (estimation.halfpel2)
			{
				br.skip(8);
			}
			if (estimation.halfpel4)
			{
				br.skip(8);
			}
			if (estimation.sadct)
			{
				br.skip(8);
			}
			if (estimation.quarterpel)
			{
				br.skip(8);
			}
		}
		if (codingType == 2)
		{
			if (estimation.opaque)
			{
				br.skip(8);
			}
			if (estimation.transparent)
			{
				br.skip(8);
			}
			if (estimation.intraCae)
			{
				br.skip(8);
			}
			if (estimation.interCae)
			{
				br.skip(8);
			}
			if (estimation.noUpdate)
			{
				br.skip(8);
			}
			if (estimation.upsampling)
			{
				br.skip(8);
			}
			if (estimation.intraBlocks)
			{
				br.skip(8);
			}
			if (estimation.notCodedBlocks)
			{
				br.skip(8);
			}
			if (estimation.dctCoefs)
			{
				br.skip(8);
			}
			if (estimation.dctLines)
			{
				br.skip(8);
			}
			if (estimation.vlcSymbols)
			{
				br.skip(8);
			}
			if (estimation.vlcBits)
			{
				br.skip(8);
			}
			if (estimation.interBlocks)
			{
				br.skip(8);
			}
			if (estimation.inter4vBlocks)
			{
				br.skip(8);
			}
			if (estimation.apm)
			{
				br.skip(8);
			}
			if (estimation.npm)
			{
				br.skip(8);
			}
			if (estimation.forwBackMcQ)
			{
				br.skip(8);
			}
			if (estimation.halfpel2)
			{
				br.skip(8);
			}
			if (estimation.halfpel4)
			{
				br.skip(8);
			}
			if (estimation.interpolateMcQ)
			{
				br.skip(8);
			}
			if (estimation.sadct)
			{
				br.skip(8);
			}
			if (estimation.quarterpel)
			{
				br.skip(8);
			}
		}
		if (codingType == 3 && spriteEnable == 1)
		{
			if (estimation.intraBlocks)
			{
				br.skip(8);
			}
			if (estimation.notCodedBlocks)
			{
				br.skip(8);
			}
			if (estimation.dctCoefs)
			{
				br.skip(8);
			}
			if (estimation.dctLines)
			{
				br.skip(8);
			}
			if (estimation.vlcSymbols)
			{
				br.skip(8);
			}
			if (estimation.vlcBits)
			{
				br.skip(8);
			}
			if (estimation.interBlocks)
			{
				br.skip(8);
			}
			if (estimation.inter4vBlocks)
			{
				br.skip(8);
			}
			if (estimation.apm)
			{
				br.skip(8);
			}
			if (estimation.npm)
			{
				br.skip(8);
			}
			if (estimation.forwBackMcQ)
			{
				br.skip(8);
			}
			if (estimation.halfpel2)
			{
				br.skip(8);
			}
			if (estimation.halfpel4)
			{
				br.skip(8);
			}
			if (estimation.interpolateMcQ)
			{
				br.skip(8);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 225, 98, 104, 123, 113, 227, 61, 231, 71 })]
	private int getSpriteTrajectory(BitReader br)
	{
		for (int i = 0; i < 12; i++)
		{
			if (br.checkNBit(MPEG4Consts.SPRITE_TRAJECTORY_LEN[i][1]) == MPEG4Consts.SPRITE_TRAJECTORY_LEN[i][0])
			{
				br.skip(MPEG4Consts.SPRITE_TRAJECTORY_LEN[i][1]);
				return i;
			}
		}
		return -1;
	}

	[LineNumberTable(869)]
	public virtual int getTimeIncrementResolution()
	{
		return timeIncrementResolution;
	}

	[LineNumberTable(861)]
	public virtual bool getPackedMode()
	{
		return packedMode;
	}
}
