using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using net.sourceforge.jaad.aac.error;
using net.sourceforge.jaad.aac.gain;
using net.sourceforge.jaad.aac.huffman;
using net.sourceforge.jaad.aac.tools;
using org.jcodec.common.logging;

namespace net.sourceforge.jaad.aac.syntax;

[Implements(new string[] { "net.sourceforge.jaad.aac.syntax.SyntaxConstants", "net.sourceforge.jaad.aac.huffman.HCB", "net.sourceforge.jaad.aac.syntax.ScaleFactorTable", "net.sourceforge.jaad.aac.syntax.IQTable" })]
public class ICStream : Object, SyntaxConstants, HCB, ScaleFactorTable, IQTable
{
	private const int SF_DELTA = 60;

	private const int SF_OFFSET = 200;

	private static int randomState;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int frameLength;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private ICSInfo info;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int[] sfbCB;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int[] sectEnd;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[] data;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private float[] scaleFactors;

	private int globalGain;

	private bool pulseDataPresent;

	private bool tnsDataPresent;

	private bool gainControlPresent;

	private TNS tns;

	private GainControl gainControl;

	private int[] pulseOffset;

	private int[] pulseAmp;

	private int pulseCount;

	private int pulseStartSWB;

	private bool noiseUsed;

	private int reorderedSpectralDataLen;

	private int longestCodewordLen;

