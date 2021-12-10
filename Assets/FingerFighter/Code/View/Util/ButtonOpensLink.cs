using UnityEngine;
using UnityEngine.UI;

namespace FingerFighter.View.Util
{
    [RequireComponent(typeof(Button))]
    public class ButtonOpensLink : MonoBehaviour // TODO UU 
    {
        [SerializeField] private string url;
        [SerializeField] private Button button;

        private void OnValidate() => button ??= GetComponent<Button>();
        private void Awake() => button.onClick.AddListener(OpenUrl);
        private void OpenUrl() => Application.OpenURL(url);
    }
}
