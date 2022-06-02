using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class TrackHeaderBox : FullBox
{
	private int trackId;

	private long duration;

	private float width;

	private float height;

	private long created;

	private long modified;

	private float volume;

	private short layer;

	private long altGroup;

	private int[] matrix;

	[LineNumberTable(new byte[] { 159, 93, 66, 104 })]
	public virtual void setNo(int no)
	{
		trackId = no;
	}

	[LineNumberTable(180)]
	public virtual int[] getMatrix()
	{
		return matrix;
	}

	[LineNumberTable(28)]
	public static string fourcc()
	{
		return "tkhd";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 66, 106 })]
	public TrackHeaderBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(93)]
	private float readVolume(ByteBuffer input)
	{
		return (float)((double)input.getShort() / 256.0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 162, 110, 104, 47, 135 })]
	private void readMatrix(ByteBuffer input)
	{
		matrix = new int[9];
		for (int i = 0; i < 9; i++)
		{
			matrix[i] = input.getInt();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 66, 127, 0 })]
	private void writeVolume(ByteBuffer @out)
	{
		@out.putShort((short)ByteCodeHelper.d2i((double)volume * 256.0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 98, 116, 48, 135, 117, 41, 135 })]
	private void writeMatrix(ByteBuffer @out)
	{
		for (int j = 0; j < Math.min(9, matrix.Length); j++)
		{
			@out.putInt(matrix[j]);
		}
		for (int i = Math.min(9, matrix.Length); i < 9; i++)
		{
			@out.putInt(0);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 97, 68, 118, 104, 104, 105, 105, 105,
		105, 106, 104, 105, 105
	})]
	public static TrackHeaderBox createTrackHeaderBox(int trackId, long duration, float width, float height, long created, long modified, float volume, short layer, long altGroup, int[] matrix)
	{
		
		TrackHeaderBox box = new TrackHeaderBox(new Header(fourcc()));
		box.trackId = trackId;
		box.duration = duration;
		box.width = width;
		box.height = height;
		box.created = created;
		box.modified = modified;
		box.volume = volume;
		box.layer = layer;
		box.altGroup = altGroup;
		box.matrix = matrix;
		return box;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 66, 136, 106, 114, 148, 115, 147, 109,
		136, 106, 144, 173, 104, 136, 109, 142, 142, 136,
		136, 117, 117
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		if ((sbyte)version == 0)
		{
			created = TimeUtil.fromMovTime(input.getInt());
			modified = TimeUtil.fromMovTime(input.getInt());
		}
		else
		{
			created = TimeUtil.fromMovTime((int)input.getLong());
			modified = TimeUtil.fromMovTime((int)input.getLong());
		}
		trackId = input.getInt();
		input.getInt();
		if ((sbyte)version == 0)
		{
			duration = input.getInt();
		}
		else
		{
			duration = input.getLong();
		}
		input.getInt();
		input.getInt();
		layer = input.getShort();
		altGroup = input.getShort();
		volume = readVolume(input);
		input.getShort();
		readMatrix(input);
		width = (float)input.getInt() / 65536f;
		height = (float)input.getInt() / 65536f;
	}

	[LineNumberTable(97)]
	public virtual int getNo()
	{
		return trackId;
	}

	[LineNumberTable(101)]
	public virtual long getDuration()
	{
		return duration;
	}

	[LineNumberTable(105)]
	public virtual float getWidth()
	{
		return width;
	}

	[LineNumberTable(109)]
	public virtual float getHeight()
	{
		return height;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 114, 98, 136, 115, 147, 110, 137, 143, 105,
		137, 110, 144, 136, 137, 136, 122, 122
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(TimeUtil.toMovTime(created));
		@out.putInt(TimeUtil.toMovTime(modified));
		@out.putInt(trackId);
		@out.putInt(0);
		@out.putInt((int)duration);
		@out.putInt(0);
		@out.putInt(0);
		@out.putShort(layer);
		@out.putShort((short)altGroup);
		writeVolume(@out);
		@out.putShort(0);
		writeMatrix(@out);
		@out.putInt(ByteCodeHelper.f2i(width * 65536f));
		@out.putInt(ByteCodeHelper.f2i(height * 65536f));
	}

	[LineNumberTable(141)]
	public override int estimateSize()
	{
		return 92;
	}

	[LineNumberTable(156)]
	public virtual int getTrackId()
	{
		return trackId;
	}

	[LineNumberTable(160)]
	public virtual long getCreated()
	{
		return created;
	}

	[LineNumberTable(164)]
	public virtual long getModified()
	{
		return modified;
	}

	[LineNumberTable(168)]
	public virtual float getVolume()
	{
		return volume;
	}

	[LineNumberTable(172)]
	public virtual short getLayer()
	{
		return layer;
	}

	[LineNumberTable(176)]
	public virtual long getAltGroup()
	{
		return altGroup;
	}

	[LineNumberTable(new byte[] { 159, 96, 66, 105 })]
	public virtual void setWidth(float width)
	{
		this.width = width;
	}

	[LineNumberTable(new byte[] { 159, 95, 66, 105 })]
	public virtual void setHeight(float height)
	{
		this.height = height;
	}

	[LineNumberTable(new byte[] { 159, 94, 66, 104 })]
	public virtual void setDuration(long duration)
	{
		this.duration = duration;
	}

	[LineNumberTable(199)]
	public virtual bool isOrientation0()
	{
		return (matrix != null && matrix[0] == 65536 && matrix[4] == 65536) ? true : false;
	}

	[LineNumberTable(200)]
	public virtual bool isOrientation90()
	{
		return (matrix != null && matrix[1] == 65536 && matrix[3] == -65536) ? true : false;
	}

	[LineNumberTable(201)]
	public virtual bool isOrientation180()
	{
		return (matrix != null && matrix[0] == -65536 && matrix[4] == -65536) ? true : false;
	}

	[LineNumberTable(202)]
	public virtual bool isOrientation270()
	{
		return (matrix != null && matrix[1] == -65536 && matrix[3] == 65536) ? true : false;
	}
}
