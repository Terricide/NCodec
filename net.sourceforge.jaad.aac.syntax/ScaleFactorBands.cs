using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.syntax;

internal interface ScaleFactorBands
{
	static readonly int[] SWB_LONG_WINDOW_COUNT;

	static readonly int[] SWB_OFFSET_1024_96;

	static readonly int[] SWB_OFFSET_1024_64;

	static readonly int[] SWB_OFFSET_1024_48;

	static readonly int[] SWB_OFFSET_1024_32;

	static readonly int[] SWB_OFFSET_1024_24;

	static readonly int[] SWB_OFFSET_1024_16;

	static readonly int[] SWB_OFFSET_1024_8;

	static readonly int[][] SWB_OFFSET_LONG_WINDOW;

	static readonly int[] SWB_SHORT_WINDOW_COUNT;

	static readonly int[] SWB_OFFSET_128_96;

	static readonly int[] SWB_OFFSET_128_64;

	static readonly int[] SWB_OFFSET_128_48;

	static readonly int[] SWB_OFFSET_128_24;

	static readonly int[] SWB_OFFSET_128_16;

	static readonly int[] SWB_OFFSET_128_8;

	static readonly int[][] SWB_OFFSET_SHORT_WINDOW;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[]
	{
		159,
		139,
		130,
		191,
		45,
		byte.MaxValue,
		160,
		223,
		70,
		byte.MaxValue,
		161,
		21,
		71,
		byte.MaxValue,
		161,
		45,
		71,
		byte.MaxValue,
		161,
		63,
		71,
		byte.MaxValue,
		161,
		21,
		71,
		byte.MaxValue,
		161,
		7,
		70,
		byte.MaxValue,
		160,
		249,
		70,
		byte.MaxValue,
		81,
		78,
		191,
		45,
		223,
		56,
		223,
		56,
		223,
		68,
		223,
		74,
		223,
		74,
		223,
		74
	})]
	static ScaleFactorBands()
	{
		SWB_LONG_WINDOW_COUNT = new int[12]
		{
			41, 41, 47, 49, 49, 51, 47, 47, 43, 43,
			43, 40
		};
		SWB_OFFSET_1024_96 = new int[43]
		{
			0, 4, 8, 12, 16, 20, 24, 28, 32, 36,
			40, 44, 48, 52, 56, 64, 72, 80, 88, 96,
			108, 120, 132, 144, 156, 172, 188, 212, 240, 276,
			320, 384, 448, 512, 576, 640, 704, 768, 832, 896,
			960, 1024, -1
		};
		SWB_OFFSET_1024_64 = new int[49]
		{
			0, 4, 8, 12, 16, 20, 24, 28, 32, 36,
			40, 44, 48, 52, 56, 64, 72, 80, 88, 100,
			112, 124, 140, 156, 172, 192, 216, 240, 268, 304,
			344, 384, 424, 464, 504, 544, 584, 624, 664, 704,
			744, 784, 824, 864, 904, 944, 984, 1024, -1
		};
		SWB_OFFSET_1024_48 = new int[51]
		{
			0, 4, 8, 12, 16, 20, 24, 28, 32, 36,
			40, 48, 56, 64, 72, 80, 88, 96, 108, 120,
			132, 144, 160, 176, 196, 216, 240, 264, 292, 320,
			352, 384, 416, 448, 480, 512, 544, 576, 608, 640,
			672, 704, 736, 768, 800, 832, 864, 896, 928, 1024,
			-1
		};
		SWB_OFFSET_1024_32 = new int[53]
		{
			0, 4, 8, 12, 16, 20, 24, 28, 32, 36,
			40, 48, 56, 64, 72, 80, 88, 96, 108, 120,
			132, 144, 160, 176, 196, 216, 240, 264, 292, 320,
			352, 384, 416, 448, 480, 512, 544, 576, 608, 640,
			672, 704, 736, 768, 800, 832, 864, 896, 928, 960,
			992, 1024, -1
		};
		SWB_OFFSET_1024_24 = new int[49]
		{
			0, 4, 8, 12, 16, 20, 24, 28, 32, 36,
			40, 44, 52, 60, 68, 76, 84, 92, 100, 108,
			116, 124, 136, 148, 160, 172, 188, 204, 220, 240,
			260, 284, 308, 336, 364, 396, 432, 468, 508, 552,
			600, 652, 704, 768, 832, 896, 960, 1024, -1
		};
		SWB_OFFSET_1024_16 = new int[45]
		{
			0, 8, 16, 24, 32, 40, 48, 56, 64, 72,
			80, 88, 100, 112, 124, 136, 148, 160, 172, 184,
			196, 212, 228, 244, 260, 280, 300, 320, 344, 368,
			396, 424, 456, 492, 532, 572, 616, 664, 716, 772,
			832, 896, 960, 1024, -1
		};
		SWB_OFFSET_1024_8 = new int[42]
		{
			0, 12, 24, 36, 48, 60, 72, 84, 96, 108,
			120, 132, 144, 156, 172, 188, 204, 220, 236, 252,
			268, 288, 308, 328, 348, 372, 396, 420, 448, 476,
			508, 544, 580, 620, 664, 712, 764, 820, 880, 944,
			1024, -1
		};
		SWB_OFFSET_LONG_WINDOW = new int[12][]
		{
			SWB_OFFSET_1024_96, SWB_OFFSET_1024_96, SWB_OFFSET_1024_64, SWB_OFFSET_1024_48, SWB_OFFSET_1024_48, SWB_OFFSET_1024_32, SWB_OFFSET_1024_24, SWB_OFFSET_1024_24, SWB_OFFSET_1024_16, SWB_OFFSET_1024_16,
			SWB_OFFSET_1024_16, SWB_OFFSET_1024_8
		};
		SWB_SHORT_WINDOW_COUNT = new int[12]
		{
			12, 12, 12, 14, 14, 14, 15, 15, 15, 15,
			15, 15
		};
		SWB_OFFSET_128_96 = new int[14]
		{
			0, 4, 8, 12, 16, 20, 24, 32, 40, 48,
			64, 92, 128, -1
		};
		SWB_OFFSET_128_64 = new int[14]
		{
			0, 4, 8, 12, 16, 20, 24, 32, 40, 48,
			64, 92, 128, -1
		};
		SWB_OFFSET_128_48 = new int[16]
		{
			0, 4, 8, 12, 16, 20, 28, 36, 44, 56,
			68, 80, 96, 112, 128, -1
		};
		SWB_OFFSET_128_24 = new int[17]
		{
			0, 4, 8, 12, 16, 20, 24, 28, 36, 44,
			52, 64, 76, 92, 108, 128, -1
		};
		SWB_OFFSET_128_16 = new int[17]
		{
			0, 4, 8, 12, 16, 20, 24, 28, 32, 40,
			48, 60, 72, 88, 108, 128, -1
		};
		SWB_OFFSET_128_8 = new int[17]
		{
			0, 4, 8, 12, 16, 20, 24, 28, 36, 44,
			52, 60, 72, 88, 108, 128, -1
		};
		SWB_OFFSET_SHORT_WINDOW = new int[12][]
		{
			SWB_OFFSET_128_96, SWB_OFFSET_128_96, SWB_OFFSET_128_64, SWB_OFFSET_128_48, SWB_OFFSET_128_48, SWB_OFFSET_128_48, SWB_OFFSET_128_24, SWB_OFFSET_128_24, SWB_OFFSET_128_16, SWB_OFFSET_128_16,
			SWB_OFFSET_128_16, SWB_OFFSET_128_8
		};
	}
}
