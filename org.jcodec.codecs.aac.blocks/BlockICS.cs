using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;
using org.jcodec.codecs.prores;
using org.jcodec.common.io;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.aac.blocks;

public class BlockICS : Block
{
	[Serializable]
	[InnerClass(null, Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/aac/blocks/BlockICS$BandType;>;")]
	[Modifiers(Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public class BandType : java.lang.Enum
	{
		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType ZERO_BT;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_1;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_2;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_3;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_4;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType FIRST_PAIR_BT;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_6;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_7;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_8;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_9;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_10;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType ESC_BT;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType BT_12;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType NOISE_BT;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType INTENSITY_BT2;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static BandType INTENSITY_BT;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static BandType[] _0024VALUES;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(171)]
		private BandType(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(171)]
		public static BandType[] values()
		{
			return (BandType[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(171)]
		public static BandType valueOf(string name)
		{
			return (BandType)java.lang.Enum.valueOf(ClassLiteral<BandType>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 99, 66, 63, 160, 169 })]
		static BandType()
		{
			ZERO_BT = new BandType("ZERO_BT", 0);
			BT_1 = new BandType("BT_1", 1);
			BT_2 = new BandType("BT_2", 2);
			BT_3 = new BandType("BT_3", 3);
			BT_4 = new BandType("BT_4", 4);
			FIRST_PAIR_BT = new BandType("FIRST_PAIR_BT", 5);
			BT_6 = new BandType("BT_6", 6);
			BT_7 = new BandType("BT_7", 7);
			BT_8 = new BandType("BT_8", 8);
			BT_9 = new BandType("BT_9", 9);
			BT_10 = new BandType("BT_10", 10);
			ESC_BT = new BandType("ESC_BT", 11);
			BT_12 = new BandType("BT_12", 12);
			NOISE_BT = new BandType("NOISE_BT", 13);
			INTENSITY_BT2 = new BandType("INTENSITY_BT2", 14);
			INTENSITY_BT = new BandType("INTENSITY_BT", 15);
			_0024VALUES = new BandType[16]
			{
				ZERO_BT, BT_1, BT_2, BT_3, BT_4, FIRST_PAIR_BT, BT_6, BT_7, BT_8, BT_9,
				BT_10, ESC_BT, BT_12, NOISE_BT, INTENSITY_BT2, INTENSITY_BT
			};
		}
	}

	public class Pulse : java.lang.Object
	{
		private int numPulse;

		private int[] pos;

		private int[] amp;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 81, 98, 105, 104, 104, 104 })]
		public Pulse(int numPulse, int[] pos, int[] amp)
		{
			this.numPulse = numPulse;
			this.pos = pos;
			this.amp = amp;
		}

		[LineNumberTable(252)]
		public virtual int getNumPulse()
		{
			return numPulse;
		}

		[LineNumberTable(256)]
		public virtual int[] getPos()
		{
			return pos;
		}

		[LineNumberTable(260)]
		public virtual int[] getAmp()
		{
			return amp;
		}
	}

	public class Tns : java.lang.Object
	{
		private int[] nFilt;

		private int[][] length;

		private int[][] order;

		private int[][] direction;

		private float[][][] coeff;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 69, 130, 105, 104, 104, 104, 105, 105 })]
		public Tns(int[] nFilt, int[][] length, int[][] order, int[][] direction, float[][][] coeff)
		{
			this.nFilt = nFilt;
			this.length = length;
			this.order = order;
			this.direction = direction;
			this.coeff = coeff;
		}
	}

	[Serializable]
	[InnerClass(null, Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/aac/blocks/BlockICS$WindowSequence;>;")]
	[Modifiers(Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	internal sealed class WindowSequence : java.lang.Enum
	{
		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static WindowSequence ONLY_LONG_SEQUENCE;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static WindowSequence LONG_START_SEQUENCE;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static WindowSequence EIGHT_SHORT_SEQUENCE;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static WindowSequence LONG_STOP_SEQUENCE;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static WindowSequence[] _0024VALUES;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(78)]
		private WindowSequence(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(78)]
		public static WindowSequence[] values()
		{
			return (WindowSequence[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(78)]
		public static WindowSequence valueOf(string name)
		{
			return (WindowSequence)java.lang.Enum.valueOf(ClassLiteral<WindowSequence>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 123, 162, 63, 34 })]
		static WindowSequence()
		{
			ONLY_LONG_SEQUENCE = new WindowSequence("ONLY_LONG_SEQUENCE", 0);
			LONG_START_SEQUENCE = new WindowSequence("LONG_START_SEQUENCE", 1);
			EIGHT_SHORT_SEQUENCE = new WindowSequence("EIGHT_SHORT_SEQUENCE", 2);
			LONG_STOP_SEQUENCE = new WindowSequence("LONG_STOP_SEQUENCE", 3);
			_0024VALUES = new WindowSequence[4] { ONLY_LONG_SEQUENCE, LONG_START_SEQUENCE, EIGHT_SHORT_SEQUENCE, LONG_STOP_SEQUENCE };
		}
	}

	private bool commonWindow;

	private bool scaleFlag;

	private Profile profile;

	private int samplingIndex;

	private static VLC[] spectral;

	private static VLC vlc;

	internal float[][] ff_aac_codebook_vector_vals;

	private const int MAX_LTP_LONG_SFB = 40;

	private int windowSequence;

	internal int num_window_groups;

	private int[] group_len;

	internal int maxSfb;

	private int[] band_type;

	private int[] band_type_run_end;

	private int globalGain;

	internal static float[] ff_aac_pow2sf_tab;

	private const int POW_SF2_ZERO = 200;

	private double[] sfs;

	private int numSwb;

	private int[] swbOffset;

	private int numWindows;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		126,
		162,
		105,
		byte.MaxValue,
		73,
		69,
		109,
		110,
		110
	})]
	public BlockICS()
	{
		ff_aac_codebook_vector_vals = new float[11][]
		{
			AACTab.codebook_vector0_vals,
			AACTab.codebook_vector0_vals,
			AACTab.codebook_vector10_vals,
			AACTab.codebook_vector10_vals,
			AACTab.codebook_vector4_vals,
			AACTab.codebook_vector4_vals,
			AACTab.codebook_vector10_vals,
			AACTab.codebook_vector10_vals,
			AACTab.codebook_vector10_vals,
			AACTab.codebook_vector10_vals,
			AACTab.codebook_vector10_vals
		};
		group_len = new int[8];
		band_type = new int[120];
		band_type_run_end = new int[120];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 7, 66, 142, 113, 169, 104, 136, 131, 105,
		107, 115, 145, 137, 107, 137, 105, 209, 106
	})]
	public override void parse(BitReader _in)
	{
		globalGain = _in.readNBit(8);
		if (!commonWindow && !scaleFlag)
		{
			parseICSInfo(_in);
		}
		decodeBandTypes(_in);
		decodeScalefactors(_in);
		int pulse_present = 0;
		if (!scaleFlag)
		{
			if ((pulse_present = _in.read1Bit()) != 0)
			{
				if (windowSequence == WindowSequence.EIGHT_SHORT_SEQUENCE.ordinal())
				{
					throw new RuntimeException("Pulse tool not allowed _in eight short sequence.");
				}
				decodePulses(_in);
			}
			int tns_present;
			if ((tns_present = _in.read1Bit()) != 0)
			{
				decodeTns(_in);
			}
			if (_in.read1Bit() != 0)
			{
				throw new RuntimeException("SSR is not supported");
			}
		}
		decodeSpectrum(_in);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 111, 130, 105, 137, 120, 40, 167 })]
	private void decodePrediction(BitReader _in, int maxSfb)
	{
		if (_in.read1Bit() != 0)
		{
			int num = _in.readNBit(5);
		}
		for (int sfb = 0; sfb < java.lang.Math.min(maxSfb, AACTab.maxSfbTab[samplingIndex]); sfb++)
		{
			_in.read1Bit();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 66, 106, 111, 110, 40, 135 })]
	private void decodeLtp(BitReader _in, int maxSfb)
	{
		int lag = _in.readNBit(11);
		float coef = AACTab.ltpCoefTab[_in.readNBit(3)];
		for (int sfb = 0; sfb < java.lang.Math.min(maxSfb, 40); sfb++)
		{
			_in.read1Bit();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 16, 98, 107, 107, 151, 103, 100, 164, 107,
		119, 103, 104, 230, 55, 242, 75
	})]
	private void readBandType1And2(BitReader _in, float[] coef, int idx, int g, int sfb, float[] vq, VLC vlc)
	{
		int g_len = group_len[g];
		int cfo = swbOffset[sfb];
		int off_len = swbOffset[sfb + 1] - swbOffset[sfb];
		int group = 0;
		while (group < g_len)
		{
			int cf = cfo;
			int len = off_len;
			do
			{
				int cb_idx = vlc.readVLC(_in);
				VMUL4(coef, cf, vq, cb_idx, (float)sfs[idx]);
				cf += 4;
				len += -4;
			}
			while (len > 0);
			group++;
			cfo += 128;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 39, 98, 107, 107, 151, 106, 100, 164, 107,
		106, 114, 121, 103, 104, 230, 53, 242, 77
	})]
	private void readBandType3And4(BitReader _in, float[] coef, int idx, int g, int sfb, float[] vq, VLC vlc)
	{
		int g_len = group_len[g];
		int cfo = swbOffset[sfb];
		int off_len = swbOffset[sfb + 1] - swbOffset[sfb];
		int group = 0;
		while (group < g_len)
		{
			int cf = cfo;
			int len = off_len;
			do
			{
				int cb_idx = vlc.readVLC(_in);
				int nnz = (cb_idx >> 8) & 0xF;
				int bits = ((nnz != 0) ? _in.readNBit(nnz) : 0);
				VMUL4S(coef, cf, vq, cb_idx, bits, (float)sfs[idx]);
				cf += 4;
				len += -4;
			}
			while (len > 0);
			group++;
			cfo += 128;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 12, 162, 107, 107, 119, 103, 100, 164, 107,
		119, 103, 104, 230, 55, 242, 75
	})]
	private void readBandType5And6(BitReader _in, float[] coef, int idx, int g, int sfb, float[] vq, VLC vlc)
	{
		int g_len = group_len[g];
		int cfo = swbOffset[sfb];
		int off_len = swbOffset[sfb + 1] - swbOffset[sfb];
		int group = 0;
		while (group < g_len)
		{
			int cf = cfo;
			int len = off_len;
			do
			{
				int cb_idx = vlc.readVLC(_in);
				VMUL2(coef, cf, vq, cb_idx, (float)sfs[idx]);
				cf += 2;
				len += -2;
			}
			while (len > 0);
			group++;
			cfo += 128;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 34, 98, 107, 107, 151, 106, 100, 164, 107,
		106, 123, 121, 103, 104, 233, 53, 242, 77
	})]
	private void readBandType7Through10(BitReader _in, float[] coef, int idx, int g, int sfb, float[] vq, VLC vlc)
	{
		int g_len = group_len[g];
		int cfo = swbOffset[sfb];
		int off_len = swbOffset[sfb + 1] - swbOffset[sfb];
		int group = 0;
		while (group < g_len)
		{
			int cf = cfo;
			int len = off_len;
			do
			{
				int cb_idx = vlc.readVLC(_in);
				int nnz = (cb_idx >> 8) & 0xF;
				int bits = ((nnz != 0) ? (_in.readNBit(nnz) << (cb_idx >> 12)) : 0);
				VMUL2S(coef, cf, vq, cb_idx, bits, (float)sfs[idx]);
				cf += 2;
				len += -2;
			}
			while (len > 0);
			group++;
			cfo += 128;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 29, 98, 107, 107, 151, 106, 100, 164, 139,
		104, 104, 103, 148, 108, 240, 73, 146, 102, 177,
		108, 103, 115, 124, 103, 99, 112, 183, 231, 38,
		236, 92, 103, 135, 233, 21, 242, 109
	})]
	private void readOther(BitReader _in, float[] coef, int idx, int g, int sfb, float[] vq, VLC vlc)
	{
		int g_len = group_len[g];
		int cfo = swbOffset[sfb];
		int off_len = swbOffset[sfb + 1] - swbOffset[sfb];
		int group = 0;
		while (group < g_len)
		{
			int cf = cfo;
			int len = off_len;
			do
			{
				int cb_idx = vlc.readVLC(_in);
				if (cb_idx == 0)
				{
					continue;
				}
				int nnz = cb_idx >> 12;
				int nzt = cb_idx >> 8;
				int bits = _in.readNBit(nnz) << 32 - nnz;
				for (int i = 0; i < 2; i++)
				{
					if ((nzt & (1 << i)) != 0)
					{
						int b = ProresDecoder.nZeros(_in.checkNBit(14) ^ -1);
						if (b > 8)
						{
							throw new RuntimeException("error _in spectral data, ESC overflow\n");
						}
						_in.skip(b + 1);
						b += 4;
						int j = (1 << b) + _in.readNBit(b);
						int num = cf;
						cf++;
						coef[num] = MathUtil.cubeRoot(j) | (bits & int.MinValue);
						bits <<= 1;
					}
					else
					{
						int v = ByteCodeHelper.f2i(vq[cb_idx & 0xF]);
						int num2 = cf;
						cf++;
						coef[num2] = (bits & int.MinValue) | v;
					}
					cb_idx >>= 4;
				}
				cf += 2;
				len += 2;
			}
			while (len > 0);
			group++;
			cfo += 128;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 56, 130, 167, 144, 108, 133, 146, 108, 133,
		146, 108, 133, 114
	})]
	internal virtual void VMUL4S(float[] result, int idx, float[] v, int code, int sign, float scale)
	{
		int nz = code >> 12;
		result[idx + 0] = v[idx & 3] * scale;
		sign <<= nz & 1;
		nz >>= 1;
		result[idx + 1] = v[(idx >> 2) & 3] * scale;
		sign <<= nz & 1;
		nz >>= 1;
		result[idx + 2] = v[(idx >> 4) & 3] * scale;
		sign <<= nz & 1;
		nz >>= 1;
		result[idx + 3] = v[(idx >> 6) & 3] * scale;
	}

	[LineNumberTable(new byte[] { 159, 49, 98, 112, 116 })]
	internal virtual void VMUL2S(float[] result, int idx, float[] v, int code, int sign, float scale)
	{
		result[idx] = v[code & 0xF] * scale;
		result[idx + 1] = v[(code >> 4) & 0xF] * scale;
	}

	[LineNumberTable(new byte[] { 159, 58, 162, 111, 115, 115, 115 })]
	internal virtual void VMUL4(float[] result, int idx, float[] v, int code, float scale)
	{
		result[idx] = v[code & 3] * scale;
		result[idx + 1] = v[(code >> 2) & 3] * scale;
		result[idx + 2] = v[(code >> 4) & 3] * scale;
		result[idx + 3] = v[(code >> 6) & 3] * scale;
	}

	[LineNumberTable(new byte[] { 159, 50, 66, 112, 116 })]
	internal virtual void VMUL2(float[] result, int idx, float[] v, int code, float scale)
	{
		result[idx] = v[code & 0xF] * scale;
		result[idx + 1] = v[(code >> 4) & 0xF] * scale;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 162, 104, 110, 104, 104, 106, 118, 137,
		103, 105, 158, 111, 241, 59, 231, 72, 115, 115,
		104, 102, 110, 115, 115, 136, 105, 101, 110, 112,
		110, 145, 105, 101, 238, 69
	})]
	protected internal virtual int parseICSInfo(BitReader _in)
	{
		_in.read1Bit();
		windowSequence = _in.readNBit(2);
		int useKbWindow = _in.read1Bit();
		num_window_groups = 1;
		group_len[0] = 1;
		if (windowSequence == WindowSequence.EIGHT_SHORT_SEQUENCE.ordinal())
		{
			int max_sfb = _in.readNBit(4);
			for (int i = 0; i < 7; i++)
			{
				if (_in.read1Bit() != 0)
				{
					int[] array = group_len;
					int num = num_window_groups - 1;
					int[] array2 = array;
					array2[num]++;
				}
				else
				{
					num_window_groups++;
					group_len[num_window_groups - 1] = 1;
				}
			}
			numSwb = AACTab.ff_aac_num_swb_128[samplingIndex];
			swbOffset = AACTab.ff_swb_offset_128[samplingIndex];
			numWindows = 8;
		}
		else
		{
			maxSfb = _in.readNBit(6);
			numSwb = AACTab.ff_aac_num_swb_1024[samplingIndex];
			swbOffset = AACTab.ff_swb_offset_1024[samplingIndex];
			numWindows = 1;
			if (_in.read1Bit() != 0)
			{
				if (profile == Profile.___003C_003EMAIN)
				{
					decodePrediction(_in, maxSfb);
				}
				else
				{
					if (profile == Profile.___003C_003ELC)
					{
						throw new RuntimeException("Prediction is not allowed _in AAC-LC.\n");
					}
					if (_in.read1Bit() != 0)
					{
						decodeLtp(_in, maxSfb);
					}
				}
			}
		}
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 162, 99, 120, 111, 99, 109, 132, 106,
		103, 145, 117, 106, 104, 117, 145, 107, 159, 21,
		102, 107, 15, 199, 230, 42, 234, 88
	})]
	private void decodeBandTypes(BitReader _in)
	{
		int idx = 0;
		int bits = ((windowSequence != WindowSequence.EIGHT_SHORT_SEQUENCE.ordinal()) ? 5 : 3);
		for (int g = 0; g < num_window_groups; g++)
		{
			int i = 0;
			while (i < maxSfb)
			{
				int sect_end = i;
				int sect_band_type = _in.readNBit(4);
				if (sect_band_type == 12)
				{
					throw new RuntimeException("invalid band type");
				}
				int sect_len_incr;
				while ((sect_len_incr = _in.readNBit(bits)) == (1 << bits) - 1)
				{
					sect_end += sect_len_incr;
				}
				sect_end += sect_len_incr;
				if (!_in.moreData() || sect_len_incr == (1 << bits) - 1)
				{
					throw new RuntimeException("Overread");
				}
				if (sect_end > maxSfb)
				{
					string message = java.lang.String.format("Number of bands (%d) exceeds limit (%d).\n", Integer.valueOf(sect_end), Integer.valueOf(maxSfb));
					throw new RuntimeException(message);
				}
				for (; i < sect_end; i++)
				{
					band_type[idx] = sect_band_type;
					int[] array = band_type_run_end;
					int num = idx;
					idx++;
					array[num] = sect_end;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 95, 98, 159, 2, 99, 127, 1, 99, 113,
		113, 107, 117, 106, 46, 141, 127, 13, 106, 127,
		1, 114, 104, 150, 107, 231, 61, 235, 69, 249,
		55, 240, 75, 120, 106, 105, 159, 3, 127, 1,
		114, 104, 150, 17, 203, 249, 53, 240, 78, 106,
		127, 1, 107, 159, 14, 253, 59, 240, 72, 230,
		23, 236, 107
	})]
	private void decodeScalefactors(BitReader _in)
	{
		int[] offset = new int[3]
		{
			globalGain,
			globalGain - 90,
			0
		};
		int noise_flag = 1;
		string[] sf_str = new string[3] { "Global gain", "Noise gain", "Intensity stereo position" };
		int idx = 0;
		for (int g = 0; g < num_window_groups; g++)
		{
			int i = 0;
			while (i < maxSfb)
			{
				int run_end = band_type_run_end[idx];
				if (band_type[idx] == BandType.ZERO_BT.ordinal())
				{
					while (i < run_end)
					{
						sfs[idx] = 0.0;
						i++;
						idx++;
					}
					continue;
				}
				if (band_type[idx] == BandType.INTENSITY_BT.ordinal() || band_type[idx] == BandType.INTENSITY_BT2.ordinal())
				{
					while (i < run_end)
					{
						int num = 2;
						int[] array = offset;
						array[num] += vlc.readVLC(_in) - 60;
						int clipped_offset = MathUtil.clip(offset[2], -155, 100);
						if (offset[2] != clipped_offset)
						{
							java.lang.System.@out.println(java.lang.String.format("Intensity stereo position clipped (%d -> %d).\nIf you heard an audible artifact, there may be a bug _in the decoder. ", Integer.valueOf(offset[2]), Integer.valueOf(clipped_offset)));
						}
						sfs[idx] = ff_aac_pow2sf_tab[-clipped_offset + 200];
						i++;
						idx++;
					}
					continue;
				}
				if (band_type[idx] == BandType.NOISE_BT.ordinal())
				{
					while (i < run_end)
					{
						int num2 = noise_flag;
						noise_flag += -1;
						if (num2 > 0)
						{
							int num = 1;
							int[] array = offset;
							array[num] += _in.readNBit(9) - 256;
						}
						else
						{
							int num = 1;
							int[] array = offset;
							array[num] += vlc.readVLC(_in) - 60;
						}
						int clipped_offset2 = MathUtil.clip(offset[1], -100, 155);
						if (offset[1] != clipped_offset2)
						{
							java.lang.System.@out.println(java.lang.String.format("Noise gain clipped (%d -> %d).\nIf you heard an audible artifact, there may be a bug _in the decoder. ", Integer.valueOf(offset[1]), Integer.valueOf(clipped_offset2)));
						}
						sfs[idx] = 0f - ff_aac_pow2sf_tab[clipped_offset2 + 200];
						i++;
						idx++;
					}
					continue;
				}
				while (i < run_end)
				{
					int num = 0;
					int[] array = offset;
					array[num] += vlc.readVLC(_in) - 60;
					if (offset[0] > 255)
					{
						string message = java.lang.String.format("%s (%d) out of range.\n", sf_str[0], Integer.valueOf(offset[0]));
						throw new RuntimeException(message);
					}
					sfs[idx] = 0f - ff_aac_pow2sf_tab[offset[0] - 100 + 200];
					i++;
					idx++;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 76, 98, 104, 136, 107, 105, 106, 113, 108,
		121, 107, 113, 107, 108, 115, 108, 127, 18, 236,
		60, 236, 70
	})]
	private Pulse decodePulses(BitReader _in)
	{
		int[] pos = new int[4];
		int[] amp = new int[4];
		int numPulse = _in.readNBit(2) + 1;
		int pulseSwb = _in.readNBit(6);
		if (pulseSwb >= numSwb)
		{
			throw new RuntimeException("pulseSwb >= numSwb");
		}
		pos[0] = swbOffset[pulseSwb];
		int num = 0;
		int[] array = pos;
		array[num] += _in.readNBit(5);
		if (pos[0] > 1023)
		{
			throw new RuntimeException("pos[0] > 1023");
		}
		amp[0] = _in.readNBit(4);
		for (int i = 1; i < numPulse; i++)
		{
			pos[i] = _in.readNBit(5) + pos[i - 1];
			if (pos[i] > 1023)
			{
				string message = new StringBuilder().append("pos[").append(i).append("] > 1023")
					.toString();
				throw new RuntimeException(message);
			}
			amp[i] = _in.readNBit(5);
		}
		Pulse result = new Pulse(numPulse, pos, amp);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 66, 66, 120, 123, 109, 127, 16, 127, 16,
		127, 16, 127, 33, 113, 127, 0, 137, 143, 148,
		127, 4, 118, 48, 177, 110, 111, 105, 106, 138,
		112, 61, 233, 50, 236, 60, 236, 88
	})]
	private Tns decodeTns(BitReader _in)
	{
		int is8 = ((windowSequence == WindowSequence.EIGHT_SHORT_SEQUENCE.ordinal()) ? 1 : 0);
		int tns_max_order = ((is8 != 0) ? 7 : ((profile != Profile.___003C_003EMAIN) ? 12 : 20));
		int[] nFilt = new int[numWindows];
		int num = numWindows;
		int[] array = new int[2];
		int num2 = (array[1] = 2);
		num2 = (array[0] = num);
		int[][] length = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		int num3 = numWindows;
		array = new int[2];
		num2 = (array[1] = 2);
		num2 = (array[0] = num3);
		int[][] order = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		int num4 = numWindows;
		array = new int[2];
		num2 = (array[1] = 2);
		num2 = (array[0] = num4);
		int[][] direction = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		int num5 = numWindows;
		int num6 = 1 << 5 - 2 * is8;
		array = new int[3];
		num2 = (array[2] = num6);
		num2 = (array[1] = 2);
		num2 = (array[0] = num5);
		float[][][] coeff = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
		for (int w = 0; w < numWindows; w++)
		{
			int num7 = w;
			num2 = _in.readNBit(2 - is8);
			int num8 = num7;
			array = nFilt;
			int num9 = num2;
			array[num8] = num2;
			if (num9 == 0)
			{
				continue;
			}
			int coefRes = _in.read1Bit();
			for (int filt = 0; filt < nFilt[w]; filt++)
			{
				length[w][filt] = _in.readNBit(6 - 2 * is8);
				int[] obj = order[w];
				int num10 = filt;
				num2 = _in.readNBit(5 - 2 * is8);
				num8 = num10;
				array = obj;
				int num11 = num2;
				array[num8] = num2;
				if (num11 > tns_max_order)
				{
					string message = java.lang.String.format("TNS filter order %d is greater than maximum %d.\n", Integer.valueOf(order[w][filt]), Integer.valueOf(tns_max_order));
					throw new RuntimeException(message);
				}
				if (order[w][filt] != 0)
				{
					direction[w][filt] = _in.read1Bit();
					int coefCompress = _in.read1Bit();
					int coefLen = coefRes + 3 - coefCompress;
					int tmp2_idx = 2 * coefCompress + coefRes;
					for (int i = 0; i < order[w][filt]; i++)
					{
						coeff[w][filt][i] = AACTab.tns_tmp2_map[tmp2_idx][_in.readNBit(coefLen)];
					}
				}
			}
		}
		Tns result = new Tns(nFilt, length, order, direction, coeff);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 48, 130, 108, 131, 111, 111, 109, 127, 8,
		108, 107, 159, 4, 112, 166, 112, 163, 112, 195,
		112, 131, 240, 41, 46, 234, 93
	})]
	private void decodeSpectrum(BitReader _in)
	{
		float[] coef = new float[1024];
		int idx = 0;
		for (int g = 0; g < num_window_groups; g++)
		{
			int i = 0;
			while (i < maxSfb)
			{
				int cbt_m1 = band_type[idx] - 1;
				if (cbt_m1 < BandType.INTENSITY_BT2.ordinal() - 1 && cbt_m1 != BandType.NOISE_BT.ordinal() - 1)
				{
					float[] vq = ff_aac_codebook_vector_vals[cbt_m1];
					VLC vlc = spectral[cbt_m1];
					switch (cbt_m1 >> 1)
					{
					case 0:
						readBandType1And2(_in, coef, idx, g, i, vq, vlc);
						break;
					case 1:
						readBandType3And4(_in, coef, idx, g, i, vq, vlc);
						break;
					case 2:
						readBandType5And6(_in, coef, idx, g, i, vq, vlc);
						break;
					case 3:
					case 4:
						readBandType7Through10(_in, coef, idx, g, i, vq, vlc);
						break;
					default:
						readOther(_in, coef, idx, g, i, vq, vlc);
						break;
					}
				}
				i++;
				idx++;
			}
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 132, 130, 117, 127, 22, 125, 125, 125, 125,
		125, 125, 125, 126, 126, 241, 160, 122, 240, 73,
		107, 63, 10, 135
	})]
	static BlockICS()
	{
		vlc = new VLC(AACTab.ff_aac_scalefactor_code, AACTab.ff_aac_scalefactor_bits);
		spectral = new VLC[11]
		{
			VLCBuilder.createVLCBuilder(AACTab.codes1, AACTab.bits1, AACTab.codebook_vector02_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes2, AACTab.bits2, AACTab.codebook_vector02_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes3, AACTab.bits3, AACTab.codebook_vector02_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes4, AACTab.bits4, AACTab.codebook_vector02_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes5, AACTab.bits5, AACTab.codebook_vector4_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes6, AACTab.bits6, AACTab.codebook_vector4_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes7, AACTab.bits7, AACTab.codebook_vector6_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes8, AACTab.bits8, AACTab.codebook_vector6_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes9, AACTab.bits9, AACTab.codebook_vector8_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes10, AACTab.bits10, AACTab.codebook_vector8_idx).getVLC(),
			VLCBuilder.createVLCBuilder(AACTab.codes11, AACTab.bits11, AACTab.codebook_vector10_idx).getVLC()
		};
		ff_aac_pow2sf_tab = new float[428];
		for (int i = 0; i < 428; i++)
		{
			ff_aac_pow2sf_tab[i] = (float)java.lang.Math.pow(2.0, (double)(i - 200) / 4.0);
		}
	}
}
