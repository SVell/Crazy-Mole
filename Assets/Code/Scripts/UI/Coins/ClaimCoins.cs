using System.Collections.Generic;
using TMPro;
using Unavinar.HCUnavinarCore;
using UnityEngine;
using UnityEngine.UI;

namespace Unavinar.UI
{
    public class ClaimCoins : MonoBehaviour
    {
        [SerializeField] private Button nextButton;
        
        [SerializeField] private RectTransform coinsSpawnPosition;
        [SerializeField] private RectTransform coinsDestination;

        [SerializeField] private UICoin coinsPrefab;

        [SerializeField] private int coinsSpawnAmount;
        [SerializeField] private float coinsCircleRadius;

        [SerializeField] private TextMeshProUGUI coinsForLevelText;
        [SerializeField] private TextMeshProUGUI totalCoinsText;

        private SavableValue<int> _totalCoins;
        
        private int _coinsForLevel;

        private List<RectTransform> _coins = new();

        public int CoinsForLevel
        {
            get => _coinsForLevel;
            set => _coinsForLevel = value;
        }

        private void Awake()
        {
            _totalCoins = new SavableValue<int>("Total Coins");
        }

        private void Start()
        {
            totalCoinsText.text = _totalCoins.ToString();
            coinsForLevelText.text = _coinsForLevel.ToString();
            
            NextButtonLogic();
        }

        public void AnimateCoins()
        {
            for (int i = 0; i < coinsSpawnAmount; ++i)
            {
                UICoin c = Instantiate(coinsPrefab, coinsSpawnPosition.position, Quaternion.identity, transform);
                Vector2 circle = coinsSpawnPosition.position + (Vector3)(Random.insideUnitCircle * coinsCircleRadius);
                
                c.AddDestination(circle);
                c.AddDestination(coinsDestination.position);

                c.ShouldAnimate = true;
            }
        }

        private void NextButtonLogic()
        {
            nextButton.onClick.AddListener(AnimateCoins);
            
            // TODO: Add your own logic on after button press
        }
    }
}
