using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCombat : MonoBehaviour
    {
        public int   AttackDamage = 20;
        public float AttackRange  = 2f;

        public LayerMask EnemyLayers;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
            }
        }

        private void Attack()
        {
            // Detect enemies in range
            var hitEnemies = Physics.OverlapSphere(transform.position
                                                 , AttackRange
                                                 , EnemyLayers);

            foreach (var enemy in hitEnemies)
            {
                var stats = enemy.GetComponent<CharacterStats>();

                if (stats != null)
                {
                    stats.TakeDamage(AttackDamage);
                }
            }

            Debug.Log("Player attacked!");
        }

        private void OnDrawGizmosSelected()
        {
            // Just for visualizing the attack range in the editor
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}