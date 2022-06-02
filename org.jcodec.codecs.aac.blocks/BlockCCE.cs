using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using org.jcodec.common.io;

namespace org.jcodec.codecs.aac.blocks;

public class BlockCCE : Block
{
	[Serializable]
	[InnerClass(null, Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/aac/blocks/BlockCCE$CouplingPoint;>;")]
	[Modifiers(Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	internal sealed class CouplingPoint : java.lang.Enum
	{
		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static CouplingPoint BEFORE_TNS;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static CouplingPoint BETWEEN_TNS_AND_IMDCT;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static CouplingPoint UNDEF;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static CouplingPoint AFTER_IMDCT;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static CouplingPoint[] _0024VALUES;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(90)]
		private CouplingPoint(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(90)]
		public static CouplingPoint[] values()
		{
			return (CouplingPoint[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(90)]
		public static CouplingPoint valueOf(string name)
		{
			return (CouplingPoint)java.lang.Enum.valueOf(ClassLiteral<CouplingPoint>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 120, 162, 63, 34 })]
		static CouplingPoint()
		{
			BEFORE_TNS = new CouplingPoint("BEFORE_TNS", 0);
			BETWEEN_TNS_AND_IMDCT = new CouplingPoint("BETWEEN_TNS_AND_IMDCT", 1);
			UNDEF = new CouplingPoint("UNDEF", 2);
			AFTER_IMDCT = new CouplingPoint("AFTER_IMDCT", 3);
			_0024VALUES = new CouplingPoint[4] { BEFORE_TNS, BETWEEN_TNS_AND_IMDCT, UNDEF, AFTER_IMDCT };
		}
	}

	private int coupling_point;

	private int num_coupled;

	private BlockType[] type;

	private int[] id_select;

	private int[] ch_select;

	private int sign;

	private object scale;

	private object[] cce_scale;

	private BlockICS blockICS;

	private BlockICS.BandType[] bandType;

	internal static VLC vlc;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 105, 104 })]
	public BlockCCE(BlockICS.BandType[] bandType)
	{
		this.bandType = bandType;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 99, 111, 110, 111, 101, 125, 112,
		112, 112, 108, 135, 234, 55, 234, 75, 157, 109,
		149, 108, 141, 106, 99, 100, 100, 100, 126, 184,
		118, 115, 115, 112, 101, 241, 61, 45, 236, 54,
		234, 85
	})]
	public override void parse(BitReader _in)
	{
		int num_gain = 0;
		coupling_point = 2 * _in.read1Bit();
		num_coupled = _in.readNBit(3);
		for (int c2 = 0; c2 <= num_coupled; c2++)
		{
			num_gain++;
			type[c2] = ((_in.read1Bit() == 0) ? BlockType.___003C_003ETYPE_SCE : BlockType.___003C_003ETYPE_CPE);
			id_select[c2] = _in.readNBit(4);
			if (type[c2] == BlockType.___003C_003ETYPE_CPE)
			{
				ch_select[c2] = _in.readNBit(2);
				if (ch_select[c2] == 3)
				{
					num_gain++;
				}
			}
			else
			{
				ch_select[c2] = 2;
			}
		}
		coupling_point += _in.read1Bit() | (coupling_point >> 1);
		sign = _in.read1Bit();
		scale = cce_scale[_in.readNBit(2)];
		blockICS = new BlockICS();
		blockICS.parse(_in);
		for (int c = 0; c < num_gain; c++)
		{
			int idx = 0;
			int cge = 1;
			int gain = 0;
			if (c != 0)
			{
				cge = ((coupling_point == CouplingPoint.AFTER_IMDCT.ordinal()) ? 1 : _in.read1Bit());
				gain = ((cge != 0) ? (vlc.readVLC(_in) - 60) : 0);
			}
			if (coupling_point == CouplingPoint.AFTER_IMDCT.ordinal())
			{
				continue;
			}
			for (int g = 0; g < blockICS.num_window_groups; g++)
			{
				int sfb = 0;
				while (sfb < blockICS.maxSfb)
				{
					if (bandType[idx] != BlockICS.BandType.ZERO_BT && cge == 0)
					{
						int num = vlc.readVLC(_in) - 60;
					}
					sfb++;
					idx++;
				}
			}
		}
	}

	[LineNumberTable(new byte[] { 159, 133, 98, 117 })]
	static BlockCCE()
	{
		vlc = new VLC(AACTab.ff_aac_scalefactor_code, AACTab.ff_aac_scalefactor_bits);
	}

	[HideFromJava]
	[NameSig("<init>", "([Lorg.jcodec.codecs.aac.blocks.BlockICS$BandType;)V")]
	public BlockCCE(object P_0)
		: this((BlockICS.BandType[])P_0)
	{
	}
}
