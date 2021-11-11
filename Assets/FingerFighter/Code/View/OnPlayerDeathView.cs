using TMPro;
using UnityEngine;
using UnityUtils.Variables;
using UnityUtils.VisualEffects;

namespace FingerFighter.View
{
    public class OnPlayerDeathView : UiFadePanel
    {
        [SerializeField] private TextMeshProUGUI scorePrompt;
        [SerializeField] private BoolVariable highScore;
        
        public override void Show()
        {
            scorePrompt.text = highScore ? "NEW HIGH SCORE" : "score";
            scorePrompt.color = highScore ? Color.yellow : Color.white;
            base.Show();
        }
    }
}