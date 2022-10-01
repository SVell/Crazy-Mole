using System;
using Unavinar.HCUnavinarCore;
using Unavinar.Managers;
using UnityEngine;

namespace Unavinar.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIElement mainMenu;
        [SerializeField] private UIElement mainGame;
        [SerializeField] private UIElement victoryScreen;
        [SerializeField] private UIElement defeatScreen;

        private void OnEnable()
        {
            GameManager.OnAfterStateChanged += OnGameStateChange;
        }

        private void OnDisable()
        {
            GameManager.OnAfterStateChanged -= OnGameStateChange;
        }

        private void OnGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.None:
                    break;
                case GameState.LoadingMainMenu:
                    break;
                case GameState.MainMenu:
                    mainMenu.gameObject.SetActive(true);
                    mainMenu.Show();
                    break;
                case GameState.SettingsMenu:
                    mainMenu.HideInstantly();
                    break;
                case GameState.Game:
                    mainMenu.HideInstantly();
                    mainGame.gameObject.SetActive(true);
                    mainGame.ShowInstantly();
                    break;
                case GameState.Victory:
                    mainGame.HideInstantly();
                    victoryScreen.gameObject.SetActive(true);
                    victoryScreen.Show();
                    break;
                case GameState.Defeat:
                    mainGame.HideInstantly();
                    defeatScreen.gameObject.SetActive(true);
                    defeatScreen.Show();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }
    }
}

