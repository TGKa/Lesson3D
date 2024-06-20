using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace CubesRain
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Cube _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private float _minPositionX;
        [SerializeField] private float _maxPositionX;
        [SerializeField] private float _minPositionZ;
        [SerializeField] private float _maxPositionZ;
        [SerializeField] private float _positionY;
        [SerializeField] private float _delay;

        private ObjectPool<Cube> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Cube>(
                createFunc: () => CreateFunc(),
                actionOnGet: (cube) => cube.gameObject.SetActive(true),
                actionOnRelease: (cube) => cube.gameObject.SetActive(false),
                actionOnDestroy: (cube) => ActionOnDestroy(cube));
        }

        private void Start() =>
            StartCoroutine(Spawn());

        private Cube CreateFunc()
        {
            Cube cube =  Instantiate(_prefab, _container);
            cube.Destroying += OnDestroying;

            return cube;
        }

        private void ActionOnDestroy(Cube cube)
        {
            cube.Destroying -= OnDestroying;
            Destroy(gameObject);
        }

        private void OnDestroying(Cube cube) =>
            _pool.Release(cube);

        private IEnumerator Spawn()
        {
            var delay = new WaitForSeconds(_delay);

            while (true)
            {
                Cube cube = _pool.Get();
                cube.transform.position = GetPositionSpawn();

                yield return delay;
            }
        }

        private Vector3 GetPositionSpawn()
        {
            float positionX = Random.Range(_minPositionX, _maxPositionX);
            float positionZ = Random.Range(_minPositionZ, _maxPositionZ);

            return new Vector3(positionX, _positionY, positionZ);
        }
    }
}
