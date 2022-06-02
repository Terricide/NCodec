using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.containers.mp4.boxes;

public class SegmentTypeBox : Box
{
	private string majorBrand;

	private int minorVersion;

	[Signature("Ljava/util/Collection<Ljava/lang/String;>;")]
	private Collection compBrands;

	[LineNumberTable(39)]
	public static string fourcc()
	{
		return "styp";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 106, 108 })]
	public SegmentTypeBox(Header header)
		: base(header)
	{
		compBrands = new LinkedList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/lang/String;ILjava/util/Collection<Ljava/lang/String;>;)Lorg/jcodec/containers/mp4/boxes/SegmentTypeBox;")]
	[LineNumberTable(new byte[] { 159, 135, 162, 118, 104, 104, 104 })]
	public static SegmentTypeBox createSegmentTypeBox(string majorBrand, int minorVersion, Collection compBrands)
	{
		
		SegmentTypeBox styp = new SegmentTypeBox(new Header(fourcc()));
		styp.majorBrand = majorBrand;
		styp.minorVersion = minorVersion;
		styp.compBrands = compBrands;
		return styp;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 110, 173, 116, 144 })]
	public override void parse(ByteBuffer input)
	{
		majorBrand = NIOUtils.readString(input, 4);
		minorVersion = input.getInt();
		string brand;
		while (input.hasRemaining() && (brand = NIOUtils.readString(input, 4)) != null)
		{
			compBrands.add(brand);
		}
	}

	[LineNumberTable(53)]
	public virtual string getMajorBrand()
	{
		return majorBrand;
	}

	[Signature("()Ljava/util/Collection<Ljava/lang/String;>;")]
	[LineNumberTable(57)]
	public virtual Collection getCompBrands()
	{
		return compBrands;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 98, 115, 142, 127, 2, 110, 99 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put(JCodecUtil2.asciiString(majorBrand));
		@out.putInt(minorVersion);
		Iterator iterator = compBrands.iterator();
		while (iterator.hasNext())
		{
			string @string = (string)iterator.next();
			@out.put(JCodecUtil2.asciiString(@string));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 125, 162, 132, 127, 2, 107, 99 })]
	public override int estimateSize()
	{
		int sz = 13;
		Iterator iterator = compBrands.iterator();
		while (iterator.hasNext())
		{
			string @string = (string)iterator.next();
			sz = (int)(sz + (nint)JCodecUtil2.asciiString(@string).LongLength);
		}
		return sz;
	}
}
