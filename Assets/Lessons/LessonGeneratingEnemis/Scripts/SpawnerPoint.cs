using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LessonGeneratingEnemy
{
    public class SpawnerPoint : MonoBehaviour
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _target;

        public void Spawn()
        {
           Enemy enemy =  Instantiate(_prefab, transform.position, transform.rotation, _container);
            enemy.SetTarget(_target);
        }
    }
}