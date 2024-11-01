#if DOOZY
using Doozy.Runtime.UIManager.Components;
#endif
using UnityEngine.Events;
using UnityEngine.UI;

namespace BreakTheCycle.Util.Extensions
{
    public static class ButtonExtensions
    {
        public static void AddListener(this Button button, UnityAction listener, bool removePreviousListener = false)
        {
            if (!button)
            {
                return;
            }
#if DOOZY
			if (button.GetComponent<UIButton>() != null)
			{
				button.GetComponent<UIButton>().AddListener(listener, removePreviousListener);
				return;
			}
#endif

            if (removePreviousListener)
            {
                button.onClick.RemoveAllListeners();
            }
            button.onClick.AddListener(listener);
        }
#if DOOZY
        public static void AddListener(this UIButton uiButton, UnityAction listener, bool removePreviousListener = false, float cooldown = 0f)
        {
            if (!uiButton)
            {
                return;
            }
            if (removePreviousListener)
            {
                uiButton.onClickBehaviour.Event.RemoveAllListeners();
            }
            uiButton.Cooldown = cooldown;
            uiButton.onClickBehaviour.Event.AddListener(listener);
        }

#endif
#if DOOZY
        public static void Interactable(this UIButton uiButton, bool isInteractable)
        {
            uiButton.interactable = isInteractable;
        }
#endif
    }
}