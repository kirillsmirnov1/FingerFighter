using System.Collections;
using FingerFighter.Control.Factories.EnemySpawn;
using TMPro;
using UnityEngine;

namespace FingerFighter.View.Display
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FormationSpawnInfoDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void OnValidate()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Awake()
        {
            SetTextAlpha(0f);
            EnemySpawnFormation.OnFormationSpawned += DisplayFormationInfo;
        }

        private void OnDestroy()
        {
            EnemySpawnFormation.OnFormationSpawned -= DisplayFormationInfo;
        }

        private void DisplayFormationInfo(string packId, string formationId)
        {
            var str = $"{packId} : {formationId}";
            text.text = str;
            StartCoroutine(FadeCoroutine());
        }

        private IEnumerator FadeCoroutine()
        {
            var steps = 100; // FIXME combine with HealthChangeDisplay 
            var time = 2f;
            var wfs = new WaitForSeconds(time / steps);
            for (float i = 0; i <= steps; i++)
            {
                SetTextAlpha(1f - i / steps);
                yield return wfs;
            }
        }

        private void SetTextAlpha(float alpha)
        {
            var color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}