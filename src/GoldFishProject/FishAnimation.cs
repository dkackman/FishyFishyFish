using System;
using System.Drawing;
using System.Collections.Generic;

namespace FishTank
{
    sealed class FishAnimation
    {
        private readonly Rectangle _tank;
        private readonly LoopingEnumerator<Tuple<Bitmap, Bitmap>> _frameEnumerator;
        private readonly int _width;

        private Counter _animationCounter;

        private SizeF _velocity = new SizeF(2, 0);
        private PointF _location;

        public FishAnimation(Rectangle tank, IEnumerable<Tuple<Bitmap, Bitmap>> frames, int width)
        {
            _tank = tank;
            _frameEnumerator = new LoopingEnumerator<Tuple<Bitmap, Bitmap>>(frames.GetEnumerator());
            _width = width;

            _location = new PointF(_tank.Left - _width, _tank.Height * (float)Program.TheRandom.NextDouble());

            ResetAnimationCounter();
        }

        public void MoveTo(Point point)
        {
            _location = new PointF(point.X, point.Y);
        }

        public Point Move(Rectangle screen)
        {
            _location += _velocity;

            if (_location.Y > screen.Bottom)
            {
                _velocity.Height = -1;
            }
            else if (_location.Y < screen.Top)
            {
                _velocity.Height = 1;
            }

            if ((_velocity.Width > 0 && _location.X > _tank.Right) || (_velocity.Width < 0 && _location.X < _tank.Left - _width))
            {
                SwitchDirections();
            }

            return Point.Round(_location);
        }

        public void SwitchDirections()
        {
            _velocity.Width *= -1f;
            ResetAnimationCounter();
        }

        public Bitmap NextFrame()
        {
            _frameEnumerator.MoveNext();

            return _velocity.Width > 0 ? _frameEnumerator.Current.Item2 : _frameEnumerator.Current.Item1;
        }

        public void ResetAnimationCounter()
        {
            _animationCounter = new Counter(Program.TheRandom.Next(70) + 40);
        }

        public bool CheckAnimationCounter()
        {
            // on one chance in the tank width switch directions
            if (Program.TheRandom.Next(_tank.Width) == 1)
            {
                SwitchDirections();
            }

            if (_animationCounter.Next() == 0)
            {
                float x = ((float)Program.TheRandom.NextDouble() * 3f + 0.5f) * Math.Sign(_velocity.Width);
                float y = ((float)Program.TheRandom.NextDouble() - 0.5f) * 0.5f;

                _velocity = new SizeF(x, y);
                ResetAnimationCounter();

                return true;
            }

            return false;
        }
    }
}
