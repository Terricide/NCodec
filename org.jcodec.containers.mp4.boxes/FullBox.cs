using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public abstract class FullBox : Box
{
	protected internal byte version;

	protected internal int flags;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 106 })]
	public FullBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 104, 114, 110 })]
	public override void parse(ByteBuffer input)
	{
		int vf = input.getInt();
		version = (byte)(sbyte)((uint)(vf >> 24) & 0xFFu);
		flags = vf & 0xFFFFFF;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 127, 0 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(((sbyte)version << 24) | (flags & 0xFFFFFF));
	}

	[LineNumberTable(34)]
	public virtual byte getVersion()
	{
		return (byte)(sbyte)version;
	}

	[LineNumberTable(38)]
	public virtual int getFlags()
	{
		return flags;
	}

	[LineNumberTable(new byte[] { 159, 132, 129, 68, 104 })]
	public virtual void setVersion(byte version)
	{
		int version2 = (sbyte)version;
		this.version = (byte)version2;
	}

	[LineNumberTable(new byte[] { 159, 131, 130, 104 })]
	public virtual void setFlags(int flags)
	{
		this.flags = flags;
	}
}
