using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.huffman;

namespace net.sourceforge.jaad.aac.syntax;

[Implements(new string[] { "net.sourceforge.jaad.aac.syntax.SyntaxConstants" })]
internal class CCE : Element, SyntaxConstants
{
	public const int BEFORE_TNS = 0;

	public const int AFTER_TNS = 1;

	public const int AFTER_IMDCT = 2;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static float[] CCE_SCALE;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private ICStream ics;

	private float[] iqData;

	private int couplingPoint;

	private int coupledCount;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private bool[] channelPair;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int[] idSelect;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int[] chSelect;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[][] gain;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 105, 109, 109, 109, 109, 127, 13 })]
	internal CCE(int frameLength)
	{
		ics = new ICStream(frameLength);
		channelPair = new bool[8];
		idSelect = new int[8];
		chSelect = new int[8];
		int[] array = new int[2];
		int num = (array[1] = 120);
		num = (array[0] = 16);
		gain = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
	}

	[LineNumberTable(47)]
	internal virtual int getCouplingPoint()
	{
		return couplingPoint;
	}

	[LineNumberTable(51)]
	internal virtual int getCoupledCount()
	{
		return coupledCount;
	}

	[LineNumberTable(55)]
	internal virtual bool isChannelPair(int index)
	{
		return channelPair[index];
	}

	[LineNumberTable(59)]
	internal virtual int getIDSelect(int index)
	{
		return idSelect[index];
	}

	[LineNumberTable(63)]
	internal virtual int getCHSelect(int index)
	{
		return chSelect[index];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 126, 162, 111, 110, 131, 111, 101, 111, 112,
		107, 112, 146, 234, 56, 234, 74, 116, 150, 104,
		144, 111, 110, 106, 138, 146, 106, 100, 100, 100,
		104, 101, 117, 115, 142, 187, 109, 109, 110, 101,
		108, 101, 100, 107, 100, 108, 135, 178, 238, 50,
		50, 236, 51, 234, 98
	})]
	internal virtual void decode(IBitStream _in, AACDecoderConfig conf)
	{
		couplingPoint = 2 * _in.readBit();
		coupledCount = _in.readBits(3);
		int gainCount = 0;
		for (int i = 0; i <= coupledCount; i++)
		{
			gainCount++;
			channelPair[i] = _in.readBool();
			idSelect[i] = _in.readBits(4);
			if (channelPair[i])
			{
				chSelect[i] = _in.readBits(2);
				if (chSelect[i] == 3)
				{
					gainCount++;
				}
			}
			else
			{
				chSelect[i] = 2;
			}
		}
		couplingPoint += _in.readBit();
		couplingPoint |= couplingPoint >> 1;
		int sign = (_in.readBool() ? 1 : 0);
		double scale = CCE_SCALE[_in.readBits(2)];
		ics.decode(_in, commonWindow: false, conf);
		ICSInfo info = ics.getInfo();
		int windowGroupCount = info.getWindowGroupCount();
		int maxSFB = info.getMaxSFB();
		int[][] sfbCB = new int[1][] { new int[0] };
		for (int i = 0; i < gainCount; i++)
		{
			int idx = 0;
			int cge = 1;
			int xg = 0;
			float gainCache = 1f;
			if (i > 0)
			{
				cge = ((couplingPoint == 2) ? 1 : _in.readBit());
				xg = ((cge != 0) ? (Huffman.decodeScaleFactor(_in) - 60) : 0);
				gainCache = (float)Math.pow(scale, -xg);
			}
			if (couplingPoint == 2)
			{
				gain[i][0] = gainCache;
				continue;
			}
			for (int g = 0; g < windowGroupCount; g++)
			{
				int sfb = 0;
				while (sfb < maxSFB)
				{
					if (sfbCB[g][sfb] != 0)
					{
						if (cge == 0)
						{
							int t = Huffman.decodeScaleFactor(_in) - 60;
							if (t != 0)
							{
								int s = 1;
								t = (xg += t);
								if (sign == 0)
								{
									s -= 2 * (t & 1);
									t >>= 1;
								}
								gainCache = (float)(Math.pow(scale, -t) * (double)s);
							}
						}
						gain[i][idx] = gainCache;
					}
					sfb++;
					idx++;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 110, 162, 114 })]
	internal virtual void process()
	{
		iqData = ics.getInvQuantData();
	}

	[LineNumberTable(new byte[] { 159, 109, 162, 109, 104, 57, 167 })]
	internal virtual void applyIndependentCoupling(int index, float[] data)
	{
		double g = gain[index][0];
		for (int i = 0; i < (nint)data.LongLength; i++)
		{
			int num = i;
			data[num] = (float)((double)data[num] + g * (double)iqData[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 107, 130, 109, 104, 104, 136, 146, 100, 132,
		132, 108, 107, 108, 110, 110, 109, 113, 63, 26,
		41, 236, 61, 242, 74, 110, 238, 51, 236, 79
	})]
	internal virtual void applyDependentCoupling(int index, float[] data)
	{
		ICSInfo info = ics.getInfo();
		int[] swbOffsets = info.getSWBOffsets();
		int windowGroupCount = info.getWindowGroupCount();
		int maxSFB = info.getMaxSFB();
		int[][] sfbCB = new int[1][] { new int[0] };
		int srcOff = 0;
		int dstOff = 0;
		int idx = 0;
		for (int g = 0; g < windowGroupCount; g++)
		{
			int len = info.getWindowGroupLength(g);
			int sfb = 0;
			while (sfb < maxSFB)
			{
				if (sfbCB[g][sfb] != 0)
				{
					float x = gain[index][idx];
					for (int group = 0; group < len; group++)
					{
						for (int i = swbOffsets[sfb]; i < swbOffsets[sfb + 1]; i++)
						{
							int num = dstOff + group * 128 + i;
							data[num] += x * iqData[srcOff + group * 128 + i];
						}
					}
				}
				sfb++;
				idx++;
			}
			dstOff += len * 128;
			srcOff += len * 128;
		}
	}

	[LineNumberTable(20)]
	static CCE()
	{
		CCE_SCALE = new float[4] { 1.09050775f, 1.18920708f, 1.41421354f, 2f };
	}
}
