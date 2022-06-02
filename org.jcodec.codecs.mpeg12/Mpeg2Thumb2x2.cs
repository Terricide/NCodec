using System.Runtime.CompilerServices;
using IKVM.Attributes;
using org.jcodec.codecs.mpeg12.bitstream;
using org.jcodec.common.dct;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12;

public class Mpeg2Thumb2x2 : MPEGDecoder
{
	private MPEGPred localPred;

	private MPEGPred oldPred;

	public static int[] BLOCK_POS_X;

	public static int[] BLOCK_POS_Y;

	public static int[][] scan2x2;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(25)]
	public Mpeg2Thumb2x2()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 66, 102, 137, 105, 99, 105, 110, 140,
		136, 99
	})]
	private void finishOff(BitReader bits, int idx, VLC vlcCoeff, int escSize)
	{
		while (idx < 64)
		{
			switch (vlcCoeff.readVLC(bits))
			{
			case 2048:
				return;
			case 2049:
				idx += bits.readNBit(6) + 1;
				bits.readNBit(escSize);
				break;
			default:
				bits.read1Bit();
				break;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 98, 131, 105, 110, 114, 114, 148, 105,
		136, 112, 114, 114, 185, 103, 110, 114, 114, 146,
		102, 229, 57, 231, 74
	})]
	protected internal override void putSub(byte[] big, int off, int stride, int[] block, int mbW, int mbH)
	{
		int blOff = 0;
		if (mbW == 1)
		{
			big[off] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff]);
			big[off + 1] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 1]);
			big[off + stride] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 2]);
			big[off + stride + 1] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 3]);
			if (mbH == 2)
			{
				off += stride << 1;
				big[off] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 4]);
				big[off + 1] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 5]);
				big[off + stride] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 6]);
				big[off + stride + 1] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 7]);
			}
		}
		else
		{
			for (int i = 0; i < 4; i++)
			{
				big[off] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff]);
				big[off + 1] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 1]);
				big[off + 2] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 2]);
				big[off + 3] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 3]);
				off += stride;
				blOff += 4;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 162, 106, 119, 111, 107, 107, 127, 2,
		100, 108, 170, 106, 102, 106, 111, 116, 150, 108,
		158, 106, 102, 106, 109, 106
	})]
	protected internal override void blockIntra(BitReader bits, VLC vlcCoeff, int[] block, int[] intra_dc_predictor, int blkIdx, int[] scan, int escSize, int intra_dc_mult, int qScale, int[] qmat)
	{
		int cc = MPEGConst.___003C_003EBLOCK_TO_CC[blkIdx];
		int size = ((cc != 0) ? MPEGConst.___003C_003EvlcDCSizeChroma : MPEGConst.___003C_003EvlcDCSizeLuma).readVLC(bits);
		int delta = ((size != 0) ? MPEGDecoder.mpegSigned(bits, size) : 0);
		intra_dc_predictor[cc] += delta;
		block[0] = intra_dc_predictor[cc] * intra_dc_mult;
		int num = 0;
		int num2 = 3;
		int[] array = block;
		int num3 = num;
		array[num2] = num;
		num = num3;
		num2 = 2;
		array = block;
		int num4 = num;
		array[num2] = num;
		block[1] = num4;
		int readVLC = 0;
		int idx;
		int level;
		for (idx = 0; idx < 6; block[scan[idx]] = level)
		{
			readVLC = vlcCoeff.readVLC(bits);
			switch (readVLC)
			{
			case 2049:
			{
				idx += bits.readNBit(6) + 1;
				int num5 = MPEGDecoder.twosSigned(bits, escSize) * qScale * qmat[idx];
				level = ((num5 < 0) ? (-(-num5 >> 4)) : (num5 >> 4));
				continue;
			}
			default:
				idx += (readVLC >> 6) + 1;
				level = MPEGDecoder.toSigned((readVLC & 0x3F) * qScale * qmat[idx] >> 4, bits.read1Bit());
				continue;
			case 2048:
				break;
			}
			break;
		}
		if (readVLC != 2048)
		{
			finishOff(bits, idx, vlcCoeff, escSize);
		}
		IDCT2x2.idct(block, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 162, 153, 99, 115, 104, 124, 135, 165,
		100, 104, 138, 106, 102, 106, 109, 153, 106, 159,
		0, 105, 102, 106, 108, 106
	})]
	protected internal override void blockInter(BitReader bits, VLC vlcCoeff, int[] block, int[] scan, int escSize, int qScale, int[] qmat)
	{
		int num = 0;
		int num2 = 3;
		int[] array = block;
		int num3 = num;
		array[num2] = num;
		num = num3;
		num2 = 2;
		array = block;
		int num4 = num;
		array[num2] = num;
		block[1] = num4;
		int idx = -1;
		if (vlcCoeff == MPEGConst.___003C_003EvlcCoeff0 && bits.checkNBit(1) == 1)
		{
			bits.read1Bit();
			block[0] = MPEGDecoder.toSigned(MPEGDecoder.quantInter(1, qScale * qmat[0]), bits.read1Bit());
			idx++;
		}
		else
		{
			block[0] = 0;
		}
		int readVLC = 0;
		int ac;
		for (; idx < 6; block[scan[idx]] = ac)
		{
			readVLC = vlcCoeff.readVLC(bits);
			switch (readVLC)
			{
			case 2049:
				idx += bits.readNBit(6) + 1;
				ac = MPEGDecoder.quantInterSigned(MPEGDecoder.twosSigned(bits, escSize), qScale * qmat[idx]);
				continue;
			default:
				idx += (readVLC >> 6) + 1;
				ac = MPEGDecoder.toSigned(MPEGDecoder.quantInter(readVLC & 0x3F, qScale * qmat[idx]), bits.read1Bit());
				continue;
			case 2048:
				break;
			}
			break;
		}
		if (readVLC != 2048)
		{
			finishOff(bits, idx, vlcCoeff, escSize);
		}
		IDCT2x2.idct(block, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 130, 115, 110, 169 })]
	public override int decodeMacroblock(PictureHeader ph, Context context, int prevAddr, int[] qScaleCode, byte[][] buf, int stride, BitReader bits, int vertOff, int vertStep, MPEGPred pred)
	{
		if (localPred == null || oldPred != pred)
		{
			localPred = new MPEGPredQuad(pred);
			oldPred = pred;
		}
		int result = base.decodeMacroblock(ph, context, prevAddr, qScaleCode, buf, stride, bits, vertOff, vertStep, localPred);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 98, 116, 147, 104, 105, 106, 150, 118,
		120, 121, 123
	})]
	protected internal override void mapBlock(int[] block, int[] @out, int blkIdx, int dctType, int chromaFormat)
	{
		int stepVert = ((chromaFormat != 1 || (blkIdx != 4 && blkIdx != 5)) ? dctType : 0);
		int log2stride = ((blkIdx >= 4) ? (2 - MPEGConst.___003C_003ESQUEEZE_X[chromaFormat]) : 2);
		int blkIdxExt = blkIdx + (dctType << 4);
		int x = BLOCK_POS_X[blkIdxExt];
		int y = BLOCK_POS_Y[blkIdxExt];
		int off = (y << log2stride) + x;
		int stride = 1 << log2stride + stepVert;
		int num = off;
		int[] array = @out;
		array[num] += block[0];
		num = off + 1;
		array = @out;
		array[num] += block[1];
		num = off + stride;
		array = @out;
		array[num] += block[2];
		num = off + stride + 1;
		array = @out;
		array[num] += block[3];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 162, 127, 0, 108, 140, 159, 13, 159,
		19, 159, 21
	})]
	protected internal override void put(int[][] mbPix, byte[][] buf, int stride, int chromaFormat, int mbX, int mbY, int width, int height, int vertOff, int vertStep)
	{
		int chromaStride = stride + (1 << MPEGConst.___003C_003ESQUEEZE_X[chromaFormat]) - 1 >> MPEGConst.___003C_003ESQUEEZE_X[chromaFormat];
		int chromaMBW = 2 - MPEGConst.___003C_003ESQUEEZE_X[chromaFormat];
		int chromaMBH = 2 - MPEGConst.___003C_003ESQUEEZE_Y[chromaFormat];
		putSub(buf[0], (mbY << 2) * (stride << vertStep) + vertOff * stride + (mbX << 2), stride << vertStep, mbPix[0], 2, 2);
		putSub(buf[1], (mbY << chromaMBH) * (chromaStride << vertStep) + vertOff * chromaStride + (mbX << chromaMBW), chromaStride << vertStep, mbPix[1], chromaMBW, chromaMBH);
		putSub(buf[2], (mbY << chromaMBH) * (chromaStride << vertStep) + vertOff * chromaStride + (mbX << chromaMBW), chromaStride << vertStep, mbPix[2], chromaMBW, chromaMBH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 94, 98, 106, 111, 111, 111, 143, 159, 4 })]
	protected internal override Context initContext(SequenceHeader sh, PictureHeader ph)
	{
		Context context = base.initContext(sh, ph);
		context.codedWidth >>= 2;
		context.codedHeight >>= 2;
		context.picWidth >>= 2;
		context.picHeight >>= 2;
		context.scan = scan2x2[(ph.pictureCodingExtension != null) ? ph.pictureCodingExtension.alternate_scan : 0];
		return context;
	}

	[LineNumberTable(new byte[]
	{
		159,
		113,
		162,
		159,
		113,
		byte.MaxValue,
		113,
		160,
		65
	})]
	static Mpeg2Thumb2x2()
	{
		BLOCK_POS_X = new int[28]
		{
			0, 2, 0, 2, 0, 0, 0, 0, 2, 2,
			2, 2, 0, 0, 0, 0, 0, 2, 0, 2,
			0, 0, 0, 0, 2, 2, 2, 2
		};
		BLOCK_POS_Y = new int[28]
		{
			0, 0, 2, 2, 0, 0, 2, 2, 0, 0,
			2, 2, 0, 0, 0, 0, 0, 0, 1, 1,
			0, 0, 1, 1, 0, 0, 1, 1
		};
		scan2x2 = new int[2][]
		{
			new int[64]
			{
				0, 1, 2, 4, 3, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4
			},
			new int[64]
			{
				0, 2, 4, 4, 1, 3, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
				4, 4, 4, 4
			}
		};
	}
}
