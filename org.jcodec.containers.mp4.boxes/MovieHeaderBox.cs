using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.containers.mp4.boxes;

public class MovieHeaderBox : FullBox
{
	private int timescale;

	private long duration;

	private float rate;

	private float volume;

	private long created;

	private long modified;

	private int[] matrix;

	private int nextTrackId;

	[LineNumberTable(52)]
	public virtual int getTimescale()
	{
		return timescale;
	}

	[LineNumberTable(new byte[] { 159, 121, 66, 104 })]
	public virtual void setTimescale(int newTs)
	{
		timescale = newTs;
	}

	[LineNumberTable(new byte[] { 159, 120, 66, 104 })]
	public virtual void setDuration(long duration)
	{
		this.duration = duration;
	}

	[LineNumberTable(56)]
	public virtual long getDuration()
	{
		return duration;
	}

	[LineNumberTable(60)]
	public virtual int getNextTrackId()
	{
		return nextTrackId;
	}

	[LineNumberTable(new byte[] { 159, 119, 66, 104 })]
	public virtual void setNextTrackId(int nextTrackId)
	{
		this.nextTrackId = nextTrackId;
	}

	[LineNumberTable(30)]
	public static string fourcc()
	{
		return "mvhd";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 66, 106 })]
	public MovieHeaderBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(107)]
	private float readRate(ByteBuffer input)
	{
		return (float)input.getInt() / 65536f;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(103)]
	private float readVolume(ByteBuffer input)
	{
		return (float)input.getShort() / 256f;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 66, 105, 104, 42, 135 })]
	private int[] readMatrix(ByteBuffer input)
	{
		int[] matrix = new int[9];
		for (int i = 0; i < 9; i++)
		{
			matrix[i] = input.getInt();
		}
		return matrix;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 101, 66, 122 })]
	private void writeFixed1616(ByteBuffer @out, float rate)
	{
		@out.putInt(ByteCodeHelper.d2i((double)rate * 65536.0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 102, 66, 123 })]
	private void writeFixed88(ByteBuffer @out, float volume)
	{
		@out.putShort((short)ByteCodeHelper.d2i((double)volume * 256.0));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 98, 116, 48, 135, 117, 41, 135 })]
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
		159, 134, 162, 118, 104, 104, 105, 105, 105, 105,
		105, 105
	})]
	public static MovieHeaderBox createMovieHeaderBox(int timescale, long duration, float rate, float volume, long created, long modified, int[] matrix, int nextTrackId)
	{
		
		MovieHeaderBox mvhd = new MovieHeaderBox(new Header(fourcc()));
		mvhd.timescale = timescale;
		mvhd.duration = duration;
		mvhd.rate = rate;
		mvhd.volume = volume;
		mvhd.created = created;
		mvhd.modified = modified;
		mvhd.matrix = matrix;
		mvhd.nextTrackId = nextTrackId;
		return mvhd;
	}

	[LineNumberTable(64)]
	public virtual float getRate()
	{
		return rate;
	}

	[LineNumberTable(68)]
	public virtual float getVolume()
	{
		return volume;
	}

	[LineNumberTable(72)]
	public virtual long getCreated()
	{
		return created;
	}

	[LineNumberTable(76)]
	public virtual long getModified()
	{
		return modified;
	}

	[LineNumberTable(80)]
	public virtual int[] getMatrix()
	{
		return matrix;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 115, 162, 104, 106, 114, 114, 109, 112, 107,
		115, 115, 109, 143, 145, 110, 110, 106, 110, 106,
		109
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		if ((sbyte)version == 0)
		{
			created = TimeUtil.fromMovTime(input.getInt());
			modified = TimeUtil.fromMovTime(input.getInt());
			timescale = input.getInt();
			duration = input.getInt();
		}
		else
		{
			if ((sbyte)version != 1)
			{
				
				throw new RuntimeException("Unsupported version");
			}
			created = TimeUtil.fromMovTime((int)input.getLong());
			modified = TimeUtil.fromMovTime((int)input.getLong());
			timescale = input.getInt();
			duration = input.getLong();
		}
		rate = readRate(input);
		volume = readVolume(input);
		NIOUtils.skip(input, 10);
		matrix = readMatrix(input);
		NIOUtils.skip(input, 24);
		nextTrackId = input.getInt();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 130, 104, 115, 115, 110, 111, 110, 110,
		111, 104, 111, 110
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(TimeUtil.toMovTime(created));
		@out.putInt(TimeUtil.toMovTime(modified));
		@out.putInt(timescale);
		@out.putInt((int)duration);
		writeFixed1616(@out, rate);
		writeFixed88(@out, volume);
		@out.put(new byte[10]);
		writeMatrix(@out);
		@out.put(new byte[24]);
		@out.putInt(nextTrackId);
	}

	[LineNumberTable(149)]
	public override int estimateSize()
	{
		return 144;
	}
}
