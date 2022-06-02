using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using net.sourceforge.jaad.aac.syntax;
using org.jcodec.common.logging;

namespace net.sourceforge.jaad.aac.tools;

public class ICPrediction : Object
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

	[InnerClass(null, Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	internal sealed class PredictorState : Object
	{
		internal float cor0;

		internal float cor1;

		internal float var0;

		internal float var1;

		internal float r0;

		internal float r1;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Synthetic)]
		[LineNumberTable(30)]
		internal PredictorState(_1 x0)
			: this()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 135, 130, 137, 108, 108, 108, 108, 108 })]
		private PredictorState()
		{
			cor0 = 0f;
			cor1 = 0f;
			var0 = 0f;
			var1 = 0f;
			r0 = 1f;
			r1 = 1f;
		}
	}

	private const float SF_SCALE = -0.0009765625f;

	private const float INV_SF_SCALE = -1024f;

	private const int MAX_PREDICTORS = 672;

	private const float A = 61f / 64f;

	private const float ALPHA = 29f / 32f;

	private bool predictorReset;

	private int predictorResetGroup;

	private bool[] predictionUsed;

	private PredictorState[] states;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 132, 66, 105, 113, 105 })]
	public ICPrediction()
	{
		states = new PredictorState[672];
		resetAllPredictors();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "net.sourceforge.jaad.aac.AACException" })]
	[LineNumberTable(new byte[]
	{
		159,
		131,
		130,
		136,
		159,
		2,
		104,
		105,
		109,
		105,
		48,
		169,
		byte.MaxValue,
		5,
		69
	})]
	public virtual void decode(IBitStream _in, int maxSFB, SampleFrequency sf)
	{
		int predictorCount = sf.getPredictorCount();
		int num = (_in.readBool() ? 1 : 0);
		predictorReset = (byte)num != 0;
		if (num != 0)
		{
			predictorResetGroup = _in.readBits(5);
		}
		int maxPredSFB = sf.getMaximalPredictionSFB();
		int length = Math.min(maxSFB, maxPredSFB);
		predictionUsed = new bool[length];
		for (int sfb = 0; sfb < length; sfb++)
		{
			predictionUsed[sfb] = _in.readBool();
		}
		Logger.warn("ICPrediction: maxSFB={0}, maxPredSFB={1}", new int[2] { maxSFB, maxPredSFB });
	}

	[LineNumberTable(new byte[] { 159, 126, 66, 106 })]
	public virtual void setPredictionUnused(int sfb)
	{
		predictionUsed[sfb] = false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 125, 66, 136, 148, 115, 136, 103, 111, 50,
		41, 231, 69, 151
	})]
	public virtual void process(ICStream ics, float[] data, SampleFrequency sf)
	{
		ICSInfo info = ics.getInfo();
		if (info.isEightShortFrame())
		{
			resetAllPredictors();
			return;
		}
		int len = Math.min(sf.getMaximalPredictionSFB(), info.getMaxSFB());
		int[] swbOffsets = info.getSWBOffsets();
		for (int sfb = 0; sfb < len; sfb++)
		{
			for (int i = swbOffsets[sfb]; i < swbOffsets[sfb + 1]; i++)
			{
				predict(data, i, predictionUsed[sfb]);
			}
		}
		if (predictorReset)
		{
			resetPredictorGroup(predictorResetGroup);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 118, 66, 109, 40, 167 })]
	private void resetAllPredictors()
	{
		for (int i = 0; i < (nint)states.LongLength; i++)
		{
			resetPredictState(i);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 115, 97, 67, 121, 106, 111, 113, 145, 127,
		7, 159, 7, 117, 159, 0, 109, 140, 125, 127,
		12, 125, 159, 12, 125, 118
	})]
	private void predict(float[] data, int off, bool output)
	{
		if (states[off] == null)
		{
			states[off] = new PredictorState(null);
		}
		PredictorState state = states[off];
		float r0 = state.r0;
		float r1 = state.r1;
		float cor0 = state.cor0;
		float cor1 = state.cor1;
		float var0 = state.var0;
		float var1 = state.var1;
		float k1 = ((!(var0 > 1f)) ? 0f : (cor0 * even(61f / 64f / var0)));
		float k2 = ((!(var1 > 1f)) ? 0f : (cor1 * even(61f / 64f / var1)));
		float pv = round(k1 * r0 + k2 * r1);
		if (output)
		{
			data[off] += pv * -0.0009765625f;
		}
		float e0 = data[off] * -1024f;
		float e1 = e0 - k1 * r0;
		state.cor1 = trunc(29f / 32f * cor1 + r1 * e1);
		state.var1 = trunc(29f / 32f * var1 + 0.5f * (r1 * r1 + e1 * e1));
		state.cor0 = trunc(29f / 32f * cor0 + r0 * e0);
		state.var0 = trunc(29f / 32f * var0 + 0.5f * (r0 * r0 + e0 * e0));
		state.r1 = trunc(61f / 64f * (r0 - k1 * e0));
		state.r0 = trunc(61f / 64f * e0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 117, 162, 111, 40, 168 })]
	private void resetPredictorGroup(int group)
	{
		for (int i = group - 1; i < (nint)states.LongLength; i += 30)
		{
			resetPredictState(i);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 121, 98, 121, 115, 115, 115, 115, 115, 115 })]
	private void resetPredictState(int index)
	{
		if (states[index] == null)
		{
			states[index] = new PredictorState(null);
		}
		states[index].r0 = 0f;
		states[index].r1 = 0f;
		states[index].cor0 = 0f;
		states[index].cor1 = 0f;
		states[index].var0 = 16256f;
		states[index].var1 = 16256f;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 108, 130, 105, 115 })]
	private float even(float pf)
	{
		int i = Float.floatToIntBits(pf);
		i = (i + 32767 + (i & 1)) & -65536;
		FloatConverter converter = default(FloatConverter);
		float result = FloatConverter.ToFloat(i, ref converter);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(134)]
	private float round(float pf)
	{
		FloatConverter converter = default(FloatConverter);
		float result = FloatConverter.ToFloat((Float.floatToIntBits(pf) + 32768) & -65536, ref converter);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(144)]
	private float trunc(float pf)
	{
		FloatConverter converter = default(FloatConverter);
		float result = FloatConverter.ToFloat(Float.floatToIntBits(pf) & -65536, ref converter);
		
		return result;
	}
}
