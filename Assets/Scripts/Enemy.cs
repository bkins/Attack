using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public int MaxHp   = 30;
        public int Defense = 2;

        public int CurrentHp;

        private void Start()
        {
            CurrentHp = MaxHp;
        }

        public void TakeDamage(int damage)
        {
            var effectiveDamage = Mathf.Max(damage - Defense, 1);

            CurrentHp -= effectiveDamage;
            Debug.Log(gameObject.name + " took " + effectiveDamage + " damage.");

            if (CurrentHp <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // For now, just destroy the enemy.
            Destroy(gameObject);
        }
    }
}