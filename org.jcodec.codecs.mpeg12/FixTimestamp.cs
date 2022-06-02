using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.containers.mps;

namespace org.jcodec.codecs.mpeg12;

public abstract class FixTimestamp : Object
{
	[SpecialName]
	[EnclosingMethod(null, "fix", "(Ljava.io.File;)V")]
	internal class _1 : MTSUtils.TSReader
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal FixTimestamp val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal FixTimestamp this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 136, 129, 67 })]
		internal _1(FixTimestamp this_00240, bool flush, FixTimestamp ft) : base(flush)
		{
			this.this_00240 = this_00240;
			val_0024self = ft;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 129, 70 })]
		protected internal override bool onPkt(int guid, bool payloadStart, ByteBuffer bb, long filePos, bool sectionSyntax, ByteBuffer fullPkt)
		{
			bool result = access_0024000(val_0024self, payloadStart, bb, sectionSyntax, fullPkt);
			
			return result;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 65, 69, 103, 131, 104, 121, 104, 144,
		112, 112, 145, 175
	})]
	private bool processPacket(bool payloadStart, ByteBuffer bb, bool sectionSyntax, ByteBuffer fullPkt)
	{
		if (!payloadStart || sectionSyntax)
		{
			return true;
		}
		int streamId = bb.getInt();
		switch (streamId)
		{
		case 445:
		case 448:
		case 449:
		case 450:
		case 451:
		case 452:
		case 453:
		case 454:
		case 455:
		case 456:
		case 457:
		case 458:
		case 459:
		case 460:
		case 461:
		case 462:
		case 463:
		case 464:
		case 465:
		case 466:
		case 467:
		case 468:
		case 469:
		case 470:
		case 471:
		case 472:
		case 473:
		case 474:
		case 475:
		case 476:
		case 477:
		case 478:
		case 479:
		case 480:
		case 481:
		case 482:
		case 483:
		case 484:
		case 485:
		case 486:
		case 487:
		case 488:
		case 489:
		case 490:
		case 491:
		case 492:
		case 493:
		case 494:
		{
			int len = bb.getShort();
			int b0 = (sbyte)bb.get() & 0xFF;
			bb.position(bb.position() - 1);
			if ((b0 & 0xC0) == 128)
			{
				fixMpeg2(streamId & 0xFF, bb);
			}
			else
			{
				fixMpeg1(streamId & 0xFF, bb);
			}
			break;
		}
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 116, 98, 111, 111, 143, 111, 109, 111, 107,
		139
	})]
	public virtual void fixMpeg2(int streamId, ByteBuffer @is)
	{
		int flags1 = (sbyte)@is.get() & 0xFF;
		int flags2 = (sbyte)@is.get() & 0xFF;
		int header_len = (sbyte)@is.get() & 0xFF;
		if ((flags2 & 0xC0) == 128)
		{
			fixTs(streamId, @is, isPts: true);
		}
		else if ((flags2 & 0xC0) == 192)
		{
			fixTs(streamId, @is, isPts: true);
			fixTs(streamId, @is, isPts: false);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 162, 110, 105, 177, 108, 105, 143, 108,
		112, 109, 108, 112, 107, 141, 102, 145
	})]
	public virtual void fixMpeg1(int streamId, ByteBuffer @is)
	{
		int c;
		for (c = @is.getInt() & 0xFF; c == 255; c = (sbyte)@is.get() & 0xFF)
		{
		}
		if ((c & 0xC0) == 64)
		{
			_ = (sbyte)@is.get();
			c = (sbyte)@is.get() & 0xFF;
		}
		if ((c & 0xF0) == 32)
		{
			@is.position(@is.position() - 1);
			fixTs(streamId, @is, isPts: true);
		}
		else if ((c & 0xF0) == 48)
		{
			@is.position(@is.position() - 1);
			fixTs(streamId, @is, isPts: true);
			fixTs(streamId, @is, isPts: false);
		}
		else if (c != 15)
		{
			
			throw new RuntimeException("Invalid data");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 129, 67, 105, 105, 105, 106, 138, 191,
		31, 141, 144, 123, 111, 114, 110, 145
	})]
	public virtual long fixTs(int streamId, ByteBuffer @is, bool isPts)
	{
		int b0 = (sbyte)@is.get();
		int b1 = (sbyte)@is.get();
		int b2 = (sbyte)@is.get();
		int b3 = (sbyte)@is.get();
		int b4 = (sbyte)@is.get();
		long pts = ((b0 & 0xEu) << 29) | ((b1 & 0xFF) << 22) | ((b2 & 0xFF) >> 1 << 15) | ((b3 & 0xFF) << 7) | ((b4 & 0xFF) >> 1);
		pts = doWithTimestamp(streamId, pts, isPts);
		@is.position(@is.position() - 5);
		@is.put((byte)(sbyte)((ulong)(int)((uint)b0 & 0xF0u) | ((ulong)pts >> 29) | 1u));
		@is.put((byte)(sbyte)((ulong)pts >> 22));
		@is.put((byte)(sbyte)(((ulong)pts >> 14) | 1u));
		@is.put((byte)(sbyte)((ulong)pts >> 7));
		@is.put((byte)(sbyte)((pts << 1) | 1u));
		return pts;
	}

	protected internal abstract long doWithTimestamp(int i, long l, bool b);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(18)]
	public FixTimestamp()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 131, 109, 109, 99, 234, 70, 138,
		100, 42, 100, 137
	})]
	public virtual void fix(File file)
	{
		RandomAccessFile ra = null;
		try
		{
			ra = new RandomAccessFile(file, "rw");
			FileChannelWrapper ch = new FileChannelWrapper(ra.getChannel());
			new _1(this, flush: true, this).readTsFile(ch);
		}
		catch
		{
			//try-fault
			ra?.close();
			throw;
		}
		ra?.close();
	}

	[LineNumberTable(118)]
	public virtual bool isVideo(int streamId)
	{
		return (streamId >= 224 && streamId <= 239) ? true : false;
	}

	[LineNumberTable(122)]
	public virtual bool isAudio(int streamId)
	{
		return (streamId >= 191 && streamId <= 223) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(new byte[] { 159, 138, 129, 69 })]
	internal static bool access_0024000(FixTimestamp x0, bool x1, ByteBuffer x2, bool x3, ByteBuffer x4)
	{
		bool result = x0.processPacket(x1, x2, x3, x4);
		
		return result;
	}
}
