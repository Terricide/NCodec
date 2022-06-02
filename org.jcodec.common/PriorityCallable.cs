using IKVM.Attributes;
using java.util.concurrent;

namespace org.jcodec.common;

[Signature("<T:Ljava/lang/Object;>Ljava/lang/Object;Ljava/util/concurrent/Callable<TT;>;")]
[Implements(new string[] { "java.util.concurrent.Callable" })]
public interface PriorityCallable : Callable
{
	int getPriority();
}
