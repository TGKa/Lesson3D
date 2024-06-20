using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _pathPoints;
    private int _currentPoint;

    void Start()
    {
        _pathPoints = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _pathPoints[i] = _path.GetChild(i);

        transform.LookAt(_pathPoints[_currentPoint]);
    }

    public void Update()
    {
        if (transform.position == _pathPoints[_currentPoint].position)
            SelectNextPoint();

        transform.position = Vector3.MoveTowards(transform.position, _pathPoints[_currentPoint].position, _speed * Time.deltaTime);
    }

    public void SelectNextPoint()
    {
        _currentPoint = (_currentPoint + 1) % _pathPoints.Length;

        transform.LookAt(_pathPoints[_currentPoint]);
    }
}
