using System.Security.Cryptography;

using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCombat : MonoBehaviour
    {
        public int   AttackDamage = 20;
        public float AttackRange  = 2f;

        public LayerMask EnemyLayers;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if ( ! Input.GetKeyDown(Constants.AttackButton)) return;

            _animator.SetTrigger(Constants.AttackTrigger);
            Attack();
        }

        private void Attack()
        {
            // Detect enemies in range
            var hitEnemies = Physics.OverlapSphere(transform.position
                                                 , AttackRange
                                                 , EnemyLayers);

            SetStats(hitEnemies);

            Debug.Log("Player attacked!");
        }

        private CharacterStats SetStats(Collider[] hitEnemies)
        {
            foreach (var enemy in hitEnemies)
            {
                var stats = enemy.GetComponent<CharacterStats>();

                if (stats == null) return stats;

                stats.TakeDamage(AttackDamage);
                return stats;
            }

            return null;
        }

        private void OnDrawGizmosSelected()
        {
            // Just for visualizing the attack range in the editor
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}