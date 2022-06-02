using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.io;
using org.jcodec.containers.mps;
using org.jcodec.containers.mps.psi;

namespace org.jcodec.codecs.mpeg12;

public class MTSMediaInfo : Object
{
	[SpecialName]
	[EnclosingMethod(null, "getMediaInfo", "(Ljava.io.File;)Ljava.util.List;")]
	internal class _1 : MTSUtils.TSReader
	{
		private ByteBuffer pmtBuffer;

		private int pmtPid;

		private bool pmtDone;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal List val_0024pmtSections;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal Map val_0024pids;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal List val_0024result;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal MTSMediaInfo this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 133, 129, 67, 159, 9 })]
		internal _1(MTSMediaInfo this_00240, bool flush, List l1, Map m, List l2) : base(flush)
		{
			this.this_00240 = this_00240;
			val_0024pmtSections = l1;
			val_0024pids = m;
			val_0024result = l2;
			pmtPid = -1;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159,
			131,
			130,
			100,
			114,
			120,
			105,
			127,
			4,
			110,
			191,
			4,
			113,
			109,
			109,
			110,
			104,
			104,
			101,
			121,
			253,
			61,
			231,
			69,
			117,
			104,
			102,
			151,
			byte.MaxValue,
			20,
			70,
			227,
			59,
			99,
			127,
			9,
			115,
			110,
			163
		})]
		protected internal override bool onPkt(int guid, bool payloadStart, ByteBuffer tsBuf, long filePos, bool sectionSyntax, ByteBuffer fullPkt)
		{
			MPSMediaInfo.MediaInfoDone mediaInfoDone;
			if (guid == 0)
			{
				pmtPid = MTSUtils.parsePAT(tsBuf);
			}
			else if (guid == pmtPid && !pmtDone)
			{
				if (pmtBuffer == null)
				{
					pmtBuffer = ByteBuffer.allocate(((tsBuf.duplicate().getInt() >> 8) & 0x3FF) + 3);
				}
				else if (pmtBuffer.hasRemaining())
				{
					NIOUtils.writeL(pmtBuffer, tsBuf, Math.min(pmtBuffer.remaining(), tsBuf.remaining()));
				}
				if (!pmtBuffer.hasRemaining())
				{
					pmtBuffer.flip();
					PMTSection pmt = MTSUtils.parsePMT(pmtBuffer);
					val_0024pmtSections.add(pmt);
					PMTSection.PMTStream[] streams = pmt.getStreams();
					for (int i = 0; i < (nint)streams.LongLength; i++)
					{
						PMTSection.PMTStream stream = streams[i];
						if (!val_0024pids.containsKey(Integer.valueOf(stream.getPid())))
						{
							val_0024pids.put(Integer.valueOf(stream.getPid()), new MPSMediaInfo());
						}
					}
					pmtDone = pmt.getSectionNumber() == pmt.getLastSectionNumber();
					pmtBuffer = null;
				}
			}
			else if (val_0024pids.containsKey(Integer.valueOf(guid)))
			{
				try
				{
					((MPSMediaInfo)val_0024pids.get(Integer.valueOf(guid))).analyseBuffer(tsBuf, filePos);
				}
				catch (MPSMediaInfo.MediaInfoDone x)
				{
					mediaInfoDone = ByteCodeHelper.MapException<MPSMediaInfo.MediaInfoDone>(x, ByteCodeHelper.MapFlags.NoRemapping);
					goto IL_0180;
				}
			}
			goto IL_01d6;
			IL_0180:
			MPSMediaInfo.MediaInfoDone e = mediaInfoDone;
			val_0024result.addAll(((MPSMediaInfo)val_0024pids.get(Integer.valueOf(guid))).getInfos());
			val_0024pids.remove(Integer.valueOf(guid));
			if (val_0024pids.size() == 0)
			{
				return false;
			}
			goto IL_01d6;
			IL_01d6:
			return true;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(29)]
	public MTSMediaInfo()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[Signature("(Ljava/io/File;)Ljava/util/List<Lorg/jcodec/codecs/mpeg12/MPSMediaInfo$MPEGTrackMetadata;>;")]
	[LineNumberTable(new byte[]
	{
		159, 134, 66, 99, 103, 103, 135, 104, 236, 106,
		138, 74, 99, 131
	})]
	public virtual List getMediaInfo(File f)
	{
		FileChannelWrapper ch = null;
		ArrayList pmtSections = new ArrayList();
		HashMap pids = new HashMap();
		ArrayList result = new ArrayList();
		try
		{
			ch = NIOUtils.readableChannel(f);
			new _1(this, flush: false, pmtSections, pids, result).readTsFile(ch);
			return result;
		}
		finally
		{
			NIOUtils.closeQuietly(ch);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 120, 98, 121, 124, 113, 99 })]
	public static void main1(string[] args)
	{
		MTSMediaInfo mTSMediaInfo = new MTSMediaInfo();
		
		List info = mTSMediaInfo.getMediaInfo(new File(args[0]));
		Iterator iterator = info.iterator();
		while (iterator.hasNext())
		{
			MPSMediaInfo.MPEGTrackMetadata stream = (MPSMediaInfo.MPEGTrackMetadata)iterator.next();
			java.lang.System.@out.println(stream.codec);
		}
	}

	[LineNumberTable(97)]
	public static MTSMediaInfo extract(SeekableByteChannel input)
	{
		return null;
	}

	[LineNumberTable(102)]
	public virtual MPSMediaInfo.MPEGTrackMetadata getVideoTrack()
	{
		return null;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/codecs/mpeg12/MPSMediaInfo$MPEGTrackMetadata;>;")]
	[LineNumberTable(107)]
	public virtual List getAudioTracks()
	{
		return null;
	}
}
