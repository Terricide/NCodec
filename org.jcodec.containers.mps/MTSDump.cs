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
using org.jcodec.platform;

namespace org.jcodec.containers.mps;

public class MTSDump : MPSDump
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag DUMP_FROM;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag STOP_AT;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag[] ALL_FLAGS;

	private int guid;

	private ByteBuffer buf;

	private ByteBuffer tsBuf;

	private int tsNo;

	private int globalPayload;

	private int[] payloads;

	private int[] nums;

	private int[] prevPayloads;

	private int[] prevNums;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 122, 98, 103, 108, 105, 104, 127, 5, 99,
		108, 109, 119, 127, 1, 107, 109, 101, 111, 141,
		106, 112, 104, 100, 104, 149, 102, 181, 101, 105,
		106, 107, 104, 104, 105, 106, 162, 102, 127, 1,
		109, 99
	})]
	private static void dumpProgramPids(ReadableByteChannel readableFileChannel)
	{
		HashSet pids = new HashSet();
		ByteBuffer buf = ByteBuffer.allocate(1925120);
		readableFileChannel.read(buf);
		buf.flip();
		int num = buf.limit();
		int num2 = buf.limit();
		buf.limit(num - ((188 != -1) ? (num2 % 188) : 0));
		int pmtPid = -1;
		while (buf.hasRemaining())
		{
			ByteBuffer tsBuf = NIOUtils.read(buf, 188);
			Preconditions.checkState(71 == ((sbyte)tsBuf.get() & 0xFF));
			int guidFlags = (((sbyte)tsBuf.get() & 0xFF) << 8) | ((sbyte)tsBuf.get() & 0xFF);
			int guid = guidFlags & 0x1FFF;
			java.lang.System.@out.println(guid);
			if (guid != 0)
			{
				((Set)pids).add((object)Integer.valueOf(guid));
			}
			if (guid == 0 || guid == pmtPid)
			{
				int payloadStart = (guidFlags >> 14) & 1;
				int b0 = (sbyte)tsBuf.get() & 0xFF;
				int counter = b0 & 0xF;
				int payloadOff = 0;
				if (((uint)b0 & 0x20u) != 0)
				{
					NIOUtils.skip(tsBuf, (sbyte)tsBuf.get() & 0xFF);
				}
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
					return;
				}
			}
		}
		Iterator iterator = ((Set)pids).iterator();
		while (iterator.hasNext())
		{
			Integer pid = (Integer)iterator.next();
			java.lang.System.@out.println(pid);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 66, 106, 113, 145, 104, 120, 120 })]
	public MTSDump(ReadableByteChannel ch, int targetGuid)
		: base(ch)
	{
		buf = ByteBuffer.allocate(192512);
		tsBuf = ByteBuffer.allocate(188);
		guid = targetGuid;
		buf.position(buf.limit());
		tsBuf.position(tsBuf.limit());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 111, 130, 104, 112, 104, 116, 63, 30, 169,
		109
	})]
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
		java.lang.System.@out.println();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 108, 66, 112, 120, 127, 32, 127, 6, 114,
		227, 60, 234, 70, 109
	})]
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
			Iterator iterator = pmtStream.getDesctiptors().iterator();
			while (iterator.hasNext())
			{
				MPSUtils.MPEGMediaDescriptor descriptor = (MPSUtils.MPEGMediaDescriptor)iterator.next();
				java.lang.System.@out.println(Platform.toJSON(descriptor));
			}
		}
		java.lang.System.@out.println();
	}

	[LineNumberTable(new byte[]
	{
		159, 104, 98, 104, 111, 108, 102, 234, 61, 231,
		70, 105, 111, 108, 102, 234, 61, 231, 71
	})]
	private int mapPos(long pos)
	{
		int left = globalPayload;
		for (int j = (int)((nint)payloads.LongLength - 1); j >= 0; j += -1)
		{
			left -= payloads[j];
			if (left <= pos)
			{
				return nums[j];
			}
		}
		if (prevPayloads != null)
		{
			for (int i = (int)((nint)prevPayloads.LongLength - 1); i >= 0; i += -1)
			{
				left -= prevPayloads[i];
				if (left <= pos)
				{
					return prevNums[i];
				}
			}
		}
		return -1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159,
		128,
		130,
		131,
		109,
		107,
		byte.MaxValue,
		13,
		78,
		76,
		231,
		51,
		98,
		107,
		112,
		byte.MaxValue,
		4,
		74,
		76,
		231,
		55,
		162,
		121,
		109,
		141,
		159,
		0,
		74,
		99,
		99
	})]
	public static void main2(string[] args)
	{
		FileChannelWrapper ch = null;
		MainUtils.Cmd cmd;
		try
		{
			cmd = MainUtils.parseArguments(args, ALL_FLAGS);
			if ((nint)cmd.args.LongLength < 1)
			{
				MainUtils.printHelp(ALL_FLAGS, Arrays.asList("file name", "guid"));
				goto IL_0054;
			}
		}
		catch
		{
			//try-fault
			NIOUtils.closeQuietly(ch);
			throw;
		}
		try
		{
			if ((nint)cmd.args.LongLength == 1)
			{
				java.lang.System.@out.println("MTS programs:");
				
				dumpProgramPids(NIOUtils.readableChannel(new File(cmd.args[0])));
				goto IL_00a7;
			}
		}
		catch
		{
			//try-fault
			NIOUtils.closeQuietly(ch);
			throw;
		}
		try
		{
			
			ch = NIOUtils.readableChannel(new File(cmd.args[0]));
			Long dumpAfterPts = cmd.getLongFlag(DUMP_FROM);
			Long stopPts = cmd.getLongFlag(STOP_AT);
			new MTSDump(ch, Integer.parseInt(cmd.args[1])).dump(dumpAfterPts, stopPts);
			return;
		}
		finally
		{
			NIOUtils.closeQuietly(ch);
		}
		IL_00a7:
		NIOUtils.closeQuietly(null);
		return;
		IL_0054:
		NIOUtils.closeQuietly(null);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 162, 127, 48, 118, 31, 29, 168 })]
	protected internal override void logPes(PESPacket pkt, int hdrSize, ByteBuffer payload)
	{
		java.lang.System.@out.println(new StringBuilder().append(pkt.streamId).append("(").append((pkt.streamId < 224) ? "audio" : "video")
			.append(") [ts#")
			.append(mapPos(pkt.pos))
			.append(", ")
			.append(payload.remaining() + hdrSize)
			.append("b], pts: ")
			.append(pkt.pts)
			.append(", dts: ")
			.append(pkt.dts)
			.toString());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159,
		99,
		98,
		103,
		103,
		168,
		byte.MaxValue,
		14,
		98,
		109,
		109,
		109,
		240,
		28,
		105,
		110,
		109,
		104,
		111,
		102,
		byte.MaxValue,
		2,
		91,
		109,
		109,
		109,
		252,
		61,
		109,
		109,
		109,
		237,
		34,
		100,
		104,
		127,
		5,
		236,
		88,
		109,
		109,
		109,
		240,
		40,
		119,
		124,
		111,
		127,
		11,
		107,
		107,
		102,
		106,
		117,
		104,
		104,
		191,
		0,
		121,
		114,
		143,
		127,
		10,
		133,
		109,
		109,
		109,
		243,
		61,
		109,
		109,
		109,
		109,
		99
	})]
	protected internal override int fillBuffer(ByteBuffer dst)
	{
		IntArrayList payloads = IntArrayList.createIntArrayList();
		IntArrayList nums = IntArrayList.createIntArrayList();
		int remaining = dst.remaining();
		try
		{
			dst.put(NIOUtils.read(tsBuf, Math.min(dst.remaining(), tsBuf.remaining())));
		}
		catch
		{
			//try-fault
			prevPayloads = this.payloads;
			this.payloads = payloads.toArray();
			prevNums = this.nums;
			this.nums = nums.toArray();
			throw;
		}
		while (true)
		{
			ByteBuffer dub;
			int result;
			try
			{
				if (!dst.hasRemaining())
				{
					break;
				}
				if (!buf.hasRemaining())
				{
					dub = buf.duplicate();
					dub.clear();
					int read = ch.read(dub);
					if (read == -1)
					{
						result = ((dst.remaining() == remaining) ? (-1) : (remaining - dst.remaining()));
						goto IL_0120;
					}
					goto IL_0158;
				}
				goto IL_01c7;
			}
			catch
			{
				//try-fault
				prevPayloads = this.payloads;
				this.payloads = payloads.toArray();
				prevNums = this.nums;
				this.nums = nums.toArray();
				throw;
			}
			IL_0120:
			prevPayloads = this.payloads;
			this.payloads = payloads.toArray();
			prevNums = this.nums;
			this.nums = nums.toArray();
			return result;
			IL_0158:
			try
			{
				dub.flip();
				int num = dub.limit();
				int num2 = dub.limit();
				dub.limit(num - ((188 != -1) ? (num2 % 188) : 0));
				buf = dub;
			}
			catch
			{
				//try-fault
				prevPayloads = this.payloads;
				this.payloads = payloads.toArray();
				prevNums = this.nums;
				this.nums = nums.toArray();
				throw;
			}
			goto IL_01c7;
			IL_01c7:
			try
			{
				tsBuf = NIOUtils.read(buf, 188);
				Preconditions.checkState(71 == ((sbyte)tsBuf.get() & 0xFF));
				tsNo++;
				int guidFlags = (((sbyte)tsBuf.get() & 0xFF) << 8) | ((sbyte)tsBuf.get() & 0xFF);
				int guid = guidFlags & 0x1FFF;
				if (guid == this.guid)
				{
					int payloadStart = (guidFlags >> 14) & 1;
					int b0 = (sbyte)tsBuf.get() & 0xFF;
					int counter = b0 & 0xF;
					if (((uint)b0 & 0x20u) != 0)
					{
						NIOUtils.skip(tsBuf, (sbyte)tsBuf.get() & 0xFF);
					}
					globalPayload += tsBuf.remaining();
					payloads.add(tsBuf.remaining());
					nums.add(tsNo - 1);
					dst.put(NIOUtils.read(tsBuf, Math.min(dst.remaining(), tsBuf.remaining())));
				}
			}
			catch
			{
				//try-fault
				prevPayloads = this.payloads;
				this.payloads = payloads.toArray();
				prevNums = this.nums;
				this.nums = nums.toArray();
				throw;
			}
		}
		prevPayloads = this.payloads;
		this.payloads = payloads.toArray();
		prevNums = this.nums;
		this.nums = nums.toArray();
		return remaining - dst.remaining();
	}

	[LineNumberTable(new byte[] { 159, 134, 103, 118, 118 })]
	static MTSDump()
	{
		MPSDump.___003Cclinit_003E();
		DUMP_FROM = MainUtils.Flag.flag("dump-from", null, "Stop reading at timestamp");
		STOP_AT = MainUtils.Flag.flag("stop-at", null, "Start dumping from timestamp");
		ALL_FLAGS = new MainUtils.Flag[2] { DUMP_FROM, STOP_AT };
	}
}
