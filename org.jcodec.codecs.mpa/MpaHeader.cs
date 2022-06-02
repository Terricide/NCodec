using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.codecs.mpa;

internal class MpaHeader : Object
{
	internal int layer;

	internal int protectionBit;

	internal int bitrateIndex;

	internal int paddingBit;

	internal int modeExtension;

	internal int version;

	internal int mode;

	internal int sampleFreq;

	internal int numSubbands;

	internal int intensityStereoBound;

	internal bool copyright;

	internal bool original;

	internal int framesize;

	internal int frameBytes;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 162, 167, 104, 109, 105, 105, 138, 113,
		117, 145, 111, 109, 110, 109, 108, 108, 106, 147,
		104, 105, 104, 105, 104, 106, 142, 105, 106, 102,
		134, 104, 107, 106, 139, 106, 116, 139, 137, 111,
		109, 135
	})]
	internal static MpaHeader read_header(ByteBuffer bb)
	{
		MpaHeader ret = new MpaHeader();
		int headerstring = bb.getInt();
		ret.version = (int)(((uint)headerstring >> 19) & 1);
		if ((((uint)headerstring >> 20) & 1) == 0)
		{
			if (ret.version != 0)
			{
				
				throw new RuntimeException("UNKNOWN_ERROR");
			}
			ret.version = 2;
		}
		int num = (int)(((uint)headerstring >> 10) & 3);
		MpaHeader mpaHeader = ret;
		mpaHeader.sampleFreq = num;
		if (num == 3)
		{
			
			throw new RuntimeException("UNKNOWN_ERROR");
		}
		ret.layer = (int)((4 - ((uint)headerstring >> 17)) & 3);
		ret.protectionBit = (int)(((uint)headerstring >> 16) & 1);
		ret.bitrateIndex = (int)(((uint)headerstring >> 12) & 0xF);
		ret.paddingBit = (int)(((uint)headerstring >> 9) & 1);
		ret.mode = (int)(((uint)headerstring >> 6) & 3);
		ret.modeExtension = (int)(((uint)headerstring >> 4) & 3);
		if (ret.mode == 1)
		{
			ret.intensityStereoBound = (ret.modeExtension << 2) + 4;
		}
		else
		{
			ret.intensityStereoBound = 0;
		}
		if ((((uint)headerstring >> 3) & 1) == 1)
		{
			ret.copyright = true;
		}
		if ((((uint)headerstring >> 2) & 1) == 1)
		{
			ret.original = true;
		}
		if (ret.layer == 1)
		{
			ret.numSubbands = 32;
		}
		else
		{
			int channel_bitrate = ret.bitrateIndex;
			if (ret.mode != 3)
			{
				channel_bitrate = ((channel_bitrate == 4) ? 1 : (channel_bitrate + -4));
			}
			if (channel_bitrate == 1 || channel_bitrate == 2)
			{
				if (ret.sampleFreq == 2)
				{
					ret.numSubbands = 12;
				}
				else
				{
					ret.numSubbands = 8;
				}
			}
			else if (ret.sampleFreq == 1 || (channel_bitrate >= 3 && channel_bitrate <= 5))
			{
				ret.numSubbands = 27;
			}
			else
			{
				ret.numSubbands = 30;
			}
		}
		if (ret.intensityStereoBound > ret.numSubbands)
		{
			ret.intensityStereoBound = ret.numSubbands;
		}
		calculateFramesize(ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 105 })]
	internal MpaHeader()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 118, 98, 106, 159, 28, 105, 111, 111, 141,
		159, 38, 114, 111, 105, 111, 109, 106, 191, 15,
		223, 15, 168, 111
	})]
	public static void calculateFramesize(MpaHeader ret)
	{
		if (ret.layer == 1)
		{
			int num = 12 * MpaConst.bitrates[ret.version][0][ret.bitrateIndex];
			int num2 = MpaConst.frequencies[ret.version][ret.sampleFreq];
			ret.framesize = ((num2 != -1) ? (num / num2) : (-num));
			if (ret.paddingBit != 0)
			{
				ret.framesize++;
			}
			ret.framesize <<= 2;
			ret.frameBytes = 0;
		}
		else
		{
			int num3 = 144 * MpaConst.bitrates[ret.version][ret.layer - 1][ret.bitrateIndex];
			int num4 = MpaConst.frequencies[ret.version][ret.sampleFreq];
			ret.framesize = ((num4 != -1) ? (num3 / num4) : (-num3));
			if (ret.version == 0 || ret.version == 2)
			{
				ret.framesize >>= 1;
			}
			if (ret.paddingBit != 0)
			{
				ret.framesize++;
			}
			if (ret.layer == 3)
			{
				if (ret.version == 1)
				{
					ret.frameBytes = ret.framesize - ((ret.mode != 3) ? 32 : 17) - ((ret.protectionBit == 0) ? 2 : 0) - 4;
				}
				else
				{
					ret.frameBytes = ret.framesize - ((ret.mode != 3) ? 17 : 9) - ((ret.protectionBit == 0) ? 2 : 0) - 4;
				}
			}
			else
			{
				ret.frameBytes = 0;
			}
		}
		ret.framesize -= 4;
	}
}
