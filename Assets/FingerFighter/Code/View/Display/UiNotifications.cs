using TMPro;
using UnityEngine;
using UnityUtils.Extensions;
using UnityUtils.VisualEffects;

namespace FingerFighter.View.Display
{
    public class UiNotifications : MonoBehaviour
    {
        [SerializeField] private LazyFade notificationFade;
        [SerializeField] private TextMeshProUGUI notificationText;
        
        private static UiNotifications _instance;

        private void Awake()
        {
            _instance = this;
            notificationFade.gameObject.SetActive(false);
        }

        public static void Show(string text)
        {
            _instance.ShowImpl(text);
        }

        public void ShowImpl(string text)
        {
            StopAllCoroutines();
            notificationText.text = text;
            notificationFade.gameObject.SetActive(true);
            notificationFade.SetVisibility(true);
            this.DelayAction(3f, () => notificationFade.SetVisibility(false));
        }
    }
}
