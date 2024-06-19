using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _startingForce;
    [SerializeField] private float _startingRadius;

    private float _force;
    private float _radius;

    private void OnEnable() =>
        _cube.Clicked += OnClicked;

    private void OnDisable()=>
        _cube.Clicked -= OnClicked;

    private void OnClicked(Cube cube, bool canExplode)
    {
        if (canExplode == false)
        {
            _force = _startingForce / transform.localScale.x;
            _radius = _startingRadius / transform.localScale.x;
            _collider.enabled = false;
            Explode();
        }
    }

    private void Explode()
    {
        foreach (var explodableobject in GetExplodableObject())
        {
            float explosionForce = GetForce(explodableobject);
            explodableobject.AddExplosionForce(explosionForce, transform.position, _radius);
        }
    }

    private List<Rigidbody> GetExplodableObject()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);
        List<Rigidbody> objects = new();

        foreach (var hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);
        
        return objects;
    }

    private float GetForce(Rigidbody explodableobject)
    {
        float distance = Mathf.Ceil(Vector3.Distance(transform.position, explodableobject.transform.position));
        
        return _force / distance;
    }
}
