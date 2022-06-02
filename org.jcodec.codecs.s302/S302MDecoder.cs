using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.s302;

public class S302MDecoder : Object, AudioDecoder
{
	public static int SAMPLE_RATE;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(22)]
	public S302MDecoder()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 109, 136, 104, 108, 106, 113, 108,
		141, 106, 109, 141, 118, 118, 118, 114, 117, 117,
		149, 106, 106, 106, 114, 114, 114, 134, 104, 127,
		11, 106, 109, 109, 117, 117, 117, 114, 114, 141,
		117, 117, 117, 114, 114, 141, 102, 104, 159, 11,
		109, 109, 118, 118, 117, 117, 149, 106, 106, 114,
		114, 134, 104
	})]
	public virtual AudioBuffer decodeFrame(ByteBuffer frame, ByteBuffer dst)
	{
		frame.order(ByteOrder.BIG_ENDIAN);
		ByteBuffer dup = dst.duplicate();
		int h = frame.getInt();
		int frameSize = (h >> 16) & 0xFFFF;
		if (frame.remaining() != frameSize)
		{
			
			throw new IllegalArgumentException("Wrong s302m frame");
		}
		int channels = ((h >> 14) & 3) * 2 + 2;
		switch (((h >> 4) & 3) * 4 + 16)
		{
		case 24:
		{
			int nSamples3 = frame.remaining() / 7 * 2;
			while (frame.remaining() > 6)
			{
				int c3 = (sbyte)MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int b3 = (sbyte)MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int a3 = (sbyte)MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int g = MathUtil.reverse((sbyte)frame.get() & 0xF);
				int f = MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int e = MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int d = MathUtil.reverse((sbyte)frame.get() & 0xF0);
				dup.put((byte)a3);
				dup.put((byte)b3);
				dup.put((byte)c3);
				dup.put((byte)(sbyte)((d << 4) | (e >> 4)));
				dup.put((byte)(sbyte)((e << 4) | (f >> 4)));
				dup.put((byte)(sbyte)((f << 4) | (g >> 4)));
			}
			dup.flip();
			AudioFormat.___003Cclinit_003E();
			AudioBuffer result3 = new AudioBuffer(dup, new AudioFormat(SAMPLE_RATE, 24, channels, signed: true, bigEndian: true), (channels != -1) ? (nSamples3 / channels) : (-nSamples3));
			
			return result3;
		}
		case 20:
		{
			int nSamples2 = frame.remaining() / 6 * 2;
			while (frame.remaining() > 5)
			{
				int c2 = MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int b2 = MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int a2 = MathUtil.reverse((sbyte)frame.get() & 0xF0);
				dup.put((byte)(sbyte)((a2 << 4) | (b2 >> 4)));
				dup.put((byte)(sbyte)((b2 << 4) | (c2 >> 4)));
				dup.put((byte)(sbyte)(c2 << 4));
				int cc = MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int bb2 = MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int aa2 = MathUtil.reverse((sbyte)frame.get() & 0xF0);
				dup.put((byte)(sbyte)((aa2 << 4) | (bb2 >> 4)));
				dup.put((byte)(sbyte)((bb2 << 4) | (cc >> 4)));
				dup.put((byte)(sbyte)(cc << 4));
			}
			dup.flip();
			AudioFormat.___003Cclinit_003E();
			AudioBuffer result2 = new AudioBuffer(dup, new AudioFormat(SAMPLE_RATE, 24, channels, signed: true, bigEndian: true), (channels != -1) ? (nSamples2 / channels) : (-nSamples2));
			
			return result2;
		}
		default:
		{
			int nSamples = frame.remaining() / 5 * 2;
			while (frame.remaining() > 4)
			{
				int bb = (sbyte)MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int aa = (sbyte)MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int c = MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int b = MathUtil.reverse((sbyte)frame.get() & 0xFF);
				int a = MathUtil.reverse((sbyte)frame.get() & 0xF0);
				dst.put((byte)aa);
				dst.put((byte)bb);
				dst.put((byte)(sbyte)((a << 4) | (b >> 4)));
				dst.put((byte)(sbyte)((b << 4) | (c >> 4)));
			}
			dup.flip();
			AudioFormat.___003Cclinit_003E();
			AudioBuffer result = new AudioBuffer(dup, new AudioFormat(SAMPLE_RATE, 16, channels, signed: true, bigEndian: true), (channels != -1) ? (nSamples / channels) : (-nSamples));
			
			return result;
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 117, 66, 104, 141, 104, 108, 106, 113, 108,
		109
	})]
	public virtual AudioCodecMeta getCodecMeta(ByteBuffer _data)
	{
		ByteBuffer frame = _data.duplicate();
		frame.order(ByteOrder.BIG_ENDIAN);
		int h = frame.getInt();
		int frameSize = (h >> 16) & 0xFFFF;
		if (frame.remaining() != frameSize)
		{
			
			throw new IllegalArgumentException("Wrong s302m frame");
		}
		int channels = ((h >> 14) & 3) * 2 + 2;
		int sampleSizeInBits = ((h >> 4) & 3) * 4 + 16;
		AudioFormat.___003Cclinit_003E();
		AudioCodecMeta result = AudioCodecMeta.fromAudioFormat(new AudioFormat(SAMPLE_RATE, sampleSizeInBits, channels, signed: true, bigEndian: true));
		
		return result;
	}

	[LineNumberTable(23)]
	static S302MDecoder()
	{
		SAMPLE_RATE = 48000;
	}
}
