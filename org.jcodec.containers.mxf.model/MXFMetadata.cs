using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.io;
using org.jcodec.platform;

namespace org.jcodec.containers.mxf.model;

public abstract class MXFMetadata : Object
{
	protected internal UL ul;

	protected internal UL uid;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 105, 104 })]
	public MXFMetadata(UL ul)
	{
		this.ul = ul;
	}

	public abstract void readBuf(ByteBuffer bb);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 104, 104, 104, 103, 42, 167 })]
	protected internal static UL[] readULBatch(ByteBuffer _bb)
	{
		int count = _bb.getInt();
		_bb.getInt();
		UL[] result = new UL[count];
		for (int i = 0; i < count; i++)
		{
			result[i] = UL.read(_bb);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 98, 104, 104, 104, 103, 42, 167 })]
	protected internal static int[] readInt32Batch(ByteBuffer _bb)
	{
		int count = _bb.getInt();
		_bb.getInt();
		int[] result = new int[count];
		for (int i = 0; i < count; i++)
		{
			result[i] = _bb.getInt();
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 162, 103, 110, 111, 111, 112, 112, 112,
		152
	})]
	protected internal static Date readDate(ByteBuffer _bb)
	{
		Calendar calendar = Calendar.getInstance();
		calendar.set(1, _bb.getShort());
		calendar.set(2, (sbyte)_bb.get());
		calendar.set(5, (sbyte)_bb.get());
		calendar.set(10, (sbyte)_bb.get());
		calendar.set(12, (sbyte)_bb.get());
		calendar.set(13, (sbyte)_bb.get());
		calendar.set(14, ((sbyte)_bb.get() & 0xFF) << 2);
		Date time = calendar.getTime();
		
		return time;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 130, 113, 138, 154 })]
	protected internal virtual string readUtf16String(ByteBuffer _bb)
	{
		byte[] array = ((_bb.getShort(_bb.limit() - 2) == 0) ? NIOUtils.toArray((ByteBuffer)_bb.limit(_bb.limit() - 2)) : NIOUtils.toArray(_bb));
		string result = Platform.stringFromCharset(array, "UTF-16");
		
		return result;
	}

	[LineNumberTable(83)]
	public virtual UL getUl()
	{
		return ul;
	}

	[LineNumberTable(87)]
	public virtual UL getUid()
	{
		return uid;
	}
}
