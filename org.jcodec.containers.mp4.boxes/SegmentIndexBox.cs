using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.platform;

namespace org.jcodec.containers.mp4.boxes;

public class SegmentIndexBox : FullBox
{
	public class Reference : Object
	{
		public bool reference_type;

		public long referenced_size;

		public long subsegment_duration;

		public bool starts_with_SAP;

		public int SAP_type;

		public long SAP_delta_time;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(30)]
		public Reference()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(40)]
		public override string toString()
		{
			string result = new StringBuilder().append("Reference [reference_type=").append(reference_type).append(", referenced_size=")
				.append(referenced_size)
				.append(", subsegment_duration=")
				.append(subsegment_duration)
				.append(", starts_with_SAP=")
				.append(starts_with_SAP)
				.append(", SAP_type=")
				.append(SAP_type)
				.append(", SAP_delta_time=")
				.append(SAP_delta_time)
				.append("]")
				.toString();
			
			return result;
		}
	}

	public long reference_ID;

	public long timescale;

	public long earliest_presentation_time;

	public long first_offset;

	public int reserved;

	public int reference_count;

	public Reference[] references;

	[LineNumberTable(48)]
	public static string fourcc()
	{
		return "sidx";
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 139, 162, 106 })]
	public SegmentIndexBox(Header atom)
		: base(atom)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(19)]
	public static SegmentIndexBox createSegmentIndexBox()
	{
		
		SegmentIndexBox result = new SegmentIndexBox(new Header(fourcc()));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 98, 104, 114, 114, 106, 114, 148, 109,
		141, 109, 115, 114, 111, 109, 109, 141, 104, 115,
		112, 105, 115, 112, 144, 235, 51, 234, 79
	})]
	public override void parse(ByteBuffer input)
	{
		base.parse(input);
		reference_ID = Platform.unsignedInt(input.getInt());
		timescale = Platform.unsignedInt(input.getInt());
		if ((sbyte)version == 0)
		{
			earliest_presentation_time = Platform.unsignedInt(input.getInt());
			first_offset = Platform.unsignedInt(input.getInt());
		}
		else
		{
			earliest_presentation_time = input.getLong();
			first_offset = input.getLong();
		}
		reserved = input.getShort();
		reference_count = input.getShort() & 0xFFFF;
		references = new Reference[reference_count];
		for (int i = 0; i < reference_count; i++)
		{
			long i2 = Platform.unsignedInt(input.getInt());
			long i3 = Platform.unsignedInt(input.getInt());
			long i4 = Platform.unsignedInt(input.getInt());
			Reference @ref = new Reference();
			@ref.reference_type = (((ulong)i2 >> 31) & 1u) == 1u;
			@ref.referenced_size = i2 & 0x7FFFFFFFu;
			@ref.subsegment_duration = i3;
			@ref.starts_with_SAP = (((ulong)i4 >> 31) & 1u) == 1u;
			@ref.SAP_type = (int)(((ulong)i4 >> 28) & 7u);
			@ref.SAP_delta_time = i4 & 0xFFFFFFFu;
			references[i] = @ref;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 98, 104, 111, 111, 106, 111, 145, 110,
		142, 111, 111, 111, 106, 122, 105, 100, 105, 139,
		113, 149, 105, 105, 234, 51, 234, 79
	})]
	protected internal override void doWrite(ByteBuffer @out)
	{
		base.doWrite(@out);
		@out.putInt((int)reference_ID);
		@out.putInt((int)timescale);
		if ((sbyte)version == 0)
		{
			@out.putInt((int)earliest_presentation_time);
			@out.putInt((int)first_offset);
		}
		else
		{
			@out.putLong(earliest_presentation_time);
			@out.putLong(first_offset);
		}
		@out.putShort((short)reserved);
		@out.putShort((short)reference_count);
		for (int i = 0; i < reference_count; i++)
		{
			Reference @ref = references[i];
			int i2 = (int)(((@ref.reference_type ? 1 : 0) << 31) | @ref.referenced_size);
			int i3 = (int)@ref.subsegment_duration;
			int i4 = 0;
			if (@ref.starts_with_SAP)
			{
				i4 |= int.MinValue;
			}
			i4 |= (@ref.SAP_type & 7) << 28;
			i4 = (int)(i4 | (@ref.SAP_delta_time & 0xFFFFFFFu));
			@out.putInt(i2);
			@out.putInt(i3);
			@out.putInt(i4);
		}
	}

	[LineNumberTable(116)]
	public override int estimateSize()
	{
		return 40 + reference_count * 12;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		112,
		98,
		191,
		117,
		byte.MaxValue,
		61,
		61
	})]
	public override string toString()
	{
		string result = new StringBuilder().append("SegmentIndexBox [reference_ID=").append(reference_ID).append(", timescale=")
			.append(timescale)
			.append(", earliest_presentation_time=")
			.append(earliest_presentation_time)
			.append(", first_offset=")
			.append(first_offset)
			.append(", reserved=")
			.append(reserved)
			.append(", reference_count=")
			.append(reference_count)
			.append(", references=")
			.append(Platform.arrayToString(references))
			.append(", version=")
			.append((sbyte)version)
			.append(", flags=")
			.append(flags)
			.append(", header=")
			.append(header)
			.append("]")
			.toString();
		
		return result;
	}
}
