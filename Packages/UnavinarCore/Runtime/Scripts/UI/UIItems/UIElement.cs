using UnityEngine;

namespace Unavinar.HCUnavinarCore
{
    public abstract class UIElement : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();
        public abstract void ShowInstantly();
        public abstract void HideInstantly();
    }
}

