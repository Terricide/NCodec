using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;

namespace org.jcodec.audio;

public class ChannelSplit : Object, AudioFilter
{
	private AudioFormat format;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104 })]
	public ChannelSplit(AudioFormat format)
	{
		this.format = format;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		136,
		130,
		102,
		145,
		112,
		223,
		27,
		133,
		115,
		107,
		108,
		159,
		20,
		byte.MaxValue,
		6,
		60,
		234,
		71,
		116,
		104,
		48,
		201
	})]
	public virtual void filter(FloatBuffer[] _in, long[] inPos, FloatBuffer[] @out)
	{
		if ((nint)_in.LongLength != 1)
		{
			
			throw new IllegalArgumentException("Channel split invoked on more then one input");
		}
		if ((nint)@out.LongLength != format.getChannels())
		{
			string s = new StringBuilder().append("Channel split must be supplied with ").append(format.getChannels()).append(" output buffers to hold the channels.")
				.toString();
			
			throw new IllegalArgumentException(s);
		}
		FloatBuffer in0 = _in[0];
		int num = in0.remaining();
		nint num2 = (nint)@out.LongLength;
		int outSampleCount = (int)((num2 != -1) ? (num / num2) : (-num));
		for (int j = 0; j < (nint)@out.LongLength; j++)
		{
			if (@out[j].remaining() < outSampleCount)
			{
				string s2 = new StringBuilder().append("Supplied buffer for ").append(j).append("th channel doesn't have sufficient space to put the samples ( required: ")
					.append(outSampleCount)
					.append(", actual: ")
					.append(@out[j].remaining())
					.append(")")
					.toString();
				
				throw new IllegalArgumentException(s2);
			}
		}
		while (in0.remaining() >= format.getChannels())
		{
			for (int i = 0; i < (nint)@out.LongLength; i++)
			{
				@out[i].put(in0.get());
			}
		}
	}

	[LineNumberTable(53)]
	public virtual int getDelay()
	{
		return 0;
	}

	[LineNumberTable(58)]
	public virtual int getNInputs()
	{
		return 1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(63)]
	public virtual int getNOutputs()
	{
		int channels = format.getChannels();
		
		return channels;
	}
}
