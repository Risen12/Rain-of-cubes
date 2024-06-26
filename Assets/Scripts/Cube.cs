using System;
using Unity.Profiling;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> OnCollisionPlatform;
    public event Action<Cube> OnCubeLiveTimeEnded;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private bool _isCollidePlatform;
    private bool _isCountDownStarted;
    private float _countDownTime;
    private float _time;

    public bool IsCollidePlatform => _isCollidePlatform;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _isCollidePlatform = false;
        _isCountDownStarted = false;
        _countDownTime = 0;
        _time = 0;
    }

    private void Update()
    {
        if (_isCountDownStarted)
        {
            _time += Time.deltaTime;

            if (_time >= _countDownTime)
            {
                OnCubeLiveTimeEnded?.Invoke(this);
                _isCountDownStarted = false;
            }
        }
    }

    public void SetCollideStatus(bool status) => _isCollidePlatform = status;

    public Renderer GetRenderer() => _renderer;

    public void SetVelocity(Vector3 velocity) => _rigidbody.velocity = velocity;

    public void CollidePlatform()
    {
        OnCollisionPlatform?.Invoke(this);
    }

    public void StartCountDown()
    {
        _isCountDownStarted = true;
        _countDownTime = GetLiveTime();      
    }

    private float GetLiveTime()
    {
        float minLiveTime = 2f;
        float maxLiveTime = 5f;

        return UnityEngine.Random.Range(minLiveTime, maxLiveTime);
    }
}
