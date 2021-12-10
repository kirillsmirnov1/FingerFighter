using System;
using FingerFighter.View.Display;
using UnityEngine;
using UnityUtils;
using UnityUtils.Events;
using UnityUtils.Extensions;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat.Flow.Revive
{
    public class ReviveManager : MonoBehaviour
    {
        [SerializeField] private GameEvent reviveEvent;

        [SerializeField] private TransformVariable playerTransform;
        [SerializeField] private OnRoomEndView playerDeathView;
        [SerializeField] private GameObject currentScore;
        [SerializeField] private ReviveBlast blast;
        [SerializeField] private float reviveBlastDelay = 0.2f;
        [SerializeField] private float playerReviveDelay = 0.5f;
        [SerializeField] private FloatVariable baseHealth;
        [SerializeField] private FloatVariable currentHealth;

        private void OnValidate()
        {
            this.CheckNullFieldsIfNotPrefab();
        }

        private void Awake()
        {
            reviveEvent.RegisterAction(OnRevive);
        }

        private void OnDestroy()
        {
            reviveEvent.UnregisterAction(OnRevive);
        }

        private void OnRevive()
        {
            currentHealth.Value = baseHealth;
            playerDeathView.Hide();
            currentScore.gameObject.SetActive(true);
            this.DelayAction(reviveBlastDelay, () => blast.Blast());
            this.DelayAction(playerReviveDelay, () => playerTransform.Value.gameObject.SetActive(true));
        }
    }
}