using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.util;
using org.jcodec.codecs.aac;
using org.jcodec.codecs.mpeg4.es;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.codecs.mpeg4.mp4;

public class EsdsBox : FullBox
{
	private ByteBuffer streamInfo;

	private int objectType;

	private int bufSize;

	private int maxBitrate;

	private int avgBitrate;

	private int trackId;

	[LineNumberTable(82)]
	public virtual ByteBuffer getStreamInfo()
	{
		return streamInfo;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 162, 118, 104, 104, 104, 105, 105, 104 })]
	public static EsdsBox createEsdsBox(ByteBuffer streamInfo, int objectType, int bufSize, int maxBitrate, int avgBitrate, int trackId)
	{
		
		EsdsBox esds = new EsdsBox(new Header(fourcc()));
		esds.objectType = objectType;
		esds.bufSize = bufSize;
		esds.maxBitrate = maxBitrate;
		esds.avgBitrate = avgBitrate;
		esds.trackId = trackId;
		esds.streamInfo = streamInfo;
		return esds;
	}

	[LineNumberTable(36)]
	public static string fourcc()
	{
		return "esds";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 106 })]
	public EsdsBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 136, 125, 103, 103, 115, 127, 7,
		109, 115, 99, 103, 127, 11, 109, 149
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		if (streamInfo != null && streamInfo.remaining() > 0)
		{
			ArrayList j = new ArrayList();
			ArrayList l1 = new ArrayList();
			l1.add(new DecoderSpecific(streamInfo));
			j.add(new DecoderConfig(objectType, bufSize, maxBitrate, avgBitrate, l1));
			j.add(new SL());
			new ES(trackId, j).write(@out);
		}
		else
		{
			ArrayList i = new ArrayList();
			i.add(new DecoderConfig(objectType, bufSize, maxBitrate, avgBitrate, new ArrayList()));
			i.add(new SL());
			new ES(trackId, i).write(@out);
		}
	}

	[LineNumberTable(64)]
	public override int estimateSize()
	{
		return 64;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 66, 104, 141, 109, 114, 109, 109, 109,
		109, 114, 109
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		ES es = (ES)DescriptorParser.read(input);
		trackId = es.getTrackId();
		DecoderConfig decoderConfig = (DecoderConfig)NodeDescriptor.findByTag(es, DecoderConfig.tag());
		objectType = decoderConfig.getObjectType();
		bufSize = decoderConfig.getBufSize();
		maxBitrate = decoderConfig.getMaxBitrate();
		avgBitrate = decoderConfig.getAvgBitrate();
		DecoderSpecific decoderSpecific = (DecoderSpecific)NodeDescriptor.findByTag(decoderConfig, DecoderSpecific.tag());
		streamInfo = decoderSpecific.getData();
	}

	[LineNumberTable(86)]
	public virtual int getObjectType()
	{
		return objectType;
	}

	[LineNumberTable(90)]
	public virtual int getBufSize()
	{
		return bufSize;
	}

	[LineNumberTable(94)]
	public virtual int getMaxBitrate()
	{
		return maxBitrate;
	}

	[LineNumberTable(98)]
	public virtual int getAvgBitrate()
	{
		return avgBitrate;
	}

	[LineNumberTable(102)]
	public virtual int getTrackId()
	{
		return trackId;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(106)]
	public static EsdsBox fromADTS(ADTSParser.Header hdr)
	{
		EsdsBox result = createEsdsBox(ADTSParser.adtsToStreamInfo(hdr), hdr.getObjectType() << 5, 0, 210750, 133350, 2);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(122)]
	public static EsdsBox newEsdsBox()
	{
		
		EsdsBox result = new EsdsBox(new Header(fourcc()));
		
		return result;
	}
}
