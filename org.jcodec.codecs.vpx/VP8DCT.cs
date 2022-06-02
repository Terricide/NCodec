using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;

namespace org.jcodec.codecs.vpx;

public class VP8DCT : Object
{
	private const int cospi8sqrt2minus1 = 20091;

	private const int sinpi8sqrt2 = 35468;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public VP8DCT()
	{
	}

	[LineNumberTable(new byte[]
	{
		159, 136, 130, 131, 169, 106, 109, 142, 113, 185,
		136, 151, 114, 136, 106, 107, 107, 139, 229, 44,
		234, 87, 99, 106, 114, 146, 115, 155, 136, 155,
		115, 136, 113, 113, 113, 145, 229, 45, 234, 86
	})]
	public static int[] decodeDCT(int[] input)
	{
		int offset = 0;
		int[] output = new int[16];
		for (int i = 0; i < 4; i++)
		{
			int a2 = input[offset + 0] + input[offset + 8];
			int b2 = input[offset + 0] - input[offset + 8];
			int temp2 = input[offset + 4] * 35468 >> 16;
			int temp4 = input[offset + 12] + (input[offset + 12] * 20091 >> 16);
			int c2 = temp2 - temp4;
			temp2 = input[offset + 4] + (input[offset + 4] * 20091 >> 16);
			temp4 = input[offset + 12] * 35468 >> 16;
			int d2 = temp2 + temp4;
			output[offset + 0] = a2 + d2;
			output[offset + 12] = a2 - d2;
			output[offset + 4] = b2 + c2;
			output[offset + 8] = b2 - c2;
			offset++;
		}
		offset = 0;
		for (int i = 0; i < 4; i++)
		{
			int a1 = output[offset * 4 + 0] + output[offset * 4 + 2];
			int b1 = output[offset * 4 + 0] - output[offset * 4 + 2];
			int temp1 = output[offset * 4 + 1] * 35468 >> 16;
			int temp3 = output[offset * 4 + 3] + (output[offset * 4 + 3] * 20091 >> 16);
			int c1 = temp1 - temp3;
			temp1 = output[offset * 4 + 1] + (output[offset * 4 + 1] * 20091 >> 16);
			temp3 = output[offset * 4 + 3] * 35468 >> 16;
			int d1 = temp1 + temp3;
			output[offset * 4 + 0] = a1 + d1 + 4 >> 3;
			output[offset * 4 + 3] = a1 - d1 + 4 >> 3;
			output[offset * 4 + 1] = b1 + c1 + 4 >> 3;
			output[offset * 4 + 2] = b1 - c1 + 4 >> 3;
			offset++;
		}
		return output;
	}

	[LineNumberTable(new byte[]
	{
		159, 121, 66, 99, 105, 131, 106, 112, 112, 112,
		144, 107, 139, 127, 1, 159, 1, 101, 229, 51,
		234, 80, 99, 99, 106, 111, 110, 110, 143, 111,
		143, 127, 10, 159, 2, 101, 229, 51, 234, 79
	})]
	public static int[] encodeDCT(int[] input)
	{
		int ip = 0;
		int[] output = new int[(nint)input.LongLength];
		int op = 0;
		for (int i = 0; i < 4; i++)
		{
			int a2 = input[ip + 0] + input[ip + 3] << 3;
			int b2 = input[ip + 1] + input[ip + 2] << 3;
			int c2 = input[ip + 1] - input[ip + 2] << 3;
			int d2 = input[ip + 0] - input[ip + 3] << 3;
			output[op + 0] = a2 + b2;
			output[op + 2] = a2 - b2;
			output[op + 1] = c2 * 2217 + d2 * 5352 + 14500 >> 12;
			output[op + 3] = d2 * 2217 - c2 * 5352 + 7500 >> 12;
			ip += 4;
			op += 4;
		}
		ip = 0;
		op = 0;
		for (int i = 0; i < 4; i++)
		{
			int a1 = output[ip + 0] + output[ip + 12];
			int b1 = output[ip + 4] + output[ip + 8];
			int c1 = output[ip + 4] - output[ip + 8];
			int d1 = output[ip + 0] - output[ip + 12];
			output[op + 0] = a1 + b1 + 7 >> 4;
			output[op + 8] = a1 - b1 + 7 >> 4;
			output[op + 4] = (c1 * 2217 + d1 * 5352 + 12000 >> 16) + ((d1 != 0) ? 1 : 0);
			output[op + 12] = d1 * 2217 - c1 * 5352 + 51000 >> 16;
			ip++;
			op++;
		}
		return output;
	}

