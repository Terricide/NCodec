using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;

namespace org.jcodec.audio;

public class FilterGraph : Object, AudioFilter
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : Object
	{
		_1()
		{
			throw null;
		}
	}

	public class Factory : Object
	{
		[Signature("Ljava/util/List<Lorg/jcodec/audio/FilterSocket;>;")]
		private List sockets;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 130, 105, 140, 169, 127, 7, 148, 124 })]
		protected internal Factory(AudioFilter firstFilter)
		{
			sockets = new ArrayList();
			if (firstFilter.getDelay() != 0)
			{
				sockets.add(FilterSocket.createFilterSocket(new Audio.DummyFilter[1]
				{
					new Audio.DummyFilter(firstFilter.getNInputs())
				}));
				addLevel(new AudioFilter[1] { firstFilter });
			}
			else
			{
				sockets.add(FilterSocket.createFilterSocket(new AudioFilter[1] { firstFilter }));
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 127, 98, 104, 108, 110 })]
		public virtual Factory addLevel(AudioFilter[] filters)
		{
			FilterSocket socket = FilterSocket.createFilterSocket(filters);
			socket.allocateBuffers(4096);
			sockets.add(socket);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 124, 162, 104, 104 })]
		public virtual Factory addLevels(AudioFilter filter, int n)
		{
			AudioFilter[] filters = new AudioFilter[n];
			Arrays.fill(filters, filter);
			Factory result = addLevel(filters);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 120, 98, 127, 5, 116, 127, 2, 155 })]
		public virtual Factory addLevelSpan(AudioFilter filter)
		{
			int prevLevelOuts = ((FilterSocket)sockets.get(sockets.size() - 1)).getTotalOutputs();
			int nInputs = filter.getNInputs();
			if (nInputs != -1 && prevLevelOuts % nInputs != 0)
			{
				string s = new StringBuilder().append("Can't fill ").append(prevLevelOuts).append(" with multiple of ")
					.append(filter.getNInputs())
					.toString();
				
				throw new IllegalArgumentException(s);
			}
			int nInputs2 = filter.getNInputs();
			Factory result = addLevels(filter, (nInputs2 != -1) ? (prevLevelOuts / nInputs2) : (-prevLevelOuts));
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(98)]
		public virtual FilterGraph create()
		{
			FilterGraph result = new FilterGraph((FilterSocket[])sockets.toArray(new FilterSocket[0]), null);
			
			return result;
		}
	}

	private FilterSocket[] sockets;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 66, 105, 104 })]
	private FilterGraph(FilterSocket[] sockets)
	{
		this.sockets = sockets;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public static Factory addLevel(AudioFilter first)
	{
		Factory result = new Factory(first);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 115, 130, 112, 112, 159, 1, 143, 101, 174,
		109, 116, 41, 233, 54, 234, 78
	})]
	public virtual void filter(FloatBuffer[] ins, long[] pos, FloatBuffer[] outs)
	{
		sockets[0].setBuffers(ins, pos);
		for (int i = 0; i < (nint)sockets.LongLength; i++)
		{
			FloatBuffer[] curOut = ((i >= (nint)sockets.LongLength - 1) ? outs : sockets[i + 1].getBuffers());
			sockets[i].filter(curOut);
			if (i > 0)
			{
				sockets[i].rotate();
			}
			if (i < (nint)sockets.LongLength - 1)
			{
				FloatBuffer[] array = curOut;
				int num = array.Length;
				for (int j = 0; j < num; j++)
				{
					FloatBuffer b = array[j];
					b.flip();
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(129)]
	public virtual int getDelay()
	{
		int delay = sockets[0].getFilters()[0].getDelay();
		
		return delay;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(134)]
	public virtual int getNInputs()
	{
		int totalInputs = sockets[0].getTotalInputs();
		
		return totalInputs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(139)]
	public virtual int getNOutputs()
	{
		int totalOutputs = sockets[(nint)sockets.LongLength - 1].getTotalOutputs();
		
		return totalOutputs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Synthetic)]
	[LineNumberTable(21)]
	internal FilterGraph(FilterSocket[] x0, _1 x1)
		: this(x0)
	{
	}
}
