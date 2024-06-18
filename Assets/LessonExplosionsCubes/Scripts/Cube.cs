using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    
    private int _chanceFalling = 100;

    public event UnityAction<Cube,bool> Clicked;
    public event UnityAction Destroyed;

    public int ChanceFalling => _chanceFalling;

    private void Start() =>
        SetColor();

    private void OnMouseDown()
    {
        bool isNewSpawn = IsNewSpawn();
        Clicked?.Invoke(this, isNewSpawn);

        if (isNewSpawn == false)
            Destroyed?.Invoke();

        Destroy(gameObject);
    }

    public void Init(float scale, int chanceFalling)
    {
        transform.localScale = new Vector3(scale, scale, scale);
        _chanceFalling = chanceFalling;
    }

    private void SetColor()
    {
        float randomRed = Random.Range(0f, 1f);
        float randomGreen = Random.Range(0f, 1f);
        float randomBlue = Random.Range(0f, 1f);

        _mesh.material.color = new Color(randomRed, randomGreen, randomBlue);
    }

    private bool IsNewSpawn()
    {
        int maxNumberRandom = 100;
        int random = Random.Range(1, maxNumberRandom + 1);

        return random <= _chanceFalling ? true : false;
    }
}
