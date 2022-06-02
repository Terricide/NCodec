using System.Runtime.CompilerServices;
using IKVM.Attributes;
using ikvm.@internal;
using java.lang;
using java.util;
using java.util.concurrent;

namespace org.jcodec.common.io;

public class AutoPool : Object
{
	[SpecialName]
	[EnclosingMethod(null, "<init>", "()V")]
	internal class _1 : Object, Runnable
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal List val_0024res;

		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal AutoPool this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(30)]
		internal _1(AutoPool this_00240, List l)
		{
			this.this_00240 = this_00240;
			val_0024res = l;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 134, 66, 103, 127, 2, 104, 99 })]
		public virtual void run()
		{
			long curTime = java.lang.System.currentTimeMillis();
			Iterator iterator = val_0024res.iterator();
			while (iterator.hasNext())
			{
				AutoResource autoResource = (AutoResource)iterator.next();
				autoResource.setCurTime(curTime);
			}
		}
	}

	[SpecialName]
	[EnclosingMethod(null, "daemonThreadFactory", "()Ljava.util.concurrent.ThreadFactory;")]
	internal class _2 : Object, ThreadFactory
	{
		[Modifiers(Modifiers.Final | Modifiers.Synthetic)]
		internal AutoPool this_00240;

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(41)]
		internal _2(AutoPool this_00240)
		{
			this.this_00240 = this_00240;
			
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[LineNumberTable(new byte[] { 159, 131, 66, 104, 104, 113 })]
		public virtual Thread newThread(Runnable r)
		{
			Thread t = new Thread(r);
			t.setDaemon(on: true);
			t.setName(ClassLiteral<AutoPool>.Value.getName());
			return t;
		}
	}

	[Modifiers(Modifiers.Private | Modifiers.Final)]
	[Signature("Ljava/util/List<Lorg/jcodec/common/io/AutoResource;>;")]
	private List resources;

	private ScheduledExecutorService scheduler;

	private static AutoPool instance;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[LineNumberTable(53)]
	public static AutoPool getInstance()
	{
		return instance;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 128, 98, 110 })]
	public virtual void add(AutoResource res)
	{
		resources.add(res);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(41)]
	private ThreadFactory daemonThreadFactory()
	{
		_2 result = new _2(this);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 136, 130, 105, 113, 115, 104, 254, 72 })]
	private AutoPool()
	{
		resources = Collections.synchronizedList(new ArrayList());
		scheduler = Executors.newScheduledThreadPool(1, daemonThreadFactory());
		List res = resources;
		scheduler.scheduleAtFixedRate(new _1(this, res), 0L, 100L, TimeUnit.MILLISECONDS);
	}

	[LineNumberTable(60)]
	static AutoPool()
	{
		instance = new AutoPool();
	}
}
