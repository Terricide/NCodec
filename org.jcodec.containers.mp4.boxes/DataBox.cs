using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mp4.boxes;

public class DataBox : Box
{
	private const string FOURCC = "data";

	private int type;

	private int locale;

	private byte[] data;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 106 })]
	public DataBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 115, 104, 104, 104 })]
	public static DataBox createDataBox(int type, int locale, byte[] data)
	{
		DataBox box = new DataBox(Header.createHeader("data", 0L));
		box.type = type;
		box.locale = locale;
		box.data = data;
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 130, 109, 109, 114 })]
	public override void parse(ByteBuffer buf)
	{
		type = buf.getInt();
		locale = buf.getInt();
		data = NIOUtils.toArray(NIOUtils.readBuf(buf));
	}

	[LineNumberTable(40)]
	public virtual int getType()
	{
		return type;
	}

	[LineNumberTable(44)]
	public virtual int getLocale()
	{
		return locale;
	}

	[LineNumberTable(48)]
	public virtual byte[] getData()
	{
		return data;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 98, 110, 110, 110 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(type);
		@out.putInt(locale);
		@out.put(data);
	}

	[LineNumberTable(60)]
	public override int estimateSize()
	{
		return (int)(16 + (nint)data.LongLength);
	}

	[LineNumberTable(64)]
	public static string fourcc()
	{
		return "data";
	}
}
