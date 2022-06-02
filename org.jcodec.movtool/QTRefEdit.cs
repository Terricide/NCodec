using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.util;
using org.jcodec.containers.mp4;

namespace org.jcodec.movtool;

public class QTRefEdit : java.lang.Object
{
	internal QTEdit.EditFactory[] ___003C_003Efactories;

	[Modifiers(Modifiers.Protected | Modifiers.Final)]
	protected internal QTEdit.EditFactory[] factories
	{
		[HideFromJava]
		get
		{
			return ___003C_003Efactories;
		}
		[HideFromJava]
		private set
		{
			___003C_003Efactories = value;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 98, 112, 112, 112, 117, 63, 11, 199,
		105
	})]
	protected internal virtual void help()
	{
		java.lang.System.@out.println("Quicktime movie editor");
		java.lang.System.@out.println("Syntax: qtedit <command1> <options> ... <commandN> <options> <movie> <output>");
		java.lang.System.@out.println("Where options:");
		QTEdit.EditFactory[] __003C_003Efactories = ___003C_003Efactories;
		int num = __003C_003Efactories.Length;
		for (int i = 0; i < num; i++)
		{
			QTEdit.EditFactory commandFactory = __003C_003Efactories[i];
			java.lang.System.@out.println(new StringBuilder().append("\t").append(commandFactory.getHelp()).toString());
		}
		java.lang.System.exit(-1);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 162, 105, 104 })]
	public QTRefEdit(QTEdit.EditFactory[] editFactories)
	{
		___003C_003Efactories = editFactories;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 141, 103, 141, 112, 127, 5, 137,
		223, 14, 227, 61, 99, 127, 12, 226, 57, 234,
		76, 107, 99, 102, 105, 112, 135, 105, 112, 135,
		153, 105, 112, 167, 153, 106, 127, 22, 167, 106,
		191, 22, 106, 115, 106, 127, 12
	})]
	public virtual void execute(string[] args)
	{
		LinkedList aa = new LinkedList(Arrays.asList(args));
		LinkedList edits = new LinkedList();
		while (aa.size() > 0)
		{
			int i;
			for (i = 0; i < (nint)___003C_003Efactories.LongLength; i++)
			{
				if (!java.lang.String.instancehelper_equals((string)aa.get(0), ___003C_003Efactories[i].getName()))
				{
					continue;
				}
				aa.remove(0);
				java.lang.Exception ex2;
				try
				{
					((List)edits).add((object)___003C_003Efactories[i].parseArgs(aa));
				}
				catch (System.Exception x)
				{
					java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
					if (ex == null)
					{
						throw;
					}
					ex2 = ex;
					goto IL_0089;
				}
				break;
				IL_0089:
				java.lang.Exception e = ex2;
				java.lang.System.err.println(new StringBuilder().append("ERROR: ").append(Throwable.instancehelper_getMessage(e)).toString());
				return;
			}
			if ((nint)i == (nint)___003C_003Efactories.LongLength)
			{
				break;
			}
		}
		if (aa.size() == 0)
		{
			java.lang.System.err.println("ERROR: A movie file should be specified");
			help();
		}
		if (((List)edits).size() == 0)
		{
			java.lang.System.err.println("ERROR: At least one command should be specified");
			help();
		}
		
		File input = new File((string)aa.remove(0));
		if (aa.size() == 0)
		{
			java.lang.System.err.println("ERROR: A movie output file should be specified");
			help();
		}
		
		File output = new File((string)aa.remove(0));
		if (!input.exists())
		{
			java.lang.System.err.println(new StringBuilder().append("ERROR: Input file '").append(input.getAbsolutePath()).append("' doesn't exist")
				.toString());
			help();
		}
		if (output.exists())
		{
			java.lang.System.err.println(new StringBuilder().append("WARNING: Output file '").append(output.getAbsolutePath()).append("' exist, overwritting")
				.toString());
		}
		MP4Util.Movie @ref = MP4Util.createRefFullMovieFromFile(input);
		new CompoundMP4Edit(edits).apply(@ref.getMoov());
		MP4Util.writeFullMovieToFile(output, @ref);
		java.lang.System.@out.println(new StringBuilder().append("INFO: Created reference file: ").append(output.getAbsolutePath()).toString());
	}
}
