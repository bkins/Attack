using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<EnemyTypeSo> EnemyTypes = new();
        public Transform         Player;
        public float             SpawnRadius      = 10f;
        public float             MinSpawnDistance = 3f;
        public AnimationCurve    DifficultyCurve;

        private float       _elapsedTime = 0f;
        private List<float> _timers;

        private void Start()
        {
            
        }
        private void Awake()
        {
            //Safer to initialize `_timers` here, in case any other script's Start() references this spawner and expects `_timers` to be ready.
            _timers = new List<float>(new float[EnemyTypes.Count]);
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            var difficultyMultiplier = Mathf.Clamp(DifficultyCurve.Evaluate(_elapsedTime), 0.1f, 5f);

            for (var i = 0; i < EnemyTypes.Count; i++)
            {
                _timers[i] += Time.deltaTime;
                var scaledInterval = EnemyTypes[i].SpawnInterval / difficultyMultiplier;

                if (!(_timers[i] >= scaledInterval)) continue;
                
                _timers[i] = 0f;
                SpawnEnemy(EnemyTypes[i]);
            }
        }

        private void SpawnEnemy(EnemyTypeSo enemyType)
        {
            Vector3 spawnPosition;
            do
            {
                var randomCircle = Random.insideUnitCircle * SpawnRadius;
                spawnPosition = Player.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            }
            while (Vector3.Distance(spawnPosition, Player.position) < MinSpawnDistance);

            var enemy = Instantiate(enemyType.Prefab, spawnPosition, Quaternion.identity);

            var foundAi = enemy.GetComponent<EnemyAi>();

            if (foundAi) foundAi.Speed = enemyType.MoveSpeed;
        }
    }
}
