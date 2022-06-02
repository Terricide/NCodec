using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class AliasBox : FullBox
{
	public class ExtraField : Object
	{
		internal short type;

		internal int len;

		internal byte[] data;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 126, 97, 67, 105, 104, 104, 104 })]
		public ExtraField(short type, int len, byte[] bs)
		{
			this.type = type;
			this.len = len;
			data = bs;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(72)]
		public override string toString()
		{
			string result = Platform.stringFromCharset4(data, 0, len, (type != 14 && type != 15) ? "UTF-8" : "UTF-16");
			
			return result;
		}
	}

	public const int DirectoryName = 0;

	public const int DirectoryIDs = 1;

	public const int AbsolutePath = 2;

	public const int AppleShareZoneName = 3;

	public const int AppleShareServerName = 4;

	public const int AppleShareUserName = 5;

	public const int DriverName = 6;

	public const int RevisedAppleShare = 9;

	public const int AppleRemoteAccessDialup = 10;

	public const int UNIXAbsolutePath = 18;

	public const int UTF16AbsolutePath = 14;

	public const int UFT16VolumeName = 15;

	public const int VolumeMountPoint = 19;

	private string type;

	private short recordSize;

	private new short version;

	private short kind;

	private string volumeName;

	private int volumeCreateDate;

	private short volumeSignature;

	private short volumeType;

	private int parentDirId;

	private string fileName;

	private int fileNumber;

	private int createdLocalDate;

	private string fileTypeName;

	private string creatorName;

	private short nlvlFrom;

	private short nlvlTo;

	private int volumeAttributes;

	private short fsId;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/AliasBox$ExtraField;>;")]
	private List extra;

	[LineNumberTable(57)]
	public static string fourcc()
	{
		return "alis";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 123, 98, 106 })]
	public AliasBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 99, 98, 127, 2, 106, 99, 99 })]
	public virtual ExtraField getExtra(int type)
	{
		Iterator iterator = extra.iterator();
		while (iterator.hasNext())
		{
			ExtraField extraField = (ExtraField)iterator.next();
			if (extraField.type == type)
			{
				return extraField;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 98, 104, 107, 98, 110, 109, 109, 109,
		111, 109, 109, 109, 109, 111, 109, 109, 110, 110,
		109, 109, 109, 109, 138, 140, 104, 101, 99, 104,
		115, 100, 99, 117, 99
	})]
	public override void parse(ByteBuffer @is)
	{
		base.parse(@is);
		if (((uint)flags & (true ? 1u : 0u)) != 0)
		{
			return;
		}
		this.type = NIOUtils.readString(@is, 4);
		recordSize = @is.getShort();
		version = @is.getShort();
		kind = @is.getShort();
		volumeName = NIOUtils.readPascalStringL(@is, 27);
		volumeCreateDate = @is.getInt();
		volumeSignature = @is.getShort();
		volumeType = @is.getShort();
		parentDirId = @is.getInt();
		fileName = NIOUtils.readPascalStringL(@is, 63);
		fileNumber = @is.getInt();
		createdLocalDate = @is.getInt();
		fileTypeName = NIOUtils.readString(@is, 4);
		creatorName = NIOUtils.readString(@is, 4);
		nlvlFrom = @is.getShort();
		nlvlTo = @is.getShort();
		volumeAttributes = @is.getInt();
		fsId = @is.getShort();
		NIOUtils.skip(@is, 10);
		extra = new ArrayList();
		while (true)
		{
			int type = @is.getShort();
			if (type == -1)
			{
				break;
			}
			int len = @is.getShort();
			byte[] bs = NIOUtils.toArray(NIOUtils.read(@is, (len + 1) & -2));
			if (bs == null)
			{
				break;
			}
			extra.add(new ExtraField((short)type, len, bs));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 113, 130, 104, 107, 98, 117, 110, 110, 110,
		111, 110, 110, 110, 110, 111, 110, 110, 117, 117,
		110, 110, 110, 110, 111, 127, 2, 110, 111, 110,
		99, 105, 105
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		if ((flags & 1) == 0)
		{
			@out.put(JCodecUtil2.asciiString(type), 0, 4);
			@out.putShort(recordSize);
			@out.putShort(version);
			@out.putShort(kind);
			NIOUtils.writePascalStringL(@out, volumeName, 27);
			@out.putInt(volumeCreateDate);
			@out.putShort(volumeSignature);
			@out.putShort(volumeType);
			@out.putInt(parentDirId);
			NIOUtils.writePascalStringL(@out, fileName, 63);
			@out.putInt(fileNumber);
			@out.putInt(createdLocalDate);
			@out.put(JCodecUtil2.asciiString(fileTypeName), 0, 4);
			@out.put(JCodecUtil2.asciiString(creatorName), 0, 4);
			@out.putShort(nlvlFrom);
			@out.putShort(nlvlTo);
			@out.putInt(volumeAttributes);
			@out.putShort(fsId);
			@out.put(new byte[10]);
			Iterator iterator = extra.iterator();
			while (iterator.hasNext())
			{
				ExtraField extraField = (ExtraField)iterator.next();
				@out.putShort(extraField.type);
				@out.putShort((short)extraField.len);
				@out.put(extraField.data);
			}
			@out.putShort(-1);
			@out.putShort(0);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 105, 162, 103, 107, 127, 2, 109, 131 })]
	public override int estimateSize()
	{
		int sz = 166;
		if ((flags & 1) == 0)
		{
			Iterator iterator = extra.iterator();
			while (iterator.hasNext())
			{
				ExtraField extraField = (ExtraField)iterator.next();
				sz = (int)(sz + (4 + (nint)extraField.data.LongLength));
			}
		}
		return 12 + sz;
	}

	[LineNumberTable(161)]
	public virtual int getRecordSize()
	{
		return recordSize;
	}

	[LineNumberTable(165)]
	public virtual string getFileName()
	{
		return fileName;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/containers/mp4/boxes/AliasBox$ExtraField;>;")]
	[LineNumberTable(169)]
	public virtual List getExtras()
	{
		return extra;
	}

	[LineNumberTable(181)]
	public virtual bool isSelfRef()
	{
		return (((uint)flags & (true ? 1u : 0u)) != 0) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 98, 118, 104 })]
	public static AliasBox createSelfRef()
	{
		
		AliasBox alis = new AliasBox(new Header(fourcc()));
		alis.setFlags(1);
		return alis;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 95, 162, 106 })]
	public virtual string getUnixPath()
	{
		ExtraField extraField = getExtra(18);
		string result = ((extraField != null) ? new StringBuilder().append("/").append(extraField.toString()).toString() : null);
		
		return result;
	}
}
