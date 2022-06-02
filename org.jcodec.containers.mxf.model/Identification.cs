using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;

namespace org.jcodec.containers.mxf.model;

public class Identification : MXFInterchangeObject
{
	private UL thisGenerationUID;

	private string companyName;

	private string productName;

	private short versionString;

	private UL productUID;

	private Date modificationDate;

	private string platform;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 98, 106 })]
	public Identification(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 134, 130, 120, 141, 109, 191, 38, 109, 166,
		110, 166, 110, 166, 109, 166, 109, 163, 109, 163,
		110, 131, 127, 36, 134, 103, 102
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
			case 15369:
				thisGenerationUID = UL.read(_bb);
				break;
			case 15361:
				companyName = readUtf16String(_bb);
				break;
			case 15362:
				productName = readUtf16String(_bb);
				break;
			case 15364:
				versionString = _bb.getShort();
				break;
			case 15365:
				productUID = UL.read(_bb);
				break;
			case 15366:
				modificationDate = MXFMetadata.readDate(_bb);
				break;
			case 15368:
				platform = readUtf16String(_bb);
				break;
			default:
				Logger.warn(String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(76)]
	public virtual UL getThisGenerationUID()
	{
		return thisGenerationUID;
	}

	[LineNumberTable(80)]
	public virtual string getCompanyName()
	{
		return companyName;
	}

	[LineNumberTable(84)]
	public virtual string getProductName()
	{
		return productName;
	}

	[LineNumberTable(88)]
	public virtual short getVersionString()
	{
		return versionString;
	}

	[LineNumberTable(92)]
	public virtual UL getProductUID()
	{
		return productUID;
	}

	[LineNumberTable(96)]
	public virtual Date getModificationDate()
	{
		return modificationDate;
	}

	[LineNumberTable(100)]
	public virtual string getPlatform()
	{
		return platform;
	}
}
