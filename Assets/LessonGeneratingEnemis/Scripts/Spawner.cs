using System.Collections;
using UnityEngine;

namespace LessonGeneratingEnemy
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private Unit _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private float _delay;

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            var delay = new WaitForSeconds(_delay);

            while (true)
            {
                int index = Random.Range(0, _points.Length);
                Instantiate(_prefab, _points[index].position, _points[index].rotation, _container);

                yield return delay;
            }
        }
    }
}
