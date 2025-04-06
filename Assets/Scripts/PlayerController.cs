using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float MoveSpeed    = 5f;
        public int   AttackDamage = 10;
        public float AttackRange  = 2f;

        private Rigidbody _rigidBody;
        private Vector3   _movement;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //Get input
            _movement = new Vector3(Input.GetAxis("Horizontal")
                                  , 0
                                  , Input.GetAxis("Vertical"));

            // Rotate if moving
            RotatePlayer(_movement);

            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
            }
        }

        private void FixedUpdate()
        {
            // Move the player
            _rigidBody.MovePosition(_rigidBody.position + _movement * MoveSpeed * Time.fixedDeltaTime);
        }

        private void Attack()
        {
            var hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, AttackRange);
        
            foreach (var enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
                }
            }
        }

        private void RotatePlayer(Vector3 direction)
        {
            if (direction.sqrMagnitude == 0) return;

            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

    }
}