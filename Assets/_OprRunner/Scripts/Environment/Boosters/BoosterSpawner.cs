using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _OprRunner.Scripts.Environment.Boosters
{
    public class BoosterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _boosterPrefabs;
        [Range(0f, 100f)] [SerializeField] private float _invulnerabilityChance;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _boosterHolder;
        [SerializeField] private float _spawnTimer;
        
        private Coroutine _spawnRoutine;

        private void Start()
        {
            if (_spawnRoutine != null)
                StopCoroutine(_spawnRoutine);
            _spawnRoutine = StartCoroutine(SpawnCoinRoutine());
        }

        private IEnumerator SpawnCoinRoutine()
        {
            while (true)
            {
                int randomIndex = Random.Range(0, _boosterPrefabs.Length);
                var booster = _boosterPrefabs[randomIndex];
                yield return new WaitForSeconds(_spawnTimer);
                if (booster.CompareTag("Coin"))
                {
                    SpawnBooster(booster);
                }

                if (booster.CompareTag("Invulnerability"))
                {
                    float roll = Random.Range(0f, 100f);
                    if (roll <= _invulnerabilityChance)
                        SpawnBooster(booster);
                }
                
            }
        }
        
        private void SpawnBooster(GameObject obstacle)
        {
            Instantiate(obstacle, ChooseSpawnPoint().position, Quaternion.identity, _boosterHolder);
        }

        private Transform ChooseSpawnPoint()
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[randomIndex];
        }
    }
}