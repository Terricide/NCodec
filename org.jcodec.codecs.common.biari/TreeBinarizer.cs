using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.common.biari;

public class TreeBinarizer : Object
{
	private Context[] models;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 66, 113, 107, 48, 231, 69 })]
	private void initContextModels()
	{
		models = new Context[255];
		for (int i = 0; i < 255; i++)
		{
			models[i] = new Context(0, 0);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 66, 137, 105 })]
	public TreeBinarizer()
	{
		initContextModels();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 133, 130, 99, 99, 99, 103, 109, 177, 107,
		106, 229, 57, 231, 74
	})]
	public virtual void binarize(int symbol, MQEncoder encoder)
	{
		int inverted = 0;
		int nextModel = 0;
		int levelOffset = 0;
		for (int i = 0; i < 8; i++)
		{
			int bin = (symbol >> 7 - i) & 1;
			encoder.encode(bin, models[nextModel]);
			inverted |= bin << i;
			levelOffset += 1 << i;
			nextModel = levelOffset + inverted;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 129, 162, 99, 99, 99, 99, 105, 113, 174,
		108, 107, 229, 57, 233, 74
	})]
	public virtual int debinarize(MQDecoder decoder)
	{
		int symbol = 0;
		int inverted = 0;
		int nextModel = 0;
		int levelOffset = 0;
		for (int i = 0; i < 8; i++)
		{
			int bin = decoder.decode(models[nextModel]);
			symbol |= bin << 7 - i;
			inverted |= bin << i;
			levelOffset += 1 << i;
			nextModel = levelOffset + inverted;
		}
		return symbol;
	}
}
