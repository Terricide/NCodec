using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.containers.dpx;

public class FileHeader : Object
{
	public int magic;

	public int imageOffset;

	public string version;

	public int ditto;

	public string filename;

	public Date created;

	public int filesize;

	public string creator;

	public string projectName;

	public string copyright;

	public int encKey;

	public int genericHeaderLength;

	public int industryHeaderLength;

	public int userHeaderLength;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(5)]
	public FileHeader()
	{
	}
}
