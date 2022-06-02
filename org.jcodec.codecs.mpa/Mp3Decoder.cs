using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpa;

public class Mp3Decoder : Object, AudioDecoder
{
	[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	private static bool[] ALL_TRUE;

	private const int SAMPLES_PER_BAND = 18;

	private const int NUM_BANDS = 32;

	private ChannelSynthesizer[] filter;

	private bool initialized;

	private const double fourByThree = 1.3333333333333333;

	private float[][] prevBlk;

	private ByteBuffer frameData;

	private int channels;

	private int sfreq;

	private float[] samples;

	private float[] mdctIn;

	private float[] mdctOut;

	private float[][] dequant;

	private short[][] tmpOut;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 101, 98, 102, 103, 49, 167, 102, 104, 49,
		167, 102, 105, 49, 167, 102, 105, 49, 167
	})]
	private void mergeScaleFac(Mp3Bitstream.ScaleFactors sf, Mp3Bitstream.ScaleFactors old, bool[] scfsi)
	{
		if (!scfsi[0])
		{
			for (int l = 0; l < 6; l++)
			{
				sf.large[l] = old.large[l];
			}
		}
		if (!scfsi[1])
		{
			for (int k = 6; k < 11; k++)
			{
				sf.large[k] = old.large[k];
			}
		}
		if (!scfsi[2])
		{
			for (int j = 11; j < 16; j++)
			{
				sf.large[j] = old.large[j];
			}
		}
		if (!scfsi[3])
		{
			for (int i = 16; i < 21; i++)
			{
				sf.large[i] = old.large[i];
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 66, 159, 13, 114, 105, 145, 177, 145 })]
	private void dequantizeCoeffs(int[] input, int nonzero, Mp3Bitstream.Granule granule, Mp3Bitstream.ScaleFactors scalefac, float[] @out)
	{
		float globalGain = (float)Math.pow(2.0, 0.25 * ((double)granule.globalGain - 210.0));
		if (granule.windowSwitchingFlag && granule.blockType == 2)
		{
			if (granule.mixedBlockFlag)
			{
				dequantMixed(input, nonzero, granule, scalefac, globalGain, @out);
			}
			else
			{
				dequantShort(input, nonzero, granule, scalefac, globalGain, @out);
			}
		}
		else
		{
			dequantLong(input, nonzero, granule, scalefac, globalGain, @out);
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 77, 66, 107, 104, 104, 114, 242, 60, 231,
		70
	})]
	private void decodeMsStereo(MpaHeader header, Mp3Bitstream.Granule granule, Mp3Bitstream.ScaleFactors[] scalefac, float[][] ro)
	{
		for (int i = 0; i < 576; i++)
		{
			float a = ro[0][i];
			float b = ro[1][i];
			ro[0][i] = (a + b) * 0.707106769f;
			ro[1][i] = (a - b) * 0.707106769f;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 75, 98, 122, 130, 159, 1, 108, 106, 105,
		105, 103, 103, 125, 253, 58, 42, 239, 74
	})]
	private void antialias(Mp3Bitstream.Granule granule, float[] @out)
	{
		if (granule.windowSwitchingFlag && granule.blockType == 2 && !granule.mixedBlockFlag)
		{
			return;
		}
		int bands = ((granule.windowSwitchingFlag && granule.mixedBlockFlag && granule.blockType == 2) ? 1 : 31);
		int band = 0;
		int bandStart = 0;
		while (band < bands)
		{
			for (int sample = 0; sample < 8; sample++)
			{
				int src_idx1 = bandStart + 17 - sample;
				int src_idx2 = bandStart + 18 + sample;
				float bu = @out[src_idx1];
				float bd = @out[src_idx2];
				@out[src_idx1] = bu * MpaConst.cs[sample] - bd * MpaConst.ca[sample];
				@out[src_idx2] = bd * MpaConst.cs[sample] + bu * MpaConst.ca[sample];
			}
			band++;
			bandStart += 18;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 71, 162, 110, 191, 1, 104, 46, 167, 101,
		148, 114, 104, 63, 2, 199, 106, 127, 0, 26,
		233, 49, 235, 84
	})]
	private void mdctDecode(int ch, Mp3Bitstream.Granule granule, float[] @out)
	{
		for (int sb18 = 0; sb18 < 576; sb18 += 18)
		{
			int blockType = ((!granule.windowSwitchingFlag || !granule.mixedBlockFlag || sb18 >= 36) ? granule.blockType : 0);
			for (int cc = 0; cc < 18; cc++)
			{
				mdctIn[cc] = @out[cc + sb18];
			}
			if (blockType == 2)
			{
				Mp3Mdct.threeShort(mdctIn, mdctOut);
			}
			else
			{
				Mp3Mdct.oneLong(mdctIn, mdctOut);
				for (int j = 0; j < 36; j++)
				{
					float[] array = mdctOut;
					int num = j;
					float[] array2 = array;
					array2[num] *= MpaConst.win[blockType][j];
				}
			}
			for (int i = 0; i < 18; i++)
			{
				@out[i + sb18] = mdctOut[i] + prevBlk[ch][sb18 + i];
				prevBlk[ch][sb18 + i] = mdctOut[18 + i];
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 103, 130, 103, 107, 11, 199 })]
	public static void appendSamplesInterleave(ByteBuffer buf, short[] f0, short[] f1, int n)
	{
		for (int i = 0; i < n; i++)
		{
			buf.putShort(f0[i]);
			buf.putShort(f1[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 104, 66, 103, 43, 167 })]
	public static void appendSamples(ByteBuffer buf, short[] f, int n)
	{
		for (int i = 0; i < n; i++)
		{
			buf.putShort(f[i]);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		93,
		162,
		99,
		113,
		120,
		127,
		9,
		28,
		42,
		234,
		70,
		114,
		127,
		3,
		100,
		108,
		113,
		159,
		6,
		byte.MaxValue,
		7,
		61,
		48,
		236,
		61,
		234,
		75
	})]
	private void dequantMixed(int[] input, int nonzero, Mp3Bitstream.Granule granule, Mp3Bitstream.ScaleFactors scalefac, float globalGain, float[] @out)
	{
		int i = 0;
		for (int sfb2 = 0; sfb2 < 8; sfb2++)
		{
			if (i >= nonzero)
			{
				break;
			}
			for (; i < MpaConst.sfbLong[sfreq][sfb2 + 1] && i < nonzero; i++)
			{
				int idx2 = scalefac.large[sfb2] + (granule.preflag ? MpaConst.pretab[sfb2] : 0) << granule.scalefacScale;
				@out[i] = globalGain * pow43(input[i]) * MpaConst.quantizerTab[idx2];
			}
		}
		for (int sfb = 3; sfb < 12; sfb++)
		{
			if (i >= nonzero)
			{
				break;
			}
			int sfbSz = MpaConst.sfbShort[sfreq][sfb + 1] - MpaConst.sfbShort[sfreq][sfb];
			int sfbStart = i;
			for (int wnd = 0; wnd < 3; wnd++)
			{
				int j = 0;
				while (j < sfbSz && i < nonzero)
				{
					int idx = (scalefac.small[wnd][sfb] << granule.scalefacScale) + (granule.subblockGain[wnd] << 2);
					@out[sfbStart + j * 3 + wnd] = globalGain * pow43(input[i]) * MpaConst.quantizerTab[idx];
					j++;
					i++;
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		87,
		98,
		108,
		127,
		2,
		99,
		108,
		112,
		159,
		6,
		byte.MaxValue,
		6,
		61,
		48,
		236,
		61,
		234,
		75
	})]
	private void dequantShort(int[] input, int nonzero, Mp3Bitstream.Granule granule, Mp3Bitstream.ScaleFactors scalefac, float globalGain, float[] @out)
	{
		int sfb = 0;
		int i = 0;
		while (i < nonzero)
		{
			int sfbSz = MpaConst.sfbShort[sfreq][sfb + 1] - MpaConst.sfbShort[sfreq][sfb];
			int sfbStart = i;
			for (int wnd = 0; wnd < 3; wnd++)
			{
				int j = 0;
				while (j < sfbSz && i < nonzero)
				{
					int idx = (scalefac.small[wnd][sfb] << granule.scalefacScale) + (granule.subblockGain[wnd] << 2);
					@out[sfbStart + j * 3 + wnd] = globalGain * pow43(input[i]) * MpaConst.quantizerTab[idx];
					j++;
					i++;
				}
			}
			sfb++;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 66, 108, 116, 133, 127, 9, 252, 59,
		234, 71
	})]
	private void dequantLong(int[] input, int nonzero, Mp3Bitstream.Granule granule, Mp3Bitstream.ScaleFactors scalefac, float globalGain, float[] @out)
	{
		int i = 0;
		int sfb = 0;
		for (; i < nonzero; i++)
		{
			if (i == MpaConst.sfbLong[sfreq][sfb + 1])
			{
				sfb++;
			}
			int idx = scalefac.large[sfb] + (granule.preflag ? MpaConst.pretab[sfb] : 0) << granule.scalefacScale;
			@out[i] = globalGain * pow43(input[i]) * MpaConst.quantizerTab[idx];
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 81, 130, 100, 135, 106, 136, 106, 141 })]
	private float pow43(int val)
	{
		if (val == 0)
		{
			return 0f;
		}
		int sign = (int)(1 - ((uint)val >> 31 << 1));
		int abs = MathUtil.abs(val);
		if (abs < (nint)MpaConst.power43Tab.LongLength)
		{
			return (float)sign * MpaConst.power43Tab[abs];
		}
		return (float)sign * (float)Math.pow(abs, 1.3333333333333333);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 130, 135, 148, 112, 106, 144, 159, 15,
		159, 8, 103, 51, 167, 104
	})]
	private void init(MpaHeader header)
	{
		float scalefactor = 32700f;
		channels = ((header.mode == 3) ? 1 : 2);
		filter[0] = new ChannelSynthesizer(0, scalefactor);
		if (channels == 2)
		{
			filter[1] = new ChannelSynthesizer(1, scalefactor);
		}
		int[] array = new int[2];
		int num = (array[1] = 576);
		num = (array[0] = 2);
		prevBlk = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		sfreq = header.sampleFreq + ((header.version == 1) ? 3 : ((header.version == 2) ? 6 : 0));
		for (int ch = 0; ch < 2; ch++)
		{
			Arrays.fill(prevBlk[ch], 0f);
		}
		initialized = true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		118,
		130,
		115,
		147,
		111,
		137,
		109,
		106,
		102,
		118,
		121,
		110,
		99,
		175,
		109,
		117,
		248,
		49,
		234,
		82,
		122,
		110,
		155,
		113,
		108,
		143,
		139,
		141,
		110,
		106,
		50,
		41,
		234,
		69,
		112,
		112,
		50,
		176,
		byte.MaxValue,
		1,
		60,
		243,
		51,
		236,
		85,
		106,
		158,
		150
	})]
	private void decodeGranule(MpaHeader header, ByteBuffer output, Mp3Bitstream.MP3SideInfo si, BitReader br, Mp3Bitstream.ScaleFactors[] scalefac, int grInd)
	{
		Arrays.fill(dequant[0], 0f);
		Arrays.fill(dequant[1], 0f);
		for (int ch2 = 0; ch2 < channels; ch2++)
		{
			int part2Start = br.position();
			Mp3Bitstream.Granule granule2 = si.granule[ch2][grInd];
			if (header.version == 1)
			{
				Mp3Bitstream.ScaleFactors old = scalefac[ch2];
				bool[] scfi = ((grInd != 0) ? si.scfsi[ch2] : ALL_TRUE);
				scalefac[ch2] = Mp3Bitstream.readScaleFactors(br, si.granule[ch2][grInd], scfi);
				mergeScaleFac(scalefac[ch2], old, scfi);
			}
			else
			{
				scalefac[ch2] = Mp3Bitstream.readLSFScaleFactors(br, header, granule2, ch2);
			}
			int[] coeffs = new int[580];
			int nonzero = Mp3Bitstream.readCoeffs(br, granule2, ch2, part2Start, sfreq, coeffs);
			dequantizeCoeffs(coeffs, nonzero, granule2, scalefac[ch2], dequant[ch2]);
		}
		if (((header.mode == 1 && ((uint)header.modeExtension & 2u) != 0) ? true : false) && channels == 2)
		{
			decodeMsStereo(header, si.granule[0][grInd], scalefac, dequant);
		}
		for (int ch = 0; ch < channels; ch++)
		{
			float[] @out = dequant[ch];
			Mp3Bitstream.Granule granule = si.granule[ch][grInd];
			antialias(granule, @out);
			mdctDecode(ch, granule, @out);
			for (int sb3 = 18; sb3 < 576; sb3 += 36)
			{
				for (int ss2 = 1; ss2 < 18; ss2 += 2)
				{
					@out[sb3 + ss2] = 0f - @out[sb3 + ss2];
				}
			}
			int ss = 0;
			int off = 0;
			while (ss < 18)
			{
				int sb2 = 0;
				int sb = 0;
				while (sb2 < 576)
				{
					samples[sb] = @out[sb2 + ss];
					sb2 += 18;
					sb++;
				}
				filter[ch].synthesize(samples, tmpOut[ch], off);
				ss++;
				off += 32;
			}
		}
		if (channels == 2)
		{
			appendSamplesInterleave(output, tmpOut[0], tmpOut[1], 576);
		}
		else
		{
			appendSamples(output, tmpOut[0], 576);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 126, 162, 105, 117, 113, 110, 110, 110, 127,
		15, 127, 15
	})]
	public Mp3Decoder()
	{
		filter = new ChannelSynthesizer[2] { null, null };
		frameData = ByteBuffer.allocate(4096);
		samples = new float[32];
		mdctIn = new float[18];
		mdctOut = new float[36];
		int[] array = new int[2];
		int num = (array[1] = 576);
		num = (array[0] = 2);
		dequant = (float[][])ByteCodeHelper.multianewarray(typeof(float[][]).TypeHandle, array);
		array = new int[2];
		num = (array[1] = 576);
		num = (array[0] = 2);
		tmpOut = (short[][])ByteCodeHelper.multianewarray(typeof(short[][]).TypeHandle, array);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 65, 162, 104, 105, 136, 121, 100, 145, 109,
		111, 109, 121, 109, 105, 136, 117, 142, 105, 111,
		106, 143, 136, 108, 136
	})]
	public virtual AudioBuffer decodeFrame(ByteBuffer frame, ByteBuffer dst)
	{
		MpaHeader header = MpaHeader.read_header(frame);
		if (!initialized)
		{
			init(header);
		}
		if ((header.mode == 1 && ((uint)header.modeExtension & (true ? 1u : 0u)) != 0) ? true : false)
		{
			
			throw new RuntimeException("Intensity stereo is not supported.");
		}
		dst.order(ByteOrder.LITTLE_ENDIAN);
		Mp3Bitstream.MP3SideInfo si = Mp3Bitstream.readSideInfo(header, frame, channels);
		int reserve = frameData.position();
		frameData.put(NIOUtils.read(frame, header.frameBytes));
		frameData.flip();
		if (header.protectionBit == 0)
		{
			frame.getShort();
		}
		NIOUtils.skip(frameData, reserve - si.mainDataBegin);
		BitReader br = BitReader.createBitReader(frameData);
		Mp3Bitstream.ScaleFactors[] scalefac = new Mp3Bitstream.ScaleFactors[2];
		decodeGranule(header, dst, si, br, scalefac, 0);
		if (header.version == 1)
		{
			decodeGranule(header, dst, si, br, scalefac, 1);
		}
		br.terminate();
		NIOUtils.relocateLeftover(frameData);
		dst.flip();
		AudioBuffer result = new AudioBuffer(dst, null, 1);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 56, 98, 109, 159, 17 })]
	public virtual AudioCodecMeta getCodecMeta(ByteBuffer data)
	{
		MpaHeader header = MpaHeader.read_header(data.duplicate());
		AudioFormat.___003Cclinit_003E();
		AudioFormat format = new AudioFormat(MpaConst.frequencies[header.version][header.sampleFreq], 16, (header.mode == 3) ? 1 : 2, signed: true, bigEndian: false);
		AudioCodecMeta result = AudioCodecMeta.fromAudioFormat(format);
		
		return result;
	}

	[LineNumberTable(45)]
	static Mp3Decoder()
	{
		ALL_TRUE = new bool[4] { true, true, true, true };
	}
}
