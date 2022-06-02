using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using java.util;

namespace org.jcodec.containers.mp4.boxes;

public class UdtaBox : NodeBox
{
	[SpecialName]
	[EnclosingMethod(null, "setFactory", "(Lorg.jcodec.containers.mp4.IBoxFactory;)V")]
	internal class _1 : Object, IBoxFactory
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal IBoxFactory val_0024_factory;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal UdtaBox this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(23)]
		internal _1(UdtaBox this_00240, IBoxFactory ibf)
		{
			this.this_00240 = this_00240;
			val_0024_factory = ibf;
			base._002Ector();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 136, 130, 115, 104, 109, 163 })]
		public virtual Box newBox(Header header)
		{
			if (String.instancehelper_equals(header.getFourcc(), MetaBox.fourcc()))
			{
				UdtaMetaBox box = new UdtaMetaBox(header);
				box.setFactory(val_0024_factory);
				return box;
			}
			Box result = val_0024_factory.newBox(header);
			
			return result;
		}
	}

	private const string FOURCC = "udta";

	[LineNumberTable(46)]
	public static string fourcc()
	{
		return "udta";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 106 })]
	public UdtaBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 162, 104, 124, 115, 131, 99 })]
	internal static Box findGps(UdtaBox udta)
	{
		List boxes1 = udta.getBoxes();
		Iterator iterator = boxes1.iterator();
		while (iterator.hasNext())
		{
			Box box = (Box)iterator.next();
			if (String.instancehelper_endsWith(box.getFourcc(), "xyz"))
			{
				return box;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 98, 105, 104, 138 })]
	internal static ByteBuffer getData(Box box)
	{
		if (box is LeafBox)
		{
			LeafBox leaf = (LeafBox)box;
			ByteBuffer data = leaf.getData();
			
			return data;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(18)]
	public static UdtaBox createUdtaBox()
	{
		UdtaBox result = new UdtaBox(Header.createHeader(fourcc(), 0L));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 238, 76 })]
	public override void setFactory(IBoxFactory _factory)
	{
		factory = new _1(this, _factory);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(42)]
	public virtual MetaBox meta()
	{
		return (MetaBox)NodeBox.findFirst(this, ClassLiteral<MetaBox>.Value, MetaBox.fourcc());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 104, 102, 104, 102, 108, 104, 109,
		105, 104
	})]
	public virtual string latlng()
	{
		Box gps = findGps(this);
		if (gps == null)
		{
			return null;
		}
		ByteBuffer data = getData(gps);
		if (data == null)
		{
			return null;
		}
		if (data.remaining() < 4)
		{
			return null;
		}
		data.getInt();
		byte[] coordsBytes = new byte[data.remaining()];
		data.get(coordsBytes);
		return String.newhelper(coordsBytes);
	}
}
