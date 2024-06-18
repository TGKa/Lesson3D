using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _startingExplosionForce;
    [SerializeField] private float _startingExplosionRadius;

    private float _explosionForce;
    private float _explosionRadius;

    private void OnEnable() =>
        _cube.Destroyed += OnDestroyed;

    private void OnDisable()=>
        _cube.Destroyed -= OnDestroyed;

    private void OnDestroyed()
    {
        _explosionForce = _startingExplosionForce / transform.localScale.x;
        _explosionRadius = _startingExplosionRadius / transform.localScale.x;
        _collider.enabled = false;
        Explode();
    }

    private void Explode()
    {
        foreach (var explodableobject in GetExplodableObject())
        {
            float explosionForce = GetExplosionForce(explodableobject);
            explodableobject.AddExplosionForce(explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObject()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);
        List<Rigidbody> objects = new();

        foreach (var hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);
        
        return objects;
    }

    private float GetExplosionForce(Rigidbody explodableobject)
    {
        float distance = Mathf.Ceil(Vector3.Distance(transform.position, explodableobject.transform.position));
        
        return _explosionForce / distance;
    }
}
