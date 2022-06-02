using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class IndexSegment : MXFInterchangeObject
{
	private IndexEntries ie;

	private int editUnitByteCount;

	private DeltaEntries deltaEntries;

	private int indexSID;

	private int bodySID;

	private int indexEditRateNum;

	private int indexEditRateDen;

	private long indexStartPosition;

	private long indexDuration;

	private UL instanceUID;

	private int sliceCount;

	private int posTableCount;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 162, 106 })]
	public IndexSegment(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 132, 66, 120, 141, 141, 159, 160, 82, 109,
		134, 109, 134, 109, 134, 109, 134, 116, 134, 109,
		134, 109, 134, 109, 109, 134, 109, 134, 109, 131,
		116, 131, 127, 36, 134, 103, 102
	})]
	protected internal override void read(Map tags)
	{
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			ByteBuffer _bb = (ByteBuffer)entry.getValue();
			switch (((Integer)entry.getKey()).intValue())
			{
			case 15370:
				instanceUID = UL.read(_bb);
				break;
			case 16133:
				editUnitByteCount = _bb.getInt();
				break;
			case 16134:
				indexSID = _bb.getInt();
				break;
			case 16135:
				bodySID = _bb.getInt();
				break;
			case 16136:
				sliceCount = (sbyte)_bb.get() & 0xFF;
				break;
			case 16137:
				deltaEntries = DeltaEntries.read(_bb);
				break;
			case 16138:
				ie = IndexEntries.read(_bb);
				break;
			case 16139:
				indexEditRateNum = _bb.getInt();
				indexEditRateDen = _bb.getInt();
				break;
			case 16140:
				indexStartPosition = _bb.getLong();
				break;
			case 16141:
				indexDuration = _bb.getLong();
				break;
			case 16142:
				posTableCount = (sbyte)_bb.get() & 0xFF;
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(89)]
	public virtual IndexEntries getIe()
	{
		return ie;
	}

	[LineNumberTable(93)]
	public virtual int getEditUnitByteCount()
	{
		return editUnitByteCount;
	}

	[LineNumberTable(97)]
	public virtual DeltaEntries getDeltaEntries()
	{
		return deltaEntries;
	}

	[LineNumberTable(101)]
	public virtual int getIndexSID()
	{
		return indexSID;
	}

	[LineNumberTable(105)]
	public virtual int getBodySID()
	{
		return bodySID;
	}

	[LineNumberTable(109)]
	public virtual int getIndexEditRateNum()
	{
		return indexEditRateNum;
	}

	[LineNumberTable(113)]
	public virtual int getIndexEditRateDen()
	{
		return indexEditRateDen;
	}

	[LineNumberTable(117)]
	public virtual long getIndexStartPosition()
	{
		return indexStartPosition;
	}

	[LineNumberTable(121)]
	public virtual long getIndexDuration()
	{
		return indexDuration;
	}

	[LineNumberTable(125)]
	public virtual UL getInstanceUID()
	{
		return instanceUID;
	}

	[LineNumberTable(129)]
	public virtual int getSliceCount()
	{
		return sliceCount;
	}

	[LineNumberTable(133)]
	public virtual int getPosTableCount()
	{
		return posTableCount;
	}
}
