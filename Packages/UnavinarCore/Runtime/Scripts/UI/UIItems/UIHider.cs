using UnityEngine;

namespace Unavinar.HCUnavinarCore
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIHider : UIElement
    {
        [SerializeField] private bool isHidden;
        [SerializeField] private float showDelay = 0.5f;
        [SerializeField] private float showTime = 0.3f;
        [SerializeField] private bool useUnscaledTime = true;
        
        [SerializeField] private float showAlpha = 1f;
        [SerializeField] private float hideAlpha;

        private CanvasGroup _canvasGroup;

        private float _showDelayTimer;
        private float _currentAlpha;
        private float _alphaChangeSpeed;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            if (showTime >= 0)
            {
                _alphaChangeSpeed = 1 / showTime;
            }
            else
            {
                _alphaChangeSpeed = float.MaxValue;
            }

            if (isHidden)
            {
                HideInstantly();
            }
        }

        private void Update()
        { 
            float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            if (isHidden && _currentAlpha <= 1f)
            {
                _showDelayTimer += Time.deltaTime;
                if(_showDelayTimer <= showDelay) return;

                _currentAlpha += _alphaChangeSpeed * deltaTime;

                if (_currentAlpha > 1)
                {
                    _currentAlpha = 1f;
                }
                
                UpdateCanvasGroup();
            }
            else if(!isHidden && _currentAlpha >= 0)
            {
                _showDelayTimer += Time.deltaTime;
                if(_showDelayTimer <= showDelay) return;
                
                _currentAlpha -= _alphaChangeSpeed * deltaTime;

                if (_currentAlpha < 0)
                {
                    _currentAlpha = 0f;
                }
                
                UpdateCanvasGroup(); 
            }
        }

        private void UpdateCanvasGroup()
        {
            if(_canvasGroup == null) return;
            
            float alpha = Mathf.SmoothStep(hideAlpha, showAlpha, 1 - _currentAlpha);
            
            _canvasGroup.alpha = alpha;
            _canvasGroup.interactable = !isHidden;
            _canvasGroup.blocksRaycasts = !isHidden;
        }

        public override void Show()
        {
            SetHidden(false);
        }

        public override void ShowInstantly()
        {
            isHidden = false;
            _currentAlpha = 0f;
            
            UpdateCanvasGroup();
        }

        public override void Hide()
        {
            SetHidden(true);
        }

        public override void HideInstantly()
        {
            isHidden = true;
            _currentAlpha = 1f;
            
            UpdateCanvasGroup();
        }

        private void SetHidden(bool isHidden)
        {
            this.isHidden = isHidden;
        }
    }
}

