using System.Collections;
using System.Collections.Generic;

namespace FishTank.Animation
{
    sealed class LoopingEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        private readonly IEnumerator<T> _innerEnumerator;
        
        public LoopingEnumerator(IEnumerator<T> innerEnumerator) => _innerEnumerator = innerEnumerator;

        public T Current => _innerEnumerator.Current;

        object IEnumerator.Current => _innerEnumerator.Current;

        public void Dispose() => _innerEnumerator.Dispose();

        public bool MoveNext()
        {
            if (!_innerEnumerator.MoveNext())
            {
                _innerEnumerator.Reset();
                return _innerEnumerator.MoveNext();
            }

            return true;
        }

        public void Reset() => _innerEnumerator.Reset();
    }
}
