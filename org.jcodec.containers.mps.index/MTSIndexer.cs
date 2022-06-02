using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mps.index;

public class MTSIndexer : Object
{
	[SpecialName]
	[EnclosingMethod(null, "main1", "([Ljava.lang.String;)V")]
	internal class _1 : Object, NIOUtils.FileReaderListener
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(131)]
		internal _1()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 109, 98, 110 })]
		public virtual void progress(int percentDone)
		{
			java.lang.System.@out.println(percentDone);
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class MTSAnalyser : BaseIndexer
	{
		private int targetGuid;

		private long predFileStartInTsPkt;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 117, 162, 105, 104 })]
		public MTSAnalyser(int targetGuid)
		{
			this.targetGuid = targetGuid;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(108)]
		public virtual MTSIndex.MTSProgram serializeTo()
		{
			MTSIndex.MTSProgram result = MTSIndex.createMTSProgram(base.serialize(), targetGuid);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 114, 66, 106, 98, 127, 9, 105, 99, 106,
			146, 114, 114, 121, 114
		})]
		protected internal override void pes(ByteBuffer pesBuffer, long start, int pesLen, int stream)
		{
			if (MPSUtils.mediaStream(stream))
			{
				Logger.debug(String.format("PES: %08x, %d", Long.valueOf(start), Integer.valueOf(pesLen)));
				PESPacket pesHeader = MPSUtils.readPESHeader(pesBuffer, start);
				int leadingTsPkt = 0;
				if (predFileStartInTsPkt != start)
				{
					leadingTsPkt = (int)(start / 188L - predFileStartInTsPkt);
				}
				predFileStartInTsPkt = (start + pesLen) / 188L;
				int tsPktInPes = (int)(predFileStartInTsPkt - start / 188L);
				savePESMeta(stream, MPSIndex.makePESToken(leadingTsPkt, tsPktInPes, pesBuffer.remaining()));
				getAnalyser(stream).pkt(pesBuffer, pesHeader);
			}
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(98)]
		internal static int access_0024100(MTSAnalyser x0)
		{
			return x0.targetGuid;
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	internal class MTSFileReader : NIOUtils.FileReader
	{
		private MTSIndexer indexer;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 128, 162, 105, 104 })]
		public MTSFileReader(MTSIndexer indexer)
		{
			this.indexer = indexer;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 125, 66, 108, 109, 107, 119, 127, 0, 137,
			149, 121, 105, 112, 104, 104, 149, 253, 55, 234,
			76, 102
		})]
		protected internal virtual void analyseBuffer(ByteBuffer buf, long pos)
		{
			while (buf.hasRemaining())
			{
				ByteBuffer tsBuf = NIOUtils.read(buf, 188);
				pos += 188u;
				Preconditions.checkState(71 == ((sbyte)tsBuf.get() & 0xFF));
				int guidFlags = (((sbyte)tsBuf.get() & 0xFF) << 8) | ((sbyte)tsBuf.get() & 0xFF);
				int guid = guidFlags & 0x1FFF;
				for (int i = 0; i < (nint)access_0024000(indexer).LongLength; i++)
				{
					if (guid == MTSAnalyser.access_0024100(access_0024000(indexer)[i]))
					{
						int payloadStart = (guidFlags >> 14) & 1;
						int b0 = (sbyte)tsBuf.get() & 0xFF;
						int counter = b0 & 0xF;
						if (((uint)b0 & 0x20u) != 0)
						{
							NIOUtils.skip(tsBuf, (sbyte)tsBuf.get() & 0xFF);
						}
						access_0024000(indexer)[i].analyseBuffer(tsBuf, pos - tsBuf.remaining());
					}
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 126, 66, 107 })]
		protected internal override void data(ByteBuffer data, long filePos)
		{
			analyseBuffer(data, filePos);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 119, 66, 122, 39, 167 })]
		protected internal override void done()
		{
			MTSAnalyser[] array = access_0024000(indexer);
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				MTSAnalyser mtsAnalyser = array[i];
				mtsAnalyser.finishAnalyse();
			}
		}
	}

	public const int BUFFER_SIZE = 96256;

	private MTSAnalyser[] indexers;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 132, 130, 110, 104, 49, 167 })]
	public virtual NIOUtils.FileReader indexReader(NIOUtils.FileReaderListener listener, int[] targetGuids)
	{
		indexers = new MTSAnalyser[(nint)targetGuids.LongLength];
		for (int i = 0; i < (nint)targetGuids.LongLength; i++)
		{
			indexers[i] = new MTSAnalyser(targetGuids[i]);
		}
		MTSFileReader result = new MTSFileReader(this);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(29)]
	public MTSIndexer()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 134, 130, 124 })]
	public virtual void index(File source, NIOUtils.FileReaderListener listener)
	{
		indexReader(listener, MTSUtils.getMediaPids(source)).readFile(source, 96256, listener);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 130, 110, 109, 49, 135 })]
	public virtual MTSIndex serialize()
	{
		MTSIndex.MTSProgram[] programs = new MTSIndex.MTSProgram[(nint)indexers.LongLength];
		for (int i = 0; i < (nint)indexers.LongLength; i++)
		{
			programs[i] = indexers[i].serializeTo();
		}
		MTSIndex result = new MTSIndex(programs);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 130, 124 })]
	public virtual void indexChannel(SeekableByteChannel source, NIOUtils.FileReaderListener listener)
	{
		indexReader(listener, MTSUtils.getMediaPidsFromChannel(source)).readChannel(source, 96256, listener);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 110, 66, 143, 103, 237, 69, 104, 123 })]
	public static void main1(string[] args)
	{
		
		File src = new File(args[0]);
		MTSIndexer indexer = new MTSIndexer();
		indexer.index(src, new _1());
		MTSIndex index = indexer.serialize();
		ByteBuffer buffer = index.serialize();
		
		NIOUtils.writeTo(buffer, new File(args[1]));
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(29)]
	internal static MTSAnalyser[] access_0024000(MTSIndexer x0)
	{
		return x0.indexers;
	}
}
