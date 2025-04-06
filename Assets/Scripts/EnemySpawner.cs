using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject EnemyPrefab;
        public Transform  Player;

        public float SpawnInterval    = 5f;
        public float SpawnRadius      = 10f;
        public float MinSpawnDistance = 3f;
        public float MoveSpeed        = 2f;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if ( ! (_timer >= SpawnInterval)) return;

            _timer = 0f;
            SpawnEnemy();

            ApplyEnemyIntelligence();

        }

        private void ApplyEnemyIntelligence()
        {
            if (GameObject.FindGameObjectWithTag("Player") is not GameObject player) return;

            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * MoveSpeed * Time.deltaTime;
        }

        private void SpawnEnemy()
        {
            if (EnemyPrefab == null 
            || Player == null)
            {
                Debug.LogWarning("Missing reference(s) in EnemySpawner.");
                return;
            }

            Vector3 spawnPosition;
            do
            {
                //TODO: Test and tweak this 
                //OR
                //TODO: See commented out code below for another way to do this.  Which is better?

                // Random position in a circle around the player
                var randomCircle = Random.insideUnitCircle * SpawnRadius;

                spawnPosition = Player.position + new Vector3(randomCircle.x
                                                            , 0
                                                            , randomCircle.y);
            }
            while (Vector3.Distance(spawnPosition, Player.position) < MinSpawnDistance);

            Instantiate(EnemyPrefab
                      , spawnPosition
                      , Quaternion.identity);
        }
    }
}
/*
 *using UnityEngine;
   using System.Collections;
   
   public class EnemySpawner : MonoBehaviour
   {
       public GameObject enemyPrefab;
       public Transform player;
       public float spawnInterval = 3f;
       public float spawnRadius = 10f;
   
       void Start()
       {
           StartCoroutine(SpawnEnemies());
       }
   
       IEnumerator SpawnEnemies()
       {
           while (true)
           {
               yield return new WaitForSeconds(spawnInterval);
               Vector3 spawnPos = player.position + Random.insideUnitSphere * spawnRadius;
               spawnPos.y = player.position.y; // Keep enemy on the same horizontal plane
               Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
           }
       }
   }
   
 */