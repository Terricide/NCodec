using System.Runtime.CompilerServices;
using IKVM.Attributes;

namespace net.sourceforge.jaad.aac.syntax;

internal class DSE : Element
{
	private byte[] dataStreamBytes;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 98, 105 })]
	internal DSE()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 137, 98, 104, 105, 147, 138, 109, 103, 49,
		167
	})]
	internal virtual void decode(IBitStream _in)
	{
		int byteAlign = (_in.readBool() ? 1 : 0);
		int count = _in.readBits(8);
		if (count == 255)
		{
			count += _in.readBits(8);
		}
		if (byteAlign != 0)
		{
			_in.byteAlign();
		}
		dataStreamBytes = new byte[count];
		for (int i = 0; i < count; i++)
		{
			dataStreamBytes[i] = (byte)(sbyte)_in.readBits(8);
		}
	}
}
