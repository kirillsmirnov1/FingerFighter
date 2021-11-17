using FingerFighter.Control.Combat.Flow;
using TMPro;
using UnityEngine;

namespace FingerFighter.View.TextColorAnim
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FormationSpawnInfoDisplay : TmpTextFade
    {
        private void Awake() 
            => RunnerFlow.State.OnRoomEntered += OnRoomEntered;

        private void OnDestroy() 
            => RunnerFlow.State.OnRoomEntered -= OnRoomEntered;

        private void OnRoomEntered(string roomName)
        {
            SetText(roomName);
            ResetDurationTimer();
        }
    }
}