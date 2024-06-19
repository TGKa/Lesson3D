using System.Collections;
using UnityEngine;

namespace LessonGeneratingEnemy
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnerPoint[] _points;
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
                _points[index].Spawn();
                
                yield return delay;
            }
        }
    }
}
