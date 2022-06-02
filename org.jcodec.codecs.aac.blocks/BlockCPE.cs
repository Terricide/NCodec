using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.io;

namespace org.jcodec.codecs.aac.blocks;

public class BlockCPE : BlockICS
{
	private int[] ms_mask;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public new static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 130, 101, 106, 47, 167 })]
	private void decodeMidSideStereo(BitReader _in, int ms_present, int numWindowGroups, int maxSfb)
	{
		if (ms_present == 1)
		{
			for (int idx = 0; idx < numWindowGroups * maxSfb; idx++)
			{
				ms_mask[idx] = _in.read1Bit();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public BlockCPE()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 130, 104, 100, 233, 72, 105, 101, 113,
		100, 139, 103, 104, 103, 138
	})]
	public override void parse(BitReader _in)
	{
		if (_in.read1Bit() != 0)
		{
			parseICSInfo(_in);
			int ms_present = _in.readNBit(2);
			switch (ms_present)
			{
			case 3:
				
				throw new RuntimeException("ms_present = 3 is reserved.");
			default:
				decodeMidSideStereo(_in, ms_present, 0, 0);
				break;
			case 0:
				break;
			}
		}
		BlockICS ics1 = new BlockICS();
		ics1.parse(_in);
		BlockICS ics2 = new BlockICS();
		ics2.parse(_in);
	}

	[HideFromJava]
	static BlockCPE()
	{
		BlockICS.___003Cclinit_003E();
	}
}
