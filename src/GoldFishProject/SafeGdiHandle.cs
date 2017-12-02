using System;

using Microsoft.Win32.SafeHandles;

namespace FishTank
{
    sealed class SafeGdiHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private readonly Func<IntPtr, int> _release;

        public SafeGdiHandle(IntPtr hObject, Func<IntPtr, int> release)
            : base(true)
        {
            SetHandle(hObject);
            _release = release;
        }

        public static implicit operator IntPtr(SafeGdiHandle h) => h != null && !h.IsInvalid ? h.handle : IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            if (!this.IsInvalid)
            {
                try
                {
                    return _release(this.handle) != 0;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    this.handle = IntPtr.Zero;
                    this.SetHandleAsInvalid();
                }
            }

            return true;
        }
    }
}
