using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;

namespace org.jcodec.containers.mps.psi;

public class PSISection : Object
{
	protected internal int tableId;

	protected internal int specificId;

	protected internal int versionNumber;

	protected internal int currentNextIndicator;

	protected internal int sectionNumber;

	protected internal int lastSectionNumber;

	[LineNumberTable(71)]
	public virtual int getSectionNumber()
	{
		return sectionNumber;
	}

	[LineNumberTable(75)]
	public virtual int getLastSectionNumber()
	{
		return lastSectionNumber;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 98, 111, 110, 111, 145, 137, 144, 110,
		112, 106, 135, 112, 144
	})]
	public static PSISection parsePSI(ByteBuffer data)
	{
		int tableId = (sbyte)data.get() & 0xFF;
		int w0 = data.getShort() & 0xFFFF;
		if ((w0 & 0xC000) != 32768)
		{
			
			throw new RuntimeException("Invalid section data");
		}
		int sectionLength = w0 & 0xFFF;
		data.limit(data.position() + sectionLength);
		int specificId = data.getShort() & 0xFFFF;
		int b0 = (sbyte)data.get() & 0xFF;
		int versionNumber = (b0 >> 1) & 0x1F;
		int currentNextIndicator = b0 & 1;
		int sectionNumber = (sbyte)data.get() & 0xFF;
		int lastSectionNumber = (sbyte)data.get() & 0xFF;
		PSISection result = new PSISection(tableId, specificId, versionNumber, currentNextIndicator, sectionNumber, lastSectionNumber);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 162, 105, 104, 104, 104, 105, 105, 105 })]
	public PSISection(int tableId, int specificId, int versionNumber, int currentNextIndicator, int sectionNumber, int lastSectionNumber)
	{
		this.tableId = tableId;
		this.specificId = specificId;
		this.versionNumber = versionNumber;
		this.currentNextIndicator = currentNextIndicator;
		this.sectionNumber = sectionNumber;
		this.lastSectionNumber = lastSectionNumber;
	}

	[LineNumberTable(55)]
	public virtual int getTableId()
	{
		return tableId;
	}

	[LineNumberTable(59)]
	public virtual int getSpecificId()
	{
		return specificId;
	}

	[LineNumberTable(63)]
	public virtual int getVersionNumber()
	{
		return versionNumber;
	}

	[LineNumberTable(67)]
	public virtual int getCurrentNextIndicator()
	{
		return currentNextIndicator;
	}
}
