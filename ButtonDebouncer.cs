using System;
using System.Timers;

namespace FIV.Debouncer
{

    public class ButtonDebouncer
    {
        private readonly object _lock = new object();
        private Action _delayedHandler = null;
        private Action<bool> _onChangeAction = null;

        private bool _firstValue = false;
        private bool _lastValue = false;

        private readonly System.Timers.Timer _throttleTimer;

        public ButtonDebouncer(Action<bool> onChangeValue, TimeSpan debounceDelay)
        {
            _onChangeAction = onChangeValue;
            _throttleTimer = new System.Timers.Timer(debounceDelay.TotalMilliseconds);
            _throttleTimer.Elapsed += Timer_Tick;
        }

        public void HandlingBtnPressing(bool incomingValue)
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

        private void SendResultPressing(bool value)
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
                    if (_firstValue != _lastValue)
                    {
                        _delayedHandler();
                    }

                    _delayedHandler = null;
                }
            }
        }


    }
}
