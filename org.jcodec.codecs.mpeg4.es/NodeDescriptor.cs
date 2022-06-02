using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.util;

namespace org.jcodec.codecs.mpeg4.es;

public class NodeDescriptor : Descriptor
{
	[Signature("Ljava/util/Collection<Lorg/jcodec/codecs/mpeg4/es/Descriptor;>;")]
	private Collection children;

	[Signature("()Ljava/util/Collection<Lorg/jcodec/codecs/mpeg4/es/Descriptor;>;")]
	[LineNumberTable(28)]
	public virtual Collection getChildren()
	{
		return children;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Lorg/jcodec/codecs/mpeg4/es/Descriptor;I)TT;")]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 106, 131, 105, 127, 7, 105, 100,
		99, 163
	})]
	public static object findByTag(Descriptor es, int tag)
	{
		if (es.getTag() == tag)
		{
			return es;
		}
		if (es is NodeDescriptor)
		{
			Iterator iterator = ((NodeDescriptor)es).getChildren().iterator();
			while (iterator.hasNext())
			{
				Descriptor descriptor = (Descriptor)iterator.next();
				object res = findByTag(descriptor, tag);
				if (res != null)
				{
					return res;
				}
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 107, 104 })]
	public NodeDescriptor(int tag, Collection children)
		: base(tag, 0)
	{
		this.children = children;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 127, 2, 104, 99 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		Iterator iterator = children.iterator();
		while (iterator.hasNext())
		{
			Descriptor descr = (Descriptor)iterator.next();
			descr.write(@out);
		}
	}
}
