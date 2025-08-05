using UnityEngine;

namespace _OprRunner.Scripts.Environment.Boosters
{
    public class CoinDestroyer : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private LayerMask _noSpawnZoneMask;
        
        private void Update()
        {
            MoveToClearPoint();
        }

        private void MoveToClearPoint()
        {
            transform.Translate(Vector3.back * (_speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ObstaclesCleaner"))
                Destroy(gameObject);
            
            if (((1 << other.gameObject.layer) & _noSpawnZoneMask) != 0) 
                Destroy(gameObject);
        }
    }
}