using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.containers.mkv.boxes;
using org.jcodec.containers.mkv.util;

namespace org.jcodec.containers.mkv;

public class SeekHeadFactory : Object
{
	public class SeekMock : Object
	{
		public long dataOffset;

		internal byte[] id;

		internal int size;

		internal int seekPointerSize;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(108)]
		public SeekMock()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 114, 162, 103, 109, 110 })]
		public static SeekMock make(EbmlBase e)
		{
			SeekMock z = new SeekMock();
			z.id = e.id;
			z.size = (int)e.size();
			return z;
		}
	}

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/SeekHeadFactory$SeekMock;>;")]
	internal List a;

	internal long currentDataOffset;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 9, 169, 108 })]
	public SeekHeadFactory()
	{
		currentDataOffset = 0L;
		a = new ArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 104, 109, 114, 149, 110 })]
	public virtual void add(EbmlBase e)
	{
		SeekMock z = SeekMock.make(e);
		z.dataOffset = currentDataOffset;
		z.seekPointerSize = EbmlUint.calculatePayloadSize(z.dataOffset);
		currentDataOffset += z.size;
		a.add(z);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 130, 136, 113, 127, 5, 146, 114, 115,
		138, 114, 113, 117, 127, 64, 138, 105, 102, 105,
		107, 159, 28
	})]
	public virtual EbmlMaster indexSeekHead()
	{
		int seekHeadSize = computeSeekHeadSize();
		EbmlMaster seekHead = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ESeekHead);
		Iterator iterator = a.iterator();
		while (iterator.hasNext())
		{
			SeekMock z = (SeekMock)iterator.next();
			EbmlMaster seek = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ESeek);
			EbmlBin seekId = (EbmlBin)MKVType.createByType(MKVType.___003C_003ESeekID);
			seekId.setBuf(ByteBuffer.wrap(z.id));
			seek.add(seekId);
			EbmlUint seekPosition = (EbmlUint)MKVType.createByType(MKVType.___003C_003ESeekPosition);
			seekPosition.setUint(z.dataOffset + seekHeadSize);
			if (seekPosition.data.limit() != z.seekPointerSize)
			{
				java.lang.System.err.println(new StringBuilder().append("estimated size of seekPosition differs from the one actually used. ElementId: ").append(EbmlUtil.toHexString(z.id)).append(" ")
					.append(seekPosition.getData().limit())
					.append(" vs ")
					.append(z.seekPointerSize)
					.toString());
			}
			seek.add(seekPosition);
			seekHead.add(seek);
		}
		ByteBuffer mux = seekHead.getData();
		if (mux.limit() != seekHeadSize)
		{
			java.lang.System.err.println(new StringBuilder().append("estimated size of seekHead differs from the one actually used. ").append(mux.limit()).append(" vs ")
				.append(seekHeadSize)
				.toString());
		}
		return seekHead;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 66, 104, 131, 99, 127, 5, 113, 107,
		127, 42, 111, 101, 99, 99, 107, 145, 102, 103
	})]
	public virtual int computeSeekHeadSize()
	{
		int seekHeadSize = estimateSize();
		int reindex = 0;
		do
		{
			reindex = 0;
			Iterator iterator = a.iterator();
			while (iterator.hasNext())
			{
				SeekMock z = (SeekMock)iterator.next();
				int minSize = EbmlUint.calculatePayloadSize(z.dataOffset + seekHeadSize);
				if (minSize > z.seekPointerSize)
				{
					java.lang.System.@out.println(new StringBuilder().append("Size ").append(seekHeadSize).append(" seems too small for element ")
						.append(EbmlUtil.toHexString(z.id))
						.append(" increasing size by one.")
						.toString());
					z.seekPointerSize++;
					seekHeadSize++;
					reindex = 1;
					break;
				}
				if (minSize < z.seekPointerSize)
				{
					
					throw new RuntimeException("Downsizing the index is not well thought through.");
				}
			}
		}
		while (reindex != 0);
		return seekHeadSize;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 119, 98, 111, 127, 2, 113, 63, 23, 167 })]
	internal virtual int estimateSize()
	{
		int s = (int)((nint)MKVType.___003C_003ESeekHead.___003C_003Eid.LongLength + 1);
		s += estimeteSeekSize(((SeekMock)a.get(0)).id.Length, 1);
		for (int i = 1; i < a.size(); i++)
		{
			s += estimeteSeekSize(((SeekMock)a.get(i)).id.Length, ((SeekMock)a.get(i)).seekPointerSize);
		}
		return s;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 130, 119, 119, 123 })]
	public static int estimeteSeekSize(int idLength, int offsetSizeInBytes)
	{
		int seekIdSize = (int)((nint)MKVType.___003C_003ESeekID.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(idLength) + idLength);
		int seekPositionSize = (int)((nint)MKVType.___003C_003ESeekPosition.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(offsetSizeInBytes) + offsetSizeInBytes);
		return (int)((nint)MKVType.___003C_003ESeek.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(seekIdSize + seekPositionSize) + seekIdSize + seekPositionSize);
	}
}