	[LineNumberTable(new byte[]
	{
		159, 110, 98, 105, 127, 6, 100, 108, 113, 112,
		112, 145, 108, 108, 108, 109, 231, 54, 236, 77,
		132, 108, 112, 112, 112, 144, 104, 104, 104, 104,
		109, 109, 109, 109, 109, 109, 109, 109, 231, 46,
		236, 85
	})]
	public static int[] decodeWHT(int[] input)
	{
		int[] output = new int[16];
		int[] array = new int[2];
		int num = (array[1] = 4);
		num = (array[0] = 4);
		int[][] diff = (int[][])ByteCodeHelper.multianewarray(typeof(int[][]).TypeHandle, array);
		int offset = 0;
		for (int i = 0; i < 4; i++)
		{
			int a2 = input[offset + 0] + input[offset + 12];
			int b2 = input[offset + 4] + input[offset + 8];
			int c2 = input[offset + 4] - input[offset + 8];
			int d2 = input[offset + 0] - input[offset + 12];
			output[offset + 0] = a2 + b2;
			output[offset + 4] = c2 + d2;
			output[offset + 8] = a2 - b2;
			output[offset + 12] = d2 - c2;
			offset++;
		}
		offset = 0;
		for (int i = 0; i < 4; i++)
		{
			int a1 = output[offset + 0] + output[offset + 3];
			int b1 = output[offset + 1] + output[offset + 2];
			int c1 = output[offset + 1] - output[offset + 2];
			int d1 = output[offset + 0] - output[offset + 3];
			int a3 = a1 + b1;
			int b3 = c1 + d1;
			int c3 = a1 - b1;
			int d3 = d1 - c1;
			output[offset + 0] = a3 + 3 >> 3;
			output[offset + 1] = b3 + 3 >> 3;
			output[offset + 2] = c3 + 3 >> 3;
			output[offset + 3] = d3 + 3 >> 3;
			diff[0][i] = a3 + 3 >> 3;
			diff[1][i] = b3 + 3 >> 3;
			diff[2][i] = c3 + 3 >> 3;
			diff[3][i] = d3 + 3 >> 3;
			offset += 4;
		}
		return output;
	}

	[LineNumberTable(new byte[]
	{
		159, 98, 66, 99, 99, 169, 202, 112, 112, 112,
		144, 116, 107, 107, 107, 101, 229, 50, 234, 81,
		99, 131, 106, 110, 111, 111, 142, 104, 104, 104,
		136, 107, 107, 107, 139, 108, 108, 108, 141, 101,
		229, 42, 234, 88
	})]
	public static int[] encodeWHT(int[] input)
	{
		int inputOffset = 0;
		int outputOffset = 0;
		int[] output = new int[(nint)input.LongLength];
		for (int i = 0; i < 4; i++)
		{
			int a2 = input[inputOffset + 0] + input[inputOffset + 2] << 2;
			int d2 = input[inputOffset + 1] + input[inputOffset + 3] << 2;
			int c2 = input[inputOffset + 1] - input[inputOffset + 3] << 2;
			int b2 = input[inputOffset + 0] - input[inputOffset + 2] << 2;
			output[outputOffset + 0] = a2 + d2 + ((a2 != 0) ? 1 : 0);
			output[outputOffset + 1] = b2 + c2;
			output[outputOffset + 2] = b2 - c2;
			output[outputOffset + 3] = a2 - d2;
			inputOffset += 4;
			outputOffset += 4;
		}
		inputOffset = 0;
		outputOffset = 0;
		for (int i = 0; i < 4; i++)
		{
			int a1 = output[inputOffset + 0] + output[inputOffset + 8];
			int d1 = output[inputOffset + 4] + output[inputOffset + 12];
			int c1 = output[inputOffset + 4] - output[inputOffset + 12];
			int b1 = output[inputOffset + 0] - output[inputOffset + 8];
			int a3 = a1 + d1;
			int b3 = b1 + c1;
			int c3 = b1 - c1;
			int d3 = a1 - d1;
			a3 += ((a3 < 0) ? 1 : 0);
			b3 += ((b3 < 0) ? 1 : 0);
			c3 += ((c3 < 0) ? 1 : 0);
			d3 += ((d3 < 0) ? 1 : 0);
			output[outputOffset + 0] = a3 + 3 >> 3;
			output[outputOffset + 4] = b3 + 3 >> 3;
			output[outputOffset + 8] = c3 + 3 >> 3;
			output[outputOffset + 12] = d3 + 3 >> 3;
			inputOffset++;
			outputOffset++;
		}
		return output;
	}
}
