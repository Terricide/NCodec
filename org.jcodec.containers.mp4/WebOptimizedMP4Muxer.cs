using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.containers.mp4.muxer;

namespace org.jcodec.containers.mp4;

public class WebOptimizedMP4Muxer : MP4Muxer
{
	private ByteBuffer header;

	private long headerPos;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 112, 113, 142, 109, 110, 141, 115,
		115, 109, 107
	})]
	public WebOptimizedMP4Muxer(SeekableByteChannel output, Brand brand, int headerSize)
		: base(output, brand.getFileTypeBox())
	{
		headerPos = output.position() - 24u;
		output.setPosition(headerPos);
		header = ByteBuffer.allocate(headerSize);
		output.write(header);
		header.clear();
		Header.createHeader("wide", 8L).writeChannel(output);
		Header.createHeader("mdat", 1L).writeChannel(output);
		mdatOffset = output.position();
		NIOUtils.writeLong(output, 0L);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 135, 98, 110, 136, 104, 110, 134, 104, 100,
		109, 142, 105, 110, 172
	})]
	public static WebOptimizedMP4Muxer withOldHeader(SeekableByteChannel output, Brand brand, MovieBox oldHeader)
	{
		int size = (int)oldHeader.getHeader().getSize();
		TrakBox vt = oldHeader.getVideoTrack();
		SampleToChunkBox stsc = vt.getStsc();
		size = (int)(size - (nint)stsc.getSampleToChunk().LongLength * 12);
		size += 12;
		ChunkOffsetsBox stco = vt.getStco();
		if (stco != null)
		{
			size = (int)(size - ((nint)stco.getChunkOffsets().LongLength << 2));
			size += vt.getFrameCount() << 3;
		}
		else
		{
			ChunkOffsets64Box co64 = vt.getCo64();
			size = (int)(size - ((nint)co64.getChunkOffsets().LongLength << 3));
			size += vt.getFrameCount() << 3;
		}
		WebOptimizedMP4Muxer result = new WebOptimizedMP4Muxer(output, brand, size + (size >> 1));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159,
		126,
		130,
		109,
		109,
		115,
		141,
		147,
		109,
		109,
		121,
		101,
		157,
		115,
		101,
		byte.MaxValue,
		16,
		70,
		227,
		59,
		99,
		107,
		127,
		3,
		110,
		141
	})]
	public override void storeHeader(MovieBox movie)
	{
		long mdatEnd = @out.position();
		long mdatSize = mdatEnd - mdatOffset + 8u;
		@out.setPosition(mdatOffset);
		NIOUtils.writeLong(@out, mdatSize);
		@out.setPosition(headerPos);
		ArrayIndexOutOfBoundsException ex2;
		try
		{
			movie.write(header);
			header.flip();
			int rem = header.capacity() - header.limit();
			if (rem < 8)
			{
				header.duplicate().putInt(header.capacity());
			}
			@out.write(header);
			if (rem >= 8)
			{
				Header.createHeader("free", rem).writeChannel(@out);
			}
			return;
		}
		catch (System.Exception x)
		{
			ArrayIndexOutOfBoundsException ex = ByteCodeHelper.MapException<ArrayIndexOutOfBoundsException>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		ArrayIndexOutOfBoundsException e = ex2;
		Logger.warn("Could not web-optimize, header is bigger then allocated space.");
		Header.createHeader("free", header.remaining()).writeChannel(@out);
		@out.setPosition(mdatEnd);
		MP4Util.writeMovie(@out, movie);
	}
}
