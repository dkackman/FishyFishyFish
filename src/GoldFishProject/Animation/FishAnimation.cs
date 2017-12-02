using System;
using System.Drawing;
using System.Collections.Generic;

namespace FishTank.Animation
{
    sealed class FishAnimation : IDisposable
    {
        private readonly Rectangle _tank;
        private readonly Size _size;
        private readonly IEnumerable<Tuple<Bitmap, Bitmap>> _frames; // this is owned so must be disposed
        private readonly LoopingEnumerator<Tuple<Bitmap, Bitmap>> _frameEnumerator; // the bitmaps for the fish - one set of left and another for right

        private Counter _ticksAtCurrentVelocity; // the number of ticks to spend at the current velocity

        private PointF _location;
        private SizeF _velocity = new SizeF(2, 0);

        public FishAnimation(Rectangle tank, IEnumerable<Tuple<Bitmap, Bitmap>> frames, Size size)
        {
            _tank = tank;
            _size = size;
            _location = new PointF(_tank.Left - _size.Width, _tank.Height * (float)Program.TheRandom.NextDouble());

            _frames = frames;
            _frameEnumerator = new LoopingEnumerator<Tuple<Bitmap, Bitmap>>(_frames.GetEnumerator());

            SetNewVelocityDuration();
        }

        public void MoveTo(Point point) => _location = new PointF(point.X, point.Y);

        public event EventHandler<Point> LocationChanged;

        public event EventHandler VelocityChanged;

        public Bitmap Tick(Rectangle screen)
        {
            // move our location by the velocity vector
            _location += _velocity;

            // if we are above or below the screen bounds point the velocity vector to get us back on screen
            if (_location.Y > screen.Bottom)
            {
                _velocity.Height = -1;
            }
            else if (_location.Y < screen.Top)
            {
                _velocity.Height = 1;
            }

            // if we are off screen to the left or right (and not already heading back) turn around
            if ((_velocity.Width > 0 && _location.X > _tank.Right)
                || (_velocity.Width < 0 && _location.X < _tank.Left - _size.Width)
                || (Program.TheRandom.Next(_tank.Width) == 1)) // random chance to turn around spontaneously
            {
                SwitchDirections();
            }
            else if (_ticksAtCurrentVelocity.Next() == 0) // once the animation counter runs down, pick a new random velocity
            {
                float dx = ((float)Program.TheRandom.NextDouble() * 3f + 0.5f) * Math.Sign(_velocity.Width);
                float dy = ((float)Program.TheRandom.NextDouble() - 0.5f) * 0.5f;

                _velocity = new SizeF(dx, dy);
                SetNewVelocityDuration();
            }

            LocationChanged(this, Point.Round(_location));

            // get the next frame and return the correct bitmap (based on direction the fish is swimming)
            _frameEnumerator.MoveNext();

            return _velocity.Width > 0 ? _frameEnumerator.Current.Item2 : _frameEnumerator.Current.Item1;
        }

        public void SwitchDirections()
        {
            _velocity.Width *= -1f;
            SetNewVelocityDuration();
        }

        public void SetNewVelocityDuration()
        {
            _ticksAtCurrentVelocity = new Counter(Program.TheRandom.Next(70) + 40);
            VelocityChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            _frameEnumerator.Dispose();

            foreach (var tuple in _frames)
            {
                tuple.Item1.Dispose();
                tuple.Item2.Dispose();
            }
        }
    }
}
