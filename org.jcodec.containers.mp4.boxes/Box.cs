using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public abstract class Box : java.lang.Object
{
	public class LeafBox : Box
	{
		internal ByteBuffer data;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(119)]
		public virtual ByteBuffer getData()
		{
			ByteBuffer result = data.duplicate();
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 115, 162, 106 })]
		public LeafBox(Header atom)
			: base(atom)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 114, 162, 121 })]
		public override void parse(ByteBuffer input)
		{
			data = NIOUtils.read(input, (int)header.getBodySize());
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 111, 66, 111 })]
		protected internal override void doWrite(ByteBuffer @out)
		{
			NIOUtils.write(@out, data);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(129)]
		public override int estimateSize()
		{
			return data.remaining() + Header.estimateHeaderSize(data.remaining());
		}
	}

	public Header header;

	public const int MAX_BOX_SIZE = 134217728;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(55)]
	public virtual string getFourcc()
	{
		string fourcc = header.getFourcc();
		return fourcc;
	}

	protected internal abstract void doWrite(ByteBuffer bb);

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 130, 127, 23 })]
	protected internal virtual void dump(StringBuilder sb)
	{
		sb.append(new StringBuilder().append("{\"tag\":\"").append(header.getFourcc()).append("\"}")
			.toString());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 130, 104, 104 })]
	public static LeafBox createLeafBox(Header atom, ByteBuffer data)
	{
		LeafBox leaf = new LeafBox(atom);
		leaf.data = data;
		return leaf;
	}

	public abstract void parse(ByteBuffer bb);

	[LineNumberTable(35)]
	public virtual Header getHeader()
	{
		return header;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 105, 104 })]
	public Box(Header header)
	{
		this.header = header;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 98, 104, 105, 136, 123, 117, 111 })]
	public virtual void write(ByteBuffer buf)
	{
		ByteBuffer dup = buf.duplicate();
		NIOUtils.skip(buf, 8);
		doWrite(buf);
		header.setBodySize(buf.position() - dup.position() - 8);
		Preconditions.checkState(header.headerSize() == 8u);
		header.write(dup);
	}

	public abstract int estimateSize();

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 162, 103, 104 })]
	public override string toString()
	{
		StringBuilder sb = new StringBuilder();
		dump(sb);
		string result = sb.toString();
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(70)]
	public static Box terminatorAtom()
	{
		
		LeafBox result = createLeafBox(new Header(Platform.stringFromBytes(new byte[4])), ByteBuffer.allocate(0));
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(74)]
	public static string[] path(string path)
	{
		string[] result = StringUtils.splitC(path, '.');
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 66, 137, 111, 104, 131 })]
	public static Box parseBox(ByteBuffer input, Header childAtom, IBoxFactory factory)
	{
		Box box = factory.newBox(childAtom);
		if (childAtom.getBodySize() < 134217728u)
		{
			box.parse(input);
			return box;
		}
		LeafBox result = new LeafBox(Header.createHeader("free", 8L));
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Lorg/jcodec/containers/mp4/boxes/Box;>(Ljava/lang/Class<TT;>;Lorg/jcodec/containers/mp4/boxes/Box;)TT;")]
	[LineNumberTable(new byte[] { 159, 118, 66, 124, 115, 104, 104, 104, 125, 99 })]
	public static Box asBox(Class class1, Box box)
	{
		//Discarded unreachable code: IL_004c
		java.lang.Exception ex2;
		try
		{
			Box res = (Box)Platform.newInstance(class1, new object[1] { box.getHeader() });
			ByteBuffer buffer = ByteBuffer.allocate((int)box.getHeader().getBodySize());
			box.doWrite(buffer);
			buffer.flip();
			res.parse(buffer);
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
}
