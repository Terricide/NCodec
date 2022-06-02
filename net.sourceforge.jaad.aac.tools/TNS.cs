using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.tools;

[Implements(new string[] { "net.sourceforge.jaad.aac.syntax.SyntaxConstants", "net.sourceforge.jaad.aac.tools.TNSTables" })]
public class TNS : Object, SyntaxConstants, TNSTables
{
	private const int TNS_MAX_ORDER = 20;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] SHORT_BITS;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] LONG_BITS;

	private int[] nFilt;

	private int[][] length;

	private int[][] order;

	private bool[][] direction;

	private float[][][] coef;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] TNS_COEF_1_3
	{
		[HideFromJava]
		get
		{
			return TNSTables.TNS_COEF_1_3;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] TNS_COEF_0_3
	{
		[HideFromJava]
		get
		{
			return TNSTables.TNS_COEF_0_3;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] TNS_COEF_1_4
	{
		[HideFromJava]
		get
		{
			return TNSTables.TNS_COEF_1_4;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] TNS_COEF_0_4
	{
		[HideFromJava]
		get
		{
			return TNSTables.TNS_COEF_0_4;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[][] TNS_TABLES
	{
		[HideFromJava]
		get
		{
			return TNSTables.TNS_TABLES;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(66)]
	public virtual void process(ICStream ics, float[] spec, SampleFrequency sf, bool decode)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 105, 109, 127, 11, 127, 11, 127,
		11, 127, 18
	})]
	public TNS()
	{
		nFilt = new int[8];
		int[] array = new int[2];
		int num = (array[1] = 4);
		num = (array[0] = 8);
		length = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 4);
		num = (array[0] = 8);
		direction = (bool[][])ByteCodeHelper.multianewarray(typeof(bool[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 4);
		num = (array[0] = 8);
		order = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		array = new int[3];
		num = (array[2] = 20);
		num = (array[1] = 4);
		num = (array[0] = 8);
		coef = (float[][][])ByteCodeHelper.multianewarray(typeof(float[][][]).TypeHandle, array);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 104, 182, 106, 127, 3, 137, 115,
		149, 127, 52, 113, 114, 105, 106, 138, 115, 63,
		1, 233, 54, 236, 60, 234, 85
	})]
	public virtual void decode(IBitStream _in, ICSInfo info)
	{
		int windowCount = info.getWindowCount();
		int[] bits = ((!info.isEightShortFrame()) ? LONG_BITS : SHORT_BITS);
		for (int w = 0; w < windowCount; w++)
		{
			int[] array = nFilt;
			int num = w;
			int num2 = _in.readBits(bits[0]);
			int num3 = num;
			int[] array2 = array;
			int num4 = num2;
			array2[num3] = num2;
			if (num4 == 0)
			{
				continue;
			}
			int coefRes = _in.readBit();
			for (int filt = 0; filt < nFilt[w]; filt++)
			{
				length[w][filt] = _in.readBits(bits[1]);
				int[] obj = order[w];
				int num5 = filt;
				num2 = _in.readBits(bits[2]);
				num3 = num5;
				array2 = obj;
				int num6 = num2;
				array2[num3] = num2;
				if (num6 > 20)
				{
					string message = new StringBuilder().append("TNS filter out of range: ").append(order[w][filt]).toString();
					
					throw new AACException(message);
				}
				if (order[w][filt] != 0)
				{
					direction[w][filt] = _in.readBool();
					int coefCompress = _in.readBit();
					int coefLen = coefRes + 3 - coefCompress;
					int tmp = 2 * coefCompress + coefRes;
					for (int i = 0; i < order[w][filt]; i++)
					{
						coef[w][filt][i] = TNSTables.TNS_TABLES[tmp][_in.readBits(coefLen)];
					}
				}
			}
		}
	}

	[LineNumberTable(21)]
	static TNS()
	{
		SHORT_BITS = new int[3] { 1, 4, 3 };
		LONG_BITS = new int[3] { 2, 6, 5 };
	}
}
