using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using java.util.zip;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mps;

namespace org.jcodec.codecs.mpeg12;

public class HLSFixPMT : Object
{
	[SpecialName]
	[EnclosingMethod(null, "main1", "([Ljava.lang.String;)V")]
	internal class _1 : Object, FilenameFilter
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(101)]
		internal _1()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(103)]
		public virtual bool accept(File dir, string name)
		{
			bool result = String.instancehelper_endsWith(name, ".ts");
			
			return result;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 162, 104, 104, 136, 106, 104, 104, 100,
		105, 137, 99, 146, 105, 106, 114, 152, 104, 110,
		110, 144, 105, 139
	})]
	public static void fixPAT(ByteBuffer data)
	{
		ByteBuffer table = data.duplicate();
		MTSUtils.parseSection(data);
		ByteBuffer newPmt = data.duplicate();
		while (data.remaining() > 4)
		{
			int num = data.getShort();
			int pid = data.getShort();
			if (num != 0)
			{
				newPmt.putShort((short)num);
				newPmt.putShort((short)pid);
			}
		}
		if (newPmt.position() != data.position())
		{
			ByteBuffer section = table.duplicate();
			_ = (sbyte)section.get();
			int sectionLen = newPmt.position() - table.position() + 1;
			section.putShort((short)((sectionLen & 0xFFF) | 0xB000));
			CRC32 crc32 = new CRC32();
			table.limit(newPmt.position());
			crc32.update(NIOUtils.toArray(table));
			newPmt.putInt((int)crc32.getValue());
			while (newPmt.hasRemaining())
			{
				newPmt.put(byte.MaxValue);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 162, 112, 108, 105 })]
	private static void exit(string message)
	{
		java.lang.System.err.println("Syntax: hls_fixpmt <hls package location>");
		java.lang.System.err.println(message);
		java.lang.System.exit(-1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(22)]
	public HLSFixPMT()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 136, 98, 163, 109, 140, 146, 109, 107, 105,
		105, 102, 104, 100, 104, 139, 102, 174, 100, 101,
		113, 117, 104, 116, 136, 136, 100, 42, 100, 137
	})]
	public virtual void fix(File file)
	{
		RandomAccessFile ra = null;
		try
		{
			ra = new RandomAccessFile(file, "rw");
			byte[] tsPkt = new byte[188];
			while (ra.read(tsPkt) == 188)
			{
				Preconditions.checkState(71 == tsPkt[0]);
				int guidFlags = (tsPkt[1] << 8) | tsPkt[2];
				int guid = guidFlags & 0x1FFF;
				int payloadStart = (guidFlags >> 14) & 1;
				int b0 = tsPkt[3];
				int counter = b0 & 0xF;
				int payloadOff = 0;
				if (((uint)b0 & 0x20u) != 0)
				{
					payloadOff = tsPkt[4 + payloadOff] + 1;
				}
				if (payloadStart == 1)
				{
					payloadOff += tsPkt[4 + payloadOff] + 1;
				}
				if (guid == 0)
				{
					if (payloadStart == 0)
					{
						
						throw new RuntimeException("PAT spans multiple TS packets, not supported!!!!!!");
					}
					ByteBuffer bb = ByteBuffer.wrap(tsPkt, 4 + payloadOff, 184 - payloadOff);
					fixPAT(bb);
					ra.seek(ra.getFilePointer() - 188u);
					ra.write(tsPkt);
				}
			}
		}
		catch
		{
			//try-fault
			ra?.close();
			throw;
		}
		ra?.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 119, 98, 102, 139, 143, 105, 139, 237, 69,
		103, 104, 102, 127, 12, 233, 61, 231, 69
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 1)
		{
			exit("Please specify package location");
		}
		
		File hlsPkg = new File(args[0]);
		if (!hlsPkg.isDirectory())
		{
			exit("Not an HLS package, expected a folder");
		}
		File[] listFiles = hlsPkg.listFiles(new _1());
		HLSFixPMT fix = new HLSFixPMT();
		for (int i = 0; i < (nint)listFiles.LongLength; i++)
		{
			File file = listFiles[i];
			java.lang.System.err.println(new StringBuilder().append("Processing: ").append(file.getName()).toString());
			fix.fix(file);
		}
	}
}
