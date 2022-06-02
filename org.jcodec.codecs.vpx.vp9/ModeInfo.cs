using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx.vp9;

public class ModeInfo : Object
{
	private int segmentId;

	private bool skip;

	private int txSize;

	private int yMode;

	private int subModes;

	private int uvMode;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 137 })]
	internal ModeInfo()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 130, 99, 115, 139, 99, 108, 143, 176,
		99, 101, 146, 143, 138, 143
	})]
	public virtual ModeInfo read(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int segmentId = 0;
		if (c.isSegmentationEnabled() && c.isUpdateSegmentMap())
		{
			segmentId = readSegmentId(decoder, c);
		}
		int skip = 1;
		if (!c.isSegmentFeatureActive(segmentId, 3))
		{
			skip = (readSkipFlag(miCol, miRow, blSz, decoder, c) ? 1 : 0);
		}
		int txSize = readTxSize(miCol, miRow, blSz, allowSelect: true, decoder, c);
		int subModes = 0;
		int yMode;
		if (blSz >= 3)
		{
			yMode = readKfIntraMode(miCol, miRow, blSz, decoder, c);
		}
		else
		{
			subModes = readKfIntraModeSub(miCol, miRow, blSz, decoder, c);
			yMode = subModes & 0xFF;
		}
		int uvMode = readInterIntraUvMode(yMode, decoder, c);
		ModeInfo result = new ModeInfo(segmentId, (byte)skip != 0, txSize, yMode, subModes, uvMode);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 89, 66, 104 })]
	public static int readSegmentId(VPXBooleanDecoder decoder, DecodingContext c)
	{
		int[] probs = c.getSegmentationTreeProbs();
		int result = decoder.readTree(Consts.___003C_003ETREE_SEGMENT_ID, probs);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 88, 98, 99, 106, 144, 105, 138, 100, 109,
		100, 144, 159, 6, 138, 149, 111, 44, 201, 111,
		41, 201
	})]
	public virtual bool readSkipFlag(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int ctx = 0;
		int availAbove = ((miRow > 0) ? 1 : 0);
		int availLeft = ((miCol > c.getMiTileStartCol()) ? 1 : 0);
		bool[] aboveSkipped = c.getAboveSkipped();
		bool[] leftSkipped = c.getLeftSkipped();
		if (availAbove != 0)
		{
			ctx += (aboveSkipped[miCol] ? 1 : 0);
		}
		if (availLeft != 0)
		{
			ctx += (leftSkipped[miRow & 7] ? 1 : 0);
		}
		java.lang.System.@out.println(new StringBuilder().append("SKIP CTX: ").append(ctx).toString());
		int[] probs = c.getSkipProbs();
		int ret = ((decoder.readBit(probs[ctx]) == 1) ? 1 : 0);
		for (int i = 0; i < Consts.___003C_003EblH[blSz]; i++)
		{
			leftSkipped[(i + miRow) & 7] = (byte)ret != 0;
		}
		for (int j = 0; j < Consts.___003C_003EblW[blSz]; j++)
		{
			aboveSkipped[j + miCol] = (byte)ret != 0;
		}
		return (byte)ret != 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 129, 68, 101, 131, 105, 111, 116, 106,
		113, 100, 100, 111, 108, 114, 110, 101, 101, 100,
		101, 143, 105, 151, 106, 131, 106, 131, 106, 131,
		145, 117, 99, 175, 111, 48, 169, 111, 48, 201
	})]
	public virtual int readTxSize(int miCol, int miRow, int blSz, bool allowSelect, VPXBooleanDecoder decoder, DecodingContext c)
	{
		if (blSz < 3)
		{
			return 0;
		}
		int maxTxSize = Consts.___003C_003EmaxTxLookup[blSz];
		int txSize = Math.min(maxTxSize, c.getTxMode());
		if (allowSelect && c.getTxMode() == 4)
		{
			int availAbove = ((miRow > 0) ? 1 : 0);
			int availLeft = ((miCol > c.getMiTileStartCol()) ? 1 : 0);
			int above = maxTxSize;
			int left = maxTxSize;
			if (availAbove != 0 && !c.getAboveSkipped()[miCol])
			{
				above = c.getAboveTxSizes()[miCol];
			}
			if (availLeft != 0 && !c.getLeftSkipped()[miRow & 7])
			{
				left = c.getLeftTxSizes()[miRow & 7];
			}
			if (availLeft == 0)
			{
				left = above;
			}
			if (availAbove == 0)
			{
				above = left;
			}
			int ctx = ((above + left > maxTxSize) ? 1 : 0);
			int[][] probs = null;
			switch (maxTxSize)
			{
			case 3:
				probs = c.getTx32x32Probs();
				break;
			case 2:
				probs = c.getTx16x16Probs();
				break;
			case 1:
				probs = c.getTx8x8Probs();
				break;
			default:
				
				throw new RuntimeException("Shouldn't happen");
			}
			txSize = decoder.readTree(Consts.___003C_003ETREE_TX_SIZE[maxTxSize], probs[ctx]);
		}
		else
		{
			txSize = Math.min(maxTxSize, c.getTxMode());
		}
		for (int i = 0; i < Consts.___003C_003EblH[blSz]; i++)
		{
			c.getLeftTxSizes()[(miRow + i) & 7] = txSize;
		}
		for (int j = 0; j < Consts.___003C_003EblW[blSz]; j++)
		{
			c.getAboveTxSizes()[(miCol + j) & 7] = txSize;
		}
		return txSize;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 162, 106, 112, 105, 169, 108, 151, 138,
		151, 102, 113
	})]
	public virtual int readKfIntraMode(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int availAbove = ((miRow > 0) ? 1 : 0);
		int availLeft = ((miCol > c.getMiTileStartCol()) ? 1 : 0);
		int[] aboveIntraModes = c.getAboveModes();
		int[] leftIntraModes = c.getLeftModes();
		int aboveMode = ((availAbove != 0) ? aboveIntraModes[miCol] : 0);
		int num;
		if (availLeft != 0)
		{
			num = leftIntraModes[(8 != -1) ? (miRow % 8) : 0];
		}
		else
		{
			num = 0;
		}
		int leftMode = num;
		int[][][] probs = c.getKfYModeProbs();
		int intraMode = (aboveIntraModes[miCol] = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[aboveMode][leftMode]));
		leftIntraModes[(8 != -1) ? (miRow % 8) : 0] = intraMode;
		return intraMode;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 113, 130, 106, 112, 105, 169, 106, 108, 110,
		119, 138, 103, 119, 119, 119, 102, 104, 113, 101,
		119, 102, 104, 113, 101, 119, 102, 104, 177
	})]
	public virtual int readKfIntraModeSub(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int availAbove = ((miRow > 0) ? 1 : 0);
		int availLeft = ((miCol > c.getMiTileStartCol()) ? 1 : 0);
		int[] aboveIntraModes = c.getAboveModes();
		int[] leftIntraModes = c.getLeftModes();
		int[][][] probs = c.getKfYModeProbs();
		int aboveMode = ((availAbove != 0) ? aboveIntraModes[miCol] : 0);
		int leftMode = ((availLeft != 0) ? leftIntraModes[miRow & 7] : 0);
		int mode0 = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[aboveMode][leftMode]);
		int mode1 = 0;
		int mode2 = 0;
		int mode3 = 0;
		switch (blSz)
		{
		case 0:
		{
			mode1 = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[aboveMode][mode0]);
			mode2 = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[mode0][leftMode]);
			mode3 = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[mode1][mode2]);
			aboveIntraModes[miCol] = mode2;
			leftIntraModes[miRow & 7] = mode1;
			int result3 = vect4(mode0, mode1, mode2, mode3);
			
			return result3;
		}
		case 1:
		{
			mode1 = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[aboveMode][mode0]);
			aboveIntraModes[miCol] = mode0;
			leftIntraModes[miRow & 7] = mode1;
			int result2 = vect4(mode0, mode1, mode0, mode1);
			
			return result2;
		}
		case 2:
		{
			mode1 = (aboveIntraModes[miCol] = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[mode0][leftMode]));
			leftIntraModes[miRow & 7] = mode0;
			int result = vect4(mode0, mode0, mode1, mode1);
			
			return result;
		}
		default:
			return 0;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 80, 162, 104 })]
	public virtual int readInterIntraUvMode(int yMode, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int[][] probs = c.getKfUVModeProbs();
		int result = decoder.readTree(Consts.___003C_003ETREE_INTRA_MODE, probs[yMode]);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 65, 67, 105, 104, 104, 104, 105, 105,
		105
	})]
	public ModeInfo(int segmentId, bool skip, int txSize, int yMode, int subModes, int uvMode)
	{
		this.segmentId = segmentId;
		this.skip = skip;
		this.txSize = txSize;
		this.yMode = yMode;
		this.subModes = subModes;
		this.uvMode = uvMode;
	}

	[LineNumberTable(153)]
	public static int vect4(int val0, int val1, int val2, int val3)
	{
		return val0 | (val1 << 8) | (val2 << 16) | (val3 << 24);
	}

	[LineNumberTable(50)]
	public virtual int getSegmentId()
	{
		return segmentId;
	}

	[LineNumberTable(54)]
	public virtual bool isSkip()
	{
		return skip;
	}

	[LineNumberTable(58)]
	public virtual int getTxSize()
	{
		return txSize;
	}

	[LineNumberTable(62)]
	public virtual int getYMode()
	{
		return yMode;
	}

	[LineNumberTable(66)]
	public virtual int getSubModes()
	{
		return subModes;
	}

	[LineNumberTable(70)]
	public virtual int getUvMode()
	{
		return uvMode;
	}

	[LineNumberTable(157)]
	public static int vect4get(int vect, int ind)
	{
		return (vect >> (ind << 3)) & 0xFF;
	}

	[LineNumberTable(247)]
	public virtual bool isInter()
	{
		return false;
	}
}
