using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.io;
using java.lang;
using java.util;
using org.jcodec.common;
using org.jcodec.containers.mp4;
using org.jcodec.containers.mp4.boxes;

namespace org.jcodec.movtool;

public class MetadataEditor : Object
{
	[SpecialName]
	[EnclosingMethod(null, "save", "(Z)V")]
	internal class _1 : Object, MP4Edit
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal MetadataEditor val_0024self;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal MetadataEditor this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(56)]
		internal _1(MetadataEditor this_00240, MetadataEditor me) : base()
		{
			this.this_00240 = this_00240;
			val_0024self = me;
		}

		[LineNumberTable(59)]
		public virtual void applyToFragment(MovieBox mov, MovieFragmentBox[] fragmentBox)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[]
		{
			159, 127, 162, 119, 127, 9, 127, 2, 100, 103,
			136, 178, 127, 8, 100, 103, 119, 100, 115, 136,
			141, 148
		})]
		public virtual void apply(MovieBox movie)
		{
			MetaBox meta1 = (MetaBox)NodeBox.findFirst(movie, ClassLiteral<MetaBox>.Value, MetaBox.fourcc());
			MetaBox meta2 = (MetaBox)NodeBox.findFirstPath(movie, ClassLiteral<MetaBox>.Value, new string[2]
			{
				"udta",
				MetaBox.fourcc()
			});
			if (access_0024000(val_0024self) != null && access_0024000(val_0024self).size() > 0)
			{
				if (meta1 == null)
				{
					meta1 = MetaBox.createMetaBox();
					movie.add(meta1);
				}
				meta1.setKeyedMeta(access_0024000(val_0024self));
			}
			if (access_0024100(val_0024self) == null || access_0024100(val_0024self).size() <= 0)
			{
				return;
			}
			if (meta2 == null)
			{
				meta2 = UdtaMetaBox.createUdtaMetaBox();
				NodeBox udta = (NodeBox)NodeBox.findFirst(movie, ClassLiteral<NodeBox>.Value, "udta");
				if (udta == null)
				{
					udta = new NodeBox(Header.createHeader("udta", 0L));
					movie.add(udta);
				}
				udta.add((UdtaMetaBox)meta2);
			}
			meta2.setItunesMeta(access_0024100(val_0024self));
		}
	}

	[Signature("Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;")]
	private Map keyedMeta;

	[Signature("Ljava/util/Map<Ljava/lang/Integer;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;")]
	private Map itunesMeta;

	private File source;

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(27)]
	internal static Map access_0024000(MetadataEditor x0)
	{
		return x0.keyedMeta;
	}

	[Modifiers(Modifiers.Static | Modifiers.Synthetic)]
	[LineNumberTable(27)]
	internal static Map access_0024100(MetadataEditor x0)
	{
		return x0.itunesMeta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 66, 105, 104, 104, 104 })]
	public MetadataEditor(File source, Map keyedMeta, Map itunesMeta)
	{
		this.source = source;
		this.keyedMeta = keyedMeta;
		this.itunesMeta = itunesMeta;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[]
	{
		159, 133, 162, 104, 105, 191, 7, 104, 124, 124,
		39, 172, 125, 63, 24
	})]
	public static MetadataEditor createFrom(File f)
	{
		Format format = JCodecUtil.detectFormat(f);
		if (format != Format.___003C_003EMOV)
		{
			string s = new StringBuilder().append("Unsupported format: ").append(format).toString();
			throw new IllegalArgumentException(s);
		}
		MP4Util.Movie movie = MP4Util.parseFullMovie(f);
		MetaBox keyedMeta = (MetaBox)NodeBox.findFirst(movie.getMoov(), ClassLiteral<MetaBox>.Value, MetaBox.fourcc());
		MetaBox itunesMeta = (MetaBox)NodeBox.findFirstPath(movie.getMoov(), ClassLiteral<MetaBox>.Value, new string[2]
		{
			"udta",
			MetaBox.fourcc()
		});
		Map obj = ((keyedMeta != null) ? keyedMeta.getKeyedMeta() : new HashMap());
		Map obj2 = ((itunesMeta != null) ? itunesMeta.getItunesMeta() : new HashMap());
		MetadataEditor result = new MetadataEditor(itunesMeta: (obj2 == null) ? null : ((obj2 as Map) ?? throw new java.lang.IncompatibleClassChangeError()), source: f, keyedMeta: (obj == null) ? null : ((obj as Map) ?? throw new java.lang.IncompatibleClassChangeError()));
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Throws(new string[] { "java.io.IOException" })]
	[LineNumberTable(new byte[] { 159, 129, 161, 67, 99, 233, 95, 100, 148, 148 })]
	public virtual void save(bool fast)
	{
		_1 edit = new _1(this, this);
		if (fast)
		{
			new RelocateMP4Editor().modifyOrRelocate(source, edit);
		}
		else
		{
			new ReplaceMP4Editor().modifyOrReplace(source, edit);
		}
	}

	[Signature("()Ljava/util/Map<Ljava/lang/Integer;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;")]
	[LineNumberTable(95)]
	public virtual Map getItunesMeta()
	{
		return itunesMeta;
	}

	[Signature("()Ljava/util/Map<Ljava/lang/String;Lorg/jcodec/containers/mp4/boxes/MetaValue;>;")]
	[LineNumberTable(99)]
	public virtual Map getKeyedMeta()
	{
		return keyedMeta;
	}
}
