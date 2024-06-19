using UnityEngine;

namespace LessonGeneratingEnemy
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Transform[] _pathPoints;
        [SerializeField] private float _speed;
        [SerializeField] private int _currentPoint;


        private void Start() =>
            SetCurrentPoint();

        private void Update()
        {
            if (transform.position == _pathPoints[_currentPoint].position)
                SetCurrentPoint();

            transform.position = 
                Vector3.MoveTowards(transform.position, _pathPoints[_currentPoint].position, _speed * Time.deltaTime);
        }

        private void SetCurrentPoint()
        {
            _currentPoint = (_currentPoint + 1) % _pathPoints.Length;
            transform.LookAt(_pathPoints[_currentPoint]);
        }
    }
}
