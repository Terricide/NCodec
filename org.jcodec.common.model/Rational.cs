using System.Runtime.CompilerServices;
using IKVM.Attributes;
using java.lang;
using org.jcodec.common.tools;

namespace org.jcodec.common.model;

public class Rational : Object
{
	internal static Rational ___003C_003EONE;

	internal static Rational ___003C_003EHALF;

	internal static Rational ___003C_003EZERO;

	internal int ___003C_003Enum;

	internal int ___003C_003Eden;

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Rational ONE
	{
		[HideFromJava]
		get
		{
			return ___003C_003EONE;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Rational HALF
	{
		[HideFromJava]
		get
		{
			return ___003C_003EHALF;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Static | Modifiers.Final)]
	public static Rational ZERO
	{
		[HideFromJava]
		get
		{
			return ___003C_003EZERO;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public int num
	{
		[HideFromJava]
		get
		{
			return ___003C_003Enum;
		}
		[HideFromJava]
		private set
		{
			___003C_003Enum = value;
		}
	}

	[Modifiers(Modifiers.Public | Modifiers.Final)]
	public int den
	{
		[HideFromJava]
		get
		{
			return ___003C_003Eden;
		}
		[HideFromJava]
		private set
		{
			___003C_003Eden = value;
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[SpecialName]
	public static void ___003Cclinit_003E()
	{
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(26)]
	public static Rational R(int num, int den)
	{
		Rational result = new Rational(num, den);
		
		return result;
	}

	[LineNumberTable(39)]
	public virtual int getNum()
	{
		return ___003C_003Enum;
	}

	[LineNumberTable(43)]
	public virtual int getDen()
	{
		return ___003C_003Eden;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 134, 98, 105, 104, 104 })]
	public Rational(int num, int den)
	{
		___003C_003Enum = num;
		___003C_003Eden = den;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 130, 162, 109, 101, 141, 101, 106, 107, 149 })]
	public static Rational parse(string @string)
	{
		int idx = String.instancehelper_indexOf(@string, ":");
		if (idx < 0)
		{
			idx = String.instancehelper_indexOf(@string, "/");
		}
		if (idx > 0)
		{
			string num = String.instancehelper_substring(@string, 0, idx);
			string den = String.instancehelper_substring(@string, idx + 1);
			Rational result = new Rational(Integer.parseInt(num), Integer.parseInt(den));
			
			return result;
		}
		Rational result2 = R(Integer.parseInt(@string), 1);
		
		return result2;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[] { 159, 92, 98, 105 })]
	public static Rational reduce(int num, int den)
	{
		int gcd = MathUtil.gcd(num, den);
		Rational result = new Rational((gcd != -1) ? (num / gcd) : (-num), (gcd != -1) ? (den / gcd) : (-den));
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(30)]
	public static Rational R1(int num)
	{
		Rational result = R(num, 1);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(47)]
	public static Rational parseRational(string @string)
	{
		Rational result = parse(@string);
		
		return result;
	}

	[LineNumberTable(new byte[] { 159, 126, 98, 100, 99, 109, 109 })]
	public override int hashCode()
	{
		int prime = 31;
		int result = 1;
		result = 31 * result + ___003C_003Eden;
		return 31 * result + ___003C_003Enum;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(new byte[]
	{
		159, 124, 130, 101, 99, 100, 99, 111, 99, 104,
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
		Rational other = (Rational)obj;
		if (___003C_003Eden != other.___003C_003Eden)
		{
			return false;
		}
		if (___003C_003Enum != other.___003C_003Enum)
		{
			return false;
		}
		return true;
	}

	[LineNumberTable(89)]
	public virtual int multiplyS(int val)
	{
		long num = (long)___003C_003Enum * (long)val;
		long num2 = ___003C_003Eden;
		return (int)((num2 != -1) ? (num / num2) : (-num));
	}

	[LineNumberTable(93)]
	public virtual int divideS(int val)
	{
		long num = (long)___003C_003Eden * (long)val;
		long num2 = ___003C_003Enum;
		return (int)((num2 != -1) ? (num / num2) : (-num));
	}

	[LineNumberTable(97)]
	public virtual int divideByS(int val)
	{
		int __003C_003Enum = ___003C_003Enum;
		int num = ___003C_003Eden * val;
		return (num != -1) ? (__003C_003Enum / num) : (-__003C_003Enum);
	}

	[LineNumberTable(101)]
	public virtual long multiplyLong(long val)
	{
		long num = ___003C_003Enum * val;
		long num2 = ___003C_003Eden;
		return (num2 != -1) ? (num / num2) : (-num);
	}

	[LineNumberTable(105)]
	public virtual long divideLong(long val)
	{
		long num = ___003C_003Eden * val;
		long num2 = ___003C_003Enum;
		return (num2 != -1) ? (num / num2) : (-num);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(109)]
	public virtual Rational flip()
	{
		Rational result = new Rational(___003C_003Eden, ___003C_003Enum);
		
		return result;
	}

	[LineNumberTable(113)]
	public virtual bool smallerThen(Rational sec)
	{
		return ___003C_003Enum * sec.___003C_003Eden < sec.___003C_003Enum * ___003C_003Eden;
	}

	[LineNumberTable(117)]
	public virtual bool greaterThen(Rational sec)
	{
		return ___003C_003Enum * sec.___003C_003Eden > sec.___003C_003Enum * ___003C_003Eden;
	}

	[LineNumberTable(121)]
	public virtual bool smallerOrEqualTo(Rational sec)
	{
		return ___003C_003Enum * sec.___003C_003Eden <= sec.___003C_003Enum * ___003C_003Eden;
	}

	[LineNumberTable(125)]
	public virtual bool greaterOrEqualTo(Rational sec)
	{
		return ___003C_003Enum * sec.___003C_003Eden >= sec.___003C_003Enum * ___003C_003Eden;
	}

	[LineNumberTable(129)]
	public virtual bool equalsRational(Rational other)
	{
		return ___003C_003Enum * other.___003C_003Eden == other.___003C_003Enum * ___003C_003Eden;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(133)]
	public virtual Rational plus(Rational other)
	{
		Rational result = reduce(___003C_003Enum * other.___003C_003Eden + other.___003C_003Enum * ___003C_003Eden, ___003C_003Eden * other.___003C_003Eden);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(137)]
	public virtual RationalLarge plusLarge(RationalLarge other)
	{
		RationalLarge result = RationalLarge.reduceLong(___003C_003Enum * other.den + other.num * ___003C_003Eden, ___003C_003Eden * other.den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(141)]
	public virtual Rational minus(Rational other)
	{
		Rational result = reduce(___003C_003Enum * other.___003C_003Eden - other.___003C_003Enum * ___003C_003Eden, ___003C_003Eden * other.___003C_003Eden);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(145)]
	public virtual RationalLarge minusLarge(RationalLarge other)
	{
		RationalLarge result = RationalLarge.reduceLong(___003C_003Enum * other.den - other.num * ___003C_003Eden, ___003C_003Eden * other.den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(149)]
	public virtual Rational plusInt(int scalar)
	{
		Rational result = new Rational(___003C_003Enum + scalar * ___003C_003Eden, ___003C_003Eden);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(153)]
	public virtual Rational minusInt(int scalar)
	{
		Rational result = new Rational(___003C_003Enum - scalar * ___003C_003Eden, ___003C_003Eden);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(157)]
	public virtual Rational multiplyInt(int scalar)
	{
		Rational result = new Rational(___003C_003Enum * scalar, ___003C_003Eden);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(161)]
	public virtual Rational divideInt(int scalar)
	{
		Rational result = new Rational(___003C_003Eden * scalar, ___003C_003Enum);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(165)]
	public virtual Rational divideByInt(int scalar)
	{
		Rational result = new Rational(___003C_003Enum, ___003C_003Eden * scalar);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(169)]
	public virtual Rational multiply(Rational other)
	{
		Rational result = reduce(___003C_003Enum * other.___003C_003Enum, ___003C_003Eden * other.___003C_003Eden);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(173)]
	public virtual RationalLarge multiplyLarge(RationalLarge other)
	{
		RationalLarge result = RationalLarge.reduceLong(___003C_003Enum * other.num, ___003C_003Eden * other.den);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(177)]
	public virtual Rational divide(Rational other)
	{
		Rational result = reduce(other.___003C_003Enum * ___003C_003Eden, other.___003C_003Eden * ___003C_003Enum);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(181)]
	public virtual RationalLarge divideLarge(RationalLarge other)
	{
		RationalLarge result = RationalLarge.reduceLong(other.num * ___003C_003Eden, other.den * ___003C_003Enum);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(185)]
	public virtual Rational divideBy(Rational other)
	{
		Rational result = reduce(___003C_003Enum * other.___003C_003Eden, ___003C_003Eden * other.___003C_003Enum);
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(189)]
	public virtual RationalLarge divideByLarge(RationalLarge other)
	{
		RationalLarge result = RationalLarge.reduceLong(___003C_003Enum * other.den, ___003C_003Eden * other.num);
		
		return result;
	}

	[LineNumberTable(193)]
	public virtual float scalar()
	{
		return (float)___003C_003Enum / (float)___003C_003Eden;
	}

	[LineNumberTable(197)]
	public virtual int scalarClip()
	{
		int __003C_003Enum = ___003C_003Enum;
		int __003C_003Eden = ___003C_003Eden;
		return (__003C_003Eden != -1) ? (__003C_003Enum / __003C_003Eden) : (-__003C_003Enum);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(206)]
	public static Rational reduceRational(Rational r)
	{
		Rational result = reduce(r.getNum(), r.getDen());
		
		return result;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[LineNumberTable(211)]
	public override string toString()
	{
		string result = new StringBuilder().append(___003C_003Enum).append("/").append(___003C_003Eden)
			.toString();
		
		return result;
	}

	[LineNumberTable(215)]
	public virtual double toDouble()
	{
		return (double)___003C_003Enum / (double)___003C_003Eden;
	}

	[LineNumberTable(new byte[] { 159, 138, 162, 109, 109 })]
	static Rational()
	{
		___003C_003EONE = new Rational(1, 1);
		___003C_003EHALF = new Rational(1, 2);
		___003C_003EZERO = new Rational(0, 1);
	}
}
