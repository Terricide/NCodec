using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;

namespace org.jcodec.containers.mp4.boxes;

public class LoadSettingsBox : Box
{
	private int preloadStartTime;

	private int preloadDuration;

	private int preloadFlags;

	private int defaultHints;

	[LineNumberTable(21)]
	public static string fourcc()
	{
		return "load";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 98, 106 })]
	public LoadSettingsBox(Header header)
		: base(header)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 109, 109, 109, 109 })]
	public override void parse(ByteBuffer input)
	{
		preloadStartTime = input.getInt();
		preloadDuration = input.getInt();
		preloadFlags = input.getInt();
		defaultHints = input.getInt();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 110, 110, 110, 110 })]
	protected internal override void doWrite(ByteBuffer @out)
	{
		@out.putInt(preloadStartTime);
		@out.putInt(preloadDuration);
		@out.putInt(preloadFlags);
		@out.putInt(defaultHints);
	}

	[LineNumberTable(44)]
	public override int estimateSize()
	{
		return 24;
	}

	[LineNumberTable(48)]
	public virtual int getPreloadStartTime()
	{
		return preloadStartTime;
	}

	[LineNumberTable(52)]
	public virtual int getPreloadDuration()
	{
		return preloadDuration;
	}

	[LineNumberTable(56)]
	public virtual int getPreloadFlags()
	{
		return preloadFlags;
	}

	[LineNumberTable(60)]
	public virtual int getDefaultHints()
	{
		return defaultHints;
	}
}
