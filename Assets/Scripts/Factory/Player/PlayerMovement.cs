using UnityEngine;

namespace Factory.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private float _speed = 10f;

        private Vector2 _movementInput;

        private void Update()
        {
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");

            var cameraTransform = _camera.transform;
            var cameraForward = cameraTransform.forward;
            var cameraRight = cameraTransform.right;

            cameraForward.y = cameraRight.y = 0;
            var direction = (cameraForward * _movementInput.y + cameraRight * _movementInput.x).normalized;

            _rigidbody.MovePosition(transform.position + direction * (_speed * Time.deltaTime));
        }
    }
}