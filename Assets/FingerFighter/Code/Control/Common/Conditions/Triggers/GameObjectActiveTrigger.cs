using UnityEngine;

namespace FingerFighter.Control.Common.Conditions.Triggers
{
    public class GameObjectActiveTrigger : ATrigger
    {
        [SerializeField] private GameObject subject;
        
        protected override void InvokeTrigger(bool flagIsOn) 
            => subject.SetActive(flagIsOn);
    }
}