using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.containers.dpx;

public class ImageHeader : Object
{
	public short orientation;

	public short numberOfImageElements;

	public int linesPerImageElement;

	public int pixelsPerLine;

	public ImageElement imageElement1;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public ImageHeader()
	{
	}
}
