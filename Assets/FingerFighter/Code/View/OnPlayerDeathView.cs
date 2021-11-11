using TMPro;
using UnityEngine;
using UnityUtils.VisualEffects;

namespace FingerFighter.View
{
    public class OnPlayerDeathView : UiFadePanel
    {
        [SerializeField] private TextMeshProUGUI scorePrompt;
        
        public void Show(bool highScore)
        {
            scorePrompt.text = highScore ? "NEW HIGH SCORE" : "score";
            scorePrompt.color = highScore ? Color.yellow : Color.white;
            Show();
        }
    }
}