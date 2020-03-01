using System;

namespace BaseLib.Threading
{
    public sealed class ThreadLoop : ThreadLoopBase
    {
        private readonly Action<TimeSpan> _callback;

        public ThreadLoop(TimeSpan tickRate, Action<TimeSpan> callback)
            : base(tickRate)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            _callback = callback;
        }

        public ThreadLoop(TimeSpan tickRate, Action callback)
            : base(tickRate)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            _callback = elapsed => callback();
        }

        protected override void OnTick(TimeSpan elapsed)
        {
            _callback(elapsed);
        }
    }
}
