using Unavinar.HCUnavinarCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unavinar.Managers
{
    public class LevelManager : PersistentSingleton<LevelManager>
    {
        [SerializeField] private int[] playableLevel;

        private SavableValue<int> level;
        private SavableValue<int> totalLevelsCompleted;
        public int CurrentLevel => playableLevel[level.Value] + 1;
        public int TotalLevels => totalLevelsCompleted.Value;

        protected override void Awake()
        {
            base.Awake();

            level = new SavableValue<int>(Constants.Constants.LEVEL);
            totalLevelsCompleted = new SavableValue<int>(Constants.Constants.TOTAL_LEVEL);
        }

        public void LoadLevel(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void LoadNextLevel()
        {
            level.Value++;
            totalLevelsCompleted.Value++;

            if (level.Value >= playableLevel.Length)
            {
                level.Value = 0;
            }
            
            LoadLevel(level.Value);
        }

        public void RestartLevel()
        {
            LoadLevel(playableLevel[level.Value]);
        }
    }
}
