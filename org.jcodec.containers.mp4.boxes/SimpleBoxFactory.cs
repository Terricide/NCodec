using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class SimpleBoxFactory : Object, IBoxFactory
{
	private Boxes boxes;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 66, 105, 104 })]
	public SimpleBoxFactory(Boxes boxes)
	{
		this.boxes = boxes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 115, 100, 106, 119 })]
	public virtual Box newBox(Header header)
	{
		Class claz = boxes.toClass(header.getFourcc());
		if (claz == null)
		{
			Box.LeafBox result = new Box.LeafBox(header);
			
			return result;
		}
		return (Box)Platform.newInstance(claz, new object[1] { header });
	}
}
