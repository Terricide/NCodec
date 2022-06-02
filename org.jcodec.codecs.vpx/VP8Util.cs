using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;

namespace org.jcodec.codecs.vpx;

public class VP8Util : java.lang.Object
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : java.lang.Object
	{
		[Modifiers(Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		internal static int[] _0024SwitchMap_0024org_0024jcodec_0024codecs_0024vpx_0024VP8Util_0024PLANE;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[LineNumberTable(931)]
		static _1()
		{
			_0024SwitchMap_0024org_0024jcodec_0024codecs_0024vpx_0024VP8Util_0024PLANE = new int[(nint)PLANE.values().LongLength];
			NoSuchFieldError noSuchFieldError2;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024vpx_0024VP8Util_0024PLANE[PLANE.___003C_003EY2.ordinal()] = 1;
			}
			catch (System.Exception x)
			{
				NoSuchFieldError noSuchFieldError = ByteCodeHelper.MapException<NoSuchFieldError>(x, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError == null)
				{
					throw;
				}
				noSuchFieldError2 = noSuchFieldError;
				goto IL_0037;
			}
			goto IL_003d;
			IL_0037:
			NoSuchFieldError noSuchFieldError3 = noSuchFieldError2;
			goto IL_003d;
			IL_003d:
			NoSuchFieldError noSuchFieldError5;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024vpx_0024VP8Util_0024PLANE[PLANE.___003C_003EY1.ordinal()] = 2;
			}
			catch (System.Exception x2)
			{
				NoSuchFieldError noSuchFieldError4 = ByteCodeHelper.MapException<NoSuchFieldError>(x2, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError4 == null)
				{
					throw;
				}
				noSuchFieldError5 = noSuchFieldError4;
				goto IL_0062;
			}
			goto IL_0068;
			IL_0062:
			NoSuchFieldError noSuchFieldError6 = noSuchFieldError5;
			goto IL_0068;
			IL_0068:
			NoSuchFieldError noSuchFieldError8;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024vpx_0024VP8Util_0024PLANE[PLANE.___003C_003EU.ordinal()] = 3;
			}
			catch (System.Exception x3)
			{
				NoSuchFieldError noSuchFieldError7 = ByteCodeHelper.MapException<NoSuchFieldError>(x3, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError7 == null)
				{
					throw;
				}
				noSuchFieldError8 = noSuchFieldError7;
				goto IL_008e;
			}
			goto IL_0096;
			IL_008e:
			NoSuchFieldError noSuchFieldError9 = noSuchFieldError8;
			goto IL_0096;
			IL_0096:
			NoSuchFieldError noSuchFieldError11;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024vpx_0024VP8Util_0024PLANE[PLANE.___003C_003EV.ordinal()] = 4;
				return;
			}
			catch (System.Exception x4)
			{
				NoSuchFieldError noSuchFieldError10 = ByteCodeHelper.MapException<NoSuchFieldError>(x4, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError10 == null)
				{
					throw;
				}
				noSuchFieldError11 = noSuchFieldError10;
			}
			NoSuchFieldError noSuchFieldError12 = noSuchFieldError11;
		}

		_1()
		{
			throw null;
		}
	}

	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/codecs/vpx/VP8Util$PLANE;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class PLANE : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			U,
			V,
			Y1,
			Y2
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static PLANE ___003C_003EU;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static PLANE ___003C_003EV;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static PLANE ___003C_003EY1;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static PLANE ___003C_003EY2;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static PLANE[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static PLANE U
		{
			[HideFromJava]
			get
			{
				return ___003C_003EU;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static PLANE V
		{
			[HideFromJava]
			get
			{
				return ___003C_003EV;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static PLANE Y1
		{
			[HideFromJava]
			get
			{
				return ___003C_003EY1;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static PLANE Y2
		{
			[HideFromJava]
			get
			{
				return ___003C_003EY2;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(515)]
		public static PLANE[] values()
		{
			return (PLANE[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(515)]
		private PLANE(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(515)]
		public static PLANE valueOf(string name)
		{
			return (PLANE)java.lang.Enum.valueOf(ClassLiteral<PLANE>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 13, 66, 63, 34 })]
		static PLANE()
		{
			___003C_003EU = new PLANE("U", 0);
			___003C_003EV = new PLANE("V", 1);
			___003C_003EY1 = new PLANE("Y1", 2);
			___003C_003EY2 = new PLANE("Y2", 3);
			_0024VALUES = new PLANE[4] { ___003C_003EU, ___003C_003EV, ___003C_003EY1, ___003C_003EY2 };
		}
	}

	public class QuantizationParams : java.lang.Object
	{
		internal int yAC;

		internal int yDC;

		internal int y2DC;

		internal int y2AC;

		internal int chromaDC;

		internal int chromaAC;

		internal static int[] ___003C_003EdcQLookup;

		internal static int[] ___003C_003EacQLookup;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] dcQLookup
		{
			[HideFromJava]
			get
			{
				return ___003C_003EdcQLookup;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] acQLookup
		{
			[HideFromJava]
			get
			{
				return ___003C_003EacQLookup;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 23, 66, 233, 71, 117, 119, 153, 127, 2,
			106, 104, 120, 120
		})]
		public QuantizationParams(int baseIndex, int ydcIndexDelta, int y2dcIndexDelta, int y2acIndexDelta, int chromaDCIndexDelta, int chromaACIndexDelta)
		{
			yAC = ___003C_003EacQLookup[clip(baseIndex, 127)];
			yDC = ___003C_003EdcQLookup[clip(baseIndex + ydcIndexDelta, 127)];
			y2DC = ___003C_003EdcQLookup[clip(baseIndex + y2dcIndexDelta, 127)] * 2;
			y2AC = ___003C_003EacQLookup[clip(baseIndex + y2acIndexDelta, 127)] * 155 / 100;
			if (y2AC < 8)
			{
				y2AC = 8;
			}
			chromaDC = ___003C_003EdcQLookup[clip(baseIndex + chromaDCIndexDelta, 127)];
			chromaAC = ___003C_003EacQLookup[clip(baseIndex + chromaACIndexDelta, 127)];
		}

		[LineNumberTable(new byte[] { 159, 16, 98, 105, 135, 101, 131 })]
		public static int clip255(int val)
		{
			if (val > 255)
			{
				return 255;
			}
			if (val < 0)
			{
				return 0;
			}
			return val;
		}

		[LineNumberTable(new byte[] { 159, 19, 162, 101, 131, 101, 131 })]
		private static int clip(int val, int max)
		{
			if (val > max)
			{
				return max;
			}
			if (val < 0)
			{
				return 0;
			}
			return val;
		}

		[LineNumberTable(new byte[]
		{
			159,
			28,
			66,
			byte.MaxValue,
			162,
			202,
			74
		})]
		static QuantizationParams()
		{
			___003C_003EdcQLookup = new int[128]
			{
				4, 5, 6, 7, 8, 9, 10, 10, 11, 12,
				13, 14, 15, 16, 17, 17, 18, 19, 20, 20,
				21, 21, 22, 22, 23, 23, 24, 25, 25, 26,
				27, 28, 29, 30, 31, 32, 33, 34, 35, 36,
				37, 37, 38, 39, 40, 41, 42, 43, 44, 45,
				46, 46, 47, 48, 49, 50, 51, 52, 53, 54,
				55, 56, 57, 58, 59, 60, 61, 62, 63, 64,
				65, 66, 67, 68, 69, 70, 71, 72, 73, 74,
				75, 76, 76, 77, 78, 79, 80, 81, 82, 83,
				84, 85, 86, 87, 88, 89, 91, 93, 95, 96,
				98, 100, 101, 102, 104, 106, 108, 110, 112, 114,
				116, 118, 122, 124, 126, 128, 130, 132, 134, 136,
				138, 140, 143, 145, 148, 151, 154, 157
			};
			___003C_003EacQLookup = new int[128]
			{
				4, 5, 6, 7, 8, 9, 10, 11, 12, 13,
				14, 15, 16, 17, 18, 19, 20, 21, 22, 23,
				24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
				34, 35, 36, 37, 38, 39, 40, 41, 42, 43,
				44, 45, 46, 47, 48, 49, 50, 51, 52, 53,
				54, 55, 56, 57, 58, 60, 62, 64, 66, 68,
				70, 72, 74, 76, 78, 80, 82, 84, 86, 88,
				90, 92, 94, 96, 98, 100, 102, 104, 106, 108,
				110, 112, 114, 116, 119, 122, 125, 128, 131, 134,
				137, 140, 143, 146, 149, 152, 155, 158, 161, 164,
				167, 170, 173, 177, 181, 185, 189, 193, 197, 201,
				205, 209, 213, 217, 221, 225, 229, 234, 239, 245,
				249, 254, 259, 264, 269, 274, 279, 284
			};
		}
	}

	public class SubblockConstants : java.lang.Object
	{
		public const int B_DC_PRED = 0;

		public const int B_TM_PRED = 1;

		public const int B_VE_PRED = 2;

		public const int B_HE_PRED = 3;

		public const int B_LD_PRED = 4;

		public const int B_RD_PRED = 5;

		public const int B_VR_PRED = 6;

		public const int B_VL_PRED = 7;

		public const int B_HD_PRED = 8;

		public const int B_HU_PRED = 9;

		public const int DC_PRED = 0;

		public const int V_PRED = 1;

		public const int H_PRED = 2;

		public const int TM_PRED = 3;

		public const int B_PRED = 4;

		public const int DCT_0 = 0;

		public const int DCT_1 = 1;

		public const int DCT_2 = 2;

		public const int DCT_3 = 3;

		public const int DCT_4 = 4;

		public const int cat_5_6 = 5;

		public const int cat_7_10 = 6;

		public const int cat_11_18 = 7;

		public const int cat_19_34 = 8;

		public const int cat_35_66 = 9;

		public const int cat_67_2048 = 10;

		public const int dct_eob = 11;

		internal static int[] ___003C_003Evp8CoefTree;

		internal static int[] ___003C_003Evp8CoefBands;

		internal static int[] ___003C_003Evp8defaultZigZag1d;

		public static int[] subblockModeTree;

		internal static int[] ___003C_003EPcat1;

		internal static int[] ___003C_003EPcat2;

		internal static int[] ___003C_003EPcat3;

		internal static int[] ___003C_003EPcat4;

		internal static int[] ___003C_003EPcat5;

		internal static int[] ___003C_003EPcat6;

		public static int[][][] keyFrameSubblockModeProb;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] vp8CoefTree
		{
			[HideFromJava]
			get
			{
				return ___003C_003Evp8CoefTree;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] vp8CoefBands
		{
			[HideFromJava]
			get
			{
				return ___003C_003Evp8CoefBands;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] vp8defaultZigZag1d
		{
			[HideFromJava]
			get
			{
				return ___003C_003Evp8defaultZigZag1d;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] Pcat1
		{
			[HideFromJava]
			get
			{
				return ___003C_003EPcat1;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] Pcat2
		{
			[HideFromJava]
			get
			{
				return ___003C_003EPcat2;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] Pcat3
		{
			[HideFromJava]
			get
			{
				return ___003C_003EPcat3;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] Pcat4
		{
			[HideFromJava]
			get
			{
				return ___003C_003EPcat4;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] Pcat5
		{
			[HideFromJava]
			get
			{
				return ___003C_003EPcat5;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static int[] Pcat6
		{
			[HideFromJava]
			get
			{
				return ___003C_003EPcat6;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(44)]
		public SubblockConstants()
		{
		}

		[LineNumberTable(new byte[]
		{
			159,
			101,
			162,
			byte.MaxValue,
			99,
			78,
			byte.MaxValue,
			53,
			79,
			byte.MaxValue,
			60,
			90,
			byte.MaxValue,
			75,
			76,
			120,
			127,
			1,
			127,
			9,
			127,
			17,
			127,
			25,
			191,
			77
		})]
		static SubblockConstants()
		{
			___003C_003Evp8CoefTree = new int[22]
			{
				-11, 2, 0, 4, -1, 6, 8, 12, -2, 10,
				-3, -4, 14, 16, -5, -6, 18, 20, -7, -8,
				-9, -10
			};
			___003C_003Evp8CoefBands = new int[16]
			{
				0, 1, 2, 3, 6, 4, 5, 6, 6, 6,
				6, 6, 6, 6, 6, 7
			};
			___003C_003Evp8defaultZigZag1d = new int[16]
			{
				0, 1, 4, 8, 5, 2, 3, 6, 9, 12,
				13, 10, 7, 11, 14, 15
			};
			subblockModeTree = new int[18]
			{
				0, 2, -1, 4, -2, 6, 8, 12, -3, 10,
				-5, -6, -4, 14, -7, 16, -8, -9
			};
			___003C_003EPcat1 = new int[2] { 159, 0 };
			___003C_003EPcat2 = new int[3] { 165, 145, 0 };
			___003C_003EPcat3 = new int[4] { 173, 148, 140, 0 };
			___003C_003EPcat4 = new int[5] { 176, 155, 140, 135, 0 };
			___003C_003EPcat5 = new int[6] { 180, 157, 141, 134, 130, 0 };
			___003C_003EPcat6 = new int[12]
			{
				254, 254, 243, 230, 196, 177, 153, 140, 133, 130,
				129, 0
			};
			keyFrameSubblockModeProb = new int[10][][]
			{
				new int[10][]
				{
					new int[9] { 231, 120, 48, 89, 115, 113, 120, 152, 112 },
					new int[9] { 152, 179, 64, 126, 170, 118, 46, 70, 95 },
					new int[9] { 175, 69, 143, 80, 85, 82, 72, 155, 103 },
					new int[9] { 56, 58, 10, 171, 218, 189, 17, 13, 152 },
					new int[9] { 144, 71, 10, 38, 171, 213, 144, 34, 26 },
					new int[9] { 114, 26, 17, 163, 44, 195, 21, 10, 173 },
					new int[9] { 121, 24, 80, 195, 26, 62, 44, 64, 85 },
					new int[9] { 170, 46, 55, 19, 136, 160, 33, 206, 71 },
					new int[9] { 63, 20, 8, 114, 114, 208, 12, 9, 226 },
					new int[9] { 81, 40, 11, 96, 182, 84, 29, 16, 36 }
				},
				new int[10][]
				{
					new int[9] { 134, 183, 89, 137, 98, 101, 106, 165, 148 },
					new int[9] { 72, 187, 100, 130, 157, 111, 32, 75, 80 },
					new int[9] { 66, 102, 167, 99, 74, 62, 40, 234, 128 },
					new int[9] { 41, 53, 9, 178, 241, 141, 26, 8, 107 },
					new int[9] { 104, 79, 12, 27, 217, 255, 87, 17, 7 },
					new int[9] { 74, 43, 26, 146, 73, 166, 49, 23, 157 },
					new int[9] { 65, 38, 105, 160, 51, 52, 31, 115, 128 },
					new int[9] { 87, 68, 71, 44, 114, 51, 15, 186, 23 },
					new int[9] { 47, 41, 14, 110, 182, 183, 21, 17, 194 },
					new int[9] { 66, 45, 25, 102, 197, 189, 23, 18, 22 }
				},
				new int[10][]
				{
					new int[9] { 88, 88, 147, 150, 42, 46, 45, 196, 205 },
					new int[9] { 43, 97, 183, 117, 85, 38, 35, 179, 61 },
					new int[9] { 39, 53, 200, 87, 26, 21, 43, 232, 171 },
					new int[9] { 56, 34, 51, 104, 114, 102, 29, 93, 77 },
					new int[9] { 107, 54, 32, 26, 51, 1, 81, 43, 31 },
					new int[9] { 39, 28, 85, 171, 58, 165, 90, 98, 64 },
					new int[9] { 34, 22, 116, 206, 23, 34, 43, 166, 73 },
					new int[9] { 68, 25, 106, 22, 64, 171, 36, 225, 114 },
					new int[9] { 34, 19, 21, 102, 132, 188, 16, 76, 124 },
					new int[9] { 62, 18, 78, 95, 85, 57, 50, 48, 51 }
				},
				new int[10][]
				{
					new int[9] { 193, 101, 35, 159, 215, 111, 89, 46, 111 },
					new int[9] { 60, 148, 31, 172, 219, 228, 21, 18, 111 },
					new int[9] { 112, 113, 77, 85, 179, 255, 38, 120, 114 },
					new int[9] { 40, 42, 1, 196, 245, 209, 10, 25, 109 },
					new int[9] { 100, 80, 8, 43, 154, 1, 51, 26, 71 },
					new int[9] { 88, 43, 29, 140, 166, 213, 37, 43, 154 },
					new int[9] { 61, 63, 30, 155, 67, 45, 68, 1, 209 },
					new int[9] { 142, 78, 78, 16, 255, 128, 34, 197, 171 },
					new int[9] { 41, 40, 5, 102, 211, 183, 4, 1, 221 },
					new int[9] { 51, 50, 17, 168, 209, 192, 23, 25, 82 }
				},
				new int[10][]
				{
					new int[9] { 125, 98, 42, 88, 104, 85, 117, 175, 82 },
					new int[9] { 95, 84, 53, 89, 128, 100, 113, 101, 45 },
					new int[9] { 75, 79, 123, 47, 51, 128, 81, 171, 1 },
					new int[9] { 57, 17, 5, 71, 102, 57, 53, 41, 49 },
					new int[9] { 115, 21, 2, 10, 102, 255, 166, 23, 6 },
					new int[9] { 38, 33, 13, 121, 57, 73, 26, 1, 85 },
					new int[9] { 41, 10, 67, 138, 77, 110, 90, 47, 114 },
					new int[9] { 101, 29, 16, 10, 85, 128, 101, 196, 26 },
					new int[9] { 57, 18, 10, 102, 102, 213, 34, 20, 43 },
					new int[9] { 117, 20, 15, 36, 163, 128, 68, 1, 26 }
				},
				new int[10][]
				{
					new int[9] { 138, 31, 36, 171, 27, 166, 38, 44, 229 },
					new int[9] { 67, 87, 58, 169, 82, 115, 26, 59, 179 },
					new int[9] { 63, 59, 90, 180, 59, 166, 93, 73, 154 },
					new int[9] { 40, 40, 21, 116, 143, 209, 34, 39, 175 },
					new int[9] { 57, 46, 22, 24, 128, 1, 54, 17, 37 },
					new int[9] { 47, 15, 16, 183, 34, 223, 49, 45, 183 },
					new int[9] { 46, 17, 33, 183, 6, 98, 15, 32, 183 },
					new int[9] { 65, 32, 73, 115, 28, 128, 23, 128, 205 },
					new int[9] { 40, 3, 9, 115, 51, 192, 18, 6, 223 },
					new int[9] { 87, 37, 9, 115, 59, 77, 64, 21, 47 }
				},
				new int[10][]
				{
					new int[9] { 104, 55, 44, 218, 9, 54, 53, 130, 226 },
					new int[9] { 64, 90, 70, 205, 40, 41, 23, 26, 57 },
					new int[9] { 54, 57, 112, 184, 5, 41, 38, 166, 213 },
					new int[9] { 30, 34, 26, 133, 152, 116, 10, 32, 134 },
					new int[9] { 75, 32, 12, 51, 192, 255, 160, 43, 51 },
					new int[9] { 39, 19, 53, 221, 26, 114, 32, 73, 255 },
					new int[9] { 31, 9, 65, 234, 2, 15, 1, 118, 73 },
					new int[9] { 88, 31, 35, 67, 102, 85, 55, 186, 85 },
					new int[9] { 56, 21, 23, 111, 59, 205, 45, 37, 192 },
					new int[9] { 55, 38, 70, 124, 73, 102, 1, 34, 98 }
				},
				new int[10][]
				{
					new int[9] { 102, 61, 71, 37, 34, 53, 31, 243, 192 },
					new int[9] { 69, 60, 71, 38, 73, 119, 28, 222, 37 },
					new int[9] { 68, 45, 128, 34, 1, 47, 11, 245, 171 },
					new int[9] { 62, 17, 19, 70, 146, 85, 55, 62, 70 },
					new int[9] { 75, 15, 9, 9, 64, 255, 184, 119, 16 },
					new int[9] { 37, 43, 37, 154, 100, 163, 85, 160, 1 },
					new int[9] { 63, 9, 92, 136, 28, 64, 32, 201, 85 },
					new int[9] { 86, 6, 28, 5, 64, 255, 25, 248, 1 },
					new int[9] { 56, 8, 17, 132, 137, 255, 55, 116, 128 },
					new int[9] { 58, 15, 20, 82, 135, 57, 26, 121, 40 }
				},
				new int[10][]
				{
					new int[9] { 164, 50, 31, 137, 154, 133, 25, 35, 218 },
					new int[9] { 51, 103, 44, 131, 131, 123, 31, 6, 158 },
					new int[9] { 86, 40, 64, 135, 148, 224, 45, 183, 128 },
					new int[9] { 22, 26, 17, 131, 240, 154, 14, 1, 209 },
					new int[9] { 83, 12, 13, 54, 192, 255, 68, 47, 28 },
					new int[9] { 45, 16, 21, 91, 64, 222, 7, 1, 197 },
					new int[9] { 56, 21, 39, 155, 60, 138, 23, 102, 213 },
					new int[9] { 85, 26, 85, 85, 128, 128, 32, 146, 171 },
					new int[9] { 18, 11, 7, 63, 144, 171, 4, 4, 246 },
					new int[9] { 35, 27, 10, 146, 174, 171, 12, 26, 128 }
				},
				new int[10][]
				{
					new int[9] { 190, 80, 35, 99, 180, 80, 126, 54, 45 },
					new int[9] { 85, 126, 47, 87, 176, 51, 41, 20, 32 },
					new int[9] { 101, 75, 128, 139, 118, 146, 116, 128, 85 },
					new int[9] { 56, 41, 15, 176, 236, 85, 37, 9, 62 },
					new int[9] { 146, 36, 19, 30, 171, 255, 97, 27, 20 },
					new int[9] { 71, 30, 17, 119, 118, 255, 17, 18, 138 },
					new int[9] { 101, 38, 60, 138, 55, 70, 43, 26, 142 },
					new int[9] { 138, 45, 61, 62, 219, 1, 81, 188, 64 },
					new int[9] { 32, 41, 20, 117, 151, 142, 20, 21, 163 },
					new int[9] { 112, 19, 12, 61, 195, 128, 48, 4, 24 }
				}
			};
		}
	}

	public const int BLOCK_TYPES = 4;

	public const int COEF_BANDS = 8;

	public const int PREV_COEF_CONTEXTS = 3;

	public const int MAX_ENTROPY_TOKENS = 12;

	public const int MAX_MODE_LF_DELTAS = 4;

	public const int MAX_REF_LF_DELTAS = 4;

	public const int MB_FEATURE_TREE_PROBS = 3;

	public static int[] vp8KeyFrameUVModeProb;

	public static int[] vp8UVModeTree;

	public static int[] keyFrameYModeProb;

	public static int[] keyFrameYModeTree;

	public static int[] segmentTree;

	internal static int[] ___003C_003EPRED_BLOCK_127;

	internal static int[] ___003C_003EPRED_BLOCK_129;

	private static int[][][][] vp8DefaultCoefProbs;

	internal static int[][][][] vp8CoefUpdateProbs;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] PRED_BLOCK_127
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPRED_BLOCK_127;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static int[] PRED_BLOCK_129
	{
		[HideFromJava]
		get
		{
			return ___003C_003EPRED_BLOCK_129;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[] { 159, 38, 98, 101, 101 })]
	public static int getBitInBytes(byte[] bs, int i)
	{
		int byteIndex = i >> 3;
		int bitIndex = i & 7;
		return (bs[byteIndex] >> bitIndex) & 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 37, 162, 99, 103, 50, 167 })]
	public static int getBitsInBytes(byte[] bytes, int idx, int len)
	{
		int val = 0;
		for (int i = 0; i < len; i++)
		{
			val |= getBitInBytes(bytes, idx + i) << i;
		}
		return val;
	}

	[LineNumberTable(new byte[] { 159, 35, 162, 103, 108 })]
	public static int getMacroblockCount(int dimention)
	{
		if (((uint)dimention & 0xFu) != 0)
		{
			dimention += 16 - (dimention & 0xF);
		}
		return dimention >> 4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 137, 106, 100 })]
	public static int delta(VPXBooleanDecoder bc)
	{
		int magnitude = bc.decodeInt(4);
		if (bc.readBitEq() > 0)
		{
			magnitude = -magnitude;
		}
		return magnitude;
	}

	[LineNumberTable(new byte[]
	{
		159, 56, 130, 127, 50, 111, 115, 114, 116, 61,
		41, 41, 44, 234, 69
	})]
	public static int[][][][] getDefaultCoefProbs()
	{
		IntPtr intPtr = (nint)vp8DefaultCoefProbs.LongLength;
		IntPtr intPtr2 = (nint)vp8DefaultCoefProbs[0].LongLength;
		IntPtr intPtr3 = (nint)vp8DefaultCoefProbs[0][0].LongLength;
		IntPtr intPtr4 = (nint)vp8DefaultCoefProbs[0][0][0].LongLength;
		int[] array = new int[4];
		int num = (array[3] = (int)(nint)intPtr4);
		num = (array[2] = (int)(nint)intPtr3);
		num = (array[1] = (int)(nint)intPtr2);
		num = (array[0] = (int)(nint)intPtr);
		int[][][][] r = (int[][][][])ByteCodeHelper.multianewarray(typeof(int[][][][]).TypeHandle, array);
		for (int i = 0; i < (nint)vp8DefaultCoefProbs.LongLength; i++)
		{
			for (int j = 0; j < (nint)vp8DefaultCoefProbs[0].LongLength; j++)
			{
				for (int k = 0; k < (nint)vp8DefaultCoefProbs[0][0].LongLength; k++)
				{
					for (int l = 0; l < (nint)vp8DefaultCoefProbs[0][0][0].LongLength; l++)
					{
						r[i][j][k][l] = vp8DefaultCoefProbs[i][j][k][l];
					}
				}
			}
		}
		return r;
	}

	[LineNumberTable(891)]
	public static int avg2(int x, int y)
	{
		return x + y + 1 >> 1;
	}

	[LineNumberTable(895)]
	public static int avg3(int x, int y, int z)
	{
		return x + y + y + z + 2 >> 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public VP8Util()
	{
	}

	[LineNumberTable(new byte[] { 159, 32, 66, 124, 135 })]
	public static int[] pickDefaultPrediction(int intra_bmode)
	{
		if (intra_bmode == 1 || intra_bmode == 0 || intra_bmode == 2 || intra_bmode == 3 || intra_bmode == 6 || intra_bmode == 5 || intra_bmode == 8)
		{
			return ___003C_003EPRED_BLOCK_129;
		}
		return ___003C_003EPRED_BLOCK_127;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 250, 162, 233, 69, 143, 146, 153, 156, 153,
		253, 70, 127, 32
	})]
	public static int[] predictHU(int[] left)
	{
		int[] p = new int[16]
		{
			avg2(left[0], left[1]),
			avg3(left[0], left[1], left[2]),
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};
		int num = avg2(left[1], left[2]);
		int num2 = 4;
		int[] array = p;
		int num3 = num;
		array[num2] = num;
		p[2] = num3;
		num = avg3(left[1], left[2], left[3]);
		num2 = 5;
		array = p;
		int num4 = num;
		array[num2] = num;
		p[3] = num4;
		num = avg2(left[2], left[3]);
		num2 = 8;
		array = p;
		int num5 = num;
		array[num2] = num;
		p[6] = num5;
		num = avg3(left[2], left[3], left[3]);
		num2 = 9;
		array = p;
		int num6 = num;
		array[num2] = num;
		p[7] = num6;
		num = left[3];
		num2 = 15;
		array = p;
		int num7 = num;
		array[num2] = num;
		num = num7;
		num2 = 14;
		array = p;
		int num8 = num;
		array[num2] = num;
		num = num8;
		num2 = 13;
		array = p;
		int num9 = num;
		array[num2] = num;
		num = num9;
		num2 = 12;
		array = p;
		int num10 = num;
		array[num2] = num;
		num = num10;
		num2 = 11;
		array = p;
		int num11 = num;
		array[num2] = num;
		p[10] = num11;
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 243, 98, 137, 105, 103, 103, 103, 135, 101,
		103, 103, 103, 231, 69, 144, 147, 156, 159, 1,
		156, 159, 0, 155, 158, 146, 114
	})]
	public static int[] predictHD(int[] above, int[] left, int aboveLeft)
	{
		int[] p = new int[16];
		int[] edge = new int[9]
		{
			left[3],
			left[2],
			left[1],
			left[0],
			aboveLeft,
			above[0],
			above[1],
			above[2],
			above[3]
		};
		p[12] = avg2(edge[0], edge[1]);
		p[13] = avg3(edge[0], edge[1], edge[2]);
		int num = avg2(edge[1], edge[2]);
		int num2 = 14;
		int[] array = p;
		int num3 = num;
		array[num2] = num;
		p[8] = num3;
		num = avg3(edge[1], edge[2], edge[3]);
		num2 = 15;
		array = p;
		int num4 = num;
		array[num2] = num;
		p[9] = num4;
		num = avg2(edge[2], edge[3]);
		num2 = 4;
		array = p;
		int num5 = num;
		array[num2] = num;
		p[10] = num5;
		num = avg3(edge[2], edge[3], edge[4]);
		num2 = 5;
		array = p;
		int num6 = num;
		array[num2] = num;
		p[11] = num6;
		num = avg2(edge[3], edge[4]);
		num2 = 0;
		array = p;
		int num7 = num;
		array[num2] = num;
		p[6] = num7;
		num = avg3(edge[3], edge[4], edge[5]);
		num2 = 1;
		array = p;
		int num8 = num;
		array[num2] = num;
		p[7] = num8;
		p[2] = avg3(edge[4], edge[5], edge[6]);
		p[3] = avg3(edge[5], edge[6], edge[7]);
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 233, 98, 233, 69, 207, 210, 217, 221, 218,
		221, 218, 253, 69, 211, 115
	})]
	public static int[] predictVL(int[] above, int[] aboveRight)
	{
		int[] p = new int[16]
		{
			avg2(above[0], above[1]),
			0,
			0,
			0,
			avg3(above[0], above[1], above[2]),
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};
		int num = avg2(above[1], above[2]);
		int num2 = 1;
		int[] array = p;
		int num3 = num;
		array[num2] = num;
		p[8] = num3;
		num = avg3(above[1], above[2], above[3]);
		num2 = 12;
		array = p;
		int num4 = num;
		array[num2] = num;
		p[5] = num4;
		num = avg2(above[2], above[3]);
		num2 = 2;
		array = p;
		int num5 = num;
		array[num2] = num;
		p[9] = num5;
		num = avg3(above[2], above[3], aboveRight[0]);
		num2 = 6;
		array = p;
		int num6 = num;
		array[num2] = num;
		p[13] = num6;
		num = avg2(above[3], aboveRight[0]);
		num2 = 3;
		array = p;
		int num7 = num;
		array[num2] = num;
		p[10] = num7;
		num = avg3(above[3], aboveRight[0], aboveRight[1]);
		num2 = 7;
		array = p;
		int num8 = num;
		array[num2] = num;
		p[14] = num8;
		p[11] = avg3(aboveRight[0], aboveRight[1], aboveRight[2]);
		p[15] = avg3(aboveRight[1], aboveRight[2], aboveRight[3]);
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 221, 66, 201, 105, 103, 103, 103, 199, 101,
		103, 103, 103, 231, 69, 211, 210, 223, 0, 220,
		223, 0, 220, 223, 0, 220, 210, 111
	})]
	public static int[] predictVR(int[] above, int[] left, int aboveLeft)
	{
		int[] p = new int[16];
		int[] edge = new int[9]
		{
			left[3],
			left[2],
			left[1],
			left[0],
			aboveLeft,
			above[0],
			above[1],
			above[2],
			above[3]
		};
		p[12] = avg3(edge[1], edge[2], edge[3]);
		p[8] = avg3(edge[2], edge[3], edge[4]);
		int num = avg3(edge[3], edge[4], edge[5]);
		int num2 = 4;
		int[] array = p;
		int num3 = num;
		array[num2] = num;
		p[13] = num3;
		num = avg2(edge[4], edge[5]);
		num2 = 0;
		array = p;
		int num4 = num;
		array[num2] = num;
		p[9] = num4;
		num = avg3(edge[4], edge[5], edge[6]);
		num2 = 5;
		array = p;
		int num5 = num;
		array[num2] = num;
		p[14] = num5;
		num = avg2(edge[5], edge[6]);
		num2 = 1;
		array = p;
		int num6 = num;
		array[num2] = num;
		p[10] = num6;
		num = avg3(edge[5], edge[6], edge[7]);
		num2 = 6;
		array = p;
		int num7 = num;
		array[num2] = num;
		p[15] = num7;
		num = avg2(edge[6], edge[7]);
		num2 = 2;
		array = p;
		int num8 = num;
		array[num2] = num;
		p[11] = num8;
		p[7] = avg3(edge[6], edge[7], edge[8]);
		p[3] = avg2(edge[7], edge[8]);
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 201, 98, 201, 105, 103, 103, 103, 199, 101,
		103, 103, 103, 231, 69, 211, 223, 0, 223, 13,
		223, 25, 223, 12, 222, 114
	})]
	public static int[] predictRD(int[] above, int[] left, int aboveLeft)
	{
		int[] p = new int[16];
		int[] edge = new int[9]
		{
			left[3],
			left[2],
			left[1],
			left[0],
			aboveLeft,
			above[0],
			above[1],
			above[2],
			above[3]
		};
		p[12] = avg3(edge[0], edge[1], edge[2]);
		int num = avg3(edge[1], edge[2], edge[3]);
		int num2 = 8;
		int[] array = p;
		int num3 = num;
		array[num2] = num;
		p[13] = num3;
		num = avg3(edge[2], edge[3], edge[4]);
		num2 = 4;
		array = p;
		int num4 = num;
		array[num2] = num;
		num = num4;
		num2 = 9;
		array = p;
		int num5 = num;
		array[num2] = num;
		p[14] = num5;
		num = avg3(edge[3], edge[4], edge[5]);
		num2 = 0;
		array = p;
		int num6 = num;
		array[num2] = num;
		num = num6;
		num2 = 5;
		array = p;
		int num7 = num;
		array[num2] = num;
		num = num7;
		num2 = 10;
		array = p;
		int num8 = num;
		array[num2] = num;
		p[15] = num8;
		num = avg3(edge[4], edge[5], edge[6]);
		num2 = 1;
		array = p;
		int num9 = num;
		array[num2] = num;
		num = num9;
		num2 = 6;
		array = p;
		int num10 = num;
		array[num2] = num;
		p[11] = num10;
		num = avg3(edge[5], edge[6], edge[7]);
		num2 = 2;
		array = p;
		int num11 = num;
		array[num2] = num;
		p[7] = num11;
		p[3] = avg3(edge[6], edge[7], edge[8]);
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 189, 162, 233, 71, 114, 124, 127, 7, 127,
		19, 127, 9, 126, 115
	})]
	public static int[] predictLD(int[] above, int[] aboveRight)
	{
		int[] p = new int[16]
		{
			avg3(above[0], above[1], above[2]),
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};
		int num = avg3(above[1], above[2], above[3]);
		int num2 = 4;
		int[] array = p;
		int num3 = num;
		array[num2] = num;
		p[1] = num3;
		num = avg3(above[2], above[3], aboveRight[0]);
		num2 = 8;
		array = p;
		int num4 = num;
		array[num2] = num;
		num = num4;
		num2 = 5;
		array = p;
		int num5 = num;
		array[num2] = num;
		p[2] = num5;
		num = avg3(above[3], aboveRight[0], aboveRight[1]);
		num2 = 12;
		array = p;
		int num6 = num;
		array[num2] = num;
		num = num6;
		num2 = 9;
		array = p;
		int num7 = num;
		array[num2] = num;
		num = num7;
		num2 = 6;
		array = p;
		int num8 = num;
		array[num2] = num;
		p[3] = num8;
		num = avg3(aboveRight[0], aboveRight[1], aboveRight[2]);
		num2 = 13;
		array = p;
		int num9 = num;
		array[num2] = num;
		num = num9;
		num2 = 10;
		array = p;
		int num10 = num;
		array[num2] = num;
		p[7] = num10;
		num = avg3(aboveRight[1], aboveRight[2], aboveRight[3]);
		num2 = 14;
		array = p;
		int num11 = num;
		array[num2] = num;
		p[11] = num11;
		p[15] = avg3(aboveRight[2], aboveRight[3], aboveRight[3]);
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 184, 98, 201, 208, 127, 27, 127, 26, 127,
		23, 127, 21
	})]
	public static int[] predictHE(int[] left, int aboveLeft)
	{
		int[] p = new int[16];
		int v = avg3(left[2], left[3], left[3]);
		int num = avg3(left[2], left[3], left[3]);
		int num2 = 15;
		int[] array = p;
		int num3 = num;
		array[num2] = num;
		num = num3;
		num2 = 14;
		array = p;
		int num4 = num;
		array[num2] = num;
		num = num4;
		num2 = 13;
		array = p;
		int num5 = num;
		array[num2] = num;
		p[12] = num5;
		num = avg3(left[1], left[2], left[3]);
		num2 = 11;
		array = p;
		int num6 = num;
		array[num2] = num;
		num = num6;
		num2 = 10;
		array = p;
		int num7 = num;
		array[num2] = num;
		num = num7;
		num2 = 9;
		array = p;
		int num8 = num;
		array[num2] = num;
		p[8] = num8;
		num = avg3(left[0], left[1], left[2]);
		num2 = 7;
		array = p;
		int num9 = num;
		array[num2] = num;
		num = num9;
		num2 = 6;
		array = p;
		int num10 = num;
		array[num2] = num;
		num = num10;
		num2 = 5;
		array = p;
		int num11 = num;
		array[num2] = num;
		p[4] = num11;
		num = avg3(aboveLeft, left[0], left[1]);
		num2 = 3;
		array = p;
		int num12 = num;
		array[num2] = num;
		num = num12;
		num2 = 2;
		array = p;
		int num13 = num;
		array[num2] = num;
		num = num13;
		num2 = 1;
		array = p;
		int num14 = num;
		array[num2] = num;
		p[0] = num14;
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 178, 98, 105, 127, 16, 127, 19, 127, 19,
		159, 19
	})]
	public static int[] predictVE(int[] above, int aboveLeft, int[] aboveRight)
	{
		int[] p = new int[16];
		int num = avg3(aboveLeft, above[0], above[1]);
		int num2 = 12;
		int[] array = p;
		int num3 = num;
		array[num2] = num;
		num = num3;
		num2 = 8;
		array = p;
		int num4 = num;
		array[num2] = num;
		num = num4;
		num2 = 4;
		array = p;
		int num5 = num;
		array[num2] = num;
		p[0] = num5;
		num = avg3(above[0], above[1], above[2]);
		num2 = 13;
		array = p;
		int num6 = num;
		array[num2] = num;
		num = num6;
		num2 = 9;
		array = p;
		int num7 = num;
		array[num2] = num;
		num = num7;
		num2 = 5;
		array = p;
		int num8 = num;
		array[num2] = num;
		p[1] = num8;
		num = avg3(above[1], above[2], above[3]);
		num2 = 14;
		array = p;
		int num9 = num;
		array[num2] = num;
		num = num9;
		num2 = 10;
		array = p;
		int num10 = num;
		array[num2] = num;
		num = num10;
		num2 = 6;
		array = p;
		int num11 = num;
		array[num2] = num;
		p[2] = num11;
		num = avg3(above[2], above[3], aboveRight[0]);
		num2 = 15;
		array = p;
		int num12 = num;
		array[num2] = num;
		num = num12;
		num2 = 11;
		array = p;
		int num13 = num;
		array[num2] = num;
		num = num13;
		num2 = 7;
		array = p;
		int num14 = num;
		array[num2] = num;
		p[3] = num14;
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 174, 162, 105, 103, 103, 54, 39, 167 })]
	public static int[] predictTM(int[] above, int[] left, int aboveLeft)
	{
		int[] p = new int[16];
		for (int aRow = 0; aRow < 4; aRow++)
		{
			for (int aCol = 0; aCol < 4; aCol++)
			{
				p[aRow * 4 + aCol] = QuantizationParams.clip255(left[aRow] + above[aCol] - aboveLeft);
			}
		}
		return p;
	}

	[LineNumberTable(new byte[]
	{
		158, 172, 162, 201, 99, 131, 107, 201, 133, 199,
		105, 42, 233, 60, 231, 70
	})]
	public static int[] predictDC(int[] above, int[] left)
	{
		int[] p = new int[16];
		int v = 4;
		int i = 0;
		do
		{
			v += above[i] + left[i];
			i++;
		}
		while (i < 4);
		v >>= 3;
		for (int aRow = 0; aRow < 4; aRow++)
		{
			for (int aCol = 0; aCol < 4; aCol++)
			{
				p[aRow * 4 + aCol] = v;
			}
		}
		return p;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 166, 162, 159, 7, 131, 105, 131, 131, 131,
		131
	})]
	public static int planeToType(PLANE plane, java.lang.Boolean withY2)
	{
		switch (_1._0024SwitchMap_0024org_0024jcodec_0024codecs_0024vpx_0024VP8Util_0024PLANE[plane.ordinal()])
		{
		case 1:
			return 1;
		case 2:
			if (withY2.booleanValue())
			{
				return 0;
			}
			return 3;
		case 3:
			return 2;
		case 4:
			return 2;
		default:
			return -1;
		}
	}

	[LineNumberTable(new byte[]
	{
		159,
		54,
		162,
		byte.MaxValue,
		2,
		80,
		byte.MaxValue,
		7,
		70,
		byte.MaxValue,
		13,
		90,
		byte.MaxValue,
		16,
		70,
		byte.MaxValue,
		7,
		91,
		127,
		69,
		byte.MaxValue,
		117,
		160,
		82,
		byte.MaxValue,
		208,
		36,
		247,
		101
	})]
	static VP8Util()
	{
		vp8KeyFrameUVModeProb = new int[3] { 142, 114, 183 };
		vp8UVModeTree = new int[6] { 0, 2, -1, 4, -2, -3 };
		keyFrameYModeProb = new int[4] { 145, 156, 163, 128 };
		keyFrameYModeTree = new int[8] { -4, 2, 4, 6, 0, -1, -2, -3 };
		segmentTree = new int[6] { 2, 4, 0, -1, -2, -3 };
		___003C_003EPRED_BLOCK_127 = new int[16]
		{
			127, 127, 127, 127, 127, 127, 127, 127, 127, 127,
			127, 127, 127, 127, 127, 127
		};
		___003C_003EPRED_BLOCK_129 = new int[16]
		{
			129, 129, 129, 129, 129, 129, 129, 129, 129, 129,
			129, 129, 129, 129, 129, 129
		};
		vp8DefaultCoefProbs = new int[4][][][]
		{
			new int[8][][]
			{
				new int[3][]
				{
					new int[11]
					{
						128, 128, 128, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						128, 128, 128, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						128, 128, 128, 128, 128, 128, 128, 128, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						253, 136, 254, 255, 228, 219, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						189, 129, 242, 255, 227, 213, 255, 219, 128, 128,
						128
					},
					new int[11]
					{
						106, 126, 227, 252, 214, 209, 255, 255, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 98, 248, 255, 236, 226, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						181, 133, 238, 254, 221, 234, 255, 154, 128, 128,
						128
					},
					new int[11]
					{
						78, 134, 202, 247, 198, 180, 255, 219, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 185, 249, 255, 243, 255, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						184, 150, 247, 255, 236, 224, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						77, 110, 216, 255, 236, 230, 128, 128, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 101, 251, 255, 241, 255, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						170, 139, 241, 252, 236, 209, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						37, 116, 196, 243, 228, 255, 255, 255, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 204, 254, 255, 245, 255, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						207, 160, 250, 255, 238, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						102, 103, 231, 255, 211, 171, 128, 128, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 152, 252, 255, 240, 255, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						177, 135, 243, 255, 234, 225, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						80, 129, 211, 255, 194, 224, 128, 128, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 1, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						246, 1, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						255, 128, 128, 128, 128, 128, 128, 128, 128, 128,
						128
					}
				}
			},
			new int[8][][]
			{
				new int[3][]
				{
					new int[11]
					{
						198, 35, 237, 223, 193, 187, 162, 160, 145, 155,
						62
					},
					new int[11]
					{
						131, 45, 198, 221, 172, 176, 220, 157, 252, 221,
						1
					},
					new int[11]
					{
						68, 47, 146, 208, 149, 167, 221, 162, 255, 223,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 149, 241, 255, 221, 224, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						184, 141, 234, 253, 222, 220, 255, 199, 128, 128,
						128
					},
					new int[11]
					{
						81, 99, 181, 242, 176, 190, 249, 202, 255, 255,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 129, 232, 253, 214, 197, 242, 196, 255, 255,
						128
					},
					new int[11]
					{
						99, 121, 210, 250, 201, 198, 255, 202, 128, 128,
						128
					},
					new int[11]
					{
						23, 91, 163, 242, 170, 187, 247, 210, 255, 255,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 200, 246, 255, 234, 255, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						109, 178, 241, 255, 231, 245, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						44, 130, 201, 253, 205, 192, 255, 255, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 132, 239, 251, 219, 209, 255, 165, 128, 128,
						128
					},
					new int[11]
					{
						94, 136, 225, 251, 218, 190, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						22, 100, 174, 245, 186, 161, 255, 199, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 182, 249, 255, 232, 235, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						124, 143, 241, 255, 227, 234, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						35, 77, 181, 251, 193, 211, 255, 205, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 157, 247, 255, 236, 231, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						121, 141, 235, 255, 225, 227, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						45, 99, 188, 251, 195, 217, 255, 224, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 1, 251, 255, 213, 255, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						203, 1, 248, 255, 255, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						137, 1, 177, 255, 224, 255, 128, 128, 128, 128,
						128
					}
				}
			},
			new int[8][][]
			{
				new int[3][]
				{
					new int[11]
					{
						253, 9, 248, 251, 207, 208, 255, 192, 128, 128,
						128
					},
					new int[11]
					{
						175, 13, 224, 243, 193, 185, 249, 198, 255, 255,
						128
					},
					new int[11]
					{
						73, 17, 171, 221, 161, 179, 236, 167, 255, 234,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 95, 247, 253, 212, 183, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						239, 90, 244, 250, 211, 209, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						155, 77, 195, 248, 188, 195, 255, 255, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 24, 239, 251, 218, 219, 255, 205, 128, 128,
						128
					},
					new int[11]
					{
						201, 51, 219, 255, 196, 186, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						69, 46, 190, 239, 201, 218, 255, 228, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 191, 251, 255, 255, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						223, 165, 249, 255, 213, 255, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						141, 124, 248, 255, 255, 128, 128, 128, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 16, 248, 255, 255, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						190, 36, 230, 255, 236, 255, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						149, 1, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 226, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						247, 192, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						240, 128, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 134, 252, 255, 255, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						213, 62, 250, 255, 255, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						55, 93, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						128, 128, 128, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						128, 128, 128, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						128, 128, 128, 128, 128, 128, 128, 128, 128, 128,
						128
					}
				}
			},
			new int[8][][]
			{
				new int[3][]
				{
					new int[11]
					{
						202, 24, 213, 235, 186, 191, 220, 160, 240, 175,
						255
					},
					new int[11]
					{
						126, 38, 182, 232, 169, 184, 228, 174, 255, 187,
						128
					},
					new int[11]
					{
						61, 46, 138, 219, 151, 178, 240, 170, 255, 216,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 112, 230, 250, 199, 191, 247, 159, 255, 255,
						128
					},
					new int[11]
					{
						166, 109, 228, 252, 211, 215, 255, 174, 128, 128,
						128
					},
					new int[11]
					{
						39, 77, 162, 232, 172, 180, 245, 178, 255, 255,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 52, 220, 246, 198, 199, 249, 220, 255, 255,
						128
					},
					new int[11]
					{
						124, 74, 191, 243, 183, 193, 250, 221, 255, 255,
						128
					},
					new int[11]
					{
						24, 71, 130, 219, 154, 170, 243, 182, 255, 255,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 182, 225, 249, 219, 240, 255, 224, 128, 128,
						128
					},
					new int[11]
					{
						149, 150, 226, 252, 216, 205, 255, 171, 128, 128,
						128
					},
					new int[11]
					{
						28, 108, 170, 242, 183, 194, 254, 223, 255, 255,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 81, 230, 252, 204, 203, 255, 192, 128, 128,
						128
					},
					new int[11]
					{
						123, 102, 209, 247, 188, 196, 255, 233, 128, 128,
						128
					},
					new int[11]
					{
						20, 95, 153, 243, 164, 173, 255, 203, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 222, 248, 255, 216, 213, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						168, 175, 246, 252, 235, 205, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						47, 116, 215, 255, 211, 212, 255, 255, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 121, 236, 253, 212, 214, 255, 255, 128, 128,
						128
					},
					new int[11]
					{
						141, 84, 213, 252, 201, 202, 255, 219, 128, 128,
						128
					},
					new int[11]
					{
						42, 80, 160, 240, 162, 185, 255, 205, 128, 128,
						128
					}
				},
				new int[3][]
				{
					new int[11]
					{
						1, 1, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						244, 1, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					},
					new int[11]
					{
						238, 1, 255, 128, 128, 128, 128, 128, 128, 128,
						128
					}
				}
			}
		};
		vp8CoefUpdateProbs = new int[4][][][]
		{
			new int[8][][]
			{
				new int[3][]
				{
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						176, 246, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						223, 241, 252, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						249, 253, 253, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 244, 252, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						234, 254, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						253, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 246, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						239, 253, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						254, 255, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 248, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						251, 255, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 253, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						251, 254, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						254, 255, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 254, 253, 255, 254, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						250, 255, 254, 255, 254, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						254, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				}
			},
			new int[8][][]
			{
				new int[3][]
				{
					new int[11]
					{
						217, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						225, 252, 241, 253, 255, 255, 254, 255, 255, 255,
						255
					},
					new int[11]
					{
						234, 250, 241, 250, 253, 255, 253, 254, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 254, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						223, 254, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						238, 253, 254, 254, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 248, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						249, 254, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 253, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						247, 254, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 253, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						252, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 254, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						253, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 254, 253, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						250, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						254, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				}
			},
			new int[8][][]
			{
				new int[3][]
				{
					new int[11]
					{
						186, 251, 250, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						234, 251, 244, 254, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						251, 251, 243, 253, 254, 255, 254, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 253, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						236, 253, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						251, 253, 253, 254, 254, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 254, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						254, 254, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 254, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						254, 254, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						254, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						254, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				}
			},
			new int[8][][]
			{
				new int[3][]
				{
					new int[11]
					{
						248, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						250, 254, 252, 254, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						248, 254, 249, 253, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 253, 253, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						246, 253, 253, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						252, 254, 251, 254, 254, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 254, 252, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						248, 254, 253, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						253, 255, 254, 254, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 251, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						245, 251, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						253, 253, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 251, 253, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						252, 253, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 254, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 252, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						249, 255, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 254, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 255, 253, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						250, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				},
				new int[3][]
				{
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						254, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					},
					new int[11]
					{
						255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
						255
					}
				}
			}
		};
	}
}
