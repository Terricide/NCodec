using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.api.transcode.filters;
using org.jcodec.common;
using org.jcodec.common.model;

namespace org.jcodec.api.transcode;

public class Transcoder : Object
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : Object
	{
		_1()
		{
			throw null;
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class Mapping : Object
	{
		private int source;

		private bool copy;

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(62)]
		internal static bool access_0024000(Mapping x0)
		{
			return x0.copy;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(62)]
		internal static int access_0024100(Mapping x0)
		{
			return x0.source;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 126, 129, 67, 105, 104, 104 })]
		public Mapping(int source, bool copy)
		{
			this.source = source;
			this.copy = copy;
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static)]
	internal class Stream : Object
	{
		private const double AUDIO_LEADING_TIME = 0.2;

		[Signature("Ljava/util/LinkedList<Lorg/jcodec/api/transcode/VideoFrameWithPacket;>;")]
		private LinkedList videoQueue;

		[Signature("Ljava/util/LinkedList<Lorg/jcodec/api/transcode/AudioFrameWithPacket;>;")]
		private LinkedList audioQueue;

		[Signature("Ljava/util/List<Lorg/jcodec/api/transcode/Filter;>;")]
		private List filters;

		[Signature("Ljava/util/List<Lorg/jcodec/api/transcode/Filter;>;")]
		private List extraFilters;

		private Sink sink;

		private bool videoCopy;

		private bool audioCopy;

		private PixelStore pixelStore;

		private VideoCodecMeta videoCodecMeta;

		private AudioCodecMeta audioCodecMeta;

		private const int REORDER_LENGTH = 5;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 121, 161, 69, 105, 104, 104, 104, 105, 105,
			108, 108
		})]
		public Stream(Sink sink, bool videoCopy, bool audioCopy, List extraFilters, PixelStore pixelStore)
		{
			this.sink = sink;
			this.videoCopy = videoCopy;
			this.audioCopy = audioCopy;
			this.extraFilters = extraFilters;
			this.pixelStore = pixelStore;
			videoQueue = new LinkedList();
			audioQueue = new LinkedList();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 85, 130, 111, 99, 119, 99 })]
		public virtual bool needsVideoFrame()
		{
			if (videoQueue.size() <= 0)
			{
				return true;
			}
			if (videoCopy && videoQueue.size() < 5)
			{
				return true;
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 83, 130, 115, 127, 2, 127, 4, 131, 99 })]
		public virtual bool hasLeadingAudio()
		{
			VideoFrameWithPacket firstVideoFrame = (VideoFrameWithPacket)videoQueue.get(0);
			Iterator iterator = audioQueue.iterator();
			while (iterator.hasNext())
			{
				AudioFrameWithPacket audioFrame = (AudioFrameWithPacket)iterator.next();
				if (audioFrame.getPacket().getPtsD() >= firstVideoFrame.getPacket().getPtsD() + 0.2)
				{
					return true;
				}
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 88, 66, 105, 114, 110, 104, 105, 127, 5 })]
		public virtual void addVideoPacket(VideoFrameWithPacket videoFrame, VideoCodecMeta meta)
		{
			if (videoFrame.getFrame() != null)
			{
				pixelStore.retake(videoFrame.getFrame());
			}
			videoQueue.add(videoFrame);
			videoCodecMeta = meta;
			if (filters == null)
			{
				filters = initColorTransform(videoCodecMeta.getColor(), extraFilters, sink);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 86, 98, 110, 104 })]
		public virtual void addAudioPacket(AudioFrameWithPacket videoFrame, AudioCodecMeta meta)
		{
			audioQueue.add(videoFrame);
			audioCodecMeta = meta;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 113, 66, 111, 98, 119, 98, 105, 130, 211,
			105, 127, 2, 121, 99, 227, 69, 109, 108, 116,
			127, 5, 99, 142, 118, 159, 1, 238, 55, 236,
			76, 110, 118, 191, 0, 106, 121, 144
		})]
		public virtual void tryFlushQueues()
		{
			if (videoQueue.size() <= 0 || (videoCopy && videoQueue.size() < 5) || !hasLeadingAudio())
			{
				return;
			}
			VideoFrameWithPacket firstVideoFrame = (VideoFrameWithPacket)videoQueue.get(0);
			if (videoCopy)
			{
				Iterator iterator = videoQueue.iterator();
				while (iterator.hasNext())
				{
					VideoFrameWithPacket videoFrame = (VideoFrameWithPacket)iterator.next();
					if (videoFrame.getPacket().getFrameNo() < firstVideoFrame.getPacket().getFrameNo())
					{
						firstVideoFrame = videoFrame;
					}
				}
			}
			int aqSize = audioQueue.size();
			for (int af = 0; af < aqSize; af++)
			{
				AudioFrameWithPacket audioFrame = (AudioFrameWithPacket)audioQueue.get(0);
				if (audioFrame.getPacket().getPtsD() >= firstVideoFrame.getPacket().getPtsD() + 0.2)
				{
					break;
				}
				audioQueue.remove(0);
				if (audioCopy && sink is PacketSink)
				{
					((PacketSink)sink).outputAudioPacket(audioFrame.getPacket(), audioCodecMeta);
				}
				else
				{
					sink.outputAudioFrame(audioFrame);
				}
			}
			videoQueue.remove(firstVideoFrame);
			if (videoCopy && sink is PacketSink)
			{
				((PacketSink)sink).outputVideoPacket(firstVideoFrame.getPacket(), videoCodecMeta);
				return;
			}
			PixelStore.LoanerPicture frame = filterFrame(firstVideoFrame);
			sink.outputVideoFrame(new VideoFrameWithPacket(firstVideoFrame.getPacket(), frame));
			pixelStore.putBack(frame);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 98, 98, 99, 127, 2, 124, 99, 99, 103,
			159, 6, 122, 99, 118, 159, 1, 142, 102, 127,
			9, 104, 118, 159, 1, 107, 122, 174, 139, 127,
			6, 118, 159, 1, 142, 131
		})]
		public virtual void finalFlushQueues()
		{
			VideoFrameWithPacket lastVideoFrame = null;
			Iterator iterator = videoQueue.iterator();
			while (iterator.hasNext())
			{
				VideoFrameWithPacket videoFrame2 = (VideoFrameWithPacket)iterator.next();
				if (lastVideoFrame == null || videoFrame2.getPacket().getPtsD() >= lastVideoFrame.getPacket().getPtsD())
				{
					lastVideoFrame = videoFrame2;
				}
			}
			if (lastVideoFrame != null)
			{
				Iterator iterator2 = audioQueue.iterator();
				while (iterator2.hasNext())
				{
					AudioFrameWithPacket audioFrame2 = (AudioFrameWithPacket)iterator2.next();
					if (audioFrame2.getPacket().getPtsD() > lastVideoFrame.getPacket().getPtsD())
					{
						break;
					}
					if (audioCopy && sink is PacketSink)
					{
						((PacketSink)sink).outputAudioPacket(audioFrame2.getPacket(), audioCodecMeta);
					}
					else
					{
						sink.outputAudioFrame(audioFrame2);
					}
				}
				Iterator iterator3 = videoQueue.iterator();
				while (iterator3.hasNext())
				{
					VideoFrameWithPacket videoFrame = (VideoFrameWithPacket)iterator3.next();
					if (videoFrame != null)
					{
						if (videoCopy && sink is PacketSink)
						{
							((PacketSink)sink).outputVideoPacket(videoFrame.getPacket(), videoCodecMeta);
							continue;
						}
						PixelStore.LoanerPicture frame = filterFrame(videoFrame);
						sink.outputVideoFrame(new VideoFrameWithPacket(videoFrame.getPacket(), frame));
						pixelStore.putBack(frame);
					}
				}
				return;
			}
			Iterator iterator4 = audioQueue.iterator();
			while (iterator4.hasNext())
			{
				AudioFrameWithPacket audioFrame = (AudioFrameWithPacket)iterator4.next();
				if (audioCopy && sink is PacketSink)
				{
					((PacketSink)sink).outputAudioPacket(audioFrame.getPacket(), audioCodecMeta);
				}
				else
				{
					sink.outputAudioFrame(audioFrame);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 102, 98, 104, 127, 2, 99, 180, 100, 133,
			141, 99
		})]
		private PixelStore.LoanerPicture filterFrame(VideoFrameWithPacket firstVideoFrame)
		{
			PixelStore.LoanerPicture frame = firstVideoFrame.getFrame();
			Iterator iterator = filters.iterator();
			while (iterator.hasNext())
			{
				Filter filter = (Filter)iterator.next();
				PixelStore.LoanerPicture old = frame;
				frame = filter.filter(frame.getPicture(), pixelStore);
				if (frame == null)
				{
					frame = old;
				}
				else
				{
					pixelStore.putBack(old);
				}
			}
			return frame;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Lorg/jcodec/common/model/ColorSpace;Ljava/util/List<Lorg/jcodec/api/transcode/Filter;>;Lorg/jcodec/api/transcode/Sink;)Ljava/util/List<Lorg/jcodec/api/transcode/Filter;>;")]
		[LineNumberTable(new byte[]
		{
			159, 118, 130, 103, 124, 104, 106, 142, 105, 110,
			105, 102, 105, 106, 111
		})]
		private List initColorTransform(ColorSpace sourceColor, List extraFilters, Sink sink)
		{
			ArrayList filters = new ArrayList();
			Iterator iterator = extraFilters.iterator();
			while (iterator.hasNext())
			{
				Filter filter = (Filter)iterator.next();
				ColorSpace inputColor2 = filter.getInputColor();
				if (!sourceColor.matches(inputColor2))
				{
					((List)filters).add((object)new ColorTransformFilter(inputColor2));
				}
				((List)filters).add((object)filter);
				if (filter.getOutputColor() != ColorSpace.___003C_003ESAME)
				{
					sourceColor = filter.getOutputColor();
				}
			}
			ColorSpace inputColor = sink.getInputColor();
			if (inputColor != null && inputColor != sourceColor)
			{
				((List)filters).add((object)new ColorTransformFilter(inputColor));
			}
			return filters;
		}
	}

	public class TranscoderBuilder : Object
	{
		[Signature("Ljava/util/List<Lorg/jcodec/api/transcode/Source;>;")]
		private List source;

		[Signature("Ljava/util/List<Lorg/jcodec/api/transcode/Sink;>;")]
		private List sink;

		[Signature("Ljava/util/List<Ljava/util/List<Lorg/jcodec/api/transcode/Filter;>;>;")]
		private List filters;

		private IntArrayList seekFrames;

		private IntArrayList maxFrames;

		[Signature("Ljava/util/List<Lorg/jcodec/api/transcode/Transcoder$Mapping;>;")]
		private List videoMappings;

		[Signature("Ljava/util/List<Lorg/jcodec/api/transcode/Transcoder$Mapping;>;")]
		private List audioMappings;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 29, 162, 110, 109, 113 })]
		public virtual TranscoderBuilder addSource(Source source)
		{
			this.source.add(source);
			seekFrames.add(0);
			maxFrames.add(int.MaxValue);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 31, 98, 110 })]
		public virtual TranscoderBuilder setSeekFrames(int source, int seekFrames)
		{
			this.seekFrames.set(source, seekFrames);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 30, 130, 110 })]
		public virtual TranscoderBuilder setMaxFrames(int source, int maxFrames)
		{
			this.maxFrames.set(source, maxFrames);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 27, 130, 110, 116, 116, 114 })]
		public virtual TranscoderBuilder addSink(Sink sink)
		{
			this.sink.add(sink);
			videoMappings.add(new Mapping(0, copy: false));
			audioMappings.add(new Mapping(0, copy: false));
			filters.add(new ArrayList());
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 24, 161, 67, 117 })]
		public virtual TranscoderBuilder setAudioMapping(int src, int sink, bool copy)
		{
			audioMappings.set(sink, new Mapping(src, copy));
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 25, 129, 67, 117 })]
		public virtual TranscoderBuilder setVideoMapping(int src, int sink, bool copy)
		{
			videoMappings.set(sink, new Mapping(src, copy));
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 32, 66, 121 })]
		public virtual TranscoderBuilder addFilter(int sink, Filter filter)
		{
			((List)filters.get(sink)).add(filter);
			return this;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 22, 66, 127, 26, 127, 14, 31, 10 })]
		public virtual Transcoder create()
		{
			Transcoder result = new Transcoder((Source[])source.toArray(new Source[0]), (Sink[])sink.toArray(new Sink[0]), (Mapping[])videoMappings.toArray(new Mapping[0]), (Mapping[])audioMappings.toArray(new Mapping[0]), (List[])filters.toArray(new List[0]), seekFrames.toArray(), maxFrames.toArray(), null);
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 35, 98, 105, 108, 108, 108, 110, 110, 108,
			108
		})]
		public TranscoderBuilder()
		{
			source = new ArrayList();
			sink = new ArrayList();
			filters = new ArrayList();
			seekFrames = new IntArrayList(20);
			maxFrames = new IntArrayList(20);
			videoMappings = new ArrayList();
			audioMappings = new ArrayList();
		}
	}

	internal const int REORDER_BUFFER_SIZE = 7;

	private Source[] sources;

	private Sink[] sinks;

	[Signature("[Ljava/util/List<Lorg/jcodec/api/transcode/Filter;>;")]
	private List[] extraFilters;

	private int[] seekFrames;

	private int[] maxFrames;

	private Mapping[] videoMappings;

	private Mapping[] audioMappings;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(487)]
	public static TranscoderBuilder newTranscoder()
	{
		TranscoderBuilder result = new TranscoderBuilder();
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 80, 98, 135, 110, 110, 110, 111, 111, 111,
		111, 143, 111, 106, 10, 233, 69, 111, 47, 169,
		111, 112, 24, 233, 69, 114, 159, 24, 104, 125,
		121, 113, 148, 179, 125, 121, 113, 149, 243, 47,
		236, 89, 114, 236, 69, 110, 127, 4, 127, 4,
		131, 200, 113, 100, 108, 112, 106, 101, 140, 117,
		114, 38, 200, 111, 101, 137, 149, 235, 70, 107,
		127, 4, 106, 45, 169, 99, 170, 101, 127, 4,
		113, 195, 106, 238, 69, 207, 113, 106, 101, 137,
		111, 101, 103, 134, 171, 101, 127, 4, 113, 131,
		99, 231, 159, 171, 236, 160, 91, 107, 43, 233,
		69, 100, 111, 49, 201, 101, 99, 134, 107, 43,
		203, 111, 46, 137, 111, 47, 12, 111, 46, 137,
		111, 47, 137, 99
	})]
	public virtual void transcode()
	{
		PixelStoreImpl pixelStore = new PixelStoreImpl();
		List[] videoStreams = new List[(nint)sources.LongLength];
		List[] audioStreams = new List[(nint)sources.LongLength];
		bool[] decodeVideo = new bool[(nint)sources.LongLength];
		bool[] decodeAudio = new bool[(nint)sources.LongLength];
		bool[] finishedVideo = new bool[(nint)sources.LongLength];
		bool[] finishedAudio = new bool[(nint)sources.LongLength];
		Stream[] allStreams = new Stream[(nint)sinks.LongLength];
		int[] videoFramesRead = new int[(nint)sources.LongLength];
		for (int s6 = 0; s6 < (nint)sources.LongLength; s6++)
		{
			videoStreams[s6] = new ArrayList();
			audioStreams[s6] = new ArrayList();
		}
		for (int n = 0; n < (nint)sinks.LongLength; n++)
		{
			sinks[n].init();
		}
		for (int m = 0; m < (nint)sources.LongLength; m++)
		{
			sources[m].init(pixelStore);
			sources[m].seekFrames(seekFrames[m]);
		}
		for (int s5 = 0; s5 < (nint)sinks.LongLength; s5++)
		{
			Stream stream5 = (allStreams[s5] = new Stream(sinks[s5], Mapping.access_0024000(videoMappings[s5]), Mapping.access_0024000(audioMappings[s5]), extraFilters[s5], pixelStore));
			if (sources[Mapping.access_0024100(videoMappings[s5])].isVideo())
			{
				videoStreams[Mapping.access_0024100(videoMappings[s5])].add(stream5);
				if (!Mapping.access_0024000(videoMappings[s5]))
				{
					decodeVideo[Mapping.access_0024100(videoMappings[s5])] = true;
				}
			}
			else
			{
				finishedVideo[Mapping.access_0024100(videoMappings[s5])] = true;
			}
			if (sources[Mapping.access_0024100(audioMappings[s5])].isAudio())
			{
				audioStreams[Mapping.access_0024100(audioMappings[s5])].add(stream5);
				if (!Mapping.access_0024000(audioMappings[s5]))
				{
					decodeAudio[Mapping.access_0024100(audioMappings[s5])] = true;
				}
			}
			else
			{
				finishedAudio[Mapping.access_0024100(audioMappings[s5])] = true;
			}
		}
		try
		{
			int allFinished;
			do
			{
				for (int s4 = 0; s4 < (nint)sources.LongLength; s4++)
				{
					Source source = sources[s4];
					int needsVideoFrame = ((!finishedVideo[s4]) ? 1 : 0);
					Iterator iterator = videoStreams[s4].iterator();
					while (iterator.hasNext())
					{
						Stream stream4 = (Stream)iterator.next();
						needsVideoFrame &= ((stream4.needsVideoFrame() || stream4.hasLeadingAudio() || finishedAudio[s4]) ? 1 : 0);
					}
					if (needsVideoFrame != 0)
					{
						VideoFrameWithPacket nextVideoFrame;
						if (videoFramesRead[s4] >= maxFrames[s4])
						{
							nextVideoFrame = null;
							finishedVideo[s4] = true;
						}
						else if (decodeVideo[s4] || !(source is PacketSource))
						{
							nextVideoFrame = source.getNextVideoFrame();
							if (nextVideoFrame == null)
							{
								finishedVideo[s4] = true;
							}
							else
							{
								int num = s4;
								int[] array = videoFramesRead;
								array[num]++;
								printLegend((int)nextVideoFrame.getPacket().getFrameNo(), 0, nextVideoFrame.getPacket());
							}
						}
						else
						{
							Packet packet2 = ((PacketSource)source).inputVideoPacket();
							if (packet2 == null)
							{
								finishedVideo[s4] = true;
							}
							else
							{
								int num = s4;
								int[] array = videoFramesRead;
								array[num]++;
							}
							nextVideoFrame = new VideoFrameWithPacket(packet2, null);
						}
						if (finishedVideo[s4])
						{
							Iterator iterator2 = videoStreams[s4].iterator();
							while (iterator2.hasNext())
							{
								Stream stream3 = (Stream)iterator2.next();
								for (int ss = 0; ss < (nint)audioStreams.LongLength; ss++)
								{
									audioStreams[ss].remove(stream3);
								}
							}
							videoStreams[s4].clear();
						}
						if (nextVideoFrame != null)
						{
							Iterator iterator3 = videoStreams[s4].iterator();
							while (iterator3.hasNext())
							{
								Stream stream2 = (Stream)iterator3.next();
								stream2.addVideoPacket(nextVideoFrame, source.getVideoCodecMeta());
							}
							if (nextVideoFrame.getFrame() != null)
							{
								((PixelStore)pixelStore).putBack(nextVideoFrame.getFrame());
							}
						}
					}
					if (!audioStreams[s4].isEmpty())
					{
						AudioFrameWithPacket nextAudioFrame;
						if (decodeAudio[s4] || !(source is PacketSource))
						{
							nextAudioFrame = source.getNextAudioFrame();
							if (nextAudioFrame == null)
							{
								finishedAudio[s4] = true;
							}
						}
						else
						{
							Packet packet = ((PacketSource)source).inputAudioPacket();
							if (packet == null)
							{
								finishedAudio[s4] = true;
								nextAudioFrame = null;
							}
							else
							{
								nextAudioFrame = new AudioFrameWithPacket(null, packet);
							}
						}
						if (nextAudioFrame != null)
						{
							Iterator iterator4 = audioStreams[s4].iterator();
							while (iterator4.hasNext())
							{
								Stream stream = (Stream)iterator4.next();
								stream.addAudioPacket(nextAudioFrame, source.getAudioCodecMeta());
							}
						}
					}
					else
					{
						finishedAudio[s4] = true;
					}
				}
				for (int s3 = 0; s3 < (nint)allStreams.LongLength; s3++)
				{
					allStreams[s3].tryFlushQueues();
				}
				allFinished = 1;
				for (int s2 = 0; s2 < (nint)sources.LongLength; s2++)
				{
					allFinished &= ((finishedVideo[s2] & finishedAudio[s2]) ? 1 : 0);
				}
			}
			while (allFinished == 0);
			for (int s = 0; s < (nint)allStreams.LongLength; s++)
			{
				allStreams[s].finalFlushQueues();
			}
		}
		catch
		{
			//try-fault
			for (int l = 0; l < (nint)sources.LongLength; l++)
			{
				sources[0].finish();
			}
			for (int k = 0; k < (nint)sinks.LongLength; k++)
			{
				sinks[k].finish();
			}
			throw;
		}
		for (int j = 0; j < (nint)sources.LongLength; j++)
		{
			sources[0].finish();
		}
		for (int i = 0; i < (nint)sinks.LongLength; i++)
		{
			sinks[i].finish();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 130, 105, 105, 104, 137, 105, 137, 104,
		104
	})]
	private Transcoder(Source[] source, Sink[] sink, Mapping[] videoMappings, Mapping[] audioMappings, List[] extraFilters, int[] seekFrames, int[] maxFrames)
	{
		this.extraFilters = extraFilters;
		this.videoMappings = videoMappings;
		this.audioMappings = audioMappings;
		this.seekFrames = seekFrames;
		this.maxFrames = maxFrames;
		sources = source;
		sinks = sink;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 39, 162, 112, 127, 7 })]
	private void printLegend(int frameNo, int maxFrames, Packet inVideoPacket)
	{
		if (100 == -1 || frameNo % 100 == 0)
		{
			java.lang.System.@out.print(String.format("[%6d]\r", Integer.valueOf(frameNo)));
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Synthetic)]
	[LineNumberTable(29)]
	internal Transcoder(Source[] x0, Sink[] x1, Mapping[] x2, Mapping[] x3, List[] x4, int[] x5, int[] x6, _1 x7)
		: this(x0, x1, x2, x3, x4, x5, x6)
	{
	}
}
