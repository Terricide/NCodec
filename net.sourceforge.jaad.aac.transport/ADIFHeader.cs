using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using net.sourceforge.jaad.aac.syntax;

namespace net.sourceforge.jaad.aac.transport;

public class ADIFHeader : Object
{
	private const long ADIF_ID = 1094994246L;

	private long id;

	private bool copyrightIDPresent;

	private byte[] copyrightID;

	private bool originalCopy;

	private bool home;

	private bool bitstreamType;

	private int bitrate;

	private int pceCount;

	private int[] adifBufferFullness;

	private PCE[] pces;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(27)]
	public static bool isPresent(IBitStream _in)
	{
		return _in.peekBits(32) == 1094994246u;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[] { 159, 134, 162, 103, 104 })]
	public static ADIFHeader readHeader(IBitStream _in)
	{
		ADIFHeader h = new ADIFHeader();
		h.decode(_in);
		return h;
	}

	[LineNumberTable(65)]
	public virtual PCE getFirstPCE()
	{
		return pces[0];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 130, 105, 110 })]
	private ADIFHeader()
	{
		copyrightID = new byte[9];
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159, 132, 130, 112, 109, 105, 104, 49, 199, 109,
		109, 109, 111, 112, 114, 114, 108, 116, 113, 110,
		239, 60, 231, 70
	})]
	private void decode(IBitStream _in)
	{
		id = _in.readBits(32);
		copyrightIDPresent = _in.readBool();
		if (copyrightIDPresent)
		{
			for (int j = 0; j < 9; j++)
			{
				copyrightID[j] = (byte)(sbyte)_in.readBits(8);
			}
		}
		originalCopy = _in.readBool();
		home = _in.readBool();
		bitstreamType = _in.readBool();
		bitrate = _in.readBits(23);
		pceCount = _in.readBits(4) + 1;
		pces = new PCE[pceCount];
		adifBufferFullness = new int[pceCount];
		for (int i = 0; i < pceCount; i++)
		{
			if (bitstreamType)
			{
				adifBufferFullness[i] = -1;
			}
			else
			{
				adifBufferFullness[i] = _in.readBits(20);
			}
			pces[i] = new PCE();
			pces[i].decode(_in);
		}
	}
}
