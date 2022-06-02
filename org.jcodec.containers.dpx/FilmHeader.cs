using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.containers.dpx;

public class FilmHeader : Object
{
	public string idCode;

	public string type;

	public string offset;

	public string prefix;

	public string count;

	public string format;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(3)]
	public FilmHeader()
	{
	}
}
