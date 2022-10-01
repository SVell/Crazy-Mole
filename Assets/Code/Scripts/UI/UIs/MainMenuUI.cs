using Unavinar.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Unavinar.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;

        private void Start()
        {
            AssignPlayButton();
        }

        private void AssignPlayButton()
        {
            if(playButton == null) return;
            
            playButton.onClick.AddListener(() =>
            {
                GameManager.Instance.ChangeState(GameState.Game);
            });
        }
    }
}

