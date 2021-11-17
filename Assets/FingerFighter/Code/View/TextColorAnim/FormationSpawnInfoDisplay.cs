using FingerFighter.Control.Factories.EnemySpawn;
using TMPro;
using UnityEngine;

namespace FingerFighter.View.TextColorAnim
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FormationSpawnInfoDisplay : TmpTextFade
    {
        private void Awake() 
            => EnemySpawnFormation.OnFormationSpawned += DisplayFormationInfo;

        private void OnDestroy() 
            => EnemySpawnFormation.OnFormationSpawned -= DisplayFormationInfo;

        private void DisplayFormationInfo(string packId, string formationId)
        {
            var str = $"{packId} : {formationId}";
            SetText(str);
            ResetDurationTimer();
        }
    }
}