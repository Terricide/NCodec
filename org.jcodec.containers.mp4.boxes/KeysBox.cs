using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class KeysBox : NodeBox
{
	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class LocalBoxes : Boxes
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 137, 98, 105, 119 })]
		internal LocalBoxes()
		{
			___003C_003Emappings.put(MdtaBox.fourcc(), ClassLiteral<MdtaBox>.Value);
		}
	}

	private const string FOURCC = "keys";

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 106, 113 })]
	public KeysBox(Header atom)
		: base(atom)
	{
		factory = new SimpleBoxFactory(new LocalBoxes());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(32)]
	public static KeysBox createKeysBox()
	{
		KeysBox result = new KeysBox(Header.createHeader("keys", 0L));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 104, 104, 106 })]
	public override void parse(ByteBuffer input)
	{
		int vf = input.getInt();
		int cnt = input.getInt();
		base.parse(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 130, 105, 115, 106 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(0);
		@out.putInt(boxes.size());
		base.doWrite(@out);
	}

	[LineNumberTable(48)]
	public static string fourcc()
	{
		return "keys";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(53)]
	public override int estimateSize()
	{
		return 8 + base.estimateSize();
	}
}
