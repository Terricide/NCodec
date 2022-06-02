using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class SyncSamplesBox : FullBox
{
	public const string STSS = "stss";

	protected internal int[] syncSamples;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 106 })]
	public SyncSamplesBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 113, 104 })]
	public static SyncSamplesBox createSyncSamplesBox(int[] array)
	{
		SyncSamplesBox stss = new SyncSamplesBox(new Header("stss"));
		stss.syncSamples = array;
		return stss;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 104, 104, 109, 103, 47, 167 })]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		int len = input.getInt();
		syncSamples = new int[len];
		for (int i = 0; i < len; i++)
		{
			syncSamples[i] = input.getInt();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 130, 104, 111, 109, 48, 135 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt(syncSamples.Length);
		for (int i = 0; i < (nint)syncSamples.LongLength; i++)
		{
			@out.putInt(syncSamples[i]);
		}
	}

	[LineNumberTable(46)]
	public override int estimateSize()
	{
		return (int)(16 + (nint)syncSamples.LongLength * 4);
	}

	[LineNumberTable(50)]
	public virtual int[] getSyncSamples()
	{
		return syncSamples;
	}
}
