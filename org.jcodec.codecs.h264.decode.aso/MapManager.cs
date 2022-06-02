using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.codecs.h264.io.model;

namespace org.jcodec.codecs.h264.decode.aso;

public class MapManager : Object
{
	private SeqParameterSet sps;

	private PictureParameterSet pps;

	private MBToSliceGroupMap mbToSliceGroupMap;

	private int prevSliceGroupChangeCycle;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 138, 136, 106, 136, 105, 104, 105,
		48, 169, 107, 111, 112, 106, 153, 115, 99, 106,
		139, 177, 173
	})]
	private MBToSliceGroupMap buildMap(SeqParameterSet sps, PictureParameterSet pps)
	{
		int numGroups = pps.numSliceGroupsMinus1 + 1;
		if (numGroups > 1)
		{
			int picWidthInMbs = sps.picWidthInMbsMinus1 + 1;
			int picHeightInMbs = SeqParameterSet.getPicHeightInMbs(sps);
			int[] map;
			if (pps.sliceGroupMapType == 0)
			{
				int[] runLength = new int[numGroups];
				for (int i = 0; i < numGroups; i++)
				{
					runLength[i] = pps.runLengthMinus1[i] + 1;
				}
				map = SliceGroupMapBuilder.buildInterleavedMap(picWidthInMbs, picHeightInMbs, runLength);
			}
			else if (pps.sliceGroupMapType == 1)
			{
				map = SliceGroupMapBuilder.buildDispersedMap(picWidthInMbs, picHeightInMbs, numGroups);
			}
			else if (pps.sliceGroupMapType == 2)
			{
				map = SliceGroupMapBuilder.buildForegroundMap(picWidthInMbs, picHeightInMbs, numGroups, pps.topLeft, pps.bottomRight);
			}
			else
			{
				if (pps.sliceGroupMapType >= 3 && pps.sliceGroupMapType <= 5)
				{
					return null;
				}
				if (pps.sliceGroupMapType != 6)
				{
					
					throw new RuntimeException("Unsupported slice group map type");
				}
				map = pps.sliceGroupId;
			}
			MBToSliceGroupMap result = buildMapIndices(map, numGroups);
			
			return result;
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 98, 104, 137, 104, 63, 1, 199, 105,
		105, 47, 169, 104, 106, 103, 31, 5, 233, 69
	})]
	private MBToSliceGroupMap buildMapIndices(int[] map, int numGroups)
	{
		int[] ind = new int[numGroups];
		int[] indices = new int[(nint)map.LongLength];
		for (int k = 0; k < (nint)map.LongLength; k++)
		{
			int num = k;
			int[] array = ind;
			int num2 = map[k];
			int[] array2 = array;
			int[] array3 = array2;
			int num3 = num2;
			num2 = array2[num2];
			int num4 = num3;
			array2 = array3;
			int num5 = num2;
			array2[num4] = num2 + 1;
			indices[num] = num5;
		}
		int[][] inverse = new int[numGroups][];
		for (int j = 0; j < numGroups; j++)
		{
			inverse[j] = new int[ind[j]];
		}
		ind = new int[numGroups];
		for (int i = 0; i < (nint)map.LongLength; i++)
		{
			int sliceGroup = map[i];
			int[] obj = inverse[sliceGroup];
			int[] array4 = ind;
			int num2 = sliceGroup;
			int[] array2 = array4;
			int[] array5 = array2;
			int num6 = num2;
			num2 = array2[num2];
			int num4 = num6;
			array2 = array5;
			int num7 = num2;
			array2[num4] = num2 + 1;
			obj[num7] = i;
		}
		MBToSliceGroupMap result = new MBToSliceGroupMap(map, indices, inverse);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 122, 130, 109, 143, 191, 16, 141, 111, 109,
		102, 119, 175, 217, 101, 153, 101, 185, 215, 144
	})]
	private void updateMap(SliceHeader sh)
	{
		int mapType = pps.sliceGroupMapType;
		int numGroups = pps.numSliceGroupsMinus1 + 1;
		if (numGroups > 1 && mapType >= 3 && mapType <= 5 && (sh.sliceGroupChangeCycle != prevSliceGroupChangeCycle || mbToSliceGroupMap == null))
		{
			prevSliceGroupChangeCycle = sh.sliceGroupChangeCycle;
			int picWidthInMbs = sps.picWidthInMbsMinus1 + 1;
			int picHeightInMbs = SeqParameterSet.getPicHeightInMbs(sps);
			int picSizeInMapUnits = picWidthInMbs * picHeightInMbs;
			int mapUnitsInSliceGroup0 = sh.sliceGroupChangeCycle * (pps.sliceGroupChangeRateMinus1 + 1);
			mapUnitsInSliceGroup0 = ((mapUnitsInSliceGroup0 <= picSizeInMapUnits) ? mapUnitsInSliceGroup0 : picSizeInMapUnits);
			int sizeOfUpperLeftGroup = ((!pps.sliceGroupChangeDirectionFlag) ? mapUnitsInSliceGroup0 : (picSizeInMapUnits - mapUnitsInSliceGroup0));
			mbToSliceGroupMap = buildMapIndices(mapType switch
			{
				3 => SliceGroupMapBuilder.buildBoxOutMap(picWidthInMbs, picHeightInMbs, pps.sliceGroupChangeDirectionFlag, mapUnitsInSliceGroup0), 
				4 => SliceGroupMapBuilder.buildRasterScanMap(picWidthInMbs, picHeightInMbs, sizeOfUpperLeftGroup, pps.sliceGroupChangeDirectionFlag), 
				_ => SliceGroupMapBuilder.buildWipeMap(picWidthInMbs, picHeightInMbs, sizeOfUpperLeftGroup, pps.sliceGroupChangeDirectionFlag), 
			}, numGroups);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 98, 105, 104, 104, 111 })]
	public MapManager(SeqParameterSet sps, PictureParameterSet pps)
	{
		this.sps = sps;
		this.pps = pps;
		mbToSliceGroupMap = buildMap(sps, pps);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 113, 98, 104, 104, 143, 157 })]
	public virtual Mapper getMapper(SliceHeader sh)
	{
		updateMap(sh);
		int firstMBInSlice = sh.firstMbInSlice;
		if (pps.numSliceGroupsMinus1 > 0)
		{
			PrebuiltMBlockMapper result = new PrebuiltMBlockMapper(mbToSliceGroupMap, firstMBInSlice, sps.picWidthInMbsMinus1 + 1);
			
			return result;
		}
		FlatMBlockMapper result2 = new FlatMBlockMapper(sps.picWidthInMbsMinus1 + 1, firstMBInSlice);
		
		return result2;
	}
}
