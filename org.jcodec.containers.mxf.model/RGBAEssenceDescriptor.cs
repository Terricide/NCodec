using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class RGBAEssenceDescriptor : GenericPictureEssenceDescriptor
{
	private int componentMaxRef;

	private int componentMinRef;

	private int alphaMaxRef;

	private int alphaMinRef;

	private byte scanningDirection;

	private ByteBuffer pixelLayout;

	private ByteBuffer palette;

	private ByteBuffer paletteLayout;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 106 })]
	public RGBAEssenceDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 134, 98, 136, 120, 141, 141, 159, 38, 109,
		134, 109, 134, 109, 134, 109, 134, 110, 134, 104,
		131, 104, 131, 104, 131, 127, 20, 39, 139, 134,
		103, 102
	})]
	protected internal override void read(Map tags)
	{
		base.read(tags);
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			ByteBuffer _bb = (ByteBuffer)entry.getValue();
			switch (((Integer)entry.getKey()).intValue())
			{
			case 13318:
				componentMaxRef = _bb.getInt();
				break;
			case 13319:
				componentMinRef = _bb.getInt();
				break;
			case 13320:
				alphaMaxRef = _bb.getInt();
				break;
			case 13321:
				alphaMinRef = _bb.getInt();
				break;
			case 13317:
				scanningDirection = (byte)(sbyte)_bb.get();
				break;
			case 13313:
				pixelLayout = _bb;
				break;
			case 13315:
				palette = _bb;
				break;
			case 13316:
				paletteLayout = _bb;
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(75)]
	public virtual int getComponentMaxRef()
	{
		return componentMaxRef;
	}

	[LineNumberTable(79)]
	public virtual int getComponentMinRef()
	{
		return componentMinRef;
	}

	[LineNumberTable(83)]
	public virtual int getAlphaMaxRef()
	{
		return alphaMaxRef;
	}

	[LineNumberTable(87)]
	public virtual int getAlphaMinRef()
	{
		return alphaMinRef;
	}

	[LineNumberTable(91)]
	public virtual byte getScanningDirection()
	{
		return (byte)(sbyte)scanningDirection;
	}

	[LineNumberTable(95)]
	public virtual ByteBuffer getPixelLayout()
	{
		return pixelLayout;
	}

	[LineNumberTable(99)]
	public virtual ByteBuffer getPalette()
	{
		return palette;
	}

	[LineNumberTable(103)]
	public virtual ByteBuffer getPaletteLayout()
	{
		return paletteLayout;
	}
}
