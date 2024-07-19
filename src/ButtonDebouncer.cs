using System;
using System.Timers;

namespace FIV.Debouncer
{

    public class ButtonDebouncer<T>
    {
        private readonly object _lock = new object();
        private Action _delayedHandler = null;
        private Action<T> _onChangeAction = null;

        private T _firstValue = default(T);
        private T _lastValue = default(T);

        private readonly System.Timers.Timer _throttleTimer;

        public ButtonDebouncer(Action<T> onChangeValue, TimeSpan debounceDelay)
        {
            _onChangeAction = onChangeValue;
            _throttleTimer = new System.Timers.Timer(debounceDelay.TotalMilliseconds);
            _throttleTimer.Elapsed += Timer_Tick;
        }

        public void HandlingBtnPressing(T incomingValue)
        {
            lock (_lock)
            {
                if (_throttleTimer.Enabled)
                {
                    _lastValue = incomingValue;

                    _delayedHandler = () => SendResultPressing(_lastValue);
                    _throttleTimer.Stop();
                    _throttleTimer.Start();

                }
                else
                {
                    _firstValue = incomingValue;
                    SendResultPressing(_firstValue);
                }
            }
        }

        private void SendResultPressing(T value)
        {
            if (_onChangeAction != null)
            {
                _onChangeAction(value);
                _throttleTimer.Start();

            }
        }

        private void Timer_Tick(object sender, EventArgs args)
        {
            lock (_lock)
            {
                _throttleTimer.Stop();
                if (_delayedHandler != null)
                {

                    if(!_firstValue.Equals(_lastValue))
                    {
                        _delayedHandler();
                    }


                    _delayedHandler = null;
                }
            }
        }


    }
}
