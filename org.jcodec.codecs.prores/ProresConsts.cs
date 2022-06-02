using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.prores;

public class ProresConsts : Object
{
	public class FrameHeader : Object
	{
		public int payloadSize;

		public int width;

		public int height;

		public int frameType;

		public bool topFieldFirst;

		public int chromaType;

		public int[] scan;

		public int[] qMatLuma;

		public int[] qMatChroma;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 112, 65, 68, 105, 104, 104, 104, 105, 104,
			105, 105, 105, 105
		})]
		public FrameHeader(int frameSize, int width, int height, int frameType, bool topFieldFirst, int[] scan, int[] qMatLuma, int[] qMatChroma, int chromaType)
		{
			payloadSize = frameSize;
			this.width = width;
			this.height = height;
			this.frameType = frameType;
			this.topFieldFirst = topFieldFirst;
			this.scan = scan;
			this.qMatChroma = qMatChroma;
			this.qMatLuma = qMatLuma;
			this.chromaType = chromaType;
		}
	}

	public class PictureHeader : Object
	{
		public int log2SliceMbWidth;

		public short[] sliceSizes;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 108, 130, 105, 104, 104 })]
		public PictureHeader(int log2SliceMbWidth, short[] sliceSizes)
		{
			this.log2SliceMbWidth = log2SliceMbWidth;
			this.sliceSizes = sliceSizes;
		}
	}

	public static Codebook firstDCCodebook;

	internal static Codebook[] ___003C_003EdcCodebooks;

	internal static Codebook[] ___003C_003ErunCodebooks;

	internal static Codebook[] ___003C_003ElevCodebooks;

	public static int[] progressive_scan;

	public static int[] interlaced_scan;

	internal static int[] ___003C_003EQMAT_LUMA_APCH;

	internal static int[] ___003C_003EQMAT_CHROMA_APCH;

	internal static int[] ___003C_003EQMAT_LUMA_APCO;

	internal static int[] ___003C_003EQMAT_CHROMA_APCO;

	internal static int[] ___003C_003EQMAT_LUMA_APCN;

	internal static int[] ___003C_003EQMAT_CHROMA_APCN;

	internal static int[] ___003C_003EQMAT_LUMA_APCS;

	internal static int[] ___003C_003EQMAT_CHROMA_APCS;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codebook[] dcCodebooks
	{
		[HideFromJava]
		get
		{
			return ___003C_003EdcCodebooks;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codebook[] runCodebooks
	{
		[HideFromJava]
		get
		{
			return ___003C_003ErunCodebooks;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Codebook[] levCodebooks
	{
		[HideFromJava]
		get
		{
			return ___003C_003ElevCodebooks;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] QMAT_LUMA_APCH
	{
		[HideFromJava]
		get
		{
			return ___003C_003EQMAT_LUMA_APCH;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] QMAT_CHROMA_APCH
	{
		[HideFromJava]
		get
		{
			return ___003C_003EQMAT_CHROMA_APCH;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] QMAT_LUMA_APCO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EQMAT_LUMA_APCO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] QMAT_CHROMA_APCO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EQMAT_CHROMA_APCO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] QMAT_LUMA_APCN
	{
		[HideFromJava]
		get
		{
			return ___003C_003EQMAT_LUMA_APCN;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] QMAT_CHROMA_APCN
	{
		[HideFromJava]
		get
		{
			return ___003C_003EQMAT_CHROMA_APCN;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] QMAT_LUMA_APCS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EQMAT_LUMA_APCS;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] QMAT_CHROMA_APCS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EQMAT_CHROMA_APCS;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(10)]
	public ProresConsts()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		140,
		162,
		110,
		byte.MaxValue,
		58,
		74,
		byte.MaxValue,
		160,
		101,
		83,
		byte.MaxValue,
		93,
		77,
		byte.MaxValue,
		161,
		28,
		75,
		byte.MaxValue,
		161,
		28,
		75,
		191,
		160,
		229,
		223,
		160,
		229,
		223,
		161,
		33,
		223,
		161,
		33,
		223,
		161,
		0,
		223,
		161,
		0,
		223,
		161,
		24
	})]
	static ProresConsts()
	{
		firstDCCodebook = new Codebook(5, 6, 0);
		___003C_003EdcCodebooks = new Codebook[7]
		{
			new Codebook(0, 1, 0),
			new Codebook(1, 2, 0),
			new Codebook(1, 2, 0),
			new Codebook(2, 3, 1),
			new Codebook(2, 3, 1),
			new Codebook(3, 4, 0),
			new Codebook(3, 4, 0)
		};
		___003C_003ErunCodebooks = new Codebook[16]
		{
			new Codebook(0, 1, 2),
			new Codebook(0, 1, 2),
			new Codebook(0, 1, 1),
			new Codebook(0, 1, 1),
			new Codebook(0, 1, 0),
			new Codebook(1, 2, 1),
			new Codebook(1, 2, 1),
			new Codebook(1, 2, 1),
			new Codebook(1, 2, 1),
			new Codebook(1, 2, 0),
			new Codebook(1, 2, 0),
			new Codebook(1, 2, 0),
			new Codebook(1, 2, 0),
			new Codebook(1, 2, 0),
			new Codebook(1, 2, 0),
			new Codebook(2, 3, 0)
		};
		___003C_003ElevCodebooks = new Codebook[10]
		{
			new Codebook(0, 1, 0),
			new Codebook(0, 2, 2),
			new Codebook(0, 1, 1),
			new Codebook(0, 1, 2),
			new Codebook(0, 1, 0),
			new Codebook(1, 2, 0),
			new Codebook(1, 2, 0),
			new Codebook(1, 2, 0),
			new Codebook(1, 2, 0),
			new Codebook(2, 3, 0)
		};
		progressive_scan = new int[64]
		{
			0, 1, 8, 9, 2, 3, 10, 11, 16, 17,
			24, 25, 18, 19, 26, 27, 4, 5, 12, 20,
			13, 6, 7, 14, 21, 28, 29, 22, 15, 23,
			30, 31, 32, 33, 40, 48, 41, 34, 35, 42,
			49, 56, 57, 50, 43, 36, 37, 44, 51, 58,
			59, 52, 45, 38, 39, 46, 53, 60, 61, 54,
			47, 55, 62, 63
		};
		interlaced_scan = new int[64]
		{
			0, 8, 1, 9, 16, 24, 17, 25, 2, 10,
			3, 11, 18, 26, 19, 27, 32, 40, 33, 34,
			41, 48, 56, 49, 42, 35, 43, 50, 57, 58,
			51, 59, 4, 12, 5, 6, 13, 20, 28, 21,
			14, 7, 15, 22, 29, 36, 44, 37, 30, 23,
			31, 38, 45, 52, 60, 53, 46, 39, 47, 54,
			61, 62, 55, 63
		};
		___003C_003EQMAT_LUMA_APCH = new int[64]
		{
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 5, 4, 4, 4, 4, 4, 4, 5, 5,
			4, 4, 4, 4, 4, 5, 5, 6, 4, 4,
			4, 4, 5, 5, 6, 7, 4, 4, 4, 4,
			5, 6, 7, 7
		};
		___003C_003EQMAT_CHROMA_APCH = new int[64]
		{
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 5, 4, 4, 4, 4, 4, 4, 5, 5,
			4, 4, 4, 4, 4, 5, 5, 6, 4, 4,
			4, 4, 5, 5, 6, 7, 4, 4, 4, 4,
			5, 6, 7, 7
		};
		___003C_003EQMAT_LUMA_APCO = new int[64]
		{
			4, 7, 9, 11, 13, 14, 15, 63, 7, 7,
			11, 12, 14, 15, 63, 63, 9, 11, 13, 14,
			15, 63, 63, 63, 11, 11, 13, 14, 63, 63,
			63, 63, 11, 13, 14, 63, 63, 63, 63, 63,
			13, 14, 63, 63, 63, 63, 63, 63, 13, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63
		};
		___003C_003EQMAT_CHROMA_APCO = new int[64]
		{
			4, 7, 9, 11, 13, 14, 63, 63, 7, 7,
			11, 12, 14, 63, 63, 63, 9, 11, 13, 14,
			63, 63, 63, 63, 11, 11, 13, 14, 63, 63,
			63, 63, 11, 13, 14, 63, 63, 63, 63, 63,
			13, 14, 63, 63, 63, 63, 63, 63, 13, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63
		};
		___003C_003EQMAT_LUMA_APCN = new int[64]
		{
			4, 4, 5, 5, 6, 7, 7, 9, 4, 4,
			5, 6, 7, 7, 9, 9, 5, 5, 6, 7,
			7, 9, 9, 10, 5, 5, 6, 7, 7, 9,
			9, 10, 5, 6, 7, 7, 8, 9, 10, 12,
			6, 7, 7, 8, 9, 10, 12, 15, 6, 7,
			7, 9, 10, 11, 14, 17, 7, 7, 9, 10,
			11, 14, 17, 21
		};
		___003C_003EQMAT_CHROMA_APCN = new int[64]
		{
			4, 4, 5, 5, 6, 7, 7, 9, 4, 4,
			5, 6, 7, 7, 9, 9, 5, 5, 6, 7,
			7, 9, 9, 10, 5, 5, 6, 7, 7, 9,
			9, 10, 5, 6, 7, 7, 8, 9, 10, 12,
			6, 7, 7, 8, 9, 10, 12, 15, 6, 7,
			7, 9, 10, 11, 14, 17, 7, 7, 9, 10,
			11, 14, 17, 21
		};
		___003C_003EQMAT_LUMA_APCS = new int[64]
		{
			4, 5, 6, 7, 9, 11, 13, 15, 5, 5,
			7, 8, 11, 13, 15, 17, 6, 7, 9, 11,
			13, 15, 15, 17, 7, 7, 9, 11, 13, 15,
			17, 19, 7, 9, 11, 13, 14, 16, 19, 23,
			9, 11, 13, 14, 16, 19, 23, 29, 9, 11,
			13, 15, 17, 21, 28, 35, 11, 13, 16, 17,
			21, 28, 35, 41
		};
		___003C_003EQMAT_CHROMA_APCS = new int[64]
		{
			4, 5, 6, 7, 9, 11, 13, 15, 5, 5,
			7, 8, 11, 13, 15, 17, 6, 7, 9, 11,
			13, 15, 15, 17, 7, 7, 9, 11, 13, 15,
			17, 19, 7, 9, 11, 13, 14, 16, 19, 23,
			9, 11, 13, 14, 16, 19, 23, 29, 9, 11,
			13, 15, 17, 21, 28, 35, 11, 13, 16, 17,
			21, 28, 35, 41
		};
	}
}
