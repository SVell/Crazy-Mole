using UnityEngine;
using UnityEngine.UI;

namespace Unavinar.UI
{
    public class VictoryScreen : MonoBehaviour
    {
        [SerializeField] private Button nextLevelButton;

        private void Start()
        {
            AssignNextLevelButton();
        }

        private void AssignNextLevelButton()
        {
            if(nextLevelButton == null) return;
            
            // TODO: Add logic
        }
    }
}

