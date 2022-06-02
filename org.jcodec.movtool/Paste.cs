using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio.channels;
using java.util;
using org.jcodec.common.io;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;
using org.jcodec.platform;

namespace org.jcodec.movtool;

public class Paste : Object
{
	internal long[] tv;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(41)]
	public Paste()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 162, 104, 114, 141, 145, 103, 103, 104,
		104, 139, 109, 110, 107, 149, 105, 234, 58, 236,
		74, 109, 107, 31, 0, 233, 70, 105
	})]
	public virtual void paste(MovieBox to, MovieBox from, double sec)
	{
		TrakBox videoTrack = to.getVideoTrack();
		if (videoTrack != null && videoTrack.getTimescale() != to.getTimescale())
		{
			to.fixTimescale(videoTrack.getTimescale());
		}
		long displayTv = ByteCodeHelper.d2l((double)to.getTimescale() * sec);
		Util.forceEditListMov(to);
		Util.forceEditListMov(from);
		TrakBox[] fromTracks = from.getTracks();
		TrakBox[] toTracks = to.getTracks();
		int[][] matches = findMatches(fromTracks, toTracks);
		for (int j = 0; j < (nint)matches[0].LongLength; j++)
		{
			TrakBox localTrack = to.importTrack(from, fromTracks[j]);
			if (matches[0][j] != -1)
			{
				Util.insertTo(to, toTracks[matches[0][j]], localTrack, displayTv);
				continue;
			}
			to.appendTrack(localTrack);
			Util.shift(to, localTrack, displayTv);
		}
		for (int i = 0; i < (nint)matches[1].LongLength; i++)
		{
			if (matches[1][i] == -1)
			{
				Util.spread(to, toTracks[i], displayTv, to.rescale(from.getDuration(), from.getTimescale()));
			}
		}
		to.updateDuration();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 115, 162, 104, 104, 101, 15, 199 })]
	public virtual void addToMovie(MovieBox to, MovieBox from)
	{
		TrakBox[] tracks = from.getTracks();
		for (int i = 0; i < (nint)tracks.LongLength; i++)
		{
			TrakBox track = tracks[i];
			to.appendTrack(to.importTrack(from, track));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 110, 66, 105, 137, 104, 136, 107, 103, 99,
		104, 103, 99, 111, 101, 101, 227, 58, 231, 61,
		234, 78
	})]
	private int[][] findMatches(TrakBox[] fromTracks, TrakBox[] toTracks)
	{
		int[] f2t = new int[(nint)fromTracks.LongLength];
		int[] t2f = new int[(nint)toTracks.LongLength];
		Arrays.fill(f2t, -1);
		Arrays.fill(t2f, -1);
		for (int i = 0; i < (nint)fromTracks.LongLength; i++)
		{
			if (f2t[i] != -1)
			{
				continue;
			}
			for (int j = 0; j < (nint)toTracks.LongLength; j++)
			{
				if (t2f[j] == -1 && matches(fromTracks[i], toTracks[j]))
				{
					f2t[i] = j;
					t2f[j] = i;
					break;
				}
			}
		}
		return new int[2][] { f2t, t2f };
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 66, 127, 2, 117, 22 })]
	private bool matches(TrakBox trakBox1, TrakBox trakBox2)
	{
		return (String.instancehelper_equals(trakBox1.getHandlerType(), trakBox2.getHandlerType()) && matchHeaders(trakBox1, trakBox2) && matchSampleSizes(trakBox1, trakBox2) && matchMediaHeader(trakBox1, trakBox2) && matchClip(trakBox1, trakBox2) && matchLoad(trakBox1, trakBox2)) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 96, 66, 104, 136, 127, 8, 127, 4, 110,
		127, 2, 241, 60
	})]
	private bool matchHeaders(TrakBox trakBox1, TrakBox trakBox2)
	{
		TrackHeaderBox th1 = trakBox1.getTrackHeader();
		TrackHeaderBox th2 = trakBox2.getTrackHeader();
		return ((String.instancehelper_equals("vide", trakBox1.getHandlerType()) && Platform.arrayEqualsInt(th1.getMatrix(), th2.getMatrix()) && th1.getLayer() == th2.getLayer() && th1.getWidth() == th2.getWidth() && th1.getHeight() == th2.getHeight()) || (String.instancehelper_equals("soun", trakBox1.getHandlerType()) && th1.getVolume() == th2.getVolume()) || String.instancehelper_equals("tmcd", trakBox1.getHandlerType())) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 130, 124, 124 })]
	private bool matchSampleSizes(TrakBox trakBox1, TrakBox trakBox2)
	{
		SampleSizesBox stsz1 = (SampleSizesBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<SampleSizesBox>.Value, Box.path("mdia.minf.stbl.stsz"));
		SampleSizesBox stsz2 = (SampleSizesBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<SampleSizesBox>.Value, Box.path("mdia.minf.stbl.stsz"));
		return stsz1.getDefaultSize() == stsz2.getDefaultSize();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 101, 66, 124, 124, 109, 99, 103, 126, 63,
		1, 162, 124, 124, 109, 99, 103, 176
	})]
	private bool matchMediaHeader(TrakBox trakBox1, TrakBox trakBox2)
	{
		VideoMediaHeaderBox vmhd1 = (VideoMediaHeaderBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<VideoMediaHeaderBox>.Value, Box.path("mdia.minf.vmhd"));
		VideoMediaHeaderBox vmhd2 = (VideoMediaHeaderBox)NodeBox.findFirstPath(trakBox2, ClassLiteral<VideoMediaHeaderBox>.Value, Box.path("mdia.minf.vmhd"));
		if ((vmhd1 != null && vmhd2 == null) || (vmhd1 == null && vmhd2 != null))
		{
			return false;
		}
		if (vmhd1 != null && vmhd2 != null)
		{
			return (vmhd1.getGraphicsMode() == vmhd2.getGraphicsMode() && vmhd1.getbOpColor() == vmhd2.getbOpColor() && vmhd1.getgOpColor() == vmhd2.getgOpColor() && vmhd1.getrOpColor() == vmhd2.getrOpColor()) ? true : false;
		}
		SoundMediaHeaderBox smhd1 = (SoundMediaHeaderBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<SoundMediaHeaderBox>.Value, Box.path("mdia.minf.smhd"));
		SoundMediaHeaderBox smhd2 = (SoundMediaHeaderBox)NodeBox.findFirstPath(trakBox2, ClassLiteral<SoundMediaHeaderBox>.Value, Box.path("mdia.minf.smhd"));
		if ((smhd1 == null && smhd2 != null) || (smhd1 != null && smhd2 == null))
		{
			return false;
		}
		if (smhd1 != null && smhd2 != null)
		{
			return smhd1.getBalance() == smhd1.getBalance();
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 90, 98, 124, 124, 109, 127, 13, 63, 1,
		162, 103, 99
	})]
	private bool matchClip(TrakBox trakBox1, TrakBox trakBox2)
	{
		ClipRegionBox crgn1 = (ClipRegionBox)NodeBox.findFirstPath(trakBox1, ClassLiteral<ClipRegionBox>.Value, Box.path("clip.crgn"));
		ClipRegionBox crgn2 = (ClipRegionBox)NodeBox.findFirstPath(trakBox2, ClassLiteral<ClipRegionBox>.Value, Box.path("clip.crgn"));
		if (crgn1 != null && crgn2 != null)
		{
			return (crgn1.getRgnSize() == crgn2.getRgnSize() && crgn1.getX() == crgn2.getX() && crgn1.getY() == crgn2.getY() && crgn1.getWidth() == crgn2.getWidth() && crgn1.getHeight() == crgn2.getHeight()) ? true : false;
		}
		if (crgn1 == null && crgn2 == null)
		{
			return true;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 94, 162, 119, 119, 103, 112, 111, 111, 242,
		61, 226, 69, 103, 99
	})]
	private bool matchLoad(TrakBox trakBox1, TrakBox trakBox2)
	{
		LoadSettingsBox load1 = (LoadSettingsBox)NodeBox.findFirst(trakBox1, ClassLiteral<LoadSettingsBox>.Value, "load");
		LoadSettingsBox load2 = (LoadSettingsBox)NodeBox.findFirst(trakBox2, ClassLiteral<LoadSettingsBox>.Value, "load");
		if (load1 != null && load2 != null)
		{
			return (load1.getPreloadStartTime() == load2.getPreloadStartTime() && load1.getPreloadDuration() == load2.getPreloadDuration() && load1.getPreloadFlags() == load2.getPreloadFlags() && load1.getDefaultHints() == load2.getDefaultHints()) ? true : false;
		}
		if (load1 == null && load2 == null)
		{
			return true;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159, 131, 66, 102, 112, 135, 111, 99, 99, 131,
		127, 34, 105, 105, 104, 112, 105, 127, 9, 127,
		10, 114, 102, 159, 4, 153, 141, 100, 103, 100,
		103, 100, 234, 59, 100, 103, 100, 103, 100, 137
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 2)
		{
			java.lang.System.@out.println("Syntax: paste <to movie> <from movie> [second]");
			java.lang.System.exit(-1);
		}
		
		File toFile = new File(args[0]);
		FileChannelWrapper to = null;
		FileChannelWrapper from = null;
		FileChannelWrapper @out = null;
		try
		{
			
			File outFile = new File(toFile.getParentFile(), new StringBuilder().append(String.instancehelper_replaceAll(toFile.getName(), "\\.mov$", "")).append(".paste.mov").toString());
			Platform.deleteFile(outFile);
			@out = NIOUtils.writableChannel(outFile);
			to = NIOUtils.writableChannel(toFile);
			
			File fromFile = new File(args[1]);
			from = NIOUtils.readableChannel(fromFile);
			MP4Util.Movie toMov = MP4Util.createRefFullMovie(to, new StringBuilder().append("file://").append(toFile.getCanonicalPath()).toString());
			MP4Util.Movie fromMov = MP4Util.createRefFullMovie(from, new StringBuilder().append("file://").append(fromFile.getCanonicalPath()).toString());
			new Strip().strip(fromMov.getMoov());
			if ((nint)args.LongLength > 2)
			{
				new Paste().paste(toMov.getMoov(), fromMov.getMoov(), Double.parseDouble(args[2]));
			}
			else
			{
				new Paste().addToMovie(toMov.getMoov(), fromMov.getMoov());
			}
			MP4Util.writeFullMovie(@out, toMov);
		}
		catch
		{
			//try-fault
			((Channel)to)?.close();
			((Channel)from)?.close();
			((Channel)@out)?.close();
			throw;
		}
		((Channel)to)?.close();
		((Channel)from)?.close();
		((Channel)@out)?.close();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 112, 98, 105, 141 })]
	private long getFrameTv(TrakBox videoTrack, int frame)
	{
		if (tv == null)
		{
			tv = Util.getTimevalues(videoTrack);
		}
		return tv[frame];
	}
}
