using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.nio.channels;
using java.util;
using org.jcodec.codecs.h264;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.containers.mp4.demuxer;
using org.jcodec.platform;

namespace org.jcodec.testing;

public class TestTool : java.lang.Object
{
	private string jm;

	private File coded;

	private File decoded;

	private File jmconf;

	private File errs;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 101, 66, 103, 127, 8, 127, 8, 109, 109,
		109, 109, 109, 109, 109, 109, 109, 109, 109, 109,
		109, 116
	})]
	private void prepareJMConf()
	{
		StringBuilder sb = new StringBuilder();
		sb.append("InputFile              = \"").append(coded.getAbsolutePath()).append("\"\n");
		sb.append("OutputFile             = \"").append(decoded.getAbsolutePath()).append("\"\n");
		sb.append("RefFile                = \"/dev/null\"\n");
		sb.append("WriteUV                = 1\n");
		sb.append("FileFormat             = 0\n");
		sb.append("RefOffset              = 0\n");
		sb.append("POCScale               = 2\n");
		sb.append("DisplayDecParams       = 0\n");
		sb.append("ConcealMode            = 0\n");
		sb.append("RefPOCGap              = 2\n");
		sb.append("POCGap                 = 2\n");
		sb.append("Silent                 = 1\n");
		sb.append("IntraProfileDeblocking = 1\n");
		sb.append("DecFrmNum              = 0\n");
		sb.append("DecodeAllLayers        = 0\n");
		IOUtils.writeStringToFile(jmconf, sb.toString());
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 132, 130, 105, 104, 141, 118, 118, 150, 105 })]
	public TestTool(string jm, string errs)
	{
		this.jm = jm;
		this.errs = new File(errs);
		coded = File.createTempFile("seq", ".264");
		decoded = File.createTempFile("seq_dec", ".yuv");
		jmconf = File.createTempFile("ldecod", ".conf");
		prepareJMConf();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159,
		126,
		66,
		99,
		131,
		146,
		136,
		141,
		141,
		110,
		202,
		104,
		107,
		117,
		99,
		101,
		145,
		143,
		104,
		113,
		115,
		106,
		106,
		105,
		106,
		137,
		106,
		100,
		103,
		107,
		104,
		133,
		124,
		138,
		100,
		145,
		138,
		147,
		105,
		127,
		4,
		6,
		172,
		116,
		byte.MaxValue,
		21,
		36,
		236,
		94,
		107,
		143,
		100,
		103,
		100,
		234,
		61,
		100,
		103,
		100,
		137
	})]
	private void doIt(string _in)
	{
		FileChannelWrapper raw = null;
		FileChannelWrapper source = null;
		try
		{
			source = new FileChannelWrapper(new FileInputStream(_in).getChannel());
			MP4Demuxer demux = MP4Demuxer.createMP4Demuxer(source);
			SeekableDemuxerTrack inTrack = (SeekableDemuxerTrack)demux.getVideoTrack();
			ByteBuffer _rawData = ByteBuffer.allocate(12533760);
			ByteBuffer codecPrivate = inTrack.getMeta().getCodecPrivate();
			H264Decoder decoder = H264Decoder.createH264DecoderFromCodecPrivate(codecPrivate);
			int sf = 2600;
			inTrack.gotoFrame(sf);
			Packet inFrame;
			while ((inFrame = inTrack.nextFrame()) != null && !inFrame.isKeyFrame())
			{
			}
			if (inFrame == null)
			{
				
				throw new NullPointerException("inFrame == null");
			}
			inTrack.gotoFrame(inFrame.getFrameNo());
			ArrayList decodedPics = new ArrayList();
			int totalFrames = inTrack.getMeta().getTotalFrames();
			int seqNo = 0;
			int i = sf;
			while ((inFrame = inTrack.nextFrame()) != null)
			{
				ByteBuffer data = inFrame.getData();
				List nalUnits = H264Utils.splitFrame(data);
				_rawData.clear();
				H264Utils.joinNALUnitsToBuffer(nalUnits, _rawData);
				_rawData.flip();
				if (H264Utils.isByteBufferIDRSlice(_rawData))
				{
					if (raw != null)
					{
						((Channel)raw).close();
						runJMCompareResults(decodedPics, seqNo);
						decodedPics = new ArrayList();
						seqNo = i;
					}
					raw = new FileChannelWrapper(new FileOutputStream(coded).getChannel());
					((WritableByteChannel)raw).write(codecPrivate);
				}
				if (raw == null)
				{
					
					throw new IllegalStateException("IDR slice not found");
				}
				((WritableByteChannel)raw).write(_rawData);
				Size size = inTrack.getMeta().getVideoCodecMeta().getSize();
				((List)decodedPics).add((object)decoder.decodeFrameFromNals(nalUnits, Picture.create((size.getWidth() + 15) & -16, (size.getHeight() + 15) & -16, ColorSpace.___003C_003EYUV420).getData()));
				int num = i;
				if (500 == -1 || num % 500 == 0)
				{
					PrintStream @out = java.lang.System.@out;
					StringBuilder stringBuilder = new StringBuilder();
					int num2 = i * 100;
					@out.println(stringBuilder.append((totalFrames != -1) ? (num2 / totalFrames) : (-num2)).append("%").toString());
				}
				i++;
			}
			if (((List)decodedPics).size() > 0)
			{
				runJMCompareResults(decodedPics, seqNo);
			}
		}
		catch
		{
			//try-fault
			((Channel)source)?.close();
			((Channel)raw)?.close();
			throw;
		}
		((Channel)source)?.close();
		((Channel)raw)?.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[Signature("(Ljava/util/List<Lorg/jcodec/common/model/Picture;>;I)V")]
	[LineNumberTable(new byte[]
	{
		159, 109, 130, 127, 28, 136, 109, 127, 0, 104,
		100, 122, 6, 168, 102, 122, 6, 169, 102, 122,
		6, 169, 101, 104, 189, 3, 99, 136
	})]
	private void runJMCompareResults(List decodedPics, int seqNo)
	{
		java.lang.Exception ex2;
		try
		{
			Process process = Runtime.getRuntime().exec(new StringBuilder().append(jm).append(" -d ").append(jmconf.getAbsolutePath())
				.toString());
			process.waitFor();
			ByteBuffer yuv = NIOUtils.fetchFromFile(decoded);
			Iterator iterator = decodedPics.iterator();
			while (iterator.hasNext())
			{
				Picture pic = (Picture)iterator.next();
				pic = pic.cropped();
				int equals = (Platform.arrayEqualsByte(ArrayUtil.toByteArrayShifted(JCodecUtil2.getAsIntArray(yuv, pic.getPlaneWidth(0) * pic.getPlaneHeight(0))), pic.getPlaneData(0)) ? 1 : 0);
				equals &= (Platform.arrayEqualsByte(ArrayUtil.toByteArrayShifted(JCodecUtil2.getAsIntArray(yuv, pic.getPlaneWidth(1) * pic.getPlaneHeight(1))), pic.getPlaneData(1)) ? 1 : 0);
				if (((uint)equals & (Platform.arrayEqualsByte(ArrayUtil.toByteArrayShifted(JCodecUtil2.getAsIntArray(yuv, pic.getPlaneWidth(2) * pic.getPlaneHeight(2))), pic.getPlaneData(2)) ? 1u : 0u)) == 0)
				{
					diff(seqNo);
				}
			}
			return;
		}
		catch (System.Exception x)
		{
			java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
			if (ex == null)
			{
				throw;
			}
			ex2 = ex;
		}
		java.lang.Exception e = ex2;
		diff(seqNo);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 130, 127, 6, 127, 23, 127, 23 })]
	private void diff(int seqNo)
	{
		java.lang.System.@out.println(new StringBuilder().append(seqNo).append(": DIFF!!!").toString());
		File file = coded;
		
		file.renameTo(new File(errs, java.lang.String.format("seq%08d.264", Integer.valueOf(seqNo))));
		File file2 = decoded;
		
		file2.renameTo(new File(errs, java.lang.String.format("seq%08d_dec.yuv", Integer.valueOf(seqNo))));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[] { 159, 129, 130, 102, 112, 114, 162, 118 })]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength != 3)
		{
			java.lang.System.@out.println("JCodec h.264 test tool");
			java.lang.System.@out.println("Syntax: <path to ldecod> <movie file> <foder for errors>");
		}
		else
		{
			new TestTool(args[0], args[2]).doIt(args[1]);
		}
	}
}
