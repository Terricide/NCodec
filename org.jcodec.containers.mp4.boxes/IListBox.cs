using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.io;

namespace org.jcodec.containers.mp4.boxes;

public class IListBox : Box
{
	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class LocalBoxes : Boxes
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 98, 105, 119 })]
		internal LocalBoxes()
		{
			___003C_003Emappings.put(DataBox.fourcc(), ClassLiteral<DataBox>.Value);
		}
	}

	private const string FOURCC = "ilst";

	[Signature("Ljava/util/Map<Ljava/lang/Integer;Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Box;>;>;")]
	private Map values;

	private IBoxFactory factory;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 106, 113, 108 })]
	public IListBox(Header atom)
		: base(atom)
	{
		factory = new SimpleBoxFactory(new LocalBoxes());
		values = new LinkedHashMap();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Box;>;>;)Lorg/jcodec/containers/mp4/boxes/IListBox;")]
	[LineNumberTable(new byte[] { 159, 132, 98, 115, 104 })]
	public static IListBox createIListBox(Map values)
	{
		IListBox box = new IListBox(Header.createHeader("ilst", 0L));
		box.values = values;
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 162, 109, 104, 107, 104, 103, 116, 105,
		105, 117, 126, 138, 99, 102
	})]
	public override void parse(ByteBuffer input)
	{
		while (input.remaining() >= 4)
		{
			int size = input.getInt();
			ByteBuffer local = NIOUtils.read(input, size - 4);
			int index = local.getInt();
			ArrayList children = new ArrayList();
			values.put(Integer.valueOf(index), children);
			while (local.hasRemaining())
			{
				Header childAtom = Header.read(local);
				if (childAtom != null && local.remaining() >= childAtom.getBodySize())
				{
					Box box = Box.parseBox(NIOUtils.read(local, (int)childAtom.getBodySize()), childAtom, factory);
					((List)children).add((object)box);
				}
			}
		}
	}

	[Signature("()Ljava/util/Map<Ljava/lang/Integer;Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Box;>;>;")]
	[LineNumberTable(64)]
	public virtual Map getValues()
	{
		return values;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 66, 127, 10, 104, 105, 120, 127, 8,
		105, 99, 117, 102
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		Iterator iterator = values.entrySet().iterator();
		while (iterator.hasNext())
		{
			Map.Entry entry = (Map.Entry)iterator.next();
			ByteBuffer fork = @out.duplicate();
			@out.putInt(0);
			@out.putInt(((Integer)entry.getKey()).intValue());
			Iterator iterator2 = ((List)entry.getValue()).iterator();
			while (iterator2.hasNext())
			{
				Box box = (Box)iterator2.next();
				box.write(@out);
			}
			fork.putInt(@out.position() - fork.position());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 98, 99, 127, 7, 127, 8, 109, 99,
		99
	})]
	public override int estimateSize()
	{
		int sz = 8;
		Iterator iterator = values.entrySet().iterator();
		while (iterator.hasNext())
		{
			Map.Entry entry = (Map.Entry)iterator.next();
			Iterator iterator2 = ((List)entry.getValue()).iterator();
			while (iterator2.hasNext())
			{
				Box box = (Box)iterator2.next();
				sz += 8 + box.estimateSize();
			}
		}
		return sz;
	}

	[LineNumberTable(91)]
	public static string fourcc()
	{
		return "ilst";
	}
}
