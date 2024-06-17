using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private int _chanceFalling = 100;

    public event UnityAction<Cube,bool> CubeClicked;

    public int ChanceFalling => _chanceFalling;

    private void Start() =>
        SetColor();

    private void OnMouseDown()
    {
        CubeClicked?.Invoke(this,IsNewCubeSpawn());
        Explode();
        Destroy(gameObject);
    }

    public void Init(float scale, int chanceFalling)
    {
        transform.localScale = new Vector3(scale, scale, scale);
        _chanceFalling = chanceFalling;
    }

    private void Explode()
    {
        foreach (var explodableObject in GetExplodableObject())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
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

    private void SetColor()
    {
        float randomRed = Random.Range(0f, 1f);
        float randomGreen = Random.Range(0f, 1f);
        float randomBlue = Random.Range(0f, 1f);

        _mesh.material.color = new Color(randomRed, randomGreen, randomBlue);
    }

    private bool IsNewCubeSpawn()
    {
        int maxNumberRandom = 101;
        int random = Random.Range(1, maxNumberRandom);

        return random <= _chanceFalling ? true : false;
    }
}
