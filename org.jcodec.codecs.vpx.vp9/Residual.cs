using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.vpx.vp9;

public class Residual : Object
{
	private int[][][] coefs;

	public static int[][] blk_size_lookup;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 103, 111 })]
	public static Residual readResidual(int miCol, int miRow, int blSz, VPXBooleanDecoder decoder, DecodingContext c, ModeInfo mode)
	{
		Residual ret = new Residual();
		ret.read(miCol, miRow, blSz, decoder, c, mode);
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 105 })]
	protected internal Residual()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		128,
		162,
		106,
		98,
		112,
		144,
		104,
		106,
		118,
		121,
		138,
		111,
		111,
		100,
		112,
		144,
		117,
		117,
		102,
		102,
		100,
		114,
		114,
		112,
		144,
		112,
		144,
		110,
		109,
		109,
		104,
		136,
		100,
		106,
		101,
		152,
		106,
		byte.MaxValue,
		14,
		54,
		45,
		237,
		39,
		234,
		105,
		104
	})]
	public virtual void read(int miCol, int miRow, int blType, VPXBooleanDecoder decoder, DecodingContext c, ModeInfo modeInfo)
	{
		if (modeInfo.isSkip())
		{
			return;
		}
		int subXRound = (1 << c.getSubX()) - 1;
		int subYRound = (1 << c.getSubY()) - 1;
		int[][][] coefs = new int[3][][];
		for (int pl = 0; pl < 3; pl++)
		{
			int txSize = ((pl != 0) ? Consts.___003C_003Euv_txsize_lookup[blType][modeInfo.getTxSize()][c.getSubX()][c.getSubY()] : modeInfo.getTxSize());
			int step4x4 = 1 << txSize;
			int n4w = 1 << Consts.___003C_003EblW[blType];
			int n4h = 1 << Consts.___003C_003EblH[blType];
			if (pl != 0)
			{
				n4w >>= c.getSubX();
				n4h >>= c.getSubY();
			}
			int extra4w = (miCol << 1) + n4w - (c.getFrameWidth() + 3 >> 2);
			int extra4h = (miRow << 1) + n4h - (c.getFrameHeight() + 3 >> 2);
			int startBlkX = miCol << 1;
			int startBlkY = miRow << 1;
			if (pl != 0)
			{
				extra4w = extra4w + subXRound >> c.getSubX();
				extra4h = extra4h + subYRound >> c.getSubY();
				startBlkX >>= c.getSubX();
				startBlkY >>= c.getSubY();
			}
			int max4w = n4w - ((extra4w > 0) ? extra4w : 0);
			int max4h = n4h - ((extra4h > 0) ? extra4h : 0);
			coefs[pl] = new int[n4w * n4h][];
			for (int y = 0; y < max4h; y += step4x4)
			{
				for (int x = 0; x < max4w; x += step4x4)
				{
					int blkCol = startBlkX + x;
					int blkRow = startBlkY + y;
					int predMode;
					if (pl == 0)
					{
						predMode = modeInfo.getYMode();
						if (blType < 3)
						{
							predMode = ModeInfo.vect4get(modeInfo.getSubModes(), (y << 1) + x);
						}
					}
					else
					{
						predMode = modeInfo.getUvMode();
					}
					coefs[pl][x + n4w * y] = readOneTU((pl != 0) ? 1 : 0, blkCol, blkRow, txSize, modeInfo.isInter(), predMode, decoder, c);
				}
			}
		}
		this.coefs = coefs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		114,
		161,
		68,
		113,
		108,
		99,
		116,
		159,
		3,
		159,
		3,
		105,
		111,
		108,
		121,
		104,
		159,
		1,
		100,
		117,
		101,
		166,
		110,
		102,
		136,
		99,
		110,
		102,
		137,
		123,
		102,
		101,
		102,
		136,
		136,
		102,
		136,
		102,
		174,
		106,
		146,
		126,
		byte.MaxValue,
		7,
		25,
		236,
		105
	})]
	public virtual int[] readOneTU(int plane, int blkCol, int blkRow, int txSz, bool isInter, int intraMode, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int[] tokenCache = new int[16 << (txSz << 1)];
		int maxCoeff = 16 << (txSz << 1);
		int expectMoreCoefs = 0;
		int txType = ((plane == 0 && !isInter) ? Consts.___003C_003Eintra_mode_to_tx_type_lookup[intraMode] : 0);
		int[] scan = ((plane != 0 || isInter) ? Scan.___003C_003Evp9_default_scan_orders[txSz][0] : Scan.___003C_003Evp9_scan_orders[txSz][txType][0]);
		int[] neighbors = ((plane != 0 || isInter) ? Scan.___003C_003Evp9_default_scan_orders[txSz][2] : Scan.___003C_003Evp9_scan_orders[txSz][txType][2]);
		int[] coefs = new int[maxCoeff];
		int ctx = calcTokenContextCoef0(plane, txSz, blkCol, blkRow, c);
		for (int cf = 0; cf < maxCoeff; cf++)
		{
			int band = ((txSz != 0) ? Consts.___003C_003Ecoefband_8x8plus[cf] : Consts.___003C_003Ecoefband_4x4[cf]);
			int pos = scan[cf];
			int[] probs = c.getCoefProbs()[txSz][(plane > 0) ? 1u : 0u][isInter ? 1 : 0][band][ctx];
			if (expectMoreCoefs == 0 && decoder.readBit(probs[0]) != 1 && 0 == 0)
			{
				break;
			}
			if (decoder.readBit(probs[1]) == 0)
			{
				tokenCache[pos] = 0;
				expectMoreCoefs = 1;
			}
			else
			{
				expectMoreCoefs = 0;
				int coef;
				if (decoder.readBit(probs[2]) == 0)
				{
					tokenCache[pos] = 1;
					coef = 1;
				}
				else
				{
					int token = decoder.readTree(Consts.___003C_003ETOKEN_TREE, Consts.___003C_003EPARETO_TABLE[probs[2] - 1]);
					if (token < 5)
					{
						coef = token;
						if (token == 2)
						{
							tokenCache[pos] = 2;
						}
						else
						{
							tokenCache[pos] = 3;
						}
					}
					else
					{
						if (token < 7)
						{
							tokenCache[pos] = 4;
						}
						else
						{
							tokenCache[pos] = 5;
						}
						coef = readCoef(token, decoder, c);
					}
				}
				int sign = decoder.readBitEq();
				coefs[pos] = ((sign != 1) ? coef : (-coef));
			}
			ctx = 1 + tokenCache[neighbors[2 * cf + 2]] + tokenCache[neighbors[2 * cf + 3]] >> 1;
			java.lang.System.@out.println(new StringBuilder().append("CTX: ").append(ctx).toString());
		}
		return coefs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 95, 98, 105, 105, 112, 112, 113, 113, 105,
		100, 100, 106, 105, 110, 105, 241, 60, 233, 70
	})]
	private static int calcTokenContextCoef0(int plane, int txSz, int blkCol, int blkRow, DecodingContext c)
	{
		int[][] aboveNonzeroContext = c.getAboveNonzeroContext();
		int[][] leftNonzeroContext = c.getLeftNonzeroContext();
		int subX = ((plane > 0) ? c.getSubX() : 0);
		int subY = ((plane > 0) ? c.getSubY() : 0);
		int max4x = c.getMiFrameWidth() << 1 >> subX;
		int max4y = c.getMiFrameHeight() << 1 >> subY;
		int tx4 = 1 << txSz;
		int aboveNz = 0;
		int leftNz = 0;
		for (int i = 0; i < tx4; i++)
		{
			if (blkCol + i < max4x)
			{
				aboveNz |= aboveNonzeroContext[plane][blkCol + i];
			}
			if (blkRow + i < max4y)
			{
				leftNz |= leftNonzeroContext[plane][(blkRow + i) & 0xF];
			}
		}
		return aboveNz + leftNz;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 100, 130, 107, 107, 235, 72, 103, 146, 239,
		61, 231, 69
	})]
	private static int readCoef(int token, VPXBooleanDecoder decoder, DecodingContext c)
	{
		int cat = Consts.___003C_003Eextra_bits[token][0];
		int numExtra = Consts.___003C_003Eextra_bits[token][1];
		int coef = Consts.___003C_003Eextra_bits[token][2];
		for (int bit = 0; bit < numExtra; bit++)
		{
			int coef_bit = decoder.readBit(Consts.___003C_003Ecat_probs[cat][bit]);
			coef += coef_bit << numExtra - 1 - bit;
		}
		return coef;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 105, 104 })]
	public Residual(int[][][] coefs)
	{
		this.coefs = coefs;
	}

	[LineNumberTable(208)]
	public virtual int[][][] getCoefs()
	{
		return coefs;
	}

	[LineNumberTable(109)]
	static Residual()
	{
		blk_size_lookup = new int[5][]
		{
			new int[3] { -1, 0, 2 },
			new int[3] { 1, 3, 5 },
			new int[3] { 4, 6, 8 },
			new int[3] { 7, 9, 11 },
			new int[3] { 10, 12, -1 }
		};
	}
}
