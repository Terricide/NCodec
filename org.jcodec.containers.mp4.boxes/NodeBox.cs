using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.lang.reflect;
using java.nio;
using java.util;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class NodeBox : Box
{
	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Box;>;")]
	protected internal List boxes;

	protected internal IBoxFactory factory;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mp4/boxes/Box;>(Lorg/jcodec/containers/mp4/boxes/NodeBox;Ljava/lang/Class<TT;>;Ljava/lang/String;)TT;")]
	[LineNumberTable(173)]
	public static Box findFirst(NodeBox box, Class clazz, string path)
	{
		Box result = findFirstPath(box, clazz, new string[1] { path });
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mp4/boxes/Box;>(Lorg/jcodec/containers/mp4/boxes/NodeBox;Ljava/lang/Class<TT;>;[Ljava/lang/String;)TT;")]
	[LineNumberTable(new byte[] { 159, 98, 98, 111 })]
	public static Box findFirstPath(NodeBox box, Class clazz, string[] path)
	{
		Box[] result = findAllPath(box, clazz, path);
		return ((nint)result.LongLength <= 0) ? null : result[0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 130, 110 })]
	public virtual void add(Box box)
	{
		boxes.add(box);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 162, 104, 114, 106, 106, 131, 99, 104,
		115, 150
	})]
	public static Box parseChildBox(ByteBuffer input, IBoxFactory factory)
	{
		ByteBuffer fork = input.duplicate();
		while (input.remaining() >= 4 && fork.getInt() == 0)
		{
			input.getInt();
		}
		if (input.remaining() < 4)
		{
			return null;
		}
		Box ret = null;
		Header childAtom = Header.read(input);
		if (childAtom != null && input.remaining() >= childAtom.getBodySize())
		{
			ret = Box.parseBox(NIOUtils.read(input, (int)childAtom.getBodySize()), childAtom, factory);
		}
		return ret;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 162, 117, 109, 104, 104, 102, 107, 103,
		227, 60, 231, 71, 102
	})]
	public virtual void removeChildren(string[] fourcc)
	{
		Iterator it = boxes.iterator();
		while (it.hasNext())
		{
			Box box = (Box)it.next();
			string fcc = box.getFourcc();
			for (int i = 0; i < (nint)fourcc.LongLength; i++)
			{
				string cand = fourcc[i];
				if (java.lang.String.instancehelper_equals(cand, fcc))
				{
					it.remove();
					break;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 114, 162, 113, 120, 113, 237, 61, 231, 69 })]
	protected internal virtual void dumpBoxes(StringBuilder sb)
	{
		for (int i = 0; i < boxes.size(); i++)
		{
			((Box)boxes.get(i)).dump(sb);
			if (i < boxes.size() - 1)
			{
				sb.append(",");
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 98, 104, 104, 104 })]
	public static Box doCloneBox(Box box, int approxSize, IBoxFactory bf)
	{
		ByteBuffer buf = ByteBuffer.allocate(approxSize);
		box.write(buf);
		buf.flip();
		Box result = parseChildBox(buf, bf);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mp4/boxes/Box;>(Lorg/jcodec/containers/mp4/boxes/Box;Ljava/lang/Class<TT;>;Ljava/lang/String;Ljava/util/List<TT;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 104, 130, 100, 98, 116, 105, 130, 105, 104,
		127, 2, 106, 131
	})]
	public static void findDeepInner(Box box, Class class1, string name, List storage)
	{
		if (box == null)
		{
			return;
		}
		if (java.lang.String.instancehelper_equals(name, box.getHeader().getFourcc()))
		{
			storage.add(box);
		}
		else if (box is NodeBox)
		{
			NodeBox nb = (NodeBox)box;
			Iterator iterator = nb.getBoxes().iterator();
			while (iterator.hasNext())
			{
				Box candidate = (Box)iterator.next();
				findDeepInner(candidate, class1, name, storage);
			}
		}
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/Box;>;")]
	[LineNumberTable(70)]
	public virtual List getBoxes()
	{
		return boxes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mp4/boxes/Box;>(Lorg/jcodec/containers/mp4/boxes/Box;Ljava/lang/Class<TT;>;[Ljava/lang/String;)[TT;")]
	[LineNumberTable(new byte[]
	{
		159,
		97,
		130,
		103,
		152,
		115,
		109,
		100,
		108,
		178,
		byte.MaxValue,
		6,
		69,
		227,
		60,
		99,
		127,
		29,
		48,
		134,
		167,
		102
	})]
	public static Box[] findAllPath(Box box, Class class1, string[] path)
	{
		LinkedList result = new LinkedList();
		findBox(box, new ArrayList(Arrays.asList(path)), result);
		ListIterator it = ((List)result).listIterator();
		while (it.hasNext())
		{
			Box next = (Box)it.next();
			java.lang.Exception ex2;
			if (next == null)
			{
				it.remove();
			}
			else
			{
				if (Platform.isAssignableFrom(class1, java.lang.Object.instancehelper_getClass(next)))
				{
					continue;
				}
				try
				{
					it.set(Box.asBox(class1, next));
				}
				catch (System.Exception x)
				{
					java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
					if (ex == null)
					{
						throw;
					}
					ex2 = ex;
					goto IL_0084;
				}
			}
			continue;
			IL_0084:
			java.lang.Exception e = ex2;
			Logger.warn(new StringBuilder().append("Failed to reinterpret box: ").append(next.getFourcc()).append(" as: ")
				.append(class1.getName())
				.append(".")
				.append(Throwable.instancehelper_getMessage(e))
				.toString());
			it.remove();
		}
		return (Box[])((List)result).toArray((object[])(Box[])java.lang.reflect.Array.newInstance(class1, 0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Lorg/jcodec/containers/mp4/boxes/Box;Ljava/util/List<Ljava/lang/String;>;Ljava/util/Collection<Lorg/jcodec/containers/mp4/boxes/Box;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 91, 98, 109, 110, 105, 104, 127, 2, 119,
		137, 131, 105, 99, 137
	})]
	public static void findBox(Box root, List path, Collection result)
	{
		if (path.size() > 0)
		{
			string head = (string)path.remove(0);
			if (root is NodeBox)
			{
				NodeBox nb = (NodeBox)root;
				Iterator iterator = nb.getBoxes().iterator();
				while (iterator.hasNext())
				{
					Box candidate = (Box)iterator.next();
					if (head == null || java.lang.String.instancehelper_equals(head, candidate.header.getFourcc()))
					{
						findBox(candidate, path, result);
					}
				}
			}
			path.add(0, head);
		}
		else
		{
			result.add(root);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 106, 108 })]
	public NodeBox(Header atom)
		: base(atom)
	{
		boxes = new LinkedList();
	}

	[LineNumberTable(new byte[] { 159, 132, 130, 104 })]
	public virtual void setFactory(IBoxFactory factory)
	{
		this.factory = factory;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 162, 106, 110, 100, 110, 99 })]
	public override void parse(ByteBuffer input)
	{
		while (input.remaining() >= 8)
		{
			Box child = parseChildBox(input, factory);
			if (child != null)
			{
				boxes.add(child);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 130, 127, 2, 104, 99 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		Iterator iterator = boxes.iterator();
		while (iterator.hasNext())
		{
			Box box = (Box)iterator.next();
			box.write(@out);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 98, 99, 127, 2, 106, 99 })]
	public override int estimateSize()
	{
		int total = 0;
		Iterator iterator = boxes.iterator();
		while (iterator.hasNext())
		{
			Box box = (Box)iterator.next();
			total += box.estimateSize();
		}
		return total + Header.estimateHeaderSize(total);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 98, 112 })]
	public virtual void addFirst(MovieHeaderBox box)
	{
		boxes.add(0, box);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 98, 113, 106 })]
	public virtual void replace(string fourcc, Box box)
	{
		removeChildren(new string[1] { fourcc });
		add(box);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 130, 118, 106 })]
	public virtual void replaceBox(Box box)
	{
		removeChildren(new string[1] { box.getFourcc() });
		add(box);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 116, 162, 127, 23, 109, 104, 109, 109 })]
	protected internal override void dump(StringBuilder sb)
	{
		sb.append(new StringBuilder().append("{\"tag\":\"").append(header.getFourcc()).append("\",")
			.toString());
		sb.append("\"boxes\": [");
		dumpBoxes(sb);
		sb.append("]");
		sb.append("}");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(144)]
	public static Box cloneBox(Box box, int approxSize, IBoxFactory bf)
	{
		Box result = doCloneBox(box, approxSize, bf);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mp4/boxes/Box;>(Lorg/jcodec/containers/mp4/boxes/Box;Ljava/lang/Class<TT;>;Ljava/lang/String;)[TT;")]
	[LineNumberTable(new byte[] { 159, 105, 66, 103, 106 })]
	public static Box[] findDeep(Box box, Class class1, string name)
	{
		ArrayList storage = new ArrayList();
		findDeepInner(box, class1, name, storage);
		return (Box[])((List)storage).toArray((object[])(Box[])java.lang.reflect.Array.newInstance(class1, 0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mp4/boxes/Box;>(Lorg/jcodec/containers/mp4/boxes/Box;Ljava/lang/Class<TT;>;Ljava/lang/String;)[TT;")]
	[LineNumberTable(169)]
	public static Box[] findAll(Box box, Class class1, string path)
	{
		Box[] result = findAllPath(box, class1, new string[1] { path });
		
		return result;
	}
}
