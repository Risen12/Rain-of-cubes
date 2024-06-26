using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private const float Second = 1f;

    public event Action TimeEnded;

    private float _currentTime;
    private bool _isTimerActive;
    private float _secondsRemaining;

    private void Start()
    {
        SetDefaultSettings(false);
    }

    private void Update()
    {
        if (_isTimerActive)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= Second)
            {
                _secondsRemaining--;

                if (_secondsRemaining <= 0)
                { 
                    Stop();
                }
            }
        }
    }

    public void Set(float seconds)
    {
        SetDefaultSettings(true);
        _secondsRemaining = seconds;
    }

    private void Stop()
    {
        SetDefaultSettings(false);
        TimeEnded?.Invoke();
    }

    private void SetDefaultSettings(bool timerStatus)
    {
        _currentTime = 0f;
        _isTimerActive = timerStatus;
    }
}
