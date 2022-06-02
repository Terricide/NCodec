using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.util;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.io;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.codecs.h264.io.write;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.h264.encode;

public class MBEncoderP16x16 : Object, SaveRestore
{
	private CAVLC[] cavlc;

	private SeqParameterSet sps;

	private Picture @ref;

	private MotionEstimator me;

	private int[] mvTopX;

	private int[] mvTopY;

	private int mvLeftX;

	private int mvLeftY;

	private int mvTopLeftX;

	private int mvTopLeftY;

	private int[] mvTopXSave;

	private int[] mvTopYSave;

	private int mvLeftXSave;

	private int mvLeftYSave;

	private int mvTopLeftXSave;

	private int mvTopLeftYSave;

	private BlockInterpolator interpolator;

	[LineNumberTable(194)]
	private int decideRef()
	{
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 65, 77, 100, 101, 163, 106, 103, 165,
		106, 106, 138
	})]
	public virtual int median(int a, int b, int c, int d, bool aAvb, bool bAvb, bool cAvb, bool dAvb)
	{
		int cAvb2 = (cAvb ? 1 : 0);
		int bAvb2 = (bAvb ? 1 : 0);
		if (cAvb2 == 0)
		{
			c = d;
			cAvb2 = (dAvb ? 1 : 0);
		}
		if (aAvb && bAvb2 == 0 && cAvb2 == 0)
		{
			b = (c = a);
			bAvb2 = (cAvb2 = (aAvb ? 1 : 0));
		}
		a = (aAvb ? a : 0);
		b = ((bAvb2 != 0) ? b : 0);
		c = ((cAvb2 != 0) ? c : 0);
		return a + b + c - Math.min(Math.min(a, b), c) - Math.max(Math.max(a, b), c);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 97, 130, 108, 159, 7 })]
	private int[] mvEstimate(Picture pic, int mbX, int mbY, int mvpx, int mvpy)
	{
		byte[] patch = new byte[256];
		MBEncoderHelper.take(pic.getPlaneData(0), pic.getPlaneWidth(0), pic.getPlaneHeight(0), mbX << 4, mbY << 4, patch, 16, 16);
		int[] result = me.estimate(@ref, patch, mbX, mbY, mvpx, mvpy);
		
		return result;
	}

	[LineNumberTable(178)]
	private int getCodedBlockPattern()
	{
		return 47;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 93, 130, 127, 8, 104, 112, 51, 169, 233,
		60, 231, 71, 151, 106, 109, 106, 113, 53, 233,
		61, 236, 70
	})]
	private void luma(Picture pic, int[] pix, int mbX, int mbY, BitWriter @out, int qp, int[] nc)
	{
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 16);
		int[][] ac = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int j = 0; j < (nint)ac.LongLength; j++)
		{
			for (int l = 0; l < (nint)H264Const.___003C_003EPIX_MAP_SPLIT_4x4[j].LongLength; l++)
			{
				ac[j][l] = pix[H264Const.___003C_003EPIX_MAP_SPLIT_4x4[j][l]];
			}
			CoeffTransformer.fdct4x4(ac[j]);
		}
		writeAC(0, mbX, mbY, @out, mbX << 2, mbY << 2, ac, qp);
		for (int i = 0; i < (nint)ac.LongLength; i++)
		{
			CoeffTransformer.dequantizeAC(ac[i], qp, null);
			CoeffTransformer.idct4x4(ac[i]);
			for (int k = 0; k < (nint)H264Const.___003C_003EPIX_MAP_SPLIT_4x4[i].LongLength; k++)
			{
				pix[H264Const.___003C_003EPIX_MAP_SPLIT_4x4[i][k]] = ac[i][k];
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 88, 98, 127, 7, 127, 7, 106, 113, 53,
		41, 201, 106, 113, 53, 41, 201, 159, 12, 106,
		113, 53, 41, 201, 106, 113, 53, 41, 201
	})]
	private void chroma(Picture pic, int[] pix1, int[] pix2, int mbX, int mbY, BitWriter @out, int qp)
	{
		int[] array = new int[2];
		int num = (array[1] = 16);
		num = (array[0] = 4);
		int[][] ac1 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 16);
		num = (array[0] = 4);
		int[][] ac2 = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		for (int l = 0; l < (nint)ac1.LongLength; l++)
		{
			for (int j3 = 0; j3 < (nint)H264Const.___003C_003EPIX_MAP_SPLIT_2x2[l].LongLength; j3++)
			{
				ac1[l][j3] = pix1[H264Const.___003C_003EPIX_MAP_SPLIT_2x2[l][j3]];
			}
		}
		for (int k = 0; k < (nint)ac2.LongLength; k++)
		{
			for (int j2 = 0; j2 < (nint)H264Const.___003C_003EPIX_MAP_SPLIT_2x2[k].LongLength; j2++)
			{
				ac2[k][j2] = pix2[H264Const.___003C_003EPIX_MAP_SPLIT_2x2[k][j2]];
			}
		}
		MBEncoderI16x16.chromaResidual(pic, mbX, mbY, @out, qp, ac1, ac2, cavlc[1], cavlc[2], MBType.___003C_003EP_16x16, MBType.___003C_003EP_16x16);
		for (int j = 0; j < (nint)ac1.LongLength; j++)
		{
			for (int n = 0; n < (nint)H264Const.___003C_003EPIX_MAP_SPLIT_2x2[j].LongLength; n++)
			{
				pix1[H264Const.___003C_003EPIX_MAP_SPLIT_2x2[j][n]] = ac1[j][n];
			}
		}
		for (int i = 0; i < (nint)ac2.LongLength; i++)
		{
			for (int m = 0; m < (nint)H264Const.___003C_003EPIX_MAP_SPLIT_2x2[i].LongLength; m++)
			{
				pix2[H264Const.___003C_003EPIX_MAP_SPLIT_2x2[i][m]] = ac2[i][m];
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		82,
		66,
		105,
		105,
		108,
		byte.MaxValue,
		33,
		61,
		231,
		70
	})]
	private void writeAC(int comp, int mbX, int mbY, BitWriter @out, int mbLeftBlk, int mbTopBlk, int[][] ac, int qp)
	{
		for (int i = 0; i < (nint)ac.LongLength; i++)
		{
			int blkI = H264Const.___003C_003EBLK_INV_MAP[i];
			CoeffTransformer.quantizeAC(ac[blkI], qp);
			cavlc[comp].writeACBlock(@out, mbLeftBlk + H264Const.___003C_003EMB_BLK_OFF_LEFT[i], mbTopBlk + H264Const.___003C_003EMB_BLK_OFF_TOP[i], MBType.___003C_003EP_16x16, MBType.___003C_003EP_16x16, ac[blkI], H264Const.___003C_003EtotalZeros16, 0, 16, CoeffTransformer.zigzag4x4);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 66, 105, 104, 104, 104, 105, 116, 148,
		116, 116, 108
	})]
	public MBEncoderP16x16(SeqParameterSet sps, Picture @ref, CAVLC[] cavlc, MotionEstimator me)
	{
		this.sps = sps;
		this.cavlc = cavlc;
		this.@ref = @ref;
		this.me = me;
		mvTopX = new int[sps.picWidthInMbsMinus1 + 1];
		mvTopY = new int[sps.picWidthInMbsMinus1 + 1];
		mvTopXSave = new int[sps.picWidthInMbsMinus1 + 1];
		mvTopYSave = new int[sps.picWidthInMbsMinus1 + 1];
		interpolator = new BlockInterpolator();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 162, 109, 46, 135, 123, 155, 109, 109,
		109, 109
	})]
	public virtual void save()
	{
		for (int i = 0; i < (nint)cavlc.LongLength; i++)
		{
			cavlc[i].save();
		}
		ByteCodeHelper.arraycopy_primitive_4(mvTopX, 0, mvTopXSave, 0, mvTopX.Length);
		ByteCodeHelper.arraycopy_primitive_4(mvTopY, 0, mvTopYSave, 0, mvTopY.Length);
		mvLeftXSave = mvLeftX;
		mvLeftYSave = mvLeftY;
		mvTopLeftXSave = mvTopLeftX;
		mvTopLeftYSave = mvTopLeftY;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 66, 109, 46, 135, 104, 109, 136, 104,
		109, 136, 109, 109, 109, 109
	})]
	public virtual void restore()
	{
		for (int i = 0; i < (nint)cavlc.LongLength; i++)
		{
			cavlc[i].restore();
		}
		int[] tmp = mvTopX;
		mvTopX = mvTopXSave;
		mvTopXSave = tmp;
		tmp = mvTopY;
		mvTopY = mvTopYSave;
		mvTopYSave = tmp;
		mvLeftX = mvLeftXSave;
		mvLeftY = mvLeftYSave;
		mvTopLeftX = mvTopLeftXSave;
		mvTopLeftY = mvTopLeftYSave;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 130, 111, 104, 182, 120, 110, 159, 29,
		223, 30, 111, 111, 111, 109, 109, 107, 107, 110,
		143, 119, 159, 11, 159, 10, 127, 9, 63, 2,
		134, 127, 9, 63, 2, 166, 127, 4, 42, 134,
		127, 4, 40, 134, 127, 4, 40, 166, 105, 144,
		138, 121, 150, 127, 7, 127, 5, 159, 5, 113,
		113, 109, 138, 115
	})]
	public virtual void encodeMacroblock(Picture pic, int mbX, int mbY, BitWriter @out, EncodedMB outMB, EncodedMB leftOutMB, EncodedMB topOutMB, int qp, int qpDelta)
	{
		if (sps.numRefFrames > 1)
		{
			int refIdx = decideRef();
			CAVLCWriter.writeTE(@out, refIdx, sps.numRefFrames - 1);
		}
		int trAvb = ((mbY > 0 && mbX < sps.picWidthInMbsMinus1) ? 1 : 0);
		int tlAvb = ((mbX > 0 && mbY > 0) ? 1 : 0);
		int mvpx = median(mvLeftX, mvTopX[mbX], (trAvb != 0) ? mvTopX[mbX + 1] : 0, (tlAvb != 0) ? mvTopLeftX : 0, mbX > 0, mbY > 0, (byte)trAvb != 0, (byte)tlAvb != 0);
		int mvpy = median(mvLeftY, mvTopY[mbX], (trAvb != 0) ? mvTopY[mbX + 1] : 0, (tlAvb != 0) ? mvTopLeftY : 0, mbX > 0, mbY > 0, (byte)trAvb != 0, (byte)tlAvb != 0);
		int[] mv = mvEstimate(pic, mbX, mbY, mvpx, mvpy);
		mvTopLeftX = mvTopX[mbX];
		mvTopLeftY = mvTopY[mbX];
		mvTopX[mbX] = mv[0];
		mvTopY[mbX] = mv[1];
		mvLeftX = mv[0];
		mvLeftY = mv[1];
		CAVLCWriter.writeSE(@out, mv[0] - mvpx);
		CAVLCWriter.writeSE(@out, mv[1] - mvpy);
		Picture mbRef = Picture.create(16, 16, sps.chromaFormatIdc);
		int[][] mb = new int[3][]
		{
			new int[256],
			new int[64],
			new int[64]
		};
		interpolator.getBlockLuma(@ref, mbRef, 0, (mbX << 6) + mv[0], (mbY << 6) + mv[1], 16, 16);
		BlockInterpolator.getBlockChroma(@ref.getPlaneData(1), @ref.getPlaneWidth(1), @ref.getPlaneHeight(1), mbRef.getPlaneData(1), 0, mbRef.getPlaneWidth(1), (mbX << 6) + mv[0], (mbY << 6) + mv[1], 8, 8);
		BlockInterpolator.getBlockChroma(@ref.getPlaneData(2), @ref.getPlaneWidth(2), @ref.getPlaneHeight(2), mbRef.getPlaneData(2), 0, mbRef.getPlaneWidth(2), (mbX << 6) + mv[0], (mbY << 6) + mv[1], 8, 8);
		MBEncoderHelper.takeSubtract(pic.getPlaneData(0), pic.getPlaneWidth(0), pic.getPlaneHeight(0), mbX << 4, mbY << 4, mb[0], mbRef.getPlaneData(0), 16, 16);
		MBEncoderHelper.takeSubtract(pic.getPlaneData(1), pic.getPlaneWidth(1), pic.getPlaneHeight(1), mbX << 3, mbY << 3, mb[1], mbRef.getPlaneData(1), 8, 8);
		MBEncoderHelper.takeSubtract(pic.getPlaneData(2), pic.getPlaneWidth(2), pic.getPlaneHeight(2), mbX << 3, mbY << 3, mb[2], mbRef.getPlaneData(2), 8, 8);
		int codedBlockPattern = getCodedBlockPattern();
		CAVLCWriter.writeUE(@out, H264Const.___003C_003ECODED_BLOCK_PATTERN_INTER_COLOR_INV[codedBlockPattern]);
		CAVLCWriter.writeSE(@out, qpDelta);
		luma(pic, mb[0], mbX, mbY, @out, qp, outMB.getNc());
		chroma(pic, mb[1], mb[2], mbX, mbY, @out, qp);
		MBEncoderHelper.putBlk(outMB.getPixels().getPlaneData(0), mb[0], mbRef.getPlaneData(0), 4, 0, 0, 16, 16);
		MBEncoderHelper.putBlk(outMB.getPixels().getPlaneData(1), mb[1], mbRef.getPlaneData(1), 3, 0, 0, 8, 8);
		MBEncoderHelper.putBlk(outMB.getPixels().getPlaneData(2), mb[2], mbRef.getPlaneData(2), 3, 0, 0, 8, 8);
		Arrays.fill(outMB.getMx(), mv[0]);
		Arrays.fill(outMB.getMy(), mv[1]);
		outMB.setType(MBType.___003C_003EP_16x16);
		outMB.setQp(qp);
		new MBDeblocker().deblockMBP(outMB, leftOutMB, topOutMB);
	}
}
