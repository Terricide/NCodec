using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264.decode;

public class BlockInterpolator : Object
{
	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _1 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(921)]
		internal _1(BlockInterpolator this_00240)
		{
			this.this_00240 = this_00240;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 167, 98, 120 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			getLuma00(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _10 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(985)]
		internal _10(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 151, 98, 126 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma12(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _11 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(993)]
		internal _11(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 149, 98, 126 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma22(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _12 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(999)]
		internal _12(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 148, 162, 126 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma32(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _13 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1005)]
		internal _13(BlockInterpolator this_00240)
		{
			this.this_00240 = this_00240;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 146, 98, 120 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			getLuma03(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _14 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1013)]
		internal _14(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 144, 98, 126 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma13(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _15 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1021)]
		internal _15(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 142, 98, 126 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma23(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _16 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1027)]
		internal _16(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 141, 162, 126 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma33(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _17 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1040)]
		internal _17(BlockInterpolator this_00240)
		{
			this.this_00240 = this_00240;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 137, 66, 121 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			getLuma00Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _18 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1048)]
		internal _18(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 135, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma10Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _19 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1056)]
		internal _19(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 133, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma20Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _2 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(929)]
		internal _2(BlockInterpolator this_00240)
		{
			this.this_00240 = this_00240;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 165, 98, 120 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			getLuma10(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _20 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1062)]
		internal _20(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 132, 130, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma30Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _21 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1068)]
		internal _21(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 130, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma01Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _22 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1076)]
		internal _22(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 128, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma11Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _23 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1084)]
		internal _23(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 126, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma21Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _24 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1090)]
		internal _24(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 125, 130, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma31Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _25 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1096)]
		internal _25(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 123, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma02Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _26 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1104)]
		internal _26(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 121, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma12Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _27 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1112)]
		internal _27(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 119, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma22Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _28 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1118)]
		internal _28(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 118, 130, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma32Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _29 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1124)]
		internal _29(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 116, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma03Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _3 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(937)]
		internal _3(BlockInterpolator this_00240)
		{
			this.this_00240 = this_00240;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 163, 98, 120 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			getLuma20(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _30 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1132)]
		internal _30(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 114, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma13Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _31 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1140)]
		internal _31(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 112, 66, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma23Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initUnsafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _32 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(1146)]
		internal _32(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 111, 130, 127, 0 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma33Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _4 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(943)]
		internal _4(BlockInterpolator this_00240)
		{
			this.this_00240 = this_00240;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 162, 162, 120 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			getLuma30(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _5 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(949)]
		internal _5(BlockInterpolator this_00240)
		{
			this.this_00240 = this_00240;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 160, 98, 120 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			getLuma01(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _6 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(957)]
		internal _6(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 158, 98, 126 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma11(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _7 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(965)]
		internal _7(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 156, 98, 126 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma21(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _8 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(971)]
		internal _8(BlockInterpolator this_00240, BlockInterpolator bi)
		{
			this.this_00240 = this_00240;
			val_0024self = bi;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 155, 162, 126 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			val_0024self.getLuma31(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "initSafe", "()[Lorg.jcodec.codecs.h264.decode.BlockInterpolator$LumaInterpolator;")]
	internal class _9 : Object, LumaInterpolator
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal BlockInterpolator this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(977)]
		internal _9(BlockInterpolator this_00240)
		{
			this.this_00240 = this_00240;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 158, 153, 98, 120 })]
		public virtual void getLuma(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
		{
			getLuma02(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static | Modifiers.Interface | Modifiers.Abstract)]
	internal interface LumaInterpolator
	{
		void getLuma(byte[] barr1, int i1, int i2, byte[] barr2, int i3, int i4, int i5, int i6, int i7, int i8);
	}

	private int[] tmp1;

	private int[] tmp2;

	private byte[] tmp3;

	private LumaInterpolator[] safe;

	private LumaInterpolator[] @unsafe;

	[LineNumberTable(new byte[]
	{
		159, 120, 98, 105, 104, 108, 101, 231, 61, 231,
		69
	})]
	internal static void getLuma00(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int off = y * picW + x;
		for (int i = 0; i < blkH; i++)
		{
			ByteCodeHelper.arraycopy_primitive_1(pic, off, blk, blkOff, blkW);
			off += picW;
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 169, 130, 99 })]
	private LumaInterpolator[] initSafe()
	{
		return new LumaInterpolator[16]
		{
			new _1(this),
			new _2(this),
			new _3(this),
			new _4(this),
			new _5(this),
			new _6(this, this),
			new _7(this, this),
			new _8(this, this),
			new _9(this),
			new _10(this, this),
			new _11(this, this),
			new _12(this, this),
			new _13(this),
			new _14(this, this),
			new _15(this, this),
			new _16(this, this)
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 139, 98, 99 })]
	private LumaInterpolator[] initUnsafe()
	{
		return new LumaInterpolator[16]
		{
			new _17(this),
			new _18(this, this),
			new _19(this, this),
			new _20(this, this),
			new _21(this, this),
			new _22(this, this),
			new _23(this, this),
			new _24(this, this),
			new _25(this, this),
			new _26(this, this),
			new _27(this, this),
			new _28(this, this),
			new _29(this, this),
			new _30(this, this),
			new _31(this, this),
			new _32(this, this)
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 205, 130, 101, 133, 104, 143, 106, 56, 169,
		232, 58, 231, 72
	})]
	private static void getChroma00Unsafe(byte[] pic, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int maxH = picH - 1;
		int maxW = picW - 1;
		for (int j = 0; j < blkH; j++)
		{
			int lineStart = MathUtil.clip(j + y, 0, maxH) * picW;
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = pic[lineStart + MathUtil.clip(x + i, 0, maxW)];
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 186, 98, 102, 101, 133, 107, 109, 125, 159,
		0, 251, 60, 236, 70, 232, 57, 234, 73
	})]
	private static void getChromaX0Unsafe(byte[] pels, int picW, int picH, byte[] blk, int blkOff, int blkStride, int fullX, int fullY, int fracX, int blkW, int blkH)
	{
		int eMx = 8 - fracX;
		int maxW = picW - 1;
		int maxH = picH - 1;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int w0 = MathUtil.clip(fullY + j, 0, maxH) * picW + MathUtil.clip(fullX + i, 0, maxW);
				int w = MathUtil.clip(fullY + j, 0, maxH) * picW + MathUtil.clip(fullX + i + 1, 0, maxW);
				blk[blkOff + i] = (byte)(sbyte)(eMx * pels[w0] + fracX * pels[w] + 4 >> 3);
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 196, 130, 101, 101, 134, 107, 112, 146, 106,
		114, 146, 251, 60, 233, 70, 232, 54, 234, 76
	})]
	private static void getChroma0XUnsafe(byte[] pels, int picW, int picH, byte[] blk, int blkOff, int blkStride, int fullX, int fullY, int fracY, int blkW, int blkH)
	{
		int maxW = picW - 1;
		int maxH = picH - 1;
		int eMy = 8 - fracY;
		for (int j = 0; j < blkH; j++)
		{
			int off0 = MathUtil.clip(fullY + j, 0, maxH) * picW;
			int off = MathUtil.clip(fullY + j + 1, 0, maxH) * picW;
			for (int i = 0; i < blkW; i++)
			{
				int w0 = MathUtil.clip(fullX + i, 0, maxW) + off0;
				int w = MathUtil.clip(fullX + i, 0, maxW) + off;
				blk[blkOff + i] = (byte)(sbyte)(eMy * pels[w0] + fracY * pels[w] + 4 >> 3);
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158,
		175,
		66,
		101,
		133,
		102,
		134,
		109,
		109,
		126,
		127,
		1,
		127,
		1,
		159,
		3,
		byte.MaxValue,
		22,
		58,
		236,
		73,
		232,
		54,
		236,
		76
	})]
	private static void getChromaXXUnsafe(byte[] pels, int picW, int picH, byte[] blk, int blkOff, int blkStride, int fullX, int fullY, int fracX, int fracY, int blkW, int blkH)
	{
		int maxH = picH - 1;
		int maxW = picW - 1;
		int eMx = 8 - fracX;
		int eMy = 8 - fracY;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int w0 = MathUtil.clip(fullY + j, 0, maxH) * picW + MathUtil.clip(fullX + i, 0, maxW);
				int w = MathUtil.clip(fullY + j + 1, 0, maxH) * picW + MathUtil.clip(fullX + i, 0, maxW);
				int w2 = MathUtil.clip(fullY + j, 0, maxH) * picW + MathUtil.clip(fullX + i + 1, 0, maxW);
				int w3 = MathUtil.clip(fullY + j + 1, 0, maxH) * picW + MathUtil.clip(fullX + i + 1, 0, maxW);
				blk[blkOff + i] = (byte)(sbyte)(eMx * eMy * pels[w0] + fracX * eMy * pels[w2] + eMx * fracY * pels[w] + fracX * fracY * pels[w3] + 32 >> 6);
			}
			blkOff += blkStride;
		}
	}

	[LineNumberTable(new byte[]
	{
		158, 207, 66, 105, 104, 109, 101, 232, 61, 231,
		69
	})]
	private static void getChroma00(byte[] pic, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int off = y * picW + x;
		for (int i = 0; i < blkH; i++)
		{
			ByteCodeHelper.arraycopy_primitive_1(pic, off, blk, blkOff, blkW);
			off += picW;
			blkOff += blkStride;
		}
	}

	[LineNumberTable(new byte[]
	{
		158, 190, 98, 105, 107, 134, 107, 106, 63, 0,
		169, 101, 101, 232, 58, 234, 72
	})]
	private static void getChromaX0(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int fullX, int fullY, int fracX, int blkW, int blkH)
	{
		int w0 = fullY * picW + fullX;
		int w = w0 + ((fullX < picW - 1) ? 1 : 0);
		int eMx = 8 - fracX;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(eMx * pels[w0 + i] + fracX * pels[w + i] + 4 >> 3);
			}
			w0 += picW;
			w += picW;
			blkOff += blkStride;
		}
	}

	[LineNumberTable(new byte[]
	{
		158, 200, 66, 105, 111, 134, 107, 138, 31, 0,
		201, 101, 101, 232, 57, 234, 73
	})]
	private static void getChroma0X(byte[] pels, int picW, int picH, byte[] blk, int blkOff, int blkStride, int fullX, int fullY, int fracY, int blkW, int blkH)
	{
		int w0 = fullY * picW + fullX;
		int w = w0 + ((fullY < picH - 1) ? picW : 0);
		int eMy = 8 - fracY;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(eMy * pels[w0 + i] + fracY * pels[w + i] + 4 >> 3);
			}
			w0 += picW;
			w += picW;
			blkOff += blkStride;
		}
	}

	[LineNumberTable(new byte[]
	{
		158, 181, 98, 105, 111, 107, 103, 103, 135, 109,
		138, 31, 34, 233, 69, 104, 101, 101, 101, 229,
		54, 236, 76
	})]
	private static void getChromaXX(byte[] pels, int picW, int picH, byte[] blk, int blkOff, int blkStride, int fullX, int fullY, int fracX, int fracY, int blkW, int blkH)
	{
		int w0 = fullY * picW + fullX;
		int w = w0 + ((fullY < picH - 1) ? picW : 0);
		int w2 = w0 + ((fullX < picW - 1) ? 1 : 0);
		int w3 = w2 + w - w0;
		int eMx = 8 - fracX;
		int eMy = 8 - fracY;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(eMx * eMy * pels[w0 + i] + fracX * eMy * pels[w2 + i] + eMx * fracY * pels[w + i] + fracX * fracY * pels[w3 + i] + 32 >> 6);
			}
			blkOff += blkStride;
			w0 += picW;
			w += picW;
			w2 += picW;
			w3 += picW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 98, 130, 101, 133, 107, 111, 112, 110, 112,
		112, 144, 101, 109, 145, 113, 114, 146, 147, 232,
		55, 236, 55, 234, 85
	})]
	internal static void getLuma20UnsafeNoRound(byte[] pic, int picW, int picH, int[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int maxW = picW - 1;
		int maxH = picH - 1;
		for (int i = 0; i < blkW; i++)
		{
			int ipos_m2 = MathUtil.clip(x + i - 2, 0, maxW);
			int ipos_m1 = MathUtil.clip(x + i - 1, 0, maxW);
			int ipos = MathUtil.clip(x + i, 0, maxW);
			int ipos_p1 = MathUtil.clip(x + i + 1, 0, maxW);
			int ipos_p2 = MathUtil.clip(x + i + 2, 0, maxW);
			int ipos_p3 = MathUtil.clip(x + i + 3, 0, maxW);
			int boff = blkOff;
			for (int j = 0; j < blkH; j++)
			{
				int lineStart = MathUtil.clip(j + y, 0, maxH) * picW;
				int a = pic[lineStart + ipos_m2] + pic[lineStart + ipos_p3];
				int b = pic[lineStart + ipos_m1] + pic[lineStart + ipos_p2];
				int c = pic[lineStart + ipos] + pic[lineStart + ipos_p1];
				blk[boff + i] = a + 5 * ((c << 2) - b);
				boff += blkStride;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 73, 162, 101, 133, 107, 113, 114, 112, 114,
		114, 146, 109, 143, 113, 114, 146, 244, 57, 236,
		73, 232, 47, 234, 83
	})]
	internal static void getLuma02UnsafeNoRound(byte[] pic, int picW, int picH, int[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int maxH = picH - 1;
		int maxW = picW - 1;
		for (int j = 0; j < blkH; j++)
		{
			int offP0 = MathUtil.clip(y + j - 2, 0, maxH) * picW;
			int offP1 = MathUtil.clip(y + j - 1, 0, maxH) * picW;
			int offP2 = MathUtil.clip(y + j, 0, maxH) * picW;
			int offP3 = MathUtil.clip(y + j + 1, 0, maxH) * picW;
			int offP4 = MathUtil.clip(y + j + 2, 0, maxH) * picW;
			int offP5 = MathUtil.clip(y + j + 3, 0, maxH) * picW;
			for (int i = 0; i < blkW; i++)
			{
				int pres_x = MathUtil.clip(x + i, 0, maxW);
				int a = pic[pres_x + offP0] + pic[pres_x + offP5];
				int b = pic[pres_x + offP1] + pic[pres_x + offP4];
				int c = pic[pres_x + offP2] + pic[pres_x + offP3];
				blk[blkOff + i] = a + 5 * ((c << 2) - b);
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 103, 130, 105, 107, 100, 107, 112, 114, 114,
		127, 2, 229, 59, 234, 71, 101, 231, 54, 234,
		76
	})]
	internal static void getLuma20(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int off = y * picW + x;
		for (int j = 0; j < blkH; j++)
		{
			int off2 = -2;
			for (int i = 0; i < blkW; i++)
			{
				int a = pic[off + off2] + pic[off + off2 + 5];
				int b = pic[off + off2 + 1] + pic[off + off2 + 4];
				int c = pic[off + off2 + 2] + pic[off + off2 + 3];
				blk[blkOff + i] = (byte)(sbyte)MathUtil.clip(a + 5 * ((c << 2) - b) + 16 >> 5, -128, 127);
				off2++;
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 91, 162, 155, 104, 100, 104, 127, 1, 6,
		7, 231, 71
	})]
	internal virtual void getLuma20Unsafe(byte[] pic, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20UnsafeNoRound(pic, picW, picH, tmp1, blkOff, blkStride, x, y, blkW, blkH);
		for (int i = 0; i < blkW; i++)
		{
			int boff = blkOff;
			for (int j = 0; j < blkH; j++)
			{
				blk[boff + i] = (byte)(sbyte)MathUtil.clip(tmp1[boff + i] + 16 >> 5, -128, 127);
				boff += blkStride;
			}
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159,
		77,
		66,
		156,
		109,
		109,
		115,
		116,
		116,
		byte.MaxValue,
		3,
		60,
		236,
		70,
		101,
		231,
		56,
		236,
		74
	})]
	internal static void getLuma02(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int off = (y - 2) * picW + x;
		int picWx2 = picW + picW;
		int picWx3 = picWx2 + picW;
		int picWx4 = picWx3 + picW;
		int picWx5 = picWx4 + picW;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int a = pic[off + i] + pic[off + i + picWx5];
				int b = pic[off + i + picW] + pic[off + i + picWx4];
				int c = pic[off + i + picWx2] + pic[off + i + picWx3];
				blk[blkOff + i] = (byte)(sbyte)MathUtil.clip(a + 5 * ((c << 2) - b) + 16 >> 5, -128, 127);
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 66, 130, 155, 104, 104, 63, 3, 167, 232,
		60, 231, 70
	})]
	internal virtual void getLuma02Unsafe(byte[] pic, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma02UnsafeNoRound(pic, picW, picH, tmp1, blkOff, blkStride, x, y, blkW, blkH);
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)MathUtil.clip(tmp1[blkOff + i] + 16 >> 5, -128, 127);
			}
			blkOff += blkStride;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 107, 130, 105, 107, 100, 107, 112, 114, 114,
		114, 229, 59, 234, 71, 101, 231, 54, 234, 76
	})]
	internal static void getLuma20NoRound(byte[] pic, int picW, int[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int off = y * picW + x;
		for (int j = 0; j < blkH; j++)
		{
			int off2 = -2;
			for (int i = 0; i < blkW; i++)
			{
				int a = pic[off + off2] + pic[off + off2 + 5];
				int b = pic[off + off2 + 1] + pic[off + off2 + 4];
				int c = pic[off + off2 + 2] + pic[off + off2 + 3];
				blk[blkOff + i] = a + 5 * ((c << 2) - b);
				off2++;
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 87, 162, 156, 109, 109, 115, 116, 148, 243,
		59, 236, 71, 101, 231, 55, 236, 75
	})]
	internal static void getLuma02NoRoundInt(int[] pic, int picW, int[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int off = (y - 2) * picW + x;
		int picWx2 = picW + picW;
		int picWx3 = picWx2 + picW;
		int picWx4 = picWx3 + picW;
		int picWx5 = picWx4 + picW;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int a = pic[off + i] + pic[off + i + picWx5];
				int b = pic[off + i + picW] + pic[off + i + picWx4];
				int c = pic[off + i + picWx2] + pic[off + i + picWx3];
				blk[blkOff + i] = a + 5 * ((c << 2) - b);
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 82, 162, 156, 109, 109, 115, 116, 148, 243,
		59, 236, 71, 101, 231, 55, 236, 75
	})]
	internal static void getLuma02NoRound(byte[] pic, int picW, int[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int off = (y - 2) * picW + x;
		int picWx2 = picW + picW;
		int picWx3 = picWx2 + picW;
		int picWx4 = picWx3 + picW;
		int picWx5 = picWx4 + picW;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int a = pic[off + i] + pic[off + i + picWx5];
				int b = pic[off + i + picW] + pic[off + i + picWx4];
				int c = pic[off + i + picWx2] + pic[off + i + picWx3];
				blk[blkOff + i] = a + 5 * ((c << 2) - b);
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[LineNumberTable(new byte[]
	{
		159, 112, 98, 105, 107, 100, 107, 112, 114, 114,
		114, 229, 59, 234, 71, 101, 231, 54, 234, 76
	})]
	internal static void getLuma20NoRoundInt(int[] pic, int picW, int[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int off = y * picW + x;
		for (int j = 0; j < blkH; j++)
		{
			int off2 = -2;
			for (int i = 0; i < blkW; i++)
			{
				int a = pic[off + off2] + pic[off + off2 + 5];
				int b = pic[off + off2 + 1] + pic[off + off2 + 4];
				int c = pic[off + off2 + 2] + pic[off + off2 + 3];
				blk[blkOff + i] = a + 5 * ((c << 2) - b);
				off2++;
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[LineNumberTable(new byte[]
	{
		158, 211, 98, 99, 104, 104, 54, 167, 102, 230,
		59, 231, 71
	})]
	private static void merge(byte[] first, byte[] second, int blkOff, int blkStride, int blkW, int blkH)
	{
		int tOff = 0;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				first[blkOff + i] = (byte)(sbyte)(first[blkOff + i] + second[tOff + i] + 1 >> 1);
			}
			blkOff += blkStride;
			tOff += blkW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 105, 113, 113, 113, 109, 109 })]
	public BlockInterpolator()
	{
		tmp1 = new int[1024];
		tmp2 = new int[1024];
		tmp3 = new byte[1024];
		safe = initSafe();
		@unsafe = initUnsafe();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 134, 162, 102, 134, 102, 102, 127, 6, 127,
		12, 44, 168, 127, 12, 44, 168
	})]
	public virtual void getBlockLuma(Picture pic, Picture @out, int off, int x, int y, int w, int h)
	{
		int xInd = x & 3;
		int yInd = y & 3;
		int xFp = x >> 2;
		int yFp = y >> 2;
		if (xFp < 2 || yFp < 2 || xFp > pic.getWidth() - w - 5 || yFp > pic.getHeight() - h - 5)
		{
			@unsafe[(yInd << 2) + xInd].getLuma(pic.getData()[0], pic.getWidth(), pic.getHeight(), @out.getPlaneData(0), off, @out.getPlaneWidth(0), xFp, yFp, w, h);
		}
		else
		{
			safe[(yInd << 2) + xInd].getLuma(pic.getData()[0], pic.getWidth(), pic.getHeight(), @out.getPlaneData(0), off, @out.getPlaneWidth(0), xFp, yFp, w, h);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 130, 162, 102, 134, 102, 134, 126, 103, 121,
		132, 154, 100, 154, 187, 103, 118, 132, 151, 100,
		151, 184
	})]
	public static void getBlockChroma(byte[] pels, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int xInd = x & 7;
		int yInd = y & 7;
		int xFull = x >> 3;
		int yFull = y >> 3;
		if (xFull < 0 || xFull > picW - blkW - 1 || yFull < 0 || yFull > picH - blkH - 1)
		{
			if (xInd == 0 && yInd == 0)
			{
				getChroma00Unsafe(pels, picW, picH, blk, blkOff, blkStride, xFull, yFull, blkW, blkH);
			}
			else if (yInd == 0)
			{
				getChromaX0Unsafe(pels, picW, picH, blk, blkOff, blkStride, xFull, yFull, xInd, blkW, blkH);
			}
			else if (xInd == 0)
			{
				getChroma0XUnsafe(pels, picW, picH, blk, blkOff, blkStride, xFull, yFull, yInd, blkW, blkH);
			}
			else
			{
				getChromaXXUnsafe(pels, picW, picH, blk, blkOff, blkStride, xFull, yFull, xInd, yInd, blkW, blkH);
			}
		}
		else if (xInd == 0 && yInd == 0)
		{
			getChroma00(pels, picW, picH, blk, blkOff, blkStride, xFull, yFull, blkW, blkH);
		}
		else if (yInd == 0)
		{
			getChromaX0(pels, picW, picH, blk, blkOff, blkStride, xFull, yFull, xInd, blkW, blkH);
		}
		else if (xInd == 0)
		{
			getChroma0X(pels, picW, picH, blk, blkOff, blkStride, xFull, yFull, yInd, blkW, blkH);
		}
		else
		{
			getChromaXX(pels, picW, picH, blk, blkOff, blkStride, xFull, yFull, xInd, yInd, blkW, blkH);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 117, 130, 101, 133, 104, 143, 106, 56, 169,
		232, 58, 231, 72
	})]
	internal static void getLuma00Unsafe(byte[] pic, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int maxH = picH - 1;
		int maxW = picW - 1;
		for (int j = 0; j < blkH; j++)
		{
			int lineStart = MathUtil.clip(j + y, 0, maxH) * picW;
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = pic[lineStart + MathUtil.clip(x + i, 0, maxW)];
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 62, 98, 148, 105, 136, 136, 22, 199, 101,
		231, 57, 231, 73
	})]
	internal static void getLuma10(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20(pic, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		int off = y * picW + x;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(blk[blkOff + i] + pic[off + i] + 1 >> 1);
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 57, 66, 101, 133, 152, 107, 143, 106, 63,
		8, 169, 232, 58, 234, 72
	})]
	internal virtual void getLuma10Unsafe(byte[] pic, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int maxH = picH - 1;
		int maxW = picW - 1;
		getLuma20Unsafe(pic, picW, picH, blk, blkOff, blkStride, x, y, blkW, blkH);
		for (int j = 0; j < blkH; j++)
		{
			int lineStart = MathUtil.clip(j + y, 0, maxH) * picW;
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(blk[blkOff + i] + pic[lineStart + MathUtil.clip(x + i, 0, maxW)] + 1 >> 1);
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 52, 66, 148, 105, 104, 104, 56, 167, 101,
		231, 59, 231, 71
	})]
	internal static void getLuma30(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20(pic, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		int off = y * picW + x;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(pic[off + i + 1] + blk[blkOff + i] + 1 >> 1);
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 48, 98, 101, 133, 152, 107, 143, 106, 63,
		10, 169, 232, 58, 234, 72
	})]
	internal virtual void getLuma30Unsafe(byte[] pic, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int maxH = picH - 1;
		int maxW = picW - 1;
		getLuma20Unsafe(pic, picW, picH, blk, blkOff, blkStride, x, y, blkW, blkH);
		for (int j = 0; j < blkH; j++)
		{
			int lineStart = MathUtil.clip(j + y, 0, maxH) * picW;
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(blk[blkOff + i] + pic[lineStart + MathUtil.clip(x + i + 1, 0, maxW)] + 1 >> 1);
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 43, 98, 148, 105, 104, 104, 54, 167, 101,
		231, 59, 231, 71
	})]
	internal static void getLuma01(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma02(pic, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		int off = y * picW + x;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(blk[blkOff + i] + pic[off + i] + 1 >> 1);
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 39, 130, 101, 133, 152, 107, 111, 106, 63,
		8, 169, 232, 59, 234, 71
	})]
	internal virtual void getLuma01Unsafe(byte[] pic, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int maxH = picH - 1;
		int maxW = picW - 1;
		getLuma02Unsafe(pic, picW, picH, blk, blkOff, blkStride, x, y, blkW, blkH);
		for (int j = 0; j < blkH; j++)
		{
			int lineStart = MathUtil.clip(y + j, 0, maxH) * picW;
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(blk[blkOff + i] + pic[lineStart + MathUtil.clip(x + i, 0, maxW)] + 1 >> 1);
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 34, 98, 148, 105, 104, 104, 56, 167, 101,
		231, 59, 231, 72
	})]
	internal static void getLuma03(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma02(pic, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		int off = y * picW + x;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(blk[blkOff + i] + pic[off + i + picW] + 1 >> 1);
			}
			off += picW;
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 30, 162, 101, 133, 152, 107, 113, 106, 63,
		8, 169, 232, 59, 234, 71
	})]
	internal virtual void getLuma03Unsafe(byte[] pic, int picW, int picH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int maxH = picH - 1;
		int maxW = picW - 1;
		getLuma02Unsafe(pic, picW, picH, blk, blkOff, blkStride, x, y, blkW, blkH);
		for (int j = 0; j < blkH; j++)
		{
			int lineStart = MathUtil.clip(y + j + 1, 0, maxH) * picW;
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)(blk[blkOff + i] + pic[lineStart + MathUtil.clip(x + i, 0, maxW)] + 1 >> 1);
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 25, 130, 125, 158, 102, 107, 104, 127, 0,
		123, 240, 61, 231, 69, 104, 230, 57, 234, 73
	})]
	internal virtual void getLuma21(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20NoRound(pic, picW, tmp1, 0, blkW, x, y - 2, blkW, blkH + 7);
		getLuma02NoRoundInt(tmp1, blkW, tmp2, blkOff, blkStride, 0, 2, blkW, blkH);
		int off = blkW << 1;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int rounded = MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
				int rounded2 = MathUtil.clip(tmp1[off + i] + 16 >> 5, -128, 127);
				blk[blkOff + i] = (byte)(sbyte)(rounded + rounded2 + 1 >> 1);
			}
			blkOff += blkStride;
			off += blkW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 20, 130, 126, 158, 102, 107, 104, 127, 0,
		123, 241, 61, 231, 69, 104, 230, 57, 234, 73
	})]
	internal virtual void getLuma21Unsafe(byte[] pic, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20UnsafeNoRound(pic, picW, imgH, tmp1, 0, blkW, x, y - 2, blkW, blkH + 7);
		getLuma02NoRoundInt(tmp1, blkW, tmp2, blkOff, blkStride, 0, 2, blkW, blkH);
		int off = blkW << 1;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int rounded = MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
				int rounded2 = MathUtil.clip(tmp1[off + i] + 16 >> 5, -128, 127);
				blk[blkOff + i] = (byte)(sbyte)(rounded + rounded2 + 1 >> 1);
			}
			blkOff += blkStride;
			off += blkW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 15, 98, 125, 158, 104, 104, 63, 6, 167,
		232, 60, 231, 70
	})]
	internal virtual void getLuma22(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20NoRound(pic, picW, tmp1, 0, blkW, x, y - 2, blkW, blkH + 7);
		getLuma02NoRoundInt(tmp1, blkW, tmp2, blkOff, blkStride, 0, 2, blkW, blkH);
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 11, 98, 126, 158, 104, 104, 63, 7, 167,
		232, 60, 231, 70
	})]
	internal virtual void getLuma22Unsafe(byte[] pic, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20UnsafeNoRound(pic, picW, imgH, tmp1, 0, blkW, x, y - 2, blkW, blkH + 7);
		getLuma02NoRoundInt(tmp1, blkW, tmp2, blkOff, blkStride, 0, 2, blkW, blkH);
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				blk[blkOff + i] = (byte)(sbyte)MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
			}
			blkOff += blkStride;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 7, 98, 125, 158, 102, 107, 104, 127, 0,
		126, 240, 61, 231, 69, 104, 230, 57, 234, 73
	})]
	internal virtual void getLuma23(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20NoRound(pic, picW, tmp1, 0, blkW, x, y - 2, blkW, blkH + 7);
		getLuma02NoRoundInt(tmp1, blkW, tmp2, blkOff, blkStride, 0, 2, blkW, blkH);
		int off = blkW << 1;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int rounded = MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
				int rounded2 = MathUtil.clip(tmp1[off + i + blkW] + 16 >> 5, -128, 127);
				blk[blkOff + i] = (byte)(sbyte)(rounded + rounded2 + 1 >> 1);
			}
			blkOff += blkStride;
			off += blkW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 2, 98, 126, 158, 102, 107, 104, 127, 0,
		126, 241, 61, 231, 69, 104, 230, 57, 234, 73
	})]
	internal virtual void getLuma23Unsafe(byte[] pic, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20UnsafeNoRound(pic, picW, imgH, tmp1, 0, blkW, x, y - 2, blkW, blkH + 7);
		getLuma02NoRoundInt(tmp1, blkW, tmp2, blkOff, blkStride, 0, 2, blkW, blkH);
		int off = blkW << 1;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int rounded = MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
				int rounded2 = MathUtil.clip(tmp1[off + i + blkW] + 16 >> 5, -128, 127);
				blk[blkOff + i] = (byte)(sbyte)(rounded + rounded2 + 1 >> 1);
			}
			blkOff += blkStride;
			off += blkW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 253, 98, 134, 121, 157, 99, 107, 104, 127,
		1, 123, 241, 61, 231, 69, 104, 229, 57, 234,
		73
	})]
	internal virtual void getLuma12(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int tmpW = blkW + 7;
		getLuma02NoRound(pic, picW, tmp1, 0, tmpW, x - 2, y, tmpW, blkH);
		getLuma20NoRoundInt(tmp1, tmpW, tmp2, blkOff, blkStride, 2, 0, blkW, blkH);
		int off = 2;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int rounded = MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
				int rounded2 = MathUtil.clip(tmp1[off + i] + 16 >> 5, -128, 127);
				blk[blkOff + i] = (byte)(sbyte)(rounded + rounded2 + 1 >> 1);
			}
			blkOff += blkStride;
			off += tmpW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 248, 162, 134, 122, 157, 99, 107, 104, 127,
		1, 123, 242, 61, 231, 69, 104, 229, 57, 234,
		73
	})]
	internal virtual void getLuma12Unsafe(byte[] pic, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int tmpW = blkW + 7;
		getLuma02UnsafeNoRound(pic, picW, imgH, tmp1, 0, tmpW, x - 2, y, tmpW, blkH);
		getLuma20NoRoundInt(tmp1, tmpW, tmp2, blkOff, blkStride, 2, 0, blkW, blkH);
		int off = 2;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int rounded = MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
				int rounded2 = MathUtil.clip(tmp1[off + i] + 16 >> 5, -128, 127);
				blk[blkOff + i] = (byte)(sbyte)(rounded + rounded2 + 1 >> 1);
			}
			blkOff += blkStride;
			off += tmpW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 242, 66, 134, 121, 157, 99, 107, 104, 127,
		1, 125, 241, 61, 231, 69, 104, 229, 57, 234,
		73
	})]
	internal virtual void getLuma32(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int tmpW = blkW + 7;
		getLuma02NoRound(pic, picW, tmp1, 0, tmpW, x - 2, y, tmpW, blkH);
		getLuma20NoRoundInt(tmp1, tmpW, tmp2, blkOff, blkStride, 2, 0, blkW, blkH);
		int off = 2;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int rounded = MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
				int rounded2 = MathUtil.clip(tmp1[off + i + 1] + 16 >> 5, -128, 127);
				blk[blkOff + i] = (byte)(sbyte)(rounded + rounded2 + 1 >> 1);
			}
			blkOff += blkStride;
			off += tmpW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		158, 237, 130, 134, 122, 157, 99, 107, 104, 127,
		1, 125, 242, 61, 231, 69, 104, 229, 57, 234,
		73
	})]
	internal virtual void getLuma32Unsafe(byte[] pic, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		int tmpW = blkW + 7;
		getLuma02UnsafeNoRound(pic, picW, imgH, tmp1, 0, tmpW, x - 2, y, tmpW, blkH);
		getLuma20NoRoundInt(tmp1, tmpW, tmp2, blkOff, blkStride, 2, 0, blkW, blkH);
		int off = 2;
		for (int j = 0; j < blkH; j++)
		{
			for (int i = 0; i < blkW; i++)
			{
				int rounded = MathUtil.clip(tmp2[blkOff + i] + 512 >> 10, -128, 127);
				int rounded2 = MathUtil.clip(tmp1[off + i + 1] + 16 >> 5, -128, 127);
				blk[blkOff + i] = (byte)(sbyte)(rounded + rounded2 + 1 >> 1);
			}
			blkOff += blkStride;
			off += tmpW;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 232, 162, 119, 155, 119 })]
	internal virtual void getLuma33(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20(pic, picW, blk, blkOff, blkStride, x, y + 1, blkW, blkH);
		getLuma02(pic, picW, tmp3, 0, blkW, x + 1, y, blkW, blkH);
		merge(blk, tmp3, blkOff, blkStride, blkW, blkH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 229, 130, 122, 157, 120 })]
	internal virtual void getLuma33Unsafe(byte[] pic, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20Unsafe(pic, picW, imgH, blk, blkOff, blkStride, x, y + 1, blkW, blkH);
		getLuma02Unsafe(pic, picW, imgH, tmp3, 0, blkW, x + 1, y, blkW, blkH);
		merge(blk, tmp3, blkOff, blkStride, blkW, blkH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 226, 66, 117, 153, 119 })]
	internal virtual void getLuma11(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20(pic, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		getLuma02(pic, picW, tmp3, 0, blkW, x, y, blkW, blkH);
		merge(blk, tmp3, blkOff, blkStride, blkW, blkH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 224, 162, 120, 155, 120 })]
	internal virtual void getLuma11Unsafe(byte[] pic, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20Unsafe(pic, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		getLuma02Unsafe(pic, picW, imgH, tmp3, 0, blkW, x, y, blkW, blkH);
		merge(blk, tmp3, blkOff, blkStride, blkW, blkH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 221, 98, 119, 153, 119 })]
	internal virtual void getLuma13(byte[] pic, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20(pic, picW, blk, blkOff, blkStride, x, y + 1, blkW, blkH);
		getLuma02(pic, picW, tmp3, 0, blkW, x, y, blkW, blkH);
		merge(blk, tmp3, blkOff, blkStride, blkW, blkH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 218, 66, 122, 155, 120 })]
	internal virtual void getLuma13Unsafe(byte[] pic, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20Unsafe(pic, picW, imgH, blk, blkOff, blkStride, x, y + 1, blkW, blkH);
		getLuma02Unsafe(pic, picW, imgH, tmp3, 0, blkW, x, y, blkW, blkH);
		merge(blk, tmp3, blkOff, blkStride, blkW, blkH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 216, 162, 117, 155, 119 })]
	internal virtual void getLuma31(byte[] pels, int picW, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20(pels, picW, blk, blkOff, blkStride, x, y, blkW, blkH);
		getLuma02(pels, picW, tmp3, 0, blkW, x + 1, y, blkW, blkH);
		merge(blk, tmp3, blkOff, blkStride, blkW, blkH);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 158, 213, 130, 120, 157, 120 })]
	internal virtual void getLuma31Unsafe(byte[] pels, int picW, int imgH, byte[] blk, int blkOff, int blkStride, int x, int y, int blkW, int blkH)
	{
		getLuma20Unsafe(pels, picW, imgH, blk, blkOff, blkStride, x, y, blkW, blkH);
		getLuma02Unsafe(pels, picW, imgH, tmp3, 0, blkW, x + 1, y, blkW, blkH);
		merge(blk, tmp3, blkOff, blkStride, blkW, blkH);
	}
}
