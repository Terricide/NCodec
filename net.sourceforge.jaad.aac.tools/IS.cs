using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using net.sourceforge.jaad.aac.huffman;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.tools;

[Implements(new string[] { "net.sourceforge.jaad.aac.syntax.SyntaxConstants", "net.sourceforge.jaad.aac.tools.ISScaleTable", "net.sourceforge.jaad.aac.huffman.HCB" })]
public sealed class IS : Object, SyntaxConstants, ISScaleTable, HCB
{
	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] SCALE_TABLE
	{
		[HideFromJava]
		get
		{
			return ISScaleTable.SCALE_TABLE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 104, 104, 104, 104, 105, 105, 105,
		169, 135, 108, 109, 118, 104, 103, 112, 105, 116,
		109, 115, 115, 115, 51, 9, 236, 59, 242, 78,
		104, 107, 170, 244, 41, 236, 89
	})]
	public static void process(CPE cpe, float[] specL, float[] specR)
	{
		ICStream ics = cpe.getRightChannel();
		ICSInfo info = ics.getInfo();
		int[] offsets = info.getSWBOffsets();
		int windowGroups = info.getWindowGroupCount();
		int maxSFB = info.getMaxSFB();
		int[] sfbCB = ics.getSfbCB();
		int[] sectEnd = ics.getSectEnd();
		float[] scaleFactors = ics.getScaleFactors();
		int idx = 0;
		int groupOff = 0;
		for (int g = 0; g < windowGroups; g++)
		{
			int i = 0;
			while (i < maxSFB)
			{
				if (sfbCB[idx] == 15 || sfbCB[idx] == 14)
				{
					int end = sectEnd[idx];
					while (i < end)
					{
						int c = ((sfbCB[idx] == 15) ? 1 : (-1));
						if (cpe.isMSMaskPresent())
						{
							c *= ((!cpe.isMSUsed(idx)) ? 1 : (-1));
						}
						float scale = (float)c * scaleFactors[idx];
						for (int w = 0; w < info.getWindowGroupLength(g); w++)
						{
							int off = groupOff + w * 128 + offsets[i];
							for (int j = 0; j < offsets[i + 1] - offsets[i]; j++)
							{
								specR[off + j] = specL[off + j] * scale;
							}
						}
						i++;
						idx++;
					}
				}
				else
				{
					int end2 = sectEnd[idx];
					idx += end2 - i;
					i = end2;
				}
			}
			groupOff += info.getWindowGroupLength(g) * 128;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 105 })]
	private IS()
	{
	}
}
