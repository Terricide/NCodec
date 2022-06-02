using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace org.jcodec.codecs.mpeg4;

public class Macroblock : Object
{
	public class Vector : Object
	{
		public int x;

		public int y;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 138, 130, 105, 104, 104 })]
		public Vector(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public const int MBPRED_SIZE = 15;

	public Vector[] mvs;

	public short[][] predValues;

	public int[] acpredDirections;

	public int mode;

	public int quant;

	public bool fieldDCT;

	public bool fieldPred;

	public bool fieldForTop;

	public bool fieldForBottom;

	private Vector[] pmvs;

	private Vector[] qmvs;

	public int cbp;

	public Vector[] bmvs;

	public Vector[] bqmvs;

	public Vector amv;

	public Vector mvsAvg;

	public int x;

	public int y;

	public int bound;

	public bool acpredFlag;

	public short[] predictors;

	public short[][] block;

	public bool coded;

	public bool mcsel;

	public byte[][] pred;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public static Vector vec()
	{
		Vector result = new Vector(0, 0);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 66, 105, 109, 109, 109, 109, 141, 103,
		110, 110, 110, 110, 238, 59, 231, 71, 127, 48,
		127, 12, 109, 108, 109, 127, 12
	})]
	public Macroblock()
	{
		mvs = new Vector[4];
		pmvs = new Vector[4];
		qmvs = new Vector[4];
		bmvs = new Vector[4];
		bqmvs = new Vector[4];
		for (int i = 0; i < 4; i++)
		{
			mvs[i] = vec();
			pmvs[i] = vec();
			qmvs[i] = vec();
			bmvs[i] = vec();
			bqmvs[i] = vec();
		}
		pred = new byte[6][]
		{
			new byte[256],
			new byte[64],
			new byte[64],
			new byte[256],
			new byte[64],
			new byte[64]
		};
		int[] array = new int[2];
		int num = (array[1] = 15);
		num = (array[0] = 6);
		predValues = (short[][])ByteCodeHelper.multianewarray(typeof(short[][]).TypeHandle, array);
		acpredDirections = new int[6];
		amv = vec();
		predictors = new short[8];
		array = new int[2];
		num = (array[1] = 64);
		num = (array[0] = 6);
		block = (short[][])ByteCodeHelper.multianewarray(typeof(short[][]).TypeHandle, array);
	}

	[LineNumberTable(new byte[] { 159, 120, 162, 104, 104, 136 })]
	public virtual void reset(int x2, int y2, int bound2)
	{
		x = x2;
		y = y2;
		bound = bound2;
	}
}
