using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const string PlatformTag = "Platform";

    public event Action<Cube> OnCubeLiveTimeEnded;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private bool _isCollidePlatform;
    private Coroutine _liveTimeCountdown;
    private float _secondsRemaning;
    private ColorChanger _colorChanger;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _isCollidePlatform = false;
    }

    private void OnDisable()
    {
        if(_liveTimeCountdown != null )
            StopCoroutine(_liveTimeCountdown);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(PlatformTag))
        {
            if (_isCollidePlatform == false)
            {
                _secondsRemaning = GetLiveTime();
                _isCollidePlatform = true;

                StartCoroutine(CountDown(_secondsRemaning));
                ChangeColor();
            }
        }
    }

    public Renderer GetRenderer() => _renderer;

    public void SetColorChanger(ColorChanger colorChanger) => _colorChanger = colorChanger;

    public void SetVelocity(Vector3 velocity) => _rigidbody.velocity = velocity;

    private float GetLiveTime()
    {
        float minLiveTime = 2f;
        float maxLiveTime = 5f;

        return UnityEngine.Random.Range(minLiveTime, maxLiveTime);
    }

    private IEnumerator CountDown(float seconds)
    {
        WaitForSeconds wait = new WaitForSeconds(seconds);

        yield return wait;

        OnCubeLiveTimeEnded?.Invoke(this);
    }

    private void ChangeColor()
    {
        _colorChanger.SetRandomColor(this);
    }
}
