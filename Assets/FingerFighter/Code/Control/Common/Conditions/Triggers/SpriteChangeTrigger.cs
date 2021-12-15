using UnityEngine;

namespace FingerFighter.Control.Common.Conditions.Triggers
{
    public class SpriteChangeTrigger : ATrigger
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;
        
        protected override void InvokeTrigger(bool flagIsOn) 
            => spriteRenderer.sprite = flagIsOn ? onSprite : offSprite;
    }
}