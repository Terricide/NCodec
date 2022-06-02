using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using java.nio;
using java.util;
using java.util.concurrent;
using org.jcodec.codecs.h264.decode;
using org.jcodec.codecs.h264.decode.deblock;
using org.jcodec.codecs.h264.io.model;
using org.jcodec.common;
using org.jcodec.common.io;
using org.jcodec.common.logging;
using org.jcodec.common.model;
using org.jcodec.common.tools;

namespace org.jcodec.codecs.h264;

public class H264Decoder : VideoDecoder
{
	[SpecialName]
	[EnclosingMethod(null, "<init>", "()V")]
	internal class _1 : java.lang.Object, ThreadFactory
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal H264Decoder this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(67)]
		internal _1(H264Decoder this_00240) : base()
		{
			this.this_00240 = this_00240;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 125, 98, 109, 104 })]
		public virtual Thread newThread(Runnable r)
		{
			Thread t = Executors.defaultThreadFactory().newThread(r);
			t.setDaemon(on: true);
			return t;
		}
	}

	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _2 : java.lang.Object
	{
		[Modifiers(Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		internal static int[] _0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[LineNumberTable(272)]
		static _2()
		{
			_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType = new int[(nint)RefPicMarking.InstrType.values().LongLength];
			NoSuchFieldError noSuchFieldError2;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003EREMOVE_SHORT.ordinal()] = 1;
			}
			catch (System.Exception x)
			{
				NoSuchFieldError noSuchFieldError = ByteCodeHelper.MapException<NoSuchFieldError>(x, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError == null)
				{
					throw;
				}
				noSuchFieldError2 = noSuchFieldError;
				goto IL_0037;
			}
			goto IL_003d;
			IL_0037:
			NoSuchFieldError noSuchFieldError3 = noSuchFieldError2;
			goto IL_003d;
			IL_003d:
			NoSuchFieldError noSuchFieldError5;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003EREMOVE_LONG.ordinal()] = 2;
			}
			catch (System.Exception x2)
			{
				NoSuchFieldError noSuchFieldError4 = ByteCodeHelper.MapException<NoSuchFieldError>(x2, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError4 == null)
				{
					throw;
				}
				noSuchFieldError5 = noSuchFieldError4;
				goto IL_0062;
			}
			goto IL_0068;
			IL_0062:
			NoSuchFieldError noSuchFieldError6 = noSuchFieldError5;
			goto IL_0068;
			IL_0068:
			NoSuchFieldError noSuchFieldError8;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003ECONVERT_INTO_LONG.ordinal()] = 3;
			}
			catch (System.Exception x3)
			{
				NoSuchFieldError noSuchFieldError7 = ByteCodeHelper.MapException<NoSuchFieldError>(x3, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError7 == null)
				{
					throw;
				}
				noSuchFieldError8 = noSuchFieldError7;
				goto IL_008e;
			}
			goto IL_0096;
			IL_008e:
			NoSuchFieldError noSuchFieldError9 = noSuchFieldError8;
			goto IL_0096;
			IL_0096:
			NoSuchFieldError noSuchFieldError11;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003ETRUNK_LONG.ordinal()] = 4;
			}
			catch (System.Exception x4)
			{
				NoSuchFieldError noSuchFieldError10 = ByteCodeHelper.MapException<NoSuchFieldError>(x4, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError10 == null)
				{
					throw;
				}
				noSuchFieldError11 = noSuchFieldError10;
				goto IL_00bc;
			}
			goto IL_00c4;
			IL_00bc:
			NoSuchFieldError noSuchFieldError12 = noSuchFieldError11;
			goto IL_00c4;
			IL_00c4:
			NoSuchFieldError noSuchFieldError14;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003ECLEAR.ordinal()] = 5;
			}
			catch (System.Exception x5)
			{
				NoSuchFieldError noSuchFieldError13 = ByteCodeHelper.MapException<NoSuchFieldError>(x5, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError13 == null)
				{
					throw;
				}
				noSuchFieldError14 = noSuchFieldError13;
				goto IL_00ea;
			}
			goto IL_00f2;
			IL_00ea:
			NoSuchFieldError noSuchFieldError15 = noSuchFieldError14;
			goto IL_00f2;
			IL_00f2:
			NoSuchFieldError noSuchFieldError17;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[RefPicMarking.InstrType.___003C_003EMARK_LONG.ordinal()] = 6;
				return;
			}
			catch (System.Exception x6)
			{
				NoSuchFieldError noSuchFieldError16 = ByteCodeHelper.MapException<NoSuchFieldError>(x6, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError16 == null)
				{
					throw;
				}
				noSuchFieldError17 = noSuchFieldError16;
			}
			NoSuchFieldError noSuchFieldError18 = noSuchFieldError17;
		}

		_2()
		{
			throw null;
		}
	}

	internal class FrameDecoder : java.lang.Object
	{
		private SeqParameterSet activeSps;

		private DeblockingFilter filter;

		private SliceHeader firstSliceHeader;

		private NALUnit firstNu;

		private H264Decoder dec;

		private DeblockerInput di;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 109, 66, 105, 104 })]
		public FrameDecoder(H264Decoder decoder)
		{
			dec = decoder;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;[[B)Lorg/jcodec/codecs/h264/io/model/Frame;")]
		[LineNumberTable(new byte[]
		{
			159, 108, 98, 115, 108, 99, 117, 125, 103, 125,
			127, 3, 131, 127, 1, 105, 131, 99, 127, 1,
			127, 17, 163, 141, 136
		})]
		public virtual org.jcodec.codecs.h264.io.model.Frame decodeFrame(List nalUnits, byte[][] buffer)
		{
			List sliceReaders = access_0024500(dec).readFrame(nalUnits);
			if (sliceReaders == null || sliceReaders.size() == 0)
			{
				return null;
			}
			org.jcodec.codecs.h264.io.model.Frame result = init((SliceReader)sliceReaders.get(0), buffer);
			if (access_0024600(dec) && sliceReaders.size() > 1)
			{
				ArrayList futures = new ArrayList();
				Iterator iterator = sliceReaders.iterator();
				while (iterator.hasNext())
				{
					SliceReader sliceReader2 = (SliceReader)iterator.next();
					((List)futures).add((object)access_0024800(dec).submit(new SliceDecoderRunnable(this, sliceReader2, result, null)));
				}
				Iterator iterator2 = ((List)futures).iterator();
				while (iterator2.hasNext())
				{
					Future future = (Future)iterator2.next();
					waitForSure(future);
				}
			}
			else
			{
				Iterator iterator3 = sliceReaders.iterator();
				while (iterator3.hasNext())
				{
					SliceReader sliceReader = (SliceReader)iterator3.next();
					new SliceDecoder(activeSps, access_0024200(dec), access_0024300(dec), di, result).decodeFromReader(sliceReader);
				}
			}
			filter.deblockFrame(result);
			updateReferences(result);
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 96, 130, 141, 109, 146, 157, 143, 110, 127,
			10, 178, 146, 127, 27, 55, 167, 159, 6
		})]
		private org.jcodec.codecs.h264.io.model.Frame init(SliceReader sliceReader, byte[][] buffer)
		{
			firstNu = sliceReader.getNALUnit();
			firstSliceHeader = sliceReader.getSliceHeader();
			activeSps = firstSliceHeader.sps;
			validateSupportedFeatures(firstSliceHeader.sps, firstSliceHeader.pps);
			int picWidthInMbs = activeSps.picWidthInMbsMinus1 + 1;
			if (access_0024200(dec) == null)
			{
				access_0024202(dec, new org.jcodec.codecs.h264.io.model.Frame[1 << firstSliceHeader.sps.log2MaxFrameNumMinus4 + 4]);
				access_0024302(dec, new IntObjectMap());
			}
			di = new DeblockerInput(activeSps);
			org.jcodec.codecs.h264.io.model.Frame result = createFrame(activeSps, buffer, firstSliceHeader.frameNum, firstSliceHeader.sliceType, di.mvs, di.refsUsed, access_0024900(dec).calcPOC(firstSliceHeader, firstNu));
			DeblockingFilter.___003Cclinit_003E();
			filter = new DeblockingFilter(picWidthInMbs, activeSps.bitDepthChromaMinus8 + 8, di);
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Signature("(Ljava/util/concurrent/Future<*>;)V")]
		[LineNumberTable(new byte[] { 159, 101, 162, 127, 0, 99, 98, 173 })]
		private void waitForSure(Future future)
		{
			java.lang.Exception ex2;
			try
			{
				future.get();
				return;
			}
			catch (System.Exception x)
			{
				java.lang.Exception ex = ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.None);
				if (ex == null)
				{
					throw;
				}
				ex2 = ex;
			}
			java.lang.Exception e = ex2;
			throw new RuntimeException(e);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 98, 66, 110, 115, 149, 181 })]
		private void updateReferences(org.jcodec.codecs.h264.io.model.Frame picture)
		{
			if (firstNu.nal_ref_idc != 0)
			{
				if (firstNu.type == NALUnitType.___003C_003EIDR_SLICE)
				{
					performIDRMarking(firstSliceHeader.refPicMarkingIDR, picture);
				}
				else
				{
					performMarking(firstSliceHeader.refPicMarkingNonIDR, picture);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 85, 130, 103, 145, 105, 105, 115, 138, 121 })]
		public virtual void performIDRMarking(RefPicMarkingIDR refPicMarkingIDR, org.jcodec.codecs.h264.io.model.Frame picture)
		{
			clearAll();
			access_00241000(dec).clear();
			org.jcodec.codecs.h264.io.model.Frame saved = saveRef(picture);
			if (refPicMarkingIDR.isUseForlongTerm())
			{
				access_0024300(dec).put(0, saved);
				saved.setShortTerm(shortTerm: false);
			}
			else
			{
				access_0024200(dec)[firstSliceHeader.frameNum] = saved;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 76, 130, 137, 103, 104, 107, 101, 159, 23,
			109, 131, 109, 131, 115, 131, 111, 131, 103, 131,
			110, 227, 44, 234, 88, 100, 136, 117, 103, 127,
			6, 110, 119, 113, 127, 10, 103, 101, 150, 231,
			57, 236, 74, 103, 117, 176
		})]
		public virtual void performMarking(RefPicMarking refPicMarking, org.jcodec.codecs.h264.io.model.Frame picture)
		{
			org.jcodec.codecs.h264.io.model.Frame saved = saveRef(picture);
			if (refPicMarking != null)
			{
				RefPicMarking.Instruction[] instructions = refPicMarking.getInstructions();
				for (int j = 0; j < (nint)instructions.LongLength; j++)
				{
					RefPicMarking.Instruction instr = instructions[j];
					switch (_2._0024SwitchMap_0024org_0024jcodec_0024codecs_0024h264_0024io_0024model_0024RefPicMarking_0024InstrType[instr.getType().ordinal()])
					{
					case 1:
						unrefShortTerm(instr.getArg1());
						break;
					case 2:
						unrefLongTerm(instr.getArg1());
						break;
					case 3:
						convert(instr.getArg1(), instr.getArg2());
						break;
					case 4:
						truncateLongTerm(instr.getArg1() - 1);
						break;
					case 5:
						clearAll();
						break;
					case 6:
						saveLong(saved, instr.getArg1());
						saved = null;
						break;
					}
				}
			}
			if (saved != null)
			{
				saveShort(saved);
			}
			int maxFrames = 1 << activeSps.log2MaxFrameNumMinus4 + 4;
			if (refPicMarking != null)
			{
				return;
			}
			int maxShort = java.lang.Math.max(1, activeSps.numRefFrames - access_0024300(dec).size());
			int min = int.MaxValue;
			int num = 0;
			int minFn = 0;
			for (int i = 0; i < (nint)access_0024200(dec).LongLength; i++)
			{
				if (access_0024200(dec)[i] != null)
				{
					int fnWrap = unwrap(firstSliceHeader.frameNum, access_0024200(dec)[i].getFrameNo(), maxFrames);
					if (fnWrap < min)
					{
						min = fnWrap;
						minFn = access_0024200(dec)[i].getFrameNo();
					}
					num++;
				}
			}
			if (num > maxShort)
			{
				releaseRef(access_0024200(dec)[minFn]);
				access_0024200(dec)[minFn] = null;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 90, 162, 105, 113, 113, 113, 110, 127, 22,
			113, 113, 105, 177, 105, 113, 127, 0, 127, 22
		})]
		private void validateSupportedFeatures(SeqParameterSet sps, PictureParameterSet pps)
		{
			if (sps.mbAdaptiveFrameFieldFlag)
			{
				throw new RuntimeException("Unsupported h264 feature: MBAFF.");
			}
			if (sps.bitDepthLumaMinus8 != 0 || sps.bitDepthChromaMinus8 != 0)
			{
				throw new RuntimeException("Unsupported h264 feature: High bit depth.");
			}
			if (sps.chromaFormatIdc != ColorSpace.___003C_003EYUV420J)
			{
				string message = new StringBuilder().append("Unsupported h264 feature: ").append(sps.chromaFormatIdc).append(" color.")
					.toString();
				throw new RuntimeException(message);
			}
			if (!sps.frameMbsOnlyFlag || sps.fieldPicFlag)
			{
				throw new RuntimeException("Unsupported h264 feature: interlace.");
			}
			if (pps.constrainedIntraPredFlag)
			{
				throw new RuntimeException("Unsupported h264 feature: constrained intra prediction.");
			}
			if (sps.qpprimeYZeroTransformBypassFlag)
			{
				throw new RuntimeException("Unsupported h264 feature: qprime zero transform bypass.");
			}
			if (sps.profileIdc != 66 && sps.profileIdc != 77 && sps.profileIdc != 100)
			{
				string message2 = new StringBuilder().append("Unsupported h264 feature: ").append(sps.profileIdc).append(" profile.")
					.toString();
				throw new RuntimeException(message2);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 79, 130, 114, 116, 15, 199, 114, 104, 63,
			0, 167, 115
		})]
		public virtual void clearAll()
		{
			for (int j = 0; j < (nint)access_0024200(dec).LongLength; j++)
			{
				releaseRef(access_0024200(dec)[j]);
				access_0024200(dec)[j] = null;
			}
			int[] keys = access_0024300(dec).keys();
			for (int i = 0; i < (nint)keys.LongLength; i++)
			{
				releaseRef((org.jcodec.codecs.h264.io.model.Frame)access_0024300(dec).get(keys[i]));
			}
			access_0024300(dec).clear();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 82, 130, 127, 20, 104 })]
		private org.jcodec.codecs.h264.io.model.Frame saveRef(org.jcodec.codecs.h264.io.model.Frame decoded)
		{
			org.jcodec.codecs.h264.io.model.Frame frame = ((access_00241000(dec).size() <= 0) ? org.jcodec.codecs.h264.io.model.Frame.createFrame(decoded) : ((org.jcodec.codecs.h264.io.model.Frame)access_00241000(dec).remove(0)));
			frame.copyFromFrame(decoded);
			return frame;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 80, 66, 100, 147 })]
		private void releaseRef(org.jcodec.codecs.h264.io.model.Frame picture)
		{
			if (picture != null)
			{
				access_00241000(dec).add(picture);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 52, 66, 159, 12, 116, 111 })]
		private void unrefShortTerm(int shortNo)
		{
			int ind = MathUtil.wrap(firstSliceHeader.frameNum - shortNo, 1 << firstSliceHeader.sps.log2MaxFrameNumMinus4 + 4);
			releaseRef(access_0024200(dec)[ind]);
			access_0024200(dec)[ind] = null;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 54, 162, 125, 116 })]
		private void unrefLongTerm(int longNo)
		{
			releaseRef((org.jcodec.codecs.h264.io.model.Frame)access_0024300(dec).get(longNo));
			access_0024300(dec).remove(longNo);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 56, 130, 159, 12, 125, 127, 0, 111, 127,
			0
		})]
		private void convert(int shortNo, int longNo)
		{
			int ind = MathUtil.wrap(firstSliceHeader.frameNum - shortNo, 1 << firstSliceHeader.sps.log2MaxFrameNumMinus4 + 4);
			releaseRef((org.jcodec.codecs.h264.io.model.Frame)access_0024300(dec).get(longNo));
			access_0024300(dec).put(longNo, access_0024200(dec)[ind]);
			access_0024200(dec)[ind] = null;
			((org.jcodec.codecs.h264.io.model.Frame)access_0024300(dec).get(longNo)).setShortTerm(shortTerm: false);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 58, 66, 114, 104, 103, 127, 0, 244, 61,
			231, 70
		})]
		private void truncateLongTerm(int maxLongNo)
		{
			int[] keys = access_0024300(dec).keys();
			for (int i = 0; i < (nint)keys.LongLength; i++)
			{
				if (keys[i] > maxLongNo)
				{
					releaseRef((org.jcodec.codecs.h264.io.model.Frame)access_0024300(dec).get(keys[i]));
					access_0024300(dec).remove(keys[i]);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 61, 162, 120, 100, 104, 136, 117 })]
		private void saveLong(org.jcodec.codecs.h264.io.model.Frame saved, int longNo)
		{
			org.jcodec.codecs.h264.io.model.Frame prev = (org.jcodec.codecs.h264.io.model.Frame)access_0024300(dec).get(longNo);
			if (prev != null)
			{
				releaseRef(prev);
			}
			saved.setShortTerm(shortTerm: false);
			access_0024300(dec).put(longNo, saved);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 62, 162, 121 })]
		private void saveShort(org.jcodec.codecs.h264.io.model.Frame saved)
		{
			access_0024200(dec)[firstSliceHeader.frameNum] = saved;
		}

		[LineNumberTable(319)]
		private int unwrap(int thisFrameNo, int refFrameNo, int maxFrames)
		{
			return (refFrameNo <= thisFrameNo) ? refFrameNo : (refFrameNo - maxFrames);
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(124)]
		internal static SeqParameterSet access_0024000(FrameDecoder x0)
		{
			return x0.activeSps;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(124)]
		internal static H264Decoder access_0024100(FrameDecoder x0)
		{
			return x0.dec;
		}

		[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
		[LineNumberTable(124)]
		internal static DeblockerInput access_0024400(FrameDecoder x0)
		{
			return x0.di;
		}
	}

	[InnerClass(null, Modifiers.Private | Modifiers.Static | Modifiers.Final)]
	internal class SliceDecoderRunnable : java.lang.Object, Runnable
	{
		[Modifiers(Modifiers.Private | Modifiers.Final)]
		private SliceReader sliceReader;

		[Modifiers(Modifiers.Private | Modifiers.Final)]
		private org.jcodec.codecs.h264.io.model.Frame result;

		private FrameDecoder fdec;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[Modifiers(Modifiers.Synthetic)]
		[LineNumberTable(107)]
		internal SliceDecoderRunnable(FrameDecoder x0, SliceReader x1, org.jcodec.codecs.h264.io.model.Frame x2, _1 x3)
			: this(x0, x1, x2)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 114, 66, 105, 104, 104, 104 })]
		private SliceDecoderRunnable(FrameDecoder fdec, SliceReader sliceReader, org.jcodec.codecs.h264.io.model.Frame result)
		{
			this.fdec = fdec;
			this.sliceReader = sliceReader;
			this.result = result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 113, 162, 127, 41, 104 })]
		public virtual void run()
		{
			new SliceDecoder(FrameDecoder.access_0024000(fdec), access_0024200(FrameDecoder.access_0024100(fdec)), access_0024300(FrameDecoder.access_0024100(fdec)), FrameDecoder.access_0024400(fdec), result).decodeFromReader(sliceReader);
		}
	}

	private org.jcodec.codecs.h264.io.model.Frame[] sRefs;

	[Signature("Lorg/jcodec/common/IntObjectMap<Lorg/jcodec/codecs/h264/io/model/Frame;>;")]
	private IntObjectMap lRefs;

	[Signature("Ljava/util/List<Lorg/jcodec/codecs/h264/io/model/Frame;>;")]
	private List pictureBuffer;

	private POCManager poc;

	private FrameReader reader;

	private ExecutorService tp;

	private bool threaded;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(100)]
	public new virtual org.jcodec.codecs.h264.io.model.Frame decodeFrame(ByteBuffer data, byte[][] buffer)
	{
		org.jcodec.codecs.h264.io.model.Frame result = decodeFrameFromNals(H264Utils.splitFrame(data), buffer);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 130, 103, 127, 7, 104, 110, 111, 110,
		141, 102
	})]
	public static H264Decoder createH264DecoderFromCodecPrivate(ByteBuffer codecPrivate)
	{
		H264Decoder d = new H264Decoder();
		Iterator iterator = H264Utils.splitFrame(codecPrivate.duplicate()).iterator();
		while (iterator.hasNext())
		{
			ByteBuffer bb = (ByteBuffer)iterator.next();
			NALUnit nu = NALUnit.read(bb);
			if (nu.type == NALUnitType.___003C_003ESPS)
			{
				d.reader.addSps(bb);
			}
			else if (nu.type == NALUnitType.___003C_003EPPS)
			{
				d.reader.addPps(bb);
			}
		}
		return d;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 127, 130, 105, 108, 108, 116, 105, 252, 72,
		108
	})]
	public H264Decoder()
	{
		pictureBuffer = new ArrayList();
		poc = new POCManager();
		threaded = Runtime.getRuntime().availableProcessors() > 1;
		if (threaded)
		{
			tp = Executors.newFixedThreadPool(Runtime.getRuntime().availableProcessors(), new _1(this));
		}
		reader = new FrameReader();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;[[B)Lorg/jcodec/codecs/h264/io/model/Frame;")]
	[LineNumberTable(104)]
	public virtual org.jcodec.codecs.h264.io.model.Frame decodeFrameFromNals(List nalUnits, byte[][] buffer)
	{
		org.jcodec.codecs.h264.io.model.Frame result = new FrameDecoder(this).decodeFrame(nalUnits, buffer);
		return result;
	}

	[LineNumberTable(411)]
	private static bool validSh(SliceHeader sh)
	{
		return (sh.firstMbInSlice == 0 && sh.sliceType != null && sh.picParameterSetId < 2) ? true : false;
	}

	[LineNumberTable(415)]
	private static bool validSps(SeqParameterSet sps)
	{
		return (sps.bitDepthChromaMinus8 < 4 && sps.bitDepthLumaMinus8 < 4 && sps.chromaFormatIdc != null && sps.seqParameterSetId < 2 && sps.picOrderCntType <= 2) ? true : false;
	}

	[LineNumberTable(420)]
	private static bool validPps(PictureParameterSet pps)
	{
		return (pps.picInitQpMinus26 <= 26 && pps.seqParameterSetId <= 2 && pps.picParameterSetId <= 2) ? true : false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 50, 98, 108, 138, 99, 105, 106, 107, 111,
		112, 142
	})]
	public static org.jcodec.codecs.h264.io.model.Frame createFrame(SeqParameterSet sps, byte[][] buffer, int frameNum, SliceType frameType, H264Utils.MvList2D mvs, org.jcodec.codecs.h264.io.model.Frame[][][] refsUsed, int POC)
	{
		int width = sps.picWidthInMbsMinus1 + 1 << 4;
		int height = SeqParameterSet.getPicHeightInMbs(sps) << 4;
		Rect crop = null;
		if (sps.frameCroppingFlag)
		{
			int sX = sps.frameCropLeftOffset << 1;
			int sY = sps.frameCropTopOffset << 1;
			int w = width - (sps.frameCropRightOffset << 1) - sX;
			int h = height - (sps.frameCropBottomOffset << 1) - sY;
			crop = new Rect(sX, sY, w, h);
		}
		org.jcodec.codecs.h264.io.model.Frame.___003Cclinit_003E();
		org.jcodec.codecs.h264.io.model.Frame result = new org.jcodec.codecs.h264.io.model.Frame(width, height, buffer, ColorSpace.___003C_003EYUV420, crop, frameNum, frameType, mvs, refsUsed, POC);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[] { 159, 46, 66, 111 })]
	public virtual void addSps(List spsList)
	{
		reader.addSpsList(spsList);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/List<Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[] { 159, 45, 66, 111 })]
	public virtual void addPps(List ppsList)
	{
		reader.addPpsList(ppsList);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 44, 98, 103, 127, 11, 106, 125, 106, 110,
		99, 111, 112, 111, 142, 134
	})]
	public static int probe(ByteBuffer data)
	{
		int validSps = 0;
		int validPps = 0;
		int validSh = 0;
		Iterator iterator = H264Utils.splitFrame(data.duplicate()).iterator();
		while (iterator.hasNext())
		{
			ByteBuffer nalUnit = (ByteBuffer)iterator.next();
			NALUnit marker = NALUnit.read(nalUnit);
			if (marker.type == NALUnitType.___003C_003EIDR_SLICE || marker.type == NALUnitType.___003C_003ENON_IDR_SLICE)
			{
				BitReader reader = BitReader.createBitReader(nalUnit);
				validSh = (H264Decoder.validSh(SliceHeaderReader.readPart1(reader)) ? 1 : 0);
				break;
			}
			if (marker.type == NALUnitType.___003C_003ESPS)
			{
				validSps = (H264Decoder.validSps(SeqParameterSet.read(nalUnit)) ? 1 : 0);
			}
			else if (marker.type == NALUnitType.___003C_003EPPS)
			{
				validPps = (H264Decoder.validPps(PictureParameterSet.read(nalUnit)) ? 1 : 0);
			}
		}
		return ((validSh != 0) ? 60 : 0) + ((validSps != 0) ? 20 : 0) + ((validPps != 0) ? 20 : 0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 36, 98, 109, 109, 105, 107, 131, 115, 136 })]
	public override VideoCodecMeta getCodecMeta(ByteBuffer data)
	{
		List rawSPS = H264Utils.getRawSPS(data.duplicate());
		List rawPPS = H264Utils.getRawPPS(data.duplicate());
		if (rawSPS.size() == 0)
		{
			Logger.warn("Can not extract metadata from the packet not containing an SPS.");
			return null;
		}
		SeqParameterSet sps = SeqParameterSet.read((ByteBuffer)rawSPS.get(0));
		Size size = H264Utils.getPicSize(sps);
		VideoCodecMeta result = VideoCodecMeta.createSimpleVideoCodecMeta(size, ColorSpace.___003C_003EYUV420);
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Modifiers(Modifiers.Public | Modifiers.Volatile | Modifiers.Synthetic)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[LineNumberTable(52)]
	public virtual Picture _003Cbridge_003EdecodeFrame(ByteBuffer bb, byte[][] barr)
	{
		org.jcodec.codecs.h264.io.model.Frame result = decodeFrame(bb, barr);
		return result;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(52)]
	internal static org.jcodec.codecs.h264.io.model.Frame[] access_0024200(H264Decoder x0)
	{
		return x0.sRefs;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(52)]
	internal static IntObjectMap access_0024300(H264Decoder x0)
	{
		return x0.lRefs;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(52)]
	internal static FrameReader access_0024500(H264Decoder x0)
	{
		return x0.reader;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(52)]
	internal static bool access_0024600(H264Decoder x0)
	{
		return x0.threaded;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(52)]
	internal static ExecutorService access_0024800(H264Decoder x0)
	{
		return x0.tp;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(52)]
	internal static org.jcodec.codecs.h264.io.model.Frame[] access_0024202(H264Decoder x0, org.jcodec.codecs.h264.io.model.Frame[] x1)
	{
		x0.sRefs = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(52)]
	internal static IntObjectMap access_0024302(H264Decoder x0, IntObjectMap x1)
	{
		x0.lRefs = x1;
		return x1;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(52)]
	internal static POCManager access_0024900(H264Decoder x0)
	{
		return x0.poc;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(52)]
	internal static List access_00241000(H264Decoder x0)
	{
		return x0.pictureBuffer;
	}
}
