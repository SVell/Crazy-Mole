using UnityEngine;

namespace Unavinar.HCUnavinarCore
{
    [RequireComponent(typeof(RectTransform))]
    public class UIBouncer : MonoBehaviour
    {
        [SerializeField] private bool isBouncing = true;
        [SerializeField] private float bounceTime = 0.5f;
        [SerializeField] private float bounceKoef = 0.05f;

        [SerializeField] private bool useUnscaledTime = true;

        private RectTransform _rectTransform;

        private bool _isGrowing = true;
        private float _bounceProgress;
        private float _bounceSpeed;
        
        public bool IsBouncing
        {
            get => isBouncing;
            set => isBouncing = value;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (bounceTime > 0)
            {
                _bounceSpeed = 1 / bounceTime;
            }
            else
            {
                _bounceSpeed = 0;
            }
        }

        private void Update()
        {
            float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            if (_isGrowing && _bounceProgress <= 1f)
            {
                _bounceProgress += _bounceSpeed * deltaTime;

                if (_bounceProgress > 1)
                {
                    _bounceProgress = 0;
                    _isGrowing = false;
                }
                
                UpdateScale();
            }
            else if (!_isGrowing && _bounceProgress >= 0)
            {
                _bounceProgress -= _bounceSpeed * deltaTime; 
                
                if (_bounceProgress < 0)
                {
                    _bounceProgress = 0;
                    _isGrowing = true;
                }
                
                UpdateScale();
            }
        }

        private void UpdateScale()
        {
            if (_rectTransform == null) return;

            float scaleProgress = 1 - _bounceProgress;

            var scale = Vector3.one;

            if (_isGrowing)
            {
                float bouncyKoef = 1 +
                                   MathHelper.LerpInvParabolaSmooth(
                                       Mathf.Clamp((scaleProgress - 0.25f) / 0.75f, 0, 1)) *
                                   bounceKoef;
                scale *= bouncyKoef;
            }

            _rectTransform.localScale = scale;
        }
    }
}
