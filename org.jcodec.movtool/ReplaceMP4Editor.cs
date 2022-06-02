using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using org.jcodec.containers.mp4;

namespace org.jcodec.movtool;

public class ReplaceMP4Editor : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(24)]
	public ReplaceMP4Editor()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 136, 162, 110, 100, 107 })]
	public virtual void modifyOrReplace(File src, MP4Edit edit)
	{
		if (!new InplaceMP4Editor().modify(src, edit))
		{
			replace(src, edit);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 134, 98, 127, 18, 106, 105 })]
	public virtual void replace(File src, MP4Edit edit)
	{
		
		File tmp = new File(src.getParentFile(), new StringBuilder().append(".").append(src.getName()).toString());
		copy(src, tmp, edit);
		tmp.renameTo(src);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 133, 162, 104, 109, 135, 107 })]
	public virtual void copy(File src, File dst, MP4Edit edit)
	{
		MP4Util.Movie movie = MP4Util.createRefFullMovieFromFile(src);
		edit.apply(movie.getMoov());
		Flatten fl = new Flatten();
		fl.flatten(movie, dst);
	}
}
