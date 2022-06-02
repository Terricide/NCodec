using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.nio;
using org.jcodec.common;

namespace org.jcodec.containers.mps.psi;

public class PATSection : PSISection
{
	private int[] networkPids;

	private IntIntMap programs;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 133, 98, 136, 103, 135, 106, 110, 105, 107,
		100, 139, 106, 131
	})]
	public static PATSection parsePAT(ByteBuffer data)
	{
		PSISection psi = PSISection.parsePSI(data);
		IntArrayList networkPids = IntArrayList.createIntArrayList();
		IntIntMap programs = new IntIntMap();
		while (data.remaining() > 4)
		{
			int programNum = data.getShort() & 0xFFFF;
			int w = data.getShort();
			int pid = w & 0x1FFF;
			if (programNum == 0)
			{
				networkPids.add(pid);
			}
			else
			{
				programs.put(programNum, pid);
			}
		}
		PATSection result = new PATSection(psi, networkPids.toArray(), programs);
		
		return result;
	}

	[LineNumberTable(33)]
	public virtual IntIntMap getPrograms()
	{
		return programs;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 159, 14, 104, 104 })]
	public PATSection(PSISection psi, int[] networkPids, IntIntMap programs)
		: base(psi.tableId, psi.specificId, psi.versionNumber, psi.currentNextIndicator, psi.sectionNumber, psi.lastSectionNumber)
	{
		this.networkPids = networkPids;
		this.programs = programs;
	}

	[LineNumberTable(29)]
	public virtual int[] getNetworkPids()
	{
		return networkPids;
	}
}
