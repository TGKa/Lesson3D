using UnityEngine;

namespace AlarmSystem
{
    public class Thief : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";

        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _body;
        [SerializeField] private float _speedMovement;
        [SerializeField] private float _speedRotation;

        private void Update()
        {
            Move();
            Look();
        }

        private void Move()
        {
            Vector3 direction = new(Input.GetAxis(Horizontal), 0f, Input.GetAxis(Vertical));
            transform.Translate(direction * _speedMovement * Time.deltaTime);
        }

        private void Look()
        {
            _camera.Rotate(-Input.GetAxis(MouseY) * _speedRotation * Time.deltaTime * Vector3.right);
            _body.Rotate(Input.GetAxis(MouseX) * _speedRotation * Time.deltaTime * Vector3.up);
        }
    }
}
