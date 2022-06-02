using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mps.index;

public class MPSIndexer : BaseIndexer
{
	[SpecialName]
	[EnclosingMethod(null, "newReader", "()Lorg.jcodec.common.io.NIOUtils$FileReader;")]
	internal new class _1 : NIOUtils.FileReader
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal MPSIndexer val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal MPSIndexer this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(37)]
		internal _1(MPSIndexer this_00240, MPSIndexer mpsi) : base()
		{
			this.this_00240 = this_00240;
			val_0024self = mpsi;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 132, 66, 112 })]
		protected internal override void data(ByteBuffer data, long filePos)
		{
			val_0024self.analyseBuffer(data, filePos);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 131, 66, 110 })]
		protected internal override void done()
		{
			val_0024self.finishAnalyse();
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "main1", "([Ljava.lang.String;)V")]
	internal class _2 : Object, NIOUtils.FileReaderListener
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(65)]
		internal _2()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 126, 162, 110 })]
		public virtual void progress(int percentDone)
		{
			java.lang.System.@out.println(percentDone);
		}
	}

	private long predFileStart;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 99 })]
	private NIOUtils.FileReader newReader()
	{
		_1 result = new _1(this, this);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public MPSIndexer()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 135, 66, 117 })]
	public virtual void index(File source, NIOUtils.FileReaderListener listener)
	{
		newReader().readFile(source, 65536, listener);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 134, 66, 117 })]
	public virtual void indexChannel(SeekableByteChannel source, NIOUtils.FileReaderListener listener)
	{
		newReader().readChannel(source, 65536, listener);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 162, 106, 98, 105, 99, 106, 141, 107,
		121, 114
	})]
	protected internal override void pes(ByteBuffer pesBuffer, long start, int pesLen, int stream)
	{
		if (MPSUtils.mediaStream(stream))
		{
			PESPacket pesHeader = MPSUtils.readPESHeader(pesBuffer, start);
			int leading = 0;
			if (predFileStart != start)
			{
				leading += (int)(start - predFileStart);
			}
			predFileStart = start + pesLen;
			savePESMeta(stream, MPSIndex.makePESToken(leading, pesLen, pesBuffer.remaining()));
			getAnalyser(stream).pkt(pesBuffer, pesHeader);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 126, 66, 103, 249, 69, 108, 109, 118 })]
	public static void main1(string[] args)
	{
		MPSIndexer indexer = new MPSIndexer();
		indexer.index(new File(args[0]), new _2());
		ByteBuffer index = ByteBuffer.allocate(65536);
		indexer.serialize().serializeTo(index);
		NIOUtils.writeTo(index, new File(args[1]));
	}
}
