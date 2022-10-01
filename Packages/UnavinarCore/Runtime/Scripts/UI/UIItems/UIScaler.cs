using UnityEngine;

namespace Unavinar.HCUnavinarCore
{
    [RequireComponent(typeof(RectTransform))]
    public class UIScaler : UIElement
    {
        [SerializeField] private bool isHidden;
        [SerializeField] private float showDelay = 0.5f;
        [SerializeField] private float showTime = 0.5f;
        
        [SerializeField] private Vector3 minScale = Vector3.zero;
        [SerializeField] private Vector3 maxScale = Vector3.one;

        [SerializeField] private float bounceKoef = 0.4f;

        [SerializeField] private bool useUnscaledTime = true;

        private RectTransform _rectTransform;

        private float _showDelayTimer;
        private float _showProgress;
        private float _showSpeed;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (showTime > 0)
            {
                _showSpeed = 1 / showTime;
            }
            else
            {
                _showSpeed = float.MaxValue;
            }

            if (isHidden)
            {
                HideInstantly();
            }
        }

        private void Update()
        {
            float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            if (isHidden && _showProgress <= 1f)
            {
                _showDelayTimer += Time.deltaTime;
                if(_showDelayTimer <= showDelay) return;
                
                _showProgress += _showSpeed * deltaTime;

                if (_showProgress > 1)
                {
                    _showProgress = 1f;
                }
                
                UpdateScale();
            }
            else if(!isHidden && _showProgress >= 0)
            {
                _showDelayTimer += Time.deltaTime;
                if(_showDelayTimer <= showDelay) return;
                
                _showProgress -= _showSpeed * deltaTime;

                if (_showProgress < 0)
                {
                    _showProgress = 0f;
                }
                
                UpdateScale(); 
            }
        }

        private void UpdateScale()
        {
            if (_rectTransform == null) return;

            float showProgress = 1 - _showProgress;

            var scale = new Vector3(
                Mathf.SmoothStep(minScale.x, maxScale.x, showProgress),
                Mathf.SmoothStep(minScale.y, maxScale.y, showProgress),
                Mathf.SmoothStep(minScale.z, maxScale.z, showProgress));

            if (!isHidden)
            {
                float bouncyKoef = 1 +
                                   MathHelper.LerpInvParabolaSmooth(Mathf.Clamp((showProgress - 0.25f) / 0.75f, 0, 1)) *
                                   bounceKoef;
                scale *= bouncyKoef;
            }

            _rectTransform.localScale = scale;
        }
        
        private void SetHidden(bool isHidden)
        {
            this.isHidden = isHidden;
        }
        
        public override void Show()
        {
            SetHidden(false);
        }

        public override void Hide()
        {
            SetHidden(true);
        }

        public override void ShowInstantly()
        {
            isHidden = false;
            _showProgress = 0f;
            
            UpdateScale();
        }

        public override void HideInstantly()
        {
            isHidden = true;
            _showProgress = 1f;
            
            UpdateScale();
        }
    }
}

