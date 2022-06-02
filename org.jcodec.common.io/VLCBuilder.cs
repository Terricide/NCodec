using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.common.io;

public class VLCBuilder : Object
{
	[SpecialName]
	[EnclosingMethod(null, "getVLC", "()Lorg.jcodec.common.io.VLC;")]
	internal class _1 : VLC
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal VLCBuilder val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal VLCBuilder this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(55)]
		internal _1(VLCBuilder this_00240, int[] codes, int[] codeSizes, VLCBuilder vlcb)
		{
			this.this_00240 = this_00240;
			val_0024self = vlcb;
			base._002Ector(codes, codeSizes);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(57)]
		public override int readVLC(BitReader _in)
		{
			int result = access_0024000(val_0024self).get(base.readVLC(_in));
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(61)]
		public override int readVLC16(BitReader _in)
		{
			int result = access_0024000(val_0024self).get(base.readVLC16(_in));
			
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 126, 98, 123 })]
		public override void writeVLC(BitWriter @out, int code)
		{
			base.writeVLC(@out, access_0024100(val_0024self).get(code));
		}
	}

	private IntIntMap forward;

	private IntIntMap inverse;

	private IntArrayList codes;

	private IntArrayList codesSizes;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 162, 103, 104, 49, 167 })]
	public static VLCBuilder createVLCBuilder(int[] codes, int[] lens, int[] vals)
	{
		VLCBuilder b = new VLCBuilder();
		for (int i = 0; i < (nint)codes.LongLength; i++)
		{
			b.setInt(codes[i], lens[i], vals[i]);
		}
		return b;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 130, 99 })]
	public virtual VLC getVLC()
	{
		_1 result = new _1(this, codes.toArray(), codesSizes.toArray(), this);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 135, 162, 105, 108, 108, 108, 108 })]
	public VLCBuilder()
	{
		forward = new IntIntMap();
		inverse = new IntIntMap();
		codes = IntArrayList.createIntArrayList();
		codesSizes = IntArrayList.createIntArrayList();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 162, 150 })]
	public virtual VLCBuilder set(int val, string code)
	{
		setInt(Integer.parseInt(code, 2), String.instancehelper_length(code), val);
		return this;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 131, 98, 117, 109, 122, 154 })]
	public virtual VLCBuilder setInt(int code, int len, int val)
	{
		codes.add(code << 32 - len);
		codesSizes.add(len);
		forward.put(val, codes.size() - 1);
		inverse.put(codes.size() - 1, val);
		return this;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(16)]
	internal static IntIntMap access_0024000(VLCBuilder x0)
	{
		return x0.inverse;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(16)]
	internal static IntIntMap access_0024100(VLCBuilder x0)
	{
		return x0.forward;
	}
}
