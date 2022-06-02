using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common.io;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4;

public class BoxUtil : java.lang.Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mp4/boxes/Box;>(Ljava/lang/Class<TT;>;Lorg/jcodec/containers/mp4/boxes/Box$LeafBox;)TT;")]
	[LineNumberTable(new byte[] { 159, 132, 98, 124, 114, 125, 98 })]
	public static Box @as(Class class1, Box.LeafBox box)
	{
		//Discarded unreachable code: IL_0033
		java.lang.Exception ex2;
		try
		{
			Box res = (Box)Platform.newInstance(class1, new object[1] { box.getHeader() });
			res.parse(box.getData().duplicate());
			return res;
		}
		catch (System.Exception x)
		{
			java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		java.lang.Exception e = ex2;
		
		throw new RuntimeException(e);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 130, 137, 111, 104, 131 })]
	public static Box parseBox(ByteBuffer input, Header childAtom, IBoxFactory factory)
	{
		Box box = factory.newBox(childAtom);
		if (childAtom.getBodySize() < 134217728u)
		{
			box.parse(input);
			return box;
		}
		Box.LeafBox result = new Box.LeafBox(Header.createHeader("free", 8L));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(11)]
	public BoxUtil()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 104, 114, 106, 106, 131, 104, 115,
		152
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
		Header childAtom = Header.read(input);
		if (childAtom != null && input.remaining() >= childAtom.getBodySize())
		{
			Box result = parseBox(NIOUtils.read(input, (int)childAtom.getBodySize()), childAtom, factory);
			
			return result;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 130, 119 })]
	public static bool containsBox(NodeBox box, string path)
	{
		Box b = NodeBox.findFirstPath(box, ClassLiteral<Box>.Value, new string[1] { path });
		return b != null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 162, 123 })]
	public static bool containsBox2(NodeBox box, string path1, string path2)
	{
		Box b = NodeBox.findFirstPath(box, ClassLiteral<Box>.Value, new string[2] { path1, path2 });
		return b != null;
	}
}
