using UnityEngine;
using UnityEngine.Events;

namespace CubesRain
{
    public class Cube : MonoBehaviour
    {
        private const float _minTimeLive = 2;
        private const float _maxTimeLive = 5;

        [SerializeField] private MeshRenderer _mesh;

        private Color _originalColor;
        private bool _haveFirstCollision = false;

        public event UnityAction<Cube> Destroying;

        private void Awake() =>
            _originalColor = _mesh.material.color;

        private void OnDisable()
        {
            _haveFirstCollision = false;
            _mesh.material.color = _originalColor;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_haveFirstCollision == false && collision.collider.GetComponent<Platform>() != null)
            {
                _haveFirstCollision = true;
                float time = Random.Range(_minTimeLive, _maxTimeLive);

                ChangeColor();
                Invoke(nameof(Destroy), time);
            }
        }

        private void ChangeColor()
        {
            float red = Random.Range(0f, 1f);
            float green = Random.Range(0f, 1f);
            float blue = Random.Range(0f, 1f);

            _mesh.material.color = new Color(red, green, blue);
        }

        private void Destroy() =>
            Destroying?.Invoke(this);
    }
}
