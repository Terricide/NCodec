using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.lang.reflect;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.codecs.mpeg12;
using org.jcodec.codecs.mpeg12.bitstream;
using org.jcodec.common.io;
using org.jcodec.common.tools;
using org.jcodec.platform;

namespace org.jcodec.containers.mps;

public class MPSDump : java.lang.Object
{
	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class MPEGVideoAnalyzer : java.lang.Object
	{
		private sealed class ___003CCallerID_003E : CallerID
		{
			internal ___003CCallerID_003E()
			{
			}
		}

		private int nextStartCode;

		private ByteBuffer bselPayload;

		private int bselStartCode;

		private int bselOffset;

		private int bselBufInd;

		private int prevBufSize;

		private int curBufInd;

		private PictureHeader picHeader;

		private SequenceHeader sequenceHeader;

		private PictureCodingExtension pictureCodingExtension;

		private SequenceExtension sequenceExtension;

		[SpecialName]
		private static CallerID ___003CcallerID_003E;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 95, 130, 104, 104, 108, 119, 125, 123, 109,
			109, 105, 111, 116, 153, 109, 109, 113, 178, 111,
			104
		})]
		private void analyzeMpegVideoPacket(ByteBuffer buffer)
		{
			int pos = buffer.position();
			int bufSize = buffer.remaining();
			while (buffer.hasRemaining())
			{
				bselPayload.put((byte)(sbyte)(nextStartCode >> 24));
				nextStartCode = (nextStartCode << 8) | ((sbyte)buffer.get() & 0xFF);
				if (nextStartCode < 256 || nextStartCode > 440)
				{
					continue;
				}
				bselPayload.flip();
				bselPayload.getInt();
				if (bselStartCode != 0)
				{
					if (bselBufInd != curBufInd)
					{
						bselOffset -= prevBufSize;
					}
					dumpBSEl(bselStartCode, bselOffset, bselPayload);
				}
				bselPayload.clear();
				bselStartCode = nextStartCode;
				bselOffset = buffer.position() - 4 - pos;
				bselBufInd = curBufInd;
			}
			curBufInd++;
			prevBufSize = bufSize;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 89, 130, 127, 14, 105, 109, 105, 159, 24,
			105, 106, 105, 106, 105, 138, 144, 114
		})]
		private void dumpBSEl(int mark, int offset, ByteBuffer b)
		{
			java.lang.System.@out.print(java.lang.String.format("marker: 0x%02x [@%d] ( ", Integer.valueOf(mark), Integer.valueOf(offset)));
			if (mark == 256)
			{
				dumpPictureHeader(b);
			}
			else if (mark <= 431)
			{
				java.lang.System.@out.print(MainUtils.colorBright(java.lang.String.format("slice @0x%02x", Integer.valueOf(mark - 257)), MainUtils.ANSIColor.___003C_003EBLACK, bright: true));
			}
			else
			{
				switch (mark)
				{
				case 435:
					dumpSequenceHeader(b);
					break;
				case 437:
					dumpExtension(b);
					break;
				case 440:
					dumpGroupHeader(b);
					break;
				default:
					java.lang.System.@out.print("--");
					break;
				}
			}
			java.lang.System.@out.println(" )");
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 48, 98, 109, 104, 191, 101 })]
		private void dumpPictureHeader(ByteBuffer b)
		{
			picHeader = PictureHeader.read(b);
			pictureCodingExtension = null;
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("picture header <type:").append((picHeader.picture_coding_type == 1) ? "I" : ((picHeader.picture_coding_type != 2) ? "B" : "P")).append(", temp_ref:")
				.append(picHeader.temporal_reference)
				.append(">")
				.toString(), MainUtils.ANSIColor.___003C_003EBROWN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 50, 98, 104, 104, 104, 109, 125 })]
		private void dumpSequenceHeader(ByteBuffer b)
		{
			picHeader = null;
			pictureCodingExtension = null;
			sequenceExtension = null;
			sequenceHeader = SequenceHeader.read(b);
			java.lang.System.@out.print(MainUtils.colorBright("sequence header", MainUtils.ANSIColor.___003C_003EBLUE, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 84, 98, 104, 105, 108, 108, 159, 0, 109,
			109, 134, 109, 134, 109, 134, 191, 22, 191, 22,
			159, 39, 109, 134, 109, 134, 119, 222, 109, 109,
			131, 109, 131, 109, 131, 191, 19
		})]
		private void dumpExtension(ByteBuffer b)
		{
			BitReader _in = BitReader.createBitReader(b);
			int extType = _in.readNBit(4);
			if (picHeader == null)
			{
				if (sequenceHeader != null)
				{
					switch (extType)
					{
					case 1:
						sequenceExtension = SequenceExtension.read(_in);
						dumpSequenceExtension(sequenceExtension);
						break;
					case 5:
						dumpSequenceScalableExtension(SequenceScalableExtension.read(_in));
						break;
					case 2:
						dumpSequenceDisplayExtension(SequenceDisplayExtension.read(_in));
						break;
					default:
						java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("extension ").append(extType).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
						break;
					}
				}
				else
				{
					java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("dangling extension ").append(extType).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
				}
				return;
			}
			switch (extType)
			{
			case 3:
				dumpQuantMatrixExtension(QuantMatrixExtension.read(_in));
				break;
			case 4:
				dumpCopyrightExtension(CopyrightExtension.read(_in));
				break;
			case 7:
				if (sequenceHeader != null && pictureCodingExtension != null)
				{
					dumpPictureDisplayExtension(PictureDisplayExtension.read(_in, sequenceExtension, pictureCodingExtension));
				}
				break;
			case 8:
				pictureCodingExtension = PictureCodingExtension.read(_in);
				dumpPictureCodingExtension(pictureCodingExtension);
				break;
			case 9:
				dumpPictureSpatialScalableExtension(PictureSpatialScalableExtension.read(_in));
				break;
			case 16:
				dumpPictureTemporalScalableExtension(PictureTemporalScalableExtension.read(_in));
				break;
			default:
				java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("extension ").append(extType).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
				break;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 52, 98, 104, 127, 12, 108, 31, 46, 205 })]
		private void dumpGroupHeader(ByteBuffer b)
		{
			GOPHeader gopHeader = GOPHeader.read(b);
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("group header <closed:").append(gopHeader.isClosedGop()).append(",broken link:")
				.append(gopHeader.isBrokenLink())
				.append((gopHeader.getTimeCode() == null) ? "" : new StringBuilder().append(",timecode:").append(gopHeader.getTimeCode().toString()).toString())
				.append(">")
				.toString(), MainUtils.ANSIColor.___003C_003EMAGENTA, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 69, 130, 127, 25 })]
		private void dumpSequenceExtension(SequenceExtension read)
		{
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("sequence extension ").append(dumpBin(read)).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 70, 98, 159, 25 })]
		private void dumpSequenceScalableExtension(SequenceScalableExtension read)
		{
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("sequence scalable extension ").append(dumpBin(read)).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 71, 66, 159, 25 })]
		private void dumpSequenceDisplayExtension(SequenceDisplayExtension read)
		{
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("sequence display extension ").append(dumpBin(read)).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 62, 130, 119, 59, 136 })]
		private void dumpQuantMatrixExtension(QuantMatrixExtension read)
		{
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("quant matrix extension ").append(dumpBin(read)).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 63, 130, 127, 25 })]
		private void dumpCopyrightExtension(CopyrightExtension read)
		{
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("copyright extension ").append(dumpBin(read)).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 64, 98, 159, 25 })]
		private void dumpPictureDisplayExtension(PictureDisplayExtension read)
		{
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("picture display extension ").append(dumpBin(read)).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 65, 66, 159, 25 })]
		private void dumpPictureCodingExtension(PictureCodingExtension read)
		{
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("picture coding extension ").append(dumpBin(read)).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 67, 162, 159, 25 })]
		private void dumpPictureSpatialScalableExtension(PictureSpatialScalableExtension read)
		{
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("picture spatial scalable extension ").append(dumpBin(read)).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 68, 130, 159, 25 })]
		private void dumpPictureTemporalScalableExtension(PictureTemporalScalableExtension read)
		{
			java.lang.System.@out.print(MainUtils.colorBright(new StringBuilder().append("picture temporal scalable extension ").append(dumpBin(read)).toString(), MainUtils.ANSIColor.___003C_003EGREEN, bright: true));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 61, 162, 103, 109, 109, 107, 127, 3, 102,
			127, 16, 144, 159, 14, 38, 99, 166, 113, 101,
			146, 159, 7, 35, 163, 104, 237, 44, 234, 86,
			109
		})]
		private string dumpBin(object read)
		{
			StringBuilder bldr = new StringBuilder();
			bldr.append("<");
			Field[] fields = Platform.getFields(java.lang.Object.instancehelper_getClass(read));
			for (int i = 0; i < (nint)fields.LongLength; i++)
			{
				if (!Modifier.isPublic(fields[i].getModifiers()) || Modifier.isStatic(fields[i].getModifiers()))
				{
					continue;
				}
				bldr.append(new StringBuilder().append(convertName(fields[i].getName())).append(": ").toString());
				java.lang.Exception ex2;
				java.lang.Exception ex4;
				if (fields[i].getType().isPrimitive())
				{
					try
					{
						bldr.append(fields[i].get(read, MPEGVideoAnalyzer.___003CGetCallerID_003E()));
					}
					catch (System.Exception x)
					{
						java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
						if (ex == null)
						{
							throw;
						}
						ex2 = ex;
						goto IL_00be;
					}
				}
				else
				{
					try
					{
						object val = fields[i].get(read, MPEGVideoAnalyzer.___003CGetCallerID_003E());
						if (val != null)
						{
							bldr.append(dumpBin(val));
						}
						else
						{
							bldr.append("N/A");
						}
					}
					catch (System.Exception x2)
					{
						java.lang.Exception ex3 = ByteCodeHelper.MapException<java.lang.Exception>(x2, ByteCodeHelper.MapFlags.None);
						if (ex3 == null)
						{
							throw;
						}
						ex4 = ex3;
						goto IL_011a;
					}
				}
				goto IL_0124;
				IL_00be:
				java.lang.Exception ex5 = ex2;
				goto IL_0124;
				IL_011a:
				java.lang.Exception ex6 = ex4;
				goto IL_0124;
				IL_0124:
				if (i < (nint)fields.LongLength - 1)
				{
					bldr.append(",");
				}
			}
			bldr.append(">");
			return bldr.toString();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(357)]
		private string convertName(string name)
		{
			string result = java.lang.String.instancehelper_toLowerCase(java.lang.String.instancehelper_replaceFirst(java.lang.String.instancehelper_replaceAll(name, "([A-Z])", " $1"), "^ ", ""));
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 96, 98, 233, 52, 232, 77, 113 })]
		public MPEGVideoAnalyzer()
		{
			nextStartCode = -1;
			bselPayload = ByteBuffer.allocate(1048576);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(172)]
		internal static void access_0024000(MPEGVideoAnalyzer x0, ByteBuffer x1)
		{
			x0.analyzeMpegVideoPacket(x1);
		}

		static CallerID ___003CGetCallerID_003E()
		{
			if (___003CcallerID_003E == null)
			{
				___003CcallerID_003E = new ___003CCallerID_003E();
			}
			return ___003CcallerID_003E;
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag DUMP_FROM;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag STOP_AT;

	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static MainUtils.Flag[] ALL_FLAGS;

	protected internal ReadableByteChannel ch;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 130, 105, 104 })]
	public MPSDump(ReadableByteChannel ch)
	{
		this.ch = ch;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 124, 130, 99, 140, 99, 99, 101, 109, 107,
		102, 104, 106, 102, 173, 100, 109, 109, 107, 107,
		99, 137, 101, 102, 100, 107, 127, 2, 137, 107,
		99, 166, 135, 107, 99, 166, 104, 114, 106, 114,
		103, 114, 98, 102, 142
	})]
	public virtual void dump(Long dumpAfterPts, Long stopPts)
	{
		MPEGVideoAnalyzer analyzer = null;
		ByteBuffer buffer = ByteBuffer.allocate(1048576);
		PESPacket pkt = null;
		int hdrSize = 0;
		long position = 0L;
		while (true)
		{
			position -= buffer.position();
			if (fillBuffer(buffer) == -1)
			{
				break;
			}
			buffer.flip();
			if (buffer.remaining() < 4)
			{
				break;
			}
			position += buffer.remaining();
			while (true)
			{
				ByteBuffer payload = null;
				if (pkt != null && pkt.length > 0)
				{
					int pesLen = pkt.length - hdrSize + 6;
					if (pesLen <= buffer.remaining())
					{
						payload = NIOUtils.read(buffer, pesLen);
					}
				}
				else
				{
					payload = getPesPayload(buffer);
				}
				if (payload == null)
				{
					break;
				}
				if (pkt != null)
				{
					logPes(pkt, hdrSize, payload);
				}
				if (analyzer != null && pkt != null && pkt.streamId >= 224 && pkt.streamId <= 239)
				{
					MPEGVideoAnalyzer.access_0024000(analyzer, payload);
				}
				if (buffer.remaining() < 32)
				{
					pkt = null;
					break;
				}
				skipToNextPES(buffer);
				if (buffer.remaining() < 32)
				{
					pkt = null;
					break;
				}
				hdrSize = buffer.position();
				pkt = MPSUtils.readPESHeader(buffer, position - buffer.remaining());
				hdrSize = buffer.position() - hdrSize;
				if (dumpAfterPts != null && pkt.pts >= dumpAfterPts.longValue())
				{
					analyzer = new MPEGVideoAnalyzer();
				}
				if (stopPts != null && pkt.pts >= stopPts.longValue())
				{
					return;
				}
			}
			buffer = transferRemainder(buffer);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(129)]
	protected internal virtual int fillBuffer(ByteBuffer buffer)
	{
		int result = ch.read(buffer);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 98, 104, 104, 105, 109, 105, 110, 110,
		131, 104, 104, 99
	})]
	private static ByteBuffer getPesPayload(ByteBuffer buffer)
	{
		ByteBuffer copy = buffer.duplicate();
		ByteBuffer result = buffer.duplicate();
		while (copy.hasRemaining())
		{
			int marker = copy.duplicate().getInt();
			if (marker >= 441)
			{
				result.limit(copy.position());
				buffer.position(copy.position());
				return result;
			}
			copy.getInt();
			MPEGUtil.gotoNextMarker(copy);
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 109, 98, 127, 63, 63, 29, 168 })]
	protected internal virtual void logPes(PESPacket pkt, int hdrSize, ByteBuffer payload)
	{
		java.lang.System.@out.println(new StringBuilder().append(pkt.streamId).append("(").append((pkt.streamId < 224) ? "audio" : "video")
			.append(") [")
			.append(pkt.pos)
			.append(", ")
			.append(payload.remaining() + hdrSize)
			.append("], pts: ")
			.append(pkt.pts)
			.append(", dts: ")
			.append(pkt.dts)
			.toString());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 162, 105, 109, 121, 99, 104, 104, 99 })]
	private static void skipToNextPES(ByteBuffer buffer)
	{
		while (buffer.hasRemaining())
		{
			int marker = buffer.duplicate().getInt();
			if (marker >= 445 && marker <= 511 && marker != 446)
			{
				break;
			}
			buffer.getInt();
			MPEGUtil.gotoNextMarker(buffer);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 162, 104, 104, 105, 113 })]
	private ByteBuffer transferRemainder(ByteBuffer buffer)
	{
		ByteBuffer dup = buffer.duplicate();
		dup.clear();
		while (buffer.hasRemaining())
		{
			dup.put((byte)(sbyte)buffer.get());
		}
		return dup;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159,
		129,
		162,
		131,
		109,
		107,
		byte.MaxValue,
		5,
		74,
		76,
		231,
		55,
		162,
		121,
		109,
		141,
		146,
		74,
		99,
		99
	})]
	public static void main1(string[] args)
	{
		FileChannelWrapper ch = null;
		MainUtils.Cmd cmd;
		try
		{
			cmd = MainUtils.parseArguments(args, ALL_FLAGS);
			if ((nint)cmd.args.LongLength < 1)
			{
				MainUtils.printHelp(ALL_FLAGS, Arrays.asList("file name"));
				goto IL_004c;
			}
		}
		catch
		{
			//try-fault
			NIOUtils.closeQuietly(ch);
			throw;
		}
		try
		{
			
			ch = NIOUtils.readableChannel(new File(cmd.args[0]));
			Long dumpAfterPts = cmd.getLongFlag(DUMP_FROM);
			Long stopPts = cmd.getLongFlag(STOP_AT);
			new MPSDump(ch).dump(dumpAfterPts, stopPts);
			return;
		}
		finally
		{
			NIOUtils.closeQuietly(ch);
		}
		IL_004c:
		NIOUtils.closeQuietly(null);
	}

	[LineNumberTable(new byte[] { 159, 131, 66, 118, 118 })]
	static MPSDump()
	{
		DUMP_FROM = MainUtils.Flag.flag("dump-from", null, "Stop reading at timestamp");
		STOP_AT = MainUtils.Flag.flag("stop-at", null, "Start dumping from timestamp");
		ALL_FLAGS = new MainUtils.Flag[2] { DUMP_FROM, STOP_AT };
	}
}
