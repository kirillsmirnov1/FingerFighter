using System.Collections;
using System.Linq;
using FingerFighter.Control.Damage;
using FingerFighter.Model.Damage;
using UnityEngine;

namespace FingerFighter.View
{
    public class OnHitSpriteHighlight : MonoBehaviour
    {
        [SerializeField] private HitTaker hitTaker;
        [SerializeField] private Color highlightColor = Color.red;
        [SerializeField] private float tintDuration = 1f;
        [SerializeField] private SpriteRenderer[] sprites;

        private Color[] _defaultColors;
        private int _steps;
        private WaitForSeconds _wait;

        private void Awake()
        {
            _defaultColors = sprites.Select(s => s.color).ToArray();
            _steps = (int) (tintDuration / 0.01f);
            _wait = new WaitForSeconds(tintDuration / _steps);

            hitTaker.onHitTaken += OnHit;
        }

        private void OnDisable()
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = _defaultColors[i];
            }
        }

        private void OnHit(HitData obj)
        {
            StopAllCoroutines();
            StartCoroutine(HitHighlight());
        }

        private IEnumerator HitHighlight()
        {
            for (float step = 0; step <= _steps; step++)
            {
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i].color = Color.Lerp(highlightColor, _defaultColors[i], step / _steps);
                }

                yield return _wait;
            }
        }
    }
}