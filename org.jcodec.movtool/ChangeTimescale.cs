using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.io;
using java.lang;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class ChangeTimescale : Object
{
	[SpecialName]
	[EnclosingMethod(null, "main1", "([Ljava.lang.String;)V")]
	internal class _1 : Object, MP4Edit
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal int val_0024ts;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(33)]
		internal _1(int i)
		{
			val_0024ts = i;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 134, 162, 104, 124, 136, 106, 223, 38, 141,
			111
		})]
		public virtual void apply(MovieBox mov)
		{
			TrakBox vt = mov.getVideoTrack();
			MediaHeaderBox mdhd = (MediaHeaderBox)NodeBox.findFirstPath(vt, ClassLiteral<MediaHeaderBox>.Value, Box.path("mdia.mdhd"));
			int oldTs = mdhd.getTimescale();
			if (oldTs > val_0024ts)
			{
				string message = new StringBuilder().append("Old timescale (").append(oldTs).append(") is greater then new timescale (")
					.append(val_0024ts)
					.append("), not touching.")
					.toString();
				
				throw new RuntimeException(message);
			}
			vt.fixMediaTimescale(val_0024ts);
			mov.fixTimescale(val_0024ts);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(51)]
		public virtual void applyToFragment(MovieBox mov, MovieFragmentBox[] fragmentBox)
		{
			
			throw new RuntimeException("Unsupported");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(22)]
	public ChangeTimescale()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159,
		136,
		66,
		102,
		112,
		135,
		106,
		105,
		112,
		135,
		byte.MaxValue,
		0,
		85
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 2)
		{
			java.lang.System.@out.println("Syntax: chts <movie> <timescale>");
			java.lang.System.exit(-1);
		}
		int ts = Integer.parseInt(args[1]);
		if (ts < 600)
		{
			java.lang.System.@out.println("Could not set timescale < 600");
			java.lang.System.exit(-1);
		}
		InplaceMP4Editor inplaceMP4Editor = new InplaceMP4Editor();
		
		inplaceMP4Editor.modify(new File(args[0]), new _1(ts));
	}
}