	private RVLC rvlc;

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] SCALEFACTOR_TABLE
	{
		[HideFromJava]
		get
		{
			return ScaleFactorTable.SCALEFACTOR_TABLE;
		}
	}

	[HideFromJava(HideFromJavaFlags.Reflection | HideFromJavaFlags.StackWalk | HideFromJavaFlags.StackTrace)]
	public static float[] IQ_TABLE
	{
		[HideFromJava]
		get
		{
			return IQTable.IQ_TABLE;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(274)]
	public virtual ICSInfo getInfo()
	{
		return info;
	}

	[LineNumberTable(310)]
	public virtual int getReorderedSpectralDataLength()
	{
		return reorderedSpectralDataLen;
	}

	[LineNumberTable(306)]
	public virtual int getLongestCodewordLength()
	{
		return longestCodewordLen;
	}

	[LineNumberTable(298)]
	public virtual int getGlobalGain()
	{
		return globalGain;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 66, 105, 104, 109, 110, 110, 109, 110 })]
	public ICStream(int frameLength)
	{
		this.frameLength = frameLength;
		info = new ICSInfo(frameLength);
		sfbCB = new int[120];
		sectEnd = new int[120];
		data = new float[frameLength];
		scaleFactors = new float[120];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 128, 161, 67, 124, 141, 142, 146, 174, 136,
		109, 105, 126, 107, 168, 109, 108, 116, 179, 109,
		105, 122, 107, 248, 70, 105, 123, 117, 149, 99,
		106
	})]
	public virtual void decode(IBitStream _in, bool commonWindow, AACDecoderConfig conf)
	{
		if (conf.isScalefactorResilienceUsed() && rvlc == null)
		{
			rvlc = new RVLC();
		}
		int er = (conf.getProfile().isErrorResilientProfile() ? 1 : 0);
		globalGain = _in.readBits(8);
		if (!commonWindow)
		{
			info.decode(_in, conf, commonWindow);
		}
		decodeSectionData(_in, conf.isSectionDataResilienceUsed());
		decodeScaleFactors(_in);
		pulseDataPresent = _in.readBool();
		if (pulseDataPresent)
		{
			if (info.isEightShortFrame())
			{
				
				throw new AACException("pulse data not allowed for short frames");
			}
			Logger.debug("PULSE");
			decodePulseData(_in);
		}
		tnsDataPresent = _in.readBool();
		if (tnsDataPresent && er == 0)
		{
			if (tns == null)
			{
				tns = new TNS();
			}
			tns.decode(_in, info);
		}
		gainControlPresent = _in.readBool();
		if (gainControlPresent)
		{
			if (gainControl == null)
			{
				gainControl = new GainControl(frameLength);
			}
			Logger.debug("GAIN");
			gainControl.decode(_in, info.getWindowSequence());
		}
		if (conf.isSpectralDataResilienceUsed())
		{
			int max = ((conf.getChannelConfiguration() != ChannelConfiguration.___003C_003ECHANNEL_CONFIG_STEREO) ? 12288 : 6144);
			reorderedSpectralDataLen = Math.max(_in.readBits(14), max);
			longestCodewordLen = Math.max(_in.readBits(6), 49);
		}
		else
		{
			decodeSpectralData(_in);
		}
	}

	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(270)]
	public virtual float[] getInvQuantData()
	{
		return data;
	}

	[LineNumberTable(290)]
	public virtual bool isTNSDataPresent()
	{
		return tnsDataPresent;
	}

	[LineNumberTable(294)]
	public virtual TNS getTNS()
	{
		return tns;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 116, 66, 109, 109, 115, 138, 109, 173, 132,
		108, 100, 105, 101, 106, 119, 110, 138, 104, 127,
		29, 106, 108, 18, 233, 53, 236, 81
	})]
	public virtual void decodeSectionData(IBitStream _in, bool sectionDataResilienceUsed)
	{
		Arrays.fill(sfbCB, 0);
		Arrays.fill(sectEnd, 0);
		int bits = ((!info.isEightShortFrame()) ? 5 : 3);
		int escVal = (1 << bits) - 1;
		int windowGroupCount = info.getWindowGroupCount();
		int maxSFB = info.getMaxSFB();
		int idx = 0;
		for (int g = 0; g < windowGroupCount; g++)
		{
			int i = 0;
			while (i < maxSFB)
			{
				int end = i;
				int cb = _in.readBits(4);
				if (cb == 12)
				{
					
					throw new AACException("invalid huffman codebook: 12");
				}
				int incr;
				while ((incr = _in.readBits(bits)) == escVal)
				{
					end += incr;
				}
				end += incr;
				if (end > maxSFB)
				{
					string message = new StringBuilder().append("too many bands: ").append(end).append(", allowed: ")
						.append(maxSFB)
						.toString();
					
					throw new AACException(message);
				}
				for (; i < end; i++)
				{
					sfbCB[idx] = cb;
					int[] array = sectEnd;
					int num = idx;
					idx++;
					array[num] = end;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 103, 66, 109, 141, 191, 2, 131, 100, 108,
		108, 108, 159, 19, 106, 47, 239, 70, 106, 123,
		119, 249, 61, 242, 71, 106, 100, 127, 1, 133,
		123, 119, 249, 57, 242, 75, 106, 123, 127, 19,
		252, 61, 242, 71, 230, 28, 236, 102
	})]
	public virtual void decodeScaleFactors(IBitStream _in)
	{
		int windowGroups = info.getWindowGroupCount();
		int maxSFB = info.getMaxSFB();
		int[] offset = new int[3]
		{
			globalGain,
			globalGain - 90,
			0
		};
		int noiseFlag = 1;
		int idx = 0;
		for (int g = 0; g < windowGroups; g++)
		{
			int sfb = 0;
			while (sfb < maxSFB)
			{
				int end = sectEnd[idx];
				int num = sfbCB[idx];
				if (num != 0)
				{
					if (num != 13)
					{
						if (num == 14 || num == 15)
						{
							while (sfb < end)
							{
								int num2 = 2;
								int[] array = offset;
								array[num2] += Huffman.decodeScaleFactor(_in) - 60;
								int tmp = Math.min(Math.max(offset[2], -155), 100);
								scaleFactors[idx] = ScaleFactorTable.SCALEFACTOR_TABLE[-tmp + 200];
								sfb++;
								idx++;
							}
							continue;
						}
						while (sfb < end)
						{
							int num2 = 0;
							int[] array = offset;
							array[num2] += Huffman.decodeScaleFactor(_in) - 60;
							if (offset[0] > 255)
							{
								string message = new StringBuilder().append("scalefactor out of range: ").append(offset[0]).toString();
								
								throw new AACException(message);
							}
							scaleFactors[idx] = ScaleFactorTable.SCALEFACTOR_TABLE[offset[0] - 100 + 200];
							sfb++;
							idx++;
						}
						continue;
					}
					while (sfb < end)
					{
						if (noiseFlag != 0)
						{
							int num2 = 1;
							int[] array = offset;
							array[num2] += _in.readBits(9) - 256;
							noiseFlag = 0;
						}
						else
						{
							int num2 = 1;
							int[] array = offset;
							array[num2] += Huffman.decodeScaleFactor(_in) - 60;
						}
						int tmp2 = Math.min(Math.max(offset[1], -100), 155);
						scaleFactors[idx] = 0f - ScaleFactorTable.SCALEFACTOR_TABLE[tmp2 + 200];
						sfb++;
						idx++;
					}
				}
				else
				{
					while (sfb < end)
					{
						scaleFactors[idx] = 0f;
						sfb++;
						idx++;
					}
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 109, 162, 112, 110, 159, 57, 152, 114, 178,
		123, 120, 112, 111, 123, 127, 29, 240, 61, 234,
		69
	})]
	private void decodePulseData(IBitStream _in)
	{
		pulseCount = _in.readBits(2) + 1;
		pulseStartSWB = _in.readBits(6);
		if (pulseStartSWB >= info.getSWBCount())
		{
			string message = new StringBuilder().append("pulse SWB out of range: ").append(pulseStartSWB).append(" > ")
				.append(info.getSWBCount())
				.toString();
			
			throw new AACException(message);
		}
		if (pulseOffset == null || (nint)pulseCount != (nint)pulseOffset.LongLength)
		{
			pulseOffset = new int[pulseCount];
			pulseAmp = new int[pulseCount];
		}
		pulseOffset[0] = info.getSWBOffsets()[pulseStartSWB];
		int[] array = pulseOffset;
		int num = 0;
		int[] array2 = array;
		array2[num] += _in.readBits(5);
		pulseAmp[0] = _in.readBits(4);
		for (int i = 1; i < pulseCount; i++)
		{
			pulseOffset[i] = _in.readBits(5) + pulseOffset[i - 1];
			if (pulseOffset[i] > 1023)
			{
				string message2 = new StringBuilder().append("pulse offset out of range: ").append(pulseOffset[0]).toString();
				
				throw new AACException(message2);
			}
			pulseAmp[i] = _in.readBits(4);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159,
		91,
		130,
		113,
		109,
		109,
		109,
		168,
		103,
		108,
		144,
		108,
		108,
		106,
		110,
		113,
		109,
		56,
		211,
		138,
		109,
		136,
		106,
		113,
		115,
		byte.MaxValue,
		2,
		61,
		233,
		70,
		119,
		106,
		62,
		233,
		54,
		246,
		80,
		109,
		108,
		109,
		171,
		109,
		127,
		16,
		31,
		9,
		236,
		60,
		13,
		246,
		37,
		242,
		105,
		234,
		20,
		236,
		110
	})]
	private void decodeSpectralData(IBitStream _in)
	{
		Arrays.fill(data, 0f);
		int maxSFB = info.getMaxSFB();
		int windowGroups = info.getWindowGroupCount();
		int[] offsets = info.getSWBOffsets();
		int[] buf = new int[4];
		int groupOff = 0;
		int idx = 0;
		for (int g = 0; g < windowGroups; g++)
		{
			int groupLen = info.getWindowGroupLength(g);
			int sfb = 0;
			while (sfb < maxSFB)
			{
				int hcb = sfbCB[idx];
				int off = groupOff + offsets[sfb];
				int width = offsets[sfb + 1] - offsets[sfb];
				switch (hcb)
				{
				case 0:
				case 14:
				case 15:
				{
					int w = 0;
					while (w < groupLen)
					{
						Arrays.fill(data, off, off + width, 0f);
						w++;
						off += 128;
					}
					break;
				}
				case 13:
				{
					int w3 = 0;
					while (w3 < groupLen)
					{
						float energy = 0f;
						for (int k = 0; k < width; k++)
						{
							randomState *= 1015568748;
							data[off + k] = randomState;
							energy += data[off + k] * data[off + k];
						}
						float scale = (float)((double)scaleFactors[idx] / Math.sqrt(energy));
						for (int k = 0; k < width; k++)
						{
							float[] array3 = data;
							int num2 = off + k;
							float[] array2 = array3;
							array2[num2] *= scale;
						}
						w3++;
						off += 128;
					}
					break;
				}
				default:
				{
					int w2 = 0;
					while (w2 < groupLen)
					{
						int num = ((hcb < 5) ? 4 : 2);
						for (int j = 0; j < width; j += num)
						{
							Huffman.decodeSpectralData(_in, hcb, buf, 0);
							for (int i = 0; i < num; i++)
							{
								data[off + j + i] = ((buf[i] <= 0) ? (0f - IQTable.IQ_TABLE[-buf[i]]) : IQTable.IQ_TABLE[buf[i]]);
								float[] array = data;
								int num2 = off + j + i;
								float[] array2 = array;
								array2[num2] *= scaleFactors[idx];
							}
						}
						w2++;
						off += 128;
					}
					break;
				}
				}
				sfb++;
				idx++;
			}
			groupOff += groupLen << 7;
		}
	}

	[LineNumberTable(278)]
	public virtual int[] getSectEnd()
	{
		return sectEnd;
	}

	[LineNumberTable(282)]
	public virtual int[] getSfbCB()
	{
		return sfbCB;
	}

	[LineNumberTable(286)]
	public virtual float[] getScaleFactors()
	{
		return scaleFactors;
	}

	[LineNumberTable(302)]
	public virtual bool isNoiseUsed()
	{
		return noiseUsed;
	}

	[LineNumberTable(314)]
	public virtual bool isGainControlPresent()
	{
		return gainControlPresent;
	}

	[LineNumberTable(318)]
	public virtual GainControl getGainControl()
	{
		return gainControl;
	}

	[LineNumberTable(27)]
	static ICStream()
	{
		randomState = 523124044;
	}
}
