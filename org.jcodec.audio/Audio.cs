using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.audio;

public class Audio : Object
{
	public class DummyFilter : Object, AudioFilter
	{
		private int nInputs;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 126, 66, 105, 104 })]
		public DummyFilter(int nInputs)
		{
			this.nInputs = nInputs;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 125, 130, 107, 115, 143, 106, 121, 235, 58,
			234, 74
		})]
		public virtual void filter(FloatBuffer[] _in, long[] inPos, FloatBuffer[] @out)
		{
			for (int i = 0; i < (nint)_in.LongLength; i++)
			{
				if (@out[i].remaining() >= _in[i].remaining())
				{
					@out[i].put(_in[i]);
					continue;
				}
				FloatBuffer duplicate = _in[i].duplicate();
				duplicate.limit(_in[i].position() + @out[i].remaining());
				@out[i].put(duplicate);
			}
		}

		[LineNumberTable(84)]
		public virtual int getDelay()
		{
			return 0;
		}

		[LineNumberTable(89)]
		public virtual int getNInputs()
		{
			return nInputs;
		}

		[LineNumberTable(94)]
		public virtual int getNOutputs()
		{
			return nInputs;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 106, 113, 106, 113, 105, 145, 117,
		117, 136, 112, 106, 106, 120, 105, 106, 106, 143
	})]
	public static void filterTransfer(AudioSource src, AudioFilter filter, AudioSink sink)
	{
		if (filter.getNInputs() != 1)
		{
			
			throw new IllegalArgumentException("Audio filter has # inputs != 1");
		}
		if (filter.getNOutputs() != 1)
		{
			
			throw new IllegalArgumentException("Audio filter has # outputs != 1");
		}
		if (filter.getDelay() != 0)
		{
			
			throw new IllegalArgumentException("Audio filter has delay");
		}
		FloatBuffer[] ins = new FloatBuffer[1] { FloatBuffer.allocate(4096) };
		FloatBuffer[] outs = new FloatBuffer[1] { FloatBuffer.allocate(8192) };
		long[] pos = new long[1];
		while (src.readFloat(ins[0]) != -1)
		{
			ins[0].flip();
			filter.filter(ins, pos, outs);
			int num = 0;
			long[] array = pos;
			array[num] += ins[0].position();
			rotate(ins[0]);
			outs[0].flip();
			sink.writeFloat(outs[0]);
			outs[0].clear();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 130, 107, 47, 135, 105, 110 })]
	public static void rotate(FloatBuffer buf)
	{
		int pos = 0;
		while (buf.hasRemaining())
		{
			buf.put(pos, buf.get());
			pos++;
		}
		buf.position(pos);
		buf.limit(buf.capacity());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public Audio()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 138, 162, 112 })]
	public static void transfer(AudioSource src, AudioSink sink)
	{
		filterTransfer(src, new DummyFilter(1), sink);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 104, 105, 127, 12, 109 })]
	public static void print(FloatBuffer buf)
	{
		FloatBuffer dup = buf.duplicate();
		while (dup.hasRemaining())
		{
			java.lang.System.@out.print(String.format("%.3f,", Float.valueOf(dup.get())));
		}
		java.lang.System.@out.println();
	}
}
