using System.Collections.Generic;
using UnityEngine;

namespace Unavinar.UI
{
    public class UICoin : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private RectTransform _rectTransform;
        
        private int _currentDestinationIndex;
        private List<Vector2> _destinations = new();

        public bool ShouldAnimate { get; set; }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            MoveCoin();
        }

        public void AddDestination(Vector2 destination)
        {
            _destinations.Add(destination);
        }

        private void MoveCoin()
        {
            if (!ShouldAnimate && _destinations.Count > 0) return;


            if (_currentDestinationIndex >= _destinations.Count)
            {
                // TODO: Add your own logic on destination arrived

                Destroy(gameObject);
                return;
            }

            _rectTransform.position = Vector3.Slerp(_rectTransform.position, _destinations[_currentDestinationIndex],
                speed * Time.deltaTime);

            if (Vector3.Distance(_rectTransform.position, _destinations[_currentDestinationIndex]) <= 0.1f)
            {
                _currentDestinationIndex++;
            }
        }
    }
}
