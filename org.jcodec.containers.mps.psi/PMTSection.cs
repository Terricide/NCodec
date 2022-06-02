using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.io;

namespace org.jcodec.containers.mps.psi;

public class PMTSection : PSISection
{
	public class PMTStream : Object
	{
		private int streamTypeTag;

		private int pid;

		[Signature("Ljava/util/List<Lorg/jcodec/containers/mps/MPSUtils$MPEGMediaDescriptor;>;")]
		private List descriptors;

		private MTSStreamType streamType;

		[LineNumberTable(128)]
		public virtual int getPid()
		{
			return pid;
		}

		[LineNumberTable(120)]
		public virtual int getStreamTypeTag()
		{
			return streamTypeTag;
		}

		[Signature("()Ljava/util/List<Lorg/jcodec/containers/mps/MPSUtils$MPEGMediaDescriptor;>;")]
		[LineNumberTable(132)]
		public virtual List getDesctiptors()
		{
			return descriptors;
		}

		[LineNumberTable(124)]
		public virtual MTSStreamType getStreamType()
		{
			return streamType;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 114, 66, 105, 104, 104, 104, 109 })]
		public PMTStream(int streamTypeTag, int pid, List descriptors)
		{
			this.streamTypeTag = streamTypeTag;
			this.pid = pid;
			this.descriptors = descriptors;
			streamType = MTSStreamType.fromTag(streamTypeTag);
		}
	}

	public class Tag : Object
	{
		private int tag;

		private ByteBuffer content;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 119, 66, 105, 104, 104 })]
		public Tag(int tag, ByteBuffer content)
		{
			this.tag = tag;
			this.content = content;
		}

		[LineNumberTable(98)]
		public virtual int getTag()
		{
			return tag;
		}

		[LineNumberTable(102)]
		public virtual ByteBuffer getContent()
		{
			return content;
		}
	}

	private int pcrPid;

	private Tag[] tags;

	private PMTStream[] streams;

	[LineNumberTable(47)]
	public virtual PMTStream[] getStreams()
	{
		return streams;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 162, 136, 110, 137, 110, 138, 112, 104,
		109, 112, 111, 203, 111, 107, 107, 121, 134
	})]
	public static PMTSection parsePMT(ByteBuffer data)
	{
		PSISection psi = PSISection.parsePSI(data);
		int w1 = data.getShort() & 0xFFFF;
		int pcrPid = w1 & 0x1FFF;
		int w2 = data.getShort() & 0xFFFF;
		int programInfoLength = w2 & 0xFFF;
		List tags = parseTags(NIOUtils.read(data, programInfoLength));
		ArrayList streams = new ArrayList();
		while (data.remaining() > 4)
		{
			int streamType = (sbyte)data.get() & 0xFF;
			int wn = data.getShort() & 0xFFFF;
			int elementaryPid = wn & 0x1FFF;
			int wn2 = data.getShort() & 0xFFFF;
			int esInfoLength = wn2 & 0xFFF;
			ByteBuffer read = NIOUtils.read(data, esInfoLength);
			((List)streams).add((object)new PMTStream(streamType, elementaryPid, MPSUtils.parseDescriptors(read)));
		}
		PMTSection result = new PMTSection(psi, pcrPid, (Tag[])tags.toArray(new Tag[0]), (PMTStream[])((List)streams).toArray((object[])new PMTStream[0]));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;)Ljava/util/List<Lorg/jcodec/containers/mps/psi/PMTSection$Tag;>;")]
	[LineNumberTable(new byte[] { 159, 123, 130, 103, 105, 105, 137, 117, 99 })]
	internal static List parseTags(ByteBuffer bb)
	{
		ArrayList tags = new ArrayList();
		while (bb.hasRemaining())
		{
			int tag = (sbyte)bb.get();
			int tagLen = (sbyte)bb.get();
			((List)tags).add((object)new Tag(tag, NIOUtils.read(bb, tagLen)));
		}
		return tags;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 159, 14, 104, 104, 105 })]
	public PMTSection(PSISection psi, int pcrPid, Tag[] tags, PMTStream[] streams)
		: base(psi.tableId, psi.specificId, psi.versionNumber, psi.currentNextIndicator, psi.sectionNumber, psi.lastSectionNumber)
	{
		this.pcrPid = pcrPid;
		this.tags = tags;
		this.streams = streams;
	}

	[LineNumberTable(39)]
	public virtual int getPcrPid()
	{
		return pcrPid;
	}

	[LineNumberTable(43)]
	public virtual Tag[] getTags()
	{
		return tags;
	}
}
