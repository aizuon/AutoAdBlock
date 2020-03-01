using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLib.Threading.Tasks
{
    public abstract class TaskLoopBase : ILoop
    {
        private const float WaitTolerance = 0.9585f;

        private CancellationTokenSource _cts;

        private double _lastTick;
        private double _lastUpdate;
        private int _tickCount;
        private Stopwatch _time;

        public Task Task { get; protected set; }
        public bool IsRunning { get; protected set; }
        public int TicksPerSecond { get; private set; }
        public TimeSpan TickRate { get; }
        private double Elapsed => _time.Elapsed.TotalMilliseconds - _lastTick;

        protected TaskLoopBase(TimeSpan tickRate)
        {
            TickRate = tickRate;
        }

        public virtual void Start()
        {
            if (IsRunning)
                return;

            _cts = new CancellationTokenSource();
            IsRunning = true;
            Task = Task.Run(new Func<Task>(InternalLoop));
        }

        public virtual void Stop()
        {
            Stop(Timeout.InfiniteTimeSpan);
        }

        public virtual void Stop(TimeSpan timeout)
        {
            if (!IsRunning || _cts.IsCancellationRequested)
                return;

            _cts.Cancel();
            Task?.Wait(timeout);
        }

        protected abstract Task OnTickAsync(TimeSpan elapsed);

        private async Task InternalLoop()
        {
            _time = Stopwatch.StartNew();
            _lastTick = _time.Elapsed.TotalMilliseconds;
            _tickCount = 0;
            _lastUpdate = 0;
            TicksPerSecond = 0;
            bool isFirst = true;

            while (!_cts.IsCancellationRequested)
            {
                if (TickRate.TotalMilliseconds > 0 && !isFirst)
                {
                    try
                    {
                        await WaitTickRequestedAsync().ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }

                if (isFirst)
                    isFirst = false;

                double elapsed = Elapsed;
                _lastTick = _time.Elapsed.TotalMilliseconds;
                _tickCount++;
                UpdateTicksPerSecond();
                await OnTickAsync(TimeSpan.FromMilliseconds(elapsed)).ConfigureAwait(false);
            }

            _time.Stop();
            Task = null;
            IsRunning = false;
        }

        private async Task WaitTickRequestedAsync()
        {
            while (true)
            {
                double targetTickTime = TickRate.TotalMilliseconds * WaitTolerance;
                double elapsed = Elapsed;
                if (elapsed >= targetTickTime)
                    return;

                double waitFor = targetTickTime - elapsed;
                await Task.Delay((int)waitFor, _cts.Token).ConfigureAwait(false);
            }
        }

        private void UpdateTicksPerSecond()
        {
            double elapsed = _time.Elapsed.TotalSeconds - _lastUpdate;
            TicksPerSecond = (int)(_tickCount / elapsed);
            _lastUpdate = _time.Elapsed.TotalSeconds;
            _tickCount = 0;
        }
    }
}
