using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.nio;
using java.util;
using org.jcodec.common.logging;
using org.jcodec.common.model;

namespace org.jcodec.containers.mxf.model;

public class GenericPictureEssenceDescriptor : FileDescriptor
{
	[Serializable]
	[InnerClass(null, Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
	[Signature("Ljava/lang/Enum<Lorg/jcodec/containers/mxf/model/GenericPictureEssenceDescriptor$LayoutType;>;")]
	[Modifiers(Modifiers.Public | Modifiers.Final | Modifiers.Super | Modifiers.Enum)]
	public sealed class LayoutType : java.lang.Enum
	{
		[Serializable]
		[HideFromJava]
		public enum __Enum
		{
			FullFrame,
			SeparateFields,
			OneField,
			MixedFields,
			SegmentedFrame
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static LayoutType ___003C_003EFullFrame;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static LayoutType ___003C_003ESeparateFields;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static LayoutType ___003C_003EOneField;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static LayoutType ___003C_003EMixedFields;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		internal static LayoutType ___003C_003ESegmentedFrame;

		[Modifiers(Modifiers.Private | Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		private static LayoutType[] _0024VALUES;

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static LayoutType FullFrame
		{
			[HideFromJava]
			get
			{
				return ___003C_003EFullFrame;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static LayoutType SeparateFields
		{
			[HideFromJava]
			get
			{
				return ___003C_003ESeparateFields;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static LayoutType OneField
		{
			[HideFromJava]
			get
			{
				return ___003C_003EOneField;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static LayoutType MixedFields
		{
			[HideFromJava]
			get
			{
				return ___003C_003EMixedFields;
			}
		}

		[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final | Modifiers.Enum)]
		public static LayoutType SegmentedFrame
		{
			[HideFromJava]
			get
			{
				return ___003C_003ESegmentedFrame;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(20)]
		public static LayoutType[] values()
		{
			return (LayoutType[])_0024VALUES.Clone();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(20)]
		private LayoutType(string str, int i)
			: base(str, i)
		{
			GC.KeepAlive(this);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(20)]
		public static LayoutType valueOf(string name)
		{
			return (LayoutType)java.lang.Enum.valueOf(ClassLiteral<LayoutType>.Value, name);
		}

		[LineNumberTable(new byte[] { 159, 137, 98, 63, 50 })]
		static LayoutType()
		{
			___003C_003EFullFrame = new LayoutType("FullFrame", 0);
			___003C_003ESeparateFields = new LayoutType("SeparateFields", 1);
			___003C_003EOneField = new LayoutType("OneField", 2);
			___003C_003EMixedFields = new LayoutType("MixedFields", 3);
			___003C_003ESegmentedFrame = new LayoutType("SegmentedFrame", 4);
			_0024VALUES = new LayoutType[5] { ___003C_003EFullFrame, ___003C_003ESeparateFields, ___003C_003EOneField, ___003C_003EMixedFields, ___003C_003ESegmentedFrame };
		}
	}

	private byte signalStandard;

	private LayoutType frameLayout;

	private int storedWidth;

	private int storedHeight;

	private int storedF2Offset;

	private int sampledWidth;

	private int sampledHeight;

	private int sampledXOffset;

	private int sampledYOffset;

	private int displayHeight;

	private int displayWidth;

	private int displayXOffset;

	private int displayYOffset;

	private int displayF2Offset;

	private Rational aspectRatio;

	private byte activeFormatDescriptor;

	private int[] videoLineMap;

	private byte alphaTransparency;

	private UL transferCharacteristic;

	private int imageAlignmentOffset;

	private int imageStartOffset;

	private int imageEndOffset;

	private byte fieldDominance;

	private UL pictureEssenceCoding;

	private UL codingEquations;

	private UL colorPrimaries;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 129, 66, 106 })]
	public GenericPictureEssenceDescriptor(UL ul)
		: base(ul)
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/util/Map<Ljava/lang/Integer;Ljava/nio/ByteBuffer;>;)V")]
	[LineNumberTable(new byte[]
	{
		159, 128, 98, 136, 120, 141, 141, 159, 106, 110,
		134, 116, 134, 109, 134, 109, 134, 109, 134, 109,
		134, 109, 134, 109, 134, 109, 134, 109, 134, 109,
		134, 109, 134, 109, 134, 109, 134, 125, 134, 110,
		134, 109, 134, 110, 134, 109, 134, 109, 134, 109,
		134, 109, 134, 110, 134, 109, 131, 109, 131, 109,
		131, 127, 36, 134, 103, 102
	})]
	protected internal override void read(Map tags)
	{
		base.read(tags);
		Iterator it = tags.entrySet().iterator();
		while (it.hasNext())
		{
			Map.Entry entry = (Map.Entry)it.next();
			ByteBuffer _bb = (ByteBuffer)entry.getValue();
			switch (((Integer)entry.getKey()).intValue())
			{
			case 12821:
				signalStandard = (byte)(sbyte)_bb.get();
				break;
			case 12812:
				frameLayout = LayoutType.values()[(sbyte)_bb.get()];
				break;
			case 12803:
				storedWidth = _bb.getInt();
				break;
			case 12802:
				storedHeight = _bb.getInt();
				break;
			case 12822:
				storedF2Offset = _bb.getInt();
				break;
			case 12805:
				sampledWidth = _bb.getInt();
				break;
			case 12804:
				sampledHeight = _bb.getInt();
				break;
			case 12806:
				sampledXOffset = _bb.getInt();
				break;
			case 12807:
				sampledYOffset = _bb.getInt();
				break;
			case 12808:
				displayHeight = _bb.getInt();
				break;
			case 12809:
				displayWidth = _bb.getInt();
				break;
			case 12810:
				displayXOffset = _bb.getInt();
				break;
			case 12811:
				displayYOffset = _bb.getInt();
				break;
			case 12823:
				displayF2Offset = _bb.getInt();
				break;
			case 12814:
				Rational.___003Cclinit_003E();
				aspectRatio = new Rational(_bb.getInt(), _bb.getInt());
				break;
			case 12824:
				activeFormatDescriptor = (byte)(sbyte)_bb.get();
				break;
			case 12813:
				videoLineMap = MXFMetadata.readInt32Batch(_bb);
				break;
			case 12815:
				alphaTransparency = (byte)(sbyte)_bb.get();
				break;
			case 12816:
				transferCharacteristic = UL.read(_bb);
				break;
			case 12817:
				imageAlignmentOffset = _bb.getInt();
				break;
			case 12819:
				imageStartOffset = _bb.getInt();
				break;
			case 12820:
				imageEndOffset = _bb.getInt();
				break;
			case 12818:
				fieldDominance = (byte)(sbyte)_bb.get();
				break;
			case 12801:
				pictureEssenceCoding = UL.read(_bb);
				break;
			case 12826:
				codingEquations = UL.read(_bb);
				break;
			case 12825:
				colorPrimaries = UL.read(_bb);
				break;
			default:
				Logger.warn(java.lang.String.format(new StringBuilder().append("Unknown tag [ ").append(ul).append("]: %04x")
					.toString(), entry.getKey()));
				continue;
			}
			it.remove();
		}
	}

	[LineNumberTable(152)]
	public virtual byte getSignalStandard()
	{
		return (byte)(sbyte)signalStandard;
	}

	[LineNumberTable(156)]
	public virtual LayoutType getFrameLayout()
	{
		return frameLayout;
	}

	[LineNumberTable(160)]
	public virtual int getStoredWidth()
	{
		return storedWidth;
	}

	[LineNumberTable(164)]
	public virtual int getStoredHeight()
	{
		return storedHeight;
	}

	[LineNumberTable(168)]
	public virtual int getStoredF2Offset()
	{
		return storedF2Offset;
	}

	[LineNumberTable(172)]
	public virtual int getSampledWidth()
	{
		return sampledWidth;
	}

	[LineNumberTable(176)]
	public virtual int getSampledHeight()
	{
		return sampledHeight;
	}

	[LineNumberTable(180)]
	public virtual int getSampledXOffset()
	{
		return sampledXOffset;
	}

	[LineNumberTable(184)]
	public virtual int getSampledYOffset()
	{
		return sampledYOffset;
	}

	[LineNumberTable(188)]
	public virtual int getDisplayHeight()
	{
		return displayHeight;
	}

	[LineNumberTable(192)]
	public virtual int getDisplayWidth()
	{
		return displayWidth;
	}

	[LineNumberTable(196)]
	public virtual int getDisplayXOffset()
	{
		return displayXOffset;
	}

	[LineNumberTable(200)]
	public virtual int getDisplayYOffset()
	{
		return displayYOffset;
	}

	[LineNumberTable(204)]
	public virtual int getDisplayF2Offset()
	{
		return displayF2Offset;
	}

	[LineNumberTable(208)]
	public virtual Rational getAspectRatio()
	{
		return aspectRatio;
	}

	[LineNumberTable(212)]
	public virtual byte getActiveFormatDescriptor()
	{
		return (byte)(sbyte)activeFormatDescriptor;
	}

	[LineNumberTable(216)]
	public virtual int[] getVideoLineMap()
	{
		return videoLineMap;
	}

	[LineNumberTable(220)]
	public virtual byte getAlphaTransparency()
	{
		return (byte)(sbyte)alphaTransparency;
	}

	[LineNumberTable(224)]
	public virtual UL getTransferCharacteristic()
	{
		return transferCharacteristic;
	}

	[LineNumberTable(228)]
	public virtual int getImageAlignmentOffset()
	{
		return imageAlignmentOffset;
	}

	[LineNumberTable(232)]
	public virtual int getImageStartOffset()
	{
		return imageStartOffset;
	}

	[LineNumberTable(236)]
	public virtual int getImageEndOffset()
	{
		return imageEndOffset;
	}

	[LineNumberTable(240)]
	public virtual byte getFieldDominance()
	{
		return (byte)(sbyte)fieldDominance;
	}

	[LineNumberTable(244)]
	public virtual UL getPictureEssenceCoding()
	{
		return pictureEssenceCoding;
	}

	[LineNumberTable(248)]
	public virtual UL getCodingEquations()
	{
		return codingEquations;
	}

	[LineNumberTable(252)]
	public virtual UL getColorPrimaries()
	{
		return colorPrimaries;
	}
}
