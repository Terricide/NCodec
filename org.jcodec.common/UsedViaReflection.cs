using IKVM.Attributes;
using java.lang.annotation;

namespace org.jcodec.common;

[Modifiers(Modifiers.Public | Modifiers.Interface | Modifiers.Abstract | Modifiers.Annotation)]
[Implements(new string[] { "java.lang.annotation.Annotation" })]
public interface UsedViaReflection : Annotation
{
}
