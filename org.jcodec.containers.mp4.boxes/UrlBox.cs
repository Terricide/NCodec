using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class UrlBox : FullBox
{
	private string url;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 118, 104 })]
	public static UrlBox createUrlBox(string url)
	{
		
		UrlBox urlBox = new UrlBox(new Header(fourcc()));
		urlBox.url = url;
		return urlBox;
	}

	[LineNumberTable(20)]
	public static string fourcc()
	{
		return "url ";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 106 })]
	public UrlBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 104, 107, 98, 114 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		if ((flags & 1) == 0)
		{
			url = NIOUtils.readNullTermStringCharset(input, "UTF-8");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 136, 105, 124, 137 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		if (url != null)
		{
			NIOUtils.write(@out, ByteBuffer.wrap(Platform.getBytesForCharset(url, "UTF-8")));
			@out.put(0);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 98, 132, 105, 149 })]
	public override int estimateSize()
	{
		int sz = 13;
		if (url != null)
		{
			sz = (int)(sz + (nint)Platform.getBytesForCharset(url, "UTF-8").LongLength);
		}
		return sz;
	}

	[LineNumberTable(62)]
	public virtual string getUrl()
	{
		return url;
	}
}
