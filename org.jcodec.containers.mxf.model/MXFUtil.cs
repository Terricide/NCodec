using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;
using org.jcodec.platform;

namespace org.jcodec.containers.mxf.model;

public class MXFUtil : Object
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/containers/mxf/model/MXFMetadata;>;[Lorg/jcodec/containers/mxf/model/UL;Ljava/lang/Class<TT;>;)Ljava/util/List<TT;>;")]
	[LineNumberTable(new byte[]
	{
		159, 136, 130, 104, 112, 109, 119, 103, 131, 103,
		106, 127, 1, 115, 138, 227, 59, 233, 71
	})]
	public static List resolveRefs(List metadata, UL[] refs, Class class1)
	{
		ArrayList copy = new ArrayList(metadata);
		Iterator iterator = ((List)copy).iterator();
		while (iterator.hasNext())
		{
			MXFMetadata next = (MXFMetadata)iterator.next();
			if (next.getUid() == null || !Platform.isAssignableFrom(class1, Object.instancehelper_getClass(next)))
			{
				iterator.remove();
			}
		}
		ArrayList result = new ArrayList();
		for (int i = 0; i < (nint)refs.LongLength; i++)
		{
			Iterator iterator2 = ((List)copy).iterator();
			while (iterator2.hasNext())
			{
				MXFMetadata meta = (MXFMetadata)iterator2.next();
				if (meta.getUid().equals(refs[i]))
				{
					((List)result).add((object)meta);
				}
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(19)]
	public MXFUtil()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Ljava/util/List<Lorg/jcodec/containers/mxf/model/MXFMetadata;>;Lorg/jcodec/containers/mxf/model/UL;Ljava/lang/Class<TT;>;)TT;")]
	[LineNumberTable(new byte[] { 159, 137, 98, 115 })]
	public static object resolveRef(List metadata, UL refs, Class class1)
	{
		List res = resolveRefs(metadata, new UL[1] { refs }, class1);
		return (res.size() <= 0) ? null : res.get(0);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Ljava/util/Collection<Lorg/jcodec/containers/mxf/model/MXFMetadata;>;Ljava/lang/Class<TT;>;)TT;")]
	[LineNumberTable(new byte[] { 159, 131, 98, 124, 111, 99, 99 })]
	public static object findMeta(Collection metadata, Class class1)
	{
		Iterator iterator = metadata.iterator();
		while (iterator.hasNext())
		{
			MXFMetadata mxfMetadata = (MXFMetadata)iterator.next();
			if (Platform.isAssignableFrom(Object.instancehelper_getClass(mxfMetadata), class1))
			{
				return mxfMetadata;
			}
		}
		return null;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("<T:Ljava/lang/Object;>(Ljava/util/Collection<Lorg/jcodec/containers/mxf/model/MXFMetadata;>;Ljava/lang/Class<TT;>;)Ljava/util/List<TT;>;")]
	[LineNumberTable(new byte[] { 159, 129, 98, 103, 124, 111, 105, 99 })]
	public static List findAllMeta(Collection metadata, Class class1)
	{
		ArrayList result = new ArrayList();
		Iterator iterator = metadata.iterator();
		while (iterator.hasNext())
		{
			MXFMetadata mxfMetadata = (MXFMetadata)iterator.next();
			if (Platform.isAssignableFrom(class1, Object.instancehelper_getClass(mxfMetadata)))
			{
				((List)result).add((object)mxfMetadata);
			}
		}
		return result;
	}
}
