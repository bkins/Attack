using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyAi : MonoBehaviour
    {
        public  float     Speed = 2f;
        private Transform _player;


        private void Start()
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            
            if (playerObj != null)
            {
                _player = playerObj.transform;
            }
        }

        private void Update()
        {
            if (_player == null) return;

            var direction = (_player.position - transform.position).normalized;
            transform.position += direction * Speed * Time.deltaTime;
        }
    }
}