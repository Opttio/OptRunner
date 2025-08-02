using UnityEngine;

namespace _OprRunner.Scripts.Environment.Road
{
    public class RoadTextureMoving : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private float _offsetY;
        [Range(0f, 4f)]
        [SerializeField] private float _speed;
        [Space]
        [SerializeField] private Transform _stripesHolder;
        [SerializeField] private Transform _leftStripe;
        [SerializeField] private Transform _middleStripe;
        [SerializeField] private Transform _rightStripe;

        private void Update()
        {
            MoveTextureOffsetOnY();
        }

        private void MoveTextureOffsetOnY()
        {
            _offsetY -= Time.deltaTime * _speed;
            if (_offsetY > 1f)
                _offsetY -= 1f;
            if (_offsetY < -1f)
                _offsetY += 1f;
            _material.mainTextureOffset = new Vector2(_material.mainTextureOffset.x, _offsetY);
        }
    }
}
