using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using net.sourceforge.jaad.aac.huffman;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.tools;

[Implements(new string[] { "net.sourceforge.jaad.aac.syntax.SyntaxConstants", "net.sourceforge.jaad.aac.huffman.HCB" })]
public sealed class MS : Object, SyntaxConstants, HCB
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 104, 104, 104, 104, 105, 105, 110,
		100, 132, 108, 109, 127, 7, 115, 115, 115, 115,
		126, 234, 61, 12, 12, 242, 76, 244, 51, 236,
		79
	})]
	public static void process(CPE cpe, float[] specL, float[] specR)
	{
		ICStream ics = cpe.getLeftChannel();
		ICSInfo info = ics.getInfo();
		int[] offsets = info.getSWBOffsets();
		int windowGroups = info.getWindowGroupCount();
		int maxSFB = info.getMaxSFB();
		int[] sfbCBl = ics.getSfbCB();
		int[] sfbCBr = cpe.getRightChannel().getSfbCB();
		int groupOff = 0;
		int idx = 0;
		for (int g = 0; g < windowGroups; g++)
		{
			int i = 0;
			while (i < maxSFB)
			{
				if (cpe.isMSUsed(idx) && sfbCBl[idx] < 13 && sfbCBr[idx] < 13)
				{
					for (int w = 0; w < info.getWindowGroupLength(g); w++)
					{
						int off = groupOff + w * 128 + offsets[i];
						for (int j = 0; j < offsets[i + 1] - offsets[i]; j++)
						{
							float t = specL[off + j] - specR[off + j];
							int num = off + j;
							specL[num] += specR[off + j];
							specR[off + j] = t;
						}
					}
				}
				i++;
				idx++;
			}
			groupOff += info.getWindowGroupLength(g) * 128;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105 })]
	private MS()
	{
	}
}
