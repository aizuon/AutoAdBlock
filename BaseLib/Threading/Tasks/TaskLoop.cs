using System;
using System.Threading.Tasks;

namespace BaseLib.Threading.Tasks
{
    public sealed class TaskLoop : TaskLoopBase
    {
        private readonly Func<TimeSpan, Task> _callback;

        public TaskLoop(TimeSpan tickRate, Func<TimeSpan, Task> callback)
            : base(tickRate)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            _callback = callback;
        }

        protected override Task OnTickAsync(TimeSpan elapsed)
        {
            return _callback(elapsed);
        }
    }
}
