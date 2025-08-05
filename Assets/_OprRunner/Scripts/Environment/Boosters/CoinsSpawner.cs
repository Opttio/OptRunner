using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _OprRunner.Scripts.Environment.Boosters
{
    public class CoinsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _coinHolder;
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
                yield return new WaitForSeconds(_spawnTimer);
                SpawnCoin(_coinPrefab);
            }
        }
        
        private void SpawnCoin(GameObject obstacle)
        {
            Instantiate(obstacle, ChooseSpawnPoint().position, Quaternion.identity, _coinHolder);
        }

        private Transform ChooseSpawnPoint()
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[randomIndex];
        }
    }
}