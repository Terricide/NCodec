using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class FielExtension : Box
{
	private int type;

	private int order;

	[LineNumberTable(25)]
	public virtual bool isInterlaced()
	{
		return type == 2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 130, 106 })]
	public FielExtension(Header header)
		: base(header)
	{
	}

	[LineNumberTable(21)]
	public static string fourcc()
	{
		return "fiel";
	}

	[LineNumberTable(29)]
	public virtual bool topFieldFirst()
	{
		return (order == 1 || order == 6) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 201, 191, 9, 167, 167, 199, 167 })]
	public virtual string getOrderInterpretation()
	{
		if (isInterlaced())
		{
			switch (order)
			{
			case 1:
				return "top";
			case 6:
				return "bottom";
			case 9:
				return "bottomtop";
			case 14:
				return "topbottom";
			}
		}
		return "";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 130, 116, 105, 148 })]
	public override void parse(ByteBuffer input)
	{
		type = (sbyte)input.get() & 0xFF;
		if (isInterlaced())
		{
			order = (sbyte)input.get() & 0xFF;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 130, 111, 111 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put((byte)(sbyte)type);
		@out.put((byte)(sbyte)order);
	}

	[LineNumberTable(72)]
	public override int estimateSize()
	{
		return 10;
	}
}
