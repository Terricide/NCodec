using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.io;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.api;
using org.jcodec.common.io;
using org.jcodec.common.logging;

namespace org.jcodec.containers.avi;

public class AVIReader : java.lang.Object
{
	internal class AVI_SEGM : AVIChunk
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(487)]
		internal AVI_SEGM()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 20, 130, 107 })]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
		}

		[LineNumberTable(new byte[] { 159, 18, 66, 105, 131 })]
		public override int getChunkSize()
		{
			if (dwChunkSize == 0)
			{
				return 0;
			}
			return dwChunkSize + 1;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(505)]
		public override string toString()
		{
			string result = new StringBuilder().append("SEGMENT Align, Size [").append(dwChunkSize).append("], StartOfChunk [")
				.append(getStartOfChunk())
				.append("]")
				.toString();
			
			return result;
		}
	}

	internal class AVIChunk : java.lang.Object
	{
		protected internal int dwFourCC;

		protected internal string fwFourCCStr;

		protected internal int dwChunkSize;

		protected internal long startOfChunk;

		[LineNumberTable(new byte[] { 159, 34, 98, 108, 138 })]
		public virtual int getChunkSize()
		{
			if ((dwChunkSize & 1) == 1)
			{
				return dwChunkSize + 1;
			}
			return dwChunkSize;
		}

		[LineNumberTable(408)]
		public virtual long getStartOfChunk()
		{
			return startOfChunk;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(391)]
		internal AVIChunk()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 43, 130, 208, 104, 109, 109 })]
		public virtual void read(int dwFourCC, DataReader raf)
		{
			startOfChunk = raf.position() - 4u;
			this.dwFourCC = dwFourCC;
			fwFourCCStr = toFourCC(dwFourCC);
			dwChunkSize = raf.readInt();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(412)]
		public virtual long getEndOfChunk()
		{
			return startOfChunk + 8u + getChunkSize();
		}

		[LineNumberTable(417)]
		public virtual int getFourCC()
		{
			return dwFourCC;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 37, 98, 104, 101, 159, 27, 105 })]
		public virtual void skip(DataReader raf)
		{
			int chunkSize = getChunkSize();
			if (chunkSize < 0)
			{
				string message = new StringBuilder().append("Negative chunk size for chunk [").append(toFourCC(dwFourCC)).append("]")
					.toString();
				
				throw new IOException(message);
			}
			raf.skipBytes(chunkSize);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 32, 130, 109, 110, 173 })]
		public override string toString()
		{
			string chunkStr = toFourCC(dwFourCC);
			if (java.lang.String.instancehelper_length(java.lang.String.instancehelper_trim(chunkStr)) == 0)
			{
				chunkStr = Integer.toHexString(dwFourCC);
			}
			string result = new StringBuilder().append("\tCHUNK [").append(chunkStr).append("], Size [")
				.append(dwChunkSize)
				.append("], StartOfChunk [")
				.append(getStartOfChunk())
				.append("]")
				.toString();
			
			return result;
		}
	}

	internal class AVIList : AVIChunk
	{
		protected internal int dwListTypeFourCC;

		protected internal string dwListTypeFourCCStr;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(454)]
		internal AVIList()
		{
		}

		[LineNumberTable(469)]
		public virtual int getListType()
		{
			return dwListTypeFourCC;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 159, 27, 66, 137, 143, 109, 114 })]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
			dwChunkSize -= 4;
			dwListTypeFourCC = raf.readInt();
			dwListTypeFourCCStr = toFourCC(dwListTypeFourCC);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 22, 66, 141, 127, 34, 60 })]
		public override string toString()
		{
			string dwFourCCStr = toFourCC(dwFourCC);
			string result = new StringBuilder().append(dwFourCCStr).append(" [").append(dwListTypeFourCCStr)
				.append("], Size [")
				.append(dwChunkSize)
				.append("], StartOfChunk [")
				.append(getStartOfChunk())
				.append("]")
				.toString();
			
			return result;
		}
	}

	internal class AVITag_AudioChunk : AVIChunk
	{
		protected internal int streamNo;

		private DataReader raf;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1014)]
		internal AVITag_AudioChunk()
		{
		}

		[LineNumberTable(new byte[] { 158, 140, 98, 108, 138 })]
		public override int getChunkSize()
		{
			if ((dwChunkSize & 1) == 1)
			{
				return dwChunkSize + 1;
			}
			return dwChunkSize;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 158, 143, 66, 104, 137, 104, 116 })]
		public override void read(int dwFourCC, DataReader raf)
		{
			this.raf = raf;
			base.read(dwFourCC, raf);
			string fourccStr = toFourCC(dwFourCC);
			streamNo = Integer.parseInt(java.lang.String.instancehelper_substring(fourccStr, 0, 2));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			158, 138, 66, 141, 110, 106, 191, 38, 111, 101,
			142
		})]
		public virtual byte[] getAudioPacket()
		{
			byte[] audioFrameData = new byte[dwChunkSize];
			int bytesRead = raf.readFully(audioFrameData);
			if (bytesRead != dwChunkSize)
			{
				string message = new StringBuilder().append("Read mismatch expected chunksize [").append(dwChunkSize).append("], Actual read [")
					.append(bytesRead)
					.append("]")
					.toString();
				
				throw new IOException(message);
			}
			int alignment = getChunkSize() - dwChunkSize;
			if (alignment > 0)
			{
				raf.skipBytes(alignment);
			}
			return audioFrameData;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 134, 66, 127, 28, 50 })]
		public override string toString()
		{
			string result = new StringBuilder().append("\tAUDIO CHUNK - Stream ").append(streamNo).append(", StartOfChunk=")
				.append(getStartOfChunk())
				.append(", ChunkSize=")
				.append(getChunkSize())
				.toString();
			
			return result;
		}
	}

	internal class AVITag_AviDmlStandardIndex : AVIChunk
	{
		protected internal short wLongsPerEntry;

		protected internal byte bIndexSubType;

		protected internal byte bIndexType;

		protected internal int nEntriesInUse;

		protected internal int dwChunkId;

		protected internal long qwBaseOffset;

		protected internal int dwReserved2;

		protected internal int[] dwOffset;

		protected internal int[] dwDuration;

		internal int lastOffset;

		internal int lastDuration;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 88, 66, 233, 77 })]
		internal AVITag_AviDmlStandardIndex()
		{
			lastOffset = -1;
			lastDuration = -1;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158,
			75,
			66,
			150,
			127,
			41,
			byte.MaxValue,
			52,
			61
		})]
		public override string toString()
		{
			string result = java.lang.String.format("\tAvi DML Standard Index List Type=%d, SubType=%d, ChunkId=%s, StartOfChunk=%d, NumIndexes=%d, LongsPerEntry=%d, ChunkSize=%d, FirstOffset=%d, FirstDuration=%d,LastOffset=%d, LastDuration=%d", java.lang.Byte.valueOf((byte)(sbyte)bIndexType), java.lang.Byte.valueOf((byte)(sbyte)bIndexSubType), toFourCC(dwChunkId), Long.valueOf(getStartOfChunk()), Integer.valueOf(nEntriesInUse), Short.valueOf(wLongsPerEntry), Integer.valueOf(getChunkSize()), Integer.valueOf(dwOffset[0]), Integer.valueOf(dwDuration[0]), Integer.valueOf(lastOffset), Integer.valueOf(lastDuration));
			
			return result;
		}

		[LineNumberTable(1257)]
		public override int getChunkSize()
		{
			return dwChunkSize;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			158, 83, 130, 137, 109, 110, 110, 109, 109, 109,
			141, 114, 178, 108, 111, 143, 111, 239, 59, 252,
			73, 3, 98, 191, 6, 110
		})]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
			wLongsPerEntry = raf.readShort();
			bIndexSubType = (byte)(sbyte)raf.readByte();
			bIndexType = (byte)(sbyte)raf.readByte();
			nEntriesInUse = raf.readInt();
			dwChunkId = raf.readInt();
			qwBaseOffset = raf.readLong();
			dwReserved2 = raf.readInt();
			dwOffset = new int[nEntriesInUse];
			dwDuration = new int[nEntriesInUse];
			java.lang.Exception ex2;
			try
			{
				for (int i = 0; i < nEntriesInUse; i++)
				{
					dwOffset[i] = raf.readInt();
					dwDuration[i] = raf.readInt();
					lastOffset = dwOffset[i];
					lastDuration = dwDuration[i];
				}
			}
			catch (System.Exception x)
			{
				java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
				if (ex == null)
				{
					throw;
				}
				ex2 = ex;
				goto IL_00ec;
			}
			goto IL_0119;
			IL_00ec:
			java.lang.Exception e = ex2;
			Logger.debug(new StringBuilder().append("Failed to read : ").append(toString()).toString());
			goto IL_0119;
			IL_0119:
			raf.setPosition(getEndOfChunk());
		}
	}

	internal class AVITag_AviDmlSuperIndex : AVIChunk
	{
		protected internal short wLongsPerEntry;

		protected internal byte bIndexSubType;

		protected internal byte bIndexType;

		protected internal int nEntriesInUse;

		protected internal int dwChunkId;

		protected internal int[] dwReserved;

		protected internal long[] qwOffset;

		protected internal int[] dwSize;

		protected internal int[] dwDuration;

		private int numIndex;

		private int numIndexFill;

		internal StringBuilder sb;

		private int streamNo;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 105, 66, 233, 61, 200, 109, 140 })]
		public AVITag_AviDmlSuperIndex()
		{
			streamNo = 0;
			dwReserved = new int[3];
			sb = new StringBuilder();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			158, 103, 66, 137, 109, 110, 110, 109, 141, 111,
			111, 143, 114, 114, 146, 109, 149, 127, 24, 231,
			61, 236, 69, 111, 111, 111, 143, 127, 13, 55,
			236, 59, 234, 73, 110
		})]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
			wLongsPerEntry = raf.readShort();
			bIndexSubType = (byte)(sbyte)raf.readByte();
			bIndexType = (byte)(sbyte)raf.readByte();
			nEntriesInUse = raf.readInt();
			dwChunkId = raf.readInt();
			dwReserved[0] = raf.readInt();
			dwReserved[1] = raf.readInt();
			dwReserved[2] = raf.readInt();
			qwOffset = new long[nEntriesInUse];
			dwSize = new int[nEntriesInUse];
			dwDuration = new int[nEntriesInUse];
			string chunkIdStr = toFourCC(dwChunkId);
			sb.append(java.lang.String.format("\tAvi DML Super Index List - ChunkSize=%d, NumIndexes = %d, longsPerEntry = %d, Stream = %s, Type = %s", Integer.valueOf(getChunkSize()), Integer.valueOf(nEntriesInUse), Short.valueOf(wLongsPerEntry), java.lang.String.instancehelper_substring(chunkIdStr, 0, 2), java.lang.String.instancehelper_substring(chunkIdStr, 2)));
			for (int i = 0; i < nEntriesInUse; i++)
			{
				qwOffset[i] = raf.readLong();
				dwSize[i] = raf.readInt();
				dwDuration[i] = raf.readInt();
				sb.append(java.lang.String.format("\n\t\tStandard Index - Offset [%d], Size [%d], Duration [%d]", Long.valueOf(qwOffset[i]), Integer.valueOf(dwSize[i]), Integer.valueOf(dwDuration[i])));
			}
			raf.setPosition(getEndOfChunk());
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1216)]
		public override string toString()
		{
			string result = sb.toString();
			
			return result;
		}
	}

	internal class AVITag_AVIH : AVIChunk
	{
		public string _getHeight;

		internal const int AVIF_HASINDEX = 16;

		internal const int AVIF_MUSTUSEINDEX = 32;

		internal const int AVIF_ISINTERLEAVED = 256;

		internal const int AVIF_TRUSTCKTYPE = 2048;

		internal const int AVIF_WASCAPTUREFILE = 65536;

		internal const int AVIF_COPYRIGHTED = 131072;

		private int dwMicroSecPerFrame;

		private int dwMaxBytesPerSec;

		private int dwPaddingGranularity;

		private int dwFlags;

		private int dwTotalFrames;

		private int dwInitialFrames;

		private int dwStreams;

		private int dwSuggestedBufferSize;

		private int dwWidth;

		private int dwHeight;

		private int[] dwReserved;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 9, 130, 105, 109 })]
		public AVITag_AVIH()
		{
			dwReserved = new int[4];
		}

		[LineNumberTable(572)]
		public virtual int getStreams()
		{
			return dwStreams;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			159, 7, 66, 105, 105, 159, 12, 107, 145, 109,
			109, 109, 109, 109, 109, 109, 109, 109, 109, 111,
			111, 111, 111
		})]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
			if (dwFourCC != 1751742049)
			{
				string message = new StringBuilder().append("Unexpected AVI header : ").append(toFourCC(dwFourCC)).toString();
				
				throw new IOException(message);
			}
			if (getChunkSize() != 56)
			{
				
				throw new IOException("Expected dwSize=56");
			}
			dwMicroSecPerFrame = raf.readInt();
			dwMaxBytesPerSec = raf.readInt();
			dwPaddingGranularity = raf.readInt();
			dwFlags = raf.readInt();
			dwTotalFrames = raf.readInt();
			dwInitialFrames = raf.readInt();
			dwStreams = raf.readInt();
			dwSuggestedBufferSize = raf.readInt();
			dwWidth = raf.readInt();
			dwHeight = raf.readInt();
			dwReserved[0] = raf.readInt();
			dwReserved[1] = raf.readInt();
			dwReserved[2] = raf.readInt();
			dwReserved[3] = raf.readInt();
		}

		[LineNumberTable(564)]
		public virtual int getWidth()
		{
			return dwWidth;
		}

		[LineNumberTable(568)]
		public virtual int getHeight()
		{
			return dwHeight;
		}

		[LineNumberTable(576)]
		public virtual int getTotalFrames()
		{
			return dwTotalFrames;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 253, 98, 135, 108, 109, 108, 109, 111, 109,
			111, 109, 111, 141, 127, 54, 63, 23
		})]
		public override string toString()
		{
			StringBuilder sb = new StringBuilder();
			if (((uint)dwFlags & 0x10u) != 0)
			{
				sb.append("HASINDEX ");
			}
			if (((uint)dwFlags & 0x20u) != 0)
			{
				sb.append("MUSTUSEINDEX ");
			}
			if (((uint)dwFlags & 0x100u) != 0)
			{
				sb.append("ISINTERLEAVED ");
			}
			if (((uint)dwFlags & 0x10000u) != 0)
			{
				sb.append("AVIF_WASCAPTUREFILE ");
			}
			if (((uint)dwFlags & 0x20000u) != 0)
			{
				sb.append("AVIF_COPYRIGHTED ");
			}
			string result = new StringBuilder().append("AVIH Resolution [").append(dwWidth).append("x")
				.append(dwHeight)
				.append("], NumFrames [")
				.append(dwTotalFrames)
				.append("], Flags [")
				.append(Integer.toHexString(dwFlags))
				.append("] - [")
				.append(java.lang.String.instancehelper_trim(sb.toString()))
				.append("]")
				.toString();
			
			return result;
		}
	}

	internal class AVITag_AviIndex : AVIChunk
	{
		protected internal int numIndexes;

		protected internal int[] ckid;

		protected internal int[] dwFlags;

		protected internal int[] dwChunkOffset;

		protected internal int[] dwChunkLength;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 131, 162, 105 })]
		internal AVITag_AviIndex()
		{
			numIndexes = 0;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			158, 128, 66, 137, 143, 114, 114, 114, 146, 108,
			111, 111, 111, 239, 60, 231, 71, 142, 111, 101,
			105
		})]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
			numIndexes = getChunkSize() >> 4;
			ckid = new int[numIndexes];
			dwFlags = new int[numIndexes];
			dwChunkOffset = new int[numIndexes];
			dwChunkLength = new int[numIndexes];
			for (int i = 0; i < numIndexes; i++)
			{
				ckid[i] = raf.readInt();
				dwFlags[i] = raf.readInt();
				dwChunkOffset[i] = raf.readInt();
				dwChunkLength[i] = raf.readInt();
			}
			raf.setPosition(getEndOfChunk());
			int alignment = getChunkSize() - dwChunkSize;
			if (alignment > 0)
			{
				raf.skipBytes(alignment);
			}
		}

		[LineNumberTable(1104)]
		public virtual int getNumIndexes()
		{
			return numIndexes;
		}

		[LineNumberTable(1108)]
		public virtual int[] getCkid()
		{
			return ckid;
		}

		[LineNumberTable(1112)]
		public virtual int[] getDwFlags()
		{
			return dwFlags;
		}

		[LineNumberTable(1116)]
		public virtual int[] getDwChunkOffset()
		{
			return dwChunkOffset;
		}

		[LineNumberTable(1120)]
		public virtual int[] getDwChunkLength()
		{
			return dwChunkLength;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 117, 66, 108, 43, 199 })]
		public virtual void debugOut()
		{
			for (int i = 0; i < numIndexes; i++)
			{
				Logger.debug("\t");
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 115, 66, 111, 63, 11 })]
		public override string toString()
		{
			string result = java.lang.String.format("\tAvi Index List, StartOfChunk [%d], ChunkSize [%d], NumIndexes [%d]", Long.valueOf(getStartOfChunk()), Integer.valueOf(dwChunkSize), Integer.valueOf(getChunkSize() >> 4));
			
			return result;
		}
	}

	internal class AVITag_BitmapInfoHeader : AVIChunk
	{
		private int biSize;

		private int biWidth;

		private int biHeight;

		private short biPlanes;

		private short biBitCount;

		private int biCompression;

		private int biSizeImage;

		private int biXPelsPerMeter;

		private int biYPelsPerMeter;

		private int biClrUsed;

		private int biClrImportant;

		private byte r;

		private byte g;

		private byte b;

		private byte x;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(684)]
		internal AVITag_BitmapInfoHeader()
		{
		}

		[LineNumberTable(740)]
		public override int getChunkSize()
		{
			return biSize;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			158, 222, 98, 137, 109, 109, 109, 109, 109, 109,
			109, 109, 109, 109, 141, 203, 110, 110, 110, 238,
			74
		})]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
			biSize = raf.readInt();
			biWidth = raf.readInt();
			biHeight = raf.readInt();
			biPlanes = raf.readShort();
			biBitCount = raf.readShort();
			biCompression = raf.readInt();
			biSizeImage = raf.readInt();
			biXPelsPerMeter = raf.readInt();
			biYPelsPerMeter = raf.readInt();
			biClrUsed = raf.readInt();
			biClrImportant = raf.readInt();
			if (getChunkSize() == 56)
			{
				r = (byte)(sbyte)raf.readByte();
				g = (byte)(sbyte)raf.readByte();
				b = (byte)(sbyte)raf.readByte();
				x = (byte)(sbyte)raf.readByte();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(745)]
		public override string toString()
		{
			string result = new StringBuilder().append("\tCHUNK [").append(toFourCC(dwFourCC)).append("], BitsPerPixel [")
				.append(biBitCount)
				.append("], Resolution [")
				.append(biWidth & 0xFFFFFFFFu)
				.append(" x ")
				.append(biHeight & 0xFFFFFFFFu)
				.append("], Planes [")
				.append(biPlanes)
				.append("]")
				.toString();
			
			return result;
		}
	}

	internal class AVITag_STRH : AVIChunk
	{
		internal const int AVISF_DISABLED = 1;

		internal const int AVISF_VIDEO_PALCHANGES = 65536;

		private int fccType;

		private int fccCodecHandler;

		private int dwFlags;

		private short wPriority;

		private short wLanguage;

		private int dwInitialFrames;

		private int dwScale;

		private int dwRate;

		private int dwStart;

		private int dwLength;

		private int dwSuggestedBufferSize;

		private int dwQuality;

		private int dwSampleSize;

		private short left;

		private short top;

		private short right;

		private short bottom;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 249, 162, 233, 72, 104, 104, 104, 104, 104,
			108, 104, 104, 104, 104, 104, 104, 104, 104
		})]
		internal AVITag_STRH()
		{
			dwFlags = 0;
			wPriority = 0;
			wLanguage = 0;
			dwInitialFrames = 0;
			dwScale = 0;
			dwRate = 1000000;
			dwStart = 0;
			dwLength = 0;
			dwSuggestedBufferSize = 0;
			dwQuality = -1;
			dwSampleSize = 0;
			left = 0;
			top = 0;
			right = 0;
			bottom = 0;
		}

		[LineNumberTable(653)]
		public virtual int getType()
		{
			return fccType;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			158, 242, 98, 105, 105, 159, 27, 141, 109, 141,
			109, 141, 109, 109, 109, 109, 109, 109, 109, 141,
			109, 109, 109, 109
		})]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
			if (dwFourCC != 1752331379)
			{
				string message = new StringBuilder().append("Expected 'strh' fourcc got [").append(toFourCC(base.dwFourCC)).append("]")
					.toString();
				
				throw new IOException(message);
			}
			fccType = raf.readInt();
			fccCodecHandler = raf.readInt();
			dwFlags = raf.readInt();
			wPriority = raf.readShort();
			wLanguage = raf.readShort();
			dwInitialFrames = raf.readInt();
			dwScale = raf.readInt();
			dwRate = raf.readInt();
			dwStart = raf.readInt();
			dwLength = raf.readInt();
			dwSuggestedBufferSize = raf.readInt();
			dwQuality = raf.readInt();
			dwSampleSize = raf.readInt();
			left = raf.readShort();
			top = raf.readShort();
			right = raf.readShort();
			bottom = raf.readShort();
		}

		[LineNumberTable(657)]
		public virtual int getHandler()
		{
			return fccCodecHandler;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 233, 98, 105, 143 })]
		public virtual string getHandlerStr()
		{
			if (fccCodecHandler != 0)
			{
				string result = toFourCC(fccCodecHandler);
				
				return result;
			}
			return "";
		}

		[LineNumberTable(668)]
		public virtual int getInitialFrames()
		{
			return dwInitialFrames;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 230, 98, 127, 68, 63, 4 })]
		public override string toString()
		{
			string result = new StringBuilder().append("\tCHUNK [").append(toFourCC(dwFourCC)).append("], Type[")
				.append((fccType <= 0) ? "    " : toFourCC(fccType))
				.append("], Handler [")
				.append((fccCodecHandler <= 0) ? "    " : toFourCC(fccCodecHandler))
				.append("]")
				.toString();
			
			return result;
		}
	}

	internal class AVITag_VideoChunk : AVIChunk
	{
		protected internal int streamNo;

		protected internal bool compressed;

		protected internal int frameNo;

		private DataReader raf;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158, 159, 65, 67, 233, 59, 104, 232, 70, 104,
			104
		})]
		public AVITag_VideoChunk(bool compressed, DataReader raf)
		{
			this.compressed = false;
			frameNo = -1;
			this.compressed = compressed;
			this.raf = raf;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			158, 151, 162, 141, 110, 106, 191, 38, 111, 101,
			142
		})]
		public virtual byte[] getVideoPacket()
		{
			byte[] videoFrameData = new byte[dwChunkSize];
			int bytesRead = raf.readFully(videoFrameData);
			if (bytesRead != dwChunkSize)
			{
				string message = new StringBuilder().append("Read mismatch expected chunksize [").append(dwChunkSize).append("], Actual read [")
					.append(bytesRead)
					.append("]")
					.toString();
				
				throw new IOException(message);
			}
			int alignment = getChunkSize() - dwChunkSize;
			if (alignment > 0)
			{
				raf.skipBytes(alignment);
			}
			return videoFrameData;
		}

		[LineNumberTable(new byte[] { 158, 157, 162, 104 })]
		public virtual void setFrameNo(int frameNo)
		{
			this.frameNo = frameNo;
		}

		[LineNumberTable(new byte[] { 158, 154, 66, 108, 138 })]
		public override int getChunkSize()
		{
			if ((dwChunkSize & 1) == 1)
			{
				return dwChunkSize + 1;
			}
			return dwChunkSize;
		}

		[LineNumberTable(963)]
		public virtual int getStreamNo()
		{
			return streamNo;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[] { 158, 152, 66, 137, 104, 116 })]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
			string fourccStr = toFourCC(dwFourCC);
			streamNo = Integer.parseInt(java.lang.String.instancehelper_substring(fourccStr, 0, 2));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 146, 66, 127, 63, 63, 8 })]
		public override string toString()
		{
			string result = new StringBuilder().append("\tVIDEO CHUNK - Stream ").append(streamNo).append(",  chunkStart=")
				.append(getStartOfChunk())
				.append(", ")
				.append((!compressed) ? "uncompressed" : "compressed")
				.append(", ChunkSize=")
				.append(getChunkSize())
				.append(", FrameNo=")
				.append(frameNo)
				.toString();
			
			return result;
		}
	}

	internal class AVITag_WaveFormatEx : AVIChunk
	{
		public const int SPEAKER_FRONT_LEFT = 1;

		public const int SPEAKER_FRONT_RIGHT = 2;

		public const int SPEAKER_FRONT_CENTER = 4;

		public const int SPEAKER_LOW_FREQUENCY = 8;

		public const int SPEAKER_BACK_LEFT = 16;

		public const int SPEAKER_BACK_RIGHT = 32;

		public const int SPEAKER_FRONT_LEFT_OF_CENTER = 64;

		public const int SPEAKER_FRONT_RIGHT_OF_CENTER = 128;

		public const int SPEAKER_BACK_CENTER = 256;

		public const int SPEAKER_SIDE_LEFT = 512;

		public const int SPEAKER_SIDE_RIGHT = 1024;

		public const int SPEAKER_TOP_CENTER = 2048;

		public const int SPEAKER_TOP_FRONT_LEFT = 4096;

		public const int SPEAKER_TOP_FRONT_CENTER = 8192;

		public const int SPEAKER_TOP_FRONT_RIGHT = 16384;

		public const int SPEAKER_TOP_BACK_LEFT = 32768;

		public const int SPEAKER_TOP_BACK_CENTER = 65536;

		public const int SPEAKER_TOP_BACK_RIGHT = 131072;

		protected internal short wFormatTag;

		protected internal short channels;

		protected internal int nSamplesPerSec;

		protected internal int nAvgBytesPerSec;

		protected internal short nBlockAlign;

		protected internal short wBitsPerSample;

		protected internal short cbSize;

		protected internal short wValidBitsPerSample;

		protected internal short samplesValidBitsPerSample;

		protected internal short wReserved;

		protected internal int channelMask;

		protected internal int guid_data1;

		protected internal short guid_data2;

		protected internal short guid_data3;

		protected internal byte[] guid_data4;

		protected internal bool mp3Flag;

		protected internal short wID;

		protected internal int fdwFlags;

		protected internal short nBlockSize;

		protected internal short nFramesPerBlock;

		protected internal short nCodecDelay;

		private string audioFormat;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 192, 66, 233, 55, 232, 71, 172, 109 })]
		public AVITag_WaveFormatEx()
		{
			mp3Flag = false;
			audioFormat = "?";
			guid_data4 = new byte[8];
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Throws(new string[] { "java.io.IOException" })]
		[LineNumberTable(new byte[]
		{
			158,
			191,
			130,
			169,
			109,
			109,
			109,
			109,
			141,
			223,
			56,
			141,
			142,
			127,
			4,
			141,
			173,
			109,
			109,
			109,
			142,
			108,
			230,
			69,
			109,
			141,
			109,
			109,
			109,
			109,
			141,
			104,
			108,
			198,
			108,
			198,
			108,
			198,
			108,
			230,
			69,
			109,
			237,
			69,
			159,
			4,
			173,
			109,
			109,
			109,
			142,
			108,
			195,
			byte.MaxValue,
			12,
			69
		})]
		public override void read(int dwFourCC, DataReader raf)
		{
			base.read(dwFourCC, raf);
			wFormatTag = raf.readShort();
			channels = raf.readShort();
			nSamplesPerSec = raf.readInt();
			nAvgBytesPerSec = raf.readInt();
			nBlockAlign = raf.readShort();
			switch (wFormatTag)
			{
			case 1:
				wBitsPerSample = raf.readShort();
				if (dwChunkSize == 40)
				{
					int num = raf.readShort();
					int num4 = num;
					wReserved = (short)num;
					num = num4;
					int num5 = num;
					samplesValidBitsPerSample = (short)num;
					wValidBitsPerSample = (short)num5;
					cbSize = raf.readShort();
					channelMask = raf.readInt();
					guid_data1 = raf.readInt();
					guid_data2 = raf.readShort();
					guid_data3 = raf.readShort();
					raf.readFully(guid_data4);
				}
				audioFormat = "PCM";
				break;
			case 85:
				wBitsPerSample = raf.readShort();
				cbSize = raf.readShort();
				wID = raf.readShort();
				fdwFlags = raf.readInt();
				nBlockSize = raf.readShort();
				nFramesPerBlock = raf.readShort();
				nCodecDelay = raf.readShort();
				mp3Flag = true;
				audioFormat = "MP3";
				break;
			case 8192:
				audioFormat = "AC3";
				break;
			case 8193:
				audioFormat = "DTS";
				break;
			case 22127:
				audioFormat = "VORBIS";
				break;
			case -2:
			{
				wBitsPerSample = raf.readShort();
				cbSize = raf.readShort();
				int num = raf.readShort();
				int num2 = num;
				wReserved = (short)num;
				num = num2;
				int num3 = num;
				samplesValidBitsPerSample = (short)num;
				wValidBitsPerSample = (short)num3;
				channelMask = raf.readInt();
				guid_data1 = raf.readInt();
				guid_data2 = raf.readShort();
				guid_data3 = raf.readShort();
				raf.readFully(guid_data4);
				audioFormat = "EXTENSIBLE";
				break;
			}
			default:
				audioFormat = new StringBuilder().append("Unknown : ").append(Integer.toHexString(wFormatTag)).toString();
				break;
			}
		}

		[LineNumberTable(924)]
		public virtual bool isMP3()
		{
			return mp3Flag;
		}

		[LineNumberTable(928)]
		public virtual short getCbSize()
		{
			return cbSize;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			158,
			165,
			98,
			149,
			127,
			35,
			byte.MaxValue,
			18,
			61
		})]
		public override string toString()
		{
			string result = java.lang.String.format("\tCHUNK [%s], ChunkSize [%d], Format [%s], Channels [%d], Channel Mask [%s], MP3 [%b], SamplesPerSec [%d], nBlockAlign [%d]", toFourCC(dwFourCC), Integer.valueOf(getChunkSize()), audioFormat, Short.valueOf(channels), Integer.toHexString(channelMask), java.lang.Boolean.valueOf(mp3Flag), Integer.valueOf(nSamplesPerSec), Long.valueOf(getStartOfChunk()), Short.valueOf(nBlockAlign));
			
			return result;
		}
	}

	public const int FOURCC_RIFF = 1179011410;

	public const int FOURCC_AVI = 541677121;

	public const int FOURCC_AVIX = 1481201217;

	public const int FOURCC_AVIH = 1751742049;

	public const int FOURCC_LIST = 1414744396;

	public const int FOURCC_HDRL = 1819436136;

	public const int FOURCC_JUNK = 1263424842;

	public const int FOURCC_INDX = 2019847785;

	public const int FOURCC_IDXL = 829973609;

	public const int FOURCC_STRL = 1819440243;

	public const int FOURCC_STRH = 1752331379;

	public const int FOURCC_STRF = 1718776947;

	public const int FOURCC_MOVI = 1769369453;

	public const int FOURCC_REC = 543384946;

	public const int FOURCC_SEGM = 1835492723;

	public const int FOURCC_ODML = 1819108463;

	public const int FOURCC_VIDS = 1935960438;

	public const int FOURCC_AUDS = 1935963489;

	public const int FOURCC_MIDS = 1935960429;

	public const int FOURCC_TXTS = 1937012852;

	public const int FOURCC_strd = 1685222515;

	public const int FOURCC_strn = 1852994675;

	public const int AVIF_HASINDEX = 16;

	public const int AVIF_MUSTUSEINDEX = 32;

	public const int AVIF_ISINTERLEAVED = 256;

	public const int AVIF_TRUSTCKTYPE = 2048;

	public const int AVIF_WASCAPTUREFILE = 65536;

	public const int AVIF_COPYRIGHTED = 131072;

	public const int AVIIF_LIST = 1;

	public const int AVIIF_KEYFRAME = 16;

	public const int AVIIF_FIRSTPART = 32;

	public const int AVIIF_LASTPART = 64;

	public const int AVIIF_NOTIME = 256;

	public const int AUDIO_FORMAT_PCM = 1;

	public const int AUDIO_FORMAT_MP3 = 85;

	public const int AUDIO_FORMAT_AC3 = 8192;

	public const int AUDIO_FORMAT_DTS = 8193;

	public const int AUDIO_FORMAT_VORBIS = 22127;

	public const int AUDIO_FORMAT_EXTENSIBLE = 65534;

	internal int ___003C_003EAVI_INDEX_OF_INDEXES;

	internal int ___003C_003EAVI_INDEX_OF_CHUNKS;

	internal int ___003C_003EAVI_INDEX_OF_TIMED_CHUNKS;

	internal int ___003C_003EAVI_INDEX_OF_SUB_2FIELD;

	internal int ___003C_003EAVI_INDEX_IS_DATA;

	public const int STDINDEXSIZE = 16384;

	private const long SIZE_MASK = 4294967295L;

	private DataReader raf;

	private long fileLeft;

	private AVITag_AVIH aviHeader;

	private AVITag_STRH[] streamHeaders;

	private AVIChunk[] streamFormats;

	[Signature("Ljava/util/List<Lorg/jcodec/containers/avi/AVIReader$AVITag_AviIndex;>;")]
	private List aviIndexes;

	private AVITag_AviDmlSuperIndex[] openDmlSuperIndex;

	private PrintStream ps;

	private bool skipFrames;

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public int AVI_INDEX_OF_INDEXES
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAVI_INDEX_OF_INDEXES;
		}
		[HideFromJava]
		private set
		{
			___003C_003EAVI_INDEX_OF_INDEXES = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public int AVI_INDEX_OF_CHUNKS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAVI_INDEX_OF_CHUNKS;
		}
		[HideFromJava]
		private set
		{
			___003C_003EAVI_INDEX_OF_CHUNKS = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public int AVI_INDEX_OF_TIMED_CHUNKS
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAVI_INDEX_OF_TIMED_CHUNKS;
		}
		[HideFromJava]
		private set
		{
			___003C_003EAVI_INDEX_OF_TIMED_CHUNKS = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public int AVI_INDEX_OF_SUB_2FIELD
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAVI_INDEX_OF_SUB_2FIELD;
		}
		[HideFromJava]
		private set
		{
			___003C_003EAVI_INDEX_OF_SUB_2FIELD = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public int AVI_INDEX_IS_DATA
	{
		[HideFromJava]
		get
		{
			return ___003C_003EAVI_INDEX_IS_DATA;
		}
		[HideFromJava]
		private set
		{
			___003C_003EAVI_INDEX_IS_DATA = value;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 105, 66, 103, 103, 105, 111, 230, 61, 231,
		69
	})]
	public static string toFourCC(int fourcc)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < 4; i++)
		{
			int c = fourcc & 0xFF;
			sb.append(Character.toString((char)c));
			fourcc >>= 8;
		}
		string result = sb.toString();
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 112, 162, 233, 37, 104, 104, 104, 104, 236,
		72, 104, 233, 73, 104, 232, 69, 114, 140
	})]
	public AVIReader(SeekableByteChannel src)
	{
		___003C_003EAVI_INDEX_OF_INDEXES = 0;
		___003C_003EAVI_INDEX_OF_CHUNKS = 1;
		___003C_003EAVI_INDEX_OF_TIMED_CHUNKS = 2;
		___003C_003EAVI_INDEX_OF_SUB_2FIELD = 3;
		___003C_003EAVI_INDEX_IS_DATA = 128;
		raf = null;
		fileLeft = 0L;
		ps = null;
		skipFrames = true;
		raf = DataReader.createDataReader(src, ByteOrder.LITTLE_ENDIAN);
		aviIndexes = new ArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 110, 130, 104, 102, 159, 8, 101, 107, 107,
		139
	})]
	public static int fromFourCC(string str)
	{
		byte[] strBytes = java.lang.String.instancehelper_getBytes(str);
		if ((nint)strBytes.LongLength != 4)
		{
			string s = new StringBuilder().append("Expected 4 bytes not ").append(strBytes.Length).toString();
			
			throw new IllegalArgumentException(s);
		}
		int fourCCInt = strBytes[3];
		fourCCInt = (fourCCInt <<= 8) | strBytes[2];
		fourCCInt = (fourCCInt <<= 8) | strBytes[1];
		return (fourCCInt <<= 8) | strBytes[0];
	}

	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(158)]
	public virtual long getFileLeft()
	{
		return fileLeft;
	}

	[Signature("()Ljava/util/List<Lorg/jcodec/containers/avi/AVIReader$AVITag_AviIndex;>;")]
	[LineNumberTable(162)]
	public virtual List getAviIndexes()
	{
		return aviIndexes;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159,
		101,
		162,
		135,
		109,
		136,
		99,
		99,
		196,
		110,
		106,
		145,
		104,
		117,
		146,
		132,
		110,
		138,
		159,
		104,
		104,
		245,
		70,
		198,
		104,
		149,
		156,
		248,
		70,
		104,
		117,
		198,
		118,
		149,
		141,
		109,
		109,
		109,
		198,
		101,
		223,
		17,
		133,
		126,
		117,
		148,
		230,
		70,
		159,
		0,
		126,
		117,
		198,
		126,
		117,
		198,
		byte.MaxValue,
		23,
		71,
		104,
		149,
		115,
		230,
		69,
		104,
		149,
		121,
		230,
		70,
		110,
		150,
		107,
		230,
		71,
		143,
		111,
		149,
		105,
		152,
		116,
		170,
		102,
		146,
		111,
		149,
		116,
		135,
		106,
		145,
		105,
		149,
		116,
		234,
		70,
		148,
		104,
		117,
		120,
		143,
		104,
		112,
		112,
		143,
		104,
		247,
		77,
		104,
		112,
		238,
		71,
		141,
		148,
		142,
		136,
		127,
		16,
		191,
		18,
		105,
		47,
		105,
		174
	})]
	public virtual void parse()
	{
		try
		{
			long t1 = java.lang.System.currentTimeMillis();
			long fileSize = (fileLeft = raf.size());
			int numStreams = 0;
			int streamIndex = -1;
			int videoFrameNo = 1;
			int dwFourCC = raf.readInt();
			if (dwFourCC != 1179011410)
			{
				
				throw new org.jcodec.api.FormatException("No RIFF header found");
			}
			AVIChunk aviItem = new AVIList();
			((AVIList)aviItem).read(dwFourCC, raf);
			Logger.debug(((AVIList)aviItem).toString());
			int previousStreamType = 0;
			do
			{
				dwFourCC = raf.readInt();
				string dwFourCCStr = toFourCC(dwFourCC);
				switch (dwFourCC)
				{
				case 1179011410:
					aviItem = new AVIList();
					((AVIList)aviItem).read(dwFourCC, raf);
					break;
				case 1414744396:
					aviItem = new AVIList();
					((AVIList)aviItem).read(dwFourCC, raf);
					if (((AVIList)aviItem).getListType() == 1769369453)
					{
						((AVIList)aviItem).skip(raf);
					}
					break;
				case 1819440243:
					aviItem = new AVIList();
					((AVIList)aviItem).read(dwFourCC, raf);
					break;
				case 1751742049:
				{
					AVITag_AVIH aVITag_AVIH = new AVITag_AVIH();
					aviHeader = aVITag_AVIH;
					aviItem = aVITag_AVIH;
					((AVITag_AVIH)aviItem).read(dwFourCC, raf);
					numStreams = aviHeader.getStreams();
					streamHeaders = new AVITag_STRH[numStreams];
					streamFormats = new AVIChunk[numStreams];
					openDmlSuperIndex = new AVITag_AviDmlSuperIndex[numStreams];
					break;
				}
				case 1752331379:
				{
					if (streamIndex >= numStreams)
					{
						string s = new StringBuilder().append("Read more stream headers than expected, expected [").append(numStreams).append("]")
							.toString();
						
						throw new IllegalStateException(s);
					}
					streamIndex++;
					AVITag_STRH[] array = streamHeaders;
					int num = streamIndex;
					AVITag_STRH aVITag_STRH = new AVITag_STRH();
					int num2 = num;
					AVITag_STRH[] array2 = array;
					array2[num2] = aVITag_STRH;
					aviItem = aVITag_STRH;
					((AVITag_STRH)aviItem).read(dwFourCC, raf);
					previousStreamType = ((AVITag_STRH)aviItem).getType();
					break;
				}
				case 1718776947:
					switch (previousStreamType)
					{
					case 1935960438:
					{
						AVIChunk[] array5 = streamFormats;
						int num4 = streamIndex;
						AVITag_BitmapInfoHeader aVITag_BitmapInfoHeader = new AVITag_BitmapInfoHeader();
						int num2 = num4;
						AVIChunk[] array4 = array5;
						array4[num2] = aVITag_BitmapInfoHeader;
						aviItem = aVITag_BitmapInfoHeader;
						((AVITag_BitmapInfoHeader)aviItem).read(dwFourCC, raf);
						break;
					}
					case 1935963489:
					{
						AVIChunk[] array3 = streamFormats;
						int num3 = streamIndex;
						AVITag_WaveFormatEx aVITag_WaveFormatEx = new AVITag_WaveFormatEx();
						int num2 = num3;
						AVIChunk[] array4 = array3;
						array4[num2] = aVITag_WaveFormatEx;
						aviItem = aVITag_WaveFormatEx;
						((AVITag_WaveFormatEx)aviItem).read(dwFourCC, raf);
						break;
					}
					default:
					{
						string message = new StringBuilder().append("Expected vids or auds got [").append(toFourCC(previousStreamType)).append("]")
							.toString();
						
						throw new IOException(message);
					}
					}
					break;
				case 1835492723:
					aviItem = new AVI_SEGM();
					((AVI_SEGM)aviItem).read(dwFourCC, raf);
					((AVI_SEGM)aviItem).skip(raf);
					break;
				case 829973609:
					aviItem = new AVITag_AviIndex();
					((AVITag_AviIndex)aviItem).read(dwFourCC, raf);
					aviIndexes.add((AVITag_AviIndex)aviItem);
					break;
				case 2019847785:
					openDmlSuperIndex[streamIndex] = new AVITag_AviDmlSuperIndex();
					openDmlSuperIndex[streamIndex].read(dwFourCC, raf);
					aviItem = openDmlSuperIndex[streamIndex];
					break;
				default:
					if (java.lang.String.instancehelper_endsWith(dwFourCCStr, "db"))
					{
						aviItem = new AVITag_VideoChunk(compressed: false, raf);
						((AVITag_VideoChunk)aviItem).read(dwFourCC, raf);
						if (skipFrames)
						{
							((AVITag_VideoChunk)aviItem).skip(raf);
							break;
						}
						byte[] videoFrameData2 = ((AVITag_VideoChunk)aviItem).getVideoPacket();
						ByteBuffer byteBuffer = ByteBuffer.wrap(videoFrameData2);
					}
					else if (java.lang.String.instancehelper_endsWith(dwFourCCStr, "dc"))
					{
						aviItem = new AVITag_VideoChunk(compressed: true, raf);
						((AVITag_VideoChunk)aviItem).read(dwFourCC, raf);
						((AVITag_VideoChunk)aviItem).setFrameNo(videoFrameNo);
						videoFrameNo++;
						string fourccStr = toFourCC(dwFourCC);
						int streamNo = Integer.parseInt(java.lang.String.instancehelper_substring(fourccStr, 0, 2));
						if (skipFrames)
						{
							((AVITag_VideoChunk)aviItem).skip(raf);
							break;
						}
						byte[] videoFrameData = ((AVITag_VideoChunk)aviItem).getVideoPacket();
						ByteBuffer byteBuffer2 = ByteBuffer.wrap(videoFrameData);
					}
					else if (java.lang.String.instancehelper_endsWith(dwFourCCStr, "wb"))
					{
						aviItem = new AVITag_AudioChunk();
						((AVITag_AudioChunk)aviItem).read(dwFourCC, raf);
						((AVITag_AudioChunk)aviItem).skip(raf);
					}
					else if (java.lang.String.instancehelper_endsWith(dwFourCCStr, "tx"))
					{
						aviItem = new AVIChunk();
						aviItem.read(dwFourCC, raf);
						aviItem.skip(raf);
					}
					else if (java.lang.String.instancehelper_startsWith(dwFourCCStr, "ix"))
					{
						aviItem = new AVITag_AviDmlStandardIndex();
						((AVITag_AviDmlStandardIndex)aviItem).read(dwFourCC, raf);
					}
					else
					{
						aviItem = new AVIChunk();
						aviItem.read(dwFourCC, raf);
						aviItem.skip(raf);
					}
					break;
				}
				Logger.debug(aviItem.toString());
				fileLeft = fileSize - raf.position();
			}
			while (fileLeft > 0u);
			long t2 = java.lang.System.currentTimeMillis();
			Logger.debug(new StringBuilder().append("\tFile Left [").append(fileLeft).append("]")
				.toString());
			Logger.debug(new StringBuilder().append("\tParse time : ").append(t2 - t1).append("ms")
				.toString());
		}
		catch
		{
			//try-fault
			if (ps != null)
			{
				ps.close();
			}
			throw;
		}
		if (ps != null)
		{
			ps.close();
		}
	}
}
