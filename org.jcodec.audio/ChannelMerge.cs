using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;

namespace org.jcodec.audio;

public class ChannelMerge : Object, AudioFilter
{
	private AudioFormat format;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 104 })]
	public ChannelMerge(AudioFormat format)
	{
		this.format = format;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 162, 112, 223, 27, 102, 177, 133, 103,
		104, 108, 10, 199, 104, 49, 199, 109, 191, 53,
		105, 106, 49, 41, 201
	})]
	public virtual void filter(FloatBuffer[] _in, long[] inPos, FloatBuffer[] @out)
	{
		if ((nint)_in.LongLength != format.getChannels())
		{
			string s = new StringBuilder().append("Channel merge must be supplied with ").append(format.getChannels()).append(" input buffers to hold the channels.")
				.toString();
			
			throw new IllegalArgumentException(s);
		}
		if ((nint)@out.LongLength != 1)
		{
			
			throw new IllegalArgumentException("Channel merget invoked on more then one output");
		}
		FloatBuffer out2 = @out[0];
		int min = int.MaxValue;
		for (int k = 0; k < (nint)_in.LongLength; k++)
		{
			if (_in[k].remaining() < min)
			{
				min = _in[k].remaining();
			}
		}
		for (int j = 0; j < (nint)_in.LongLength; j++)
		{
			Preconditions.checkState(_in[j].remaining() == min);
		}
		if (out2.remaining() < min * (nint)_in.LongLength)
		{
			string s2 = new StringBuilder().append("Supplied output buffer is not big enough to hold ").append(min).append(" * ")
				.append(_in.Length)
				.append(" = ")
				.append((int)(min * (nint)_in.LongLength))
				.append(" output samples.")
				.toString();
			
			throw new IllegalArgumentException(s2);
		}
		for (int i = 0; i < min; i++)
		{
			for (int l = 0; l < (nint)_in.LongLength; l++)
			{
				out2.put(_in[l].get());
			}
		}
	}

	[LineNumberTable(59)]
	public virtual int getDelay()
	{
		return 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(64)]
	public virtual int getNInputs()
	{
		int channels = format.getChannels();
		
		return channels;
	}

	[LineNumberTable(69)]
	public virtual int getNOutputs()
	{
		return 1;
	}
}
