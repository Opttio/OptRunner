using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _OprRunner.Scripts.Environment.Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _obstaclesPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _obstaclesHolder;
        [SerializeField] private int _spawnTimer;

        private Coroutine _spawnRoutine;

        private void Start()
        {
            if (_spawnRoutine != null)
                StopCoroutine(_spawnRoutine);
            _spawnRoutine = StartCoroutine(SpawnObstacleRoutine());
        }

        private IEnumerator SpawnObstacleRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnTimer);

                SpawnObstacle(ChooseObstacle());
            }
        }

        private void SpawnObstacle(GameObject obstacle)
        {
            Instantiate(obstacle, _spawnPoint.position, _spawnPoint.rotation, _obstaclesHolder);
        }

        private GameObject ChooseObstacle()
        {
            int randomIndex  = Random.Range(0, _obstaclesPrefab.Length);
            return _obstaclesPrefab[randomIndex];
        }
    }
}