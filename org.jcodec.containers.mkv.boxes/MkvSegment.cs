using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.containers.mkv.util;
using org.jcodec.platform;

namespace org.jcodec.containers.mkv.boxes;

public class MkvSegment : EbmlMaster
{
	internal int headerSize;

	internal static byte[] ___003C_003ESEGMENT_ID;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static byte[] SEGMENT_ID
	{
		[HideFromJava]
		get
		{
			return ___003C_003ESEGMENT_ID;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 234, 60, 232, 69 })]
	public MkvSegment(byte[] id)
		: base(id)
	{
		headerSize = 0;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 98, 106, 112, 121, 127, 2, 120, 131,
		106, 163
	})]
	public virtual long getHeaderSize()
	{
		long returnValue = id.LongLength;
		returnValue += EbmlUtil.ebmlLength(getDataLen());
		if (___003C_003Echildren != null && !___003C_003Echildren.isEmpty())
		{
			Iterator iterator = ___003C_003Echildren.iterator();
			while (iterator.hasNext())
			{
				EbmlBase e = (EbmlBase)iterator.next();
				if (!Platform.arrayEqualsByte(EbmlMaster.___003C_003ECLUSTER_ID, e.type.___003C_003Eid))
				{
					returnValue += e.size();
				}
			}
		}
		return returnValue;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(32)]
	public static MkvSegment createMkvSegment()
	{
		MkvSegment result = new MkvSegment(___003C_003ESEGMENT_ID);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 66, 136, 106, 127, 81, 137, 110, 147,
		153, 127, 2, 120, 131, 110, 163, 136
	})]
	public virtual ByteBuffer getHeader()
	{
		long headerSize = getHeaderSize();
		if (headerSize > 2147483647u)
		{
			java.lang.System.@out.println(new StringBuilder().append("MkvSegment.getHeader: id.length ").append(id.Length).append("  Element.getEbmlSize(")
				.append(dataLen)
				.append("): ")
				.append(EbmlUtil.ebmlLength(dataLen))
				.append(" size: ")
				.append(dataLen)
				.toString());
		}
		ByteBuffer bb = ByteBuffer.allocate((int)headerSize);
		bb.put(id);
		bb.put(EbmlUtil.ebmlEncode(getDataLen()));
		if (___003C_003Echildren != null && !___003C_003Echildren.isEmpty())
		{
			Iterator iterator = ___003C_003Echildren.iterator();
			while (iterator.hasNext())
			{
				EbmlBase e = (EbmlBase)iterator.next();
				if (!Platform.arrayEqualsByte(EbmlMaster.___003C_003ECLUSTER_ID, e.type.___003C_003Eid))
				{
					bb.put(e.getData());
				}
			}
		}
		bb.flip();
		return bb;
	}

	[LineNumberTable(25)]
	static MkvSegment()
	{
		EbmlMaster.___003Cclinit_003E();
		___003C_003ESEGMENT_ID = new byte[4] { 24, 83, 128, 103 };
	}
}
