using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.util;
using org.jcodec.common.io;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class Undo : java.lang.Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(35)]
	public Undo()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Ljava/lang/String;)Ljava/util/List<Lorg/jcodec/containers/mp4/MP4Util$Atom;>;")]
	[LineNumberTable(new byte[]
	{
		159, 125, 130, 103, 131, 109, 99, 127, 6, 127,
		5, 138, 121, 106, 131, 136, 74, 99, 99
	})]
	private List list(string fileName)
	{
		ArrayList result = new ArrayList();
		FileChannelWrapper @is = null;
		try
		{
			@is = NIOUtils.readableChannel(new File(fileName));
			int version = 0;
			Iterator iterator = MP4Util.getRootAtoms(@is).iterator();
			while (iterator.hasNext())
			{
				MP4Util.Atom atom = (MP4Util.Atom)iterator.next();
				if (java.lang.String.instancehelper_equals("free", atom.getHeader().getFourcc()) && isMoov(@is, atom))
				{
					result.add(atom);
				}
				if (java.lang.String.instancehelper_equals("moov", atom.getHeader().getFourcc()))
				{
					result.add(atom);
					return result;
				}
			}
			return result;
		}
		finally
		{
			IOUtils.closeQuietly(@is);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 129, 66, 105, 106, 112, 130, 131, 119, 127,
		3, 127, 2, 127, 3, 159, 6, 74, 99, 99
	})]
	private void undo(string fineName)
	{
		List versions = list(fineName);
		if (versions.size() < 2)
		{
			java.lang.System.err.println("Nowhere to rollback.");
			return;
		}
		RandomAccessFile raf = null;
		try
		{
			RandomAccess
			raf = new RandomAccessFile(new File(fineName), "rw");
			raf.seek(((MP4Util.Atom)versions.get(versions.size() - 2)).getOffset() + 4u);
			raf.write(new byte[4] { 109, 111, 111, 118 });
			raf.seek(((MP4Util.Atom)versions.get(versions.size() - 1)).getOffset() + 4u);
			raf.write(new byte[4] { 102, 114, 101, 101 });
		}
		finally
		{
			IOUtils.closeQuietly(raf);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 120, 162, 154, 121, 43, 102, 38, 135, 127,
		22, 98
	})]
	private bool isMoov(SeekableByteChannel @is, MP4Util.Atom atom)
	{
		//Discarded unreachable code: IL_0073
		@is.setPosition(atom.getOffset() + atom.getHeader().headerSize());
		System.Exception ex;
		try
		{
			Box mov = BoxUtil.parseBox(NIOUtils.fetchFromChannel(@is, (int)atom.getHeader().getSize()), Header.createHeader("moov", atom.getHeader().getSize()), BoxFactory.getDefault());
			return (mov is MovieBox && BoxUtil.containsBox((NodeBox)mov, "mvhd")) ? true : false;
		}
		catch (System.Exception x)
		{
			ex = ByteCodeHelper.MapException<System.Exception>(x, ByteCodeHelper.MapFlags.None);
		}
		System.Exception t = ex;
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 102, 112, 112, 135, 103, 112, 107,
		127, 13, 99, 140
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 1)
		{
			java.lang.System.err.println("Syntax: qt-undo [-l] <movie>");
			java.lang.System.err.println("\t-l\t\tList all the previous versions of this movie.");
			java.lang.System.exit(-1);
		}
		Undo undo = new Undo();
		if (java.lang.String.instancehelper_equals("-l", args[0]))
		{
			List list = undo.list(args[1]);
			java.lang.System.@out.println(new StringBuilder().append(list.size() - 1).append(" versions.").toString());
		}
		else
		{
			undo.undo(args[0]);
		}
	}
}
