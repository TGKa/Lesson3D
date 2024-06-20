using System.Collections;
using UnityEngine;

public class Shootting : MonoBehaviour
{
    [SerializeField] private Rigidbody _prefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] float _secondBetweenShot;

    void Start() =>
        StartCoroutine(Shoot());

    private IEnumerator Shoot()
    {
        var delay = new WaitForSeconds(_secondBetweenShot);

        while (true)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            var bullet = Instantiate(_prefab, transform.position + direction, Quaternion.identity);

            bullet.transform.up = direction;
            bullet.velocity = direction * _speed;

            yield return delay;
        }
    }
}
