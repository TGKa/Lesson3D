using UnityEngine;

namespace LessonGeneratingEnemy
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}
