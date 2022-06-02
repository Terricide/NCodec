using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.tools;
using org.jcodec.containers.mps.psi;

namespace org.jcodec.containers.mps;

public class MTSPktDump : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 131, 66, 140, 110, 104, 122, 99, 110, 109,
		119, 127, 1, 107, 106, 112, 104, 104, 149, 191,
		72, 141, 112, 102, 181, 101, 105, 106, 107, 104,
		104, 105, 104, 131, 159, 11, 235, 31, 234, 99,
		104, 102
	})]
	private static void dumpTSPackets(ReadableByteChannel _in)
	{
		ByteBuffer buf = ByteBuffer.allocate(192512);
		while (_in.read(buf) != -1)
		{
			buf.flip();
			buf.limit(buf.limit() / 188 * 188);
			int pmtPid = -1;
			int pkt = 0;
			while (buf.hasRemaining())
			{
				ByteBuffer tsBuf = NIOUtils.read(buf, 188);
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
				java.lang.System.@out.print(new StringBuilder().append("#").append(pkt).append("[guid: ")
					.append(guid)
					.append(", cnt: ")
					.append(counter)
					.append(", start: ")
					.append((payloadStart != 1) ? "-" : "y")
					.toString());
				if (guid == 0 || guid == pmtPid)
				{
					java.lang.System.@out.print(", PSI]: ");
					if (payloadStart == 1)
					{
						NIOUtils.skip(tsBuf, (sbyte)tsBuf.get() & 0xFF);
					}
					if (guid == 0)
					{
						PATSection pat = PATSection.parsePAT(tsBuf);
						IntIntMap programs = pat.getPrograms();
						pmtPid = programs.values()[0];
						printPat(pat);
					}
					else if (guid == pmtPid)
					{
						PMTSection pmt = PMTSection.parsePMT(tsBuf);
						printPmt(pmt);
					}
				}
				else
				{
					java.lang.System.@out.print(new StringBuilder().append("]: ").append(tsBuf.remaining()).toString());
				}
				java.lang.System.@out.println();
				pkt++;
			}
			buf.clear();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 130, 104, 112, 104, 116, 63, 30, 169 })]
	private static void printPat(PATSection pat)
	{
		IntIntMap programs = pat.getPrograms();
		java.lang.System.@out.print("PAT: ");
		int[] keys = programs.keys();
		int[] array = keys;
		int num = array.Length;
		for (int j = 0; j < num; j++)
		{
			int i = array[j];
			java.lang.System.@out.print(new StringBuilder().append(i).append(":").append(programs.get(i))
				.append(", ")
				.toString());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 162, 112, 117, 63, 32, 167 })]
	private static void printPmt(PMTSection pmt)
	{
		java.lang.System.@out.print("PMT: ");
		PMTSection.PMTStream[] streams = pmt.getStreams();
		int num = streams.Length;
		for (int i = 0; i < num; i++)
		{
			PMTSection.PMTStream pmtStream = streams[i];
			java.lang.System.@out.print(new StringBuilder().append(pmtStream.getPid()).append(":").append(pmtStream.getStreamTypeTag())
				.append(", ")
				.toString());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public MTSPktDump()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 110, 107, 116, 162, 131, 121, 139,
		74, 99, 99
	})]
	public static void main1(string[] args)
	{
		MainUtils.Cmd cmd = MainUtils.parseArguments(args, new MainUtils.Flag[0]);
		if ((nint)cmd.args.LongLength < 1)
		{
			MainUtils.printHelpNoFlags("file name");
			return;
		}
		FileChannelWrapper ch = null;
		try
		{
			
			ch = NIOUtils.readableChannel(new File(cmd.args[0]));
			dumpTSPackets(ch);
		}
		finally
		{
			NIOUtils.closeQuietly(ch);
		}
	}
}
