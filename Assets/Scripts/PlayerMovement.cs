using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public float MoveSpeed     = 5f;
        public float Gravity       = -9.81f;
        public float JumpHeight    = 1.5f;
        public float RotationSpeed = 10f;

        private CharacterController _controller;
        private Vector3             _velocity;
        private bool                _isGrounded;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _isGrounded = _controller.isGrounded;

            if (_isGrounded
            && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            var moveX = Input.GetAxis("Horizontal");
            var moveZ = Input.GetAxis("Vertical");

            var move           = transform.right * moveX + transform.forward * moveZ;
            var inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            
            if (inputDirection.sqrMagnitude >= 0.01f)
            {
                // Rotate toward movement direction
                var targetRotation = Quaternion.LookRotation(inputDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation
                                                    , targetRotation
                                                    , RotationSpeed * Time.deltaTime);
            }

            _controller.Move(inputDirection.normalized * MoveSpeed * Time.deltaTime);

            //if (move.magnitude >= 0.1f)
            //{
            //    // Rotate toward movement direction
            //    var targetRotation = Quaternion.LookRotation(move);
            //    transform.rotation = Quaternion.Slerp(transform.rotation
            //                                        , targetRotation
            //                                        , RotationSpeed * Time.deltaTime);
            //}

            //_controller.Move(move * MoveSpeed * Time.deltaTime);

            // Jumping
            if (_isGrounded
            && Input.GetButtonDown("Jump"))
            {
                _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            _velocity.y += Gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}