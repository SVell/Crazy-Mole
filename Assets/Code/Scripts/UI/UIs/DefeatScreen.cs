using System;
using UnityEngine;
using UnityEngine.UI;

namespace Unavinar.UI
{
    public class DefeatScreen : MonoBehaviour
    {
        [SerializeField] private Button restartLevelButton;

        private void Start()
        {
            AssignRestartLevelButton();
        }

        private void AssignRestartLevelButton()
        {
            if(restartLevelButton == null) return;
            
            // TODO: Add logic
        }
    }
}

