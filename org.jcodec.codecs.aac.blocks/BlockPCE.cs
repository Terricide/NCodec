using System;
using System.Runtime.CompilerServices;
using IKVM.Attributes;
using IKVM.Runtime;
using java.lang;
using org.jcodec.common.io;

namespace org.jcodec.codecs.aac.blocks;

public class BlockPCE : Block
{
	[SpecialName]
	[InnerClass(null, Modifiers.Static | Modifiers.Synthetic)]
	[EnclosingMethod(null, null, null)]
	[Modifiers(Modifiers.Super | Modifiers.Synthetic)]
	internal class _1 : java.lang.Object
	{
		[Modifiers(Modifiers.Static | Modifiers.Final | Modifiers.Synthetic)]
		internal static int[] _0024SwitchMap_0024org_0024jcodec_0024codecs_0024aac_0024ChannelPosition;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[SpecialName]
		public static void ___003Cclinit_003E()
		{
		}

		[LineNumberTable(87)]
		static _1()
		{
			_0024SwitchMap_0024org_0024jcodec_0024codecs_0024aac_0024ChannelPosition = new int[(nint)ChannelPosition.values().LongLength];
			NoSuchFieldError noSuchFieldError2;
			try
			{
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024aac_0024ChannelPosition[ChannelPosition.___003C_003EAAC_CHANNEL_FRONT.ordinal()] = 1;
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
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024aac_0024ChannelPosition[ChannelPosition.___003C_003EAAC_CHANNEL_BACK.ordinal()] = 2;
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
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024aac_0024ChannelPosition[ChannelPosition.___003C_003EAAC_CHANNEL_SIDE.ordinal()] = 3;
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
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024aac_0024ChannelPosition[ChannelPosition.___003C_003EAAC_CHANNEL_CC.ordinal()] = 4;
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
				_0024SwitchMap_0024org_0024jcodec_0024codecs_0024aac_0024ChannelPosition[ChannelPosition.___003C_003EAAC_CHANNEL_LFE.ordinal()] = 5;
				return;
			}
			catch (System.Exception x5)
			{
				NoSuchFieldError noSuchFieldError13 = ByteCodeHelper.MapException<NoSuchFieldError>(x5, ByteCodeHelper.MapFlags.None);
				if (noSuchFieldError13 == null)
				{
					throw;
				}
				noSuchFieldError14 = noSuchFieldError13;
			}
			NoSuchFieldError noSuchFieldError15 = noSuchFieldError14;
		}

		_1()
		{
			throw null;
		}
	}

	public class ChannelMapping : java.lang.Object
	{
		internal RawDataBlockType syn_ele;

		internal int someInt;

		internal ChannelPosition position;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(20)]
		public ChannelMapping()
		{
		}
	}

	private const int MAX_ELEM_ID = 16;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 121, 98, 111, 99, 223, 11, 111, 131, 105,
		103, 131, 167, 106, 113, 106, 102, 102
	})]
	private void decodeChannelMap(ChannelMapping[] layout_map, int offset, ChannelPosition type, BitReader _in, int n)
	{
		while (true)
		{
			int num = n;
			n += -1;
			if (num > 0)
			{
				RawDataBlockType syn_ele = null;
				switch (_1._0024SwitchMap_0024org_0024jcodec_0024codecs_0024aac_0024ChannelPosition[type.ordinal()])
				{
				case 1:
				case 2:
				case 3:
					syn_ele = RawDataBlockType.values()[_in.read1Bit()];
					break;
				case 4:
					_in.read1Bit();
					syn_ele = RawDataBlockType.___003C_003ETYPE_CCE;
					break;
				case 5:
					syn_ele = RawDataBlockType.___003C_003ETYPE_LFE;
					break;
				}
				layout_map[offset].syn_ele = syn_ele;
				layout_map[offset].someInt = _in.readNBit(4);
				layout_map[offset].position = type;
				offset++;
				continue;
			}
			break;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(16)]
	public BlockPCE()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 135, 66, 137, 137, 105, 105, 105, 106, 106,
		138, 105, 105, 105, 137, 105, 233, 70, 138, 100,
		114, 100, 114, 103, 114, 103, 115, 136, 140, 115,
		136, 168, 204, 106
	})]
	public override void parse(BitReader _in)
	{
		_in.readNBit(2);
		int samplingIndex = _in.readNBit(4);
		int num_front = _in.readNBit(4);
		int num_side = _in.readNBit(4);
		int num_back = _in.readNBit(4);
		int num_lfe = _in.readNBit(2);
		int num_assoc_data = _in.readNBit(3);
		int num_cc = _in.readNBit(4);
		if (_in.read1Bit() != 0)
		{
			_in.readNBit(4);
		}
		if (_in.read1Bit() != 0)
		{
			_in.readNBit(4);
		}
		if (_in.read1Bit() != 0)
		{
			_in.readNBit(3);
		}
		ChannelMapping[] layout_map = new ChannelMapping[64];
		int tags = 0;
		decodeChannelMap(layout_map, tags, ChannelPosition.___003C_003EAAC_CHANNEL_FRONT, _in, num_front);
		tags = num_front;
		decodeChannelMap(layout_map, tags, ChannelPosition.___003C_003EAAC_CHANNEL_SIDE, _in, num_side);
		tags += num_side;
		decodeChannelMap(layout_map, tags, ChannelPosition.___003C_003EAAC_CHANNEL_BACK, _in, num_back);
		tags += num_back;
		decodeChannelMap(layout_map, tags, ChannelPosition.___003C_003EAAC_CHANNEL_LFE, _in, num_lfe);
		tags += num_lfe;
		_in.skip(4 * num_assoc_data);
		decodeChannelMap(layout_map, tags, ChannelPosition.___003C_003EAAC_CHANNEL_CC, _in, num_cc);
		tags += num_cc;
		_in.align();
		int comment_len = _in.readNBit(8) * 8;
		_in.skip(comment_len);
	}
}
