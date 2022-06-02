using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.platform;

namespace org.jcodec.audio;

public class FilterSocket : Object
{
	private FloatBuffer[] buffers;

	private long[] positions;

	private int[] delays;

	private AudioFilter[] filters;

	private int totalInputs;

	private int totalOutputs;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 98, 107, 145, 107, 145, 104, 104 })]
	public virtual void setBuffers(FloatBuffer[] ins, long[] pos)
	{
		if ((nint)ins.LongLength != totalInputs)
		{
			
			throw new IllegalArgumentException("Number of input buffers provided is less then the number of filter inputs.");
		}
		if ((nint)pos.LongLength != totalInputs)
		{
			
			throw new IllegalArgumentException("Number of input buffer positions provided is less then the number of filter inputs.");
		}
		buffers = ins;
		positions = pos;
	}

	[LineNumberTable(78)]
	internal virtual FloatBuffer[] getBuffers()
	{
		return buffers;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 130, 107, 159, 39, 148, 127, 25, 119,
		18, 6, 122, 241, 69
	})]
	public virtual void filter(FloatBuffer[] outputs)
	{
		if ((nint)outputs.LongLength != totalOutputs)
		{
			string s = new StringBuilder().append("Can not output to provided filter socket inputs != outputs (").append(outputs.Length).append("!=")
				.append(totalOutputs)
				.append(")")
				.toString();
			
			throw new IllegalArgumentException(s);
		}
		int i = 0;
		int ii = 0;
		int oi = 0;
		for (; i < (nint)filters.LongLength; i++)
		{
			filters[i].filter((FloatBuffer[])Platform.copyOfRangeO(buffers, ii, filters[i].getNInputs() + ii), Platform.copyOfRangeL(positions, ii, filters[i].getNInputs() + ii), (FloatBuffer[])Platform.copyOfRangeO(outputs, oi, filters[i].getNOutputs() + oi));
			ii += filters[i].getNInputs();
			oi += filters[i].getNOutputs();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 122, 130, 109, 127, 0, 14, 199 })]
	public virtual void rotate()
	{
		for (int i = 0; i < (nint)buffers.LongLength; i++)
		{
			long[] array = positions;
			int num = i;
			long[] array2 = array;
			array2[num] += buffers[i].position();
			Audio.rotate(buffers[i]);
		}
	}

	[LineNumberTable(108)]
	internal virtual AudioFilter[] getFilters()
	{
		return filters;
	}

	[LineNumberTable(100)]
	public virtual int getTotalInputs()
	{
		return totalInputs;
	}

	[LineNumberTable(104)]
	public virtual int getTotalOutputs()
	{
		return totalOutputs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 103, 104, 136, 104, 118, 22, 231,
		69, 114, 114, 114, 106, 112, 49, 45, 231, 69,
		104
	})]
	public static FilterSocket createFilterSocket(AudioFilter[] filters)
	{
		FilterSocket fs = new FilterSocket();
		fs.totalInputs = 0;
		fs.totalOutputs = 0;
		for (int j = 0; j < (nint)filters.LongLength; j++)
		{
			fs.totalInputs += filters[j].getNInputs();
			fs.totalOutputs += filters[j].getNOutputs();
		}
		fs.buffers = new FloatBuffer[fs.totalInputs];
		fs.positions = new long[fs.totalInputs];
		fs.delays = new int[fs.totalInputs];
		int i = 0;
		int b = 0;
		for (; i < (nint)filters.LongLength; i++)
		{
			int k = 0;
			while (k < filters[i].getNInputs())
			{
				fs.delays[b] = filters[i].getDelay();
				k++;
				b++;
			}
		}
		fs.filters = filters;
		return fs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 98, 108, 122, 23, 199 })]
	public virtual void allocateBuffers(int bufferSize)
	{
		for (int i = 0; i < totalInputs; i++)
		{
			buffers[i] = FloatBuffer.allocate(bufferSize + delays[i] * 2);
			buffers[i].position(delays[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 130, 105 })]
	private FilterSocket()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 103, 113, 104, 104, 118, 109, 109 })]
	public static FilterSocket createFilterSocket2(AudioFilter filter, FloatBuffer[] buffers, long[] positions)
	{
		FilterSocket fs = new FilterSocket();
		fs.filters = new AudioFilter[1] { filter };
		fs.buffers = buffers;
		fs.positions = positions;
		fs.delays = new int[1] { filter.getDelay() };
		fs.totalInputs = filter.getNInputs();
		fs.totalOutputs = filter.getNOutputs();
		return fs;
	}

	[LineNumberTable(112)]
	public virtual long[] getPositions()
	{
		return positions;
	}
}
