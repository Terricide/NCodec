using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;

namespace org.jcodec.codecs.common.biari;

public class Context : Object
{
	private int stateIdx;

	private int mps;

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 138, 130, 105, 104, 104 })]
	public Context(int state, int mps)
	{
		stateIdx = state;
		this.mps = mps;
	}

	[LineNumberTable(24)]
	public virtual int getState()
	{
		return stateIdx;
	}

	[LineNumberTable(28)]
	public virtual int getMps()
	{
		return mps;
	}

	[LineNumberTable(new byte[] { 159, 134, 66, 104 })]
	public virtual void setMps(int mps)
	{
		this.mps = mps;
	}

	[LineNumberTable(new byte[] { 159, 133, 66, 104 })]
	public virtual void setState(int state)
	{
		stateIdx = state;
	}
}
