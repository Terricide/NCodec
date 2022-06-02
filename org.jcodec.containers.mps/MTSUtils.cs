using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mps.psi;

namespace org.jcodec.containers.mps;

public class MTSUtils : java.lang.Object
{
	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class PMTExtractor : TSReader
	{
		private int pmtGuid;

		private PMTSection pmt;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 123, 66, 170, 8 })]
		public PMTExtractor()
			: base(flush: false)
		{
			pmtGuid = -1;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 121, 98, 100, 111, 115, 109, 131 })]
		protected internal override bool onPkt(int guid, bool payloadStart, ByteBuffer tsBuf, long filePos, bool sectionSyntax, ByteBuffer fullPkt)
		{
			if (guid == 0)
			{
				pmtGuid = parsePAT(tsBuf);
			}
			else if (pmtGuid != -1 && guid == pmtGuid)
			{
				pmt = parsePMT(tsBuf);
				return false;
			}
			return true;
		}

		[LineNumberTable(95)]
		public virtual PMTSection getPmt()
		{
			return pmt;
		}
	}

	public abstract class TSReader : java.lang.Object
	{
		private const int TS_SYNC_MARKER = 71;

		private const int TS_PKT_SIZE = 188;

		public const int BUFFER_SIZE = 96256;

		private bool flush;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 115, 162, 106, 140, 121, 99, 104, 113, 109,
			105, 106, 119, 127, 1, 139, 106, 112, 104, 104,
			149, 127, 11, 133, 149, 126, 98, 102, 105, 104,
			105, 137, 232, 34, 237, 96
		})]
		public virtual void readTsFile(SeekableByteChannel ch)
		{
			ch.setPosition(0L);
			ByteBuffer buf = ByteBuffer.allocate(96256);
			long pos = ch.position();
			while (ch.read(buf) >= 188)
			{
				long posRem = pos;
				buf.flip();
				while (buf.remaining() >= 188)
				{
					ByteBuffer tsBuf = NIOUtils.read(buf, 188);
					ByteBuffer fullPkt = tsBuf.duplicate();
					pos += 188u;
					Preconditions.checkState(71 == ((sbyte)tsBuf.get() & 0xFF));
					int guidFlags = (((sbyte)tsBuf.get() & 0xFF) << 8) | ((sbyte)tsBuf.get() & 0xFF);
					int guid = guidFlags & 0x1FFF;
					int payloadStart = (guidFlags >> 14) & 1;
					int b0 = (sbyte)tsBuf.get() & 0xFF;
					int counter = b0 & 0xF;
					if (((uint)b0 & 0x20u) != 0)
					{
						NIOUtils.skip(tsBuf, (sbyte)tsBuf.get() & 0xFF);
					}
					int sectionSyntax = ((payloadStart == 1 && ((sbyte)NIOUtils.getRel(tsBuf, (sbyte)NIOUtils.getRel(tsBuf, 0) + 2) & 0x80) == 128) ? 1 : 0);
					if (sectionSyntax != 0)
					{
						NIOUtils.skip(tsBuf, (sbyte)tsBuf.get() & 0xFF);
					}
					if (!onPkt(guid, payloadStart == 1, tsBuf, pos - tsBuf.remaining(), (byte)sectionSyntax != 0, fullPkt))
					{
						return;
					}
				}
				if (flush)
				{
					buf.flip();
					ch.setPosition(posRem);
					ch.write(buf);
				}
				buf.clear();
				pos = ch.position();
			}
		}

		[LineNumberTable(151)]
		protected internal virtual bool onPkt(int guid, bool payloadStart, ByteBuffer tsBuf, long filePos, bool sectionSyntax, ByteBuffer fullPkt)
		{
			return true;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 116, 129, 67, 105, 104 })]
		public TSReader(bool flush)
		{
			this.flush = flush;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Obsolete]
	[LineNumberTable(50)]
	[Deprecated(new object[]
	{
		(byte)64,
		"Ljava/lang/Deprecated;"
	})]
	public static PSISection parseSection(ByteBuffer data)
	{
		PSISection result = PSISection.parsePSI(data);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Obsolete]
	[LineNumberTable(new byte[] { 159, 133, 66, 104, 111, 143 })]
	[Deprecated(new object[]
	{
		(byte)64,
		"Ljava/lang/Deprecated;"
	})]
	public static int parsePAT(ByteBuffer data)
	{
		PATSection pat = PATSection.parsePAT(data);
		if (pat.getPrograms().size() > 0)
		{
			return pat.getPrograms().values()[0];
		}
		return -1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Obsolete]
	[LineNumberTable(45)]
	[Deprecated(new object[]
	{
		(byte)64,
		"Ljava/lang/Deprecated;"
	})]
	public static PMTSection parsePMT(ByteBuffer data)
	{
		PMTSection result = PMTSection.parsePMT(data);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(178)]
	public static int[] getMediaPids(File src)
	{
		int[] result = filterMediaPids(getProgramGuids(src));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(174)]
	public static int[] getMediaPidsFromChannel(SeekableByteChannel src)
	{
		int[] result = filterMediaPids(getProgramGuidsFromChannel(src));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 125, 66, 103, 104, 104 })]
	public static PMTSection.PMTStream[] getProgramGuidsFromChannel(SeekableByteChannel _in)
	{
		PMTExtractor ex = new PMTExtractor();
		ex.readTsFile(_in);
		PMTSection pmt = ex.getPmt();
		PMTSection.PMTStream[] streams = pmt.getStreams();
		
		return streams;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 128, 130, 131, 104, 140, 74, 3 })]
	public static PMTSection.PMTStream[] getProgramGuids(File src)
	{
		FileChannelWrapper ch = null;
		try
		{
			ch = NIOUtils.readableChannel(src);
			return getProgramGuidsFromChannel(ch);
		}
		finally
		{
			NIOUtils.closeQuietly(ch);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 97, 130, 103, 113, 125, 14, 231, 69 })]
	private static int[] filterMediaPids(PMTSection.PMTStream[] programs)
	{
		IntArrayList result = IntArrayList.createIntArrayList();
		int num = programs.Length;
		for (int i = 0; i < num; i++)
		{
			PMTSection.PMTStream stream = programs[i];
			if (stream.getStreamType().isVideo() || stream.getStreamType().isAudio())
			{
				result.add(stream.getPid());
			}
		}
		int[] result2 = result.toArray();
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public MTSUtils()
	{
	}

	[LineNumberTable(55)]
	private static void parseEsInfo(ByteBuffer read)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 103, 66, 117, 110, 10, 231, 69 })]
	public static int getVideoPid(File src)
	{
		PMTSection.PMTStream[] programGuids = getProgramGuids(src);
		int num = programGuids.Length;
		for (int i = 0; i < num; i++)
		{
			PMTSection.PMTStream stream = programGuids[i];
			if (stream.getStreamType().isVideo())
			{
				int pid = stream.getPid();
				
				return pid;
			}
		}
		
		throw new RuntimeException("No video stream");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 101, 98, 117, 110, 10, 231, 69 })]
	public static int getAudioPid(File src)
	{
		PMTSection.PMTStream[] programGuids = getProgramGuids(src);
		int num = programGuids.Length;
		for (int i = 0; i < num; i++)
		{
			PMTSection.PMTStream stream = programGuids[i];
			if (stream.getStreamType().isAudio())
			{
				int pid = stream.getPid();
				
				return pid;
			}
		}
		
		throw new RuntimeException("No audio stream");
	}
}
