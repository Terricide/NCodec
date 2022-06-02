using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.codecs.h264.io.write;
using org.jcodec.codecs.h264.mp4;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.codecs.h264;

public class H264Utils : Object
{
	public class Mv : Object
	{
		[LineNumberTable(964)]
		public static int mvX(int mv)
		{
			return mv << 18 >> 18;
		}

		[LineNumberTable(968)]
		public static int mvY(int mv)
		{
			return mv << 6 >> 20;
		}

		[LineNumberTable(972)]
		public static int mvRef(int mv)
		{
			return mv >> 26;
		}

		[LineNumberTable(976)]
		public static int packMv(int mvx, int mvy, int r)
		{
			return ((r & 0x3F) << 26) | ((mvy & 0xFFF) << 14) | (mvx & 0x3FFF);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(980)]
		public static int mvC(int mv, int comp)
		{
			int result = ((comp != 0) ? mvY(mv) : mvX(mv));
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(962)]
		public Mv()
		{
		}
	}

	public class MvList : Object
	{
		private int[] list;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static int NA;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 149, 66, 105, 111, 105 })]
		public MvList(int size)
		{
			list = new int[size << 1];
			clear();
		}

		[LineNumberTable(new byte[] { 158, 148, 130, 109, 63, 0, 167 })]
		public virtual void clear()
		{
			for (int i = 0; i < (nint)list.LongLength; i += 2)
			{
				int[] array = list;
				int num = i;
				int[] array2 = list;
				int num2 = i + 1;
				int nA = NA;
				int num3 = num2;
				int[] array3 = array2;
				array3[num3] = nA;
				array[num] = nA;
			}
		}

		[LineNumberTable(1032)]
		public virtual int getMv(int off, int forward)
		{
			return list[(off << 1) + forward];
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1016)]
		public virtual int mv0R(int off)
		{
			int result = Mv.mvRef(list[off << 1]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1028)]
		public virtual int mv1R(int off)
		{
			int result = Mv.mvRef(list[(off << 1) + 1]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1008)]
		public virtual int mv0X(int off)
		{
			int result = Mv.mvX(list[off << 1]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1012)]
		public virtual int mv0Y(int off)
		{
			int result = Mv.mvY(list[off << 1]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1020)]
		public virtual int mv1X(int off)
		{
			int result = Mv.mvX(list[(off << 1) + 1]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1024)]
		public virtual int mv1Y(int off)
		{
			int result = Mv.mvY(list[(off << 1) + 1]);
			
			return result;
		}

		[LineNumberTable(new byte[] { 158, 138, 66, 108, 110 })]
		public virtual void setPair(int off, int mv0, int mv1)
		{
			list[off << 1] = mv0;
			list[(off << 1) + 1] = mv1;
		}

		[LineNumberTable(new byte[] { 158, 137, 98, 117, 121 })]
		public virtual void copyPair(int off, MvList other, int otherOff)
		{
			list[off << 1] = other.list[otherOff << 1];
			list[(off << 1) + 1] = other.list[(otherOff << 1) + 1];
		}

		[LineNumberTable(new byte[] { 158, 139, 66, 110 })]
		public virtual void setMv(int off, int forward, int mv)
		{
			list[(off << 1) + forward] = mv;
		}

		[LineNumberTable(994)]
		static MvList()
		{
			NA = Mv.packMv(0, 0, -1);
		}
	}

	public class MvList2D : Object
	{
		private int[] list;

		private int stride;

		private int width;

		private int height;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static int NA;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[LineNumberTable(1104)]
		public virtual int getHeight()
		{
			return height;
		}

		[LineNumberTable(1108)]
		public virtual int getWidth()
		{
			return width;
		}

		[LineNumberTable(1096)]
		public virtual int getMv(int offX, int offY, int forward)
		{
			return list[(offX << 1) + stride * offY + forward];
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 134, 98, 105, 113, 106, 104, 104, 105 })]
		public MvList2D(int width, int height)
		{
			list = new int[(width << 1) * height];
			stride = width << 1;
			this.width = width;
			this.height = height;
			clear();
		}

		[LineNumberTable(new byte[] { 158, 123, 66, 120 })]
		public virtual void setMv(int offX, int offY, int forward, int mv)
		{
			list[(offX << 1) + stride * offY + forward] = mv;
		}

		[LineNumberTable(new byte[] { 158, 132, 130, 109, 63, 0, 167 })]
		public virtual void clear()
		{
			for (int i = 0; i < (nint)list.LongLength; i += 2)
			{
				int[] array = list;
				int num = i;
				int[] array2 = list;
				int num2 = i + 1;
				int nA = NA;
				int num3 = num2;
				int[] array3 = array2;
				array3[num3] = nA;
				array[num] = nA;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1072)]
		public virtual int mv0X(int offX, int offY)
		{
			int result = Mv.mvX(list[(offX << 1) + stride * offY]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1076)]
		public virtual int mv0Y(int offX, int offY)
		{
			int result = Mv.mvY(list[(offX << 1) + stride * offY]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1080)]
		public virtual int mv0R(int offX, int offY)
		{
			int result = Mv.mvRef(list[(offX << 1) + stride * offY]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1084)]
		public virtual int mv1X(int offX, int offY)
		{
			int result = Mv.mvX(list[(offX << 1) + stride * offY + 1]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1088)]
		public virtual int mv1Y(int offX, int offY)
		{
			int result = Mv.mvY(list[(offX << 1) + stride * offY + 1]);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1092)]
		public virtual int mv1R(int offX, int offY)
		{
			int result = Mv.mvRef(list[(offX << 1) + stride * offY + 1]);
			
			return result;
		}

		[LineNumberTable(1055)]
		static MvList2D()
		{
			NA = Mv.packMv(0, 0, -1);
		}
	}

	public abstract class SliceHeaderTweaker : Object
	{
		[Signature("Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;>;")]
		protected internal List sps;

		[Signature("Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;>;")]
		protected internal List pps;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 207, 98, 104, 144, 137, 156, 106, 142, 140,
			143, 136, 143
		})]
		private SliceHeader part2(ByteBuffer @is, ByteBuffer os, NALUnit nu, SeqParameterSet sps, PictureParameterSet pps, ByteBuffer nal, BitReader reader, SliceHeader sh)
		{
			BitWriter writer = new BitWriter(os);
			SliceHeaderReader.readPart2(sh, nu, sps, pps, reader);
			tweak(sh);
			SliceHeaderWriter.write(sh, nu.type == NALUnitType.___003C_003EIDR_SLICE, nu.nal_ref_idc, writer);
			if (pps.entropyCodingModeFlag)
			{
				copyDataCABAC(@is, os, reader, writer);
			}
			else
			{
				copyDataCAVLC(@is, os, reader, writer);
			}
			nal.limit(os.position());
			escapeNALinplace(nal);
			os.position(nal.limit());
			return sh;
		}

		protected internal abstract void tweak(SliceHeader sh);

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 195, 130, 105, 102, 109, 113, 177, 106, 118,
			104, 135, 105
		})]
		private void copyDataCABAC(ByteBuffer @is, ByteBuffer os, BitReader reader, BitWriter writer)
		{
			long bp = reader.curBit();
			if (bp != 0u)
			{
				long rem = reader.readNBit(8 - (int)bp);
				if ((1 << (int)(8u - bp)) - 1 != rem)
				{
					
					throw new RuntimeException("Invalid CABAC padding");
				}
			}
			if (writer.curBit() != 0)
			{
				writer.writeNBit(255, 8 - writer.curBit());
			}
			writer.flush();
			reader.stop();
			os.put(@is);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 202, 162, 107, 100, 112, 168, 104, 103, 101,
			105, 135, 105, 105, 111, 140, 107, 99, 111, 99,
			103, 137
		})]
		private void copyDataCAVLC(ByteBuffer @is, ByteBuffer os, BitReader reader, BitWriter writer)
		{
			int wLeft = 8 - writer.curBit();
			if (wLeft != 0)
			{
				writer.writeNBit(reader.readNBit(wLeft), wLeft);
			}
			writer.flush();
			int shift = reader.curBit();
			if (shift != 0)
			{
				int mShift = 8 - shift;
				int inp = reader.readNBit(mShift);
				reader.stop();
				while (@is.hasRemaining())
				{
					int @out = inp << shift;
					inp = (sbyte)@is.get() & 0xFF;
					@out |= inp >> mShift;
					os.put((byte)(sbyte)@out);
				}
				os.put((byte)(sbyte)(inp << shift));
			}
			else
			{
				reader.stop();
				os.put(@is);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(731)]
		public SliceHeaderTweaker()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 214, 162, 136, 135, 104, 136, 147 })]
		public virtual SliceHeader run(ByteBuffer @is, ByteBuffer os, NALUnit nu)
		{
			ByteBuffer nal = os.duplicate();
			unescapeNAL(@is);
			BitReader reader = BitReader.createBitReader(@is);
			SliceHeader sh = SliceHeaderReader.readPart1(reader);
			PictureParameterSet pp = findPPS(pps, sh.picParameterSetId);
			SliceHeader result = part2(@is, os, nu, findSPS(sps, pp.picParameterSetId), pp, nal, reader, sh);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 210, 98, 136, 135, 104, 136 })]
		public virtual SliceHeader runSpsPps(ByteBuffer @is, ByteBuffer os, NALUnit nu, SeqParameterSet sps, PictureParameterSet pps)
		{
			ByteBuffer nal = os.duplicate();
			unescapeNAL(@is);
			BitReader reader = BitReader.createBitReader(@is);
			SliceHeader sh = SliceHeaderReader.readPart1(reader);
			SliceHeader result = part2(@is, os, nu, sps, pps, nal, reader, sh);
			
			return result;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 98, 135, 105, 138 })]
	public static ByteBuffer nextNALUnit(ByteBuffer buf)
	{
		skipToNALUnit(buf);
		if (buf.hasArray())
		{
			ByteBuffer result = gotoNALUnitWithArray(buf);
			
			return result;
		}
		ByteBuffer result2 = gotoNALUnit(buf);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 225, 130, 104, 103, 104 })]
	public static SeqParameterSet readSPS(ByteBuffer data)
	{
		ByteBuffer input = NIOUtils.duplicate(data);
		unescapeNAL(input);
		return SeqParameterSet.read(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(new byte[] { 158, 250, 130, 167, 107, 171 })]
	public static List splitFrame(ByteBuffer frame)
	{
		ArrayList result = new ArrayList();
		ByteBuffer segment;
		while ((segment = nextNALUnit(frame)) != null)
		{
			result.add(segment);
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)Z")]
	[LineNumberTable(new byte[] { 159, 2, 66, 124, 120, 99, 99 })]
	public static bool idrSlice(List _data)
	{
		Iterator iterator = _data.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer segment = (ByteBuffer)iterator.next();
			if (NALUnit.read(segment.duplicate()).type == NALUnitType.___003C_003EIDR_SLICE)
			{
				return true;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 5, 130, 136, 107, 115, 131 })]
	public static bool isByteBufferIDRSlice(ByteBuffer _data)
	{
		ByteBuffer data = _data.duplicate();
		ByteBuffer segment;
		while ((segment = nextNALUnit(data)) != null)
		{
			if (NALUnit.read(segment).type == NALUnitType.___003C_003EIDR_SLICE)
			{
				return true;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 104, 66, 106, 98, 104, 104, 105, 105, 105,
		105, 105, 106, 108, 106, 99, 100, 99, 110
	})]
	public static void unescapeNAL(ByteBuffer _buf)
	{
		if (_buf.remaining() < 2)
		{
			return;
		}
		ByteBuffer _in = _buf.duplicate();
		ByteBuffer @out = _buf.duplicate();
		int p1 = (sbyte)_in.get();
		@out.put((byte)p1);
		int p2 = (sbyte)_in.get();
		@out.put((byte)p2);
		while (_in.hasRemaining())
		{
			int b = (sbyte)_in.get();
			if (p1 != 0 || p2 != 0 || b != 3)
			{
				@out.put((byte)b);
			}
			p1 = p2;
			p2 = b;
		}
		_buf.limit(@out.position());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(940)]
	public static List getRawSPS(ByteBuffer codecPrivate)
	{
		List rawNALUnitsOfType = getRawNALUnitsOfType(codecPrivate, NALUnitType.___003C_003ESPS);
		
		return rawNALUnitsOfType;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(929)]
	public static List getRawPPS(ByteBuffer codecPrivate)
	{
		List rawNALUnitsOfType = getRawNALUnitsOfType(codecPrivate, NALUnitType.___003C_003EPPS);
		
		return rawNALUnitsOfType;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 191, 162, 108, 106, 105, 127, 3, 159, 3 })]
	public static Size getPicSize(SeqParameterSet sps)
	{
		int w = sps.picWidthInMbsMinus1 + 1 << 4;
		int h = SeqParameterSet.getPicHeightInMbs(sps) << 4;
		if (sps.frameCroppingFlag)
		{
			w -= sps.frameCropLeftOffset + sps.frameCropRightOffset << sps.chromaFormatIdc.compWidth[1];
			h -= sps.frameCropTopOffset + sps.frameCropBottomOffset << sps.chromaFormatIdc.compHeight[1];
		}
		Size result = new Size(w, h);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 91, 98, 113, 105, 105, 105, 105, 113, 105,
		99, 131, 105, 99, 99, 99
	})]
	public static void escapeNAL(ByteBuffer src, ByteBuffer dst)
	{
		int p1 = (sbyte)src.get();
		int p2 = (sbyte)src.get();
		dst.put((byte)p1);
		dst.put((byte)p2);
		while (src.hasRemaining())
		{
			int b = (sbyte)src.get();
			if (p1 == 0 && p2 == 0 && (b & 0xFF) <= 3)
			{
				dst.put(3);
				p1 = p2;
				p2 = 3;
			}
			dst.put((byte)b);
			p1 = p2;
			p2 = b;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 129, 130, 105, 130, 99, 105, 101, 113, 107,
		110, 163
	})]
	public static void skipToNALUnit(ByteBuffer buf)
	{
		if (!buf.hasRemaining())
		{
			return;
		}
		int val = -1;
		while (buf.hasRemaining())
		{
			val <<= 8;
			val |= (sbyte)buf.get() & 0xFF;
			if ((val & 0xFFFFFF) == 1)
			{
				buf.position(buf.position());
				break;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 114, 66, 105, 131, 104, 104, 141, 104, 106,
		100, 144, 105, 134, 110, 110, 136, 105, 121, 149,
		117, 144, 227, 69, 101, 134, 142
	})]
	public static ByteBuffer gotoNALUnitWithArray(ByteBuffer buf)
	{
		if (!buf.hasRemaining())
		{
			return null;
		}
		int from = buf.position();
		ByteBuffer result = buf.slice();
		result.order(ByteOrder.BIG_ENDIAN);
		byte[] arr = buf.array();
		int pos = from + buf.arrayOffset();
		int posFrom = pos;
		for (int lim = buf.limit() + buf.arrayOffset(); pos < lim; pos += 3)
		{
			int b = arr[pos];
			if (((uint)b & 0xFEu) != 0)
			{
				continue;
			}
			while (b == 0)
			{
				pos++;
				if (pos >= lim)
				{
					break;
				}
				b = arr[pos];
			}
			if (b == 1 && pos - posFrom >= 2 && arr[pos - 1] == 0 && arr[pos - 2] == 0)
			{
				int lenSize = ((pos - posFrom < 3 || arr[pos - 3] != 0) ? 3 : 4);
				buf.position(pos + 1 - buf.arrayOffset() - lenSize);
				result.limit(buf.position() - from);
				return result;
			}
		}
		buf.position(buf.limit());
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 122, 98, 105, 131, 104, 104, 141, 99, 105,
		101, 113, 107, 119, 112, 163
	})]
	public static ByteBuffer gotoNALUnit(ByteBuffer buf)
	{
		if (!buf.hasRemaining())
		{
			return null;
		}
		int from = buf.position();
		ByteBuffer result = buf.slice();
		result.order(ByteOrder.BIG_ENDIAN);
		int val = -1;
		while (buf.hasRemaining())
		{
			val <<= 8;
			val |= (sbyte)buf.get() & 0xFF;
			if ((val & 0xFFFFFF) == 1)
			{
				buf.position(buf.position() - ((val != 1) ? 3 : 4));
				result.limit(buf.position() - from);
				break;
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 95, 66, 103, 104, 104, 105, 105, 106, 111,
		131, 107, 107, 99, 105
	})]
	private static int[] searchEscapeLocations(ByteBuffer src)
	{
		IntArrayList points = IntArrayList.createIntArrayList();
		ByteBuffer search = src.duplicate();
		int p = search.getShort();
		while (search.hasRemaining())
		{
			int b = (sbyte)search.get();
			if (p == 0 && (b & -4) == 0)
			{
				points.add(search.position() - 1);
				p = 3;
			}
			p = (p << 8) & 0xFFFF;
			p |= b & 0xFF;
		}
		return points.toArray();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 84, 162, 155, 143, 142, 158, 138 })]
	private static int readLen(ByteBuffer dup, int nls)
	{
		switch (nls)
		{
		case 1:
			return (sbyte)dup.get() & 0xFF;
		case 2:
			return dup.getShort() & 0xFFFF;
		case 3:
			return ((dup.getShort() & 0xFFFF) << 8) | ((sbyte)dup.get() & 0xFF);
		case 4:
		{
			int @int = dup.getInt();
			
			return @int;
		}
		default:
		{
			string s = new StringBuilder().append("NAL Unit length size can not be ").append(nls).toString();
			
			throw new IllegalArgumentException(s);
		}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 60, 66, 106, 113, 104, 106, 104, 112, 105,
		112, 99
	})]
	public static void decodeMOVPacketInplace(ByteBuffer result, AvcCBox avcC)
	{
		if (avcC.getNalLengthSize() != 4)
		{
			
			throw new IllegalArgumentException("Can only inplace decode AVC MOV packet with nal_length_size = 4.");
		}
		ByteBuffer dup = result.duplicate();
		while (dup.remaining() >= 4)
		{
			int size = dup.getInt();
			dup.position(dup.position() - 4);
			dup.putInt(1);
			dup.position(dup.position() + size);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;Lorg/jcodec/codecs/h264/mp4/AvcCBox;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(new byte[]
	{
		159, 87, 130, 103, 104, 104, 106, 105, 100, 99,
		111, 99
	})]
	public static List splitMOVPacket(ByteBuffer buf, AvcCBox avcC)
	{
		ArrayList result = new ArrayList();
		int nls = avcC.getNalLengthSize();
		ByteBuffer dup = buf.duplicate();
		while (dup.remaining() >= nls)
		{
			int len = readLen(dup, nls);
			if (len == 0)
			{
				break;
			}
			((List)result).add((object)NIOUtils.read(dup, len));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)Ljava/nio/ByteBuffer;")]
	[LineNumberTable(new byte[] { 158, 245, 98, 99, 124, 108, 99, 104, 104 })]
	public static ByteBuffer joinNALUnits(List nalUnits)
	{
		int size = 0;
		Iterator iterator = nalUnits.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer nal = (ByteBuffer)iterator.next();
			size += 4 + nal.remaining();
		}
		ByteBuffer allocate = ByteBuffer.allocate(size);
		joinNALUnitsToBuffer(nalUnits, allocate);
		return allocate;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 100, 162, 136, 104, 145, 118, 104, 113, 109,
		101, 106, 231, 59, 235, 72
	})]
	public static void escapeNALinplace(ByteBuffer src)
	{
		int[] loc = searchEscapeLocations(src);
		int old = src.limit();
		src.limit((int)(src.limit() + (nint)loc.LongLength));
		int newPos = src.limit() - 1;
		int oldPos = old - 1;
		int locIdx = (int)((nint)loc.LongLength - 1);
		while (newPos >= src.position())
		{
			src.put(newPos, (byte)(sbyte)src.get(oldPos));
			if (locIdx >= 0 && loc[locIdx] == oldPos)
			{
				newPos += -1;
				src.put(newPos, 3);
				locIdx += -1;
			}
			newPos += -1;
			oldPos += -1;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;>;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(new byte[]
	{
		159, 28, 130, 103, 124, 108, 104, 104, 103, 105,
		99
	})]
	public static List saveSPS(List initSPS)
	{
		ArrayList serialSps = new ArrayList();
		Iterator iterator = initSPS.iterator();
		while (iterator.hasNext())
		{
			SeqParameterSet sps = (SeqParameterSet)iterator.next();
			ByteBuffer bb1 = ByteBuffer.allocate(512);
			sps.write(bb1);
			bb1.flip();
			escapeNALinplace(bb1);
			((List)serialSps).add((object)bb1);
		}
		return serialSps;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;>;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(new byte[]
	{
		159, 32, 130, 103, 124, 108, 104, 104, 103, 105,
		99
	})]
	public static List savePPS(List initPPS)
	{
		ArrayList serialPps = new ArrayList();
		Iterator iterator = initPPS.iterator();
		while (iterator.hasNext())
		{
			PictureParameterSet pps = (PictureParameterSet)iterator.next();
			ByteBuffer bb1 = ByteBuffer.allocate(512);
			pps.write(bb1);
			bb1.flip();
			escapeNALinplace(bb1);
			((List)serialPps).add((object)bb1);
		}
		return serialPps;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;Ljava/util/List<Ljava/nio/ByteBuffer;>;I)Lorg/jcodec/containers/mp4/boxes/SampleEntry;")]
	[LineNumberTable(new byte[] { 159, 21, 98, 138 })]
	public static SampleEntry createMOVSampleEntryFromSpsPpsList(List spsList, List ppsList, int nalLengthSize)
	{
		AvcCBox avcC = createAvcCFromPS(spsList, ppsList, nalLengthSize);
		SampleEntry result = createMOVSampleEntryFromAvcC(avcC);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;Ljava/util/List<Ljava/nio/ByteBuffer;>;I)Lorg/jcodec/codecs/h264/mp4/AvcCBox;")]
	[LineNumberTable(new byte[] { 159, 16, 98, 120 })]
	public static AvcCBox createAvcCFromPS(List spsList, List ppsList, int nalLengthSize)
	{
		SeqParameterSet sps = readSPS(NIOUtils.duplicate((ByteBuffer)spsList.get(0)));
		AvcCBox result = AvcCBox.createAvcCBox(sps.profileIdc, 0, sps.levelIdc, nalLengthSize, spsList, ppsList);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 15, 130, 125, 108, 138, 119, 104 })]
	public static SampleEntry createMOVSampleEntryFromAvcC(AvcCBox avcC)
	{
		SeqParameterSet sps = SeqParameterSet.read(((ByteBuffer)avcC.getSpsList().get(0)).duplicate());
		int codedWidth = sps.picWidthInMbsMinus1 + 1 << 4;
		int codedHeight = SeqParameterSet.getPicHeightInMbs(sps) << 4;
		VideoSampleEntry se = VideoSampleEntry.videoSampleEntry("avc1", getPicSize(sps), "JCodec");
		se.add(avcC);
		return se;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 10, 130, 122, 39 })]
	public static SampleEntry createMOVSampleEntryFromBuffer(ByteBuffer sps, ByteBuffer pps, int nalLengthSize)
	{
		SampleEntry result = createMOVSampleEntryFromSpsPpsList(Arrays.asList(sps), Arrays.asList(pps), nalLengthSize);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		158,
		byte.MaxValue,
		162,
		108,
		127,
		2,
		159,
		9,
		109,
		104,
		105,
		104,
		99,
		127,
		3,
		127,
		9,
		110,
		104,
		105,
		104,
		99
	})]
	public static void saveStreamParams(AvcCBox avcC, org.jcodec.common.io.SeekableByteChannel raw)
	{
		ByteBuffer bb = ByteBuffer.allocate(1024);
		Iterator iterator = avcC.getSpsList().iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer2 = (ByteBuffer)iterator.next();
			raw.write(ByteBuffer.wrap(new byte[5] { 0, 0, 0, 1, 103 }));
			escapeNAL(byteBuffer2.duplicate(), bb);
			bb.flip();
			raw.write(bb);
			bb.clear();
		}
		Iterator iterator2 = avcC.getPpsList().iterator();
		while (iterator2.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator2.next();
			raw.write(ByteBuffer.wrap(new byte[5] { 0, 0, 0, 1, 104 }));
			escapeNAL(byteBuffer.duplicate(), bb);
			bb.flip();
			raw.write(bb);
			bb.clear();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;Ljava/nio/ByteBuffer;)V")]
	[LineNumberTable(new byte[] { 158, 241, 162, 124, 105, 110, 99 })]
	public static void joinNALUnitsToBuffer(List nalUnits, ByteBuffer @out)
	{
		Iterator iterator = nalUnits.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer nal = (ByteBuffer)iterator.next();
			@out.putInt(1);
			@out.put(nal.duplicate());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(682)]
	public static AvcCBox parseAVCCFromBuffer(ByteBuffer bb)
	{
		AvcCBox result = AvcCBox.parseAvcCBox(bb);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;Ljava/util/List<Ljava/nio/ByteBuffer;>;)Ljava/nio/ByteBuffer;")]
	[LineNumberTable(new byte[]
	{
		158, 235, 130, 99, 124, 108, 99, 125, 109, 131,
		105, 127, 1, 106, 107, 112, 99, 127, 1, 106,
		107, 112, 99, 105
	})]
	public static ByteBuffer saveCodecPrivate(List spsList, List ppsList)
	{
		int totalCodecPrivateSize = 0;
		Iterator iterator = spsList.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer4 = (ByteBuffer)iterator.next();
			totalCodecPrivateSize += byteBuffer4.remaining() + 5;
		}
		Iterator iterator2 = ppsList.iterator();
		while (iterator2.hasNext())
		{
			ByteBuffer byteBuffer3 = (ByteBuffer)iterator2.next();
			totalCodecPrivateSize += byteBuffer3.remaining() + 5;
		}
		ByteBuffer bb = ByteBuffer.allocate(totalCodecPrivateSize);
		Iterator iterator3 = spsList.iterator();
		while (iterator3.hasNext())
		{
			ByteBuffer byteBuffer2 = (ByteBuffer)iterator3.next();
			bb.putInt(1);
			bb.put(103);
			bb.put(byteBuffer2.duplicate());
		}
		Iterator iterator4 = ppsList.iterator();
		while (iterator4.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator4.next();
			bb.putInt(1);
			bb.put(104);
			bb.put(byteBuffer.duplicate());
		}
		bb.flip();
		return bb;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 221, 98, 104, 103, 104 })]
	public static PictureParameterSet readPPS(ByteBuffer data)
	{
		ByteBuffer input = NIOUtils.duplicate(data);
		unescapeNAL(input);
		return PictureParameterSet.read(input);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 223, 98, 106, 104, 104, 103 })]
	public static ByteBuffer writePPS(PictureParameterSet pps, int approxSize)
	{
		ByteBuffer output = ByteBuffer.allocate(approxSize + 8);
		pps.write(output);
		output.flip();
		escapeNALinplace(output);
		return output;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 227, 130, 106, 104, 104, 103 })]
	public static ByteBuffer writeSPS(SeqParameterSet sps, int approxSize)
	{
		ByteBuffer output = ByteBuffer.allocate(approxSize + 8);
		sps.write(output);
		output.flip();
		escapeNALinplace(output);
		return output;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 170, 98, 111, 104, 104, 105, 106, 105 })]
	private static void putNAL(ByteBuffer codecPrivate, ByteBuffer byteBuffer, int nalType)
	{
		ByteBuffer dst = ByteBuffer.allocate(byteBuffer.remaining() * 2);
		escapeNAL(byteBuffer, dst);
		dst.flip();
		codecPrivate.putInt(1);
		codecPrivate.put((byte)(sbyte)nalType);
		codecPrivate.put(dst);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;Lorg/jcodec/codecs/h264/io/model/NALUnitType;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(new byte[] { 158, 162, 66, 103, 127, 7, 104, 106, 137, 99 })]
	public static List getRawNALUnitsOfType(ByteBuffer codecPrivate, NALUnitType type)
	{
		ArrayList result = new ArrayList();
		Iterator iterator = splitFrame(codecPrivate.duplicate()).iterator();
		while (iterator.hasNext())
		{
			ByteBuffer bb = (ByteBuffer)iterator.next();
			NALUnit nu = NALUnit.read(bb);
			if (nu.type == type)
			{
				((List)result).add((object)bb);
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(42)]
	public H264Utils()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 77, 98, 104, 136, 104, 104, 100, 99, 105,
		110, 108, 99
	})]
	public static void encodeMOVPacketInplace(ByteBuffer avcFrame)
	{
		ByteBuffer dup = avcFrame.duplicate();
		ByteBuffer d1 = avcFrame.duplicate();
		int tot = d1.position();
		while (true)
		{
			ByteBuffer buf = nextNALUnit(dup);
			if (buf == null)
			{
				break;
			}
			d1.position(tot);
			d1.putInt(buf.remaining());
			tot += buf.remaining() + 4;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 71, 130, 136, 135, 99, 107, 105, 140, 114,
		127, 1, 112, 107, 99, 105
	})]
	public static ByteBuffer encodeMOVPacket(ByteBuffer avcFrame)
	{
		ByteBuffer dup = avcFrame.duplicate();
		ArrayList list = new ArrayList();
		int totalLen = 0;
		ByteBuffer buf;
		while ((buf = nextNALUnit(dup)) != null)
		{
			((List)list).add((object)buf);
			totalLen += buf.remaining();
		}
		ByteBuffer result = ByteBuffer.allocate(((List)list).size() * 4 + totalLen);
		Iterator iterator = ((List)list).iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator.next();
			result.putInt(byteBuffer.remaining());
			result.put(byteBuffer);
		}
		result.flip();
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 64, 98, 106, 104, 131 })]
	public static ByteBuffer decodeMOVPacket(ByteBuffer result, AvcCBox avcC)
	{
		if (avcC.getNalLengthSize() == 4)
		{
			decodeMOVPacketInplace(result, avcC);
			return result;
		}
		ByteBuffer result2 = joinNALUnits(splitMOVPacket(result, avcC));
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;Ljava/nio/ByteBuffer;Ljava/util/List<Ljava/nio/ByteBuffer;>;Ljava/util/List<Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 54, 162, 104, 108, 104, 100, 134, 109, 110,
		100, 112, 110, 100, 112, 100, 105, 137, 102, 100,
		104
	})]
	public static void wipePS(ByteBuffer _in, ByteBuffer @out, List spsList, List ppsList)
	{
		ByteBuffer dup = _in.duplicate();
		while (dup.hasRemaining())
		{
			ByteBuffer buf = nextNALUnit(dup);
			if (buf == null)
			{
				break;
			}
			NALUnit nu = NALUnit.read(buf.duplicate());
			if (nu.type == NALUnitType.___003C_003EPPS)
			{
				ppsList?.add(NIOUtils.duplicate(buf));
			}
			else if (nu.type == NALUnitType.___003C_003ESPS)
			{
				spsList?.add(NIOUtils.duplicate(buf));
			}
			else if (@out != null)
			{
				@out.putInt(1);
				@out.put(buf);
			}
		}
		@out?.flip();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;Ljava/util/Collection<Ljava/nio/ByteBuffer;>;Ljava/util/Collection<Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 45, 162, 104, 108, 104, 100, 134, 104, 110,
		100, 110, 112, 110, 100, 110, 112, 123, 99, 102
	})]
	public static void wipePSinplace(ByteBuffer _in, Collection spsList, Collection ppsList)
	{
		ByteBuffer dup = _in.duplicate();
		while (dup.hasRemaining())
		{
			ByteBuffer buf = nextNALUnit(dup);
			if (buf == null)
			{
				break;
			}
			NALUnit nu = NALUnit.read(buf);
			if (nu.type == NALUnitType.___003C_003EPPS)
			{
				ppsList?.add(NIOUtils.duplicate(buf));
				_in.position(dup.position());
			}
			else if (nu.type == NALUnitType.___003C_003ESPS)
			{
				spsList?.add(NIOUtils.duplicate(buf));
				_in.position(dup.position());
			}
			else if (nu.type == NALUnitType.___003C_003EIDR_SLICE || nu.type == NALUnitType.___003C_003ENON_IDR_SLICE)
			{
				break;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 39, 66, 108, 104, 104, 135, 108, 104, 104,
		135, 127, 9, 38, 167
	})]
	public static AvcCBox createAvcC(SeqParameterSet sps, PictureParameterSet pps, int nalLengthSize)
	{
		ByteBuffer serialSps = ByteBuffer.allocate(512);
		sps.write(serialSps);
		serialSps.flip();
		escapeNALinplace(serialSps);
		ByteBuffer serialPps = ByteBuffer.allocate(512);
		pps.write(serialPps);
		serialPps.flip();
		escapeNALinplace(serialPps);
		return AvcCBox.createAvcCBox(sps.profileIdc, 0, sps.levelIdc, nalLengthSize, Arrays.asList(serialSps), Arrays.asList(serialPps));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;>;Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;>;I)Lorg/jcodec/codecs/h264/mp4/AvcCBox;")]
	[LineNumberTable(new byte[] { 159, 35, 130, 104, 136, 110 })]
	public static AvcCBox createAvcCFromList(List initSPS, List initPPS, int nalLengthSize)
	{
		List serialSps = saveSPS(initSPS);
		List serialPps = savePPS(initPPS);
		SeqParameterSet sps = (SeqParameterSet)initSPS.get(0);
		AvcCBox result = AvcCBox.createAvcCBox(sps.profileIdc, 0, sps.levelIdc, nalLengthSize, serialSps, serialPps);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 23, 130, 109, 109 })]
	public static SampleEntry createMOVSampleEntryFromBytes(ByteBuffer codecPrivate)
	{
		List rawSPS = getRawSPS(codecPrivate.duplicate());
		List rawPPS = getRawPPS(codecPrivate.duplicate());
		SampleEntry result = createMOVSampleEntryFromSpsPpsList(rawSPS, rawPPS, 4);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 18, 162, 109, 109 })]
	public static AvcCBox createAvcCFromBytes(ByteBuffer codecPrivate)
	{
		List rawSPS = getRawSPS(codecPrivate.duplicate());
		List rawPPS = getRawPPS(codecPrivate.duplicate());
		AvcCBox result = createAvcCFromPS(rawSPS, rawPPS, 4);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 12, 98, 119, 104, 104, 104, 104 })]
	public static SampleEntry createMOVSampleEntryFromSpsPps(SeqParameterSet initSPS, PictureParameterSet initPPS, int nalLengthSize)
	{
		ByteBuffer bb1 = ByteBuffer.allocate(512);
		ByteBuffer bb2 = ByteBuffer.allocate(512);
		initSPS.write(bb1);
		initPPS.write(bb2);
		bb1.flip();
		bb2.flip();
		SampleEntry result = createMOVSampleEntryFromBuffer(bb1, bb2, nalLengthSize);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 9, 162, 136, 107, 109, 113, 103, 104, 105,
		144, 99
	})]
	public static bool iFrame(ByteBuffer _data)
	{
		ByteBuffer data = _data.duplicate();
		ByteBuffer segment;
		while ((segment = nextNALUnit(data)) != null)
		{
			NALUnitType type = NALUnit.read(segment).type;
			if (type == NALUnitType.___003C_003EIDR_SLICE || type == NALUnitType.___003C_003ENON_IDR_SLICE)
			{
				unescapeNAL(segment);
				BitReader reader = BitReader.createBitReader(segment);
				SliceHeader part1 = SliceHeaderReader.readPart1(reader);
				return part1.sliceType == SliceType.___003C_003EI;
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 0, 66, 104, 104, 110, 105 })]
	public static void saveRawFrame(ByteBuffer data, AvcCBox avcC, File f)
	{
		FileChannelWrapper raw = NIOUtils.writableChannel(f);
		saveStreamParams(avcC, raw);
		((WritableByteChannel)raw).write(data.duplicate());
		((Channel)raw).close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 239, 130, 108, 104, 104 })]
	public static ByteBuffer getAvcCData(AvcCBox avcC)
	{
		ByteBuffer bb = ByteBuffer.allocate(2048);
		avcC.doWrite(bb);
		bb.flip();
		return bb;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 237, 98, 114, 105, 136 })]
	public static AvcCBox parseAVCC(VideoSampleEntry vse)
	{
		Box lb = NodeBox.findFirst(vse, ClassLiteral<Box>.Value, "avcC");
		if (lb is AvcCBox)
		{
			return (AvcCBox)lb;
		}
		AvcCBox result = parseAVCCFromBuffer(((Box.LeafBox)lb).getData().duplicate());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(678)]
	public static ByteBuffer avcCToAnnexB(AvcCBox avcC)
	{
		ByteBuffer result = saveCodecPrivate(avcC.getSpsList(), avcC.getPpsList());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;>;I)Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;")]
	[LineNumberTable(new byte[] { 158, 219, 66, 124, 106, 99, 99 })]
	public static PictureParameterSet findPPS(List ppss, int id)
	{
		Iterator iterator = ppss.iterator();
		while (iterator.hasNext())
		{
			PictureParameterSet pps = (PictureParameterSet)iterator.next();
			if (pps.picParameterSetId == id)
			{
				return pps;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;>;I)Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;")]
	[LineNumberTable(new byte[] { 158, 217, 66, 124, 106, 99, 99 })]
	public static SeqParameterSet findSPS(List spss, int id)
	{
		Iterator iterator = spss.iterator();
		while (iterator.hasNext())
		{
			SeqParameterSet sps = (SeqParameterSet)iterator.next();
			if (sps.seqParameterSetId == id)
			{
				return sps;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;>;")]
	[LineNumberTable(new byte[] { 158, 188, 98, 103, 124, 115, 99 })]
	public static List readSPSFromBufferList(List spsList)
	{
		ArrayList result = new ArrayList();
		Iterator iterator = spsList.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator.next();
			((List)result).add((object)readSPS(NIOUtils.duplicate(byteBuffer)));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;>;")]
	[LineNumberTable(new byte[] { 158, 186, 98, 103, 124, 115, 99 })]
	public static List readPPSFromBufferList(List ppsList)
	{
		ArrayList result = new ArrayList();
		Iterator iterator = ppsList.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator.next();
			((List)result).add((object)readPPS(NIOUtils.duplicate(byteBuffer)));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;>;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(new byte[] { 158, 184, 98, 103, 124, 112, 99 })]
	public static List writePPSList(List allPps)
	{
		ArrayList result = new ArrayList();
		Iterator iterator = allPps.iterator();
		while (iterator.hasNext())
		{
			PictureParameterSet pps = (PictureParameterSet)iterator.next();
			((List)result).add((object)writePPS(pps, 64));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;>;)Ljava/util/List<Ljava/nio/ByteBuffer;>;")]
	[LineNumberTable(new byte[] { 158, 182, 98, 103, 124, 115, 99 })]
	public static List writeSPSList(List allSps)
	{
		ArrayList result = new ArrayList();
		Iterator iterator = allSps.iterator();
		while (iterator.hasNext())
		{
			SeqParameterSet sps = (SeqParameterSet)iterator.next();
			((List)result).add((object)writeSPS(sps, 256));
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Lorg/jcodec/common/io/FileChannelWrapper;[Lorg/jcodec/codecs/h264/io/model/SeqParameterSet;[Lorg/jcodec/codecs/h264/io/model/PictureParameterSet;Ljava/util/List<Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		158, 180, 130, 104, 101, 104, 105, 243, 60, 231,
		71, 104, 101, 104, 105, 243, 60, 231, 71, 127,
		1, 104, 111, 99
	})]
	public static void dumpFrame(FileChannelWrapper ch, SeqParameterSet[] values, PictureParameterSet[] values2, List nalUnits)
	{
		for (int j = 0; j < (nint)values.LongLength; j++)
		{
			SeqParameterSet sps = values[j];
			NIOUtils.writeInt(ch, 1);
			NIOUtils.writeByte(ch, 103);
			ch.write(writeSPS(sps, 128));
		}
		for (int i = 0; i < (nint)values2.LongLength; i++)
		{
			PictureParameterSet pps = values2[i];
			NIOUtils.writeInt(ch, 1);
			NIOUtils.writeByte(ch, 104);
			ch.write(writePPS(pps, 256));
		}
		Iterator iterator = nalUnits.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator.next();
			NIOUtils.writeInt(ch, 1);
			ch.write(byteBuffer.duplicate());
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 175, 162, 119, 104, 104, 104, 136, 106, 108 })]
	public static void toNAL(ByteBuffer codecPrivate, SeqParameterSet sps, PictureParameterSet pps)
	{
		ByteBuffer bb1 = ByteBuffer.allocate(512);
		ByteBuffer bb2 = ByteBuffer.allocate(512);
		sps.write(bb1);
		pps.write(bb2);
		bb1.flip();
		bb2.flip();
		putNAL(codecPrivate, bb1, 103);
		putNAL(codecPrivate, bb2, 104);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/nio/ByteBuffer;Ljava/util/List<Ljava/nio/ByteBuffer;>;Ljava/util/List<Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[] { 158, 172, 130, 124, 108, 124, 108 })]
	public static void toNALList(ByteBuffer codecPrivate, List spsList2, List ppsList2)
	{
		Iterator iterator = spsList2.iterator();
		while (iterator.hasNext())
		{
			ByteBuffer byteBuffer2 = (ByteBuffer)iterator.next();
			putNAL(codecPrivate, byteBuffer2, 103);
		}
		Iterator iterator2 = ppsList2.iterator();
		while (iterator2.hasNext())
		{
			ByteBuffer byteBuffer = (ByteBuffer)iterator2.next();
			putNAL(codecPrivate, byteBuffer, 104);
		}
	}
}
