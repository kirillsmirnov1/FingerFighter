using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityUtils.Variables;
using UnityUtils.VisualEffects;

namespace FingerFighter.View.Display
{
    public class OnRoomEndView : UiFadePanel
    {
        [SerializeField] private TextMeshProUGUI scorePrompt;
        [SerializeField] private BoolVariable highScore;

        [SerializeField] private GameObject[] deathObjects;
        [SerializeField] private GameObject[] winObjects;

        public void ShowOnDeath()
        {
            Set(deathObjects, true);
            Set(winObjects, false);
            Show();
        }

        public void ShowOnWin()
        {
            Set(deathObjects, false);
            Set(winObjects, true);
            Show();
        }

        private static void Set(IReadOnlyList<GameObject> go, bool active)
        {
            for (var i = 0; i < go.Count; i++)
            {
                go[i].SetActive(active);
            }
        }

        private new void Show()
        {
            scorePrompt.text = highScore ? "NEW HIGH SCORE" : "score";
            scorePrompt.color = highScore ? Color.yellow : Color.white;
            base.Show();
        }
    }
}