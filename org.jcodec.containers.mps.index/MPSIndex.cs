using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;

namespace org.jcodec.containers.mps.index;

public class MPSIndex : Object
{
	public class MPSStreamIndex : Object
	{
		protected internal int streamId;

		protected internal int[] fsizes;

		protected internal int[] fpts;

		protected internal int[] fdur;

		protected internal int[] sync;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 136, 162, 105, 104, 104, 104, 105, 105 })]
		public MPSStreamIndex(int streamId, int[] fsizes, int[] fpts, int[] fdur, int[] sync)
		{
			this.streamId = streamId;
			this.fsizes = fsizes;
			this.fpts = fpts;
			this.fdur = fdur;
			this.sync = sync;
		}

		[LineNumberTable(36)]
		public virtual int getStreamId()
		{
			return streamId;
		}

		[LineNumberTable(40)]
		public virtual int[] getFsizes()
		{
			return fsizes;
		}

		[LineNumberTable(44)]
		public virtual int[] getFpts()
		{
			return fpts;
		}

		[LineNumberTable(48)]
		public virtual int[] getFdur()
		{
			return fdur;
		}

		[LineNumberTable(52)]
		public virtual int[] getSync()
		{
			return sync;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 128, 66, 143, 104, 104, 103, 42, 199, 105,
			106, 106, 44, 201, 105, 106, 106, 44, 201, 105,
			106, 106, 44, 169
		})]
		public static MPSStreamIndex parseIndex(ByteBuffer index)
		{
			int streamId = (sbyte)index.get() & 0xFF;
			int fCnt = index.getInt();
			int[] fsizes = new int[fCnt];
			for (int l = 0; l < fCnt; l++)
			{
				fsizes[l] = index.getInt();
			}
			int fptsCnt = index.getInt();
			int[] fpts = new int[fptsCnt];
			for (int k = 0; k < fptsCnt; k++)
			{
				fpts[k] = index.getInt();
			}
			int fdurCnt = index.getInt();
			int[] fdur = new int[fdurCnt];
			for (int j = 0; j < fdurCnt; j++)
			{
				fdur[j] = index.getInt();
			}
			int syncCount = index.getInt();
			int[] sync = new int[syncCount];
			for (int i = 0; i < syncCount; i++)
			{
				sync[i] = index.getInt();
			}
			MPSStreamIndex result = new MPSStreamIndex(streamId, fsizes, fpts, fdur, sync);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 121, 98, 143, 111, 109, 48, 167, 111, 109,
			48, 167, 111, 109, 48, 167, 111, 109, 48, 135
		})]
		public virtual void serialize(ByteBuffer index)
		{
			index.put((byte)(sbyte)streamId);
			index.putInt(fsizes.Length);
			for (int l = 0; l < (nint)fsizes.LongLength; l++)
			{
				index.putInt(fsizes[l]);
			}
			index.putInt(fpts.Length);
			for (int k = 0; k < (nint)fpts.LongLength; k++)
			{
				index.putInt(fpts[k]);
			}
			index.putInt(fdur.Length);
			for (int j = 0; j < (nint)fdur.LongLength; j++)
			{
				index.putInt(fdur[j]);
			}
			index.putInt(sync.Length);
			for (int i = 0; i < (nint)sync.LongLength; i++)
			{
				index.putInt(sync[i]);
			}
		}

		[LineNumberTable(105)]
		public virtual int estimateSize()
		{
			return (int)(((nint)fpts.LongLength << 2) + ((nint)fdur.LongLength << 2) + ((nint)sync.LongLength << 2) + ((nint)fsizes.LongLength << 2) + 64);
		}
	}

	protected internal long[] pesTokens;

	protected internal RunLength.Integer pesStreamIds;

	protected internal MPSStreamIndex[] streams;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 98, 105, 104, 104, 104 })]
	public MPSIndex(long[] pesTokens, RunLength.Integer pesStreamIds, MPSStreamIndex[] streams)
	{
		this.pesTokens = pesTokens;
		this.pesStreamIds = pesStreamIds;
		this.streams = streams;
	}

	[LineNumberTable(116)]
	public virtual long[] getPesTokens()
	{
		return pesTokens;
	}

	[LineNumberTable(120)]
	public virtual RunLength.Integer getPesStreamIds()
	{
		return pesStreamIds;
	}

	[LineNumberTable(124)]
	public virtual MPSStreamIndex[] getStreams()
	{
		return streams;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 110, 66, 104, 136, 103, 42, 199, 136, 105,
		106, 106, 44, 169
	})]
	public static MPSIndex parseIndex(ByteBuffer index)
	{
		int pesCnt = index.getInt();
		long[] pesTokens = new long[pesCnt];
		for (int j = 0; j < pesCnt; j++)
		{
			pesTokens[j] = index.getLong();
		}
		RunLength.Integer pesStreamId = RunLength.Integer.parse(index);
		int nStreams = index.getInt();
		MPSStreamIndex[] streams = new MPSStreamIndex[nStreams];
		for (int i = 0; i < nStreams; i++)
		{
			streams[i] = MPSStreamIndex.parseIndex(index);
		}
		MPSIndex result = new MPSIndex(pesTokens, pesStreamId, streams);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 130, 111, 109, 48, 167, 141, 111, 118,
		41, 167
	})]
	public virtual void serializeTo(ByteBuffer index)
	{
		index.putInt(pesTokens.Length);
		for (int i = 0; i < (nint)pesTokens.LongLength; i++)
		{
			index.putLong(pesTokens[i]);
		}
		pesStreamIds.serialize(index);
		index.putInt(streams.Length);
		MPSStreamIndex[] array = streams;
		int num = array.Length;
		for (int j = 0; j < num; j++)
		{
			MPSStreamIndex mpsStreamIndex = array[j];
			mpsStreamIndex.serialize(index);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 162, 119, 118, 43, 167 })]
	public virtual int estimateSize()
	{
		int size = (int)(((nint)pesTokens.LongLength << 3) + pesStreamIds.estimateSize());
		MPSStreamIndex[] array = streams;
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			MPSStreamIndex mpsStreamIndex = array[i];
			size += mpsStreamIndex.estimateSize();
		}
		return size + 64;
	}

	[LineNumberTable(167)]
	public static long makePESToken(long leading, long pesLen, long payloadLen)
	{
		return (leading << 48) | (pesLen << 24) | payloadLen;
	}

	[LineNumberTable(171)]
	public static int leadingSize(long token)
	{
		return (int)(token >> 48) & 0xFFFF;
	}

	[LineNumberTable(175)]
	public static int pesLen(long token)
	{
		return (int)(token >> 24) & 0xFFFFFF;
	}

	[LineNumberTable(179)]
	public static int payLoadSize(long token)
	{
		return (int)token & 0xFFFFFF;
	}
}
