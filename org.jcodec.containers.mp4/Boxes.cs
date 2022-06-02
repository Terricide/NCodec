using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using java.util;

namespace org.jcodec.containers.mp4;

public abstract class Boxes : Object
{
	[Signature("Ljava/util/Map<Ljava/lang/String;Ljava/lang/Class<+Lorg/jcodec/containers/mp4/boxes/Box;>;>;")]
	internal Map ___003C_003Emappings;

	[Modifiers(Modifiers.Protected | Modifiers.Final)]
	protected internal Map mappings
	{
		[HideFromJava]
		get
		{
			return ___003C_003Emappings;
		}
		[HideFromJava]
		private set
		{
			___003C_003Emappings = value;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 140, 162, 105, 108 })]
	public Boxes()
	{
		___003C_003Emappings = new HashMap();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/lang/String;)Ljava/lang/Class<+Lorg/jcodec/containers/mp4/boxes/Box;>;")]
	[LineNumberTable(16)]
	public virtual Class toClass(string fourcc)
	{
		return (Class)___003C_003Emappings.get(fourcc);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[Signature("(Ljava/lang/String;Ljava/lang/Class<+Lorg/jcodec/containers/mp4/boxes/Box;>;)V")]
	[LineNumberTable(new byte[] { 159, 137, 66, 111 })]
	public virtual void @override(string fourcc, Class cls)
	{
		___003C_003Emappings.put(fourcc, cls);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 66, 110 })]
	public virtual void clear()
	{
		___003C_003Emappings.clear();
	}
}
