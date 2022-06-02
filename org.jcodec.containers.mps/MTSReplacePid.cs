using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.tools;
using org.jcodec.containers.mps.psi;

namespace org.jcodec.containers.mps;

public class MTSReplacePid : MTSUtils.TSReader
{
	[Signature("Ljava/util/Set<Ljava/lang/Integer;>;")]
	private Set pmtPids;

	private IntIntMap replaceSpec;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Lorg/jcodec/common/IntIntMap;ILjava/nio/ByteBuffer;Ljava/util/Set<Ljava/lang/Integer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 127, 130, 100, 104, 123, 48, 167, 120, 117,
		136, 136, 148, 109, 106, 106, 159, 59, 111, 107,
		108, 159, 3, 116, 134
	})]
	private void replaceRefs(IntIntMap replaceSpec, int guid, ByteBuffer buf, Set pmtPids)
	{
		if (guid == 0)
		{
			PATSection pat = PATSection.parsePAT(buf);
			int[] array = pat.getPrograms().values();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				int pids = array[i];
				pmtPids.add(Integer.valueOf(pids));
			}
		}
		else if (pmtPids.contains(Integer.valueOf(guid)))
		{
			java.lang.System.@out.println(MainUtils.bold("PMT"));
			PSISection.parsePSI(buf);
			buf.getShort();
			NIOUtils.skip(buf, buf.getShort() & 0xFFF);
			while (buf.remaining() > 4)
			{
				int streamType = (sbyte)buf.get();
				MTSStreamType fromTag = MTSStreamType.fromTag(streamType);
				java.lang.System.@out.print(new StringBuilder().append((fromTag != null) ? ((object)fromTag) : ((object)"UNKNOWN")).append("(").append(String.format("0x%02x", Byte.valueOf((byte)streamType)))
					.append("):\t")
					.toString());
				int wn = buf.getShort() & 0xFFFF;
				int wasPid = wn & 0x1FFF;
				int elementaryPid = replacePid(replaceSpec, wasPid);
				buf.putShort(buf.position() - 2, (short)((elementaryPid & 0x1FFF) | (wn & -8192)));
				NIOUtils.skip(buf, buf.getShort() & 0xFFF);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 120, 162, 99, 106, 137, 127, 32 })]
	private int replacePid(IntIntMap replaceSpec, int pid)
	{
		int newPid = pid;
		if (replaceSpec.contains(pid))
		{
			newPid = replaceSpec.get(pid);
		}
		java.lang.System.@out.println(new StringBuilder().append("[").append(pid).append("->")
			.append(newPid)
			.append("]")
			.toString());
		return newPid;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 103, 123, 111, 25, 231, 69 })]
	private static IntIntMap parseReplaceSpec(string spec)
	{
		IntIntMap map = new IntIntMap();
		string[] array = String.instancehelper_split(spec, ",");
		int num = array.Length;
		for (int i = 0; i < num; i++)
		{
			string pidPair = array[i];
			string[] pidPairParsed = String.instancehelper_split(pidPair, ":");
			map.put(Integer.parseInt(pidPairParsed[0]), Integer.parseInt(pidPairParsed[1]));
		}
		return map;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 106, 108, 104 })]
	public MTSReplacePid(IntIntMap replaceSpec)
		: base(flush: true)
	{
		pmtPids = new HashSet();
		this.replaceSpec = replaceSpec;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 65, 68, 100, 151, 112, 105, 112, 159,
		13
	})]
	protected internal override bool onPkt(int guid, bool payloadStart, ByteBuffer tsBuf, long filePos, bool sectionSyntax, ByteBuffer fullPkt)
	{
		if (sectionSyntax)
		{
			replaceRefs(replaceSpec, guid, tsBuf, pmtPids);
		}
		else
		{
			java.lang.System.@out.print("TS ");
			ByteBuffer buf = fullPkt.duplicate();
			int tsFlags = buf.getShort(buf.position() + 1);
			buf.putShort(buf.position() + 1, (short)(replacePid(replaceSpec, tsFlags & 0x1FFF) | (tsFlags & -8192)));
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 117, 66, 110, 107, 124, 162, 142, 131, 152,
		145, 74, 99, 99
	})]
	public static void main1(string[] args)
	{
		MainUtils.Cmd cmd = MainUtils.parseArguments(args, new MainUtils.Flag[0]);
		if ((nint)cmd.args.LongLength < 2)
		{
			MainUtils.printHelpNoFlags("pid_from:pid_to,[pid_from:pid_to...]", "file");
			return;
		}
		IntIntMap replaceSpec = parseReplaceSpec(cmd.getArg(0));
		FileChannelWrapper ch = null;
		try
		{
			
			ch = NIOUtils.rwChannel(new File(cmd.getArg(1)));
			new MTSReplacePid(replaceSpec).readTsFile(ch);
		}
		finally
		{
			NIOUtils.closeQuietly(ch);
		}
	}
}
