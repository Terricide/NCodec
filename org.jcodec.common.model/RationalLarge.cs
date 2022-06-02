using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.common.model;

public class RationalLarge : Object
{
	internal static RationalLarge ___003C_003EONE;

	internal static RationalLarge ___003C_003EHALF;

	internal static RationalLarge ___003C_003EZERO;

	[Modifiers(Modifiers.Final)]
	internal long num;

	[Modifiers(Modifiers.Final)]
	internal long den;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static RationalLarge ONE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EONE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static RationalLarge HALF
	{
		[HideFromJava]
		get
		{
			return ___003C_003EHALF;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static RationalLarge ZERO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EZERO;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 133, 66, 109, 126, 47 })]
	public static RationalLarge parse(string @string)
	{
		string[] split = StringUtils.splitS(@string, ":");
		RationalLarge result = (((nint)split.LongLength <= 1) ? R(Long.parseLong(@string), 1L) : R(Long.parseLong(split[0]), Long.parseLong(split[1])));
		
		return result;
	}

	[LineNumberTable(67)]
	public virtual long multiplyS(long scalar)
	{
		long num = this.num * scalar;
		long num2 = den;
		return (num2 != -1) ? (num / num2) : (-num);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(83)]
	public static RationalLarge R(long num, long den)
	{
		RationalLarge result = new RationalLarge(num, den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 96, 66, 105 })]
	public static RationalLarge reduceLong(long num, long den)
	{
		long gcd = MathUtil.gcdLong(num, den);
		RationalLarge result = new RationalLarge((gcd != -1) ? (num / gcd) : (-num), (gcd != -1) ? (den / gcd) : (-den));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 137, 130, 105, 104, 104 })]
	public RationalLarge(long num, long den)
	{
		this.num = num;
		this.den = den;
	}

	[LineNumberTable(28)]
	public virtual long getNum()
	{
		return num;
	}

	[LineNumberTable(32)]
	public virtual long getDen()
	{
		return den;
	}

	[LineNumberTable(new byte[] { 159, 132, 162, 100, 99, 120, 120 })]
	public override int hashCode()
	{
		int prime = 31;
		int result = 1;
		result = 31 * result + (int)(den ^ (long)((ulong)den >> 32));
		return 31 * result + (int)(num ^ (long)((ulong)num >> 32));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 129, 66, 101, 99, 100, 99, 111, 99, 104,
		111, 99, 111, 99
	})]
	public override bool equals(object obj)
	{
		if (this == obj)
		{
			return true;
		}
		if (obj == null)
		{
			return false;
		}
		if ((object)((object)this).GetType() != obj.GetType())
		{
			return false;
		}
		RationalLarge other = (RationalLarge)obj;
		if (den != other.den)
		{
			return false;
		}
		if (num != other.num)
		{
			return false;
		}
		return true;
	}

	[LineNumberTable(71)]
	public virtual long divideS(long scalar)
	{
		long num = den * scalar;
		long num2 = this.num;
		return (num2 != -1) ? (num / num2) : (-num);
	}

	[LineNumberTable(75)]
	public virtual long divideByS(long scalar)
	{
		long num = this.num;
		long num2 = den * scalar;
		return (num2 != -1) ? (num / num2) : (-num);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(79)]
	public virtual RationalLarge flip()
	{
		RationalLarge result = new RationalLarge(den, num);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(87)]
	public static RationalLarge R1(long num)
	{
		RationalLarge result = R(num, 1L);
		
		return result;
	}

	[LineNumberTable(91)]
	public virtual bool lessThen(RationalLarge sec)
	{
		return num * sec.den < sec.num * den;
	}

	[LineNumberTable(95)]
	public virtual bool greaterThen(RationalLarge sec)
	{
		return num * sec.den > sec.num * den;
	}

	[LineNumberTable(99)]
	public virtual bool smallerOrEqualTo(RationalLarge sec)
	{
		return num * sec.den <= sec.num * den;
	}

	[LineNumberTable(103)]
	public virtual bool greaterOrEqualTo(RationalLarge sec)
	{
		return num * sec.den >= sec.num * den;
	}

	[LineNumberTable(107)]
	public virtual bool equalsLarge(RationalLarge other)
	{
		return num * other.den == other.num * den;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(111)]
	public virtual RationalLarge plus(RationalLarge other)
	{
		RationalLarge result = reduceLong(num * other.den + other.num * den, den * other.den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(115)]
	public virtual RationalLarge plusR(Rational other)
	{
		RationalLarge result = reduceLong(num * other.___003C_003Eden + other.___003C_003Enum * den, den * other.___003C_003Eden);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(119)]
	public virtual RationalLarge minus(RationalLarge other)
	{
		RationalLarge result = reduceLong(num * other.den - other.num * den, den * other.den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(123)]
	public virtual RationalLarge minusR(Rational other)
	{
		RationalLarge result = reduceLong(num * other.___003C_003Eden - other.___003C_003Enum * den, den * other.___003C_003Eden);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(127)]
	public virtual RationalLarge plusLong(long scalar)
	{
		RationalLarge result = new RationalLarge(num + scalar * den, den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(131)]
	public virtual RationalLarge minusLong(long scalar)
	{
		RationalLarge result = new RationalLarge(num - scalar * den, den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(135)]
	public virtual RationalLarge multiplyLong(long scalar)
	{
		RationalLarge result = new RationalLarge(num * scalar, den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(139)]
	public virtual RationalLarge divideLong(long scalar)
	{
		RationalLarge result = new RationalLarge(den * scalar, num);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(143)]
	public virtual RationalLarge divideByLong(long scalar)
	{
		RationalLarge result = new RationalLarge(num, den * scalar);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(147)]
	public virtual RationalLarge multiply(RationalLarge other)
	{
		RationalLarge result = reduceLong(num * other.num, den * other.den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(151)]
	public virtual RationalLarge multiplyR(Rational other)
	{
		RationalLarge result = reduceLong(num * other.___003C_003Enum, den * other.___003C_003Eden);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(155)]
	public virtual RationalLarge divideRL(RationalLarge other)
	{
		RationalLarge result = reduceLong(other.num * den, other.den * num);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(159)]
	public virtual RationalLarge divideR(Rational other)
	{
		RationalLarge result = reduceLong(other.___003C_003Enum * den, other.___003C_003Eden * num);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(163)]
	public virtual RationalLarge divideBy(RationalLarge other)
	{
		RationalLarge result = reduceLong(num * other.den, den * other.num);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(167)]
	public virtual RationalLarge divideByR(Rational other)
	{
		RationalLarge result = reduceLong(num * other.___003C_003Eden, den * other.___003C_003Enum);
		
		return result;
	}

	[LineNumberTable(171)]
	public virtual double scalar()
	{
		return (double)num / (double)den;
	}

	[LineNumberTable(175)]
	public virtual long scalarClip()
	{
		long num = this.num;
		long num2 = den;
		return (num2 != -1) ? (num / num2) : (-num);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(180)]
	public override string toString()
	{
		string result = new StringBuilder().append(num).append(":").append(den)
			.toString();
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 139, 162, 111, 111 })]
	static RationalLarge()
	{
		___003C_003EONE = new RationalLarge(1L, 1L);
		___003C_003EHALF = new RationalLarge(1L, 2L);
		___003C_003EZERO = new RationalLarge(0L, 1L);
	}
}
