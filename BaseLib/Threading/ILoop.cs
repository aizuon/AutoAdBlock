using System;

namespace BaseLib.Threading
{
    public interface ILoop
    {
        bool IsRunning { get; }
        int TicksPerSecond { get; }
        TimeSpan TickRate { get; }

        void Start();
        void Stop();
        void Stop(TimeSpan timeout);
    }
}
