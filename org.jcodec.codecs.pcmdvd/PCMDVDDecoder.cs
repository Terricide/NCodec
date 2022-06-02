using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.codecs.pcmdvd;

public class PCMDVDDecoder : Object, AudioDecoder
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static int[] lpcm_freq_tab;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(21)]
	public PCMDVDDecoder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 104, 136, 109, 141, 111, 137, 145,
		111, 111, 145, 145, 177, 143, 144, 112, 105, 107,
		105, 110, 153, 156, 108, 106, 105, 106, 105, 234,
		60, 233, 70, 234, 57, 236, 73, 134, 111, 106,
		105, 106, 105, 234, 60, 233, 70, 236, 57, 236,
		75, 136
	})]
	public virtual AudioBuffer decodeFrame(ByteBuffer _frame, ByteBuffer _dst)
	{
		ByteBuffer dst = _dst.duplicate();
		ByteBuffer frame = _frame.duplicate();
		frame.order(ByteOrder.BIG_ENDIAN);
		dst.order(ByteOrder.LITTLE_ENDIAN);
		int dvdaudioSubstreamType = (sbyte)frame.get() & 0xFF;
		NIOUtils.skip(frame, 3);
		if ((dvdaudioSubstreamType & 0xE0) != 160)
		{
			if ((dvdaudioSubstreamType & 0xE0) == 128)
			{
				if ((dvdaudioSubstreamType & 0xF8) == 136)
				{
					
					throw new RuntimeException("CODEC_ID_DTS");
				}
				
				throw new RuntimeException("CODEC_ID_AC3");
			}
			
			throw new RuntimeException("MPEG DVD unknown coded");
		}
		int b0 = (sbyte)frame.get() & 0xFF;
		int b1 = (sbyte)frame.get() & 0xFF;
		int b2 = (sbyte)frame.get() & 0xFF;
		int freq = (b1 >> 4) & 3;
		int sampleRate = lpcm_freq_tab[freq];
		int channelCount = 1 + (b1 & 7);
		int sampleSizeInBits = 16 + ((b1 >> 6) & 3) * 4;
		int num = frame.remaining();
		int num2 = channelCount * (sampleSizeInBits >> 3);
		int nFrames = ((num2 != -1) ? (num / num2) : (-num));
		switch (sampleSizeInBits)
		{
		case 20:
		{
			for (int j = 0; j < nFrames >> 1; j++)
			{
				for (int c2 = 0; c2 < channelCount; c2++)
				{
					int s = frame.getShort();
					dst.putShort((short)s);
					int s3 = frame.getShort();
					dst.putShort((short)s3);
				}
				NIOUtils.skip(frame, channelCount);
			}
			break;
		}
		case 24:
		{
			for (int i = 0; i < nFrames >> 1; i++)
			{
				for (int c = 0; c < channelCount; c++)
				{
					int s0 = frame.getShort();
					dst.putShort((short)s0);
					int s2 = frame.getShort();
					dst.putShort((short)s2);
				}
				NIOUtils.skip(frame, channelCount << 1);
			}
			break;
		}
		}
		dst.flip();
		AudioBuffer result = new AudioBuffer(dst, new AudioFormat(sampleRate, sampleSizeInBits, channelCount, signed: true, bigEndian: false), nFrames);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 66, 136, 141, 111, 137, 145, 111, 111,
		145, 145, 177, 143, 143, 112, 104, 107, 104, 109
	})]
	public virtual AudioCodecMeta getCodecMeta(ByteBuffer _frame)
	{
		ByteBuffer frame = _frame.duplicate();
		frame.order(ByteOrder.BIG_ENDIAN);
		int dvdaudioSubstreamType = (sbyte)frame.get() & 0xFF;
		NIOUtils.skip(frame, 3);
		if ((dvdaudioSubstreamType & 0xE0) != 160)
		{
			if ((dvdaudioSubstreamType & 0xE0) == 128)
			{
				if ((dvdaudioSubstreamType & 0xF8) == 136)
				{
					
					throw new RuntimeException("CODEC_ID_DTS");
				}
				
				throw new RuntimeException("CODEC_ID_AC3");
			}
			
			throw new RuntimeException("MPEG DVD unknown coded");
		}
		int b0 = (sbyte)frame.get() & 0xFF;
		int b1 = (sbyte)frame.get() & 0xFF;
		int b2 = (sbyte)frame.get() & 0xFF;
		int freq = (b1 >> 4) & 3;
		int sampleRate = lpcm_freq_tab[freq];
		int channelCount = 1 + (b1 & 7);
		int sampleSizeInBits = 16 + ((b1 >> 6) & 3) * 4;
		AudioCodecMeta result = AudioCodecMeta.fromAudioFormat(new AudioFormat(sampleRate, sampleSizeInBits, channelCount, signed: true, bigEndian: false));
		
		return result;
	}

	[LineNumberTable(22)]
	static PCMDVDDecoder()
	{
		lpcm_freq_tab = new int[4] { 48000, 96000, 44100, 32000 };
	}
}
