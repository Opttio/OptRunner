using UnityEngine;

namespace _OprRunner.Scripts.Environment.Obstacles
{
    public class ObstaclesDestroyer : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
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
        }
    }
}