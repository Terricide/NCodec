using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class CompoundMP4Edit : Object, MP4Edit
{
	[Signature("Ljava/util/List<Lorg/jcodec/movtool/MP4Edit;>;")]
	private List edits;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 105, 104 })]
	public CompoundMP4Edit(List edits)
	{
		this.edits = edits;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 127, 2, 105, 99 })]
	public virtual void applyToFragment(MovieBox mov, MovieFragmentBox[] fragmentBox)
	{
		Iterator iterator = edits.iterator();
		while (iterator.hasNext())
		{
			MP4Edit command = (MP4Edit)iterator.next();
			command.applyToFragment(mov, fragmentBox);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 127, 2, 104, 99 })]
	public virtual void apply(MovieBox mov)
	{
		Iterator iterator = edits.iterator();
		while (iterator.hasNext())
		{
			MP4Edit command = (MP4Edit)iterator.next();
			command.apply(mov);
		}
	}
}
