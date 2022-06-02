using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;

namespace org.jcodec.containers.mp4.boxes;

public class VideoSampleEntry : SampleEntry
{
	private short version;

	private short revision;

	private string vendor;

	private int temporalQual;

	private int spacialQual;

	private short width;

	private short height;

	private float hRes;

	private float vRes;

	private short frameCount;

	private string compressorName;

	private short depth;

	private short clrTbl;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 117, 63, 5 })]
	public static VideoSampleEntry videoSampleEntry(string fourcc, Size size, string encoderName)
	{
		VideoSampleEntry result = createVideoSampleEntry(new Header(fourcc), 0, 0, "jcod", 0, 768, (short)size.getWidth(), (short)size.getHeight(), 72L, 72L, 1, (encoderName == null) ? "jcodec" : encoderName, 24, 1, -1);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 65, 91, 105, 105, 105, 105, 105, 106,
		106, 105, 106, 107, 107, 106, 106, 106, 106
	})]
	public static VideoSampleEntry createVideoSampleEntry(Header atom, short version, short revision, string vendor, int temporalQual, int spacialQual, short width, short height, long hRes, long vRes, short frameCount, string compressorName, short depth, short drefInd, short clrTbl)
	{
		VideoSampleEntry e = new VideoSampleEntry(atom);
		e.drefInd = drefInd;
		e.version = version;
		e.revision = revision;
		e.vendor = vendor;
		e.temporalQual = temporalQual;
		e.spacialQual = spacialQual;
		e.width = width;
		e.height = height;
		e.hRes = hRes;
		e.vRes = vRes;
		e.frameCount = frameCount;
		e.compressorName = compressorName;
		e.depth = depth;
		e.clrTbl = clrTbl;
		return e;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 127, 98, 106 })]
	public VideoSampleEntry(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 136, 109, 109, 110, 109, 141, 109,
		141, 117, 149, 136, 141, 143, 141, 141, 106
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		version = input.getShort();
		revision = input.getShort();
		vendor = NIOUtils.readString(input, 4);
		temporalQual = input.getInt();
		spacialQual = input.getInt();
		width = input.getShort();
		height = input.getShort();
		hRes = (float)input.getInt() / 65536f;
		vRes = (float)input.getInt() / 65536f;
		input.getInt();
		frameCount = input.getShort();
		compressorName = NIOUtils.readPascalStringL(input, 31);
		depth = input.getShort();
		clrTbl = input.getShort();
		parseExtensions(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 119, 162, 136, 110, 110, 117, 110, 142, 110,
		142, 122, 154, 137, 142, 143, 142, 142, 106
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putShort(version);
		@out.putShort(revision);
		@out.put(JCodecUtil2.asciiString(vendor), 0, 4);
		@out.putInt(temporalQual);
		@out.putInt(spacialQual);
		@out.putShort(width);
		@out.putShort(height);
		@out.putInt(ByteCodeHelper.f2i(hRes * 65536f));
		@out.putInt(ByteCodeHelper.f2i(vRes * 65536f));
		@out.putInt(0);
		@out.putShort(frameCount);
		NIOUtils.writePascalStringL(@out, compressorName, 31);
		@out.putShort(depth);
		@out.putShort(clrTbl);
		writeExtensions(@out);
	}

	[LineNumberTable(123)]
	public virtual int getWidth()
	{
		return width;
	}

	[LineNumberTable(127)]
	public virtual int getHeight()
	{
		return height;
	}

	[LineNumberTable(131)]
	public virtual float gethRes()
	{
		return hRes;
	}

	[LineNumberTable(135)]
	public virtual float getvRes()
	{
		return vRes;
	}

	[LineNumberTable(139)]
	public virtual long getFrameCount()
	{
		return frameCount;
	}

	[LineNumberTable(143)]
	public virtual string getCompressorName()
	{
		return compressorName;
	}

	[LineNumberTable(147)]
	public virtual long getDepth()
	{
		return depth;
	}

	[LineNumberTable(151)]
	public virtual string getVendor()
	{
		return vendor;
	}

	[LineNumberTable(155)]
	public virtual short getVersion()
	{
		return version;
	}

	[LineNumberTable(159)]
	public virtual short getRevision()
	{
		return revision;
	}

	[LineNumberTable(163)]
	public virtual int getTemporalQual()
	{
		return temporalQual;
	}

	[LineNumberTable(167)]
	public virtual int getSpacialQual()
	{
		return spacialQual;
	}

	[LineNumberTable(171)]
	public virtual short getClrTbl()
	{
		return clrTbl;
	}
}
