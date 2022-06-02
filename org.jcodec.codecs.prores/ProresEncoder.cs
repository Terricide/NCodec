using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.dct;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.prores;

public class ProresEncoder : VideoEncoder
{
	public sealed class Profile : Object
	{
		internal static Profile ___003C_003EPROXY;

		internal static Profile ___003C_003ELT;

		internal static Profile ___003C_003ESTANDARD;

		internal static Profile ___003C_003EHQ;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
		private static Profile[] _values;

		[Modifiers(Modifiers.Final)]
		internal string name;

		[Modifiers(Modifiers.Final)]
		internal int[] qmatLuma;

		[Modifiers(Modifiers.Final)]
		internal int[] qmatChroma;

		internal string ___003C_003Efourcc;

		[Modifiers(Modifiers.Final)]
		internal int bitrate;

		[Modifiers(Modifiers.Final)]
		internal int firstQp;

		[Modifiers(Modifiers.Final)]
		internal int lastQp;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static Profile PROXY
		{
			[HideFromJava]
			get
			{
				return ___003C_003EPROXY;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static Profile LT
		{
			[HideFromJava]
			get
			{
				return ___003C_003ELT;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static Profile STANDARD
		{
			[HideFromJava]
			get
			{
				return ___003C_003ESTANDARD;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
		public static Profile HQ
		{
			[HideFromJava]
			get
			{
				return ___003C_003EHQ;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Final)]
		public string fourcc
		{
			[HideFromJava]
			get
			{
				return ___003C_003Efourcc;
			}
			[HideFromJava]
			private set
			{
				___003C_003Efourcc = value;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 123, 162, 105, 104, 104, 104, 105, 105, 105,
			105
		})]
		private Profile(string name, int[] qmatLuma, int[] qmatChroma, string fourcc, int bitrate, int firstQp, int lastQp)
		{
			this.name = name;
			this.qmatLuma = qmatLuma;
			this.qmatChroma = qmatChroma;
			___003C_003Efourcc = fourcc;
			this.bitrate = bitrate;
			this.firstQp = firstQp;
			this.lastQp = lastQp;
		}

		[LineNumberTable(57)]
		public static Profile[] values()
		{
			return _values;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 127, 98, 104, 117, 106, 4, 199 })]
		public static Profile valueOf(string name)
		{
			string nameU = String.instancehelper_toUpperCase(name);
			Profile[] array = _values;
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				Profile profile2 = array[i];
				if (String.instancehelper_equals(name, nameU))
				{
					return profile2;
				}
			}
			return null;
		}

		[LineNumberTable(new byte[]
		{
			159, 130, 66, 127, 7, 127, 8, 159, 7, 159,
			7
		})]
		static Profile()
		{
			___003C_003EPROXY = new Profile("PROXY", ProresConsts.___003C_003EQMAT_LUMA_APCO, ProresConsts.___003C_003EQMAT_CHROMA_APCO, "apco", 1000, 4, 8);
			___003C_003ELT = new Profile("LT", ProresConsts.___003C_003EQMAT_LUMA_APCS, ProresConsts.___003C_003EQMAT_CHROMA_APCS, "apcs", 2100, 1, 9);
			___003C_003ESTANDARD = new Profile("STANDARD", ProresConsts.___003C_003EQMAT_LUMA_APCN, ProresConsts.___003C_003EQMAT_CHROMA_APCN, "apcn", 3500, 1, 6);
			___003C_003EHQ = new Profile("HQ", ProresConsts.___003C_003EQMAT_LUMA_APCH, ProresConsts.___003C_003EQMAT_CHROMA_APCH, "apch", 5400, 1, 6);
			_values = new Profile[4] { ___003C_003EPROXY, ___003C_003ELT, ___003C_003ESTANDARD, ___003C_003EHQ };
		}
	}

	private const int LOG_DEFAULT_SLICE_MB_WIDTH = 3;

	private const int DEFAULT_SLICE_MB_WIDTH = 8;

	protected internal Profile profile;

	private int[][] scaledLuma;

	private int[][] scaledChroma;

	private bool interlaced;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 97, 67 })]
	public static ProresEncoder createProresEncoder(string profile, bool interlaced)
	{
		ProresEncoder result = new ProresEncoder((profile != null) ? Profile.valueOf(profile) : Profile.___003C_003EHQ, interlaced);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 65, 67, 105, 104, 118, 118, 104 })]
	public ProresEncoder(Profile profile, bool interlaced)
	{
		this.profile = profile;
		scaledLuma = scaleQMat(profile.qmatLuma, 1, 16);
		scaledChroma = scaleQMat(profile.qmatChroma, 1, 16);
		this.interlaced = interlaced;
	}

	[LineNumberTable(new byte[]
	{
		159, 115, 66, 104, 103, 107, 104, 45, 7, 231,
		69
	})]
	private int[][] scaleQMat(int[] qmatLuma, int start, int count)
	{
		int[][] result = new int[count][];
		for (int i = 0; i < count; i++)
		{
			result[i] = new int[(nint)qmatLuma.LongLength];
			for (int j = 0; j < (nint)qmatLuma.LongLength; j++)
			{
				result[i][j] = qmatLuma[j] * (i + start);
			}
		}
		return result;
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(143)]
	private static int qScale(int[] qMat, int ind, int val)
	{
		int num = qMat[ind];
		return (num != -1) ? (val / num) : (-val);
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(147)]
	private static int toGolumb(int val)
	{
		return (val << 1) ^ (val >> 31);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 113, 130, 116, 104, 102, 144, 104, 115, 103,
		40, 135, 104, 137, 111, 115, 40, 137, 104, 158,
		105, 40, 137, 138
	})]
	public static void writeCodeword(BitWriter writer, Codebook codebook, int val)
	{
		int firstExp = codebook.switchBits + 1 << codebook.riceOrder;
		if (val >= firstExp)
		{
			val -= firstExp;
			val += 1 << codebook.expOrder;
			int exp = MathUtil.log2(val);
			int zeros = exp - codebook.expOrder + codebook.switchBits + 1;
			for (int k = 0; k < zeros; k++)
			{
				writer.write1Bit(0);
			}
			writer.write1Bit(1);
			writer.writeNBit(val, exp);
		}
		else if (codebook.riceOrder > 0)
		{
			for (int j = 0; j < val >> codebook.riceOrder; j++)
			{
				writer.write1Bit(0);
			}
			writer.write1Bit(1);
			writer.writeNBit(val & ((1 << codebook.riceOrder) - 1), codebook.riceOrder);
		}
		else
		{
			for (int i = 0; i < val; i++)
			{
				writer.write1Bit(0);
			}
			writer.write1Bit(1);
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 102, 98, 102 })]
	public static int getLevel(int val)
	{
		int sign = val >> 31;
		return (val ^ sign) - sign;
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(157)]
	private static int diffSign(int val, int sign)
	{
		return (val >> 31) ^ sign;
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 105, 162, 100, 99 })]
	private static int toGolumbSign(int val, int sign)
	{
		if (val == 0)
		{
			return 0;
		}
		return (val << 1) + sign;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 101, 130, 114, 146, 104, 108, 115, 103, 119,
		118, 100, 103, 228, 57, 241, 73
	})]
	internal static void writeDCCoeffs(BitWriter bits, int[] qMat, int[] _in, int blocksPerSlice)
	{
		int prevDc = qScale(qMat, 0, _in[0] - 16384);
		writeCodeword(bits, ProresConsts.firstDCCodebook, toGolumb(prevDc));
		int code = 5;
		int sign = 0;
		int idx = 64;
		int i = 1;
		while (i < blocksPerSlice)
		{
			int newDc = qScale(qMat, 0, _in[idx] - 16384);
			int delta = newDc - prevDc;
			int newCode = toGolumbSign(getLevel(delta), diffSign(delta, sign));
			writeCodeword(bits, ProresConsts.___003C_003EdcCodebooks[Math.min(code, 6)], newCode);
			code = newCode;
			sign = delta >> 31;
			prevDc = newDc;
			i++;
			idx += 64;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[]
	{
		159, 97, 162, 99, 131, 99, 107, 103, 108, 116,
		101, 135, 118, 99, 99, 106, 121, 100, 238, 53,
		12, 234, 81
	})]
	internal static void writeACCoeffs(BitWriter bits, int[] qMat, int[] _in, int blocksPerSlice, int[] scan, int maxCoeff)
	{
		int prevRun = 4;
		int prevLevel = 2;
		int run = 0;
		for (int i = 1; i < maxCoeff; i++)
		{
			int indp = scan[i];
			for (int j = 0; j < blocksPerSlice; j++)
			{
				int val = qScale(qMat, indp, _in[(j << 6) + indp]);
				if (val == 0)
				{
					run++;
					continue;
				}
				writeCodeword(bits, ProresConsts.___003C_003ErunCodebooks[Math.min(prevRun, 15)], run);
				prevRun = run;
				run = 0;
				int level = getLevel(val);
				writeCodeword(bits, ProresConsts.___003C_003ElevCodebooks[Math.min(prevLevel, 9)], level - 1);
				prevLevel = level;
				bits.write1Bit(MathUtil.sign(val));
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 56, 161, 68, 121, 100, 106, 113, 190, 113,
		99, 177
	})]
	private Picture splitSlice(Picture result, int mbX, int mbY, int sliceMbCount, bool @unsafe, int vStep, int vOffset)
	{
		Picture @out = Picture.createCroppedHiBD(sliceMbCount << 4, 16, result.getLowBitsNum(), ColorSpace.___003C_003EYUV422, null);
		if (@unsafe)
		{
			int mbHeightPix = 16 << vStep;
			Picture filled = Picture.create(sliceMbCount << 4, mbHeightPix, ColorSpace.___003C_003EYUV422);
			ImageOP.subImageWithFillPic8(result, filled, new Rect(mbX << 4, mbY << 4 + vStep, sliceMbCount << 4, mbHeightPix));
			split(filled, @out, 0, 0, sliceMbCount, vStep, vOffset);
		}
		else
		{
			split(result, @out, mbX, mbY, sliceMbCount, vStep, vOffset);
		}
		return @out;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 89, 130, 104, 48, 167, 100, 104, 48, 231,
		69, 105, 44, 169
	})]
	private void dctOnePlane(int blocksPerSlice, byte[] src, byte[] hibd, int[] dst)
	{
		for (int j = 0; j < (nint)src.LongLength; j++)
		{
			dst[j] = src[j] + 128 << 2;
		}
		if (hibd != null)
		{
			for (int k = 0; k < (nint)src.LongLength; k++)
			{
				int num = k;
				dst[num] += hibd[k];
			}
		}
		for (int i = 0; i < blocksPerSlice; i++)
		{
			SimpleIDCT10Bit.fdctProres10(dst, i << 6);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Protected | Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 72, 66, 117, 117, 117 })]
	protected internal static void encodeSliceData(ByteBuffer @out, int[] qmatLuma, int[] qmatChroma, int[] scan, int sliceMbCount, int[][] ac, int qp, int[] sizes)
	{
		sizes[0] = onePlane(@out, sliceMbCount << 2, qmatLuma, scan, ac[0]);
		sizes[1] = onePlane(@out, sliceMbCount << 1, qmatChroma, scan, ac[1]);
		sizes[2] = onePlane(@out, sliceMbCount << 1, qmatChroma, scan, ac[2]);
	}

	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(274)]
	internal static int bits(int[] sizes)
	{
		return sizes[0] + sizes[1] + sizes[2] << 3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 71, 130, 104, 104, 108, 103 })]
	internal static int onePlane(ByteBuffer @out, int blocksPerSlice, int[] qmatLuma, int[] scan, int[] data)
	{
		int rem = @out.position();
		BitWriter bits = new BitWriter(@out);
		encodeOnePlane(bits, blocksPerSlice, qmatLuma, scan, data);
		bits.flush();
		return @out.position() - rem;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 90, 66, 107, 112 })]
	internal static void encodeOnePlane(BitWriter bits, int blocksPerSlice, int[] qMat, int[] scan, int[] _in)
	{
		writeDCCoeffs(bits, qMat, _in, blocksPerSlice);
		writeACCoeffs(bits, qMat, _in, blocksPerSlice, scan, 64);
	}

	[LineNumberTable(new byte[] { 159, 58, 130, 101, 103, 44, 167 })]
	private int calcNSlices(int mbWidth, int mbHeight)
	{
		int nSlices = mbWidth >> 3;
		for (int i = 0; i < 3; i++)
		{
			nSlices += (mbWidth >> i) & 1;
		}
		return nSlices * mbHeight;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 60, 130, 99, 108, 105, 106, 108 })]
	public static void writePictureHeader(int logDefaultSliceMbWidth, int nSlices, ByteBuffer @out)
	{
		int headerLen = 8;
		@out.put((byte)(sbyte)(headerLen << 3));
		@out.putInt(0);
		@out.putShort((short)nSlices);
		@out.put((byte)(sbyte)(logDefaultSliceMbWidth << 4));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 84, 65, 68, 117, 127, 13, 104, 105, 124,
		124, 156, 115, 106, 138, 133, 106, 105, 105, 105,
		105, 124, 158, 103, 106, 124, 127, 4, 155, 103,
		106, 124, 187, 108, 110, 142
	})]
	protected internal virtual int encodeSlice(ByteBuffer @out, int[][] scaledLuma, int[][] scaledChroma, int[] scan, int sliceMbCount, int mbX, int mbY, Picture result, int prevQp, int mbWidth, int mbHeight, bool @unsafe, int vStep, int vOffset)
	{
		Picture striped = splitSlice(result, mbX, mbY, sliceMbCount, @unsafe, vStep, vOffset);
		int[][] ac = new int[3][]
		{
			new int[sliceMbCount << 8],
			new int[sliceMbCount << 7],
			new int[sliceMbCount << 7]
		};
		byte[][] data = striped.getData();
		byte[][] lowBits = striped.getLowBits();
		dctOnePlane(sliceMbCount << 2, data[0], (lowBits != null) ? lowBits[0] : null, ac[0]);
		dctOnePlane(sliceMbCount << 1, data[1], (lowBits != null) ? lowBits[1] : null, ac[1]);
		dctOnePlane(sliceMbCount << 1, data[2], (lowBits != null) ? lowBits[2] : null, ac[2]);
		int est = (sliceMbCount >> 2) * profile.bitrate;
		int low = est - (est >> 3);
		int high = est + (est >> 3);
		int qp = prevQp;
		@out.put(48);
		ByteBuffer fork = @out.duplicate();
		NIOUtils.skip(@out, 5);
		int rem = @out.position();
		int[] sizes = new int[3];
		encodeSliceData(@out, scaledLuma[qp - 1], scaledChroma[qp - 1], scan, sliceMbCount, ac, qp, sizes);
		if (bits(sizes) > high && qp < profile.lastQp)
		{
			do
			{
				qp++;
				@out.position(rem);
				encodeSliceData(@out, scaledLuma[qp - 1], scaledChroma[qp - 1], scan, sliceMbCount, ac, qp, sizes);
			}
			while (bits(sizes) > high && qp < profile.lastQp);
		}
		else if (bits(sizes) < low && qp > profile.firstQp)
		{
			do
			{
				qp += -1;
				@out.position(rem);
				encodeSliceData(@out, scaledLuma[qp - 1], scaledChroma[qp - 1], scan, sliceMbCount, ac, qp, sizes);
			}
			while (bits(sizes) < low && qp > profile.firstQp);
		}
		fork.put((byte)(sbyte)qp);
		fork.putShort((short)sizes[0]);
		fork.putShort((short)sizes[1]);
		return qp;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 51, 66, 104, 136, 104, 136, 126, 126, 158,
		105, 126, 126, 159, 1
	})]
	private void split(Picture src, Picture dst, int mbX, int mbY, int sliceMbCount, int vStep, int vOffset)
	{
		byte[][] inData = src.getData();
		byte[][] inhbdData = src.getLowBits();
		byte[][] outData = dst.getData();
		byte[][] outhbdData = dst.getLowBits();
		doSplit(inData[0], outData[0], src.getPlaneWidth(0), mbX, mbY, sliceMbCount, 0, vStep, vOffset);
		doSplit(inData[1], outData[1], src.getPlaneWidth(1), mbX, mbY, sliceMbCount, 1, vStep, vOffset);
		doSplit(inData[2], outData[2], src.getPlaneWidth(2), mbX, mbY, sliceMbCount, 1, vStep, vOffset);
		if (src.getLowBits() != null)
		{
			doSplit(inhbdData[0], outhbdData[0], src.getPlaneWidth(0), mbX, mbY, sliceMbCount, 0, vStep, vOffset);
			doSplit(inhbdData[1], outhbdData[1], src.getPlaneWidth(1), mbX, mbY, sliceMbCount, 1, vStep, vOffset);
			doSplit(inhbdData[2], outhbdData[2], src.getPlaneWidth(2), mbX, mbY, sliceMbCount, 1, vStep, vOffset);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 47, 162, 99, 126, 138, 107, 108, 156, 101,
		113, 184, 111, 236, 54, 234, 76
	})]
	private void doSplit(byte[] _in, byte[] @out, int stride, int mbX, int mbY, int sliceMbCount, int chroma, int vStep, int vOffset)
	{
		int outOff = 0;
		int off = (mbY << 4) * (stride << vStep) + (mbX << 4 - chroma) + stride * vOffset;
		stride <<= vStep;
		for (int i = 0; i < sliceMbCount; i++)
		{
			splitBlock(_in, stride, off, @out, outOff);
			splitBlock(_in, stride, off + (stride << 3), @out, outOff + (128 >> chroma));
			if (chroma == 0)
			{
				splitBlock(_in, stride, off + 8, @out, outOff + 64);
				splitBlock(_in, stride, off + (stride << 3) + 8, @out, outOff + 192);
			}
			outOff += 256 >> chroma;
			off += 16 >> chroma;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 42, 130, 103, 103, 52, 135, 232, 61, 231,
		69
	})]
	private void splitBlock(byte[] y, int stride, int off, byte[] @out, int outOff)
	{
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				int num = outOff;
				outOff++;
				int num2 = off;
				off++;
				@out[num] = y[num2];
			}
			off += stride - 8;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 35, 98, 103, 114, 159, 3, 105, 137, 159,
		3, 111, 143, 157, 159, 8, 105, 109, 111
	})]
	public static void writeFrameHeader(ByteBuffer outp, ProresConsts.FrameHeader header)
	{
		int headerSize = 148;
		outp.putInt(headerSize + 8 + header.payloadSize);
		outp.put(new byte[4] { 105, 99, 112, 102 });
		outp.putShort((short)headerSize);
		outp.putShort(0);
		outp.put(new byte[4] { 97, 112, 108, 48 });
		outp.putShort((short)header.width);
		outp.putShort((short)header.height);
		outp.put((byte)(sbyte)((header.frameType != 0) ? 135u : 131u));
		outp.put(new byte[6] { 0, 2, 2, 6, 32, 0 });
		outp.put(3);
		writeQMat(outp, header.qMatLuma);
		writeQMat(outp, header.qMatChroma);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 68, 66, 110, 102, 106, 112, 142, 107, 106,
		105, 140, 100, 106, 108, 100, 100, 105, 105, 137,
		105, 127, 4, 127, 5, 159, 11, 115, 150, 104,
		230, 48, 236, 82
	})]
	protected internal virtual void encodePicture(ByteBuffer @out, int[][] scaledLuma, int[][] scaledChroma, int[] scan, Picture picture, int vStep, int vOffset)
	{
		int mbWidth = picture.getWidth() + 15 >> 4;
		int shift = 4 + vStep;
		int round = (1 << shift) - 1;
		int mbHeight = picture.getHeight() + round >> shift;
		int qp = profile.firstQp;
		int nSlices = calcNSlices(mbWidth, mbHeight);
		writePictureHeader(3, nSlices, @out);
		ByteBuffer fork = @out.duplicate();
		NIOUtils.skip(@out, nSlices << 1);
		int i = 0;
		int[] total = new int[nSlices];
		for (int mbY = 0; mbY < mbHeight; mbY++)
		{
			int mbX = 0;
			for (int sliceMbCount = 8; mbX < mbWidth; mbX += sliceMbCount)
			{
				while (mbWidth - mbX < sliceMbCount)
				{
					sliceMbCount >>= 1;
				}
				int sliceStart = @out.position();
				int height = picture.getHeight();
				int unsafeBottom = ((16 != -1 && height % 16 != 0 && mbY == mbHeight - 1) ? 1 : 0);
				int width = picture.getWidth();
				int unsafeRight = ((16 != -1 && width % 16 != 0 && mbX + sliceMbCount == mbWidth) ? 1 : 0);
				qp = encodeSlice(@out, scaledLuma, scaledChroma, scan, sliceMbCount, mbX, mbY, picture, qp, mbWidth, mbHeight, (unsafeBottom != 0 || unsafeRight != 0) ? true : false, vStep, vOffset);
				fork.putShort((short)(@out.position() - sliceStart));
				int num = i;
				i++;
				total[num] = (short)(@out.position() - sliceStart);
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Static | Modifiers.Final)]
	[LineNumberTable(new byte[] { 159, 30, 162, 104, 44, 135 })]
	internal static void writeQMat(ByteBuffer @out, int[] qmat)
	{
		for (int i = 0; i < 64; i++)
		{
			@out.put((byte)(sbyte)qmat[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 40, 162, 104, 136, 118, 191, 31, 127, 4,
		105, 127, 4, 104, 142
	})]
	public override EncodedFrame encodeFrame(Picture pic, ByteBuffer buffer)
	{
		ByteBuffer @out = buffer.duplicate();
		ByteBuffer fork = @out.duplicate();
		int[] scan = ((!interlaced) ? ProresConsts.progressive_scan : ProresConsts.interlaced_scan);
		writeFrameHeader(@out, new ProresConsts.FrameHeader(0, pic.getCroppedWidth(), pic.getCroppedHeight(), interlaced ? 1 : 0, topFieldFirst: true, scan, profile.qmatLuma, profile.qmatChroma, 2));
		encodePicture(@out, scaledLuma, scaledChroma, scan, pic, interlaced ? 1 : 0, 0);
		if (interlaced)
		{
			encodePicture(@out, scaledLuma, scaledChroma, scan, pic, interlaced ? 1 : 0, 1);
		}
		@out.flip();
		fork.putInt(@out.remaining());
		EncodedFrame result = new EncodedFrame(@out, keyFrame: true);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(457)]
	public override ColorSpace[] getSupportedColorSpaces()
	{
		return new ColorSpace[1] { ColorSpace.___003C_003EYUV422 };
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(462)]
	public override int estimateBufferSize(Picture frame)
	{
		return 3 * frame.getWidth() * frame.getHeight() / 2;
	}
}
