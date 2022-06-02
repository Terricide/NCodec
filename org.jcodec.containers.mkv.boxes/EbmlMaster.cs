using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.containers.mkv.util;

namespace org.jcodec.containers.mkv.boxes;

public class EbmlMaster : EbmlBase
{
	protected internal long usedSize;

	[Signature("Ljava/util/ArrayList<Lorg/jcodec/containers/mkv/boxes/EbmlBase;>;")]
	internal ArrayList ___003C_003Echildren;

	internal static byte[] ___003C_003ECLUSTER_ID;

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public ArrayList children
	{
		[HideFromJava]
		get
		{
			return ___003C_003Echildren;
		}
		[HideFromJava]
		private set
		{
			___003C_003Echildren = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static byte[] CLUSTER_ID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ECLUSTER_ID;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 130, 118, 137, 100, 127, 2, 108 })]
	protected internal virtual long getDataLen()
	{
		if (___003C_003Echildren == null || ___003C_003Echildren.isEmpty())
		{
			return dataLen;
		}
		long dataLength = 0L;
		Iterator iterator = ___003C_003Echildren.iterator();
		while (iterator.hasNext())
		{
			EbmlBase e = (EbmlBase)iterator.next();
			dataLength += e.size();
		}
		return dataLength;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 106, 108, 104 })]
	public EbmlMaster(byte[] id)
		: base(id)
	{
		___003C_003Echildren = new ArrayList();
		base.id = id;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 100, 130, 104, 142 })]
	public virtual void add(EbmlBase elem)
	{
		if (elem != null)
		{
			elem.parent = this;
			___003C_003Echildren.add(elem);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 132, 162, 136, 106, 127, 65, 153, 110, 142,
		113, 62, 199, 136
	})]
	public override ByteBuffer getData()
	{
		long size = getDataLen();
		if (size > 2147483647u)
		{
			java.lang.System.@out.println(new StringBuilder().append("EbmlMaster.getData: id.length ").append(id.Length).append("  EbmlUtil.ebmlLength(")
				.append(size)
				.append("): ")
				.append(EbmlUtil.ebmlLength(size))
				.append(" size: ")
				.append(size)
				.toString());
		}
		ByteBuffer bb = ByteBuffer.allocate((int)((nint)id.LongLength + EbmlUtil.ebmlLength(size) + size));
		bb.put(id);
		bb.put(EbmlUtil.ebmlEncode(size));
		for (int i = 0; i < ___003C_003Echildren.size(); i++)
		{
			bb.put(((EbmlBase)___003C_003Echildren.get(i)).getData());
		}
		bb.flip();
		return bb;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 124, 98, 136, 139, 108 })]
	public override long size()
	{
		long size = getDataLen();
		size += EbmlUtil.ebmlLength(size);
		return size + id.LongLength;
	}

	[LineNumberTable(24)]
	static EbmlMaster()
	{
		___003C_003ECLUSTER_ID = new byte[4] { 31, 67, 182, 117 };
	}
}
