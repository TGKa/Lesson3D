using UnityEngine;

public class Mover : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime);
    }
}
