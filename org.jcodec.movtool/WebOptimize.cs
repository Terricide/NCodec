using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using org.jcodec.containers.mp4;

namespace org.jcodec.movtool;

public class WebOptimize : java.lang.Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 127, 18, 105, 131, 127, 38, 137 })]
	public static File hidFile(File tgt)
	{
		
		File src = new File(tgt.getParentFile(), new StringBuilder().append(".").append(tgt.getName()).toString());
		if (src.exists())
		{
			int i = 1;
			do
			{
				
				File parentFile = tgt.getParentFile();
				StringBuilder stringBuilder = new StringBuilder().append(".").append(tgt.getName()).append(".");
				int i2 = i;
				i++;
				src = new File(parentFile, stringBuilder.append(i2).toString());
			}
			while (src.exists());
		}
		return src;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public WebOptimize()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159,
		138,
		130,
		102,
		112,
		135,
		111,
		104,
		169,
		136,
		byte.MaxValue,
		0,
		69,
		227,
		60,
		99,
		104,
		127,
		24,
		137
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 1)
		{
			java.lang.System.@out.println("Syntax: optimize <movie>");
			java.lang.System.exit(-1);
		}
		
		File tgt = new File(args[0]);
		File src = hidFile(tgt);
		tgt.renameTo(src);
		System.Exception ex;
		try
		{
			MP4Util.Movie movie = MP4Util.createRefFullMovieFromFile(src);
			new Flatten().flatten(movie, tgt);
			return;
		}
		catch (System.Exception x)
		{
			ex = ByteCodeHelper.MapException<System.Exception>(x, ByteCodeHelper.MapFlags.None);
		}
		System.Exception t = ex;
		Throwable.instancehelper_printStackTrace(t);
		
		tgt.renameTo(new File(tgt.getParentFile(), new StringBuilder().append(tgt.getName()).append(".error").toString()));
		src.renameTo(tgt);
	}
}
