using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.api;
using org.jcodec.common.tools;

namespace org.jcodec.common;

public class AudioUtil : Object
{
	private const float f24 = 8388607f;

	private const float f16 = 32767f;

	public const float r16 = 3.05175781E-05f;

	public const float r24 = 1.1920929E-07f;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 89, 66, 105, 145, 117, 159, 12, 105, 107,
		139, 171, 107, 139
	})]
	public static int toInt(AudioFormat format, ByteBuffer buf, int[] samples)
	{
		if (!format.isSigned())
		{
			
			throw new NotSupportedException("Unsigned PCM is not supported ( yet? ).");
		}
		if (format.getSampleSizeInBits() != 16 && format.getSampleSizeInBits() != 24)
		{
			string msg = new StringBuilder().append(format.getSampleSizeInBits()).append(" bit PCM is not supported ( yet? ).").toString();
			
			throw new NotSupportedException(msg);
		}
		if (format.isBigEndian())
		{
			if (format.getSampleSizeInBits() == 16)
			{
				int result = toInt16BE(buf, samples);
				
				return result;
			}
			int result2 = toInt24BE(buf, samples);
			
			return result2;
		}
		if (format.getSampleSizeInBits() == 16)
		{
			int result3 = toInt16LE(buf, samples);
			
			return result3;
		}
		int result4 = toInt24LE(buf, samples);
		
		return result4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 105, 145, 117, 159, 12, 105, 107,
		138, 170, 107, 138, 170
	})]
	public static void toFloat(AudioFormat format, ByteBuffer buf, FloatBuffer floatBuf)
	{
		if (!format.isSigned())
		{
			
			throw new NotSupportedException("Unsigned PCM is not supported ( yet? ).");
		}
		if (format.getSampleSizeInBits() != 16 && format.getSampleSizeInBits() != 24)
		{
			string msg = new StringBuilder().append(format.getSampleSizeInBits()).append(" bit PCM is not supported ( yet? ).").toString();
			
			throw new NotSupportedException(msg);
		}
		if (format.isBigEndian())
		{
			if (format.getSampleSizeInBits() == 16)
			{
				toFloat16BE(buf, floatBuf);
			}
			else
			{
				toFloat24BE(buf, floatBuf);
			}
		}
		else if (format.getSampleSizeInBits() == 16)
		{
			toFloat16LE(buf, floatBuf);
		}
		else
		{
			toFloat24LE(buf, floatBuf);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 66, 105, 145, 117, 159, 12, 105, 107,
		138, 170, 107, 138, 170
	})]
	public static void fromFloat(FloatBuffer floatBuf, AudioFormat format, ByteBuffer buf)
	{
		if (!format.isSigned())
		{
			
			throw new NotSupportedException("Unsigned PCM is not supported ( yet? ).");
		}
		if (format.getSampleSizeInBits() != 16 && format.getSampleSizeInBits() != 24)
		{
			string msg = new StringBuilder().append(format.getSampleSizeInBits()).append(" bit PCM is not supported ( yet? ).").toString();
			
			throw new NotSupportedException(msg);
		}
		if (format.isBigEndian())
		{
			if (format.getSampleSizeInBits() == 16)
			{
				fromFloat16BE(buf, floatBuf);
			}
			else
			{
				fromFloat24BE(buf, floatBuf);
			}
		}
		else if (format.getSampleSizeInBits() == 16)
		{
			fromFloat16LE(buf, floatBuf);
		}
		else
		{
			fromFloat24LE(buf, floatBuf);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 66, 105, 145, 117, 159, 12, 105, 107,
		140, 172, 107, 140
	})]
	public static int fromInt(int[] data, int len, AudioFormat format, ByteBuffer buf)
	{
		if (!format.isSigned())
		{
			
			throw new NotSupportedException("Unsigned PCM is not supported ( yet? ).");
		}
		if (format.getSampleSizeInBits() != 16 && format.getSampleSizeInBits() != 24)
		{
			string msg = new StringBuilder().append(format.getSampleSizeInBits()).append(" bit PCM is not supported ( yet? ).").toString();
			
			throw new NotSupportedException(msg);
		}
		if (format.isBigEndian())
		{
			if (format.getSampleSizeInBits() == 16)
			{
				int result = fromInt16BE(buf, data, len);
				
				return result;
			}
			int result2 = fromInt24BE(buf, data, len);
			
			return result2;
		}
		if (format.getSampleSizeInBits() == 16)
		{
			int result3 = fromInt16LE(buf, data, len);
			
			return result3;
		}
		int result4 = fromInt24LE(buf, data, len);
		
		return result4;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 66, 114, 159, 17 })]
	private static void toFloat16BE(ByteBuffer buf, FloatBuffer @out)
	{
		while (buf.remaining() >= 2 && @out.hasRemaining())
		{
			@out.put(3.05175781E-05f * (float)(short)((((sbyte)buf.get() & 0xFF) << 8) | ((sbyte)buf.get() & 0xFF)));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 130, 114, 159, 38 })]
	private static void toFloat24BE(ByteBuffer buf, FloatBuffer @out)
	{
		while (buf.remaining() >= 3 && @out.hasRemaining())
		{
			@out.put(1.1920929E-07f * (float)(((((sbyte)buf.get() & 0xFF) << 24) | (((sbyte)buf.get() & 0xFF) << 16) | (((sbyte)buf.get() & 0xFF) << 8)) >> 8));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 66, 114, 159, 17 })]
	private static void toFloat16LE(ByteBuffer buf, FloatBuffer @out)
	{
		while (buf.remaining() >= 2 && @out.hasRemaining())
		{
			@out.put(3.05175781E-05f * (float)(short)(((sbyte)buf.get() & 0xFF) | (((sbyte)buf.get() & 0xFF) << 8)));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 130, 114, 159, 38 })]
	private static void toFloat24LE(ByteBuffer buf, FloatBuffer @out)
	{
		while (buf.remaining() >= 3 && @out.hasRemaining())
		{
			@out.put(1.1920929E-07f * (float)(((((sbyte)buf.get() & 0xFF) << 8) | (((sbyte)buf.get() & 0xFF) << 16) | (((sbyte)buf.get() & 0xFF) << 24)) >> 8));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 107, 66, 114, 127, 10, 108, 106, 99 })]
	private static void fromFloat16BE(ByteBuffer buf, FloatBuffer _in)
	{
		while (buf.remaining() >= 2 && _in.hasRemaining())
		{
			int val = MathUtil.clip(ByteCodeHelper.f2i(_in.get() * 32767f), -32768, 32767) & 0xFFFF;
			buf.put((byte)(sbyte)(val >> 8));
			buf.put((byte)(sbyte)val);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 110, 162, 114, 127, 10, 109, 108, 106, 99 })]
	private static void fromFloat24BE(ByteBuffer buf, FloatBuffer _in)
	{
		while (buf.remaining() >= 3 && _in.hasRemaining())
		{
			int val = MathUtil.clip(ByteCodeHelper.f2i(_in.get() * 8388607f), -8388608, 8388607) & 0xFFFFFF;
			buf.put((byte)(sbyte)(val >> 16));
			buf.put((byte)(sbyte)(val >> 8));
			buf.put((byte)(sbyte)val);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 112, 162, 114, 127, 10, 106, 108, 99 })]
	private static void fromFloat16LE(ByteBuffer buf, FloatBuffer _in)
	{
		while (buf.remaining() >= 2 && _in.hasRemaining())
		{
			int val = MathUtil.clip(ByteCodeHelper.f2i(_in.get() * 32767f), -32768, 32767) & 0xFFFF;
			buf.put((byte)(sbyte)val);
			buf.put((byte)(sbyte)(val >> 8));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 130, 114, 127, 10, 106, 108, 109, 99 })]
	private static void fromFloat24LE(ByteBuffer buf, FloatBuffer _in)
	{
		while (buf.remaining() >= 3 && _in.hasRemaining())
		{
			int val = MathUtil.clip(ByteCodeHelper.f2i(_in.get() * 8388607f), -8388608, 8388607) & 0xFFFFFF;
			buf.put((byte)(sbyte)val);
			buf.put((byte)(sbyte)(val >> 8));
			buf.put((byte)(sbyte)(val >> 16));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 92, 130, 99, 110, 105, 108, 106, 99 })]
	private static int fromInt16BE(ByteBuffer buf, int[] @out, int len)
	{
		int samples = 0;
		while (buf.remaining() >= 2 && samples < len)
		{
			int num = samples;
			samples++;
			int val = @out[num];
			buf.put((byte)(sbyte)(val >> 8));
			buf.put((byte)(sbyte)val);
		}
		return samples;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 95, 162, 99, 110, 105, 109, 108, 106, 99 })]
	private static int fromInt24BE(ByteBuffer buf, int[] @out, int len)
	{
		int samples = 0;
		while (buf.remaining() >= 3 && samples < len)
		{
			int num = samples;
			samples++;
			int val = @out[num];
			buf.put((byte)(sbyte)(val >> 16));
			buf.put((byte)(sbyte)(val >> 8));
			buf.put((byte)(sbyte)val);
		}
		return samples;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 97, 98, 99, 110, 105, 106, 108, 99 })]
	private static int fromInt16LE(ByteBuffer buf, int[] @out, int len)
	{
		int samples = 0;
		while (buf.remaining() >= 2 && samples < len)
		{
			int num = samples;
			samples++;
			int val = @out[num];
			buf.put((byte)(sbyte)val);
			buf.put((byte)(sbyte)(val >> 8));
		}
		return samples;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 100, 130, 99, 110, 105, 106, 108, 109, 99 })]
	private static int fromInt24LE(ByteBuffer buf, int[] @out, int len)
	{
		int samples = 0;
		while (buf.remaining() >= 3 && samples < len)
		{
			int num = samples;
			samples++;
			int val = @out[num];
			buf.put((byte)(sbyte)val);
			buf.put((byte)(sbyte)(val >> 8));
			buf.put((byte)(sbyte)(val >> 16));
		}
		return samples;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 78, 130, 99, 111, 159, 9 })]
	private static int toInt16BE(ByteBuffer buf, int[] @out)
	{
		int samples = 0;
		while (buf.remaining() >= 2 && samples < (nint)@out.LongLength)
		{
			int num = samples;
			samples++;
			@out[num] = (short)((((sbyte)buf.get() & 0xFF) << 8) | ((sbyte)buf.get() & 0xFF));
		}
		return samples;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 80, 130, 99, 111, 159, 30 })]
	private static int toInt24BE(ByteBuffer buf, int[] @out)
	{
		int samples = 0;
		while (buf.remaining() >= 3 && samples < (nint)@out.LongLength)
		{
			int num = samples;
			samples++;
			@out[num] = ((((sbyte)buf.get() & 0xFF) << 24) | (((sbyte)buf.get() & 0xFF) << 16) | (((sbyte)buf.get() & 0xFF) << 8)) >> 8;
		}
		return samples;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 82, 130, 99, 111, 159, 9 })]
	private static int toInt16LE(ByteBuffer buf, int[] @out)
	{
		int samples = 0;
		while (buf.remaining() >= 2 && samples < (nint)@out.LongLength)
		{
			int num = samples;
			samples++;
			@out[num] = (short)(((sbyte)buf.get() & 0xFF) | (((sbyte)buf.get() & 0xFF) << 8));
		}
		return samples;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 84, 130, 99, 111, 159, 30 })]
	private static int toInt24LE(ByteBuffer buf, int[] @out)
	{
		int samples = 0;
		while (buf.remaining() >= 3 && samples < (nint)@out.LongLength)
		{
			int num = samples;
			samples++;
			@out[num] = ((((sbyte)buf.get() & 0xFF) << 8) | (((sbyte)buf.get() & 0xFF) << 16) | (((sbyte)buf.get() & 0xFF) << 24)) >> 8;
		}
		return samples;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public AudioUtil()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 74, 130, 106, 134, 99, 104, 108, 10, 199,
		120, 109, 109, 105, 41, 171, 105, 50, 233, 59,
		44, 236, 76
	})]
	public static void interleave(AudioFormat format, ByteBuffer[] ins, ByteBuffer outb)
	{
		int bytesPerSample = format.getSampleSizeInBits() >> 3;
		int bytesPerFrame = (int)(bytesPerSample * (nint)ins.LongLength);
		int max = 0;
		for (int k = 0; k < (nint)ins.LongLength; k++)
		{
			if (ins[k].remaining() > max)
			{
				max = ins[k].remaining();
			}
		}
		for (int frames = 0; frames < max; frames++)
		{
			if (outb.remaining() < bytesPerFrame)
			{
				break;
			}
			for (int l = 0; l < (nint)ins.LongLength; l++)
			{
				if (ins[l].remaining() < bytesPerSample)
				{
					for (int j = 0; j < bytesPerSample; j++)
					{
						outb.put(0);
					}
				}
				else
				{
					for (int i = 0; i < bytesPerSample; i++)
					{
						outb.put((byte)(sbyte)ins[l].get());
					}
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 66, 98, 106, 134, 106, 104, 103, 49, 39,
		233, 70
	})]
	public static void deinterleave(AudioFormat format, ByteBuffer inb, ByteBuffer[] outs)
	{
		int bytesPerSample = format.getSampleSizeInBits() >> 3;
		int bytesPerFrame = (int)(bytesPerSample * (nint)outs.LongLength);
		while (inb.remaining() >= bytesPerFrame)
		{
			for (int j = 0; j < (nint)outs.LongLength; j++)
			{
				for (int i = 0; i < bytesPerSample; i++)
				{
					outs[j].put((byte)(sbyte)inb.get());
				}
			}
		}
	}
}
