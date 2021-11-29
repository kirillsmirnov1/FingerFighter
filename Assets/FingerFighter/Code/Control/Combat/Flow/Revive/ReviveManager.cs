using UnityEngine;
using UnityUtils.Events;
using UnityUtils.Extensions;
using UnityUtils.Variables;

namespace FingerFighter.Control.Combat.Flow.Revive
{
    public class ReviveManager : MonoBehaviour
    {
        [SerializeField] private GameEvent reviveEvent;

        [SerializeField] private TransformVariable playerTransform;
        [SerializeField] private GameObject currentScore;
        [SerializeField] private ReviveBlast blast;
        [SerializeField] private float playerReviveDelay = 0.5f;
        
        
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
            currentScore.gameObject.SetActive(true);
            blast.Blast();
            this.DelayAction(playerReviveDelay, () => playerTransform.Value.gameObject.SetActive(true));
        }
    }
}