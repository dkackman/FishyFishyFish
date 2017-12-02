namespace FishTank.Animation
{
    struct Counter
    {
        private int _current;
        private readonly int _max;

        public Counter(int max)
        {
            _current = 0;
            _max = max;
        }

        public int Next() => ++_current >= _max ? _current = 0 : _current;

        public static implicit operator int(Counter counter) => counter._current;
    }
}
