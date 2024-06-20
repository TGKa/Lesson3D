using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int MinCountCube = 2;
    private const int MaxCountCube = 6;

    [SerializeField] private List<Cube> _cubes;
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private Cube _currentCube;

    private void OnEnable()
    {
        foreach (var cube in _cubes)
            cube.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
            cube.Clicked -= OnClicked;
    }

    private void OnClicked(Cube cube, bool canSpawn)
    {
        _currentCube = cube;

        if (canSpawn)
            Spawn(cube.transform.position);

        cube.Clicked -= OnClicked;
        _cubes.Remove(cube);
    }

    private void Spawn(Vector3 position)
    {
        int countCubeSpawning = Random.Range(MinCountCube, MaxCountCube + 1);
        
        for (int i = 0; i < countCubeSpawning; i++)
        {
            Cube newCube = Instantiate(_prefab, position, Quaternion.identity,_container);
            InitializeCube(newCube);
            ScatterCube(newCube,position);
        }
    }

    private void InitializeCube(Cube cube)
    {
        float scale = _currentCube.transform.localScale.x / 2;
        int chanceFalling = _currentCube.ChanceFalling / 2;

        cube.Init(scale, chanceFalling);
        cube.Clicked += OnClicked;
        _cubes.Add(cube);
    }

    private void ScatterCube(Cube cube, Vector3 explosionPosition)
    {
        if (cube.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            rigidbody.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
    }
}
