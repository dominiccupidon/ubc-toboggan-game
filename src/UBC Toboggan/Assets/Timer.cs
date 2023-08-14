using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Timer
    {
        private float _secondsRemaining;
        private float _timerLength;
        private bool _isTimerPaused;

        public Timer(float initialLength)
        {
            _secondsRemaining = initialLength;
            _timerLength = initialLength;
        }

        public float secondsRemaining
        {
            get
            {
                return _secondsRemaining;
            }
        }

        public bool isTimerComplete
        {
            get
            {
                return _secondsRemaining <= 0f;
            }
        }

        public bool isTimerPaused
        {
            get
            {
                return _isTimerPaused;
            }
        }

        public void resetTimer() => _secondsRemaining = _timerLength;

        public void countDownBy(float amount)
        {
            if (_secondsRemaining > 0f)
            {
                _secondsRemaining -= amount;
            }
        }

        public void pauseTimer() => _isTimerPaused = true;

        public void playTimer() => _isTimerPaused = false;

        public void stopTimer() => _secondsRemaining = 0;
    }
}
