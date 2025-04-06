using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterStats : MonoBehaviour
    {
        public int MaxHealth = 100;
        public int Defense   = 0;

        public int CurrentHealth;

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            var finalDamage = Mathf.Max(damage - Defense, 0);
            
            CurrentHealth -= finalDamage;
            
            Debug.Log($"{gameObject.name} took {finalDamage} damage. Remaining HP: {CurrentHealth}");

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name} died.");
            Destroy(gameObject);
        }
    }
}