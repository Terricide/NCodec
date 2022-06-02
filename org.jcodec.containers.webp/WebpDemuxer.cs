using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.platform;

namespace org.jcodec.containers.webp;

[Implements(new string[] { "org.jcodec.common.Demuxer", "org.jcodec.common.DemuxerTrack" })]
public class WebpDemuxer : java.lang.Object, Demuxer, Closeable, AutoCloseable, DemuxerTrack
{
	public const int FOURCC_RIFF = 1179011410;

	public const int FOURCC_WEBP = 1346520407;

	public const int FOURCC_VP8 = 540561494;

	public const int FOURCC_ICCP = 1346585417;

	public const int FOURCC_ANIM = 1296649793;

	public const int FOURCC_ANMF = 1179471425;

	public const int FOURCC_XMP = 542133592;

	public const int FOURCC_EXIF = 1179211845;

	public const int FOURCC_ALPH = 1213221953;

	public const int FOURCC_VP8L = 1278758998;

	public const int FOURCC_VP8X = 1480085590;

	[Signature("Ljava/util/ArrayList<Lorg/jcodec/common/DemuxerTrack;>;")]
	private ArrayList vt;

	private bool headerRead;

	private DataReader raf;

	private bool done;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 105, 114, 108, 110 })]
	public WebpDemuxer(SeekableByteChannel channel)
	{
		raf = DataReader.createDataReader(channel, ByteOrder.LITTLE_ENDIAN);
		vt = new ArrayList();
		vt.add(this);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 120, 162, 115, 113, 109, 115, 113 })]
	private void readHeader()
	{
		if (raf.readInt() != 1179011410)
		{
			
			throw new IOException("Invalid RIFF file.");
		}
		int size = raf.readInt();
		if (raf.readInt() != 1346520407)
		{
			
			throw new IOException("Not a WEBP file.");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 130, 104, 111, 111, 110, 110 })]
	public static string dwToFourCC(int fourCC)
	{
		string result = Platform.stringFromChars(new char[4]
		{
			(char)((uint)(fourCC >> 24) & 0xFFu),
			(char)((uint)(fourCC >> 16) & 0xFFu),
			(char)((uint)(fourCC >> 8) & 0xFFu),
			(char)((uint)(fourCC >> 0) & 0xFFu)
		});
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 129, 130, 110 })]
	public virtual void close()
	{
		raf.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159,
		128,
		162,
		105,
		131,
		105,
		103,
		136,
		109,
		109,
		104,
		159,
		91,
		104,
		110,
		byte.MaxValue,
		4,
		74,
		127,
		16,
		104,
		142
	})]
	public virtual Packet nextFrame()
	{
		if (done)
		{
			return null;
		}
		if (!headerRead)
		{
			readHeader();
			headerRead = true;
		}
		int fourCC = raf.readInt();
		int size = raf.readInt();
		done = true;
		switch (fourCC)
		{
		case 540561494:
		{
			byte[] b = new byte[size];
			raf.readFully(b);
			Packet.___003Cclinit_003E();
			Packet result = new Packet(ByteBuffer.wrap(b), 0L, 25, 1L, 0L, Packet.FrameType.___003C_003EKEY, null, 0);
			
			return result;
		}
		default:
		{
			Logger.warn(new StringBuilder().append("Skipping unsupported chunk: ").append(dwToFourCC(fourCC)).append(".")
				.toString());
			byte[] b2 = new byte[size];
			raf.readFully(b2);
			return null;
		}
		}
	}

	[LineNumberTable(101)]
	public virtual DemuxerTrackMeta getMeta()
	{
		return null;
	}

	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(106)]
	public virtual List getTracks()
	{
		return vt;
	}

	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(111)]
	public virtual List getVideoTracks()
	{
		return vt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<+Lorg/jcodec/common/DemuxerTrack;>;")]
	[LineNumberTable(116)]
	public virtual List getAudioTracks()
	{
		ArrayList result = new ArrayList();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 98, 104, 107, 99, 109, 110, 99, 104,
		110, 99
	})]
	public static int probe(ByteBuffer b_)
	{
		ByteBuffer b = b_.duplicate();
		if (b.remaining() < 12)
		{
			return 0;
		}
		b.order(ByteOrder.LITTLE_ENDIAN);
		if (b.getInt() != 1179011410)
		{
			return 0;
		}
		int size = b.getInt();
		if (b.getInt() != 1346520407)
		{
			return 0;
		}
		return 100;
	}

	public void Dispose()
	{
		close();
	}
}
