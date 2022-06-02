using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.error;

public class RVLC : Object, RVLCTables
{
	private const int ESCAPE_FLAG = 7;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] RVLC_BOOK
	{
		[HideFromJava]
		get
		{
			return RVLCTables.RVLC_BOOK;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static int[][] ESCAPE_BOOK
	{
		[HideFromJava]
		get
		{
			return RVLCTables.ESCAPE_BOOK;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 117, 66, 99, 107, 169, 114, 101, 109, 101,
		104, 173
	})]
	private int decodeHuffman(IBitStream _in)
	{
		int off = 0;
		int i = RVLCTables.RVLC_BOOK[off][1];
		int cw = _in.readBits(i);
		while (cw != RVLCTables.RVLC_BOOK[off][2] && i < 10)
		{
			off++;
			int j = RVLCTables.RVLC_BOOK[off][1] - i;
			i += j;
			cw <<= j;
			cw |= _in.readBits(j);
		}
		return RVLCTables.RVLC_BOOK[off][0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 123, 98, 104, 104, 104, 145, 138, 164, 108,
		108, 120, 112, 106, 127, 6, 248, 59, 44, 236,
		74
	})]
	private void decodeEscapes(IBitStream _in, ICStream ics, int[][] scaleFactors)
	{
		ICSInfo info = ics.getInfo();
		int windowGroupCount = info.getWindowGroupCount();
		int maxSFB = info.getMaxSFB();
		int[][] sfbCB = new int[1][] { new int[0] };
		int escapesLen = _in.readBits(8);
		int noiseUsed = 0;
		for (int g = 0; g < windowGroupCount; g++)
		{
			for (int sfb = 0; sfb < maxSFB; sfb++)
			{
				if (sfbCB[g][sfb] == 13 && noiseUsed == 0)
				{
					noiseUsed = 1;
				}
				else if (Math.abs(sfbCB[g][sfb]) == 7)
				{
					int val = decodeHuffmanEscape(_in);
					if (sfbCB[g][sfb] == -7)
					{
						int[] obj = scaleFactors[g];
						int num = sfb;
						int[] array = obj;
						array[num] -= val;
					}
					else
					{
						int[] obj2 = scaleFactors[g];
						int num = sfb;
						int[] array = obj2;
						array[num] += val;
					}
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 113, 98, 99, 107, 169, 114, 101, 109, 101,
		104, 173
	})]
	private int decodeHuffmanEscape(IBitStream _in)
	{
		int off = 0;
		int i = RVLCTables.ESCAPE_BOOK[off][1];
		int cw = _in.readBits(i);
		while (cw != RVLCTables.ESCAPE_BOOK[off][2] && i < 21)
		{
			off++;
			int j = RVLCTables.ESCAPE_BOOK[off][1] - i;
			i += j;
			cw <<= j;
			cw |= _in.readBits(j);
		}
		return RVLCTables.ESCAPE_BOOK[off][0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(19)]
	public RVLC()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 136, 66, 117, 104, 105, 137, 105, 106, 106,
		146, 105, 100, 110, 167, 109, 109, 159, 15, 105,
		166, 104, 109, 106, 131, 101, 109, 172, 100, 138,
		131, 109, 234, 41, 44, 236, 94, 100, 110, 100,
		116
	})]
	public virtual void decode(IBitStream _in, ICStream ics, int[][] scaleFactors)
	{
		int bits = ((!ics.getInfo().isEightShortFrame()) ? 9 : 11);
		int sfConcealment = (_in.readBool() ? 1 : 0);
		int revGlobalGain = _in.readBits(8);
		int rvlcSFLen = _in.readBits(bits);
		ICSInfo info = ics.getInfo();
		int windowGroupCount = info.getWindowGroupCount();
		int maxSFB = info.getMaxSFB();
		int[][] sfbCB = new int[1][] { new int[0] };
		int sf = ics.getGlobalGain();
		int intensityPosition = 0;
		int noiseEnergy = sf - 90 - 256;
		int intensityUsed = 0;
		int noiseUsed = 0;
		for (int g = 0; g < windowGroupCount; g++)
		{
			for (int sfb = 0; sfb < maxSFB; sfb++)
			{
				switch (sfbCB[g][sfb])
				{
				case 0:
					scaleFactors[g][sfb] = 0;
					break;
				case 14:
				case 15:
					if (intensityUsed == 0)
					{
						intensityUsed = 1;
					}
					intensityPosition += decodeHuffman(_in);
					scaleFactors[g][sfb] = intensityPosition;
					break;
				case 13:
					if (noiseUsed != 0)
					{
						noiseEnergy += decodeHuffman(_in);
						scaleFactors[g][sfb] = noiseEnergy;
					}
					else
					{
						noiseUsed = 1;
						noiseEnergy = decodeHuffman(_in);
					}
					break;
				default:
					sf += decodeHuffman(_in);
					scaleFactors[g][sfb] = sf;
					break;
				}
			}
		}
		int lastIntensityPosition = 0;
		if (intensityUsed != 0)
		{
			lastIntensityPosition = decodeHuffman(_in);
		}
		noiseUsed = 0;
		if (_in.readBool())
		{
			decodeEscapes(_in, ics, scaleFactors);
		}
	}
}
