using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int MinCountCube = 2;
    private const int MaxCountCube = 7;

    [SerializeField] private List<Cube> _cubes;
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _container;

    private Cube _currentCube;

    private void OnEnable()
    {
        foreach (var cube in _cubes)
            cube.CubeClicked += OnCubeClicked;
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
            cube.CubeClicked -= OnCubeClicked;
    }

    private void OnCubeClicked(Cube cube, bool canSpawn)
    {
        _currentCube = cube;

        if (canSpawn)
            Spawn(cube.transform.position);

        cube.CubeClicked -= OnCubeClicked;
        _cubes.Remove(cube);
    }

    private void Spawn(Vector3 position)
    {
        int countCubeSpawning = Random.Range(MinCountCube, MaxCountCube);
        
        for (int i = 0; i < countCubeSpawning; i++)
        {
            Cube newCube = Instantiate(_prefab, position, Quaternion.identity,_container);
            InitializeCube(newCube);
        }
    }

    private void InitializeCube(Cube cube)
    {
        float scale = _currentCube.transform.localScale.x / 2;
        int chanceFalling = _currentCube.ChanceFalling / 2;

        cube.Init(scale, chanceFalling);
        cube.CubeClicked += OnCubeClicked;
        _cubes.Add(cube);
    }
}
