using UnityEngine;

namespace Unavinar.HCUnavinarCore
{
    [RequireComponent(typeof(RectTransform))]
    public class UIRotator : MonoBehaviour
    {
        [SerializeField] private bool isRotating = true;
        [SerializeField] private float rotationSpeed = 40f;

        [SerializeField] private bool useUnscaledTime = true;

        private RectTransform _rectTransform;
        
        public bool IsRotation
        {
            get => isRotating;
            set => isRotating = value;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            
            if (isRotating)
            {
                _rectTransform.Rotate(Vector3.back * rotationSpeed * deltaTime);
            }
        }
    }
}

