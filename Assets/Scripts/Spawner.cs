using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _defaultCapasity = 50;
    [SerializeField] private int _maxPoolSize = 100;
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] private ColorChanger _colorChanger;

    private ObjectPool<Cube> _cubePool;
    private float _time;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapasity,
            maxSize: _maxPoolSize);
        _time = 0f;
    }

    private void Start()
    {
        StartCoroutine(StartRain());
    }

    private IEnumerator StartRain()
    {
        WaitForSeconds wait = new(_delay);

        while (true)
        {
            yield return wait;
            GetCube();
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _delay)
        {
            _time = 0;
            GetCube();
        }
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetStartPosition();
        cube.gameObject.SetActive(true);
        cube.SetVelocity(Vector3.zero);
        cube.SetCollideStatus(false);
        cube.OnCollisionPlatform += OnCubeCollidePlatform;
        cube.OnCubeLiveTimeEnded += ReleaseCube;
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _colorChanger.SetDefaultColor(cube);
        cube.OnCubeLiveTimeEnded -= ReleaseCube;
    }

    private void GetCube()
    {
        _cubePool.Get();
    }

    private void ReleaseCube(Cube cube) => _cubePool.Release(cube);

    private void OnCubeCollidePlatform(Cube cube)
    {
        if (cube.IsCollidePlatform == false)
        {
            _colorChanger.SetRandomColor(cube);
            cube.SetCollideStatus(true);
            cube.OnCollisionPlatform -= OnCubeCollidePlatform;
            cube.StartCountDown();
        }
    }

    private Vector3 GetStartPosition()
    {
        float minX = -13f;
        float maxX = 13f;
        float minZ = -13f;
        float maxZ = 13f;
        int descreaser = 1;

        return new Vector3(Random.Range(minX, maxX), transform.position.y - descreaser, Random.Range(minZ, maxZ));
    }
}
