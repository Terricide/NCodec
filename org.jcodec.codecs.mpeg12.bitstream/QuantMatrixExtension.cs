using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.nio;
using org.jcodec.common.io;

namespace org.jcodec.codecs.mpeg12.bitstream;

public class QuantMatrixExtension : Object, MPEGHeader
{
	public int[] intra_quantiser_matrix;

	public int[] non_intra_quantiser_matrix;

	public int[] chroma_intra_quantiser_matrix;

	public int[] chroma_non_intra_quantiser_matrix;

	public const int Quant_Matrix_Extension = 3;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 137, 162, 103, 105, 109, 105, 109, 105, 109,
		105, 141
	})]
	public static QuantMatrixExtension read(BitReader _in)
	{
		QuantMatrixExtension qme = new QuantMatrixExtension();
		if (_in.read1Bit() != 0)
		{
			qme.intra_quantiser_matrix = readQMat(_in);
		}
		if (_in.read1Bit() != 0)
		{
			qme.non_intra_quantiser_matrix = readQMat(_in);
		}
		if (_in.read1Bit() != 0)
		{
			qme.chroma_intra_quantiser_matrix = readQMat(_in);
		}
		if (_in.read1Bit() != 0)
		{
			qme.chroma_non_intra_quantiser_matrix = readQMat(_in);
		}
		return qme;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 131, 98, 104, 137, 115, 105, 110, 115, 105,
		110, 115, 105, 110, 115, 105, 142, 105
	})]
	public virtual void write(ByteBuffer bb)
	{
		BitWriter bw = new BitWriter(bb);
		bw.writeNBit(3, 4);
		bw.write1Bit((intra_quantiser_matrix != null) ? 1 : 0);
		if (intra_quantiser_matrix != null)
		{
			writeQMat(intra_quantiser_matrix, bw);
		}
		bw.write1Bit((non_intra_quantiser_matrix != null) ? 1 : 0);
		if (non_intra_quantiser_matrix != null)
		{
			writeQMat(non_intra_quantiser_matrix, bw);
		}
		bw.write1Bit((chroma_intra_quantiser_matrix != null) ? 1 : 0);
		if (chroma_intra_quantiser_matrix != null)
		{
			writeQMat(chroma_intra_quantiser_matrix, bw);
		}
		bw.write1Bit((chroma_non_intra_quantiser_matrix != null) ? 1 : 0);
		if (chroma_non_intra_quantiser_matrix != null)
		{
			writeQMat(chroma_non_intra_quantiser_matrix, bw);
		}
		bw.flush();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(14)]
	public QuantMatrixExtension()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 98, 105, 104, 43, 135 })]
	private static int[] readQMat(BitReader _in)
	{
		int[] qmat = new int[64];
		for (int i = 0; i < 64; i++)
		{
			qmat[i] = _in.readNBit(8);
		}
		return qmat;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 126, 98, 104, 43, 135 })]
	private void writeQMat(int[] matrix, BitWriter ob)
	{
		for (int i = 0; i < 64; i++)
		{
			ob.writeNBit(matrix[i], 8);
		}
	}
}
