using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.h264.decode.aso;

public class SliceGroupMapBuilder : Object
{
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 100, 101, 136, 131, 109, 115, 41,
		41, 240, 69, 136
	})]
	public static int[] buildInterleavedMap(int picWidthInMbs, int picHeightInMbs, int[] runLength)
	{
		int numSliceGroups = runLength.Length;
		int picSizeInMbs = picWidthInMbs * picHeightInMbs;
		int[] groups = new int[picSizeInMbs];
		int i = 0;
		do
		{
			int iGroup = 0;
			while (iGroup < numSliceGroups && i < picSizeInMbs)
			{
				for (int j = 0; j < runLength[iGroup] && i + j < picSizeInMbs; j++)
				{
					groups[i + j] = iGroup;
				}
				int num = i;
				int num2 = iGroup;
				iGroup++;
				i = num + runLength[num2];
			}
		}
		while (i < picSizeInMbs);
		return groups;
	}

	[LineNumberTable(new byte[]
	{
		159, 128, 98, 101, 136, 103, 127, 10, 5, 231,
		69
	})]
	public static int[] buildDispersedMap(int picWidthInMbs, int picHeightInMbs, int numSliceGroups)
	{
		int picSizeInMbs = picWidthInMbs * picHeightInMbs;
		int[] groups = new int[picSizeInMbs];
		for (int i = 0; i < picSizeInMbs; i++)
		{
			int num = i;
			int num2 = ((picWidthInMbs != -1) ? (num % picWidthInMbs) : 0);
			int num3 = i;
			int num4 = num2 + ((picWidthInMbs != -1) ? (num3 / picWidthInMbs) : (-num3)) * numSliceGroups / 2;
			int group = (groups[i] = ((numSliceGroups != -1) ? (num4 % numSliceGroups) : 0));
		}
		return groups;
	}

	[LineNumberTable(new byte[]
	{
		159, 120, 130, 101, 136, 103, 39, 167, 99, 142,
		113, 114, 114, 147, 114, 134, 100, 107, 107, 106,
		7, 41, 233, 53, 236, 82
	})]
	public static int[] buildForegroundMap(int picWidthInMbs, int picHeightInMbs, int numSliceGroups, int[] topLeftAddr, int[] bottomRightAddr)
	{
		int picSizeInMbs = picWidthInMbs * picHeightInMbs;
		int[] groups = new int[picSizeInMbs];
		for (int i = 0; i < picSizeInMbs; i++)
		{
			groups[i] = numSliceGroups - 1;
		}
		int tot = 0;
		for (int iGroup = numSliceGroups - 2; iGroup >= 0; iGroup += -1)
		{
			int num = topLeftAddr[iGroup];
			int yTopLeft = ((picWidthInMbs != -1) ? (num / picWidthInMbs) : (-num));
			int num2 = topLeftAddr[iGroup];
			int xTopLeft = ((picWidthInMbs != -1) ? (num2 % picWidthInMbs) : 0);
			int num3 = bottomRightAddr[iGroup];
			int yBottomRight = ((picWidthInMbs != -1) ? (num3 / picWidthInMbs) : (-num3));
			int num4 = bottomRightAddr[iGroup];
			int xBottomRight = ((picWidthInMbs != -1) ? (num4 % picWidthInMbs) : 0);
			int sz = (yBottomRight - yTopLeft + 1) * (xBottomRight - xTopLeft + 1);
			tot += sz;
			int ind = 0;
			for (int y = yTopLeft; y <= yBottomRight; y++)
			{
				for (int x = xTopLeft; x <= xBottomRight; x++)
				{
					int mbAddr = y * picWidthInMbs + x;
					groups[mbAddr] = iGroup;
				}
			}
		}
		return groups;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 109, 97, 67, 101, 104, 137, 105, 38, 169,
		104, 104, 101, 101, 101, 101, 102, 132, 100, 108,
		106, 106, 101, 134, 108, 109, 101, 100, 109, 108,
		111, 101, 100, 109, 108, 109, 101, 104, 102, 108,
		111, 101, 104, 134, 104, 232, 36, 243, 96
	})]
	public static int[] buildBoxOutMap(int picWidthInMbs, int picHeightInMbs, bool changeDirection, int numberOfMbsInBox)
	{
		int picSizeInMbs = picWidthInMbs * picHeightInMbs;
		int[] groups = new int[picSizeInMbs];
		int changeDirectionInt = (changeDirection ? 1 : 0);
		for (int i = 0; i < picSizeInMbs; i++)
		{
			groups[i] = 1;
		}
		int x = (picWidthInMbs - changeDirectionInt) / 2;
		int y = (picHeightInMbs - changeDirectionInt) / 2;
		int leftBound = x;
		int topBound = y;
		int rightBound = x;
		int bottomBound = y;
		int xDir = changeDirectionInt - 1;
		int yDir = changeDirectionInt;
		int mapUnitVacant = 0;
		for (int j = 0; j < numberOfMbsInBox; j += ((mapUnitVacant != 0) ? 1 : 0))
		{
			int mbAddr = y * picWidthInMbs + x;
			mapUnitVacant = ((groups[mbAddr] == 1) ? 1 : 0);
			if (mapUnitVacant != 0)
			{
				groups[mbAddr] = 0;
			}
			if (xDir == -1 && x == leftBound)
			{
				leftBound = Max(leftBound - 1, 0);
				x = leftBound;
				xDir = 0;
				yDir = 2 * changeDirectionInt - 1;
			}
			else if (xDir == 1 && x == rightBound)
			{
				rightBound = Min(rightBound + 1, picWidthInMbs - 1);
				x = rightBound;
				xDir = 0;
				yDir = 1 - 2 * changeDirectionInt;
			}
			else if (yDir == -1 && y == topBound)
			{
				topBound = Max(topBound - 1, 0);
				y = topBound;
				xDir = 1 - 2 * changeDirectionInt;
				yDir = 0;
			}
			else if (yDir == 1 && y == bottomBound)
			{
				bottomBound = Min(bottomBound + 1, picHeightInMbs - 1);
				y = bottomBound;
				xDir = 2 * changeDirectionInt - 1;
				yDir = 0;
			}
			else
			{
				x += xDir;
				y += yDir;
			}
		}
		return groups;
	}

	[LineNumberTable(new byte[]
	{
		159, 91, 129, 67, 101, 104, 169, 105, 38, 201,
		102, 40, 201
	})]
	public static int[] buildRasterScanMap(int picWidthInMbs, int picHeightInMbs, int sizeOfUpperLeftGroup, bool changeDirection)
	{
		int picSizeInMbs = picWidthInMbs * picHeightInMbs;
		int[] groups = new int[picSizeInMbs];
		int changeDirectionInt = (changeDirection ? 1 : 0);
		int i;
		for (i = 0; i < sizeOfUpperLeftGroup; i++)
		{
			groups[i] = changeDirectionInt;
		}
		for (; i < picSizeInMbs; i++)
		{
			groups[i] = 1 - changeDirectionInt;
		}
		return groups;
	}

	[LineNumberTable(new byte[]
	{
		159, 84, 129, 67, 101, 104, 137, 100, 105, 105,
		106, 108, 136, 232, 59, 41, 233, 75
	})]
	public static int[] buildWipeMap(int picWidthInMbs, int picHeightInMbs, int sizeOfUpperLeftGroup, bool changeDirection)
	{
		int picSizeInMbs = picWidthInMbs * picHeightInMbs;
		int[] groups = new int[picSizeInMbs];
		int changeDirectionInt = (changeDirection ? 1 : 0);
		int k = 0;
		for (int j = 0; j < picWidthInMbs; j++)
		{
			for (int i = 0; i < picHeightInMbs; i++)
			{
				int mbAddr = i * picWidthInMbs + j;
				int num = k;
				k++;
				if (num < sizeOfUpperLeftGroup)
				{
					groups[mbAddr] = changeDirectionInt;
				}
				else
				{
					groups[mbAddr] = 1 - changeDirectionInt;
				}
			}
		}
		return groups;
	}

	[LineNumberTable(190)]
	private static int Max(int i, int j)
	{
		return (i <= j) ? j : i;
	}

	[LineNumberTable(186)]
	private static int Min(int i, int j)
	{
		return (i >= j) ? j : i;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(13)]
	public SliceGroupMapBuilder()
	{
	}
}
