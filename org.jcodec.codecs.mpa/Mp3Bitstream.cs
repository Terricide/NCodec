using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.mpa;

public class Mp3Bitstream : Object
{
	internal class Granule : Object
	{
		internal int part23Length;

		internal int bigValues;

		internal int globalGain;

		internal int scalefacCompress;

		internal bool windowSwitchingFlag;

		internal int blockType;

		internal bool mixedBlockFlag;

		internal int[] tableSelect;

		internal int[] subblockGain;

		internal int region0Count;

		internal int region1Count;

		internal bool preflag;

		internal int scalefacScale;

		internal int count1tableSelect;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 128, 130, 105, 109, 109 })]
		internal Granule()
		{
			tableSelect = new int[3];
			subblockGain = new int[3];
		}
	}

	internal class MP3SideInfo : Object
	{
		internal int mainDataBegin;

		internal int privateBits;

		internal bool[][] scfsi;

		internal Granule[][] granule;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 133, 66, 105, 127, 11, 127, 32 })]
		internal MP3SideInfo()
		{
			int[] array = new int[2];
			int num = (array[1] = 4);
			num = (array[0] = 2);
			scfsi = (bool[][])ByteCodeHelper.multianewarray(typeof(bool[][]).TypeHandle, array);
			granule = new Granule[2][]
			{
				new Granule[2]
				{
					new Granule(),
					new Granule()
				},
				new Granule[2]
				{
					new Granule(),
					new Granule()
				}
			};
		}
	}

	internal class ScaleFactors : Object
	{
		internal int[] large;

		internal int[][] small;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 125, 66, 105, 110, 127, 12 })]
		internal ScaleFactors()
		{
			large = new int[23];
			int[] array = new int[2];
			int num = (array[1] = 13);
			num = (array[0] = 3);
			small = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 93, 162, 103, 103, 61, 135, 103, 103, 63,
		0, 39, 167, 106, 105, 63, 2, 41, 169, 109,
		46, 137
	})]
	private static ScaleFactors readScaleFacMixed(BitReader br, Granule granule)
	{
		ScaleFactors sf = new ScaleFactors();
		for (int sfb4 = 0; sfb4 < 8; sfb4++)
		{
			sf.large[sfb4] = br.readNBit(MpaConst.scaleFactorLen[0][granule.scalefacCompress]);
		}
		for (int sfb3 = 3; sfb3 < 6; sfb3++)
		{
			for (int window3 = 0; window3 < 3; window3++)
			{
				sf.small[window3][sfb3] = br.readNBit(MpaConst.scaleFactorLen[0][granule.scalefacCompress]);
			}
		}
		for (int sfb2 = 6; sfb2 < 12; sfb2++)
		{
			for (int window2 = 0; window2 < 3; window2++)
			{
				sf.small[window2][sfb2] = br.readNBit(MpaConst.scaleFactorLen[1][granule.scalefacCompress]);
			}
		}
		int sfb = 12;
		for (int window = 0; window < 3; window++)
		{
			sf.small[window][sfb] = 0;
		}
		return sf;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 83, 162, 103, 112, 112, 103, 105, 51, 41,
		199, 106, 105, 52, 41, 201, 109, 109, 109
	})]
	private static ScaleFactors readScaleFacShort(BitReader br, Granule granule)
	{
		ScaleFactors sf = new ScaleFactors();
		int length0 = MpaConst.scaleFactorLen[0][granule.scalefacCompress];
		int length1 = MpaConst.scaleFactorLen[1][granule.scalefacCompress];
		for (int j = 0; j < 6; j++)
		{
			for (int l = 0; l < 3; l++)
			{
				sf.small[l][j] = br.readNBit(length0);
			}
		}
		for (int i = 6; i < 12; i++)
		{
			for (int k = 0; k < 3; k++)
			{
				sf.small[k][i] = br.readNBit(length1);
			}
		}
		sf.small[0][12] = 0;
		sf.small[1][12] = 0;
		sf.small[2][12] = 0;
		return sf;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 89, 130, 103, 112, 112, 102, 103, 48, 167,
		102, 106, 49, 169, 102, 107, 49, 169, 102, 107,
		49, 169, 107, 107
	})]
	private static ScaleFactors readScaleFacNonSwitch(BitReader br, Granule granule, bool[] b)
	{
		ScaleFactors sf = new ScaleFactors();
		int length0 = MpaConst.scaleFactorLen[0][granule.scalefacCompress];
		int length1 = MpaConst.scaleFactorLen[1][granule.scalefacCompress];
		if (b[0])
		{
			for (int l = 0; l < 6; l++)
			{
				sf.large[l] = br.readNBit(length0);
			}
		}
		if (b[1])
		{
			for (int k = 6; k < 11; k++)
			{
				sf.large[k] = br.readNBit(length0);
			}
		}
		if (b[2])
		{
			for (int j = 11; j < 16; j++)
			{
				sf.large[j] = br.readNBit(length1);
			}
		}
		if (b[3])
		{
			for (int i = 16; i < 21; i++)
			{
				sf.large[i] = br.readNBit(length1);
			}
		}
		sf.large[21] = 0;
		sf.large[22] = 0;
		return sf;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 67, 66, 105, 136, 136, 154, 125, 100, 104,
		105, 105, 114, 106, 103, 104, 105, 105, 111, 120,
		109, 101, 104, 137, 108, 109, 118, 101, 101, 104,
		169, 134, 106, 105, 116, 125, 101, 104, 105, 106,
		113, 113, 110, 101, 104, 102, 106, 110, 119, 101,
		101, 104, 196, 108, 117, 56, 47, 236, 70
	})]
	private static int[] readLSFScaleData(BitReader br, MpaHeader header, Granule granule, int ch)
	{
		int[] result = new int[54];
		int[] scaleFacLen = new int[4];
		int comp = granule.scalefacCompress;
		int blockType = ((granule.blockType == 2) ? ((!granule.mixedBlockFlag) ? 1 : 2) : 0);
		int ch2 = (((header.modeExtension == 1 || header.modeExtension == 3) && ch == 1) ? 1 : 0);
		int lenType = 0;
		if (ch2 == 0)
		{
			if (comp < 400)
			{
				scaleFacLen[0] = (int)((uint)comp >> 4) / 5;
				uint num = (uint)comp >> 4;
				scaleFacLen[1] = ((5 != -1) ? ((int)num % 5) : 0);
				scaleFacLen[2] = (int)((uint)(comp & 0xF) >> 2);
				scaleFacLen[3] = comp & 3;
				granule.preflag = false;
				lenType = 0;
			}
			else if (comp < 500)
			{
				scaleFacLen[0] = (int)((uint)(comp - 400) >> 2) / 5;
				uint num2 = (uint)(comp - 400) >> 2;
				scaleFacLen[1] = ((5 != -1) ? ((int)num2 % 5) : 0);
				scaleFacLen[2] = (comp - 400) & 3;
				scaleFacLen[3] = 0;
				granule.preflag = false;
				lenType = 1;
			}
			else if (comp < 512)
			{
				scaleFacLen[0] = (comp - 500) / 3;
				int num3 = comp - 500;
				scaleFacLen[1] = ((3 != -1) ? (num3 % 3) : 0);
				scaleFacLen[2] = 0;
				scaleFacLen[3] = 0;
				granule.preflag = true;
				lenType = 2;
			}
		}
		else
		{
			int halfComp = (int)((uint)comp >> 1);
			if (halfComp < 180)
			{
				scaleFacLen[0] = halfComp / 36;
				scaleFacLen[1] = ((36 != -1) ? (halfComp % 36) : 0) / 6;
				int num4 = ((36 != -1) ? (halfComp % 36) : 0);
				scaleFacLen[2] = ((6 != -1) ? (num4 % 6) : 0);
				scaleFacLen[3] = 0;
				granule.preflag = false;
				lenType = 3;
			}
			else if (halfComp < 244)
			{
				scaleFacLen[0] = (int)((uint)((halfComp - 180) & 0x3F) >> 4);
				scaleFacLen[1] = (int)((uint)((halfComp - 180) & 0xF) >> 2);
				scaleFacLen[2] = (halfComp - 180) & 3;
				scaleFacLen[3] = 0;
				granule.preflag = false;
				lenType = 4;
			}
			else if (halfComp < 255)
			{
				scaleFacLen[0] = (halfComp - 244) / 3;
				int num5 = halfComp - 244;
				scaleFacLen[1] = ((3 != -1) ? (num5 % 3) : 0);
				scaleFacLen[2] = 0;
				scaleFacLen[3] = 0;
				granule.preflag = false;
				lenType = 5;
			}
		}
		int i = 0;
		int k = 0;
		for (; i < 4; i++)
		{
			int j = 0;
			while (j < MpaConst.numberOfScaleFactors[lenType][blockType][i])
			{
				result[k] = ((scaleFacLen[i] != 0) ? br.readNBit(scaleFacLen[i]) : 0);
				j++;
				k++;
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 38, 130, 111, 101, 134, 106, 109, 113, 100,
		105, 100, 106, 109, 113, 100, 105, 100
	})]
	internal static int readBigVal(int tab, BitReader br)
	{
		int res = MpaConst.bigValVlc[tab].readVLC(br);
		int x = (int)((uint)res >> 4);
		int y = res & 0xF;
		if (MpaConst.bigValEscBits[tab] != 0 && MpaConst.bigValMaxval[tab] - 1 == x)
		{
			x += br.readNBit(MpaConst.bigValEscBits[tab]);
		}
		if (x != 0 && br.read1Bit() != 0)
		{
			x = -x;
		}
		if (MpaConst.bigValEscBits[tab] != 0 && MpaConst.bigValMaxval[tab] - 1 == y)
		{
			y += br.readNBit(MpaConst.bigValEscBits[tab]);
		}
		if (y != 0 && br.read1Bit() != 0)
		{
			y = -y;
		}
		int result = Vector2Int.pack16(x, y);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 33, 130, 151, 103, 103, 103, 134, 100, 105,
		100, 100, 105, 100, 100, 105, 100, 101, 105, 102
	})]
	internal static int readCount1(int tab, BitReader br)
	{
		int res = ((tab != 0) ? MpaConst.cnt1B : MpaConst.cnt1A).readVLC(br);
		int v = (res >> 3) & 1;
		int w = (res >> 2) & 1;
		int x = (res >> 1) & 1;
		int y = res & 1;
		if (v != 0 && br.read1Bit() != 0)
		{
			v = -v;
		}
		if (w != 0 && br.read1Bit() != 0)
		{
			w = -w;
		}
		if (x != 0 && br.read1Bit() != 0)
		{
			x = -x;
		}
		if (y != 0 && br.read1Bit() != 0)
		{
			y = -y;
		}
		int result = Vector4Int.pack8(v, w, x, y);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(28)]
	public Mp3Bitstream()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 123, 66, 103, 104, 141, 111, 101, 144, 142,
		106, 119, 119, 119, 247, 60, 234, 71, 106, 108,
		110, 112, 112, 111, 111, 117, 109, 111, 149, 113,
		145, 113, 113, 145, 106, 99, 116, 139, 137, 148,
		113, 113, 113, 111, 111, 137, 117, 111, 239, 28,
		44, 239, 106, 110, 101, 144, 142, 108, 110, 112,
		112, 111, 112, 149, 141, 111, 117, 113, 145, 113,
		113, 145, 106, 99, 116, 142, 105, 212, 113, 113,
		113, 111, 111, 169, 111, 239, 26, 236, 105, 103
	})]
	internal static MP3SideInfo readSideInfo(MpaHeader header, ByteBuffer src, int channels)
	{
		MP3SideInfo si = new MP3SideInfo();
		BitReader stream = BitReader.createBitReader(src);
		if (header.version == 1)
		{
			si.mainDataBegin = stream.readNBit(9);
			if (channels == 1)
			{
				si.privateBits = stream.readNBit(5);
			}
			else
			{
				si.privateBits = stream.readNBit(3);
			}
			for (int ch3 = 0; ch3 < channels; ch3++)
			{
				si.scfsi[ch3][0] = ((stream.read1Bit() == 0) ? true : false);
				si.scfsi[ch3][1] = ((stream.read1Bit() == 0) ? true : false);
				si.scfsi[ch3][2] = ((stream.read1Bit() == 0) ? true : false);
				si.scfsi[ch3][3] = ((stream.read1Bit() == 0) ? true : false);
			}
			for (int gr = 0; gr < 2; gr++)
			{
				for (int ch2 = 0; ch2 < channels; ch2++)
				{
					Granule granule2 = si.granule[ch2][gr];
					granule2.part23Length = stream.readNBit(12);
					granule2.bigValues = stream.readNBit(9);
					granule2.globalGain = stream.readNBit(8);
					granule2.scalefacCompress = stream.readNBit(4);
					granule2.windowSwitchingFlag = ((stream.readNBit(1) != 0) ? true : false);
					if (granule2.windowSwitchingFlag)
					{
						granule2.blockType = stream.readNBit(2);
						granule2.mixedBlockFlag = ((stream.readNBit(1) != 0) ? true : false);
						granule2.tableSelect[0] = stream.readNBit(5);
						granule2.tableSelect[1] = stream.readNBit(5);
						granule2.subblockGain[0] = stream.readNBit(3);
						granule2.subblockGain[1] = stream.readNBit(3);
						granule2.subblockGain[2] = stream.readNBit(3);
						if (granule2.blockType == 0)
						{
							return null;
						}
						if (granule2.blockType == 2 && !granule2.mixedBlockFlag)
						{
							granule2.region0Count = 8;
						}
						else
						{
							granule2.region0Count = 7;
						}
						granule2.region1Count = 20 - granule2.region0Count;
					}
					else
					{
						granule2.tableSelect[0] = stream.readNBit(5);
						granule2.tableSelect[1] = stream.readNBit(5);
						granule2.tableSelect[2] = stream.readNBit(5);
						granule2.region0Count = stream.readNBit(4);
						granule2.region1Count = stream.readNBit(3);
						granule2.blockType = 0;
					}
					granule2.preflag = ((stream.readNBit(1) != 0) ? true : false);
					granule2.scalefacScale = stream.readNBit(1);
					granule2.count1tableSelect = stream.readNBit(1);
				}
			}
		}
		else
		{
			si.mainDataBegin = stream.readNBit(8);
			if (channels == 1)
			{
				si.privateBits = stream.readNBit(1);
			}
			else
			{
				si.privateBits = stream.readNBit(2);
			}
			for (int ch = 0; ch < channels; ch++)
			{
				Granule granule = si.granule[ch][0];
				granule.part23Length = stream.readNBit(12);
				granule.bigValues = stream.readNBit(9);
				granule.globalGain = stream.readNBit(8);
				granule.scalefacCompress = stream.readNBit(9);
				granule.windowSwitchingFlag = ((stream.readNBit(1) != 0) ? true : false);
				if (granule.windowSwitchingFlag)
				{
					granule.blockType = stream.readNBit(2);
					granule.mixedBlockFlag = ((stream.readNBit(1) != 0) ? true : false);
					granule.tableSelect[0] = stream.readNBit(5);
					granule.tableSelect[1] = stream.readNBit(5);
					granule.subblockGain[0] = stream.readNBit(3);
					granule.subblockGain[1] = stream.readNBit(3);
					granule.subblockGain[2] = stream.readNBit(3);
					if (granule.blockType == 0)
					{
						return null;
					}
					if (granule.blockType == 2 && !granule.mixedBlockFlag)
					{
						granule.region0Count = 8;
					}
					else
					{
						granule.region0Count = 7;
						granule.region1Count = 20 - granule.region0Count;
					}
				}
				else
				{
					granule.tableSelect[0] = stream.readNBit(5);
					granule.tableSelect[1] = stream.readNBit(5);
					granule.tableSelect[2] = stream.readNBit(5);
					granule.region0Count = stream.readNBit(4);
					granule.region1Count = stream.readNBit(3);
					granule.blockType = 0;
				}
				granule.scalefacScale = stream.readNBit(1);
				granule.count1tableSelect = stream.readNBit(1);
			}
		}
		stream.terminate();
		return si;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 162, 114, 105, 139, 171 })]
	internal static ScaleFactors readScaleFactors(BitReader br, Granule granule, bool[] b)
	{
		if (granule.windowSwitchingFlag && granule.blockType == 2)
		{
			if (granule.mixedBlockFlag)
			{
				ScaleFactors result = readScaleFacMixed(br, granule);
				
				return result;
			}
			ScaleFactors result2 = readScaleFacShort(br, granule);
			
			return result2;
		}
		ScaleFactors result3 = readScaleFacNonSwitch(br, granule, b);
		
		return result3;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 78, 98, 103, 139, 99, 120, 108, 103, 108,
		5, 199, 106, 105, 112, 5, 41, 233, 70, 105,
		46, 206, 106, 105, 112, 5, 41, 233, 71, 105,
		46, 235, 69, 106, 109, 5, 201, 107, 139
	})]
	internal static ScaleFactors readLSFScaleFactors(BitReader br, MpaHeader header, Granule granule, int ch)
	{
		ScaleFactors scalefac = new ScaleFactors();
		int[] scalefacBuffer = readLSFScaleData(br, header, granule, ch);
		int i = 0;
		if (granule.windowSwitchingFlag && granule.blockType == 2)
		{
			if (granule.mixedBlockFlag)
			{
				for (int sfb4 = 0; sfb4 < 8; sfb4++)
				{
					scalefac.large[sfb4] = scalefacBuffer[i];
					i++;
				}
				for (int sfb3 = 3; sfb3 < 12; sfb3++)
				{
					for (int window4 = 0; window4 < 3; window4++)
					{
						scalefac.small[window4][sfb3] = scalefacBuffer[i];
						i++;
					}
				}
				for (int window3 = 0; window3 < 3; window3++)
				{
					scalefac.small[window3][12] = 0;
				}
			}
			else
			{
				for (int sfb2 = 0; sfb2 < 12; sfb2++)
				{
					for (int window2 = 0; window2 < 3; window2++)
					{
						scalefac.small[window2][sfb2] = scalefacBuffer[i];
						i++;
					}
				}
				for (int window = 0; window < 3; window++)
				{
					scalefac.small[window][12] = 0;
				}
			}
		}
		else
		{
			for (int sfb = 0; sfb < 21; sfb++)
			{
				scalefac.large[sfb] = scalefacBuffer[i];
				i++;
			}
			scalefac.large[21] = 0;
			scalefac.large[22] = 0;
		}
		return scalefac;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 50, 130, 106, 109, 135, 114, 191, 3, 115,
		172, 100, 115, 100, 102, 109, 102, 141, 107, 112,
		109, 143, 107, 115, 243, 50, 236, 82, 153, 143,
		115, 115, 115, 115, 134, 106, 144
	})]
	internal static int readCoeffs(BitReader br, Granule granule, int ch, int part2_start, int sfreq, int[] @out)
	{
		int part23End = part2_start + granule.part23Length;
		int region1Start = ((sfreq != 8) ? 36 : 72);
		int region2Start = 576;
		if (!granule.windowSwitchingFlag || granule.blockType != 2)
		{
			int region1StartIdx = MathUtil.clip(granule.region0Count + granule.region1Count + 2, 0, (int)((nint)MpaConst.sfbLong[sfreq].LongLength - 1));
			region1Start = MpaConst.sfbLong[sfreq][granule.region0Count + 1];
			region2Start = MpaConst.sfbLong[sfreq][region1StartIdx];
		}
		int index = 0;
		for (int i = 0; i < granule.bigValues << 1; i += 2)
		{
			int tab = 0;
			tab = ((i < region1Start) ? granule.tableSelect[0] : ((i >= region2Start) ? granule.tableSelect[2] : granule.tableSelect[1]));
			if (tab == 0 || tab == 4 || tab == 14)
			{
				int num = index;
				index++;
				@out[num] = 0;
				int num2 = index;
				index++;
				@out[num2] = 0;
			}
			else
			{
				int packed2 = readBigVal(tab, br);
				int num3 = index;
				index++;
				@out[num3] = Vector2Int.el16_0(packed2);
				int num4 = index;
				index++;
				@out[num4] = Vector2Int.el16_1(packed2);
			}
		}
		while (br.position() < part23End && index < 576)
		{
			int packed = readCount1(granule.count1tableSelect, br);
			int num5 = index;
			index++;
			@out[num5] = Vector4Int.el8_0(packed);
			int num6 = index;
			index++;
			@out[num6] = Vector4Int.el8_1(packed);
			int num7 = index;
			index++;
			@out[num7] = Vector4Int.el8_2(packed);
			int num8 = index;
			index++;
			@out[num8] = Vector4Int.el8_3(packed);
		}
		if (br.position() < part23End)
		{
			br.readNBit(part23End - br.position());
		}
		int result = MathUtil.clip(index, 0, 576);
		
		return result;
	}
}
