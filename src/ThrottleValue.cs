using System;
using System.Timers;

namespace FIV.Debouncer
{

    public class ThrottleValue<T>
    {
        private readonly object _lock = new object();
        private Action _delayedHandler = null;
        private Action<T> _onNewValueAction = null;

        private T _firstValue = default(T);
        private T _lastValue = default(T);

        private readonly System.Timers.Timer _throttleTimer;

        public ThrottleValue(Action<T> onNewValue, TimeSpan debounceDelay)
        {
            _onNewValueAction = onNewValue;
            _throttleTimer = new System.Timers.Timer(debounceDelay.TotalMilliseconds);
            _throttleTimer.Elapsed += Timer_Tick;
        }

        public void Handling(T incomingValue)
        {
            lock (_lock)
            {
                if (_throttleTimer.Enabled)
                {
                    _lastValue = incomingValue;

                    _delayedHandler = () => SendResult(_lastValue);

                }
                else
                {
                    _throttleTimer.Start();
                    _firstValue = incomingValue;
                    SendResult(_firstValue);
                }
            }
        }

        private void SendResult(T value)
        {
            if (_onNewValueAction != null)
            {
                _onNewValueAction(value);

            }
        }

        private void Timer_Tick(object sender, EventArgs args)
        {
            lock (_lock)
            {
                _throttleTimer.Stop();
                if (_delayedHandler != null)
                {
                    _delayedHandler();
                    _delayedHandler = null;
                }
            }
        }


    }
}
