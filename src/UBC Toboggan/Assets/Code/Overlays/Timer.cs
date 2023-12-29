using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Timer
    {
        protected float _secondsRemaining;
        protected float _timerLength;
        bool _isTimerPaused;

        public Timer(float initialLength)
        {
            _secondsRemaining = initialLength;
            _timerLength = initialLength;
        }

        public float secondsRemaining => _secondsRemaining;

        public bool isTimerComplete => _secondsRemaining <= 0f;

        public bool isTimerPaused => _isTimerPaused;

        public float maxTime => _timerLength;

        public void resetTimer() => _secondsRemaining = _timerLength;

        public virtual void countDownBy(float amount)
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

    public class BoostTimer: Timer 
    {

        public BoostTimer(float initialLength) : base(initialLength)
        {    
        }

        public override void countDownBy(float amount)
        {
            if (_secondsRemaining >= 0f)
            {
                _secondsRemaining -= amount;
            }    

            _secondsRemaining = _secondsRemaining > _timerLength ? _timerLength : _secondsRemaining;
        }
    }
}
