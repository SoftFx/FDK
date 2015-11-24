namespace SoftFX.Extended
{
    using System.Collections.Generic;

    interface ICloneable<out T>
    {
        T Clone();
    }

    interface ICloneableEnumerator<out T> : ICloneable<ICloneableEnumerator<T>>, IEnumerator<T>
    {
    }
}
