using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.io;
using java.lang;
using org.jcodec.common.model;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class SetPAR : Object
{
	[SpecialName]
	[EnclosingMethod(null, "main1", "([Ljava.lang.String;)V")]
	internal class _1 : Object, MP4Edit
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal Rational val_0024newPAR;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(34)]
		internal _1(Rational r) : base()
		{
			val_0024newPAR = r;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 133, 130, 104, 109, 127, 2, 108, 114, 104,
			104, 105, 159, 5, 143, 110, 186
		})]
		public virtual void apply(MovieBox mov)
		{
			TrakBox vt = mov.getVideoTrack();
			vt.setPAR(val_0024newPAR);
			Box box = (Box)((SampleDescriptionBox)NodeBox.findFirstPath(vt, ClassLiteral<SampleDescriptionBox>.Value, Box.path("mdia.minf.stbl.stsd"))).getBoxes().get(0);
			if (box != null && box is VideoSampleEntry)
			{
				VideoSampleEntry vs = (VideoSampleEntry)box;
				int codedWidth = vs.getWidth();
				int codedHeight = vs.getHeight();
				int num = codedWidth * val_0024newPAR.getNum();
				int den = val_0024newPAR.getDen();
				int displayWidth = ((den != -1) ? (num / den) : (-num));
				vt.getTrackHeader().setWidth(displayWidth);
				if (BoxUtil.containsBox(vt, "tapt"))
				{
					vt.setAperture(new Size(codedWidth, codedHeight), new Size(displayWidth, codedHeight));
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(58)]
		public virtual void applyToFragment(MovieBox mov, MovieFragmentBox[] fragmentBox)
		{
			throw new RuntimeException("Unsupported");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(26)]
	public SetPAR()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.lang.Exception" })]
	[LineNumberTable(new byte[]
	{
		159,
		135,
		66,
		102,
		112,
		135,
		138,
		byte.MaxValue,
		0,
		91
	})]
	public static void main1(string[] args)
	{
		if ((nint)args.LongLength < 2)
		{
			java.lang.System.@out.println("Syntax: setpasp <movie> <num:den>");
			java.lang.System.exit(-1);
		}
		Rational newPAR = Rational.parse(args[1]);
		InplaceMP4Editor inplaceMP4Editor = new InplaceMP4Editor();
		inplaceMP4Editor.modify(new File(args[0]), new _1(newPAR));
	}
}
