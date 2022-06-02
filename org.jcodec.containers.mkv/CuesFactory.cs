using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.containers.mkv.boxes;
using org.jcodec.containers.mkv.util;

namespace org.jcodec.containers.mkv;

public class CuesFactory : Object
{
	public class CuePointMock : Object
	{
		public int cueClusterPositionSize;

		public long elementOffset;

		private long timecode;

		private long size;

		private byte[] id;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 103, 130, 103, 104, 104, 104 })]
		public static CuePointMock doMake(byte[] id, long timecode, long size)
		{
			CuePointMock mock = new CuePointMock();
			mock.id = id;
			mock.timecode = timecode;
			mock.size = size;
			return mock;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(143)]
		public CuePointMock()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 104, 66, 120, 110 })]
		public static CuePointMock make(EbmlMaster c)
		{
			MKVType[] path = new MKVType[2]
			{
				MKVType.___003C_003ECluster,
				MKVType.___003C_003ETimecode
			};
			EbmlUint tc = (EbmlUint)MKVType.findFirst(c, path);
			CuePointMock result = doMake(c.id, tc.getUint(), c.size());
			
			return result;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(143)]
		internal static long access_0024000(CuePointMock x0)
		{
			return x0.size;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(143)]
		internal static long access_0024100(CuePointMock x0)
		{
			return x0.timecode;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(143)]
		internal static byte[] access_0024200(CuePointMock x0)
		{
			return x0.id;
		}
	}

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mkv/CuesFactory$CuePointMock;>;")]
	internal List a;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private long offsetBase;

	private long currentDataOffset;

	private long videoTrackNr;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 162, 104, 131, 99, 127, 5, 113, 110,
		127, 18, 127, 42, 111, 101, 99, 99, 107, 241,
		73, 102, 103
	})]
	public virtual int computeCuesSize()
	{
		int cuesSize = estimateSize();
		int reindex = 0;
		do
		{
			reindex = 0;
			Iterator iterator = a.iterator();
			while (iterator.hasNext())
			{
				CuePointMock z = (CuePointMock)iterator.next();
				int minByteSize = EbmlUint.calculatePayloadSize(z.elementOffset + cuesSize);
				if (minByteSize > z.cueClusterPositionSize)
				{
					java.lang.System.@out.println(new StringBuilder().append(minByteSize).append(">").append(z.cueClusterPositionSize)
						.toString());
					java.lang.System.err.println(new StringBuilder().append("Size ").append(cuesSize).append(" seems too small for element ")
						.append(EbmlUtil.toHexString(CuePointMock.access_0024200(z)))
						.append(" increasing size by one.")
						.toString());
					z.cueClusterPositionSize++;
					cuesSize++;
					reindex = 1;
					break;
				}
				if (minByteSize < z.cueClusterPositionSize)
				{
					
					throw new RuntimeException("Downsizing the index is not well thought through");
				}
			}
		}
		while (reindex != 0);
		return cuesSize;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 111, 98, 99, 127, 2, 159, 13, 119 })]
	public virtual int estimateSize()
	{
		int s = 0;
		Iterator iterator = a.iterator();
		while (iterator.hasNext())
		{
			CuePointMock cpm = (CuePointMock)iterator.next();
			s += estimateCuePointSize(EbmlUint.calculatePayloadSize(CuePointMock.access_0024100(cpm)), EbmlUint.calculatePayloadSize(videoTrackNr), EbmlUint.calculatePayloadSize(cpm.elementOffset));
		}
		return (int)(s + ((nint)MKVType.___003C_003ECues.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(s)));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 130, 119, 127, 5, 106, 151, 123 })]
	public static int estimateCuePointSize(int timecodeSizeInBytes, int trackNrSizeInBytes, int clusterPositionSizeInBytes)
	{
		int cueTimeSize = (int)((nint)MKVType.___003C_003ECueTime.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(timecodeSizeInBytes) + timecodeSizeInBytes);
		int cueTrackPositionSize = (int)((nint)MKVType.___003C_003ECueTrack.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(trackNrSizeInBytes) + trackNrSizeInBytes + (nint)MKVType.___003C_003ECueClusterPosition.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(clusterPositionSizeInBytes) + clusterPositionSizeInBytes);
		cueTrackPositionSize = (int)(cueTrackPositionSize + ((nint)MKVType.___003C_003ECueTrackPositions.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(cueTrackPositionSize)));
		return (int)((nint)MKVType.___003C_003ECuePoint.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(cueTimeSize + cueTrackPositionSize) + cueTimeSize + cueTrackPositionSize);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 233, 61, 201, 108, 104, 104, 116 })]
	public CuesFactory(long offset, long videoTrack)
	{
		currentDataOffset = 0L;
		a = new ArrayList();
		offsetBase = offset;
		videoTrackNr = videoTrack;
		currentDataOffset += offsetBase;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 66, 109, 104, 116, 110 })]
	public virtual void addFixedSize(CuePointMock z)
	{
		z.elementOffset = currentDataOffset;
		z.cueClusterPositionSize = 8;
		currentDataOffset += CuePointMock.access_0024000(z);
		a.add(z);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 162, 109, 114, 116, 110 })]
	public virtual void add(CuePointMock z)
	{
		z.elementOffset = currentDataOffset;
		z.cueClusterPositionSize = EbmlUint.calculatePayloadSize(z.elementOffset);
		currentDataOffset += CuePointMock.access_0024000(z);
		a.add(z);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 128, 130, 104, 113, 127, 5, 146, 114, 110,
		138, 146, 114, 110, 138, 114, 113, 117, 127, 64,
		138, 138, 105, 102
	})]
	public virtual EbmlMaster createCues()
	{
		int estimatedSize = computeCuesSize();
		EbmlMaster cues = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ECues);
		Iterator iterator = a.iterator();
		while (iterator.hasNext())
		{
			CuePointMock cpm = (CuePointMock)iterator.next();
			EbmlMaster cuePoint = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ECuePoint);
			EbmlUint cueTime = (EbmlUint)MKVType.createByType(MKVType.___003C_003ECueTime);
			cueTime.setUint(CuePointMock.access_0024100(cpm));
			cuePoint.add(cueTime);
			EbmlMaster cueTrackPositions = (EbmlMaster)MKVType.createByType(MKVType.___003C_003ECueTrackPositions);
			EbmlUint cueTrack = (EbmlUint)MKVType.createByType(MKVType.___003C_003ECueTrack);
			cueTrack.setUint(videoTrackNr);
			cueTrackPositions.add(cueTrack);
			EbmlUint cueClusterPosition = (EbmlUint)MKVType.createByType(MKVType.___003C_003ECueClusterPosition);
			cueClusterPosition.setUint(cpm.elementOffset + estimatedSize);
			if (cueClusterPosition.data.limit() != cpm.cueClusterPositionSize)
			{
				java.lang.System.err.println(new StringBuilder().append("estimated size of CueClusterPosition differs from the one actually used. ElementId: ").append(EbmlUtil.toHexString(CuePointMock.access_0024200(cpm))).append(" ")
					.append(cueClusterPosition.getData().limit())
					.append(" vs ")
					.append(cpm.cueClusterPositionSize)
					.toString());
			}
			cueTrackPositions.add(cueClusterPosition);
			cuePoint.add(cueTrackPositions);
			cues.add(cuePoint);
		}
		return cues;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 113, 66, 198, 119 })]
	public virtual int estimateFixedSize(int numberOfClusters)
	{
		int s = 34 * numberOfClusters;
		return (int)(s + ((nint)MKVType.___003C_003ECues.___003C_003Eid.LongLength + EbmlUtil.ebmlLength(s)));
	}
}
