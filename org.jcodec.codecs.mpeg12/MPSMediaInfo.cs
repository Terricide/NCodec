using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.containers.mps;

namespace org.jcodec.codecs.mpeg12;

public class MPSMediaInfo : MPSUtils.PESReader
{
	[SpecialName]
	[EnclosingMethod(null, "getMediaInfo", "(Ljava.io.File;)Ljava.util.List;")]
	internal class _1 : NIOUtils.FileReader
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal MPSMediaInfo this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(123)]
		internal _1(MPSMediaInfo this_00240)
		{
			this.this_00240 = this_00240;
			base._002Ector();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 111, 130, 112 })]
		protected internal override void data(ByteBuffer data, long filePos)
		{
			this_00240.analyseBuffer(data, filePos);
		}

		[LineNumberTable(132)]
		protected internal override void done()
		{
		}
	}

	[Serializable]
	public class MediaInfoDone : RuntimeException
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(140)]
		public MediaInfoDone()
		{
		}

		[HideFromJava]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\nversion=\"1\">\r\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\nversion=\"1\"\r\nFlags=\"SerializationFormatter\"/>\r\n</PermissionSet>\r\n")]
		protected MediaInfoDone(SerializationInfo P_0, StreamingContext P_1)
			: base(P_0, P_1)
		{
		}
	}

	public class MPEGTimecodeMetadata : java.lang.Object
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(44)]
		public MPEGTimecodeMetadata()
		{
		}

		[LineNumberTable(48)]
		public virtual string getNumFrames()
		{
			return null;
		}

		[LineNumberTable(53)]
		public virtual string isDropFrame()
		{
			return null;
		}

		[LineNumberTable(58)]
		public virtual string getStartCounter()
		{
			return null;
		}
	}

	public class MPEGTrackMetadata : java.lang.Object
	{
		internal int streamId;

		internal Codec codec;

		internal ByteBuffer probeData;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 125, 66, 105, 104 })]
		public MPEGTrackMetadata(int streamId)
		{
			this.streamId = streamId;
		}

		[LineNumberTable(74)]
		public virtual AudioFormat getAudioFormat()
		{
			return null;
		}

		[LineNumberTable(78)]
		public virtual ChannelLabel[] getChannelLables()
		{
			return null;
		}

		[LineNumberTable(83)]
		public virtual Size getDisplaySize()
		{
			return null;
		}

		[LineNumberTable(88)]
		public virtual Size getCodedSize()
		{
			return null;
		}

		[LineNumberTable(93)]
		public virtual float getFps()
		{
			return 0f;
		}

		[LineNumberTable(98)]
		public virtual float getDuration()
		{
			return 0f;
		}

		[LineNumberTable(103)]
		public virtual string getFourcc()
		{
			return null;
		}

		[LineNumberTable(108)]
		public virtual Rational getFpsR()
		{
			return null;
		}

		[LineNumberTable(113)]
		public virtual int getNumFrames()
		{
			return 0;
		}

		[LineNumberTable(117)]
		public virtual MPEGTimecodeMetadata getTimecode()
		{
			return null;
		}
	}

	public class PSM : java.lang.Object
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(200)]
		public PSM()
		{
		}
	}

	[Signature("Ljava/util/Map<Ljava/lang/Integer;Lorg/jcodec/codecs/mpeg12/MPSMediaInfo$MPEGTrackMetadata;>;")]
	private Map infos;

	private int pesTried;

	private PSM psm;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("()Ljava/util/List<Lorg/jcodec/codecs/mpeg12/MPSMediaInfo$MPEGTrackMetadata;>;")]
	[LineNumberTable(232)]
	public virtual List getInfos()
	{
		
		ArrayList result = new ArrayList(infos.values());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 102, 130, 109, 127, 0, 110, 113, 113, 108,
		110, 107, 145, 115, 113, 127, 6, 113, 115, 110,
		115, 110, 115, 172, 115, 140, 102
	})]
	private void deriveMediaInfo()
	{
		Collection values = infos.values();
		Iterator iterator = values.iterator();
		while (iterator.hasNext())
		{
			MPEGTrackMetadata stream = (MPEGTrackMetadata)iterator.next();
			int streamId = 0x100 | stream.streamId;
			if (streamId >= 448 && streamId <= 479)
			{
				stream.codec = Codec.___003C_003EMP2;
				continue;
			}
			switch (streamId)
			{
			case 445:
			{
				ByteBuffer dup = stream.probeData.duplicate();
				MPSUtils.readPESHeader(dup, 0L);
				int type = (sbyte)dup.get() & 0xFF;
				if (type >= 128 && type <= 135)
				{
					stream.codec = Codec.___003C_003EAC3;
				}
				else if ((type >= 136 && type <= 143) || (type >= 152 && type <= 159))
				{
					stream.codec = Codec.___003C_003EDTS;
				}
				else if (type >= 160 && type <= 175)
				{
					stream.codec = Codec.___003C_003EPCM_DVD;
				}
				else if (type >= 176 && type <= 191)
				{
					stream.codec = Codec.___003C_003ETRUEHD;
				}
				else if (type >= 192 && type <= 207)
				{
					stream.codec = Codec.___003C_003EAC3;
				}
				break;
			}
			case 480:
			case 481:
			case 482:
			case 483:
			case 484:
			case 485:
			case 486:
			case 487:
			case 488:
			case 489:
			case 490:
			case 491:
			case 492:
			case 493:
			case 494:
			case 495:
				stream.codec = Codec.___003C_003EMPEG2;
				break;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 87, 162, 105, 105, 105, 104, 111, 99 })]
	private void parseElStreams(ByteBuffer buf)
	{
		while (buf.hasRemaining())
		{
			int streamType = (sbyte)buf.get();
			int streamId = (sbyte)buf.get();
			int strInfoLen = buf.getShort();
			ByteBuffer byteBuffer = NIOUtils.read(buf, strInfoLen & 0xFFFF);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 105, 108 })]
	public MPSMediaInfo()
	{
		infos = new HashMap();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Ljava/io/File;)Ljava/util/List<Lorg/jcodec/codecs/mpeg12/MPSMediaInfo$MPEGTrackMetadata;>;")]
	[LineNumberTable(new byte[] { 159, 112, 162, 238, 74, 184, 3, 98, 139 })]
	public virtual List getMediaInfo(File f)
	{
		MediaInfoDone mediaInfoDone;
		try
		{
			new _1(this).readFile(f, 65536, null);
		}
		catch (MediaInfoDone x)
		{
			mediaInfoDone = ByteCodeHelper.MapException<MediaInfoDone>(x, ByteCodeHelper.MapFlags.NoRemapping);
			goto IL_0024;
		}
		goto IL_0037;
		IL_0024:
		MediaInfoDone e = mediaInfoDone;
		Logger.info("Media info done");
		goto IL_0037;
		IL_0037:
		return getInfos();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 106, 98, 106, 98, 121, 100, 105, 149, 105,
		141, 120, 103, 140
	})]
	protected internal override void pes(ByteBuffer pesBuffer, long start, int pesLen, int stream)
	{
		if (MPSUtils.mediaStream(stream))
		{
			MPEGTrackMetadata info = (MPEGTrackMetadata)infos.get(Integer.valueOf(stream));
			if (info == null)
			{
				info = new MPEGTrackMetadata(stream);
				infos.put(Integer.valueOf(stream), info);
			}
			if (info.probeData == null)
			{
				info.probeData = NIOUtils.cloneBuffer(pesBuffer);
			}
			int num = pesTried + 1;
			pesTried = num;
			if (num >= 100)
			{
				deriveMediaInfo();
				
				throw new MediaInfoDone();
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 95, 162, 106, 103, 127, 5, 116, 138 })]
	private int[] parseSystem(ByteBuffer pesBuffer)
	{
		NIOUtils.skip(pesBuffer, 12);
		IntArrayList result = IntArrayList.createIntArrayList();
		while (pesBuffer.remaining() >= 3 && ((sbyte)pesBuffer.get(pesBuffer.position()) & 0x80) == 128)
		{
			result.add((sbyte)pesBuffer.get() & 0xFF);
			pesBuffer.getShort();
		}
		int[] result2 = result.toArray();
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 91, 98, 104, 104, 105, 113, 105, 105, 103,
		113, 104, 112, 105, 117, 137
	})]
	private PSM parsePSM(ByteBuffer pesBuffer)
	{
		pesBuffer.getInt();
		int psmLen = pesBuffer.getShort();
		if (psmLen > 1018)
		{
			
			throw new RuntimeException("Invalid PSM");
		}
		int b0 = (sbyte)pesBuffer.get();
		int b1 = (sbyte)pesBuffer.get();
		if ((b1 & 1) != 1)
		{
			
			throw new RuntimeException("Invalid PSM");
		}
		int psiLen = pesBuffer.getShort();
		ByteBuffer psi = NIOUtils.read(pesBuffer, psiLen & 0xFFFF);
		int elStreamLen = pesBuffer.getShort();
		parseElStreams(NIOUtils.read(pesBuffer, elStreamLen & 0xFFFF));
		int crc = pesBuffer.getInt();
		PSM result = new PSM();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 83, 66, 121 })]
	public static void main1(string[] args)
	{
		MPSMediaInfo mPSMediaInfo = new MPSMediaInfo();
		
		mPSMediaInfo.getMediaInfo(new File(args[0]));
	}

	[LineNumberTable(241)]
	public static MPSMediaInfo extract(SeekableByteChannel input)
	{
		return null;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/codecs/mpeg12/MPSMediaInfo$MPEGTrackMetadata;>;")]
	[LineNumberTable(245)]
	public virtual List getAudioTracks()
	{
		return null;
	}

	[LineNumberTable(249)]
	public virtual MPEGTrackMetadata getVideoTrack()
	{
		return null;
	}
}
