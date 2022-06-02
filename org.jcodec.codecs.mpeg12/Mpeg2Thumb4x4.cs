using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.util;
using org.jcodec.codecs.mpeg12.bitstream;
using org.jcodec.common.dct;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12;

public class Mpeg2Thumb4x4 : MPEGDecoder
{
	private MPEGPred localPred;

	private MPEGPred oldPred;

	public static int[] BLOCK_POS_X;

	public static int[] BLOCK_POS_Y;

	public static int[][] scan4x4;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(27)]
	public Mpeg2Thumb4x4()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 162, 102, 137, 105, 99, 105, 110, 140,
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
		159, 102, 66, 131, 105, 109, 110, 114, 114, 146,
		101, 230, 57, 236, 74, 112, 110, 114, 114, 114,
		114, 114, 114, 146, 101, 230, 53, 234, 78
	})]
	protected internal override void putSub(byte[] big, int off, int stride, int[] block, int mbW, int mbH)
	{
		int blOff = 0;
		if (mbW == 2)
		{
			for (int j = 0; j < 1 << mbH; j++)
			{
				big[off] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff]);
				big[off + 1] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 1]);
				big[off + 2] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 2]);
				big[off + 3] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 3]);
				blOff += 4;
				off += stride;
			}
			return;
		}
		for (int i = 0; i < 1 << mbH; i++)
		{
			big[off] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff]);
			big[off + 1] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 1]);
			big[off + 2] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 2]);
			big[off + 3] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 3]);
			big[off + 4] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 4]);
			big[off + 5] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 5]);
			big[off + 6] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 6]);
			big[off + 7] = (byte)(sbyte)MPEGDecoder.clipTo8Bit(block[blOff + 7]);
			blOff += 8;
			off += stride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 98, 106, 119, 111, 107, 107, 139, 99,
		125, 169, 105, 102, 105, 111, 116, 150, 107, 157,
		106, 102, 105, 109, 106
	})]
	protected internal override void blockIntra(BitReader bits, VLC vlcCoeff, int[] block, int[] intra_dc_predictor, int blkIdx, int[] scan, int escSize, int intra_dc_mult, int qScale, int[] qmat)
	{
		int cc = MPEGConst.___003C_003EBLOCK_TO_CC[blkIdx];
		int size = ((cc != 0) ? MPEGConst.___003C_003EvlcDCSizeChroma : MPEGConst.___003C_003EvlcDCSizeLuma).readVLC(bits);
		int delta = ((size != 0) ? MPEGDecoder.mpegSigned(bits, size) : 0);
		intra_dc_predictor[cc] += delta;
		Arrays.fill(block, 1, 16, 0);
		block[0] = intra_dc_predictor[cc] * intra_dc_mult;
		int readVLC = 0;
		int idx;
		int level;
		for (idx = 0; idx < 19 + ((scan == scan4x4[1]) ? 7 : 0); block[scan[idx]] = level)
		{
			readVLC = vlcCoeff.readVLC(bits);
			switch (readVLC)
			{
			case 2049:
			{
				idx += bits.readNBit(6) + 1;
				int num = MPEGDecoder.twosSigned(bits, escSize) * qScale * qmat[idx];
				level = ((num < 0) ? (-(-num >> 4)) : (num >> 4));
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
		IDCT4x4.idct(block, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 130, 139, 99, 115, 104, 124, 135, 165,
		99, 121, 137, 105, 102, 105, 109, 152, 105, 157,
		104, 102, 105, 108, 106
	})]
	protected internal override void blockInter(BitReader bits, VLC vlcCoeff, int[] block, int[] scan, int escSize, int qScale, int[] qmat)
	{
		Arrays.fill(block, 1, 16, 0);
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
		for (; idx < 19 + ((scan == scan4x4[1]) ? 7 : 0); block[scan[idx]] = ac)
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
		IDCT4x4.idct(block, 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 66, 115, 110, 169 })]
	public override int decodeMacroblock(PictureHeader ph, Context context, int prevAddr, int[] qScaleCode, byte[][] buf, int stride, BitReader bits, int vertOff, int vertStep, MPEGPred pred)
	{
		if (localPred == null || oldPred != pred)
		{
			localPred = new MPEGPredDbl(pred);
			oldPred = pred;
		}
		int result = base.decodeMacroblock(ph, context, prevAddr, qScaleCode, buf, stride, bits, vertOff, vertStep, localPred);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 162, 116, 147, 104, 105, 106, 150, 109,
		119, 123, 123, 251, 60, 243, 70
	})]
	protected internal override void mapBlock(int[] block, int[] @out, int blkIdx, int dctType, int chromaFormat)
	{
		int stepVert = ((chromaFormat != 1 || (blkIdx != 4 && blkIdx != 5)) ? dctType : 0);
		int log2stride = ((blkIdx >= 4) ? (3 - MPEGConst.___003C_003ESQUEEZE_X[chromaFormat]) : 3);
		int blkIdxExt = blkIdx + (dctType << 4);
		int x = BLOCK_POS_X[blkIdxExt];
		int y = BLOCK_POS_Y[blkIdxExt];
		int off = (y << log2stride) + x;
		int stride = 1 << log2stride + stepVert;
		int i = 0;
		while (i < 16)
		{
			int num = off;
			int[] array = @out;
			array[num] += block[i];
			num = off + 1;
			array = @out;
			array[num] += block[i + 1];
			num = off + 2;
			array = @out;
			array[num] += block[i + 2];
			num = off + 3;
			array = @out;
			array[num] += block[i + 3];
			i += 4;
			off += stride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 130, 127, 0, 108, 140, 159, 13, 159,
		19, 159, 21
	})]
	protected internal override void put(int[][] mbPix, byte[][] buf, int stride, int chromaFormat, int mbX, int mbY, int width, int height, int vertOff, int vertStep)
	{
		int chromaStride = stride + (1 << MPEGConst.___003C_003ESQUEEZE_X[chromaFormat]) - 1 >> MPEGConst.___003C_003ESQUEEZE_X[chromaFormat];
		int chromaMBW = 3 - MPEGConst.___003C_003ESQUEEZE_X[chromaFormat];
		int chromaMBH = 3 - MPEGConst.___003C_003ESQUEEZE_Y[chromaFormat];
		putSub(buf[0], (mbY << 3) * (stride << vertStep) + vertOff * stride + (mbX << 3), stride << vertStep, mbPix[0], 3, 3);
		putSub(buf[1], (mbY << chromaMBH) * (chromaStride << vertStep) + vertOff * chromaStride + (mbX << chromaMBW), chromaStride << vertStep, mbPix[1], chromaMBW, chromaMBH);
		putSub(buf[2], (mbY << chromaMBH) * (chromaStride << vertStep) + vertOff * chromaStride + (mbX << chromaMBW), chromaStride << vertStep, mbPix[2], chromaMBW, chromaMBH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 93, 130, 106, 111, 111, 111, 143, 159, 4 })]
	protected internal override Context initContext(SequenceHeader sh, PictureHeader ph)
	{
		Context context = base.initContext(sh, ph);
		context.codedWidth >>= 1;
		context.codedHeight >>= 1;
		context.picWidth >>= 1;
		context.picHeight >>= 1;
		context.scan = scan4x4[(ph.pictureCodingExtension != null) ? ph.pictureCodingExtension.alternate_scan : 0];
		return context;
	}

	[LineNumberTable(new byte[]
	{
		159,
		112,
		98,
		159,
		113,
		byte.MaxValue,
		113,
		160,
		66
	})]
	static Mpeg2Thumb4x4()
	{
		BLOCK_POS_X = new int[28]
		{
			0, 4, 0, 4, 0, 0, 0, 0, 4, 4,
			4, 4, 0, 0, 0, 0, 0, 4, 0, 4,
			0, 0, 0, 0, 4, 4, 4, 4
		};
		BLOCK_POS_Y = new int[28]
		{
			0, 0, 4, 4, 0, 0, 4, 4, 0, 0,
			4, 4, 0, 0, 0, 0, 0, 0, 1, 1,
			0, 0, 1, 1, 0, 0, 1, 1
		};
		scan4x4 = new int[2][]
		{
			new int[64]
			{
				0, 1, 4, 8, 5, 2, 3, 6, 9, 12,
				16, 13, 10, 7, 16, 16, 16, 11, 14, 16,
				16, 16, 16, 16, 15, 16, 16, 16, 16, 16,
				16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
				16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
				16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
				16, 16, 16, 16
			},
			new int[64]
			{
				0, 4, 8, 12, 1, 5, 2, 6, 9, 13,
				16, 16, 16, 16, 16, 16, 16, 16, 14, 10,
				3, 7, 16, 16, 11, 15, 16, 16, 16, 16,
				16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
				16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
				16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
				16, 16, 16, 16
			}
		};
	}
}
