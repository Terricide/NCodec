using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.io;
using java.lang;
using java.util;
using org.jcodec.common.io;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class MovDump : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException", "java.io.FileNotFoundException" })]
	[LineNumberTable(new byte[]
	{
		159, 126, 98, 99, 131, 104, 136, 127, 2, 110,
		125, 137, 133, 103, 42, 131, 99
	})]
	private static void dumpHeader(File headerFile, File source)
	{
		FileChannelWrapper raf = null;
		FileChannelWrapper daos = null;
		try
		{
			raf = NIOUtils.readableChannel(source);
			daos = NIOUtils.writableChannel(headerFile);
			Iterator iterator = MP4Util.getRootAtoms(raf).iterator();
			while (iterator.hasNext())
			{
				MP4Util.Atom atom = (MP4Util.Atom)iterator.next();
				string fourcc = atom.getHeader().getFourcc();
				if (String.instancehelper_equals("moov", fourcc) || String.instancehelper_equals("ftyp", fourcc))
				{
					atom.copy(raf, daos);
				}
			}
		}
		finally
		{
			IOUtils.closeQuietly(raf);
			IOUtils.closeQuietly(daos);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(84)]
	public static string print(File file)
	{
		string result = MP4Util.parseMovie(file).toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 117, 98, 136, 105, 100, 127, 16, 163 })]
	public static string printAtom(File file, string atom)
	{
		MovieBox mov = MP4Util.parseMovie(file);
		Box found = findDeep(mov, atom);
		if (found == null)
		{
			java.lang.System.@out.println(new StringBuilder().append("Atom ").append(atom).append(" not found.")
				.toString());
			return null;
		}
		string result = found.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 120, 66, 127, 2, 111, 99, 105, 110, 100,
		131, 99
	})]
	private static Box findDeep(NodeBox root, string atom)
	{
		Iterator iterator = root.getBoxes().iterator();
		while (iterator.hasNext())
		{
			Box b = (Box)iterator.next();
			if (String.instancehelper_equalsIgnoreCase(atom, b.getFourcc()))
			{
				return b;
			}
			if (b is NodeBox)
			{
				Box res = findDeep((NodeBox)b, atom);
				if (res != null)
				{
					return res;
				}
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(28)]
	public MovDump()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159, 135, 162, 102, 112, 107, 104, 130, 99, 99,
		99, 105, 112, 101, 117, 112, 101, 206, 111, 100,
		168, 100, 147, 106, 101, 143
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 1)
		{
			java.lang.System.@out.println("Syntax: movdump [options] <filename>");
			java.lang.System.@out.println("Options: \n\t-f <filename> save header to a file\n\t-a <atom name> dump only a specific atom\n");
			return;
		}
		int idx = 0;
		File headerFile = null;
		string atom = null;
		while (idx < (nint)args.LongLength)
		{
			if (String.instancehelper_equals("-f", args[idx]))
			{
				idx++;
				
				int num = idx;
				idx++;
				headerFile = new File(args[num]);
				continue;
			}
			if (!String.instancehelper_equals("-a", args[idx]))
			{
				break;
			}
			idx++;
			int num2 = idx;
			idx++;
			atom = args[num2];
		}
		
		File source = new File(args[idx]);
		if (headerFile != null)
		{
			dumpHeader(headerFile, source);
		}
		if (atom == null)
		{
			java.lang.System.@out.println(print(source));
			return;
		}
		string dump = printAtom(source, atom);
		if (dump != null)
		{
			java.lang.System.@out.println(dump);
		}
	}
}
