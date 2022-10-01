using System;
using Unavinar.HCUnavinarCore;
using UnityEngine;

namespace Unavinar.Managers
{
    [Serializable]
    public enum GameState
    {
        None,
        LoadingMainMenu,
        MainMenu,
        SettingsMenu,
        Game,
        Victory,
        Defeat
    }

    public class GameManager : StaticInstance<GameManager>
    {
        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;

        public GameState State { get; private set; }

        // Kick the game off with the first state
        void Start()
        {
            Application.targetFrameRate = 60;
            ChangeState(GameState.MainMenu);
            
#if DEVELOPMENT_BUILD || UNITY_EDITOR 
            SRDebug.Init();
#endif
        }

        public void ChangeState(GameState newState)
        {
            OnBeforeStateChanged?.Invoke(newState);

            State = newState;
            switch (newState)
            {
                case GameState.None:
                    break;
                case GameState.LoadingMainMenu:
                    break;
                case GameState.MainMenu:
                    break;
                case GameState.SettingsMenu:
                    break;
                case GameState.Game:
                    break;
                case GameState.Victory:
                    break;
                case GameState.Defeat:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnAfterStateChanged?.Invoke(newState);

            Debug.Log($"<color=green>New state: {newState}</color>", this);
        }
    }
}