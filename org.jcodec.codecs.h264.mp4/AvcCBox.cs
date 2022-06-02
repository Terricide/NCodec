using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.codecs.h264.mp4;

public class AvcCBox : Box
{
	private int profile;

	private int profileCompat;

	private int level;

	private int nalLengthSize;

	[Signature("Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	private List spsList;

	[Signature("Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	private List ppsList;

	[LineNumberTable(146)]
	public virtual int getNalLengthSize()
	{
		return nalLengthSize;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(IIIILjava/util/List<Ljava/nio/ByteBuffer;>;Ljava/util/List<Ljava/nio/ByteBuffer;>;)Lorg/jcodec/codecs/h264/mp4/AvcCBox;")]
	[LineNumberTable(new byte[] { 159, 129, 130, 118, 104, 104, 104, 104, 105, 105 })]
	public static AvcCBox createAvcCBox(int profile, int profileCompat, int level, int nalLengthSize, List spsList, List ppsList)
	{
		AvcCBox avcc = new AvcCBox(new Header(fourcc()));
		avcc.profile = profile;
		avcc.profileCompat = profileCompat;
		avcc.level = level;
		avcc.nalLengthSize = nalLengthSize;
		avcc.spsList = spsList;
		avcc.ppsList = ppsList;
		return avcc;
	}

	[Signature("()Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(138)]
	public virtual List getSpsList()
	{
		return spsList;
	}

	[Signature("()Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(142)]
	public virtual List getPpsList()
	{
		return ppsList;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 162, 105, 111, 111, 111, 137, 122, 127,
		2, 113, 106, 104, 131, 116, 127, 2, 114, 106,
		104, 99
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.put(1);
		@out.put((byte)(sbyte)profile);
		@out.put((byte)(sbyte)profileCompat);
		@out.put((byte)(sbyte)level);
		@out.put(byte.MaxValue);
		@out.put((byte)(sbyte)((uint)spsList.size() | 0xE0u));
		Iterator iterator = spsList.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer sps = (ByteBuffer)iterator.next();
			@out.putShort((short)(sps.remaining() + 1));
			@out.put(103);
			NIOUtils.write(@out, sps);
		}
		@out.put((byte)(sbyte)ppsList.size());
		Iterator iterator2 = ppsList.iterator();
		while (iterator2.hasNext())
		{
			ByteBuffer pps = (ByteBuffer)iterator2.next();
			@out.putShort((sbyte)(pps.remaining() + 1));
			@out.put(104);
			NIOUtils.write(@out, pps);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 162, 118, 104 })]
	public static AvcCBox parseAvcCBox(ByteBuffer buf)
	{
		AvcCBox avcCBox = new AvcCBox(new Header(fourcc()));
		avcCBox.parse(buf);
		return avcCBox;
	}

	[LineNumberTable(39)]
	public static string fourcc()
	{
		return "avcC";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 106, 108, 108 })]
	public AvcCBox(Header header)
		: base(header)
	{
		spsList = new ArrayList();
		ppsList = new ArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 130, 105, 116, 116, 116, 111, 140, 140,
		103, 104, 116, 246, 61, 231, 70, 112, 106, 105,
		116, 247, 61, 233, 69
	})]
	public override void parse(ByteBuffer input)
	{
		NIOUtils.skip(input, 1);
		profile = (sbyte)input.get() & 0xFF;
		profileCompat = (sbyte)input.get() & 0xFF;
		level = (sbyte)input.get() & 0xFF;
		int flags = (sbyte)input.get() & 0xFF;
		nalLengthSize = (flags & 3) + 1;
		int nSPS = (sbyte)input.get() & 0x1F;
		for (int j = 0; j < nSPS; j++)
		{
			int spsSize = input.getShort();
			Preconditions.checkState(39 == ((sbyte)input.get() & 0x3F));
			spsList.add(NIOUtils.read(input, spsSize - 1));
		}
		int nPPS = (sbyte)input.get() & 0xFF;
		for (int i = 0; i < nPPS; i++)
		{
			int ppsSize = input.getShort();
			Preconditions.checkState(40 == ((sbyte)input.get() & 0x3F));
			ppsList.add(NIOUtils.read(input, ppsSize - 1));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(49)]
	public static AvcCBox createEmpty()
	{
		AvcCBox result = new AvcCBox(new Header(fourcc()));
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 130, 100, 127, 2, 108, 131, 127, 3,
		109, 99
	})]
	public override int estimateSize()
	{
		int sz = 17;
		Iterator iterator = spsList.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer sps = (ByteBuffer)iterator.next();
			sz += 3 + sps.remaining();
		}
		Iterator iterator2 = ppsList.iterator();
		while (iterator2.hasNext())
		{
			ByteBuffer pps = (ByteBuffer)iterator2.next();
			sz += 3 + pps.remaining();
		}
		return sz;
	}

	[LineNumberTable(126)]
	public virtual int getProfile()
	{
		return profile;
	}

	[LineNumberTable(130)]
	public virtual int getProfileCompat()
	{
		return profileCompat;
	}

	[LineNumberTable(134)]
	public virtual int getLevel()
	{
		return level;
	}
}
