using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class MdtaBox : Box
{
	private const string FOURCC = "mdta";

	private string key;

	[LineNumberTable(50)]
	public static string fourcc()
	{
		return "mdta";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 106 })]
	public MdtaBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 115, 104 })]
	public static MdtaBox createMdtaBox(string key)
	{
		MdtaBox box = new MdtaBox(Header.createHeader("mdta", 0L));
		box.key = key;
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 119 })]
	public override void parse(ByteBuffer buf)
	{
		key = Platform.stringFromBytes(NIOUtils.toArray(NIOUtils.readBuf(buf)));
	}

	[LineNumberTable(36)]
	public virtual string getKey()
	{
		return key;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 115 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put(String.instancehelper_getBytes(key));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(46)]
	public override int estimateSize()
	{
		return String.instancehelper_getBytes(key).Length;
	}
}
