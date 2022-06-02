using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.codecs.vpx.vp9;

public class CodedSuperBlock : Object
{
	private CodedBlock[] codedBlocks;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 105 })]
	protected internal CodedSuperBlock()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(IIILorg/jcodec/codecs/vpx/VPXBooleanDecoder;Lorg/jcodec/codecs/vpx/vp9/DecodingContext;Ljava/util/List<Lorg/jcodec/codecs/vpx/vp9/CodedBlock;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 127, 130, 110, 138, 104, 100, 123, 106, 108,
		108, 109, 121, 106, 108, 106, 109, 123, 106, 140,
		109, 122, 107, 108, 106, 109, 124, 107, 140, 102,
		114, 109, 116, 109, 116, 127, 0, 187, 106, 113,
		107, 122, 156
	})]
	protected internal virtual void readSubPartition(int miCol, int miRow, int logBlkSize, VPXBooleanDecoder decoder, DecodingContext c, List blocks)
	{
		int part = readPartition(miCol, miRow, logBlkSize, decoder, c);
		int nextBlkSize = 1 << logBlkSize >> 1;
		if (logBlkSize > 0)
		{
			switch (part)
			{
			case 0:
			{
				CodedBlock blk4 = readBlock(miCol, miRow, Consts.___003C_003EblSizeLookup[1 + logBlkSize][1 + logBlkSize], decoder, c);
				blocks.add(blk4);
				saveAboveSizes(miCol, 1 + logBlkSize, c);
				saveLeftSizes(miRow, 1 + logBlkSize, c);
				return;
			}
			case 1:
			{
				CodedBlock blk3 = readBlock(miCol, miRow, Consts.___003C_003EblSizeLookup[1 + logBlkSize][logBlkSize], decoder, c);
				blocks.add(blk3);
				saveAboveSizes(miCol, 1 + logBlkSize, c);
				saveLeftSizes(miRow, logBlkSize, c);
				if (miRow + nextBlkSize < c.getMiTileHeight())
				{
					blk3 = readBlock(miCol, miRow + nextBlkSize, Consts.___003C_003EblSizeLookup[1 + logBlkSize][logBlkSize], decoder, c);
					blocks.add(blk3);
					saveLeftSizes(miRow + nextBlkSize, logBlkSize, c);
				}
				return;
			}
			case 2:
			{
				CodedBlock blk2 = readBlock(miCol, miRow, Consts.___003C_003EblSizeLookup[logBlkSize][1 + logBlkSize], decoder, c);
				blocks.add(blk2);
				saveLeftSizes(miRow, 1 + logBlkSize, c);
				saveAboveSizes(miCol, logBlkSize, c);
				if (miCol + nextBlkSize < c.getMiTileWidth())
				{
					blk2 = readBlock(miCol + nextBlkSize, miRow, Consts.___003C_003EblSizeLookup[logBlkSize][1 + logBlkSize], decoder, c);
					blocks.add(blk2);
					saveAboveSizes(miCol + nextBlkSize, logBlkSize, c);
				}
				return;
			}
			}
			readSubPartition(miCol, miRow, logBlkSize - 1, decoder, c, blocks);
			if (miCol + nextBlkSize < c.getMiTileWidth())
			{
				readSubPartition(miCol + nextBlkSize, miRow, logBlkSize - 1, decoder, c, blocks);
			}
			if (miRow + nextBlkSize < c.getMiTileHeight())
			{
				readSubPartition(miCol, miRow + nextBlkSize, logBlkSize - 1, decoder, c, blocks);
			}
			if (miCol + nextBlkSize < c.getMiTileWidth() && miRow + nextBlkSize < c.getMiTileHeight())
			{
				readSubPartition(miCol + nextBlkSize, miRow + nextBlkSize, logBlkSize - 1, decoder, c, blocks);
			}
		}
		else
		{
			int subBlSz = Consts.___003C_003Esub8x8PartitiontoBlockType[part];
			CodedBlock blk = readBlock(miCol, miRow, subBlSz, decoder, c);
			blocks.add(blk);
			saveAboveSizes(miCol, 1 + logBlkSize - ((subBlSz == 0 || subBlSz == 1) ? 1 : 0), c);
			saveLeftSizes(miRow, 1 + logBlkSize - ((subBlSz == 0 || subBlSz == 2) ? 1 : 0), c);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 110, 130, 140, 107, 106, 114, 147, 104, 99,
		100, 114, 101, 146
	})]
	protected internal static int readPartition(int miCol, int miRow, int blkSize, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int ctx = calcPartitionContext(miCol, miRow, blkSize, c);
		int[] probs = c.getPartitionProbs()[ctx];
		int halfBlk = 1 << blkSize >> 1;
		int rightEdge = ((miCol + halfBlk >= c.getMiTileWidth()) ? 1 : 0);
		int bottomEdge = ((miRow + halfBlk >= c.getMiTileHeight()) ? 1 : 0);
		if (rightEdge != 0 && bottomEdge != 0)
		{
			return 3;
		}
		if (rightEdge != 0)
		{
			return (decoder.readBit(probs[2]) != 1) ? 2 : 3;
		}
		if (bottomEdge != 0)
		{
			return (decoder.readBit(probs[1]) != 1) ? 1 : 3;
		}
		int result = decoder.readTree(Consts.___003C_003ETREE_PARTITION, probs);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(57)]
	protected internal virtual CodedBlock readBlock(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c)
	{
		CodedBlock result = CodedBlock.read(miCol, miRow, blSz, decoder, c);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 113, 162, 107, 136, 104, 103, 39, 135 })]
	private static void saveAboveSizes(int miCol, int blkSize4x4, DecodingContext c)
	{
		int blkSize8x8 = ((blkSize4x4 != 0) ? (blkSize4x4 - 1) : 0);
		int miBlkSize = 1 << blkSize8x8;
		int[] aboveSizes = c.getAbovePartitionSizes();
		for (int i = 0; i < miBlkSize; i++)
		{
			aboveSizes[miCol + i] = blkSize4x4;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 130, 107, 136, 104, 103, 50, 135 })]
	private static void saveLeftSizes(int miRow, int blkSize4x4, DecodingContext c)
	{
		int blkSize8x8 = ((blkSize4x4 != 0) ? (blkSize4x4 - 1) : 0);
		int miBlkSize = 1 << blkSize8x8;
		int[] leftSizes = c.getLeftPartitionSizes();
		for (int i = 0; i < miBlkSize; i++)
		{
			leftSizes[((8 != -1) ? (miRow % 8) : 0) + i] = blkSize4x4;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 98, 133, 104, 139, 104, 216 })]
	private static int calcPartitionContext(int miCol, int miRow, int blkSize, DecodingContext c)
	{
		int left = 0;
		int above = 0;
		int[] aboveSizes = c.getAbovePartitionSizes();
		above = ((aboveSizes[miCol] <= blkSize) ? 1 : 0);
		int[] leftSizes = c.getLeftPartitionSizes();
		left |= ((leftSizes[(8 != -1) ? (miRow % 8) : 0] <= blkSize) ? 1 : 0);
		return blkSize * 4 + ((left != 0) ? 2 : 0) + ((above != 0) ? 1 : 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 105, 104 })]
	public CodedSuperBlock(CodedBlock[] codedBlocks)
	{
		this.codedBlocks = codedBlocks;
	}

	[LineNumberTable(38)]
	public virtual CodedBlock[] getCodedBlocks()
	{
		return codedBlocks;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 135, 103, 141, 151 })]
	public static CodedSuperBlock read(int miCol, int miRow, VPXBooleanDecoder decoder, DecodingContext c)
	{
		ArrayList blocks = new ArrayList();
		CodedSuperBlock result = new CodedSuperBlock();
		result.readSubPartition(miCol, miRow, 3, decoder, c, blocks);
		result.codedBlocks = (CodedBlock[])((List)blocks).toArray((object[])CodedBlock.___003C_003EEMPTY_ARR);
		return result;
	}
}
