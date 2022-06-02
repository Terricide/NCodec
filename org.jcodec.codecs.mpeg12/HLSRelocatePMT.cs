using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.tools;
using org.jcodec.containers.mps.psi;

namespace org.jcodec.codecs.mpeg12;

public class HLSRelocatePMT : Object
{
	private const int TS_START_CODE = 71;

	private const int CHUNK_SIZE_PKT = 1024;

	private const int TS_PKT_SIZE = 188;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 128, 66, 108, 103, 103, 99, 132, 100, 110,
		104, 154, 108, 110, 106, 120, 103, 127, 3, 107,
		106, 113, 104, 104, 183, 119, 102, 183, 101, 100,
		106, 111, 107, 104, 15, 201, 120, 101, 105, 106,
		127, 1, 106, 99, 169, 101, 140, 138, 102, 141
	})]
	private static int replocatePMT(ReadableByteChannel _in, WritableByteChannel @out)
	{
		ByteBuffer buf = ByteBuffer.allocate(192512);
		HashSet pmtPids = new HashSet();
		ArrayList held = new ArrayList();
		ByteBuffer patPkt = null;
		ByteBuffer pmtPkt = null;
		int totalPkt = 0;
		while (_in.read(buf) != -1)
		{
			buf.flip();
			buf.limit(buf.limit() / 188 * 188);
			while (buf.hasRemaining())
			{
				ByteBuffer pkt = NIOUtils.read(buf, 188);
				ByteBuffer pktRead = pkt.duplicate();
				Preconditions.checkState(71 == ((sbyte)pktRead.get() & 0xFF));
				totalPkt++;
				int guidFlags = (((sbyte)pktRead.get() & 0xFF) << 8) | ((sbyte)pktRead.get() & 0xFF);
				int guid = guidFlags & 0x1FFF;
				int payloadStart = (guidFlags >> 14) & 1;
				int b0 = (sbyte)pktRead.get() & 0xFF;
				int counter = b0 & 0xF;
				if (((uint)b0 & 0x20u) != 0)
				{
					NIOUtils.skip(pktRead, (sbyte)pktRead.get() & 0xFF);
				}
				if (guid == 0 || ((Set)pmtPids).contains((object)Integer.valueOf(guid)))
				{
					if (payloadStart == 1)
					{
						NIOUtils.skip(pktRead, (sbyte)pktRead.get() & 0xFF);
					}
					if (guid == 0)
					{
						patPkt = pkt;
						PATSection pat = PATSection.parsePAT(pktRead);
						int[] values = pat.getPrograms().values();
						for (int i = 0; i < (nint)values.LongLength; i++)
						{
							int pmtPid = values[i];
							((Set)pmtPids).add((object)Integer.valueOf(pmtPid));
						}
					}
					else if (((Set)pmtPids).contains((object)Integer.valueOf(guid)))
					{
						pmtPkt = pkt;
						@out.write(patPkt);
						@out.write(pmtPkt);
						Iterator iterator = ((List)held).iterator();
						while (iterator.hasNext())
						{
							ByteBuffer heldPkt = (ByteBuffer)iterator.next();
							@out.write(heldPkt);
						}
						((List)held).clear();
					}
				}
				else if (pmtPkt == null)
				{
					((List)held).add((object)pkt);
				}
				else
				{
					@out.write(pkt);
				}
			}
			buf.clear();
		}
		return totalPkt;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(30)]
	public HLSRelocatePMT()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 110, 107, 124, 162, 99, 131, 121,
		121, 159, 26, 103, 42, 131, 99
	})]
	public static void main1(string[] args)
	{
		MainUtils.Cmd cmd = MainUtils.parseArguments(args, new MainUtils.Flag[0]);
		if ((nint)cmd.args.LongLength < 2)
		{
			MainUtils.printHelpNoFlags("file _in", "file out");
			return;
		}
		FileChannelWrapper _in = null;
		FileChannelWrapper @out = null;
		try
		{
			
			_in = NIOUtils.readableChannel(new File(cmd.args[0]));
			
			@out = NIOUtils.writableChannel(new File(cmd.args[1]));
			java.lang.System.err.println(new StringBuilder().append("Processed: ").append(replocatePMT(_in, @out)).append(" packets.")
				.toString());
		}
		finally
		{
			NIOUtils.closeQuietly(_in);
			NIOUtils.closeQuietly(@out);
		}
	}
}
