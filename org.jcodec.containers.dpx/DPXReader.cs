using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.text;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;

namespace org.jcodec.containers.dpx;

public class DPXReader : Object
{
	private const int READ_BUFFER_SIZE = 3072;

	internal const int IMAGEINFO_OFFSET = 768;

	internal const int IMAGESOURCE_OFFSET = 1408;

	internal const int FILM_OFFSET = 1664;

	internal const int TVINFO_OFFSET = 1920;

	public const int SDPX = 1396985944;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private ByteBuffer readBuf;

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	private int magic;

	private bool eof;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 123, 162, 109, 112, 104, 109 })]
	private void initialRead(ReadableByteChannel ch)
	{
		readBuf.clear();
		if (ch.read(readBuf) == -1)
		{
			eof = true;
		}
		readBuf.flip();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 130, 103, 109, 110, 109, 141, 109, 109,
		141, 111, 116, 111, 114, 114, 109
	})]
	private static FileHeader readFileInfo(ByteBuffer bb)
	{
		FileHeader h = new FileHeader();
		h.imageOffset = bb.getInt();
		h.version = readNullTermString(bb, 8);
		h.filesize = bb.getInt();
		h.ditto = bb.getInt();
		h.genericHeaderLength = bb.getInt();
		h.industryHeaderLength = bb.getInt();
		h.userHeaderLength = bb.getInt();
		h.filename = readNullTermString(bb, 100);
		h.created = tryParseISO8601Date(readNullTermString(bb, 24));
		h.creator = readNullTermString(bb, 100);
		h.projectName = readNullTermString(bb, 200);
		h.copyright = readNullTermString(bb, 200);
		h.encKey = bb.getInt();
		return h;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 89, 130, 135, 109, 109, 109, 109, 172, 114 })]
	private static ImageHeader readImageInfoHeader(ByteBuffer r)
	{
		ImageHeader h = new ImageHeader();
		h.orientation = r.getShort();
		h.numberOfImageElements = r.getShort();
		h.pixelsPerLine = r.getInt();
		h.linesPerImageElement = r.getInt();
		h.imageElement1 = new ImageElement();
		h.imageElement1.dataSign = r.getInt();
		return h;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 93, 66, 103, 109, 109, 109, 109, 109, 109,
		111, 116, 111, 111, 127, 18, 127, 0
	})]
	private static ImageSourceHeader readImageSourceHeader(ByteBuffer r)
	{
		ImageSourceHeader h = new ImageSourceHeader();
		h.xOffset = r.getInt();
		h.yOffset = r.getInt();
		h.xCenter = r.getFloat();
		h.yCenter = r.getFloat();
		h.xOriginal = r.getInt();
		h.yOriginal = r.getInt();
		h.sourceImageFilename = readNullTermString(r, 100);
		h.sourceImageDate = tryParseISO8601Date(readNullTermString(r, 24));
		h.deviceName = readNullTermString(r, 32);
		h.deviceSerial = readNullTermString(r, 32);
		h.borderValidity = new short[4]
		{
			r.getShort(),
			r.getShort(),
			r.getShort(),
			r.getShort()
		};
		h.aspectRatio = new int[2]
		{
			r.getInt(),
			r.getInt()
		};
		return h;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 66, 103, 110, 110, 110, 110, 110, 143 })]
	private static FilmHeader readFilmInformationHeader(ByteBuffer r)
	{
		FilmHeader h = new FilmHeader();
		h.idCode = readNullTermString(r, 2);
		h.type = readNullTermString(r, 2);
		h.offset = readNullTermString(r, 2);
		h.prefix = readNullTermString(r, 6);
		h.count = readNullTermString(r, 4);
		h.format = readNullTermString(r, 32);
		return h;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 130, 103, 109, 109, 110, 110, 110, 110,
		109, 109, 109, 109, 109, 109, 109, 109, 109, 141
	})]
	private static TelevisionHeader readTelevisionInfoHeader(ByteBuffer r)
	{
		TelevisionHeader h = new TelevisionHeader();
		h.timecode = r.getInt();
		h.userBits = r.getInt();
		h.interlace = (byte)(sbyte)r.get();
		h.filedNumber = (byte)(sbyte)r.get();
		h.videoSignalStarted = (byte)(sbyte)r.get();
		h.zero = (byte)(sbyte)r.get();
		h.horSamplingRateHz = r.getInt();
		h.vertSampleRateHz = r.getInt();
		h.frameRate = r.getInt();
		h.timeOffset = r.getInt();
		h.gamma = r.getInt();
		h.blackLevel = r.getInt();
		h.blackGain = r.getInt();
		h.breakpoint = r.getInt();
		h.referenceWhiteLevel = r.getInt();
		h.integrationTime = r.getInt();
		return h;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 106, 130, 104, 112 })]
	private static string readNullTermString(ByteBuffer bb, int length)
	{
		ByteBuffer b = ByteBuffer.allocate(length);
		bb.get(b.array(), 0, length);
		string result = NIOUtils.readNullTermString(b);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 112, 66, 105, 163, 103, 111, 107, 145, 221 })]
	internal static Date tryParseISO8601Date(string dateString)
	{
		if (StringUtils.isEmpty(dateString))
		{
			return null;
		}
		string noTZ = "yyyy:MM:dd:HH:mm:ss";
		if (String.instancehelper_length(dateString) == String.instancehelper_length(noTZ))
		{
			Date result = date(dateString, noTZ);
			
			return result;
		}
		if (String.instancehelper_length(dateString) == String.instancehelper_length(noTZ) + 4)
		{
			dateString = new StringBuilder().append(dateString).append("00").toString();
		}
		Date result2 = date(dateString, "yyyy:MM:dd:HH:mm:ss:Z");
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 98, 146, 126, 98 })]
	private static Date date(string dateString, string dateFormat)
	{
		//Discarded unreachable code: IL_001d
		SimpleDateFormat format = new SimpleDateFormat(dateFormat, Locale.US);
		ParseException ex;
		try
		{
			return format.parse(dateString);
		}
		catch (ParseException x)
		{
			ex = ByteCodeHelper.MapException<ParseException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		ParseException e = ex;
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 134, 130, 105, 113, 104, 242, 72, 110, 148,
		146
	})]
	public DPXReader(org.jcodec.common.io.SeekableByteChannel ch)
	{
		readBuf = ByteBuffer.allocate(3072);
		initialRead(ch);
		magic = readBuf.getInt();
		if (magic == 1396985944)
		{
			readBuf.order(ByteOrder.BIG_ENDIAN);
		}
		else
		{
			readBuf.order(ByteOrder.LITTLE_ENDIAN);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 130, 103, 114, 178, 114, 178, 114, 178,
		114, 178, 114, 146, 116
	})]
	public virtual DPXMetadata parseMetadata()
	{
		DPXMetadata dpx = new DPXMetadata();
		dpx.file = readFileInfo(readBuf);
		dpx.file.magic = magic;
		readBuf.position(768);
		dpx.image = readImageInfoHeader(readBuf);
		readBuf.position(1408);
		dpx.imageSource = readImageSourceHeader(readBuf);
		readBuf.position(1664);
		dpx.film = readFilmInformationHeader(readBuf);
		readBuf.position(1920);
		dpx.television = readTelevisionInfoHeader(readBuf);
		dpx.userId = readNullTermString(readBuf, 32);
		return dpx;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 104, 66, 168, 140, 74, 3 })]
	public static DPXReader readFile(File file)
	{
		FileChannelWrapper _in = NIOUtils.readableChannel(file);
		try
		{
			return new DPXReader(_in);
		}
		finally
		{
			IOUtils.closeQuietly(_in);
		}
	}
}
